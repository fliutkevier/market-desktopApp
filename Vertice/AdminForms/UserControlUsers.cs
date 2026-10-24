using Application.Users.Dtos;
using Application.Users.Interfaces;
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

namespace WinForms.AdminForms
{
    public partial class UserControlUsers : UserControl
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public UserControlUsers(IUserService userService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userService = userService;
            _serviceProvider = serviceProvider;
        }

        private async void UserControlUsers_Load(object sender, EventArgs e)
        {
            await LoadGrid();
        }

        private async Task LoadGrid()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                //Asignar Datos
                dgvUsers.DataSource = users.ToList();

                //Configurar visualmente
                ConfigGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigGrid()
        {
            // Configuración general
            dgvUsers.AutoGenerateColumns = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ReadOnly = true;

            if (dgvUsers.Columns["Username"] != null)
            {
                dgvUsers.Columns["Username"].HeaderText = "Usuario";
            }

            if (dgvUsers.Columns["Role"] != null)
            {
                dgvUsers.Columns["Role"].HeaderText = "Rol";
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormUserEditor>();

            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadGrid();
            }
        }

        private async void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            string user = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            if (user == null) return;

            var editor = _serviceProvider.GetRequiredService<FormUserEditor>();
            editor.LoadForEdit(user);

            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                await LoadGrid();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is UserGridDto user)
            {
                if (MessageBox.Show($"¿Está seguro de eliminar al usuario '{user.Username}'?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        await _userService.DeleteUserAsync(user.Username);
                        await LoadGrid();
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
