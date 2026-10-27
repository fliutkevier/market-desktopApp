using System.Security.Cryptography;
using System.Text;

namespace Web.Security
{
    /// <summary>
    /// Todo pedido del celular tiene que traer el PIN que se configura en Ajustes de Vertice
    /// (header X-Vertice-Pin). Sin esto, cualquiera conectado al wifi del local podría leer las
    /// ventas. Solo /health queda libre, para que el celular sepa si la PC responde.
    /// </summary>
    public class PinMiddleware
    {
        public const string HeaderName = "X-Vertice-Pin";
        public const string ConfigKey = "MobileApi:Pin";

        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public PinMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/health"))
            {
                await _next(context);
                return;
            }

            // Se lee en cada pedido: si cambian el PIN en Ajustes, vale al instante sin reiniciar.
            string? expected = _config[ConfigKey];
            if (string.IsNullOrWhiteSpace(expected))
            {
                await RejectAsync(context, "El PIN del celular no está configurado en Ajustes de Vertice.");
                return;
            }

            string received = context.Request.Headers[HeaderName].ToString();
            if (!CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(received), Encoding.UTF8.GetBytes(expected.Trim())))
            {
                await RejectAsync(context, "PIN incorrecto.");
                return;
            }

            await _next(context);
        }

        private static async Task RejectAsync(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(message);
        }
    }
}
