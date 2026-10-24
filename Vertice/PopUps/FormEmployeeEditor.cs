using Application.Employees.Dtos;
using Application.Employees.Interfaces;
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
    public partial class FormEmployeeEditor : Form
    {
        private readonly IEmployeeService _service;
        private string? _dni = null;

        public FormEmployeeEditor(IEmployeeService service)
        {
            InitializeComponent();
            _service = service;
        }

        public async void LoadForEdit(string dni)
        {
            _dni = dni;
            this.Text = $"Editar Empleado - {dni}";
            lblTitle.Text = "Editar Empleado";
            btnAdd.Text = "Guardar";
            txtDni.Enabled = false;
            cbxAddUser.Enabled = false;
            cbxAddUser.Visible = false;
            lblHiredDate.Visible = false;
            cbxDateHiredToday.Visible = false;
            cbxDateHiredToday.Enabled = false;

            try
            {
                var dto = await _service.GetForEditAsync(dni);
                if (dto != null)
                {
                    txtDni.Text = dto.Dni;
                    txtCuit.Text = dto.Cuit;
                    txtName.Text = dto.Name;
                    txtLastName.Text = dto.LastName;
                    txtPhone.Text = dto.Phone;
                    txtEmail.Text = dto.Email;
                    txtAddress.Text = dto.Address;

                    dtpBirthDay.Value = dto.BirthDay.ToDateTime(TimeOnly.MinValue);
                    dtpHiredDate.Value = dto.DateHired;
                    //chkActive.Checked = dto.IsActive;
                }
                else
                {
                    MessageBox.Show("No se encontró el empleado solicitado.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
                this.Close();
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("El DNI es obligatorio (*).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                MessageBox.Show("El CUIT es obligatorio (*).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Nombre y Apellido son obligatorios (*).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxAddUser.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Si crea un usuario, el Nombre de Usuario es obligatorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Si crea un usuario, la Contraseña es obligatoria.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                var birthDay = DateOnly.FromDateTime(dtpBirthDay.Value);
                var hiredDate = DateTime.Now;

                if (cbxDateHiredToday.Checked)
                    hiredDate = dtpHiredDate.Value;


                if (_dni == null)
                {
                    // --- CREAR ---
                    var dto = new CreateEmployeeDto
                    {
                        Dni = txtDni.Text.Trim(),
                        Cuit = txtCuit.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        BirthDay = birthDay,

                        DateHired = hiredDate,

                        Phone = GetValueOrNull(txtPhone.Text),
                        Email = GetValueOrNull(txtEmail.Text),
                        Address = GetValueOrNull(txtAddress.Text),

                        CreateUser = cbxAddUser.Checked,
                        Username = cbxAddUser.Checked ? txtUsername.Text.Trim() : null,
                        Password = cbxAddUser.Checked ? txtPassword.Text : null,
                        UserRole = "E"
                    };

                    await _service.CreateAsync(dto);
                    MessageBox.Show("Empleado creado exitosamente.");
                }
                else
                {
                    // --- EDITAR ---
                    var dto = new UpdateEmployeeDto
                    {
                        Dni = _dni,
                        Cuit = txtCuit.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        BirthDay = birthDay,
                        DateHired = hiredDate,

                        Phone = GetValueOrNull(txtPhone.Text),
                        Email = GetValueOrNull(txtEmail.Text),
                        Address = GetValueOrNull(txtAddress.Text),

                        IsActive = true
                    };

                    await _service.UpdateAsync(dto);
                    MessageBox.Show("Empleado actualizado exitosamente.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string? GetValueOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            return text.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxAddUser_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Visible = cbxAddUser.Checked;
            txtPassword.Visible = cbxAddUser.Checked;
        }

        private void cbxDateHiredToday_CheckedChanged(object sender, EventArgs e)
        {
            dtpHiredDate.Visible = !cbxDateHiredToday.Checked;
        }
    }
}
