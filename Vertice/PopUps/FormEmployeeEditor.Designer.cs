namespace WinForms.PopUps
{
    partial class FormEmployeeEditor
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
            tlpEmployees = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtName = new Krypton.Toolkit.KryptonTextBox();
            txtDni = new Krypton.Toolkit.KryptonTextBox();
            txtEmail = new Krypton.Toolkit.KryptonTextBox();
            txtLastName = new Krypton.Toolkit.KryptonTextBox();
            txtCuit = new Krypton.Toolkit.KryptonTextBox();
            txtPhone = new Krypton.Toolkit.KryptonTextBox();
            txtAddress = new Krypton.Toolkit.KryptonTextBox();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            txtUsername = new Krypton.Toolkit.KryptonTextBox();
            txtPassword = new Krypton.Toolkit.KryptonTextBox();
            cbxAddUser = new Krypton.Toolkit.KryptonCheckBox();
            dtpBirthDay = new Krypton.Toolkit.KryptonDateTimePicker();
            lblBirthDay = new Krypton.Toolkit.KryptonLabel();
            lblHiredDate = new Krypton.Toolkit.KryptonLabel();
            cbxDateHiredToday = new Krypton.Toolkit.KryptonCheckBox();
            dtpHiredDate = new Krypton.Toolkit.KryptonDateTimePicker();
            tlpEmployees.SuspendLayout();
            SuspendLayout();
            // 
            // tlpEmployees
            // 
            tlpEmployees.ColumnCount = 6;
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666718F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tlpEmployees.Controls.Add(lblTitle, 2, 0);
            tlpEmployees.Controls.Add(txtName, 0, 1);
            tlpEmployees.Controls.Add(txtDni, 2, 1);
            tlpEmployees.Controls.Add(txtEmail, 4, 1);
            tlpEmployees.Controls.Add(txtLastName, 0, 2);
            tlpEmployees.Controls.Add(txtCuit, 2, 2);
            tlpEmployees.Controls.Add(txtPhone, 4, 2);
            tlpEmployees.Controls.Add(txtAddress, 4, 3);
            tlpEmployees.Controls.Add(btnCancel, 1, 7);
            tlpEmployees.Controls.Add(btnAdd, 3, 7);
            tlpEmployees.Controls.Add(txtUsername, 0, 4);
            tlpEmployees.Controls.Add(txtPassword, 0, 5);
            tlpEmployees.Controls.Add(cbxAddUser, 0, 3);
            tlpEmployees.Controls.Add(dtpBirthDay, 5, 4);
            tlpEmployees.Controls.Add(lblBirthDay, 3, 4);
            tlpEmployees.Controls.Add(lblHiredDate, 3, 5);
            tlpEmployees.Controls.Add(cbxDateHiredToday, 5, 5);
            tlpEmployees.Controls.Add(dtpHiredDate, 4, 6);
            tlpEmployees.Dock = DockStyle.Fill;
            tlpEmployees.Location = new Point(0, 0);
            tlpEmployees.Name = "tlpEmployees";
            tlpEmployees.RowCount = 8;
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpEmployees.Size = new Size(800, 450);
            tlpEmployees.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(311, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(175, 29);
            lblTitle.TabIndex = 31;
            lblTitle.Values.Text = "Agregar Empleado";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtName, 2);
            txtName.CueHint.CueHintText = "Nombre *";
            txtName.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtName.CueHint.Padding = new Padding(0);
            txtName.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtName.Location = new Point(3, 70);
            txtName.Name = "txtName";
            txtName.Size = new Size(260, 27);
            txtName.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtName.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtName.StateCommon.Border.Width = 2;
            txtName.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtName.TabIndex = 0;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtDni
            // 
            txtDni.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtDni, 2);
            txtDni.CueHint.CueHintText = "DNI *";
            txtDni.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtDni.CueHint.Padding = new Padding(0);
            txtDni.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtDni.Location = new Point(269, 70);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(260, 27);
            txtDni.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtDni.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtDni.StateCommon.Border.Width = 2;
            txtDni.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtDni.TabIndex = 2;
            txtDni.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtEmail, 2);
            txtEmail.CueHint.CueHintText = "Email";
            txtEmail.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtEmail.CueHint.Padding = new Padding(0);
            txtEmail.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtEmail.Location = new Point(535, 70);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(262, 27);
            txtEmail.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtEmail.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtEmail.StateCommon.Border.Width = 2;
            txtEmail.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtEmail.TabIndex = 4;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtLastName
            // 
            txtLastName.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtLastName, 2);
            txtLastName.CueHint.CueHintText = "Apellido *";
            txtLastName.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtLastName.CueHint.Padding = new Padding(0);
            txtLastName.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtLastName.Location = new Point(3, 126);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(260, 27);
            txtLastName.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtLastName.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtLastName.StateCommon.Border.Width = 2;
            txtLastName.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtLastName.TabIndex = 1;
            txtLastName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCuit
            // 
            txtCuit.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtCuit, 2);
            txtCuit.CueHint.CueHintText = "CUIT *";
            txtCuit.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtCuit.CueHint.Padding = new Padding(0);
            txtCuit.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtCuit.Location = new Point(269, 126);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(260, 27);
            txtCuit.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtCuit.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtCuit.StateCommon.Border.Width = 2;
            txtCuit.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtCuit.TabIndex = 3;
            txtCuit.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhone
            // 
            txtPhone.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtPhone, 2);
            txtPhone.CueHint.CueHintText = "Telefono";
            txtPhone.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPhone.CueHint.Padding = new Padding(0);
            txtPhone.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtPhone.Location = new Point(535, 126);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(262, 27);
            txtPhone.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPhone.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtPhone.StateCommon.Border.Width = 2;
            txtPhone.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPhone.TabIndex = 5;
            txtPhone.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtAddress, 2);
            txtAddress.CueHint.CueHintText = "Dirección";
            txtAddress.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtAddress.CueHint.Padding = new Padding(0);
            txtAddress.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtAddress.Location = new Point(535, 182);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(262, 27);
            txtAddress.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtAddress.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtAddress.StateCommon.Border.Width = 2;
            txtAddress.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtAddress.TabIndex = 6;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(btnCancel, 2);
            btnCancel.Location = new Point(202, 401);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 14;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(btnAdd, 2);
            btnAdd.Location = new Point(468, 401);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 13;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtUsername, 2);
            txtUsername.CueHint.CueHintText = "Usuario";
            txtUsername.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtUsername.CueHint.Padding = new Padding(0);
            txtUsername.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtUsername.Location = new Point(53, 238);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(160, 27);
            txtUsername.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtUsername.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtUsername.StateCommon.Border.Width = 2;
            txtUsername.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtUsername.TabIndex = 8;
            txtUsername.TextAlign = HorizontalAlignment.Center;
            txtUsername.Visible = false;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtPassword, 2);
            txtPassword.CueHint.CueHintText = "Contraseña";
            txtPassword.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPassword.CueHint.Padding = new Padding(0);
            txtPassword.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtPassword.Location = new Point(51, 294);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(164, 27);
            txtPassword.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPassword.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtPassword.StateCommon.Border.Width = 2;
            txtPassword.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtPassword.TabIndex = 9;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            txtPassword.Visible = false;
            // 
            // cbxAddUser
            // 
            cbxAddUser.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(cbxAddUser, 2);
            cbxAddUser.Location = new Point(62, 185);
            cbxAddUser.Name = "cbxAddUser";
            cbxAddUser.Size = new Size(141, 21);
            cbxAddUser.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxAddUser.TabIndex = 7;
            cbxAddUser.Values.Text = "Agregar Usuario";
            cbxAddUser.CheckedChanged += cbxAddUser_CheckedChanged;
            // 
            // dtpBirthDay
            // 
            dtpBirthDay.Anchor = AnchorStyles.None;
            dtpBirthDay.Format = DateTimePickerFormat.Short;
            dtpBirthDay.Location = new Point(678, 240);
            dtpBirthDay.Name = "dtpBirthDay";
            dtpBirthDay.Size = new Size(109, 23);
            dtpBirthDay.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            dtpBirthDay.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            dtpBirthDay.TabIndex = 10;
            // 
            // lblBirthDay
            // 
            lblBirthDay.Anchor = AnchorStyles.Right;
            tlpEmployees.SetColumnSpan(lblBirthDay, 2);
            lblBirthDay.Location = new Point(494, 241);
            lblBirthDay.Name = "lblBirthDay";
            lblBirthDay.Size = new Size(168, 21);
            lblBirthDay.StateCommon.Padding = new Padding(-1, -1, 8, -1);
            lblBirthDay.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBirthDay.TabIndex = 43;
            lblBirthDay.Values.Text = "Fecha de Nacimiento";
            // 
            // lblHiredDate
            // 
            lblHiredDate.Anchor = AnchorStyles.Right;
            tlpEmployees.SetColumnSpan(lblHiredDate, 2);
            lblHiredDate.Location = new Point(484, 297);
            lblHiredDate.Name = "lblHiredDate";
            lblHiredDate.Size = new Size(178, 21);
            lblHiredDate.StateCommon.Padding = new Padding(-1, -1, 8, -1);
            lblHiredDate.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHiredDate.TabIndex = 44;
            lblHiredDate.Values.Text = "Fecha de Contratación";
            // 
            // cbxDateHiredToday
            // 
            cbxDateHiredToday.Anchor = AnchorStyles.None;
            cbxDateHiredToday.Checked = true;
            cbxDateHiredToday.CheckState = CheckState.Checked;
            cbxDateHiredToday.Location = new Point(706, 297);
            cbxDateHiredToday.Name = "cbxDateHiredToday";
            cbxDateHiredToday.Size = new Size(53, 21);
            cbxDateHiredToday.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxDateHiredToday.TabIndex = 11;
            cbxDateHiredToday.Values.Text = "Hoy";
            cbxDateHiredToday.CheckedChanged += cbxDateHiredToday_CheckedChanged;
            // 
            // dtpHiredDate
            // 
            dtpHiredDate.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(dtpHiredDate, 2);
            dtpHiredDate.Format = DateTimePickerFormat.Short;
            dtpHiredDate.Location = new Point(605, 352);
            dtpHiredDate.Name = "dtpHiredDate";
            dtpHiredDate.Size = new Size(122, 23);
            dtpHiredDate.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            dtpHiredDate.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            dtpHiredDate.TabIndex = 12;
            dtpHiredDate.Visible = false;
            // 
            // FormEmployeeEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(800, 450);
            Controls.Add(tlpEmployees);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormEmployeeEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Empleado";
            tlpEmployees.ResumeLayout(false);
            tlpEmployees.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpEmployees;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonTextBox txtLastName;
        private Krypton.Toolkit.KryptonTextBox txtDni;
        private Krypton.Toolkit.KryptonTextBox txtCuit;
        private Krypton.Toolkit.KryptonTextBox txtEmail;
        private Krypton.Toolkit.KryptonTextBox txtPhone;
        private Krypton.Toolkit.KryptonTextBox txtAddress;
        private Krypton.Toolkit.KryptonDateTimePicker dtpBirthDay;
        private Krypton.Toolkit.KryptonTextBox txtUsername;
        private Krypton.Toolkit.KryptonTextBox txtPassword;
        private Krypton.Toolkit.KryptonCheckBox cbxAddUser;
        private Krypton.Toolkit.KryptonLabel lblBirthDay;
        private Krypton.Toolkit.KryptonLabel lblHiredDate;
        private Krypton.Toolkit.KryptonCheckBox cbxDateHiredToday;
        private Krypton.Toolkit.KryptonDateTimePicker dtpHiredDate;
    }
}