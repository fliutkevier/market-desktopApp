using Application.ProviderMovements.Interfaces;
using Application.Providers.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.PopUps
{
    public partial class FormMovementDetails : Form
    {
        private readonly IProviderMovementService _movementService;
        private string? _currentFilePath;

        public FormMovementDetails(IProviderMovementService movementService)
        {
            InitializeComponent();
            _movementService = movementService;

            this.MaximizeBox = false;
        }
        
        public async void LoadFromGrid(string? filePath, string providerName, DateTime date, decimal total, int id)
        {
            var dto = await _movementService.GetForEditAsync(id);

            if (dto == null)
            {
                MessageBox.Show("No se encontró el movimiento.");
                this.Close();
                return;
            }

            lblProvider.Text = $"{providerName}";
            lblCuit.Text = $"{dto.ProviderCuit}";
            lblDate.Text = $"{dto.Date.ToString("dd/MM/yyyy")}"; ;
            lblTotal.Text = $"{dto.Total.ToString("C2")}";

            _currentFilePath = filePath;
            ConfigVisualize(filePath);
        }

        private void ConfigVisualize(string? path)
        {
            // Resetear estado
            pbxDetail.Visible = false;
            btnShow.Visible = false;
            lblNoFile.Visible = false;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                lblNoFile.Visible = true;
                return;
            }

            string ext = Path.GetExtension(path).ToLower();

            if (ext == ".pdf")
            {
                // Es PDF -> Botón
                btnShow.Visible = true;
                tlpMovementDetails.SetColumnSpan(lblProviderTitle, 2);
                tlpMovementDetails.SetColumnSpan(lblCuitTitle, 2);
                tlpMovementDetails.SetColumnSpan(lblTotalTitle, 2);
                tlpMovementDetails.SetColumnSpan(lblDateTitle, 2);
                tlpMovementDetails.SetColumnSpan(lblProvider, 2);
                tlpMovementDetails.SetColumnSpan(lblCuit, 2);
                tlpMovementDetails.SetColumnSpan(lblTotal, 2);
                tlpMovementDetails.SetColumnSpan(lblDate, 2);
                tlpMovementDetails.ColumnStyles[0].Width = 50;
                tlpMovementDetails.ColumnStyles[1].Width = 50;
                pbxDetail.Visible = false;
                tlpMovementDetails.ColumnCount = 2;
                this.Size = new Size(467, 700);
            }
            else if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                pbxDetail.Visible = true;
                btnShow.Visible = false;
                try
                {
                    // Image.FromStream para no bloquear el archivo
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        pbxDetail.Image = Image.FromStream(stream);
                    }
                    pbxDetail.Visible = true;
                }
                catch
                {
                    lblNoFile.Text = "Error al cargar imagen";
                    lblNoFile.Visible = true;
                }
            }
            else
            {
                btnShow.Text = "Abrir Archivo";
                btnShow.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(_currentFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo abrir el archivo: " + ex.Message);
                }
            }
        }
    }
}
