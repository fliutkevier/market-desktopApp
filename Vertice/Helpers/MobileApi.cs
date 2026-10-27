using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace WinForms.Helpers
{
    /// <summary>
    /// Servidor de la app del celular (proyecto Web, publicado como VerticeApi.exe). El
    /// instalador lo trae en {app}\Api y Vertice lo levanta en segundo plano al arrancar.
    /// </summary>
    public static class MobileApi
    {
        public const string PinKey = "MobileApi:Pin";

        /// <summary>Ruta del servidor que viene con el instalador.</summary>
        public static string BundledPath => Path.Combine(AppContext.BaseDirectory, "Api", "VerticeApi.exe");

        /// <summary>
        /// El que trae el instalador tiene prioridad: así se actualiza junto con el sistema.
        /// "ApiPath" de Ajustes queda solo para desarrollo o para un servidor puesto a mano.
        /// </summary>
        public static string? ResolvePath(IConfiguration config)
        {
            if (File.Exists(BundledPath)) return BundledPath;

            string? apiPath = config["ApiPath"];
            return !string.IsNullOrWhiteSpace(apiPath) && File.Exists(apiPath) ? apiPath : null;
        }

        /// <summary>PIN de 6 dígitos para que el celular pueda leer los datos del local.</summary>
        public static string GeneratePin() => RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
    }
}
