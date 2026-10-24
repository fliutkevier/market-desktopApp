using Application.Employees.Interfaces;
using Application.PaymentMethods.Interfaces;
using Application.Products.Interfaces;
using Application.ProviderMovements.Interfaces;
using Application.Providers.Interfaces;
using Application.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.PopUps
{
    public partial class FormRecoverElements : Form
    {
        private readonly IProviderService _providerService;
        private readonly IProductService _productService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IProviderMovementService _movementService;

        public FormRecoverElements(
            IProductService productService,
            IProviderService providerService,
            IEmployeeService employeeService,
            IUserService userService,
            IPaymentMethodService paymentMethodService,
            IProviderMovementService movementService)
        {
            InitializeComponent();
            _productService = productService;
            _providerService = providerService;
            _employeeService = employeeService;
            _userService = userService;
            _paymentMethodService = paymentMethodService;
            _movementService = movementService;

            this.Text = "Papelera de Reciclaje";
            this.MaximizeBox = false;

            LoadCombo();
        }

        private void LoadCombo()
        {
            cbxType.Items.Clear();
            cbxType.Items.Add("Elegir Tipo");
            cbxType.Items.Add("Productos");
            cbxType.Items.Add("Proveedores");
            cbxType.Items.Add("Empleados");
            cbxType.Items.Add("Usuarios");
            cbxType.Items.Add("Métodos de Pago");
            cbxType.Items.Add("Compras");

            cbxType.SelectedIndex = 0; // Seleccionar el primero por defecto
        }

        private async Task LoadInactives()
        {
            try
            {
                if (cbxType.SelectedItem == null) return;

                string seleccion = cbxType.SelectedItem.ToString();
                dgvInactives.DataSource = null;
                dgvInactives.Columns.Clear();

                switch (seleccion)
                {
                    case "Productos":
                        var productos = await _productService.GetDeletedAsync();
                        dgvInactives.DataSource = productos.ToList();
                        break;
                    case "Proveedores":
                        var proveedores = await _providerService.GetDeletedAsync();
                        dgvInactives.DataSource = proveedores.ToList();
                        break;
                    case "Empleados":
                        var empleados = await _employeeService.GetDeletedAsync();
                        dgvInactives.DataSource = empleados.ToList();
                        break;
                    case "Usuarios":
                        var usuarios = await _userService.GetDeletedAsync();
                        dgvInactives.DataSource = usuarios.ToList();
                        break;
                    case "Métodos de Pago":
                        var pagos = await _paymentMethodService.GetDeletedAsync();
                        dgvInactives.DataSource = pagos.ToList();
                        break;
                    case "Compras":
                        var compras = await _movementService.GetDeletedAsync();
                        dgvInactives.DataSource = compras.ToList();
                        break;
                }

                ConfigGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar eliminados: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadInactives();
        }

        private async void btnRecover_Click(object sender, EventArgs e)
        {
            if (dgvInactives.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un elemento para restaurar.");
                return;
            }

            string seleccion = cbxType.SelectedItem.ToString();

            try
            {
                switch (seleccion)
                {
                    case "Productos":
                        string code = dgvInactives.CurrentRow.Cells["Code"].Value.ToString();
                        await _productService.RestoreAsync(code);
                        break;

                    case "Proveedores":
                        string cuit = dgvInactives.CurrentRow.Cells["Cuit"].Value.ToString();
                        await _providerService.RestoreAsync(cuit);
                        break;

                    case "Empleados":
                        string dni = dgvInactives.CurrentRow.Cells["Dni"].Value.ToString();
                        await _employeeService.RestoreAsync(dni);
                        break;

                    case "Usuarios":
                        string username = dgvInactives.CurrentRow.Cells["Username"].Value.ToString();
                        await _userService.RestoreAsync(username);
                        break;

                    case "Métodos de Pago":
                        int idMP = (int)dgvInactives.CurrentRow.Cells["Id"].Value;
                        await _paymentMethodService.RestoreAsync(idMP);
                        break;

                    case "Compras":
                        int idMov = (int)dgvInactives.CurrentRow.Cells["Id"].Value;
                        await _movementService.RestoreAsync(idMov);
                        break;
                }

                MessageBox.Show("Elemento restaurado exitosamente.");
                await LoadInactives();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar: " + ex.Message);
            }
        }

        private void ConfigGrid()
        {
            dgvInactives.AutoGenerateColumns = true;
            dgvInactives.ReadOnly = true;
            dgvInactives.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactives.RowHeadersVisible = false;
            dgvInactives.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvInactives.Columns["Id"] != null) dgvInactives.Columns["Id"].HeaderText = "ID";

            // PRODUCTOS
            if (dgvInactives.Columns["Code"] != null) dgvInactives.Columns["Code"].HeaderText = "Código";
            if (dgvInactives.Columns["CostPrice"] != null) dgvInactives.Columns["CostPrice"].HeaderText = "Precio Costo";
            if (dgvInactives.Columns["LastStockUpdate"] != null) dgvInactives.Columns["LastStockUpdate"].HeaderText = "Última Act.";
            if (dgvInactives.Columns["Name"] != null) dgvInactives.Columns["Name"].HeaderText = "Nombre";
            if (dgvInactives.Columns["SalePrice"] != null)
            {
                dgvInactives.Columns["SalePrice"].HeaderText = "Precio";
                dgvInactives.Columns["SalePrice"].DefaultCellStyle.Format = "C2";
            }
            if (dgvInactives.Columns["Stock"] != null) dgvInactives.Columns["Stock"].HeaderText = "Stock";

            // PROVEEDORES / EMPLEADOS
            if (dgvInactives.Columns["Cuit"] != null) dgvInactives.Columns["Cuit"].HeaderText = "CUIT";
            if (dgvInactives.Columns["Dni"] != null) dgvInactives.Columns["Dni"].HeaderText = "DNI";
            if (dgvInactives.Columns["Phone"] != null) dgvInactives.Columns["Phone"].HeaderText = "Teléfono";
            if (dgvInactives.Columns["FullName"] != null) dgvInactives.Columns["FullName"].HeaderText = "Nombre Completo";
            if (dgvInactives.Columns["DateAdded"] != null) dgvInactives.Columns["DateAdded"].HeaderText = "Fecha Añadido";
            if (dgvInactives.Columns["DateHired"] != null) dgvInactives.Columns["DateHired"].HeaderText = "Fecha Contratado";
            if (dgvInactives.Columns["LinkedUser"] != null) dgvInactives.Columns["LinkedUser"].HeaderText = "Usuario";

            // USUARIOS
            if (dgvInactives.Columns["Username"] != null) dgvInactives.Columns["Username"].HeaderText = "Usuario";
            if (dgvInactives.Columns["Role"] != null) dgvInactives.Columns["Role"].HeaderText = "Rol";
            if (dgvInactives.Columns["RoleDescription"] != null) dgvInactives.Columns["RoleDescription"].HeaderText = "Rol";

            // METODOS PAGO
            if (dgvInactives.Columns["Recharge"] != null) dgvInactives.Columns["Recharge"].HeaderText = "Recargo %";
            if (dgvInactives.Columns["Discount"] != null) dgvInactives.Columns["Discount"].HeaderText = "Descuento %";

            // COMPRAS
            if (dgvInactives.Columns["Total"] != null)
            {
                dgvInactives.Columns["Total"].HeaderText = "Total";
                dgvInactives.Columns["Total"].DefaultCellStyle.Format = "C2";
            }
            if (dgvInactives.Columns["Date"] != null) dgvInactives.Columns["Date"].HeaderText = "Fecha";
            if (dgvInactives.Columns["ProviderName"] != null) dgvInactives.Columns["ProviderName"].HeaderText = "Proveedor";
            if (dgvInactives.Columns["ProviderCuit"] != null) dgvInactives.Columns["ProviderCuit"].HeaderText = "CUIT";

            // Ocultar IDs y columnas técnicas en general
            //if (dgvInactives.Columns["Id"] != null) dgvInactives.Columns["Id"].Visible = false;
            if (dgvInactives.Columns["IsActive"] != null) dgvInactives.Columns["IsActive"].Visible = false;
            if (dgvInactives.Columns["FilePath"] != null) dgvInactives.Columns["FilePath"].Visible = false;
            if (dgvInactives.Columns["HasFile"] != null) dgvInactives.Columns["HasFile"].Visible = false;
        }
    }
}
