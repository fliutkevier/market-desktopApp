namespace WinForms.PopUps
{
    partial class FormProductEditor
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
            tlpProducts = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtCode = new Krypton.Toolkit.KryptonTextBox();
            txtName = new Krypton.Toolkit.KryptonTextBox();
            txtStock = new Krypton.Toolkit.KryptonTextBox();
            txtCostPrice = new Krypton.Toolkit.KryptonTextBox();
            txtGainPercent = new Krypton.Toolkit.KryptonTextBox();
            txtPrice = new Krypton.Toolkit.KryptonTextBox();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            tlpProducts.SuspendLayout();
            SuspendLayout();
            // 
            // tlpProducts
            // 
            tlpProducts.BackColor = Color.Transparent;
            tlpProducts.ColumnCount = 4;
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.0000038F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpProducts.Controls.Add(lblTitle, 1, 0);
            tlpProducts.Controls.Add(txtCode, 0, 1);
            tlpProducts.Controls.Add(txtName, 2, 1);
            tlpProducts.Controls.Add(txtStock, 0, 2);
            tlpProducts.Controls.Add(txtCostPrice, 2, 2);
            tlpProducts.Controls.Add(txtGainPercent, 2, 3);
            tlpProducts.Controls.Add(txtPrice, 2, 4);
            tlpProducts.Controls.Add(btnCancel, 1, 6);
            tlpProducts.Controls.Add(btnAdd, 2, 6);
            tlpProducts.Dock = DockStyle.Fill;
            tlpProducts.Location = new Point(0, 0);
            tlpProducts.Name = "tlpProducts";
            tlpProducts.RowCount = 8;
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpProducts.Size = new Size(935, 460);
            tlpProducts.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(381, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(169, 29);
            lblTitle.TabIndex = 0;
            lblTitle.Values.Text = "Agregar Producto";
            // 
            // txtCode
            // 
            txtCode.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtCode, 2);
            txtCode.CueHint.CueHintText = "Código de Barras";
            txtCode.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCode.CueHint.Padding = new Padding(0);
            txtCode.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtCode.Location = new Point(18, 72);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(429, 27);
            txtCode.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCode.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtCode.StateCommon.Border.Width = 2;
            txtCode.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCode.TabIndex = 1;
            txtCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtName, 2);
            txtName.CueHint.CueHintText = "Nombre del Producto";
            txtName.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.CueHint.Padding = new Padding(0);
            txtName.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtName.Location = new Point(486, 72);
            txtName.Name = "txtName";
            txtName.Size = new Size(429, 27);
            txtName.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtName.StateCommon.Border.Width = 2;
            txtName.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtName.TabIndex = 2;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtStock
            // 
            txtStock.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtStock, 2);
            txtStock.CueHint.CueHintText = "Stock Inicial";
            txtStock.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtStock.CueHint.Padding = new Padding(0);
            txtStock.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtStock.Location = new Point(170, 129);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(126, 27);
            txtStock.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtStock.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtStock.StateCommon.Border.Width = 2;
            txtStock.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtStock.TabIndex = 3;
            txtStock.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCostPrice
            // 
            txtCostPrice.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtCostPrice, 2);
            txtCostPrice.CueHint.CueHintText = "Precio Costo";
            txtCostPrice.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCostPrice.CueHint.Padding = new Padding(0);
            txtCostPrice.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtCostPrice.Location = new Point(486, 129);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(429, 27);
            txtCostPrice.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCostPrice.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtCostPrice.StateCommon.Border.Width = 2;
            txtCostPrice.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtCostPrice.TabIndex = 4;
            txtCostPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // txtGainPercent
            // 
            txtGainPercent.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtGainPercent, 2);
            txtGainPercent.CueHint.CueHintText = "%";
            txtGainPercent.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtGainPercent.CueHint.Padding = new Padding(0);
            txtGainPercent.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtGainPercent.Location = new Point(653, 186);
            txtGainPercent.Name = "txtGainPercent";
            txtGainPercent.Size = new Size(95, 27);
            txtGainPercent.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtGainPercent.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtGainPercent.StateCommon.Border.Width = 2;
            txtGainPercent.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtGainPercent.TabIndex = 5;
            txtGainPercent.TextAlign = HorizontalAlignment.Center;
            txtGainPercent.TextChanged += txtGainPercent_TextChanged;
            // 
            // txtPrice
            // 
            txtPrice.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtPrice, 2);
            txtPrice.CueHint.CueHintText = "Precio Venta";
            txtPrice.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPrice.CueHint.Padding = new Padding(0);
            txtPrice.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtPrice.Location = new Point(589, 243);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(223, 27);
            txtPrice.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPrice.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtPrice.StateCommon.Border.Width = 2;
            txtPrice.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtPrice.TabIndex = 6;
            txtPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(286, 350);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 8;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(519, 350);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 7;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // FormProductEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(229, 229, 229);
            CancelButton = btnCancel;
            ClientSize = new Size(935, 460);
            Controls.Add(tlpProducts);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormProductEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Producto";
            tlpProducts.ResumeLayout(false);
            tlpProducts.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpProducts;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonTextBox txtCode;
        private Krypton.Toolkit.KryptonTextBox txtStock;
        private Krypton.Toolkit.KryptonTextBox txtCostPrice;
        private Krypton.Toolkit.KryptonTextBox txtPrice;
        private Krypton.Toolkit.KryptonTextBox txtGainPercent;
    }
}