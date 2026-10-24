namespace WinForms.PopUps
{
    partial class FormProviderEditor
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
            tlpProviderEditor = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtCuit = new Krypton.Toolkit.KryptonTextBox();
            txtName = new Krypton.Toolkit.KryptonTextBox();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            txtEmail = new Krypton.Toolkit.KryptonTextBox();
            txtPhone = new Krypton.Toolkit.KryptonTextBox();
            txtAddress = new Krypton.Toolkit.KryptonTextBox();
            tlpProviderEditor.SuspendLayout();
            SuspendLayout();
            // 
            // tlpProviderEditor
            // 
            tlpProviderEditor.ColumnCount = 4;
            tlpProviderEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.68404F));
            tlpProviderEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.3724442F));
            tlpProviderEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.25947F));
            tlpProviderEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.68404F));
            tlpProviderEditor.Controls.Add(lblTitle, 1, 0);
            tlpProviderEditor.Controls.Add(txtCuit, 1, 1);
            tlpProviderEditor.Controls.Add(txtName, 1, 2);
            tlpProviderEditor.Controls.Add(btnCancel, 1, 6);
            tlpProviderEditor.Controls.Add(btnAdd, 2, 6);
            tlpProviderEditor.Controls.Add(txtEmail, 1, 3);
            tlpProviderEditor.Controls.Add(txtPhone, 1, 4);
            tlpProviderEditor.Controls.Add(txtAddress, 1, 5);
            tlpProviderEditor.Dock = DockStyle.Fill;
            tlpProviderEditor.Location = new Point(0, 0);
            tlpProviderEditor.Name = "tlpProviderEditor";
            tlpProviderEditor.RowCount = 7;
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpProviderEditor.Size = new Size(584, 409);
            tlpProviderEditor.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(202, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(179, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Values.Text = "Agregar Proveedor";
            // 
            // txtCuit
            // 
            txtCuit.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(txtCuit, 2);
            txtCuit.CueHint.CueHintText = "CUIT *";
            txtCuit.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCuit.CueHint.Padding = new Padding(0);
            txtCuit.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtCuit.Location = new Point(111, 76);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(361, 22);
            txtCuit.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCuit.StateActive.Content.Padding = new Padding(0);
            txtCuit.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtCuit.StateCommon.Border.Width = 2;
            txtCuit.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCuit.TabIndex = 0;
            txtCuit.TextAlign = HorizontalAlignment.Center;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(txtName, 2);
            txtName.CueHint.CueHintText = "Nombre *";
            txtName.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.CueHint.Padding = new Padding(0);
            txtName.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtName.Location = new Point(113, 134);
            txtName.Name = "txtName";
            txtName.Size = new Size(357, 22);
            txtName.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.StateActive.Content.Padding = new Padding(0);
            txtName.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtName.StateCommon.Border.Width = 2;
            txtName.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.TabIndex = 1;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(105, 358);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 6;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(357, 358);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 5;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(txtEmail, 2);
            txtEmail.CueHint.CueHintText = "Email";
            txtEmail.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtEmail.CueHint.Padding = new Padding(0);
            txtEmail.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtEmail.Location = new Point(113, 192);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(357, 22);
            txtEmail.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtEmail.StateActive.Content.Padding = new Padding(0);
            txtEmail.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtEmail.StateCommon.Border.Width = 2;
            txtEmail.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtEmail.TabIndex = 2;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhone
            // 
            txtPhone.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(txtPhone, 2);
            txtPhone.CueHint.CueHintText = "Teléfono";
            txtPhone.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPhone.CueHint.Padding = new Padding(0);
            txtPhone.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtPhone.Location = new Point(113, 250);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(357, 22);
            txtPhone.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPhone.StateActive.Content.Padding = new Padding(0);
            txtPhone.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtPhone.StateCommon.Border.Width = 2;
            txtPhone.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPhone.TabIndex = 3;
            txtPhone.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Anchor = AnchorStyles.None;
            tlpProviderEditor.SetColumnSpan(txtAddress, 2);
            txtAddress.CueHint.CueHintText = "Dirección";
            txtAddress.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtAddress.CueHint.Padding = new Padding(0);
            txtAddress.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtAddress.Location = new Point(113, 308);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(357, 22);
            txtAddress.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtAddress.StateActive.Content.Padding = new Padding(0);
            txtAddress.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtAddress.StateCommon.Border.Width = 2;
            txtAddress.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtAddress.TabIndex = 4;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // FormProviderEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(584, 409);
            Controls.Add(tlpProviderEditor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormProviderEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Proveedor";
            tlpProviderEditor.ResumeLayout(false);
            tlpProviderEditor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpProviderEditor;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtCuit;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonTextBox txtEmail;
        private Krypton.Toolkit.KryptonTextBox txtPhone;
        private Krypton.Toolkit.KryptonTextBox txtAddress;
    }
}