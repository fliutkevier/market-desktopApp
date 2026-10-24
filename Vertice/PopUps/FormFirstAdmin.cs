using Application.Users.Dtos;
using Application.Users.Interfaces;
using Krypton.Toolkit;

namespace WinForms.PopUps
{
    /// <summary>
    /// Se muestra solo en el primer arranque, cuando la base no tiene ningún usuario: pide crear
    /// el administrador en el momento. Así no hay usuario ni contraseña fijos en el código.
    /// </summary>
    public class FormFirstAdmin : Form
    {
        private readonly IUserService _userService;

        private readonly KryptonTextBox txtUsername = new KryptonTextBox();
        private readonly KryptonTextBox txtPassword = new KryptonTextBox();
        private readonly KryptonTextBox txtConfirm = new KryptonTextBox();
        private readonly KryptonButton btnCreate = new KryptonButton();
        private readonly KryptonButton btnCancel = new KryptonButton();

        public FormFirstAdmin(IUserService userService)
        {
            _userService = userService;
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Vertice POS - Primer uso";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 330);
            BackColor = Color.FromArgb(238, 239, 249);
            Font = new Font("Microsoft Sans Serif", 11.25F);

            var lblTitle = new Label
            {
                Text = "CREAR USUARIO ADMINISTRADOR",
                Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 25, 70),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(20, 15, 380, 30)
            };

            var lblInfo = new Label
            {
                Text = "No hay usuarios cargados. Cree el administrador para ingresar al sistema.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(20, 50, 380, 45)
            };

            ConfigTextBox(txtUsername, "USUARIO", 105, isPassword: false);
            ConfigTextBox(txtPassword, "CONTRASEÑA", 155, isPassword: true);
            ConfigTextBox(txtConfirm, "REPETIR CONTRASEÑA", 205, isPassword: true);

            ConfigButton(btnCreate, "CREAR", new Point(60, 265));
            ConfigButton(btnCancel, "SALIR", new Point(225, 265));

            btnCreate.Click += btnCreate_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            AcceptButton = btnCreate;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[] { lblTitle, lblInfo, txtUsername, txtPassword, txtConfirm, btnCreate, btnCancel });
        }

        private static void ConfigTextBox(KryptonTextBox txt, string cue, int top, bool isPassword)
        {
            txt.Bounds = new Rectangle(60, top, 300, 40);
            txt.CueHint.CueHintText = cue;
            txt.CueHint.TextH = PaletteRelativeAlign.Center;
            txt.TextAlign = HorizontalAlignment.Center;
            txt.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txt.StateCommon.Border.Width = 2;
            txt.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txt.StateCommon.Content.Padding = new Padding(1, 8, 1, 8);
            txt.UseSystemPasswordChar = isPassword;
        }

        private static void ConfigButton(KryptonButton btn, string text, Point location)
        {
            btn.Bounds = new Rectangle(location, new Size(135, 40));
            btn.Values.Text = text;
            btn.Values.DropDownArrowColor = Color.Empty;
            btn.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btn.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
        }

        private async void btnCreate_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Complete usuario y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != txtConfirm.Text.Trim())
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();
                return;
            }

            try
            {
                btnCreate.Enabled = false;

                // Sin empleado asociado: CreateUserAsync lo crea con rol Administrador.
                await _userService.CreateUserAsync(new CreateUserDto
                {
                    Username = username,
                    Password = password,
                    Role = 'A'
                });

                MessageBox.Show($"Administrador '{username}' creado. Ya puede ingresar.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Incluye las validaciones del DTO (ej. contraseña de menos de 4 caracteres).
                MessageBox.Show(ex.Message, "No se pudo crear el usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnCreate.Enabled = true;
            }
        }
    }
}
