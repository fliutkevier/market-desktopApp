namespace WinForms.AdminForms
{
    partial class UserControlPaymentMethods
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
            tlpPaymentMethods = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            dgvPaymentMethods = new DataGridView();
            btnUpdate = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            tlpPaymentMethods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentMethods).BeginInit();
            SuspendLayout();
            // 
            // tlpPaymentMethods
            // 
            tlpPaymentMethods.ColumnCount = 9;
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.9375F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.78125F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.5625F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.6875F));
            tlpPaymentMethods.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.359375F));
            tlpPaymentMethods.Controls.Add(lblTitle, 1, 0);
            tlpPaymentMethods.Controls.Add(txtFilter, 1, 1);
            tlpPaymentMethods.Controls.Add(dgvPaymentMethods, 1, 2);
            tlpPaymentMethods.Controls.Add(btnAdd, 5, 3);
            tlpPaymentMethods.Controls.Add(btnDelete, 5, 5);
            tlpPaymentMethods.Controls.Add(btnUpdate, 5, 7);
            tlpPaymentMethods.Dock = DockStyle.Fill;
            tlpPaymentMethods.Location = new Point(0, 0);
            tlpPaymentMethods.Name = "tlpPaymentMethods";
            tlpPaymentMethods.RowCount = 12;
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3511448F));
            tlpPaymentMethods.RowStyles.Add(new RowStyle(SizeType.Percent, 3.206107F));
            tlpPaymentMethods.Size = new Size(1280, 655);
            tlpPaymentMethods.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpPaymentMethods.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(133, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(166, 29);
            lblTitle.TabIndex = 10;
            lblTitle.Values.Text = "Métodos de Pago";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpPaymentMethods.SetColumnSpan(txtFilter, 2);
            txtFilter.CueHint.CueHintText = "Buscar";
            txtFilter.Location = new Point(44, 69);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(343, 23);
            txtFilter.TabIndex = 11;
            // 
            // dgvPaymentMethods
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvPaymentMethods.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvPaymentMethods.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPaymentMethods.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPaymentMethods.BorderStyle = BorderStyle.None;
            dgvPaymentMethods.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvPaymentMethods.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvPaymentMethods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpPaymentMethods.SetColumnSpan(dgvPaymentMethods, 4);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvPaymentMethods.DefaultCellStyle = dataGridViewCellStyle3;
            dgvPaymentMethods.EnableHeadersVisualStyles = false;
            dgvPaymentMethods.Location = new Point(14, 111);
            dgvPaymentMethods.MultiSelect = false;
            dgvPaymentMethods.Name = "dgvPaymentMethods";
            dgvPaymentMethods.ReadOnly = true;
            dgvPaymentMethods.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvPaymentMethods.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpPaymentMethods.SetRowSpan(dgvPaymentMethods, 9);
            dgvPaymentMethods.RowTemplate.Height = 35;
            dgvPaymentMethods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaymentMethods.Size = new Size(686, 519);
            dgvPaymentMethods.TabIndex = 9;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.None;
            tlpPaymentMethods.SetColumnSpan(btnUpdate, 2);
            btnUpdate.Location = new Point(780, 385);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(127, 40);
            btnUpdate.TabIndex = 30;
            btnUpdate.Values.DropDownArrowColor = Color.Empty;
            btnUpdate.Values.Text = "Editar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            tlpPaymentMethods.SetColumnSpan(btnAdd, 2);
            btnAdd.Location = new Point(780, 169);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.TabIndex = 29;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            tlpPaymentMethods.SetColumnSpan(btnDelete, 2);
            btnDelete.Location = new Point(782, 277);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(123, 40);
            btnDelete.TabIndex = 28;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Borrar";
            btnDelete.Click += btnDelete_Click;
            // 
            // UserControlPaymentMethods
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpPaymentMethods);
            Name = "UserControlPaymentMethods";
            Size = new Size(1280, 655);
            Load += UserControlPaymentMethods_Load;
            tlpPaymentMethods.ResumeLayout(false);
            tlpPaymentMethods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentMethods).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpPaymentMethods;
        private DataGridView dgvPaymentMethods;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonButton btnUpdate;
        private Krypton.Toolkit.KryptonButton btnAdd;
    }
}
