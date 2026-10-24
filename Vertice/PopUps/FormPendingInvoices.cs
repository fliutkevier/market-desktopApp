using Application.Sales.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForms.PopUps
{
    public partial class FormPendingInvoices : Form
    {
        private readonly IFiscalQueueService _fiscalQueueService;

        public FormPendingInvoices(IFiscalQueueService fiscalQueueService)
        {
            InitializeComponent();
            _fiscalQueueService = fiscalQueueService;

            this.Text = "Facturación Pendiente";
            this.MaximizeBox = false;
            this.Load += FormPendingInvoices_Load;
        }

        private async void FormPendingInvoices_Load(object sender, EventArgs e)
        {
            await CargarPendientesAsync();
        }

        private async System.Threading.Tasks.Task CargarPendientesAsync()
        {
            try
            {
                var pendientes = await _fiscalQueueService.GetPendingAsync();
                dgvPending.DataSource = pendientes.ToList();
                ConfigGrid();
                lblCount.Text = $"{pendientes.Count()} factura(s) pendiente(s) de ARCA";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pendientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigGrid()
        {
            dgvPending.AutoGenerateColumns = true;
            dgvPending.ReadOnly = true;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.RowHeadersVisible = false;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPending.Columns["FiscalDocumentId"] != null) dgvPending.Columns["FiscalDocumentId"].Visible = false;
            if (dgvPending.Columns["SaleId"] != null) dgvPending.Columns["SaleId"].HeaderText = "Venta N°";
            if (dgvPending.Columns["SaleDate"] != null)
            {
                dgvPending.Columns["SaleDate"].HeaderText = "Fecha";
                dgvPending.Columns["SaleDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvPending.Columns["Total"] != null)
            {
                dgvPending.Columns["Total"].HeaderText = "Total";
                dgvPending.Columns["Total"].DefaultCellStyle.Format = "C2";
            }
            if (dgvPending.Columns["AttemptCount"] != null) dgvPending.Columns["AttemptCount"].HeaderText = "Intentos";
            if (dgvPending.Columns["LastAttemptAt"] != null)
            {
                dgvPending.Columns["LastAttemptAt"].HeaderText = "Último Intento";
                dgvPending.Columns["LastAttemptAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvPending.Columns["LastError"] != null) dgvPending.Columns["LastError"].HeaderText = "Último Error";
        }

        private async void btnProcessNow_Click(object sender, EventArgs e)
        {
            btnProcessNow.Enabled = false;
            try
            {
                var resultado = await _fiscalQueueService.ProcessPendingAsync();
                MessageBox.Show(
                    $"Procesadas: {resultado.Processed}\nFacturadas: {resultado.Approved}\nSiguen pendientes: {resultado.StillPending}",
                    "Facturación de pendientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await CargarPendientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al facturar pendientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcessNow.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
