using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Helpers;
using WinForms.PopUps;

namespace WinForms.AdminForms
{
    public partial class UserControlSettings : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly UpdateService _updateService;

        // Versión más nueva encontrada al buscar actualizaciones (null = ya está actualizado).
        private ReleaseInfo? _availableUpdate;

        public UserControlSettings(IServiceProvider serviceProvider, IConfiguration config, UpdateService updateService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _config = config;
            _updateService = updateService;

            this.Load += UserControlSettings_Load;
        }

        private void btnRecicle_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<FormRecoverElements>();
            form.ShowDialog();
        }

        private void btnPendingInvoices_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<FormPendingInvoices>();
            form.ShowDialog();
        }

        private void UserControlSettings_Load(object sender, EventArgs e)
        {
            // Cargar impresoras de Windows
            cbxPrinters.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cbxPrinters.Items.Add(printer);
            }

            string savedPath = _config["ApiPath"] ?? "";
            if (File.Exists(MobileApi.BundledPath))
                lblApiPath.Text = "Incluido en el sistema";
            else
                lblApiPath.Text = string.IsNullOrWhiteSpace(savedPath) ? "Ruta no configurada" : savedPath;

            cbxPrinters.Text = _config.GetSection("Arca")["PrinterName"] ?? "";

            txtMobilePin.Text = _config[MobileApi.PinKey] ?? "";

            lblVersion.Text = $"Versión instalada: {ReleaseInfo.VersionToText(UpdateService.CurrentVersion)}";
        }

        #region Actualizaciones

        private async void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            SetUpdateBusy(true, "Buscando actualizaciones...");
            btnInstallUpdate.Visible = false;
            _availableUpdate = null;

            try
            {
                var releases = await _updateService.GetReleasesAsync();
                var latest = releases.FirstOrDefault();
                var actual = UpdateService.CurrentVersion;

                if (latest == null)
                {
                    lblUpdateStatus.Text = "Todavía no hay versiones publicadas.";
                }
                else if (latest.Version > actual)
                {
                    _availableUpdate = latest;
                    lblUpdateStatus.Text = $"Hay una nueva versión: {latest.VersionText}\n{Resumen(latest.Notes)}";
                    btnInstallUpdate.Values.Text = $"INSTALAR {latest.VersionText}";
                    btnInstallUpdate.Visible = true;
                }
                else
                {
                    lblUpdateStatus.Text = $"El sistema está actualizado (versión {ReleaseInfo.VersionToText(actual)}).";
                }
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "No se pudo consultar si hay actualizaciones. Revise la conexión a internet.";
                MessageBox.Show(ex.Message, "Actualizaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                SetUpdateBusy(false);
            }
        }

        private async void btnInstallUpdate_Click(object sender, EventArgs e)
        {
            if (_availableUpdate != null) await InstallReleaseAsync(_availableUpdate);
        }

        private async void btnPreviousVersions_Click(object sender, EventArgs e)
        {
            SetUpdateBusy(true, "Buscando versiones publicadas...");
            IReadOnlyList<ReleaseInfo> releases;

            try
            {
                releases = await _updateService.GetReleasesAsync();
                lblUpdateStatus.Text = "";
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "No se pudo consultar las versiones. Revise la conexión a internet.";
                MessageBox.Show(ex.Message, "Actualizaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                SetUpdateBusy(false);
            }

            if (releases.Count == 0)
            {
                MessageBox.Show("Todavía no hay versiones publicadas.", "Versiones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = new FormVersions();
            form.LoadReleases(releases);
            if (form.ShowDialog(this) == DialogResult.OK && form.SelectedRelease != null)
            {
                await InstallReleaseAsync(form.SelectedRelease);
            }
        }

        /// <summary>
        /// Descarga e instala una versión (más nueva o anterior). Si sale bien, el sistema se
        /// cierra, el instalador hace su trabajo con la barra de progreso visible y lo reabre.
        /// </summary>
        private async Task InstallReleaseAsync(ReleaseInfo release)
        {
            var actual = UpdateService.CurrentVersion;
            bool esAnterior = release.Version < actual;

            // Volver atrás solo si esa versión entiende la base tal como está hoy.
            if (esAnterior)
            {
                bool? compatible;
                try
                {
                    string? migracionBase = await Task.Run(() => _updateService.GetLatestAppliedMigration());
                    compatible = UpdateService.IsCompatibleWithDatabase(release, migracionBase);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo verificar la base de datos: " + ex.Message, "Versiones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (compatible == false)
                {
                    MessageBox.Show(
                        $"No se puede volver a la versión {release.VersionText}.\n\n" +
                        "La base de datos ya tiene cambios de estructura que esa versión no conoce, y el sistema podría dejar de funcionar.\n\n" +
                        "Si necesita volver atrás, contacte al soporte.",
                        "Versión incompatible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (compatible == null)
                {
                    var seguir = MessageBox.Show(
                        $"No se puede verificar si la versión {release.VersionText} es compatible con la base de datos actual.\n\n¿Instalarla igual?",
                        "Versiones", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (seguir != DialogResult.Yes) return;
                }
            }

            string mensaje = esAnterior
                ? $"Se va a instalar la versión {release.VersionText}, ANTERIOR a la actual ({ReleaseInfo.VersionToText(actual)}).\n\n"
                : $"Se va a instalar la versión {release.VersionText}.\n\n" +
                  (string.IsNullOrWhiteSpace(release.Notes) ? "" : $"Novedades:\n{release.Notes}\n\n");

            mensaje += "El sistema se va a cerrar mientras se instala y se vuelve a abrir solo al terminar. " +
                       "Las ventas en espera no se pierden.\n\nWindows va a pedir permiso de administrador.\n\n¿Continuar?";

            var confirmar = MessageBox.Show(mensaje, "Actualizar sistema", MessageBoxButtons.YesNo,
                esAnterior ? MessageBoxIcon.Warning : MessageBoxIcon.Question,
                esAnterior ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1);
            if (confirmar != DialogResult.Yes) return;

            string installerPath;
            SetUpdateBusy(true, $"Descargando versión {release.VersionText}...");
            pbUpdate.Value = 0;
            pbUpdate.Visible = true;

            try
            {
                var progress = new Progress<int>(p =>
                {
                    pbUpdate.Value = Math.Clamp(p, 0, 100);
                    lblUpdateStatus.Text = $"Descargando versión {release.VersionText}... {p}%";
                });

                installerPath = await _updateService.DownloadAsync(release, progress);
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "No se pudo descargar la actualización. No se instaló nada.";
                MessageBox.Show(ex.Message, "Actualizaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetUpdateBusy(false);
                pbUpdate.Visible = false;
                return;
            }

            try
            {
                lblUpdateStatus.Text = "Iniciando la instalación...";
                UpdateService.LaunchInstaller(installerPath);
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                // 1223 = el usuario respondió "No" al pedido de permisos de Windows.
                lblUpdateStatus.Text = "Se canceló el permiso de administrador. No se instaló nada.";
                SetUpdateBusy(false);
                pbUpdate.Visible = false;
                return;
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "No se pudo iniciar la instalación.";
                MessageBox.Show(ex.Message, "Actualizaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetUpdateBusy(false);
                pbUpdate.Visible = false;
                return;
            }

            // El instalador espera a que el sistema se cierre del todo antes de copiar archivos.
            System.Windows.Forms.Application.Exit();
        }

        private void SetUpdateBusy(bool busy, string? status = null)
        {
            btnCheckUpdates.Enabled = !busy;
            btnInstallUpdate.Enabled = !busy;
            btnPreviousVersions.Enabled = !busy;
            if (status != null) lblUpdateStatus.Text = status;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        /// <summary>Primeras líneas de las notas, para que entren en la pantalla.</summary>
        private static string Resumen(string notas)
        {
            var lineas = notas.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string texto = string.Join("\n", lineas.Take(2));
            return lineas.Length > 2 ? texto + "\n..." : texto;
        }

        #endregion

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ejecutables (*.exe)|*.exe";
                ofd.Title = "Seleccione el servidor Web.exe";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    lblApiPath.Text = ofd.FileName;
                }
            }
        }

        private void btnSavePrinter_Click(object sender, EventArgs e)
        {
            try
            {
                string pathParaGuardar = lblApiPath.Text.Trim();
                if (pathParaGuardar == "Ruta no configurada" || pathParaGuardar == "Incluido en el sistema")
                {
                    pathParaGuardar = "";
                }

                // Se guarda en la configuración de esta PC (ProgramData), no en la carpeta del
                // programa: así una actualización no la pisa y no hacen falta permisos de admin.
                LocalSettings.Save(new Dictionary<string, string>
                {
                    ["ApiPath"] = pathParaGuardar,
                    ["Arca:PrinterName"] = cbxPrinters.Text.Trim()
                });

                MessageBox.Show("Configuración guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeneratePin_Click(object sender, EventArgs e)
        {
            txtMobilePin.Text = MobileApi.GeneratePin();
        }

        private void btnSavePin_Click(object sender, EventArgs e)
        {
            string pin = txtMobilePin.Text.Trim();
            if (pin.Length < 4)
            {
                MessageBox.Show("El PIN tiene que tener al menos 4 caracteres.", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // El servidor del celular lee este mismo archivo y toma el PIN nuevo al instante:
                // los celulares que tenían el anterior dejan de poder conectarse.
                LocalSettings.Save(new Dictionary<string, string> { [MobileApi.PinKey] = pin });
                MessageBox.Show("PIN guardado. Cargá el mismo PIN en Ajustes de la app del celular.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
