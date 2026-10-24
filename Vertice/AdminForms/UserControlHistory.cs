using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using Application.Sessions.Dtos;
using Application.Sessions.Interfaces;
using Application.Users.Dtos;
using Application.Users.Interfaces;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.PopUps;

namespace WinForms
{
    public partial class UserControlHistory : UserControl
    {
        private readonly ISessionService _sessionService;
        private readonly ISaleService _saleService;
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserSessionService _userSessionService;

        private List<SessionGridDto> _sessionsLoaded = new List<SessionGridDto>();

        public UserControlHistory(
            ISessionService sessionService,
            ISaleService saleService,
            IUserService userService,
            IServiceProvider serviceProvider,
            IUserSessionService userSessionService)
        {
            InitializeComponent();
            _sessionService = sessionService;
            _saleService = saleService;
            _userService = userService;
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;
        }

        private async void UserControlHistory_Load(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;

            if (rbtDesDate != null) rbtDesDate.Checked = true;

            try
            {
                await LoadUsers();
                await LoadSessions(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de inicialización: " + ex.Message);
            }
            ConfigSessionsGrid();
        }

        private async Task LoadUsers()
        {
            try
            {

                var role = _userSessionService.Role.ToString();
                var username = _userSessionService.Username;
                object dataSource;

                if (role == "A")
                {
                    var users = await _userService.GetAllAsync();
                    var userList = users.Select(u => new { u.Username, Display = u.Username }).ToList();
                    userList.Insert(0, new { Username = "ALL", Display = "TODOS LOS USUARIOS" });
                    cbxUsersFilter.Enabled = true;
                    dataSource = userList;
                }
                else
                {
                    var userList = new[]
                    {
                        new { Username = username, Display = username }
                    }.ToList();

                    dataSource = userList;
                    cbxUsersFilter.Enabled = false;
                }

                cbxUsersFilter.DataSource = dataSource;
                cbxUsersFilter.DisplayMember = "Display";
                cbxUsersFilter.ValueMember = "Username";
                cbxUsersFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void ConfigSalesGrid()
        {
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.MultiSelect = false;
            dgvSales.RowHeadersVisible = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvSales.Columns.Count == 0) return;

            if (dgvSales.Columns["Id"] != null)
            {
                dgvSales.Columns["Id"].HeaderText = "Nro. Venta";
            }

            // El estado fiscal se sigue trayendo en el DTO, pero no se muestra en la grilla.
            if (dgvSales.Columns["Fiscal"] != null) dgvSales.Columns["Fiscal"].Visible = false;
            if (dgvSales.Columns["FiscalStatus"] != null) dgvSales.Columns["FiscalStatus"].Visible = false;
            if (dgvSales.Columns["FiscalStatusText"] != null) dgvSales.Columns["FiscalStatusText"].Visible = false;

            if (dgvSales.Columns["Date"] != null)
            {
                dgvSales.Columns["Date"].HeaderText = "Hora";
                dgvSales.Columns["Date"].DefaultCellStyle.Format = "T"; // Hora larga con segundos
            }

            if (dgvSales.Columns["PaymentMethod"] != null)
            {
                dgvSales.Columns["PaymentMethod"].HeaderText = "Forma de Pago";
            }

            if (dgvSales.Columns["ItemCount"] != null)
            {
                dgvSales.Columns["ItemCount"].HeaderText = "Productos";
                dgvSales.Columns["ItemCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvSales.Columns["Total"] != null)
            {
                dgvSales.Columns["Total"].HeaderText = "Total";
                dgvSales.Columns["Total"].DefaultCellStyle.Format = "C2";
                dgvSales.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // oculto
            if (dgvSales.Columns["User"] != null) dgvSales.Columns["User"].Visible = false;
        }

        private void ConfigSessionsGrid()
        {
            dgvSessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSessions.MultiSelect = false;
            dgvSessions.RowHeadersVisible = false;
            dgvSessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvSessions.Columns.Count == 0) return;

            if (dgvSessions.Columns["Id"] != null)
            {
                dgvSessions.Columns["Id"].HeaderText = "Nro. Caja";
                dgvSessions.Columns["Id"].Width = 80;
            }

            if (dgvSessions.Columns["Date"] != null)
            {
                dgvSessions.Columns["Date"].HeaderText = "Fecha";
            }

            if (dgvSessions.Columns["Username"] != null)
            {
                dgvSessions.Columns["Username"].HeaderText = "Usuario";
            }

            if (dgvSessions.Columns["OpenedAt"] != null)
            {
                dgvSessions.Columns["OpenedAt"].HeaderText = "Apertura";
                dgvSessions.Columns["OpenedAt"].DefaultCellStyle.Format = "t";
            }

            if (dgvSessions.Columns["ClosedAt"] != null)
            {
                dgvSessions.Columns["ClosedAt"].HeaderText = "Cierre";
                dgvSessions.Columns["ClosedAt"].DefaultCellStyle.Format = "t";
                dgvSessions.Columns["ClosedAt"].DefaultCellStyle.NullValue = "- ABIERTA -";
            }

            if (dgvSessions.Columns["Total"] != null)
            {
                dgvSessions.Columns["Total"].HeaderText = "Total Acum.";
                dgvSessions.Columns["Total"].DefaultCellStyle.Format = "C2";
            }

            if (dgvSessions.Columns["Duration"] != null) dgvSessions.Columns["Duration"].Visible = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var from = DateOnly.FromDateTime(dtpSince.Value);
            var to = DateOnly.FromDateTime(dtpTo.Value);

            await LoadSessions(from, to);
        }

        private async Task LoadSessions(DateOnly? from, DateOnly? to)
        {
            try
            {
                var sesiones = await _sessionService.GetAllForGridAsync(from, to);

                _sessionsLoaded = sesiones.ToList();

                ApplyFilters();

                dgvSales.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (_sessionsLoaded == null) return;

            var listaFiltrada = _sessionsLoaded.AsEnumerable();

            //FILTRO USUARIO (ComboBox)
            if (cbxUsersFilter.SelectedValue != null && cbxUsersFilter.SelectedValue.ToString() != "ALL")
            {
                string userFilter = cbxUsersFilter.SelectedValue.ToString();
                listaFiltrada = listaFiltrada.Where(s => s.Username == userFilter);
            }

            //orden
            if (rbtDesDate != null && rbtDesDate.Checked)
            {
                listaFiltrada = listaFiltrada.OrderByDescending(s => s.OpenedAt);
            }
            else if (rbtDesName != null && rbtDesName.Checked)
            {
                listaFiltrada = listaFiltrada.OrderBy(s => s.Username);
            }
            else if (rbtMinMoney != null && rbtMinMoney.Checked)
            {
                listaFiltrada = listaFiltrada.OrderBy(s => s.Total);
            }
            else if (rbtMaxMoney != null && rbtMaxMoney.Checked)
            {
                listaFiltrada = listaFiltrada.OrderByDescending(s => s.Total);
            }

            dgvSessions.DataSource = listaFiltrada.ToList();
        }

        private async void btnResetFilters_Click(object sender, EventArgs e)
        {
            dtpSince.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;

            // Resetear combo y radios
            if (cbxUsersFilter.Items.Count > 0) cbxUsersFilter.SelectedIndex = 0;
            if (rbtDesDate != null) rbtDesDate.Checked = true;
            lblTotals.Text = "";

            await LoadSessions(null, null);
        }

        private void cbxUsersFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void rbtDesDate_CheckedChanged(object sender, EventArgs e) { if (rbtDesDate.Checked) ApplyFilters(); }
        private void rbtDesName_CheckedChanged(object sender, EventArgs e) { if (rbtDesName.Checked) ApplyFilters(); }
        private void rbtMinMoney_CheckedChanged(object sender, EventArgs e) { if (rbtMinMoney.Checked) ApplyFilters(); }
        private void rbtMaxMoney_CheckedChanged(object sender, EventArgs e) { if (rbtMaxMoney.Checked) ApplyFilters(); }

        private async void dgvSessions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSessions.CurrentRow == null)
            {
                dgvSales.DataSource = null;
                return;
            }

            try
            {
                var sesion = (SessionGridDto)dgvSessions.CurrentRow.DataBoundItem;
                var ventas = await _saleService.GetSalesBySessionAsync(sesion.Id);
                dgvSales.DataSource = ventas.ToList();
                ConfigSalesGrid();
            }
            catch (Exception) { }
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if (dgvSales.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta para ver el detalle.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var venta = (SaleGridDto)dgvSales.CurrentRow.DataBoundItem;
            var formDetalle = _serviceProvider.GetRequiredService<FormSaleDetail>();
            formDetalle.LoadData(venta.Id);
            formDetalle.ShowDialog();
        }

        private void btnGenerateTotals_Click(object sender, EventArgs e)
        {
            if (dgvSessions.DataSource == null || dgvSessions.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos visibles para sumar.", "Información");
                return;
            }

            // Casteamos el DataSource a la lista de objetos que usamos (SessionGridDto)
            // Esto es mucho más rápido que recorrer las filas celda por celda
            if (dgvSessions.DataSource is List<SessionGridDto> listaVisible)
            {
                decimal totalAcumulado = listaVisible.Sum(x => x.Total);

                MessageBox.Show(
                    $"Total acumulado de las {listaVisible.Count} cajas:\n\n" +
                    $"{totalAcumulado.ToString("C2")}",
                    "Cálculo Rápido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblTotals.Text = $"TOTALES: {totalAcumulado.ToString("C2")}";
            }
        }

        private void btnSumSellTotals_Click(object sender, EventArgs e)
        {
            if (dgvSales.DataSource is List<SaleGridDto> ventas && ventas.Count > 0)
            {
                int cantidad = ventas.Count;
                decimal total = ventas.Sum(x => x.Total);

                MessageBox.Show(
                    $"• Cantidad de Ventas: {cantidad}\n" +
                    $"• Total: {total.ToString("C2")}",
                    "Total General",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay ventas para sumar.", "Atención");
            }
        }

        private void btnSumFacts_Click(object sender, EventArgs e)
        {
            if (dgvSales.DataSource is List<SaleGridDto> ventas && ventas.Count > 0)
            {
                var ventasFiscales = ventas.Where(x => x.Fiscal).ToList();

                int cantidad = ventasFiscales.Count;
                decimal total = ventasFiscales.Sum(x => x.Total);

                MessageBox.Show(
                    $"• Cantidad Facturada: {cantidad}\n" +
                    $"• Total: {total.ToString("C2")}",
                    "Resumen Fiscal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay ventas para sumar.", "Atención");
            }
        }
    }
}
