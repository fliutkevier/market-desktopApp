using Application.Employees.Interfaces;
using Application.PaymentMethods.Dtos;
using Application.Users.Dtos;
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
    public partial class FormUserEditor : Form
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private string? _username = null;
        private bool _changingPassword = false;

        public FormUserEditor(IUserService userService, IEmployeeService employeeService)
        {
            InitializeComponent();
            _userService = userService;
            _employeeService = employeeService;
        }

        private async void FormUserEditor_Load(object sender, EventArgs e)
        {
            try
            {
                
                var employees = await _employeeService.GetEmployeesWithoutUserAsync();

                cbxEmployees.DataSource = employees.ToList();
                cbxEmployees.DisplayMember = "DisplayName";
                cbxEmployees.ValueMember = "Dni";

                // Estado inicial
                chkAddUser.Checked = false;
                cbxEmployees.Visible = false;
                cbxEmployees.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista de empleados: " + ex.Message);
            }
        }

        public void LoadForEdit(string username)
        {
            _username = username;
            this.Text = $"Editar Usuario - {username}";
            lblTitle.Text = $"Editar Usuario - {username}";
            btnAdd.Text = "Guardar";

            txtUsername.Text = username;
            txtUsername.Enabled = false;

            chkAddUser.Visible = false;
            chkAddUser.Enabled = false;
            cbxEmployees.Visible = false;
            cbxEmployees.Enabled = false;

            txtPassword.CueHint.CueHintText = "********";  // Placeholder
            _changingPassword = true;
        }

        private void chkAddUser_CheckedChanged(object sender, EventArgs e)
        {
            cbxEmployees.Enabled = chkAddUser.Checked;
            cbxEmployees.Visible = chkAddUser.Checked;
            if (!chkAddUser.Checked)
            {
                cbxEmployees.SelectedIndex = -1;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("El usuario es obligatorio.");
                return;
            }

            // Validación de contraseña
            if (_username == null) // Creando
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 4)
                {
                    MessageBox.Show("La contraseña es obligatoria (min 4 caracteres).");
                    return;
                }
            }
            else // Editando
            {
                if (_changingPassword && (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 4))
                {
                    MessageBox.Show("La nueva contraseña debe tener al menos 4 caracteres.");
                    return;
                }
            }

            try
            {
                if (_username == null)
                {
                    // --- CREAR ---
                    var dto = new CreateUserDto
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text,
                        EmployeeDni = chkAddUser.Checked ? cbxEmployees.SelectedValue?.ToString() : null
                    };

                    await _userService.CreateUserAsync(dto);
                    MessageBox.Show("Usuario creado exitosamente.");
                }
                else
                {
                    if (_changingPassword)
                    {
                        await _userService.ChangePasswordAsync(_username, txtPassword.Text);
                        MessageBox.Show("Contraseña actualizada correctamente.");
                    }
                    else
                    {
                        // Si no cambió nada, cierra
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
