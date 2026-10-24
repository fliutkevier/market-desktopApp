using Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WinForms;
using WinForms.AdminForms;

namespace Vertice
{
    public partial class FormLogin : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        public FormLogin(
            IServiceProvider serviceProvider,
            IAuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _authService = authService;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            //Verificación

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnLogin.Enabled = false;
                btnClose.Enabled = false;

                bool isValid = await _authService.ValidateUserAsync(userName, password);

                if(!isValid)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var formAdminDashboard = _serviceProvider.GetRequiredService<FormAdminDashboard>();
                formAdminDashboard.Show();

                txtUsername.Text = "";
                txtPassword.Text = "";
                formAdminDashboard.FormClosed += (s, ev) => System.Windows.Forms.Application.Exit();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar iniciar sesión: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnLogin.Enabled = true;
                btnClose.Enabled = true;
            }
        }
    }
}
