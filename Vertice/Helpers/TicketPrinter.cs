using Application.AuditLogs.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinForms.Helpers
{
    public class TicketPrinter
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUnitOfWork _unitOfWork;

        // Los datos del emisor salen de appsettings.json (sección "Emisor"). Estaban hardcodeados
        // acá y hubo que tocar código para cambiar de contribuyente; ahora se editan sin recompilar.
        private readonly string _shopName;
        private readonly string _shopFantasyName;
        private readonly string _shopAddress;
        private readonly string _shopPhone;
        private readonly string _empresaCondicionIva;
        private readonly string _shopLocality;
        private readonly string _ingBrutos;
        private readonly string _inicioAct;

        public TicketPrinter(IAuditLogService auditLogService, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _auditLogService = auditLogService;
            _unitOfWork = unitOfWork;

            var emisor = config.GetSection("Emisor");
            _shopName = emisor["RazonSocial"] ?? "";
            _shopFantasyName = emisor["NombreFantasia"] ?? _shopName;
            _shopAddress = emisor["Domicilio"] ?? "";
            _shopPhone = emisor["Telefono"] ?? "";
            _empresaCondicionIva = emisor["CondicionIva"] ?? "";
            _shopLocality = emisor["Localidad"] ?? "";
            _ingBrutos = emisor["IngresosBrutos"] ?? "";
            _inicioAct = emisor["InicioActividades"] ?? "";
        }

        private const int PaperWidth = 270;
        private const int ContentMargin = 2;
        private const int ContentWidth = PaperWidth - (ContentMargin * 2);

        public async void PrintTicket(int saleNumber, DateTime date, List<TicketItem> items, decimal total, string paymentMethod, decimal paysWith, decimal change, string nombreImpresora = null, string? note = null)
        {
            PrintDocument pd = CreatePrintDocument(nombreImpresora);
            pd.PrintPage += (sender, e) => ConfigurarPaginaNoFiscal(e, saleNumber, date, items, total, paymentMethod, paysWith, change, note);
            Print(pd);
            await _auditLogService.LogAsync("TICKET NO FISCAL impreso", $"Venta: {saleNumber}");
        }

        private void ConfigurarPaginaNoFiscal(PrintPageEventArgs e, int saleNumber, DateTime date, List<TicketItem> items, decimal total, string paymentMethod, decimal paysWith, decimal change, string? note = null)
        {
            Graphics g = e.Graphics;
            SetupGraphics(g);

            // Fuentes
            Font fontTitle = new Font("Arial", 11, FontStyle.Bold);
            Font fontSubtitle = new Font("Arial", 10, FontStyle.Bold);
            Font fontRegular = new Font("Arial", 8, FontStyle.Regular);
            Font fontBold = new Font("Arial", 8, FontStyle.Bold);
            Font fontTotal = new Font("Arial", 12, FontStyle.Bold);
            Font fontSmall = new Font("Arial", 7, FontStyle.Regular); // Para detalles chicos

            Brush brush = Brushes.Black;
            float y = 10;

            // Helpers
            Action<string, Font> drawCenter = (txt, fnt) => DrawCentered(g, txt, fnt, brush, ref y);
            Action<string, string, Font> drawDual = (l, r, fnt) => DrawDual(g, l, r, fnt, brush, ContentMargin, ContentWidth, ref y);
            Action drawLine = () => drawCenter(new string('-', 55), fontRegular);

            //CABECERA LIMPIA
            drawCenter(_shopName, fontTitle);
            if (!string.IsNullOrWhiteSpace(_shopFantasyName) && _shopFantasyName != _shopName)
                drawCenter(_shopFantasyName, fontRegular);
            drawCenter(_shopAddress, fontRegular);
            drawCenter(_shopLocality, fontRegular);
            drawCenter(_shopPhone, fontRegular);
            drawLine();

            //DATOS VENTA
            drawDual($"Venta #: {saleNumber}", $"{date:dd/MM/yyyy}", fontBold);
            drawDual("", $"{date:HH:mm}", fontRegular);
            drawLine();

            //ITEMS
            DrawItemsHeader(g, fontBold, brush, ref y);
            foreach (var item in items)
            {
                DrawItemRow(g, item, fontRegular, brush, ref y);
            }
            drawLine();

            //TOTALES
            drawDual("TOTAL", total.ToString("C2"), fontTotal);
            y += 10;

            if (!string.IsNullOrEmpty(paymentMethod))
            {
                drawDual("Forma de Pago:", paymentMethod.ToUpper(), fontBold);
                if (paysWith > 0)
                {
                    drawDual("Su Pago:", paysWith.ToString("C2"), fontRegular);
                    drawDual("Su Vuelto:", change.ToString("C2"), fontRegular);
                }
            }

            y += 20;
            drawCenter("¡GRACIAS POR SU COMPRA!", fontBold);

            //LEYENDA NO FISCAL
            y += 5;
            drawCenter("Ticket no válido como factura", new Font("Arial", 7, FontStyle.Italic));

            if (!string.IsNullOrEmpty(note))
            {
                y += 5;
                drawCenter(note, new Font("Arial", 7, FontStyle.Bold));
            }
        }


        public async void ImprimirFactura(AfipQR datosQr, FiscalDocument datosFiscalesBD, string nombreCliente, string docCliente, List<TicketItem> items, DateTime fechaVenta, string nombreImpresora = null)
        {
            PrintDocument pd = CreatePrintDocument(nombreImpresora);
            pd.PrintPage += (sender, e) => ConfigurarPaginaFiscal(e, datosQr, datosFiscalesBD, nombreCliente, docCliente, items, fechaVenta);
            Print(pd);
            await _auditLogService.LogAsync("TICKET FISCAL impreso", "Venta Fiscal");
        }

        private void ConfigurarPaginaFiscal(PrintPageEventArgs e, AfipQR datosQr, FiscalDocument datosFiscalesBD, string nombreCliente, string docCliente, List<TicketItem> items, DateTime fechaVenta)
        {
            Graphics g = e.Graphics;
            SetupGraphics(g);

            // Fuentes 80mm
            Font fontTitle = new Font("Arial", 11, FontStyle.Bold);
            Font fontFactura = new Font("Arial", 12, FontStyle.Bold);
            Font fontHeader = new Font("Arial", 9, FontStyle.Bold);
            Font fontRegular = new Font("Arial", 8, FontStyle.Regular);
            Font fontBold = new Font("Arial", 8, FontStyle.Bold);
            Font fontTotal = new Font("Arial", 14, FontStyle.Bold);
            Font fontAfip = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);

            Brush brush = Brushes.Black;
            float y = 10;
            float left = ContentMargin;

            // Helpers
            Action<string, Font> drawCenter = (txt, fnt) => DrawCentered(g, txt, fnt, brush, ref y);
            Action<string, string, Font> drawDual = (l, r, fnt) => DrawDual(g, l, r, fnt, brush, ContentMargin, ContentWidth, ref y);
            Action drawLine = () => drawCenter(new string('-', 55), fontRegular);

            //CABECERA
            drawCenter(_shopName, fontTitle);
            drawCenter(_shopAddress, fontRegular);
            drawCenter(_shopLocality, fontRegular);
            drawCenter(_shopPhone, fontRegular);
            drawCenter(_empresaCondicionIva, fontBold);
            drawLine();

            //DATOS COMPROBANTE
            string letra = datosFiscalesBD.InvoiceType switch
            {
                1 or 2 or 3 => "A",
                6 or 7 or 8 => "B",
                11 or 12 or 13 => "C", // Monotributo
                _ => "B"
            };
            string codCmp = datosFiscalesBD.InvoiceType.ToString("D3");
            drawCenter($"FACTURA {letra}  (Cód. {codCmp})", fontFactura);

            drawDual($"P.V.: {datosQr.ptoVta:D5}", $"Nro. T.: {datosQr.nroCmp:D8}", fontHeader);

            drawDual($"Fecha: {fechaVenta:dd/MM/yyyy}", $"Hora: {fechaVenta:HH:mm:ss}", fontRegular);

            g.DrawString($"CUIT: {datosQr.cuit}", fontRegular, brush, left, y); y += 14;
            // En la config van solo los datos; la etiqueta la pone el ticket.
            if (!string.IsNullOrWhiteSpace(_ingBrutos))
            {
                g.DrawString($"Ing. Brutos: {_ingBrutos}", fontRegular, brush, left, y); y += 14;
            }
            if (!string.IsNullOrWhiteSpace(_inicioAct))
            {
                g.DrawString($"Inicio de Act.: {_inicioAct}", fontRegular, brush, left, y); y += 14;
            }
            y += 4;
            drawLine();

            //CLIENTE (Estilo "A CONSUMIDOR FINAL" destacado)
            y += 2;
            g.DrawString("A CONSUMIDOR FINAL", fontBold, brush, left, y);
            y += 18;

            if (datosQr.tipoDocRec != 99 && datosQr.tipoDocRec != 0)
            {
                string tipo = datosQr.tipoDocRec == 80 ? "CUIT" : "DNI";
                g.DrawString($"{tipo}: {docCliente}", fontRegular, brush, left, y);
                y += 14;
                g.DrawString($"Cliente: {nombreCliente}", fontRegular, brush, left, y);
                y += 14;
            }
            else
            {
                // Si es consumidor final, dejamos espacio limpio
            }

            drawLine();

            //ITEMS
            DrawItemsHeader(g, fontBold, brush, ref y);
            foreach (var item in items)
            {
                DrawItemRow(g, item, fontRegular, brush, ref y);
            }
            drawLine();

            // Un monotributista NO puede discriminar IVA ni informar IVA contenido: no es
            // responsable inscripto, así que en la Factura C solo va el total.
            bool esComprobanteC = letra == "C";

            //TOTALES
            if (datosFiscalesBD.InvoiceType == 1) // A
            {
                decimal neto = datosQr.importe / 1.21m;
                decimal iva = datosQr.importe - neto;
                drawDual("Neto Gravado:", neto.ToString("N2"), fontRegular);
                drawDual("IVA 21%:", iva.ToString("N2"), fontRegular);
            }

            //Total
            y += 5;
            drawDual("TOTAL", datosQr.importe.ToString("C2"), fontTotal);
            y += 5;

            if (!esComprobanteC)
            {
                //Texto de Transparencia
                Font fontTransparencia = new Font("Arial", 7, FontStyle.Regular);
                drawCenter("REG. TRANSPARENCIA FISCAL AL CONSUMIDOR", fontTransparencia);
                drawCenter("SOLO SON INFORMADOS IMPUESTOS NACIONALES", fontTransparencia);

                //IVA Contenido (Informativo)
                decimal ivaContenido = datosQr.importe - (datosQr.importe / 1.21m);
                drawDual("IVA CONTENIDO", ivaContenido.ToString("C2"), fontRegular);
            }

            /*
            y += 5;
            drawDual("TOTAL", datosQr.importe.ToString("C2"), fontTotal);
            y += 5;*/

            // 6. PIE FISCAL (Lado a Lado para 80mm)
            /*float qrSize = 95;
            Bitmap qrImg = GenerarImagenQrAfip(datosQr);
            g.DrawImage(qrImg, left + 5, y, qrSize, qrSize);

            float textX = left + qrSize + 15; // A la derecha del QR
            float textY = y + 5;

            g.DrawString("AFIP", fontAfip, Brushes.Gray, textX, textY); textY += 25;
            g.DrawString("Comprobante Autorizado", fontBold, brush, textX, textY); textY += 20;

            g.DrawString($"CAE: {datosFiscalesBD.CAE}", fontRegular, brush, textX, textY); textY += 14;
            g.DrawString($"Vto. CAE: {datosFiscalesBD.CAEExpirationDate:dd/MM/yyyy}", fontRegular, brush, textX, textY);

            y += qrSize + 15;
            drawCenter("GRACIAS POR SU COMPRA", fontBold);*/
            drawLine();

            //Textos AFIP Centrados
            drawCenter("AFIP", fontAfip);
            drawCenter("Comprobante Autorizado", fontBold);

            // Datos CAE grandes y claros
            drawCenter($"CAE: {datosFiscalesBD.CAE}", fontBold);
            drawCenter($"Vto. CAE: {datosFiscalesBD.CAEExpirationDate:dd/MM/yyyy}", fontBold);

            y += 10; // Espacio antes del QR

            //QR
            // Tamaño: 180px
            float qrSize = 180;
            Bitmap qrImg = GenerarImagenQrAfip(datosQr);
            float qrX = (PaperWidth - qrSize) / 2;

            g.DrawImage(qrImg, qrX, y, qrSize, qrSize);
            y += qrSize + 15;

            drawCenter("GRACIAS POR SU COMPRA", fontBold);
            y += 20;
        }

        private void DrawItemsHeader(Graphics g, Font font, Brush brush, ref float y)
        {
            g.DrawString("Cnt", font, brush, ContentMargin, y);
            g.DrawString("Descripción", font, brush, ContentMargin + 30, y);
            
            float wP = g.MeasureString("P.Unit", font).Width;
            g.DrawString("P.Unit", font, brush, ContentWidth - 60 - wP, y); 
            
            float wT = g.MeasureString("Total", font).Width;
            g.DrawString("Total", font, brush, ContentWidth - wT, y);
            y += 14;
        }

        private void DrawItemRow(Graphics g, TicketItem item, Font font, Brush brush, ref float y)
        {
            g.DrawString(item.Quantity.ToString(), font, brush, ContentMargin, y);

            decimal pUnit = item.Quantity > 0 ? item.Subtotal / item.Quantity : 0;
            string pUnitStr = pUnit.ToString("N2");
            float wUnit = g.MeasureString(pUnitStr, font).Width;
            g.DrawString(pUnitStr, font, brush, ContentWidth - 60 - wUnit, y);

            string totalStr = item.Subtotal.ToString("N2");
            float wTotal = g.MeasureString(totalStr, font).Width;
            g.DrawString(totalStr, font, brush, ContentWidth - wTotal, y);

            string desc = item.ProductName;
            float maxDescWidth = ContentWidth - 30 - 65 - 55;
            if (g.MeasureString(desc, font).Width > maxDescWidth)
            {
                int maxChars = 18;
                if (desc.Length > maxChars) desc = desc.Substring(0, maxChars) + "..";
            }
            g.DrawString(desc, font, brush, ContentMargin + 30, y);
            y += 12;
        }

        private PrintDocument CreatePrintDocument(string nombreImpresora)
        {
            PrintDocument pd = new PrintDocument();
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            pd.DefaultPageSettings.PaperSize = new PaperSize("Ticket80mm", PaperWidth, 10000);
            pd.PrintController = new StandardPrintController();

            if (!string.IsNullOrEmpty(nombreImpresora))
            {
                pd.PrinterSettings.PrinterName = nombreImpresora;
                if (!pd.PrinterSettings.IsValid) throw new Exception($"No se encuentra la impresora: '{nombreImpresora}'.");
            }
            return pd;
        }

        private void DrawCentered(Graphics g, string text, Font font, Brush brush, ref float y)
        {
            float w = g.MeasureString(text, font).Width;
            float x = (ContentWidth - w) / 2 + ContentMargin;
            if (x < ContentMargin) x = ContentMargin;
            g.DrawString(text, font, brush, x, y);
            y += font.Height + 1;
        }

        private void DrawDual(Graphics g, string l, string r, Font font, Brush brush, float left, float right, ref float y)
        {
            g.DrawString(l, font, brush, left, y);
            float wR = g.MeasureString(r, font).Width;
            g.DrawString(r, font, brush, right - wR, y);
            y += font.Height + 1;
        }

        private void SetupGraphics(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
        }

        private void Print(PrintDocument pd)
        {
            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error imprimiendo: " + ex.Message);
            }
        }

        //generación de QR
        private Bitmap GenerarImagenQrAfip(AfipQR datos)
        {
            string jsonString = JsonSerializer.Serialize(datos);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
            string base64Json = Convert.ToBase64String(bytes);
            string urlFinal = $"https://www.afip.gob.ar/fe/qr/?p={base64Json}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(urlFinal, QRCodeGenerator.ECCLevel.M);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(2);
                }
            }
        }

    }


    public class TicketItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
