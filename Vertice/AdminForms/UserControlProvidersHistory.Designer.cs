namespace WinForms
{
    partial class UserControlProvidersHistory
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
            tlpProducts = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            btnResetFilters = new Krypton.Toolkit.KryptonButton();
            cbxProvidersFilter = new Krypton.Toolkit.KryptonComboBox();
            gbxFilters = new GroupBox();
            tlpFilters = new TableLayoutPanel();
            rbtDesDate = new Krypton.Toolkit.KryptonRadioButton();
            rbtMaxCost = new Krypton.Toolkit.KryptonRadioButton();
            rbtDesName = new Krypton.Toolkit.KryptonRadioButton();
            rbtMinCost = new Krypton.Toolkit.KryptonRadioButton();
            lblSince = new Krypton.Toolkit.KryptonLabel();
            dtpSince = new Krypton.Toolkit.KryptonDateTimePicker();
            lblTo = new Krypton.Toolkit.KryptonLabel();
            dtpTo = new Krypton.Toolkit.KryptonDateTimePicker();
            btnSearch = new Krypton.Toolkit.KryptonButton();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            btnUpdate = new Krypton.Toolkit.KryptonButton();
            btnShowDetail = new Krypton.Toolkit.KryptonButton();
            dgvMovements = new DataGridView();
            tlpProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cbxProvidersFilter).BeginInit();
            gbxFilters.SuspendLayout();
            tlpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovements).BeginInit();
            SuspendLayout();
            // 
            // tlpProducts
            // 
            tlpProducts.ColumnCount = 9;
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.171875F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.015625F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.96875F));
            tlpProducts.Controls.Add(lblTitle, 1, 0);
            tlpProducts.Controls.Add(txtFilter, 1, 1);
            tlpProducts.Controls.Add(btnAdd, 3, 1);
            tlpProducts.Controls.Add(btnResetFilters, 8, 9);
            tlpProducts.Controls.Add(cbxProvidersFilter, 7, 1);
            tlpProducts.Controls.Add(gbxFilters, 8, 2);
            tlpProducts.Controls.Add(lblSince, 8, 4);
            tlpProducts.Controls.Add(dtpSince, 8, 5);
            tlpProducts.Controls.Add(lblTo, 8, 6);
            tlpProducts.Controls.Add(dtpTo, 8, 7);
            tlpProducts.Controls.Add(btnSearch, 8, 8);
            tlpProducts.Controls.Add(btnDelete, 6, 1);
            tlpProducts.Controls.Add(btnUpdate, 5, 1);
            tlpProducts.Controls.Add(btnShowDetail, 4, 1);
            tlpProducts.Controls.Add(dgvMovements, 1, 2);
            tlpProducts.Dock = DockStyle.Fill;
            tlpProducts.Location = new Point(0, 0);
            tlpProducts.Name = "tlpProducts";
            tlpProducts.RowCount = 12;
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 26.5648861F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 4.12213755F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 4.885496F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 5.34351158F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 4.73282433F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 5.64885473F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 8.549619F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 8.396947F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 11.2977095F));
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Percent, 3.35877872F));
            tlpProducts.Size = new Size(1280, 655);
            tlpProducts.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(120, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(195, 29);
            lblTitle.TabIndex = 7;
            lblTitle.Values.Text = "Historial de Compras";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(txtFilter, 2);
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
            btnAdd.Values.Text = "Agregar Compra";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnResetFilters
            // 
            btnResetFilters.Anchor = AnchorStyles.Bottom;
            btnResetFilters.Location = new Point(1101, 512);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(127, 40);
            btnResetFilters.TabIndex = 24;
            btnResetFilters.Values.DropDownArrowColor = Color.Empty;
            btnResetFilters.Values.Text = "Reiniciar Filtros";
            btnResetFilters.Click += btnResetFilters_Click;
            // 
            // cbxProvidersFilter
            // 
            cbxProvidersFilter.Anchor = AnchorStyles.None;
            tlpProducts.SetColumnSpan(cbxProvidersFilter, 2);
            cbxProvidersFilter.DropDownWidth = 152;
            cbxProvidersFilter.Location = new Point(1036, 70);
            cbxProvidersFilter.Name = "cbxProvidersFilter";
            cbxProvidersFilter.Size = new Size(194, 22);
            cbxProvidersFilter.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxProvidersFilter.TabIndex = 25;
            cbxProvidersFilter.Text = "PROVEEDORES";
            cbxProvidersFilter.SelectedIndexChanged += cbxProvidersFilter_SelectedIndexChanged;
            // 
            // gbxFilters
            // 
            gbxFilters.Controls.Add(tlpFilters);
            gbxFilters.Dock = DockStyle.Fill;
            gbxFilters.Location = new Point(1052, 111);
            gbxFilters.Name = "gbxFilters";
            gbxFilters.Size = new Size(225, 168);
            gbxFilters.TabIndex = 20;
            gbxFilters.TabStop = false;
            // 
            // tlpFilters
            // 
            tlpFilters.ColumnCount = 1;
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFilters.Controls.Add(rbtDesDate, 0, 0);
            tlpFilters.Controls.Add(rbtMaxCost, 0, 3);
            tlpFilters.Controls.Add(rbtDesName, 0, 1);
            tlpFilters.Controls.Add(rbtMinCost, 0, 2);
            tlpFilters.Dock = DockStyle.Fill;
            tlpFilters.Location = new Point(3, 19);
            tlpFilters.Name = "tlpFilters";
            tlpFilters.RowCount = 4;
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.Size = new Size(219, 146);
            tlpFilters.TabIndex = 15;
            // 
            // rbtDesDate
            // 
            rbtDesDate.Anchor = AnchorStyles.None;
            rbtDesDate.Checked = true;
            rbtDesDate.Location = new Point(47, 8);
            rbtDesDate.Name = "rbtDesDate";
            rbtDesDate.Size = new Size(124, 20);
            rbtDesDate.TabIndex = 11;
            rbtDesDate.Values.Text = "Ordenar por Fecha";
            rbtDesDate.CheckedChanged += rbtDesDate_CheckedChanged;
            // 
            // rbtMaxCost
            // 
            rbtMaxCost.Anchor = AnchorStyles.None;
            rbtMaxCost.Location = new Point(28, 117);
            rbtMaxCost.Name = "rbtMaxCost";
            rbtMaxCost.Size = new Size(163, 20);
            rbtMaxCost.TabIndex = 14;
            rbtMaxCost.Values.Text = "Ordenar por Mayor Gasto";
            rbtMaxCost.CheckedChanged += rbtMaxCost_CheckedChanged;
            // 
            // rbtDesName
            // 
            rbtDesName.Anchor = AnchorStyles.None;
            rbtDesName.Location = new Point(40, 44);
            rbtDesName.Name = "rbtDesName";
            rbtDesName.Size = new Size(138, 20);
            rbtDesName.TabIndex = 12;
            rbtDesName.Values.Text = "Ordenar por Nombre";
            rbtDesName.CheckedChanged += rbtDesName_CheckedChanged;
            // 
            // rbtMinCost
            // 
            rbtMinCost.Anchor = AnchorStyles.None;
            rbtMinCost.Location = new Point(27, 80);
            rbtMinCost.Name = "rbtMinCost";
            rbtMinCost.Size = new Size(164, 20);
            rbtMinCost.TabIndex = 13;
            rbtMinCost.Values.Text = "Ordenar por Menor Gasto";
            rbtMinCost.CheckedChanged += rbtMinCost_CheckedChanged;
            // 
            // lblSince
            // 
            lblSince.Anchor = AnchorStyles.Bottom;
            lblSince.Location = new Point(1142, 318);
            lblSince.Name = "lblSince";
            lblSince.Size = new Size(45, 20);
            lblSince.TabIndex = 22;
            lblSince.Values.Text = "Desde";
            // 
            // dtpSince
            // 
            dtpSince.Anchor = AnchorStyles.Top;
            dtpSince.CalendarFirstDayOfWeek = Day.Monday;
            dtpSince.Location = new Point(1054, 344);
            dtpSince.Name = "dtpSince";
            dtpSince.Size = new Size(221, 21);
            dtpSince.TabIndex = 23;
            // 
            // lblTo
            // 
            lblTo.Anchor = AnchorStyles.Bottom;
            lblTo.Location = new Point(1144, 384);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(41, 20);
            lblTo.TabIndex = 25;
            lblTo.Values.Text = "Hasta";
            // 
            // dtpTo
            // 
            dtpTo.Anchor = AnchorStyles.Top;
            dtpTo.CalendarFirstDayOfWeek = Day.Monday;
            dtpTo.Location = new Point(1054, 410);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(221, 21);
            dtpTo.TabIndex = 26;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Bottom;
            btnSearch.Location = new Point(1101, 457);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(127, 40);
            btnSearch.TabIndex = 27;
            btnSearch.Values.DropDownArrowColor = Color.Empty;
            btnSearch.Values.Text = "Buscar";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            btnDelete.Location = new Point(854, 61);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(123, 40);
            btnDelete.TabIndex = 18;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.None;
            btnUpdate.Location = new Point(711, 61);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(127, 40);
            btnUpdate.TabIndex = 17;
            btnUpdate.Values.DropDownArrowColor = Color.Empty;
            btnUpdate.Values.Text = "Editar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnShowDetail
            // 
            btnShowDetail.Anchor = AnchorStyles.None;
            btnShowDetail.Location = new Point(570, 61);
            btnShowDetail.Name = "btnShowDetail";
            btnShowDetail.Size = new Size(127, 40);
            btnShowDetail.TabIndex = 28;
            btnShowDetail.Values.DropDownArrowColor = Color.Empty;
            btnShowDetail.Values.Text = "Ver Detalle";
            btnShowDetail.Click += btnShowDetail_Click;
            // 
            // dgvMovements
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvMovements.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvMovements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovements.BorderStyle = BorderStyle.None;
            dgvMovements.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvMovements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMovements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpProducts.SetColumnSpan(dgvMovements, 7);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvMovements.DefaultCellStyle = dataGridViewCellStyle3;
            dgvMovements.Dock = DockStyle.Fill;
            dgvMovements.EnableHeadersVisualStyles = false;
            dgvMovements.Location = new Point(17, 111);
            dgvMovements.MultiSelect = false;
            dgvMovements.Name = "dgvMovements";
            dgvMovements.ReadOnly = true;
            dgvMovements.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvMovements.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpProducts.SetRowSpan(dgvMovements, 9);
            dgvMovements.RowTemplate.Height = 35;
            dgvMovements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovements.Size = new Size(1029, 515);
            dgvMovements.TabIndex = 29;
            // 
            // UserControlProvidersHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpProducts);
            Name = "UserControlProvidersHistory";
            Size = new Size(1280, 655);
            Load += UserControlProvidersHistory_Load;
            tlpProducts.ResumeLayout(false);
            tlpProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cbxProvidersFilter).EndInit();
            gbxFilters.ResumeLayout(false);
            tlpFilters.ResumeLayout(false);
            tlpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovements).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpProducts;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonButton btnUpdate;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private GroupBox gbxFilters;
        private TableLayoutPanel tlpFilters;
        private Krypton.Toolkit.KryptonRadioButton rbtDesDate;
        private Krypton.Toolkit.KryptonRadioButton rbtDesName;
        private Krypton.Toolkit.KryptonLabel lblSince;
        private Krypton.Toolkit.KryptonDateTimePicker dtpSince;
        private Krypton.Toolkit.KryptonLabel lblTo;
        private Krypton.Toolkit.KryptonDateTimePicker dtpTo;
        private Krypton.Toolkit.KryptonButton btnResetFilters;
        private Krypton.Toolkit.KryptonRadioButton rbtMaxCost;
        private Krypton.Toolkit.KryptonRadioButton rbtMinCost;
        private Krypton.Toolkit.KryptonComboBox cbxProvidersFilter;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private Krypton.Toolkit.KryptonButton btnShowDetail;
        private DataGridView dgvMovements;
    }
}
