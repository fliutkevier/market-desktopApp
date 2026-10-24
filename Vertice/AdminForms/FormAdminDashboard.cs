using Application.Sales.Interfaces;
using Application.Sessions;
using Application.Sessions.Interfaces;
using Domain.RepositoryInterfaces;
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

namespace WinForms.AdminForms
{
    public partial class FormAdminDashboard : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserSessionService _userSessionService;
        //private readonly ISessionService _sessionService;

        private Control? _activeControl;
        private IServiceScope? _currentScope;

        public FormAdminDashboard(
            IServiceProvider serviceProvider,
            IUserSessionService userSessionService)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;
            //_sessionService = sessionService;

            this.KeyPreview = true; //El form captura teclas antes que los controles
        }

        private void OpenScreen<T>() where T : Control
        {
            try
            {
                //Limpieza de la pantalla anterior
                if (_activeControl != null)
                {
                    this.pnlContent.Controls.Remove(_activeControl);
                    _activeControl.Dispose();
                    _activeControl = null;
                }

                //Limpieza del Scope anterior
                if (_currentScope != null)
                {
                    _currentScope.Dispose();
                    _currentScope = null;
                }

                //Crear NUEVO Scope
                _currentScope = _serviceProvider.CreateScope();

                //Resolver el control dentro de este nuevo scope
                T control = _currentScope.ServiceProvider.GetRequiredService<T>();

                //Mostrar
                this.pnlMenu.Visible = false;
                ShowNavbar();

                _activeControl = control;
                control.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Add(control);
                control.BringToFront();
                control.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir pantalla: {ex.Message}");
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Si ya se manejó la tecla, salimos
            if (e.Handled) return;

            switch (e.KeyCode)
            {
                case Keys.F1:
                    e.Handled = true;
                    // Simulamos clic en el botón (o llamamos a la lógica directa)
                    if (btnSellNavbar != null && btnSellNavbar.Enabled)
                        btnSellNavbar.PerformClick();
                    break;

                case Keys.F2: // Productos
                    e.Handled = true;
                    if (btnProductsNavbar != null && btnProductsNavbar.Enabled)
                        btnProductsNavbar.PerformClick();
                    break;

                case Keys.F3: // Historial Ventas
                    e.Handled = true;
                    if (btnHistoryNavbar != null && btnHistoryNavbar.Enabled)
                        btnHistoryNavbar.PerformClick();
                    break;

                case Keys.F4: // Caja
                    e.Handled = true;
                    if (btnInitSession != null && btnInitSession.Enabled)
                        btnInitSession.PerformClick();
                    break;

                case Keys.F10: // Cerrar Sesión
                    e.Handled = true;
                    if (btnLogOutNavbar != null && btnLogOutNavbar.Enabled)
                        btnLogOutNavbar.PerformClick();
                    break;

                case Keys.Escape: // Volver al Home (Si no estamos en Home)
                    // Solo si el menú está oculto (significa que hay un control abierto)
                    if (!pnlMenu.Visible)
                    {
                        e.Handled = true;
                        btnHome_Click_1(this, EventArgs.Empty);
                    }
                    break;
            }
        }

        /* OpenControl Viejo
        private void OpenControl(Control childControl)
        {
            this.pnlMenu.Visible = false;
            ShowNavbar();

            if (_activeControl != null)
            {
                this.pnlContent.Controls.Remove(_activeControl);
                _activeControl.Dispose();
            }

            _activeControl = childControl;
            childControl.Dock = DockStyle.Fill;
            this.pnlContent.Controls.Add(childControl);
            childControl.BringToFront();
        }*/

        private void ShowNavbar()
        {
            btnHome.Visible = true;
            btnHome.Enabled = true;
            btnSellNavbar.Visible = true;
            btnProductsNavbar.Visible = true;
            btnHistoryNavbar.Visible = true;
            btnLogOutNavbar.Visible = true;
            btnSellNavbar.Enabled = true;
            btnProductsNavbar.Enabled = true;
            btnHistoryNavbar.Enabled = true;
            btnLogOutNavbar.Enabled = true;
        }

        private void HideNavbar()
        {
            btnHome.Visible = false;
            btnSellNavbar.Visible = false;
            btnProductsNavbar.Visible = false;
            btnHistoryNavbar.Visible = false;
            btnLogOutNavbar.Visible = false;
            btnHome.Enabled = false;
            btnSellNavbar.Enabled = false;
            btnProductsNavbar.Enabled = false;
            btnHistoryNavbar.Enabled = false;
            btnLogOutNavbar.Enabled = false;
        }

        private async void FormAdminDashboard_Load(object sender, EventArgs e)
        {
            btnHome_Click_1(sender, e);
            HideNavbar();
            string username = $"Usuario: {_userSessionService.Username}";
            lblUsername.Text = username;
            lblUserFooter.Text = username;

            ApplyRolPermissions();

            await VerifySessionState();
        }

        private void ApplyRolPermissions()
        {
            string rol = _userSessionService.Role?.ToString() ?? "E";

            if (rol == "E")
            {
                if (btnLogsMenu != null)
                {
                    btnLogsMenu.Enabled = false;
                    btnLogsMenu.Visible= false;
                }

                if (btnSettings != null)
                {
                    btnSettings.Enabled = false;
                    btnSettings.Visible = false;
                }

                if (btnUsersMenu != null)
                {
                    btnUsersMenu.Enabled = false;
                    btnUsersMenu.Visible = false;
                }

                if (btnPaymentMethodsMenu != null)
                {
                    btnPaymentMethodsMenu.Enabled = false;
                    btnPaymentMethodsMenu.Visible = false;
                }

                if (btnEmployeesMenu != null)
                {
                    btnEmployeesMenu.Enabled = false;
                    btnEmployeesMenu.Visible = false;
                }
            }
        }

        private async Task VerifySessionState()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                try
                {
                    string username = _userSessionService.Username;

                    var currentSession = await sessionService.GetCurrentSessionAsync(username);

                    if (currentSession != null)
                    {
                        var fechaSesion = currentSession.Date;
                        var fechaHoy = DateOnly.FromDateTime(DateTime.Now);

                        if (fechaSesion < fechaHoy)
                        {
                            var result = MessageBox.Show(
                                $"Tenés una caja abierta del día {fechaSesion} sin cerrar.\n¿Querés continuarla o cerrarla?\n\nSI: Continuar\nNO: Cerrar ahora",
                                "Caja Pendiente",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (result == DialogResult.No)
                            {
                                await sessionService.CloseSessionAsync(username);
                                MessageBox.Show("La caja anterior se cerró. Podés abrir una nueva.", "Caja Cerrada");
                            }
                        }
                    }

                    bool isOpen = await sessionService.HasOpenSessionAsync(username);
                    UpdateSessionButton(isOpen);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar estado de caja: " + ex.Message);
                }
            }
        }
        /*
        private async Task UpdateSessionButton()
        {
            try
            {
                bool isOpen = await _sessionService.HasOpenSessionAsync(_userSessionService.Username);

                if (isOpen)
                {
                    btnInitSession.Text = "CERRAR CAJA [F4]";
                    btnInitSession.BackColor = Color.Salmon;
                }
                else
                {
                    btnInitSession.Text = "ABRIR CAJA [F4]";
                    btnInitSession.BackColor = Color.LightGreen;
                }
            }
            catch { }
        }*/

        private void UpdateSessionButton(bool isOpen)
        {
            if (isOpen)
            {
                btnInitSession.Text = "CERRAR CAJA [F4]";
                btnInitSession.StateCommon.Back.Color1 = Color.Salmon;
            }
            else
            {
                btnInitSession.Text = "ABRIR CAJA [F4]";
                btnInitSession.StateCommon.Back.Color1 = Color.LightGreen;
            }
        }

        private void clockTimer_Tick(object sender, EventArgs e) => lblTime.Text = DateTime.Now.ToString("HH:mm");

        private void btnInitSellMenu_Click_1(object sender, EventArgs e) => OpenScreen<UserControlNewSales>();

        private void btnProductsMenu_Click_1(object sender, EventArgs e) => OpenScreen<UserControlProducts>();

        private void btnSalesHistory_Click_1(object sender, EventArgs e) => OpenScreen<UserControlHistory>();

        private void btnProvidersMenu_Click_1(object sender, EventArgs e) => OpenScreen<UserControlProviders>();

        private void btnEmployeesMenu_Click_1(object sender, EventArgs e) => OpenScreen<UserControlEmployees>();

        private void btnLogsMenu_Click_1(object sender, EventArgs e) => OpenScreen<UserControlLogs>();

        private void btnPaymentMethodsMenu_Click(object sender, EventArgs e) => OpenScreen<UserControlPaymentMethods>();

        private void btnPurchaseHistory_Click_1(object sender, EventArgs e) => OpenScreen<UserControlProvidersHistory>();

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            if (_activeControl != null)
            {
                this.pnlContent.Controls.Remove(_activeControl);
                _activeControl.Dispose();
                _activeControl = null;
            }

            if (_currentScope != null)
            {
                _currentScope.Dispose();
                _currentScope = null;
            }

            this.pnlMenu.Visible = true;
            HideNavbar();
        }

        private void btnLogOutNavbar_Click(object sender, EventArgs e) => this.Close();

        private void btnLogOutMenu_Click(object sender, EventArgs e) => this.Close();

        private async void btnInitSession_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
                string username = _userSessionService.Username;

                try
                {
                    bool isOpen = await sessionService.HasOpenSessionAsync(username);

                    if (isOpen)
                    {
                        // Las ventas en espera sobreviven al cierre de caja, así que una venta
                        // parada acá se terminaría cobrando dentro de la sesión siguiente.
                        var enEspera = _serviceProvider.GetRequiredService<ISuspendedSaleService>()
                            .GetAll().Where(s => !s.IsEmpty).ToList();

                        if (enEspera.Count > 0)
                        {
                            var seguir = MessageBox.Show(
                                $"Hay {enEspera.Count} venta(s) en espera sin cobrar (total ${enEspera.Sum(s => s.SubTotal):N2}).\n\n" +
                                "Si cerrás la caja, esas ventas van a quedar para la caja siguiente.\n\n¿Cerrar la caja igual?",
                                "Ventas en espera sin cobrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                            if (seguir == DialogResult.No) return;
                        }

                        if (MessageBox.Show("¿Desea cerrar la caja actual?", "Cerrar Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            await sessionService.CloseSessionAsync(username);
                            MessageBox.Show("Caja cerrada.");
                            UpdateSessionButton(false);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("¿Desea abrir una nueva caja?", "Abrir Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            await sessionService.OpenSessionAsync(username);
                            MessageBox.Show("Caja abierta.");
                            UpdateSessionButton(true);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error al cambiar estado de caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            /*
            try
            {
                string username = _userSessionService.Username;
                bool isOpen = await _sessionService.HasOpenSessionAsync(username);

                if (isOpen)
                {
                    // Si está abierta -> Intentar cerrar
                    var confirm = MessageBox.Show("¿Desea cerrar la caja actual?", "Cerrar Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        await _sessionService.CloseSessionAsync(username);
                        MessageBox.Show("Caja cerrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await UpdateSessionButton();
                    }
                }
                else
                {
                    // Si está cerrada -> Intentar abrir
                    var confirm = MessageBox.Show("¿Desea abrir una nueva caja?", "Abrir Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        await _sessionService.OpenSessionAsync(username);
                        MessageBox.Show("Caja abierta correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await UpdateSessionButton();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar estado de caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void btnUsersMenu_Click(object sender, EventArgs e) => OpenScreen<UserControlUsers>();

        private void btnSettings_Click(object sender, EventArgs e) => OpenScreen<UserControlSettings>();

        private void btnSellNavbar_Click(object sender, EventArgs e) => btnInitSellMenu_Click_1(sender, e);

        private void btnProductsNavbar_Click(object sender, EventArgs e) => btnProductsMenu_Click_1(sender, e);

        private void btnHistoryNavbar_Click(object sender, EventArgs e) => btnSalesHistory_Click_1(sender, e);
    }
}
