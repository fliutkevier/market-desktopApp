using Application.ProviderMovements.Dtos;
using Application.ProviderMovements.Interfaces;
using Application.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.PopUps;

namespace WinForms
{
    public partial class UserControlProvidersHistory : UserControl
    {
        private readonly IProviderMovementService _movementService;
        private readonly IProviderService _providerService;
        private readonly IServiceProvider _serviceProvider;

        private List<ProviderMovementGridDto> _movementsLoaded = new List<ProviderMovementGridDto>();

        public UserControlProvidersHistory(
            IProviderMovementService movementService,
            IProviderService providerService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _movementService = movementService;
            _providerService = providerService;
            _serviceProvider = serviceProvider;
        }

        private async void UserControlProvidersHistory_Load(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;

            if (rbtDesDate != null) rbtDesDate.Checked = true;

            await LoadProvidersCombo();
            await LoadMovements(null, null);
        }

        private async Task LoadProvidersCombo()
        {
            try
            {
                var providers = await _providerService.GetAllForGridAsync();

                var list = providers.Select(p => new { Cuit = p.Cuit, Display = p.Name }).ToList();
                list.Insert(0, new { Cuit = "ALL", Display = "TODOS LOS PROVEEDORES" });

                if (cbxProvidersFilter != null)
                {
                    cbxProvidersFilter.DataSource = list;
                    cbxProvidersFilter.DisplayMember = "Display";
                    cbxProvidersFilter.ValueMember = "Cuit";
                    cbxProvidersFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        private void ConfigGrid()
        {
            if (dgvMovements.Columns.Count == 0) return;
            dgvMovements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovements.MultiSelect = false;
            dgvMovements.RowHeadersVisible = false;
            dgvMovements.ReadOnly = true;

            //dgvMovements.Columns["Id"].HeaderText = "ID";
            //dgvMovements.Columns["ProviderName"].HeaderText = "Proveedor";
            //dgvMovements.Columns["ProviderCuit"].HeaderText = "CUIT";
            //dgvMovements.Columns["Date"].HeaderText = "Fecha";
            //dgvMovements.Columns["Total"].DefaultCellStyle.Format = "C2";
            //dgvMovements.Columns["HasFile"].HeaderText = "Archivo";
            //dgvMovements.Columns["FilePath"].Visible = false;

            /*dgvMovements.DataBindingComplete += (s, e) =>
            {
                if (dgvMovements.Columns["Id"].HeaderText != null) dgvMovements.Columns["Id"].HeaderText = "ID";
                if (dgvMovements.Columns["ProviderName"] != null) dgvMovements.Columns["ProviderName"].HeaderText = "Proveedor";
                if (dgvMovements.Columns["ProviderCuit"] != null) dgvMovements.Columns["ProviderCuit"].HeaderText = "CUIT";
                if (dgvMovements.Columns["Date"] != null) dgvMovements.Columns["Date"].HeaderText = "Fecha";
                if (dgvMovements.Columns["Total"] != null) dgvMovements.Columns["Total"].DefaultCellStyle.Format = "C2";
                if (dgvMovements.Columns["HasFile"] != null) dgvMovements.Columns["HasFile"].HeaderText = "Archivo";
                if (dgvMovements.Columns["FilePath"] != null) dgvMovements.Columns["FilePath"].Visible = false;
            };*/

            if (dgvMovements.Columns["Id"] != null) dgvMovements.Columns["Id"].HeaderText = "ID";

            if (dgvMovements.Columns["Date"] != null)
                dgvMovements.Columns["Date"].HeaderText = "Fecha";

            if (dgvMovements.Columns["ProviderName"] != null)
                dgvMovements.Columns["ProviderName"].HeaderText = "Proveedor";

            if (dgvMovements.Columns["ProviderCuit"] != null)
                dgvMovements.Columns["ProviderCuit"].HeaderText = "CUIT";

            if (dgvMovements.Columns["Total"] != null)
            {
                dgvMovements.Columns["Total"].HeaderText = "Total";
                dgvMovements.Columns["Total"].DefaultCellStyle.Format = "C2";
            }

            if (dgvMovements.Columns["HasFile"] != null)
            {
                dgvMovements.Columns["HasFile"].HeaderText = "Adjunto";
            }

            if (dgvMovements.Columns["FilePath"] != null) dgvMovements.Columns["FilePath"].Visible = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var from = DateOnly.FromDateTime(dtpSince.Value);
            var to = DateOnly.FromDateTime(dtpTo.Value);

            await LoadMovements(from, to);
        }

        private async Task LoadMovements(DateOnly? from, DateOnly? to)
        {
            try
            {
                var list = await _movementService.GetHistoryAsync(from, to);
                _movementsLoaded = list.ToList();

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar movimientos: " + ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (_movementsLoaded == null) return;

            var filtered = _movementsLoaded.AsEnumerable();

            //Filtro Texto (ID o CUIT)
            string text = txtFilter.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(text))
            {
                filtered = filtered.Where(m =>
                    m.Id.ToString().Contains(text) ||
                    m.ProviderCuit.Contains(text) ||
                    m.ProviderName.ToLower().Contains(text));
            }

            //Filtro Proveedor (ComboBox)
            if (cbxProvidersFilter != null && cbxProvidersFilter.SelectedValue != null && cbxProvidersFilter.SelectedValue.ToString() != "ALL")
            {
                string cuit = cbxProvidersFilter.SelectedValue.ToString();
                filtered = filtered.Where(m => m.ProviderCuit == cuit);
            }

            // 3. Ordenamiento
            if (rbtDesDate != null && rbtDesDate.Checked)
                filtered = filtered.OrderByDescending(m => m.Date);
            else if (rbtDesName != null && rbtDesName.Checked)
                filtered = filtered.OrderBy(m => m.ProviderName);
            else if (rbtMaxCost != null && rbtMaxCost.Checked)
                filtered = filtered.OrderByDescending(m => m.Total);
            else if (rbtMinCost != null && rbtMinCost.Checked)
                filtered = filtered.OrderBy(m => m.Total);

            dgvMovements.DataSource = filtered.ToList();

            ConfigGrid();
        }

        private async void btnResetFilters_Click(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;
            txtFilter.Text = "";
            if (cbxProvidersFilter != null && cbxProvidersFilter.Items.Count > 0) cbxProvidersFilter.SelectedIndex = 0;
            if (rbtDesDate != null) rbtDesDate.Checked = true;

            await LoadMovements(null, null);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void rbtDesDate_CheckedChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void rbtDesName_CheckedChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void rbtMinCost_CheckedChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void rbtMaxCost_CheckedChanged(object sender, EventArgs e) { ApplyFilters(); }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormMovementEditor>();
            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadMovements(null, null);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMovements.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una compra para editar.");
                return;
            }

            int id = (int)dgvMovements.CurrentRow.Cells["Id"].Value;

            var editor = _serviceProvider.GetRequiredService<FormMovementEditor>();
            editor.LoadForEdit(id);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadMovements(null, null);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMovements.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una compra para eliminar.");
                return;
            }

            int id = (int)dgvMovements.CurrentRow.Cells["Id"].Value;
            string proveedor = dgvMovements.CurrentRow.Cells["ProviderName"].Value.ToString();
            decimal total = (decimal)dgvMovements.CurrentRow.Cells["Total"].Value;

            var confirm = MessageBox.Show($"¿Eliminar la compra de {proveedor} por {total:C2}?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _movementService.SoftDeleteAsync(id);
                    await LoadMovements(null, null);
                    MessageBox.Show("Compra eliminada.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void btnShowDetail_Click(object sender, EventArgs e)
        {
            if (dgvMovements.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una compra.");
                return;
            }

            var item = (ProviderMovementGridDto)dgvMovements.CurrentRow.DataBoundItem;

            var formDetalle = _serviceProvider.GetRequiredService<FormMovementDetails>();

            formDetalle.LoadFromGrid(
                item.FilePath,
                item.ProviderName,
                item.Date.ToDateTime(TimeOnly.MinValue),
                item.Total,
                item.Id
            );

            formDetalle.ShowDialog();
        }

        private void cbxProvidersFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
