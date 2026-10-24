using Application.Providers.Dtos;
using Application.Providers.Interfaces;
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
    public partial class FormProviderEditor : Form
    {
        private readonly IProviderService _service;
        private string? _cuit = null;
        public FormProviderEditor(IProviderService service)
        {
            InitializeComponent();
            _service = service;
        }

        public async void LoadForEdit(string cuit)
        {
            _cuit = cuit;
            this.Text = $"Editar Proveedor - {cuit}";
            btnAdd.Text = "Guardar";
            txtCuit.Enabled = false;

            try
            {
                var dto = await _service.GetForEditAsync(cuit);
                if (dto != null)
                {
                    txtCuit.Text = dto.Cuit;
                    txtName.Text = dto.Name;
                    txtEmail.Text = dto.Email;
                    txtPhone.Text = dto.Phone;
                    txtAddress.Text = dto.Address;
                }
                else
                {
                    MessageBox.Show("Proveedor no encontrado.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
                this.Close();
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                MessageBox.Show("El CUIT es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El Nombre es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_cuit == null)
                {
                    // --- CREAR ---
                    var dto = new CreateProviderDto
                    {
                        Cuit = txtCuit.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Address = txtAddress.Text
                    };

                    await _service.CreateAsync(dto);
                    MessageBox.Show("Proveedor creado.");
                }
                else
                {
                    // --- EDITAR ---
                    var dto = new UpdateProviderDto
                    {
                        Cuit = _cuit,
                        Name = txtName.Text.Trim(),
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Address = txtAddress.Text,
                        IsActive = true
                    };

                    await _service.UpdateAsync(dto);
                    MessageBox.Show("Proveedor actualizado.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
