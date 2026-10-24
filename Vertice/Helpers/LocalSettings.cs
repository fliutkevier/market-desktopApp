using System.Text.Json;
using System.Text.Json.Nodes;

namespace WinForms.Helpers
{
    /// <summary>
    /// Configuración propia de cada PC: datos del contribuyente (Arca, Emisor), impresora y ruta
    /// de la API. Vive en C:\ProgramData\VerticeApp\appsettings.local.json, fuera de la carpeta
    /// del programa: no está en el repo, el instalador no la trae y las actualizaciones no la
    /// pisan. Se carga después de appsettings.json, así que sus valores tienen prioridad.
    /// </summary>
    public static class LocalSettings
    {
        public static string FolderPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "VerticeApp");

        public static string FilePath => Path.Combine(FolderPath, "appsettings.local.json");

        /// <summary>
        /// Guarda valores en el archivo local conservando todo lo demás que ya tenga.
        /// Las claves usan el formato de IConfiguration: "ApiPath", "Arca:PrinterName".
        /// </summary>
        public static void Save(IDictionary<string, string> values)
        {
            Directory.CreateDirectory(FolderPath);

            JsonObject root = new JsonObject();
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                if (!string.IsNullOrWhiteSpace(json) && JsonNode.Parse(json) is JsonObject existing)
                    root = existing;
            }

            foreach (var (key, value) in values)
            {
                string[] parts = key.Split(':');
                JsonObject node = root;

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (node[parts[i]] is not JsonObject child)
                    {
                        child = new JsonObject();
                        node[parts[i]] = child;
                    }
                    node = child;
                }

                node[parts[^1]] = value;
            }

            // Se escribe en un temporal y después se reemplaza: si se corta la luz a mitad de
            // la escritura, el archivo bueno no queda a medias.
            string tempPath = FilePath + ".tmp";
            // UnsafeRelaxedJsonEscaping: guarda "Almacén" tal cual y no "Almacén", para que el
            // archivo se pueda leer y editar a mano. Es un archivo local, nunca va a una página web.
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            File.WriteAllText(tempPath, root.ToJsonString(options));
            File.Move(tempPath, FilePath, overwrite: true);
        }
    }
}
