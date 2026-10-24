using Application.Products.Dtos;
using Application.Products.Interfaces;
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
    public partial class FormProductEditor : Form
    {
        private readonly IProductService _productService;

        //== null => Creando. != null => Editando.
        private string? _productCode = null;

        public FormProductEditor(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("El precio de venta es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtCostPrice.Text))
                {
                    MessageBox.Show("El costo es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    MessageBox.Show("El stock es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal salePrice))
                {
                    MessageBox.Show("El precio tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCostPrice.Text, out decimal costPrice))
                {
                    MessageBox.Show("El costo tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int stock))
                {
                    MessageBox.Show("El stock debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_productCode == null)
                {
                    //Crear.
                    this.Text = "Agregar Producto";
                    var dto = new CreateProductDto
                    {
                        Code = txtCode.Text,
                        Name = txtName.Text,
                        SalePrice = salePrice,
                        CostPrice = costPrice,
                        InitialStock = stock
                    };

                    await _productService.CreateProductAsync(dto);
                    MessageBox.Show("Producto creado exitosamente.");
                }
                else
                {
                    //Editar
                    var dto = new UpdateProductDto
                    {
                        Code = _productCode,
                        Name = txtName.Text,
                        SalePrice = salePrice,
                        CostPrice = costPrice,
                        Stock = stock,
                        IsActive = true
                    };

                    await _productService.UpdateProductAsync(dto);
                    MessageBox.Show("Producto actualizado exitosamente.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async void LoadForEdit(string code)
        {
            _productCode = code;
            this.Text = $"Editar Producto";
            btnAdd.Text = "Guardar";
            txtCode.Text = code;
            txtCode.Enabled = false;

            try
            {
                var dto = await _productService.GetProductForEditAsync(code);
                if (dto != null)
                {
                    txtCode.Text = dto.Code;
                    txtName.Text = dto.Name;
                    txtPrice.Text = dto.SalePrice.ToString();
                    txtCostPrice.Text = dto.CostPrice.ToString();
                    txtStock.Text = dto.Stock.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar producto: " + ex.Message);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGainPercent_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtCostPrice.Text, out decimal costPrice))
            {
                MessageBox.Show("El costo tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtGainPercent.Text))
            {
                if (!decimal.TryParse(txtGainPercent.Text, out decimal percentage))
                {
                    MessageBox.Show("El porcentaje tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (percentage > 0)
                {
                    txtPrice.Text = (costPrice + (costPrice * (percentage / 100m))).ToString();
                }
            }
        }
    }
}
