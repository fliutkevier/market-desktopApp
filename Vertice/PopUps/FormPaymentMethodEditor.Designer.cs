namespace WinForms.PopUps
{
    partial class FormPaymentMethodEditor
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
            tableLayoutPanel1 = new TableLayoutPanel();
            txtName = new Krypton.Toolkit.KryptonTextBox();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtRecharge = new Krypton.Toolkit.KryptonTextBox();
            txtDiscount = new Krypton.Toolkit.KryptonTextBox();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(txtName, 0, 1);
            tableLayoutPanel1.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel1.Controls.Add(txtRecharge, 0, 2);
            tableLayoutPanel1.Controls.Add(txtDiscount, 0, 3);
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 5);
            tableLayoutPanel1.Controls.Add(btnAdd, 1, 5);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Size = new Size(489, 448);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            tableLayoutPanel1.SetColumnSpan(txtName, 2);
            txtName.CueHint.CueHintText = "Nombre *";
            txtName.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.CueHint.Padding = new Padding(0);
            txtName.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtName.Location = new Point(30, 97);
            txtName.Name = "txtName";
            txtName.Size = new Size(429, 27);
            txtName.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtName.StateCommon.Border.Width = 2;
            txtName.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.TabIndex = 0;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tableLayoutPanel1.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(128, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(233, 29);
            lblTitle.TabIndex = 23;
            lblTitle.Values.Text = "Agregar Método de Pago";
            // 
            // txtRecharge
            // 
            txtRecharge.Anchor = AnchorStyles.None;
            tableLayoutPanel1.SetColumnSpan(txtRecharge, 2);
            txtRecharge.CueHint.CueHintText = "Recargo % (0 por defecto)";
            txtRecharge.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtRecharge.CueHint.Padding = new Padding(0);
            txtRecharge.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtRecharge.Location = new Point(131, 171);
            txtRecharge.Name = "txtRecharge";
            txtRecharge.Size = new Size(227, 27);
            txtRecharge.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtRecharge.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtRecharge.StateCommon.Border.Width = 2;
            txtRecharge.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtRecharge.TabIndex = 1;
            txtRecharge.TextAlign = HorizontalAlignment.Center;
            // 
            // txtDiscount
            // 
            txtDiscount.Anchor = AnchorStyles.None;
            tableLayoutPanel1.SetColumnSpan(txtDiscount, 2);
            txtDiscount.CueHint.CueHintText = "Descuento % (0 por defecto)";
            txtDiscount.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtDiscount.CueHint.Padding = new Padding(0);
            txtDiscount.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtDiscount.Location = new Point(131, 245);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(227, 27);
            txtDiscount.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtDiscount.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtDiscount.StateCommon.Border.Width = 2;
            txtDiscount.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtDiscount.TabIndex = 2;
            txtDiscount.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(58, 389);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 4;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(303, 389);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 3;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // FormPaymentMethodEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(489, 448);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPaymentMethodEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Metodo de Pago";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonTextBox txtRecharge;
        private Krypton.Toolkit.KryptonTextBox txtDiscount;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnAdd;
    }
}