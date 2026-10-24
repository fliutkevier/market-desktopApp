using Application.Providers.Dtos;
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
    public partial class UserControlProviders : UserControl
    {
        private readonly IProviderService _service;
        private readonly IServiceProvider _serviceProvider;

        // Lista en memoria para filtro rápido
        private List<ProviderGridDto> _providersLoaded = new List<ProviderGridDto>();

        public UserControlProviders(IProviderService service, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _service = service;
            _serviceProvider = serviceProvider;
        }

        private async Task LoadGrid()
        {
            try
            {
                var lista = await _service.GetAllForGridAsync();
                _providersLoaded = lista.ToList();

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        private void ConfigGrid()
        {
            dgvProviders.AutoGenerateColumns = true;
            dgvProviders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProviders.MultiSelect = false;
            dgvProviders.RowHeadersVisible = false;
            dgvProviders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProviders.ReadOnly = true;

            dgvProviders.DataBindingComplete += (s, e) =>
            {
                if (dgvProviders.Columns["Cuit"] != null) dgvProviders.Columns["Cuit"].HeaderText = "CUIT";
                if (dgvProviders.Columns["Name"] != null) dgvProviders.Columns["Name"].HeaderText = "Nombre";
                if (dgvProviders.Columns["Email"] != null) dgvProviders.Columns["Email"].HeaderText = "Correo";
                if (dgvProviders.Columns["Phone"] != null) dgvProviders.Columns["Phone"].HeaderText = "Teléfono";
                if (dgvProviders.Columns["Address"] != null) dgvProviders.Columns["Address"].HeaderText = "Dirección";
                if (dgvProviders.Columns["DateAdded"] != null) dgvProviders.Columns["DateAdded"].HeaderText = "Agregado";
                if (dgvProviders.Columns["IsActive"] != null) dgvProviders.Columns["IsActive"].Visible = false;
            };
        }

        private void ApplyFilters()
        {
            if (_providersLoaded == null) return;

            var filtrada = _providersLoaded.AsEnumerable();

            string busqueda = txtFilter.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(busqueda))
            {
                filtrada = filtrada.Where(p =>
                    p.Name.ToLower().Contains(busqueda) ||
                    p.Cuit.Contains(busqueda));
            }

            // Ordenar
            if (rbtDesDate != null && rbtDesDate.Checked)
            {
                filtrada = filtrada.OrderBy(p => p.DateAdded);
            }
            else
            {
                filtrada = filtrada.OrderBy(p => p.Name);
            }

            dgvProviders.DataSource = filtrada.ToList();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormProviderEditor>();
            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadGrid();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProviders.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            string cuit = dgvProviders.CurrentRow.Cells["Cuit"].Value.ToString();

            var editor = _serviceProvider.GetRequiredService<FormProviderEditor>();
            editor.LoadForEdit(cuit);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadGrid();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProviders.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            string cuit = dgvProviders.CurrentRow.Cells["Cuit"].Value.ToString();
            string nombre = dgvProviders.CurrentRow.Cells["Name"].Value.ToString();

            if (MessageBox.Show($"¿Eliminar al proveedor '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    await _service.SoftDeleteAsync(cuit);
                    await LoadGrid();
                    MessageBox.Show("Proveedor eliminado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnPurchaseHistory_Click(object sender, EventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private async void UserControlProviders_Load(object sender, EventArgs e)
        {
            ConfigGrid();
            await LoadGrid();
        }

        private void rbtDesDate_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void rbtDesName_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            rbtDesDate.Checked = true;
            ApplyFilters();
        }
    }
}
