using Application.AuditLogs.Dtos;
using Application.AuditLogs.Interfaces;
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

namespace WinForms
{
    public partial class UserControlLogs : UserControl
    {
        private readonly IAuditLogService _logService;
        private readonly IUserService _userService;

        private List<AuditLogGridDto> _logsLoaded = new List<AuditLogGridDto>();

        public UserControlLogs(IAuditLogService logService, IUserService userService)
        {
            InitializeComponent();
            _logService = logService;
            _userService = userService;
        }

        private async void UserControlLogs_Load(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now;

            ConfigGrid();

            try
            {
                await LoadUsersCombo();
                await LoadLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar logs: " + ex.Message);
            }
        }

        private async Task LoadUsersCombo()
        {
            var users = await _userService.GetAllAsync();

            var list = users.Select(u => new { u.Username, Display = u.Username }).ToList();
            list.Insert(0, new { Username = "ALL", Display = "TODOS LOS USUARIOS" });

            if (cbxUsersFilter != null)
            {
                cbxUsersFilter.DataSource = list;
                cbxUsersFilter.DisplayMember = "Display";
                cbxUsersFilter.ValueMember = "Username";
                cbxUsersFilter.SelectedIndex = 0;
            }
        }

        private async Task LoadLogs()
        {
            try
            {
                var from = dtpSince.Value;
                var to = dtpTo.Value;

                var logs = await _logService.GetLogsAsync(from, to, null);

                _logsLoaded = logs.ToList();

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar logs: " + ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (_logsLoaded == null) return;

            var filtered = _logsLoaded.AsEnumerable();

            //Filtro por Usuario (ComboBox)
            if (cbxUsersFilter.SelectedValue != null && cbxUsersFilter.SelectedValue.ToString() != "ALL")
            {
                string selectedUser = cbxUsersFilter.SelectedValue.ToString();
                filtered = filtered.Where(x => x.Username == selectedUser);
            }

            //Filtro de Texto
            string text = txtFilter.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(text))
            {
                filtered = filtered.Where(x =>
                    x.Action.ToLower().Contains(text) ||
                    x.Username.ToLower().Contains(text) ||
                    x.Details.ToLower().Contains(text));
            }

            dgvLogs.DataSource = filtered.ToList();

            ConfigGrid();
        }

        private void ConfigGrid()
        {
            dgvLogs.AutoGenerateColumns = true;
            dgvLogs.ReadOnly = true;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvLogs.Columns["Date"] != null)
            {
                dgvLogs.Columns["Date"].HeaderText = "Fecha";
                dgvLogs.Columns["Date"].DefaultCellStyle.Format = "g"; // Fecha corta + hora
            }

            if (dgvLogs.Columns["Username"] != null)
            {
                dgvLogs.Columns["Username"].HeaderText = "Usuario";
            }

            if (dgvLogs.Columns["Action"] != null)
            {
                dgvLogs.Columns["Action"].HeaderText = "Acción";
            }

            if (dgvLogs.Columns["Details"] != null)
            {
                dgvLogs.Columns["Details"].HeaderText = "Detalles";
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadLogs();
        }

        private async void btnResetFilters_Click(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now;
            txtFilter.Text = "";
            if (cbxUsersFilter.Items.Count > 0) cbxUsersFilter.SelectedIndex = 0;

            await LoadLogs();
        }

        private void cbxUsersFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
