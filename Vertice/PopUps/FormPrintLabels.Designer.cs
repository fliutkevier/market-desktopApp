namespace WinForms.PopUps
{
    partial class FormPrintLabels
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
            tlpPrintProducts = new TableLayoutPanel();
            dgvPrintProducts = new DataGridView();
            btnPrint = new Krypton.Toolkit.KryptonButton();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            btnClean = new Krypton.Toolkit.KryptonButton();
            btnClose = new Krypton.Toolkit.KryptonButton();
            btnToUpper = new Krypton.Toolkit.KryptonButton();
            btnAllToUpper = new Krypton.Toolkit.KryptonButton();
            tlpPrintProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrintProducts).BeginInit();
            SuspendLayout();
            // 
            // tlpPrintProducts
            // 
            tlpPrintProducts.ColumnCount = 6;
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.978945F));
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.5634327F));
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.2388058F));
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.1455231F));
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9776115F));
            tlpPrintProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.978945F));
            tlpPrintProducts.Controls.Add(dgvPrintProducts, 1, 2);
            tlpPrintProducts.Controls.Add(btnPrint, 4, 1);
            tlpPrintProducts.Controls.Add(lblTitle, 2, 0);
            tlpPrintProducts.Controls.Add(btnClean, 1, 1);
            tlpPrintProducts.Controls.Add(btnClose, 4, 5);
            tlpPrintProducts.Controls.Add(btnToUpper, 1, 5);
            tlpPrintProducts.Controls.Add(btnAllToUpper, 3, 5);
            tlpPrintProducts.Dock = DockStyle.Fill;
            tlpPrintProducts.Location = new Point(0, 0);
            tlpPrintProducts.Name = "tlpPrintProducts";
            tlpPrintProducts.RowCount = 6;
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 22.7027035F));
            tlpPrintProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 10.9909906F));
            tlpPrintProducts.Size = new Size(1072, 555);
            tlpPrintProducts.TabIndex = 0;
            // 
            // dgvPrintProducts
            // 
            dgvPrintProducts.AllowUserToAddRows = false;
            dgvPrintProducts.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvPrintProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvPrintProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrintProducts.BorderStyle = BorderStyle.None;
            dgvPrintProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvPrintProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvPrintProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpPrintProducts.SetColumnSpan(dgvPrintProducts, 4);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvPrintProducts.DefaultCellStyle = dataGridViewCellStyle3;
            dgvPrintProducts.Dock = DockStyle.Fill;
            dgvPrintProducts.EnableHeadersVisualStyles = false;
            dgvPrintProducts.Location = new Point(56, 187);
            dgvPrintProducts.MultiSelect = false;
            dgvPrintProducts.Name = "dgvPrintProducts";
            dgvPrintProducts.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvPrintProducts.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpPrintProducts.SetRowSpan(dgvPrintProducts, 3);
            dgvPrintProducts.RowTemplate.Height = 35;
            dgvPrintProducts.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvPrintProducts.Size = new Size(958, 303);
            dgvPrintProducts.TabIndex = 30;
            dgvPrintProducts.KeyDown += dgvPrintProducts_KeyDown;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.None;
            btnPrint.Location = new Point(858, 118);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(136, 40);
            btnPrint.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnPrint.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrint.TabIndex = 31;
            btnPrint.Values.DropDownArrowColor = Color.Empty;
            btnPrint.Values.Text = "IMPRIMIR";
            btnPrint.Click += btnPrint_Click;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpPrintProducts.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(441, 31);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(205, 29);
            lblTitle.TabIndex = 34;
            lblTitle.Values.Text = "IMPRIMIR ETIQUETAS";
            // 
            // btnClean
            // 
            btnClean.Anchor = AnchorStyles.None;
            btnClean.Location = new Point(84, 118);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(136, 40);
            btnClean.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnClean.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClean.TabIndex = 32;
            btnClean.Values.DropDownArrowColor = Color.Empty;
            btnClean.Values.Text = "LIMPIAR";
            btnClean.Click += btnClean_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.None;
            btnClose.Location = new Point(858, 504);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(136, 40);
            btnClose.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnClose.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.TabIndex = 33;
            btnClose.Values.DropDownArrowColor = Color.Empty;
            btnClose.Values.Text = "CERRAR";
            btnClose.Click += btnClose_Click;
            // 
            // btnToUpper
            // 
            btnToUpper.Anchor = AnchorStyles.None;
            tlpPrintProducts.SetColumnSpan(btnToUpper, 2);
            btnToUpper.Location = new Point(167, 504);
            btnToUpper.Name = "btnToUpper";
            btnToUpper.Size = new Size(262, 40);
            btnToUpper.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnToUpper.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnToUpper.TabIndex = 35;
            btnToUpper.Values.DropDownArrowColor = Color.Empty;
            btnToUpper.Values.Text = "PASAR NOMBRE A MAYUS";
            btnToUpper.Click += btnToUpper_Click;
            //
            // btnAllToUpper
            //
            btnAllToUpper.Anchor = AnchorStyles.None;
            btnAllToUpper.Location = new Point(589, 504);
            btnAllToUpper.Name = "btnAllToUpper";
            btnAllToUpper.Size = new Size(262, 40);
            btnAllToUpper.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAllToUpper.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAllToUpper.TabIndex = 36;
            btnAllToUpper.Values.DropDownArrowColor = Color.Empty;
            btnAllToUpper.Values.Text = "PASAR TODOS A MAYUS";
            btnAllToUpper.Click += btnAllToUpper_Click;
            //
            // FormPrintLabels
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(1072, 555);
            Controls.Add(tlpPrintProducts);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "FormPrintLabels";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Imprimir";
            Load += FormPrintLabels_Load;
            tlpPrintProducts.ResumeLayout(false);
            tlpPrintProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrintProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpPrintProducts;
        private DataGridView dgvPrintProducts;
        private Krypton.Toolkit.KryptonButton btnPrint;
        private Krypton.Toolkit.KryptonButton btnClean;
        private Krypton.Toolkit.KryptonButton btnClose;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnToUpper;
        private Krypton.Toolkit.KryptonButton btnAllToUpper;
    }
}