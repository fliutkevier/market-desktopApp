namespace WinForms.PopUps
{
    partial class FormSaleDetail
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tlpSaleDetail = new TableLayoutPanel();
            lblSaleUser = new Krypton.Toolkit.KryptonLabel();
            lblPaymentMethod = new Krypton.Toolkit.KryptonLabel();
            lblSaleDate = new Krypton.Toolkit.KryptonLabel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            lblSaleNumber = new Krypton.Toolkit.KryptonLabel();
            lblTotal = new Krypton.Toolkit.KryptonLabel();
            dgvProducts = new DataGridView();
            btnPrintTicket = new Krypton.Toolkit.KryptonButton();
            btnClose = new Krypton.Toolkit.KryptonButton();
            chkAFIP = new Krypton.Toolkit.KryptonCheckBox();
            tlpSaleDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // tlpSaleDetail
            // 
            tlpSaleDetail.ColumnCount = 4;
            tlpSaleDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSaleDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSaleDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSaleDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSaleDetail.Controls.Add(lblSaleUser, 2, 2);
            tlpSaleDetail.Controls.Add(lblPaymentMethod, 0, 2);
            tlpSaleDetail.Controls.Add(lblSaleDate, 2, 1);
            tlpSaleDetail.Controls.Add(lblTitle, 1, 0);
            tlpSaleDetail.Controls.Add(lblSaleNumber, 0, 1);
            tlpSaleDetail.Controls.Add(lblTotal, 0, 8);
            tlpSaleDetail.Controls.Add(dgvProducts, 0, 4);
            tlpSaleDetail.Controls.Add(btnPrintTicket, 2, 8);
            tlpSaleDetail.Controls.Add(btnClose, 1, 9);
            tlpSaleDetail.Controls.Add(chkAFIP, 0, 3);
            tlpSaleDetail.Dock = DockStyle.Fill;
            tlpSaleDetail.Location = new Point(0, 0);
            tlpSaleDetail.Name = "tlpSaleDetail";
            tlpSaleDetail.RowCount = 10;
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 12.8571424F));
            tlpSaleDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 7.142857F));
            tlpSaleDetail.Size = new Size(600, 700);
            tlpSaleDetail.TabIndex = 0;
            // 
            // lblSaleUser
            // 
            lblSaleUser.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(lblSaleUser, 2);
            lblSaleUser.Location = new Point(411, 163);
            lblSaleUser.Name = "lblSaleUser";
            lblSaleUser.Size = new Size(78, 23);
            lblSaleUser.StateCommon.Padding = new Padding(-1, -1, 5, -1);
            lblSaleUser.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblSaleUser.TabIndex = 12;
            lblSaleUser.Values.Text = "Usuario:";
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(lblPaymentMethod, 2);
            lblPaymentMethod.Location = new Point(116, 163);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new Size(68, 23);
            lblPaymentMethod.StateCommon.Padding = new Padding(-1, -1, 5, -1);
            lblPaymentMethod.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblPaymentMethod.TabIndex = 11;
            lblPaymentMethod.Values.Text = "Forma:";
            // 
            // lblSaleDate
            // 
            lblSaleDate.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(lblSaleDate, 2);
            lblSaleDate.Location = new Point(417, 93);
            lblSaleDate.Name = "lblSaleDate";
            lblSaleDate.Size = new Size(66, 23);
            lblSaleDate.StateCommon.Padding = new Padding(-1, -1, 5, -1);
            lblSaleDate.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblSaleDate.TabIndex = 10;
            lblSaleDate.Values.Text = "Fecha: ";
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(263, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(73, 29);
            lblTitle.TabIndex = 8;
            lblTitle.Values.Text = "VENTA";
            // 
            // lblSaleNumber
            // 
            lblSaleNumber.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(lblSaleNumber, 2);
            lblSaleNumber.Location = new Point(102, 93);
            lblSaleNumber.Name = "lblSaleNumber";
            lblSaleNumber.Size = new Size(95, 23);
            lblSaleNumber.StateCommon.Padding = new Padding(-1, -1, 5, -1);
            lblSaleNumber.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblSaleNumber.TabIndex = 9;
            lblSaleNumber.Values.Text = "Nro Venta: ";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.None;
            lblTotal.Location = new Point(46, 593);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(57, 23);
            lblTotal.StateCommon.Padding = new Padding(-1, -1, 5, -1);
            lblTotal.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTotal.TabIndex = 13;
            lblTotal.Values.Text = "Total:";
            // 
            // dgvProducts
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpSaleDetail.SetColumnSpan(dgvProducts, 4);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvProducts.DefaultCellStyle = dataGridViewCellStyle3;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.Location = new Point(3, 283);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProducts.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpSaleDetail.SetRowSpan(dgvProducts, 4);
            dgvProducts.RowTemplate.Height = 35;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(594, 274);
            dgvProducts.TabIndex = 19;
            // 
            // btnPrintTicket
            // 
            btnPrintTicket.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(btnPrintTicket, 2);
            btnPrintTicket.Location = new Point(354, 590);
            btnPrintTicket.Name = "btnPrintTicket";
            btnPrintTicket.Size = new Size(192, 30);
            btnPrintTicket.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnPrintTicket.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPrintTicket.TabIndex = 40;
            btnPrintTicket.Values.DropDownArrowColor = Color.Empty;
            btnPrintTicket.Values.Text = "IMPRIMIR TICKET";
            btnPrintTicket.Click += btnPrintTicket_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(btnClose, 2);
            btnClose.Location = new Point(234, 660);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(132, 30);
            btnClose.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnClose.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.TabIndex = 41;
            btnClose.Values.DropDownArrowColor = Color.Empty;
            btnClose.Values.Text = "CERRAR";
            btnClose.Click += btnClose_Click;
            // 
            // chkAFIP
            // 
            chkAFIP.Anchor = AnchorStyles.None;
            tlpSaleDetail.SetColumnSpan(chkAFIP, 2);
            chkAFIP.Enabled = false;
            chkAFIP.Location = new Point(99, 233);
            chkAFIP.Name = "chkAFIP";
            chkAFIP.Size = new Size(102, 23);
            chkAFIP.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            chkAFIP.TabIndex = 42;
            chkAFIP.Values.Text = "Facturado";
            // 
            // FormSaleDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 700);
            Controls.Add(tlpSaleDetail);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormSaleDetail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Detalle";
            Load += FormSaleDetail_Load;
            tlpSaleDetail.ResumeLayout(false);
            tlpSaleDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpSaleDetail;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonLabel lblSaleDate;
        private Krypton.Toolkit.KryptonLabel lblSaleNumber;
        private Krypton.Toolkit.KryptonLabel lblSaleUser;
        private Krypton.Toolkit.KryptonLabel lblPaymentMethod;
        private Krypton.Toolkit.KryptonLabel lblTotal;
        private DataGridView dgvProducts;
        private Krypton.Toolkit.KryptonButton btnPrintTicket;
        private Krypton.Toolkit.KryptonButton btnClose;
        private Krypton.Toolkit.KryptonCheckBox chkAFIP;
    }
}