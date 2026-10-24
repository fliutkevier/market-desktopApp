using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    /// <summary>
    /// Factura directo contra los web services SOAP de ARCA (ex AFIP): WSAA para
    /// autenticación y WSFEv1 para pedir el CAE. No depende de ningún intermediario
    /// de terceros (reemplaza al viejo FiscalServiceAfipSDK, que usaba AfipSDK).
    /// </summary>
    public class FiscalServiceArca : IFiscalService
    {
        private const string WsaaUrlProd = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string WsaaUrlHomo = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string WsfeUrlProd = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
        private const string WsfeUrlHomo = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx";
        private const string WsfeNamespace = "http://ar.gov.afip.dif.FEV1/";
        private const string WsaaNamespace = "http://wsaa.view.sua.dvadac.desein.afip.gov";

        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        // El WSAA rechaza un TA nuevo si todavía hay uno vigente para el mismo servicio, y ARCA
        // exige numeración de comprobantes estrictamente correlativa: nunca hay que pedir un CAE
        // en paralelo a otro pedido para el mismo punto de venta (venta en el momento vs. cola).
        private static readonly SemaphoreSlim ArcaLock = new(1, 1);
        private static (string Token, string Sign, DateTime ExpiresAt)? _cachedTicket;

        public FiscalServiceArca(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        private bool EsProduccion => string.Equals(_config["Arca:Environment"], "prod", StringComparison.OrdinalIgnoreCase);
        private string WsaaUrl => EsProduccion ? WsaaUrlProd : WsaaUrlHomo;
        private string WsfeUrl => EsProduccion ? WsfeUrlProd : WsfeUrlHomo;

        public async Task<FiscalDocument> GenerateInvoiceAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            var (token, sign) = await ObtenerAutorizacionAsync(cancellationToken);

            int ptoVta = int.Parse(_config["Arca:PuntoVenta"] ?? throw new InvalidOperationException("Falta Arca:PuntoVenta en la configuración."));
            string cuitEmisor = _config["Arca:Cuit"] ?? throw new InvalidOperationException("Falta Arca:Cuit en la configuración.");

            // 11 = Factura C (monotributo). 1 = A, 6 = B (responsable inscripto).
            int cbteTipo = int.TryParse(_config["Arca:TipoComprobante"], out var tipoConfigurado) ? tipoConfigurado : 11;
            int docTipo = 99;
            long docNro = 0;

            // Un monotributista no discrimina IVA: el total va entero como neto, ImpIVA en 0, y el
            // bloque <Iva> NO se manda. Si se manda igual, ARCA rechaza con error 10048.
            bool esComprobanteC = cbteTipo is 11 or 12 or 13;

            // Reservar número + pedir CAE deben ser atómicos entre sí (venta en el momento vs. cola).
            await ArcaLock.WaitAsync(cancellationToken);
            try
            {
                int proximoNumero = await ObtenerProximoNumeroComprobanteAsync(token, sign, cuitEmisor, ptoVta, cbteTipo, cancellationToken);

                decimal total = Math.Round(sale.Total, 2);
                decimal neto = total;
                decimal iva = 0m;

                if (!esComprobanteC)
                {
                    neto = Math.Round(total / 1.21m, 2);
                    iva = Math.Round(total - neto, 2);
                    if (neto + iva != total) iva = total - neto; // Ajuste centavos
                }

                string totalStr = total.ToString(CultureInfo.InvariantCulture);
                string netoStr = neto.ToString(CultureInfo.InvariantCulture);
                string ivaStr = iva.ToString(CultureInfo.InvariantCulture);
                int condicionIvaReceptorId = (cbteTipo == 1) ? 1 : 5; // 1=Resp Insc, 5=Cons Final

                string bloqueIva = esComprobanteC
                    ? string.Empty
                    : $"""
                                        <ar:Iva>
                                          <ar:AlicIva>
                                            <ar:Id>5</ar:Id>
                                            <ar:BaseImp>{netoStr}</ar:BaseImp>
                                            <ar:Importe>{ivaStr}</ar:Importe>
                                          </ar:AlicIva>
                                        </ar:Iva>
                      """;

                string envelope =
                    $"""
                    <?xml version="1.0" encoding="utf-8"?>
                    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ar="{WsfeNamespace}">
                      <soapenv:Header/>
                      <soapenv:Body>
                        <ar:FECAESolicitar>
                          <ar:Auth>
                            <ar:Token>{token}</ar:Token>
                            <ar:Sign>{sign}</ar:Sign>
                            <ar:Cuit>{cuitEmisor}</ar:Cuit>
                          </ar:Auth>
                          <ar:FeCAEReq>
                            <ar:FeCabReq>
                              <ar:CantReg>1</ar:CantReg>
                              <ar:PtoVta>{ptoVta}</ar:PtoVta>
                              <ar:CbteTipo>{cbteTipo}</ar:CbteTipo>
                            </ar:FeCabReq>
                            <ar:FeDetReq>
                              <ar:FECAEDetRequest>
                                <ar:Concepto>1</ar:Concepto>
                                <ar:DocTipo>{docTipo}</ar:DocTipo>
                                <ar:DocNro>{docNro}</ar:DocNro>
                                <ar:CbteDesde>{proximoNumero}</ar:CbteDesde>
                                <ar:CbteHasta>{proximoNumero}</ar:CbteHasta>
                                <ar:CbteFch>{sale.DateAndTime:yyyyMMdd}</ar:CbteFch>
                                <ar:ImpTotal>{totalStr}</ar:ImpTotal>
                                <ar:ImpTotConc>0</ar:ImpTotConc>
                                <ar:ImpNeto>{netoStr}</ar:ImpNeto>
                                <ar:ImpOpEx>0</ar:ImpOpEx>
                                <ar:ImpTrib>0</ar:ImpTrib>
                                <ar:ImpIVA>{ivaStr}</ar:ImpIVA>
                                <ar:MonId>PES</ar:MonId>
                                <ar:MonCotiz>1</ar:MonCotiz>
                                <ar:CondicionIVAReceptorId>{condicionIvaReceptorId}</ar:CondicionIVAReceptorId>{bloqueIva}
                              </ar:FECAEDetRequest>
                            </ar:FeDetReq>
                          </ar:FeCAEReq>
                        </ar:FECAESolicitar>
                      </soapenv:Body>
                    </soapenv:Envelope>
                    """;

                var responseXml = await PostSoapAsync(WsfeUrl, $"{WsfeNamespace}FECAESolicitar", envelope, cancellationToken);

                string? resultadoCabecera = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "Resultado")?.Value;
                string? cae = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "CAE")?.Value;
                string? vto = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "CAEFchVto")?.Value;

                if (string.IsNullOrEmpty(cae) || resultadoCabecera == "R")
                {
                    string errorMsg = "ARCA rechazó la solicitud de CAE.";
                    var mensajes = responseXml.Descendants().Where(e => e.Name.LocalName == "Msg").Select(e => e.Value).ToList();
                    if (mensajes.Count > 0)
                        errorMsg += " " + string.Join(" | ", mensajes);

                    throw new Exception(errorMsg);
                }

                return new FiscalDocument
                {
                    IssuerCuit = cuitEmisor,
                    PointOfSale = ptoVta,
                    InvoiceType = cbteTipo,
                    InvoiceNumber = proximoNumero,
                    CAE = cae,
                    CAEExpirationDate = DateTime.ParseExact(vto!, "yyyyMMdd", null),
                    CustomerDocType = docTipo.ToString(),
                    CustomerDocNumber = docNro.ToString()
                };
            }
            finally
            {
                ArcaLock.Release();
            }
        }

        private async Task<int> ObtenerProximoNumeroComprobanteAsync(string token, string sign, string cuit, int ptoVta, int cbteTipo, CancellationToken cancellationToken)
        {
            string envelope =
                $"""
                <?xml version="1.0" encoding="utf-8"?>
                <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ar="{WsfeNamespace}">
                  <soapenv:Header/>
                  <soapenv:Body>
                    <ar:FECompUltimoAutorizado>
                      <ar:Auth>
                        <ar:Token>{token}</ar:Token>
                        <ar:Sign>{sign}</ar:Sign>
                        <ar:Cuit>{cuit}</ar:Cuit>
                      </ar:Auth>
                      <ar:PtoVta>{ptoVta}</ar:PtoVta>
                      <ar:CbteTipo>{cbteTipo}</ar:CbteTipo>
                    </ar:FECompUltimoAutorizado>
                  </soapenv:Body>
                </soapenv:Envelope>
                """;

            var responseXml = await PostSoapAsync(WsfeUrl, $"{WsfeNamespace}FECompUltimoAutorizado", envelope, cancellationToken);

            string? cbteNro = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "CbteNro")?.Value;

            if (cbteNro == null)
            {
                string? mensaje = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "Msg")?.Value;
                throw new Exception($"No se pudo obtener el último comprobante autorizado en ARCA.{(mensaje != null ? " " + mensaje : "")}");
            }

            return int.Parse(cbteNro) + 1;
        }

        private async Task<(string Token, string Sign)> ObtenerAutorizacionAsync(CancellationToken cancellationToken)
        {
            if (_cachedTicket is { } vigente && vigente.ExpiresAt > DateTime.Now)
                return (vigente.Token, vigente.Sign);

            await ArcaLock.WaitAsync(cancellationToken);
            try
            {
                // Puede que otro hilo ya haya renovado el ticket mientras esperábamos el lock.
                if (_cachedTicket is { } vigenteTrasLock && vigenteTrasLock.ExpiresAt > DateTime.Now)
                    return (vigenteTrasLock.Token, vigenteTrasLock.Sign);

                var cert = CargarCertificado();
                string traXml = ConstruirTra();
                string cmsBase64 = FirmarTra(traXml, cert);

                string envelope =
                    $"""
                    <?xml version="1.0" encoding="utf-8"?>
                    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:wsaa="{WsaaNamespace}">
                      <soapenv:Header/>
                      <soapenv:Body>
                        <wsaa:loginCms>
                          <wsaa:in0>{cmsBase64}</wsaa:in0>
                        </wsaa:loginCms>
                      </soapenv:Body>
                    </soapenv:Envelope>
                    """;

                var responseXml = await PostSoapAsync(WsaaUrl, "", envelope, cancellationToken);

                string? loginCmsReturn = responseXml.Descendants().FirstOrDefault(e => e.Name.LocalName == "loginCmsReturn")?.Value;
                if (string.IsNullOrEmpty(loginCmsReturn))
                    throw new Exception($"WSAA no devolvió una respuesta válida: {responseXml}");

                var loginTicket = XDocument.Parse(loginCmsReturn);
                string? token = loginTicket.Descendants("token").FirstOrDefault()?.Value;
                string? sign = loginTicket.Descendants("sign").FirstOrDefault()?.Value;
                string? expirationTimeStr = loginTicket.Descendants("expirationTime").FirstOrDefault()?.Value;

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(sign))
                    throw new Exception($"WSAA no devolvió Token/Sign: {loginCmsReturn}");

                // Margen de seguridad: renovamos 10 minutos antes de que venza el TA real (~12hs).
                DateTime expiresAt = DateTimeOffset.TryParse(expirationTimeStr, out var expiracion)
                    ? expiracion.LocalDateTime.AddMinutes(-10)
                    : DateTime.Now.AddHours(11);

                _cachedTicket = (token, sign, expiresAt);
                return (token, sign);
            }
            finally
            {
                ArcaLock.Release();
            }
        }

        private X509Certificate2 CargarCertificado()
        {
            string crtName = _config["Arca:CertName"] ?? throw new InvalidOperationException("Falta Arca:CertName en la configuración.");
            string keyName = _config["Arca:KeyName"] ?? throw new InvalidOperationException("Falta Arca:KeyName en la configuración.");

            string crtPath = BuscarArchivoCertificado(crtName);
            string keyPath = BuscarArchivoCertificado(keyName);

            if (!File.Exists(crtPath))
                throw new FileNotFoundException($"No se encontró el certificado. Ruta intentada: {crtPath}");
            if (!File.Exists(keyPath))
                throw new FileNotFoundException($"No se encontró la clave privada. Ruta intentada: {keyPath}");

            return X509Certificate2.CreateFromPemFile(crtPath, keyPath);
        }

        /// <summary>
        /// Los certificados y claves son de cada PC y nunca viajan en el instalador ni en el repo:
        /// se buscan en C:\ProgramData\VerticeApp\Certificados. La carpeta Services junto al .exe
        /// queda como respaldo para instalaciones viejas que todavía no se migraron.
        /// </summary>
        private static string BuscarArchivoCertificado(string fileName)
        {
            string principal = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "VerticeApp", "Certificados", fileName);

            if (File.Exists(principal)) return principal;

            string legacy = Path.Combine(AppContext.BaseDirectory, "Services", fileName);
            return File.Exists(legacy) ? legacy : principal;
        }

        private static string ConstruirTra()
        {
            DateTime ahora = DateTime.Now;
            long uniqueId = (long)(ahora - new DateTime(1970, 1, 1)).TotalSeconds;
            string generationTime = ahora.AddMinutes(-10).ToString("yyyy-MM-ddTHH:mm:sszzz");
            string expirationTime = ahora.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:sszzz");

            return
                $"""
                <?xml version="1.0" encoding="UTF-8"?>
                <loginTicketRequest version="1.0">
                  <header>
                    <uniqueId>{uniqueId}</uniqueId>
                    <generationTime>{generationTime}</generationTime>
                    <expirationTime>{expirationTime}</expirationTime>
                  </header>
                  <service>wsfe</service>
                </loginTicketRequest>
                """;
        }

        private static string FirmarTra(string traXml, X509Certificate2 cert)
        {
            var contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(traXml));
            var signedCms = new SignedCms(contentInfo, detached: false);
            var cmsSigner = new CmsSigner(cert) { IncludeOption = X509IncludeOption.EndCertOnly };
            signedCms.ComputeSignature(cmsSigner);
            return Convert.ToBase64String(signedCms.Encode());
        }

        private async Task<XDocument> PostSoapAsync(string url, string soapAction, string envelopeXml, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("Arca");

            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(envelopeXml, Encoding.UTF8, "text/xml")
            };
            // Los servicios SOAP 1.1 de ARCA son ASMX clásicos: exigen el header SOAPAction
            // presente y con el valor entre comillas literales (incluso vacío, para WSAA).
            request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{soapAction}\"");

            using var response = await client.SendAsync(request, cancellationToken);
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string detalle = ExtraerMensajeFalla(responseBody) ?? responseBody;
                throw new Exception($"Error HTTP {response.StatusCode} llamando a ARCA ({url}): {detalle}");
            }

            return XDocument.Parse(responseBody);
        }

        // Los errores SOAP 1.1 (WSAA/WSFEv1 caídos, TA inválido, certificado, etc.) viajan como
        // HTTP 500 con un <soap:Fault><faultstring>...</faultstring></soap:Fault>. Si lo hay,
        // mostramos ese texto en vez del XML crudo completo.
        private static string? ExtraerMensajeFalla(string responseBody)
        {
            try
            {
                var doc = XDocument.Parse(responseBody);
                return doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "faultstring")?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
