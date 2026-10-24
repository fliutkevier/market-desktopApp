namespace WinForms.PopUps
{
    partial class FormUserEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpUserEditor = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtUsername = new Krypton.Toolkit.KryptonTextBox();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            btnClose = new Krypton.Toolkit.KryptonButton();
            txtPassword = new Krypton.Toolkit.KryptonTextBox();
            cbxEmployees = new Krypton.Toolkit.KryptonComboBox();
            chkAddUser = new Krypton.Toolkit.KryptonCheckBox();
            tlpUserEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cbxEmployees).BeginInit();
            SuspendLayout();
            // 
            // tlpUserEditor
            // 
            tlpUserEditor.ColumnCount = 4;
            tlpUserEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpUserEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpUserEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpUserEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpUserEditor.Controls.Add(lblTitle, 1, 0);
            tlpUserEditor.Controls.Add(txtUsername, 1, 1);
            tlpUserEditor.Controls.Add(btnAdd, 2, 5);
            tlpUserEditor.Controls.Add(btnClose, 0, 5);
            tlpUserEditor.Controls.Add(txtPassword, 1, 2);
            tlpUserEditor.Controls.Add(cbxEmployees, 2, 3);
            tlpUserEditor.Controls.Add(chkAddUser, 1, 3);
            tlpUserEditor.Dock = DockStyle.Fill;
            tlpUserEditor.Location = new Point(0, 0);
            tlpUserEditor.Name = "tlpUserEditor";
            tlpUserEditor.RowCount = 6;
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpUserEditor.Size = new Size(800, 450);
            tlpUserEditor.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpUserEditor.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(322, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(155, 29);
            lblTitle.TabIndex = 23;
            lblTitle.Values.Text = "Agregar Usuario";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.None;
            tlpUserEditor.SetColumnSpan(txtUsername, 2);
            txtUsername.CueHint.CueHintText = "Nombre de Usuario";
            txtUsername.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtUsername.CueHint.Padding = new Padding(0);
            txtUsername.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtUsername.Location = new Point(261, 99);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(278, 27);
            txtUsername.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtUsername.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtUsername.StateCommon.Border.Width = 2;
            txtUsername.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtUsername.TabIndex = 0;
            txtUsername.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            tlpUserEditor.SetColumnSpan(btnAdd, 2);
            btnAdd.Location = new Point(536, 392);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 3;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.None;
            tlpUserEditor.SetColumnSpan(btnClose, 2);
            btnClose.Location = new Point(136, 392);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(127, 40);
            btnClose.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnClose.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnClose.TabIndex = 4;
            btnClose.Values.DropDownArrowColor = Color.Empty;
            btnClose.Values.Text = "Cancelar";
            btnClose.Click += btnClose_Click;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            tlpUserEditor.SetColumnSpan(txtPassword, 2);
            txtPassword.CueHint.CueHintText = "Contraseña";
            txtPassword.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPassword.CueHint.Padding = new Padding(0);
            txtPassword.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtPassword.Location = new Point(261, 174);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(278, 27);
            txtPassword.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPassword.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtPassword.StateCommon.Border.Width = 2;
            txtPassword.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPassword.TabIndex = 1;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // cbxEmployees
            // 
            cbxEmployees.Anchor = AnchorStyles.None;
            cbxEmployees.DropDownWidth = 152;
            cbxEmployees.Location = new Point(418, 251);
            cbxEmployees.Name = "cbxEmployees";
            cbxEmployees.Size = new Size(164, 22);
            cbxEmployees.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxEmployees.TabIndex = 2;
            cbxEmployees.Text = "Empleados";
            cbxEmployees.Visible = false;
            // 
            // chkAddUser
            // 
            chkAddUser.Anchor = AnchorStyles.None;
            chkAddUser.Location = new Point(216, 252);
            chkAddUser.Name = "chkAddUser";
            chkAddUser.Size = new Size(168, 21);
            chkAddUser.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkAddUser.TabIndex = 43;
            chkAddUser.Values.Text = "Asignar a Empleado";
            chkAddUser.CheckedChanged += chkAddUser_CheckedChanged;
            // 
            // FormUserEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(800, 450);
            Controls.Add(tlpUserEditor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormUserEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Usuario";
            tlpUserEditor.ResumeLayout(false);
            tlpUserEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cbxEmployees).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpUserEditor;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtUsername;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnClose;
        private Krypton.Toolkit.KryptonTextBox txtPassword;
        private Krypton.Toolkit.KryptonComboBox cbxEmployees;
        private Krypton.Toolkit.KryptonCheckBox chkAddUser;
    }
}