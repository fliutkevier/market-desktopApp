using Application.PaymentMethods.Dtos;
using Application.PaymentMethods.Interfaces;
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
    public partial class FormPaymentMethodEditor : Form
    {
        private readonly IPaymentMethodService _service;
        private int? _id = null;

        public FormPaymentMethodEditor(IPaymentMethodService service)
        {
            InitializeComponent();
            _service = service;
        }

        public async void LoadForEdit(int id)
        {
            _id = id;
            this.Text = "Editar Método de Pago";
            lblTitle.Text = "Editar Método de Pago";
            btnAdd.Text = "Guardar";

            try
            {
                var dto = await _service.GetForEditAsync(id);
                if (dto != null)
                {
                    txtName.Text = dto.Name;

                    // Mostramos los números con formato (ej: 10.00)
                    txtRecharge.Text = dto.Recharge.ToString("0.00");
                    txtDiscount.Text = dto.Discount.ToString("0.00");
                }
                else
                {
                    MessageBox.Show("No se encontró el método de pago solicitado.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
                this.Close();
            }
        }
        
        private bool TryGetDecimal(string text, out decimal result)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                result = 0;
                return true;
            }

            return decimal.TryParse(text, out result);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryGetDecimal(txtRecharge.Text, out decimal recharge))
            {
                MessageBox.Show("El recargo tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryGetDecimal(txtDiscount.Text, out decimal discount))
            {
                MessageBox.Show("El descuento tiene un formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_id == null)
                {
                    // --- CREAR ---
                    var dto = new CreatePaymentMethodDto
                    {
                        Name = txtName.Text.Trim(),
                        Recharge = recharge,
                        Discount = discount
                    };

                    await _service.CreateAsync(dto);
                    MessageBox.Show("Método de pago creado exitosamente.");
                }
                else
                {
                    // --- EDITAR ---
                    var dto = new UpdatePaymentMethodDto
                    {
                        Id = _id.Value,
                        Name = txtName.Text.Trim(),
                        Recharge = recharge,
                        Discount = discount,
                        IsActive = true
                    };

                    await _service.UpdateAsync(dto);
                    MessageBox.Show("Método de pago actualizado exitosamente.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
