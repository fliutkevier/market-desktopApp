using Application.Employees.Dtos;
using Application.Employees.Interfaces;
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
    public partial class UserControlEmployees : UserControl
    {
        private readonly IEmployeeService _service;
        private readonly IServiceProvider _serviceProvider;

        private List<EmployeeGridDto> _employeesLoaded = new List<EmployeeGridDto>();

        public UserControlEmployees(IEmployeeService service, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _service = service;
            _serviceProvider = serviceProvider;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormEmployeeEditor>();
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un empleado para editar.");
                return;
            }

            string dni = dgvEmployees.CurrentRow.Cells["Dni"].Value.ToString();

            var editor = _serviceProvider.GetRequiredService<FormEmployeeEditor>();
            editor.LoadForEdit(dni);

            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.");
                return;
            }

            string dni = dgvEmployees.CurrentRow.Cells["Dni"].Value.ToString();
            string nombre = dgvEmployees.CurrentRow.Cells["FullName"].Value.ToString();

            var confirm = MessageBox.Show($"¿Desea eliminar al empleado {nombre}?\n(DNI: {dni})", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _service.SoftDeleteAsync(dni);
                    LoadGrid();
                    MessageBox.Show("Empleado eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void UserControlEmployees_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private async void LoadGrid()
        {
            try
            {
                var lista = await _service.GetAllForGridAsync();
                _employeesLoaded = lista.ToList();
                ApplyFilters();
                ConfigGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (_employeesLoaded == null) return;

            var listaFiltrada = _employeesLoaded.AsEnumerable();

            string busqueda = txtFilter.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(busqueda))
            {
                listaFiltrada = listaFiltrada.Where(e =>
                    e.FullName.ToLower().Contains(busqueda) ||
                    e.Dni.Contains(busqueda) ||
                    e.Cuit.Contains(busqueda));
            }


            // Ordenamiento
            if (rbtDesDate != null && rbtDesDate.Checked)
            {
                listaFiltrada = listaFiltrada.OrderByDescending(e => e.DateHired);
            }
            else if (rbtDesName != null && rbtDesName.Checked)
            {
                listaFiltrada = listaFiltrada.OrderBy(e => e.FullName);
            }

            dgvEmployees.DataSource = listaFiltrada.ToList();
        }

        private void ConfigGrid()
        {
            if (dgvEmployees.Columns.Count == 0) return;

            if (dgvEmployees.Columns["Dni"] != null)
                dgvEmployees.Columns["Dni"].HeaderText = "DNI";

            if (dgvEmployees.Columns["Cuit"] != null)
                dgvEmployees.Columns["Cuit"].HeaderText = "CUIT";

            if (dgvEmployees.Columns["FullName"] != null)
                dgvEmployees.Columns["FullName"].HeaderText = "Nombre Completo";

            if (dgvEmployees.Columns["Phone"] != null)
                dgvEmployees.Columns["Phone"].HeaderText = "Teléfono";

            if (dgvEmployees.Columns["Email"] != null)
                dgvEmployees.Columns["Email"].HeaderText = "Email";

            if (dgvEmployees.Columns["DateHired"] != null)
            {
                dgvEmployees.Columns["DateHired"].HeaderText = "Fecha Ingreso";
                dgvEmployees.Columns["DateHired"].DefaultCellStyle.Format = "d";
            }

            if (dgvEmployees.Columns["LinkedUser"] != null)
                dgvEmployees.Columns["LinkedUser"].HeaderText = "Usuario";
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void rbtDesDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDesDate.Checked) ApplyFilters();
        }

        private void rbtDesName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDesName.Checked) ApplyFilters();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            if (rbtDesDate != null) rbtDesDate.Checked = true;

            LoadGrid();
        }
    }
}
