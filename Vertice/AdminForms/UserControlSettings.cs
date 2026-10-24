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

        public UserControlSettings(IServiceProvider serviceProvider, IConfiguration config)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _config = config;

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
            lblApiPath.Text = string.IsNullOrWhiteSpace(savedPath) ? "Ruta no configurada" : savedPath;

            cbxPrinters.Text = _config.GetSection("Arca")["PrinterName"] ?? "";
        }

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
                if (pathParaGuardar == "Ruta no configurada")
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
    }
}
