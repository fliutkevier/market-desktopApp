namespace Vertice
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtUsername = new Krypton.Toolkit.KryptonTextBox();
            txtPassword = new Krypton.Toolkit.KryptonTextBox();
            btnLogin = new Krypton.Toolkit.KryptonButton();
            btnClose = new Krypton.Toolkit.KryptonButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            lblTitle.Location = new Point(204, 65);
            lblTitle.Name = "lblTitle";
            lblTitle.PaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            lblTitle.Size = new Size(128, 29);
            lblTitle.TabIndex = 0;
            lblTitle.Values.Text = "Iniciar Sesión";
            // 
            // txtUsername
            // 
            txtUsername.CueHint.CueHintText = "Usuario";
            txtUsername.CueHint.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtUsername.CueHint.Padding = new Padding(0);
            txtUsername.Location = new Point(101, 141);
            txtUsername.Name = "txtUsername";
            txtUsername.PaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            txtUsername.Size = new Size(331, 23);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.CueHint.CueHintText = "Contraseña";
            txtPassword.CueHint.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic);
            txtPassword.CueHint.Padding = new Padding(0);
            txtPassword.Location = new Point(101, 224);
            txtPassword.Name = "txtPassword";
            txtPassword.PaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(331, 23);
            txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(287, 312);
            btnLogin.Name = "btnLogin";
            btnLogin.PaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            btnLogin.Size = new Size(145, 41);
            btnLogin.TabIndex = 5;
            btnLogin.Values.DropDownArrowColor = Color.Empty;
            btnLogin.Values.Text = "Ingresar";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(101, 312);
            btnClose.Name = "btnClose";
            btnClose.PaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            btnClose.Size = new Size(145, 41);
            btnClose.TabIndex = 6;
            btnClose.Values.DropDownArrowColor = Color.Empty;
            btnClose.Values.Text = "Salir";
            btnClose.Click += btnClose_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(539, 450);
            Controls.Add(btnClose);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de Sesión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtUsername;
        private Krypton.Toolkit.KryptonTextBox txtPassword;
        private Krypton.Toolkit.KryptonButton btnLogin;
        private Krypton.Toolkit.KryptonButton btnClose;
    }
}
