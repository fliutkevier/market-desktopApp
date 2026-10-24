namespace WinForms
{
    partial class UserControlProviders
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
            tlpProviders = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            gbxFilters = new GroupBox();
            tlpFilters = new TableLayoutPanel();
            rbtDesDate = new Krypton.Toolkit.KryptonRadioButton();
            rbtDesName = new Krypton.Toolkit.KryptonRadioButton();
            btnPurchaseHistory = new Krypton.Toolkit.KryptonButton();
            btnUpdate = new Krypton.Toolkit.KryptonButton();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            btnResetFilters = new Krypton.Toolkit.KryptonButton();
            dgvProviders = new DataGridView();
            tlpProviders.SuspendLayout();
            gbxFilters.SuspendLayout();
            tlpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProviders).BeginInit();
            SuspendLayout();
            // 
            // tlpProviders
            // 
            tlpProviders.ColumnCount = 9;
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.171875F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.015625F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tlpProviders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.96875F));
            tlpProviders.Controls.Add(lblTitle, 1, 0);
            tlpProviders.Controls.Add(txtFilter, 1, 1);
            tlpProviders.Controls.Add(btnAdd, 3, 1);
            tlpProviders.Controls.Add(gbxFilters, 8, 3);
            tlpProviders.Controls.Add(btnPurchaseHistory, 8, 1);
            tlpProviders.Controls.Add(btnUpdate, 4, 1);
            tlpProviders.Controls.Add(btnDelete, 5, 1);
            tlpProviders.Controls.Add(btnResetFilters, 8, 4);
            tlpProviders.Controls.Add(dgvProviders, 1, 2);
            tlpProviders.Dock = DockStyle.Fill;
            tlpProviders.Location = new Point(0, 0);
            tlpProviders.Name = "tlpProviders";
            tlpProviders.RowCount = 12;
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 29.61832F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 8.549619F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 3.816794F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 4.73282433F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 4.4274807F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 6.87022924F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 6.259542F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 6.87022924F));
            tlpProviders.RowStyles.Add(new RowStyle(SizeType.Percent, 3.35877872F));
            tlpProviders.Size = new Size(1280, 655);
            tlpProviders.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpProviders.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(157, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(121, 29);
            lblTitle.TabIndex = 7;
            lblTitle.Values.Text = "Proveedores";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpProviders.SetColumnSpan(txtFilter, 2);
            txtFilter.CueHint.CueHintText = "Buscar";
            txtFilter.Location = new Point(46, 69);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(343, 23);
            txtFilter.TabIndex = 9;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(429, 61);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.TabIndex = 16;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // gbxFilters
            // 
            gbxFilters.Controls.Add(tlpFilters);
            gbxFilters.Dock = DockStyle.Fill;
            gbxFilters.Location = new Point(1052, 165);
            gbxFilters.Name = "gbxFilters";
            gbxFilters.Size = new Size(225, 188);
            gbxFilters.TabIndex = 20;
            gbxFilters.TabStop = false;
            // 
            // tlpFilters
            // 
            tlpFilters.ColumnCount = 1;
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFilters.Controls.Add(rbtDesDate, 0, 0);
            tlpFilters.Controls.Add(rbtDesName, 0, 1);
            tlpFilters.Dock = DockStyle.Fill;
            tlpFilters.Location = new Point(3, 19);
            tlpFilters.Name = "tlpFilters";
            tlpFilters.RowCount = 2;
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.Size = new Size(219, 166);
            tlpFilters.TabIndex = 15;
            // 
            // rbtDesDate
            // 
            rbtDesDate.Anchor = AnchorStyles.None;
            rbtDesDate.Checked = true;
            rbtDesDate.Location = new Point(47, 31);
            rbtDesDate.Name = "rbtDesDate";
            rbtDesDate.Size = new Size(124, 20);
            rbtDesDate.TabIndex = 11;
            rbtDesDate.Values.Text = "Ordenar por Fecha";
            rbtDesDate.CheckedChanged += rbtDesDate_CheckedChanged;
            // 
            // rbtDesName
            // 
            rbtDesName.Anchor = AnchorStyles.None;
            rbtDesName.Location = new Point(40, 114);
            rbtDesName.Name = "rbtDesName";
            rbtDesName.Size = new Size(138, 20);
            rbtDesName.TabIndex = 12;
            rbtDesName.Values.Text = "Ordenar por Nombre";
            rbtDesName.CheckedChanged += rbtDesName_CheckedChanged;
            // 
            // btnPurchaseHistory
            // 
            btnPurchaseHistory.Anchor = AnchorStyles.None;
            btnPurchaseHistory.Location = new Point(1103, 61);
            btnPurchaseHistory.Name = "btnPurchaseHistory";
            btnPurchaseHistory.Size = new Size(123, 40);
            btnPurchaseHistory.TabIndex = 27;
            btnPurchaseHistory.Values.DropDownArrowColor = Color.Empty;
            btnPurchaseHistory.Values.Text = "Historial de Compras";
            btnPurchaseHistory.Click += btnPurchaseHistory_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.None;
            btnUpdate.Location = new Point(570, 61);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(127, 40);
            btnUpdate.TabIndex = 17;
            btnUpdate.Values.DropDownArrowColor = Color.Empty;
            btnUpdate.Values.Text = "Editar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            btnDelete.Location = new Point(713, 61);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(123, 40);
            btnDelete.TabIndex = 18;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnResetFilters
            // 
            btnResetFilters.Anchor = AnchorStyles.Top;
            btnResetFilters.Location = new Point(1101, 359);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(127, 40);
            btnResetFilters.TabIndex = 24;
            btnResetFilters.Values.DropDownArrowColor = Color.Empty;
            btnResetFilters.Values.Text = "Reiniciar Filtros";
            btnResetFilters.Click += btnResetFilters_Click;
            // 
            // dgvProviders
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvProviders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProviders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProviders.BorderStyle = BorderStyle.None;
            dgvProviders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProviders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProviders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpProviders.SetColumnSpan(dgvProviders, 7);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvProviders.DefaultCellStyle = dataGridViewCellStyle3;
            dgvProviders.Dock = DockStyle.Fill;
            dgvProviders.EnableHeadersVisualStyles = false;
            dgvProviders.Location = new Point(17, 111);
            dgvProviders.MultiSelect = false;
            dgvProviders.Name = "dgvProviders";
            dgvProviders.ReadOnly = true;
            dgvProviders.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProviders.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpProviders.SetRowSpan(dgvProviders, 9);
            dgvProviders.RowTemplate.Height = 35;
            dgvProviders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProviders.Size = new Size(1029, 514);
            dgvProviders.TabIndex = 28;
            // 
            // UserControlProviders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpProviders);
            Name = "UserControlProviders";
            Size = new Size(1280, 655);
            Load += UserControlProviders_Load;
            tlpProviders.ResumeLayout(false);
            tlpProviders.PerformLayout();
            gbxFilters.ResumeLayout(false);
            tlpFilters.ResumeLayout(false);
            tlpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProviders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpProviders;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnUpdate;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private GroupBox gbxFilters;
        private Krypton.Toolkit.KryptonRadioButton rbtDesDate;
        private Krypton.Toolkit.KryptonRadioButton rbtDesName;
        private Krypton.Toolkit.KryptonButton btnResetFilters;
        private TableLayoutPanel tlpFilters;
        private Krypton.Toolkit.KryptonButton btnPurchaseHistory;
        private DataGridView dgvProviders;
    }
}
