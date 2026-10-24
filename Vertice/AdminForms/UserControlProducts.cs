using Application.Products;
using Application.Products.Dtos;
using Application.Products.Interfaces;
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
using System.Xml.Linq;
using WinForms.PopUps;

namespace WinForms
{
    public partial class UserControlProducts : UserControl
    {
        private readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;
        private const string CODIGO_VARIOS = "VARIOS";
        private List<ProductGridDto> _productsLoaded = new List<ProductGridDto>();
        private readonly ILabelQueueService _labelQueueService;
        public UserControlProducts(IProductService productService,
            IServiceProvider serviceProvider,
            ILabelQueueService labelQueueService)
        {
            InitializeComponent();
            _productService = productService;
            _serviceProvider = serviceProvider;
            _labelQueueService = labelQueueService;
        }

        private void UserControlProducts_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormProductEditor>();
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private async void LoadGrid()
        {
            try
            {
                var products = await _productService.GetAllForGridAsync();
                _productsLoaded = products.ToList();

                dgvProducts.DataSource = products.ToList();

                ConfigGrid();
                ApplyFilters();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_productsLoaded == null) return;

            var listaFiltrada = _productsLoaded.AsEnumerable();

            listaFiltrada = listaFiltrada.Where(p => p.Code != CODIGO_VARIOS);

            string busqueda = txtFilter.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(busqueda))
            {
                listaFiltrada = listaFiltrada.Where(p =>
                    p.Name.ToLower().Contains(busqueda) ||
                    p.Code.ToLower().Contains(busqueda));
            }

            if (rbtDesDate != null && rbtDesDate.Checked)
            {
                // Más nuevos (recién modificados) arriba
                listaFiltrada = listaFiltrada.OrderByDescending(p => p.LastStockUpdate);
            }
            else if (rbtDesName != null && rbtDesName.Checked)
            {
                listaFiltrada = listaFiltrada.OrderBy(p => p.Name);
            }
            else if (rbtMinStock != null && rbtMinStock.Checked)
            {
                // Ver qué falta comprar
                listaFiltrada = listaFiltrada.OrderBy(p => p.Stock);
            }
            else if (rbtMaxStock != null && rbtMaxStock.Checked)
            {
                // Ver qué sobra
                listaFiltrada = listaFiltrada.OrderByDescending(p => p.Stock);
            }

            dgvProducts.DataSource = listaFiltrada.ToList();
        }

        private void ConfigGrid()
        {
            if (dgvProducts.Columns.Count == 0) return;

            dgvProducts.Columns["Code"].HeaderText = "Código";

            dgvProducts.Columns["Name"].HeaderText = "Nombre";

            dgvProducts.Columns["SalePrice"].HeaderText = "Precio Venta";
            dgvProducts.Columns["SalePrice"].DefaultCellStyle.Format = "C2"; // Formato Moneda ($ 1,200.00)
            dgvProducts.Columns["SalePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvProducts.Columns["CostPrice"].HeaderText = "Costo";
            dgvProducts.Columns["CostPrice"].DefaultCellStyle.Format = "C2";
            dgvProducts.Columns["CostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvProducts.Columns["Stock"].HeaderText = "Stock Actual";
            dgvProducts.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgvProducts.Columns["LastStockUpdate"].HeaderText = "Últ. Actualización";
            dgvProducts.Columns["LastStockUpdate"].DefaultCellStyle.Format = "g"; // Formato fecha corta + hora
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;

            string code = dgvProducts.CurrentRow.Cells["Code"].Value.ToString();

            if (code == null) return;

            var editor = _serviceProvider.GetRequiredService<FormProductEditor>();
            editor.LoadForEdit(code);

            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = dgvProducts.CurrentRow.Cells["Code"].Value.ToString();
            string name = dgvProducts.CurrentRow.Cells["Name"].Value.ToString();

            var confirmResult = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto '{name}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await _productService.SoftDeleteProductAsync(code);

                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            if (rbtDesDate != null) rbtDesDate.Checked = true;

            LoadGrid();
        }

        private void rbtDesDate_CheckedChanged(object sender, EventArgs e) { if (rbtDesDate.Checked) ApplyFilters(); }

        private void rbtDesName_CheckedChanged(object sender, EventArgs e) { if (rbtDesName.Checked) ApplyFilters(); }

        private void rbtMinStock_CheckedChanged(object sender, EventArgs e) { if (rbtMinStock.Checked) ApplyFilters(); }

        private void rbtMaxStock_CheckedChanged(object sender, EventArgs e) { if (rbtMaxStock.Checked) ApplyFilters(); }

        private async void btn0Stock_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar su stock.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = dgvProducts.CurrentRow.Cells["Code"].Value.ToString();
            string name = dgvProducts.CurrentRow.Cells["Name"].Value.ToString();

            var confirmResult = MessageBox.Show(
                $"¿Está seguro que desea eliminar el stock del producto '{name}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var product = await _productService.GetProductForEditAsync(code);

                    var productoForEdit = new UpdateProductDto
                    {
                        Code = product.Code,
                        Name = product.Name,
                        SalePrice = product.SalePrice,
                        CostPrice = product.CostPrice,
                        Stock = 0,
                        IsActive = true
                    };

                    await _productService.UpdateProductAsync(productoForEdit);

                    MessageBox.Show("Stock eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is ProductGridDto product)
            {
                var label = new ProductLabelDto
                {
                    ProductCode = product.Code,
                    PrintName = product.Name,
                    Price = product.SalePrice
                };

                _labelQueueService.AddToQueue(label);
            }
        }

        private void btnShowQueue_Click(object sender, EventArgs e)
        {
            var formLabels = _serviceProvider.GetRequiredService<FormPrintLabels>();
            formLabels.ShowDialog();
        }
    }
}
