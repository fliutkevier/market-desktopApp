using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WinForms.Helpers
{
    /// <summary>Una versión publicada en GitHub Releases.</summary>
    public class ReleaseInfo
    {
        public Version Version { get; init; } = new Version(0, 0, 0);
        public string Notes { get; init; } = string.Empty;
        public DateTime PublishedAt { get; init; }
        public string AssetName { get; init; } = string.Empty;
        public string DownloadUrl { get; init; } = string.Empty;
        public long Size { get; init; }

        /// <summary>SHA-256 que calcula GitHub al subir el archivo (null si no lo informa).</summary>
        public string? Sha256 { get; init; }

        /// <summary>
        /// Última migración de base que trae esa versión (la escribe publicar.ps1 en las notas).
        /// Sirve para saber si se puede volver a esa versión sin romper la base.
        /// </summary>
        public string? LastMigration { get; init; }

        public string VersionText => VersionToText(Version);

        public static string VersionToText(Version v) => $"{v.Major}.{v.Minor}.{Math.Max(v.Build, 0)}";
    }

    /// <summary>
    /// Actualizaciones desde los Releases del repo de GitHub (Updates:Repo en appsettings.json).
    /// Cada release es un tag "vX.Y.Z" con el instalador de Inno Setup adjunto.
    /// </summary>
    public class UpdateService
    {
        private const string InstallerPrefix = "Instalador_VerticePOS_";
        private static readonly Regex MigrationMarker = new Regex(@"<!--\s*migracion:\s*(\S+)\s*-->", RegexOptions.IgnoreCase);

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public UpdateService(IHttpClientFactory httpClientFactory, IConfiguration config, IServiceProvider serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _serviceProvider = serviceProvider;
        }

        /// <summary>Versión del sistema que está corriendo (Version del WinForms.csproj).</summary>
        public static Version CurrentVersion
        {
            get
            {
                var v = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(0, 0, 0);
                return new Version(v.Major, v.Minor, Math.Max(v.Build, 0));
            }
        }

        /// <summary>Todas las versiones publicadas, de la más nueva a la más vieja.</summary>
        public async Task<IReadOnlyList<ReleaseInfo>> GetReleasesAsync(CancellationToken cancellationToken = default)
        {
            string repo = _config["Updates:Repo"] ?? throw new InvalidOperationException("Falta Updates:Repo en la configuración.");

            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{repo}/releases?per_page=100");
            request.Headers.UserAgent.ParseAdd("VerticePOS-Updater");
            request.Headers.Accept.ParseAdd("application/vnd.github+json");

            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(20);

            using var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"GitHub respondió {(int)response.StatusCode} ({response.ReasonPhrase}).");

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

            var releases = new List<ReleaseInfo>();

            foreach (var rel in json.RootElement.EnumerateArray())
            {
                if (rel.GetProperty("draft").GetBoolean() || rel.GetProperty("prerelease").GetBoolean()) continue;

                string tag = rel.GetProperty("tag_name").GetString() ?? "";
                if (!Version.TryParse(tag.TrimStart('v', 'V'), out var version)) continue;

                JsonElement? asset = null;
                foreach (var a in rel.GetProperty("assets").EnumerateArray())
                {
                    string name = a.GetProperty("name").GetString() ?? "";
                    if (name.StartsWith(InstallerPrefix, StringComparison.OrdinalIgnoreCase)
                        && name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        asset = a;
                        break;
                    }
                }
                if (asset == null) continue; // Release sin instalador: no se puede instalar

                string body = rel.TryGetProperty("body", out var b) ? b.GetString() ?? "" : "";
                var marker = MigrationMarker.Match(body);

                string? sha256 = null;
                if (asset.Value.TryGetProperty("digest", out var d) && d.ValueKind == JsonValueKind.String)
                {
                    string digest = d.GetString() ?? "";
                    if (digest.StartsWith("sha256:", StringComparison.OrdinalIgnoreCase))
                        sha256 = digest.Substring("sha256:".Length);
                }

                releases.Add(new ReleaseInfo
                {
                    Version = new Version(version.Major, version.Minor, Math.Max(version.Build, 0)),
                    Notes = MigrationMarker.Replace(body, "").Trim(),
                    PublishedAt = rel.GetProperty("published_at").GetDateTime().ToLocalTime(),
                    AssetName = asset.Value.GetProperty("name").GetString() ?? "",
                    DownloadUrl = asset.Value.GetProperty("browser_download_url").GetString() ?? "",
                    Size = asset.Value.GetProperty("size").GetInt64(),
                    Sha256 = sha256,
                    LastMigration = marker.Success ? marker.Groups[1].Value : null
                });
            }

            return releases.OrderByDescending(r => r.Version).ToList();
        }

        /// <summary>
        /// Baja el instalador a la carpeta temporal y verifica que llegó completo y sin cambios
        /// (tamaño y SHA-256). Devuelve la ruta del archivo.
        /// </summary>
        public async Task<string> DownloadAsync(ReleaseInfo release, IProgress<int> progress, CancellationToken cancellationToken = default)
        {
            string folder = Path.Combine(Path.GetTempPath(), "VerticePOS_Update");
            if (Directory.Exists(folder))
            {
                foreach (var old in Directory.GetFiles(folder))
                {
                    try { File.Delete(old); } catch { /* en uso: se ignora */ }
                }
            }
            Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, release.AssetName);

            var client = _httpClientFactory.CreateClient();
            client.Timeout = Timeout.InfiniteTimeSpan; // ~60 MB: el límite lo pone el CancellationToken

            using var request = new HttpRequestMessage(HttpMethod.Get, release.DownloadUrl);
            request.Headers.UserAgent.ParseAdd("VerticePOS-Updater");

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            long total = response.Content.Headers.ContentLength ?? release.Size;

            using (var input = await response.Content.ReadAsStreamAsync(cancellationToken))
            using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true))
            {
                var buffer = new byte[81920];
                long read = 0;
                int lastPercent = -1;
                int n;

                while ((n = await input.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await output.WriteAsync(buffer.AsMemory(0, n), cancellationToken);
                    read += n;

                    if (total > 0)
                    {
                        int percent = (int)(read * 100 / total);
                        if (percent != lastPercent)
                        {
                            lastPercent = percent;
                            progress.Report(percent);
                        }
                    }
                }
            }

            var info = new FileInfo(path);
            if (release.Size > 0 && info.Length != release.Size)
                throw new InvalidOperationException("La descarga quedó incompleta. Vuelva a intentarlo.");

            if (!string.IsNullOrEmpty(release.Sha256))
            {
                string hash;
                using (var fs = File.OpenRead(path))
                    hash = Convert.ToHexString(await SHA256.HashDataAsync(fs, cancellationToken));

                if (!hash.Equals(release.Sha256, StringComparison.OrdinalIgnoreCase))
                {
                    try { File.Delete(path); } catch { }
                    throw new InvalidOperationException("El instalador descargado no coincide con el publicado (verificación SHA-256). No se instaló nada.");
                }
            }

            return path;
        }

        /// <summary>Última migración aplicada en la base de esta PC.</summary>
        public string? GetLatestAppliedMigration()
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Los Id de migración empiezan con la fecha (yyyyMMddHHmmss): el orden alfabético es el cronológico.
            return db.Database.GetAppliedMigrations().OrderBy(m => m, StringComparer.Ordinal).LastOrDefault();
        }

        /// <summary>
        /// Una versión anterior funciona con esta base solo si ya conocía todos los cambios de
        /// estructura que tiene. null = no se puede saber (release publicado sin la marca).
        /// </summary>
        public static bool? IsCompatibleWithDatabase(ReleaseInfo release, string? latestAppliedMigration)
        {
            if (string.IsNullOrEmpty(latestAppliedMigration)) return true;
            if (string.IsNullOrEmpty(release.LastMigration)) return null;

            return string.CompareOrdinal(release.LastMigration, latestAppliedMigration) >= 0;
        }

        /// <summary>
        /// Lanza el instalador con la barra de progreso visible (/SILENT) y sin preguntas.
        /// Windows pide permiso de administrador (el instalador lo requiere). Después de esto el
        /// sistema se tiene que cerrar: el instalador espera a que lo haga y al terminar lo reabre.
        /// </summary>
        public static void LaunchInstaller(string installerPath)
        {
            string log = Path.Combine(Path.GetDirectoryName(installerPath)!, "instalacion.log");

            Process.Start(new ProcessStartInfo
            {
                FileName = installerPath,
                Arguments = $"/SILENT /SUPPRESSMSGBOXES /NORESTART /CLOSEAPPLICATIONS /LOG=\"{log}\"",
                UseShellExecute = true // necesario para que Windows muestre el pedido de permisos
            });
        }
    }
}
