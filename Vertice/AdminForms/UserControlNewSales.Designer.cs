namespace WinForms
{
    partial class UserControlNewSales
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tlpSales = new TableLayoutPanel();
            dgvSaleProducts = new DataGridView();
            lblSubTotal = new Krypton.Toolkit.KryptonLabel();
            lblTotal = new Krypton.Toolkit.KryptonLabel();
            lblChange = new Krypton.Toolkit.KryptonLabel();
            btnShowLastSale = new Krypton.Toolkit.KryptonButton();
            txtProduct = new Krypton.Toolkit.KryptonTextBox();
            lblLastProduct = new Krypton.Toolkit.KryptonLabel();
            cbxPaymentMethod = new Krypton.Toolkit.KryptonComboBox();
            txtCash = new Krypton.Toolkit.KryptonTextBox();
            txtVariosAmount = new Krypton.Toolkit.KryptonTextBox();
            btnAddVarious = new Krypton.Toolkit.KryptonButton();
            btnReset = new Krypton.Toolkit.KryptonButton();
            btnSaleTicket = new Krypton.Toolkit.KryptonButton();
            btnOnlySale = new Krypton.Toolkit.KryptonButton();
            btnSaleAFIP = new Krypton.Toolkit.KryptonButton();
            flpSaleTabs = new FlowLayoutPanel();
            tlpSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSaleProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbxPaymentMethod).BeginInit();
            SuspendLayout();
            // 
            // tlpSales
            // 
            tlpSales.ColumnCount = 8;
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.71875F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.28125F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.046875F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.9375F));
            tlpSales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.515625F));
            tlpSales.Controls.Add(dgvSaleProducts, 1, 3);
            tlpSales.Controls.Add(lblSubTotal, 5, 8);
            tlpSales.Controls.Add(lblTotal, 5, 2);
            tlpSales.Controls.Add(lblChange, 6, 8);
            tlpSales.Controls.Add(btnShowLastSale, 1, 12);
            tlpSales.Controls.Add(txtProduct, 1, 1);
            tlpSales.Controls.Add(lblLastProduct, 1, 2);
            tlpSales.Controls.Add(cbxPaymentMethod, 5, 4);
            tlpSales.Controls.Add(txtCash, 5, 5);
            tlpSales.Controls.Add(txtVariosAmount, 5, 6);
            tlpSales.Controls.Add(btnAddVarious, 6, 6);
            tlpSales.Controls.Add(btnReset, 3, 12);
            tlpSales.Controls.Add(btnSaleTicket, 3, 11);
            tlpSales.Controls.Add(btnOnlySale, 3, 10);
            tlpSales.Controls.Add(btnSaleAFIP, 5, 10);
            tlpSales.Dock = DockStyle.Fill;
            tlpSales.Location = new Point(0, 44);
            tlpSales.Name = "tlpSales";
            tlpSales.RowCount = 14;
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 1.241691F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.66639F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 9.731494F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.989902F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.41447163F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 5.715322F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 7.450147F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 6.82930231F));
            tlpSales.RowStyles.Add(new RowStyle(SizeType.Percent, 1.01176643F));
            tlpSales.Size = new Size(1280, 611);
            tlpSales.TabIndex = 0;
            // 
            // dgvSaleProducts
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvSaleProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSaleProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSaleProducts.BorderStyle = BorderStyle.None;
            dgvSaleProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSaleProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSaleProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpSales.SetColumnSpan(dgvSaleProducts, 4);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSaleProducts.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSaleProducts.Dock = DockStyle.Fill;
            dgvSaleProducts.EnableHeadersVisualStyles = false;
            dgvSaleProducts.Location = new Point(25, 146);
            dgvSaleProducts.MultiSelect = false;
            dgvSaleProducts.Name = "dgvSaleProducts";
            dgvSaleProducts.ReadOnly = true;
            dgvSaleProducts.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvSaleProducts.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpSales.SetRowSpan(dgvSaleProducts, 7);
            dgvSaleProducts.RowTemplate.Height = 35;
            dgvSaleProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaleProducts.Size = new Size(772, 327);
            dgvSaleProducts.TabIndex = 23;
            dgvSaleProducts.CellBeginEdit += dgvSaleProducts_CellBeginEdit;
            dgvSaleProducts.CellClick += dgvSaleProducts_CellClick;
            dgvSaleProducts.CellContentClick += dgvSaleProducts_CellContentClick;
            dgvSaleProducts.CellEndEdit += dgvSaleProducts_CellEndEdit;
            dgvSaleProducts.CellFormatting += dgvSaleProducts_CellFormatting;
            dgvSaleProducts.CellParsing += dgvSaleProducts_CellParsing;
            dgvSaleProducts.EditingControlShowing += dgvSaleProducts_EditingControlShowing;
            dgvSaleProducts.KeyDown += dgvSaleProducts_KeyDown;
            // 
            // lblSubTotal
            // 
            lblSubTotal.Anchor = AnchorStyles.Top;
            lblSubTotal.Location = new Point(841, 386);
            lblSubTotal.Name = "lblSubTotal";
            tlpSales.SetRowSpan(lblSubTotal, 2);
            lblSubTotal.Size = new Size(149, 33);
            lblSubTotal.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubTotal.TabIndex = 13;
            lblSubTotal.Values.Text = "SUBTOTAL";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top;
            tlpSales.SetColumnSpan(lblTotal, 2);
            lblTotal.Location = new Point(863, 87);
            lblTotal.Name = "lblTotal";
            tlpSales.SetRowSpan(lblTotal, 2);
            lblTotal.Size = new Size(308, 101);
            lblTotal.StateCommon.ShortText.Color1 = Color.Green;
            lblTotal.StateCommon.ShortText.Font = new Font("Arial", 60F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.TabIndex = 12;
            lblTotal.Values.Text = "TOTAL";
            // 
            // lblChange
            // 
            lblChange.Anchor = AnchorStyles.Top;
            lblChange.Location = new Point(1063, 386);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(139, 39);
            lblChange.StateCommon.ShortText.Color1 = Color.Red;
            lblChange.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblChange.TabIndex = 15;
            lblChange.Values.Text = "VUELTO";
            // 
            // btnShowLastSale
            // 
            btnShowLastSale.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnShowLastSale.Location = new Point(25, 558);
            btnShowLastSale.Name = "btnShowLastSale";
            btnShowLastSale.Size = new Size(292, 35);
            btnShowLastSale.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnShowLastSale.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShowLastSale.TabIndex = 41;
            btnShowLastSale.Values.DropDownArrowColor = Color.Empty;
            btnShowLastSale.Values.Text = "MOSTRAR ÚLTIMA VENTA";
            btnShowLastSale.Click += btnShowLastSale_Click;
            // 
            // txtProduct
            // 
            tlpSales.SetColumnSpan(txtProduct, 4);
            txtProduct.CueHint.CueHintText = "PRODUCTO";
            txtProduct.CueHint.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold);
            txtProduct.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtProduct.Dock = DockStyle.Fill;
            txtProduct.Location = new Point(25, 10);
            txtProduct.Name = "txtProduct";
            txtProduct.Size = new Size(772, 78);
            txtProduct.StateActive.Content.Font = new Font("Microsoft Sans Serif", 50F, FontStyle.Bold);
            txtProduct.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtProduct.StateCommon.Border.Width = 2;
            txtProduct.StateCommon.Content.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtProduct.StateCommon.Content.Padding = new Padding(1, 1, 1, -3);
            txtProduct.StateNormal.Content.Font = new Font("Arial", 36F, FontStyle.Bold);
            txtProduct.TabIndex = 0;
            txtProduct.TextAlign = HorizontalAlignment.Center;
            txtProduct.KeyDown += txtProduct_KeyDown;
            // 
            // lblLastProduct
            // 
            lblLastProduct.Anchor = AnchorStyles.None;
            tlpSales.SetColumnSpan(lblLastProduct, 4);
            lblLastProduct.Location = new Point(408, 111);
            lblLastProduct.Name = "lblLastProduct";
            lblLastProduct.Size = new Size(6, 5);
            lblLastProduct.StateCommon.Padding = new Padding(-1, 4, -1, 1);
            lblLastProduct.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 50F, FontStyle.Bold);
            lblLastProduct.TabIndex = 43;
            lblLastProduct.Values.Text = "";
            // 
            // cbxPaymentMethod
            // 
            cbxPaymentMethod.Anchor = AnchorStyles.None;
            tlpSales.SetColumnSpan(cbxPaymentMethod, 2);
            cbxPaymentMethod.DropDownWidth = 152;
            cbxPaymentMethod.Location = new Point(935, 204);
            cbxPaymentMethod.Name = "cbxPaymentMethod";
            cbxPaymentMethod.Size = new Size(164, 22);
            cbxPaymentMethod.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxPaymentMethod.TabIndex = 1;
            cbxPaymentMethod.Text = "Método de pago";
            cbxPaymentMethod.SelectedIndexChanged += cbxPaymentMethod_SelectedIndexChanged;
            // 
            // txtCash
            // 
            txtCash.Anchor = AnchorStyles.None;
            tlpSales.SetColumnSpan(txtCash, 2);
            txtCash.CueHint.CueHintText = "INGRESAR EFECTIVO";
            txtCash.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtCash.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtCash.Location = new Point(887, 242);
            txtCash.Name = "txtCash";
            txtCash.Size = new Size(260, 47);
            txtCash.StateActive.Content.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold);
            txtCash.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtCash.StateCommon.Border.Width = 2;
            txtCash.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtCash.StateCommon.Content.Padding = new Padding(1, 10, 1, 10);
            txtCash.TabIndex = 2;
            txtCash.TextAlign = HorizontalAlignment.Center;
            txtCash.TextChanged += txtCash_TextChanged;
            // 
            // txtVariosAmount
            // 
            txtVariosAmount.Anchor = AnchorStyles.Right;
            txtVariosAmount.CueHint.CueHintText = "INGRESAR PRECIO";
            txtVariosAmount.CueHint.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtVariosAmount.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtVariosAmount.Location = new Point(839, 290);
            txtVariosAmount.Name = "txtVariosAmount";
            txtVariosAmount.Size = new Size(189, 47);
            txtVariosAmount.StateActive.Content.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold);
            txtVariosAmount.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtVariosAmount.StateCommon.Border.Width = 2;
            txtVariosAmount.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.75F, FontStyle.Bold);
            txtVariosAmount.StateCommon.Content.Padding = new Padding(1, 10, 1, 10);
            txtVariosAmount.TabIndex = 3;
            txtVariosAmount.TextAlign = HorizontalAlignment.Center;
            txtVariosAmount.KeyDown += txtVariosAmount_KeyDown;
            // 
            // btnAddVarious
            // 
            btnAddVarious.Anchor = AnchorStyles.Left;
            btnAddVarious.Location = new Point(1034, 295);
            btnAddVarious.Name = "btnAddVarious";
            btnAddVarious.Size = new Size(113, 31);
            btnAddVarious.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAddVarious.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddVarious.TabIndex = 45;
            btnAddVarious.Values.DropDownArrowColor = Color.Empty;
            btnAddVarious.Values.Text = "AGREGAR";
            btnAddVarious.Click += btnAddVarious_Click;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlpSales.SetColumnSpan(btnReset, 2);
            btnReset.Location = new Point(483, 558);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(314, 35);
            btnReset.StateCommon.Back.Color1 = Color.FromArgb(255, 128, 128);
            btnReset.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.TabIndex = 7;
            btnReset.Values.DropDownArrowColor = Color.Empty;
            btnReset.Values.Text = "CANCELAR VENTA";
            btnReset.Click += btnReset_Click;
            // 
            // btnSaleTicket
            // 
            btnSaleTicket.Anchor = AnchorStyles.None;
            tlpSales.SetColumnSpan(btnSaleTicket, 2);
            btnSaleTicket.Location = new Point(560, 513);
            btnSaleTicket.Name = "btnSaleTicket";
            btnSaleTicket.Size = new Size(160, 39);
            btnSaleTicket.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSaleTicket.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaleTicket.TabIndex = 5;
            btnSaleTicket.Values.DropDownArrowColor = Color.Empty;
            btnSaleTicket.Values.Text = "VENTA + TICKET";
            btnSaleTicket.Visible = false;
            btnSaleTicket.Click += btnSaleTicket_Click;
            // 
            // btnOnlySale
            // 
            btnOnlySale.Anchor = AnchorStyles.None;
            tlpSales.SetColumnSpan(btnOnlySale, 2);
            btnOnlySale.Location = new Point(560, 479);
            btnOnlySale.Name = "btnOnlySale";
            btnOnlySale.Size = new Size(160, 28);
            btnOnlySale.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnOnlySale.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOnlySale.TabIndex = 4;
            btnOnlySale.Values.DropDownArrowColor = Color.Empty;
            btnOnlySale.Values.Text = "SOLO VENTA";
            btnOnlySale.Visible = false;
            btnOnlySale.Click += btnOnlySale_Click;
            // 
            // btnSaleAFIP
            // 
            btnSaleAFIP.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tlpSales.SetColumnSpan(btnSaleAFIP, 2);
            btnSaleAFIP.Location = new Point(853, 479);
            btnSaleAFIP.Name = "btnSaleAFIP";
            tlpSales.SetRowSpan(btnSaleAFIP, 2);
            btnSaleAFIP.Size = new Size(329, 73);
            btnSaleAFIP.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSaleAFIP.StateCommon.Content.LongText.Color1 = Color.Black;
            btnSaleAFIP.StateCommon.Content.ShortText.Color1 = Color.Black;
            btnSaleAFIP.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaleAFIP.StateNormal.Back.Color1 = Color.Lime;
            btnSaleAFIP.TabIndex = 6;
            btnSaleAFIP.Values.DropDownArrowColor = Color.Empty;
            btnSaleAFIP.Values.Text = "REALIZAR VENTA";
            btnSaleAFIP.Click += btnSaleAFIP_Click;
            // 
            // flpSaleTabs
            // 
            flpSaleTabs.BackColor = Color.FromArgb(238, 239, 249);
            flpSaleTabs.Dock = DockStyle.Top;
            flpSaleTabs.Location = new Point(0, 0);
            flpSaleTabs.Name = "flpSaleTabs";
            flpSaleTabs.Padding = new Padding(6, 5, 6, 5);
            flpSaleTabs.Size = new Size(1280, 44);
            flpSaleTabs.TabIndex = 8;
            flpSaleTabs.WrapContents = false;
            // 
            // UserControlNewSales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpSales);
            Controls.Add(flpSaleTabs);
            Name = "UserControlNewSales";
            Size = new Size(1280, 655);
            Load += UserControlNewSales_Load;
            tlpSales.ResumeLayout(false);
            tlpSales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSaleProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbxPaymentMethod).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpSales;
        private FlowLayoutPanel flpSaleTabs;
        private Krypton.Toolkit.KryptonComboBox cbxPaymentMethod;
        private Krypton.Toolkit.KryptonLabel lblSubTotal;
        private Krypton.Toolkit.KryptonLabel lblTotal;
        private Krypton.Toolkit.KryptonLabel lblChange;
        private Krypton.Toolkit.KryptonButton btnSale;
        private Krypton.Toolkit.KryptonButton btnSaleTicket;
        private Krypton.Toolkit.KryptonButton btnOnlySale;
        private Krypton.Toolkit.KryptonButton btnSaleAFIP;
        private Krypton.Toolkit.KryptonButton btnShowLastSale;
        private DataGridView dgvSaleProducts;
        private Krypton.Toolkit.KryptonTextBox txtCash;
        private Krypton.Toolkit.KryptonTextBox txtProduct;
        private Krypton.Toolkit.KryptonLabel lblLastProduct;
        private Krypton.Toolkit.KryptonTextBox txtVariosAmount;
        private Krypton.Toolkit.KryptonButton btnAddVarious;
        private Krypton.Toolkit.KryptonButton btnReset;
    }
}
