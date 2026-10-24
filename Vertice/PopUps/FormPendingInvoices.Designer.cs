namespace WinForms.PopUps
{
    partial class FormPendingInvoices
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
            tlpPendingInvoices = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            lblCount = new Krypton.Toolkit.KryptonLabel();
            dgvPending = new DataGridView();
            btnProcessNow = new Krypton.Toolkit.KryptonButton();
            btnClose = new Krypton.Toolkit.KryptonButton();
            tlpPendingInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            SuspendLayout();
            //
            // tlpPendingInvoices
            //
            tlpPendingInvoices.ColumnCount = 4;
            tlpPendingInvoices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tlpPendingInvoices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpPendingInvoices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpPendingInvoices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tlpPendingInvoices.Controls.Add(lblTitle, 1, 0);
            tlpPendingInvoices.Controls.Add(lblCount, 1, 1);
            tlpPendingInvoices.Controls.Add(dgvPending, 1, 2);
            tlpPendingInvoices.Controls.Add(btnProcessNow, 1, 3);
            tlpPendingInvoices.Controls.Add(btnClose, 2, 3);
            tlpPendingInvoices.Dock = DockStyle.Fill;
            tlpPendingInvoices.Location = new Point(0, 0);
            tlpPendingInvoices.Name = "tlpPendingInvoices";
            tlpPendingInvoices.RowCount = 4;
            tlpPendingInvoices.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            tlpPendingInvoices.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tlpPendingInvoices.RowStyles.Add(new RowStyle(SizeType.Percent, 76F));
            tlpPendingInvoices.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpPendingInvoices.Size = new Size(1000, 600);
            tlpPendingInvoices.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.None;
            tlpPendingInvoices.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(367, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(266, 24);
            lblTitle.TabIndex = 0;
            lblTitle.Values.Text = "Facturación Pendiente (ARCA)";
            //
            // lblCount
            //
            lblCount.Anchor = AnchorStyles.None;
            tlpPendingInvoices.SetColumnSpan(lblCount, 2);
            lblCount.Location = new Point(400, 54);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(200, 20);
            lblCount.TabIndex = 1;
            lblCount.Values.Text = "0 factura(s) pendiente(s) de ARCA";
            //
            // dgvPending
            //
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvPending.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPending.BorderStyle = BorderStyle.None;
            dgvPending.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpPendingInvoices.SetColumnSpan(dgvPending, 2);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvPending.DefaultCellStyle = dataGridViewCellStyle3;
            dgvPending.Dock = DockStyle.Fill;
            dgvPending.EnableHeadersVisualStyles = false;
            dgvPending.Location = new Point(53, 82);
            dgvPending.MultiSelect = false;
            dgvPending.Name = "dgvPending";
            dgvPending.ReadOnly = true;
            dgvPending.RowHeadersVisible = false;
            dgvPending.RowTemplate.Height = 30;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.Size = new Size(894, 452);
            dgvPending.TabIndex = 2;
            //
            // btnProcessNow
            //
            btnProcessNow.Anchor = AnchorStyles.None;
            btnProcessNow.Location = new Point(313, 546);
            btnProcessNow.Name = "btnProcessNow";
            btnProcessNow.Size = new Size(220, 40);
            btnProcessNow.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnProcessNow.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnProcessNow.TabIndex = 3;
            btnProcessNow.Values.DropDownArrowColor = Color.Empty;
            btnProcessNow.Values.Text = "Facturar pendientes ahora";
            btnProcessNow.Click += btnProcessNow_Click;
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.None;
            btnClose.Location = new Point(600, 546);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(127, 40);
            btnClose.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnClose.TabIndex = 4;
            btnClose.Values.DropDownArrowColor = Color.Empty;
            btnClose.Values.Text = "Cerrar";
            btnClose.Click += btnClose_Click;
            //
            // FormPendingInvoices
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            CancelButton = btnClose;
            ClientSize = new Size(1000, 600);
            Controls.Add(tlpPendingInvoices);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormPendingInvoices";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormPendingInvoices";
            tlpPendingInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpPendingInvoices;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonLabel lblCount;
        private DataGridView dgvPending;
        private Krypton.Toolkit.KryptonButton btnProcessNow;
        private Krypton.Toolkit.KryptonButton btnClose;
    }
}
