namespace WinForms
{
    partial class UserControlHistory
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
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            tlpHistory = new TableLayoutPanel();
            dgvSessions = new DataGridView();
            dgvSales = new DataGridView();
            lblSince = new Krypton.Toolkit.KryptonLabel();
            dtpSince = new Krypton.Toolkit.KryptonDateTimePicker();
            lblTo = new Krypton.Toolkit.KryptonLabel();
            dtpTo = new Krypton.Toolkit.KryptonDateTimePicker();
            btnSearch = new Krypton.Toolkit.KryptonButton();
            btnResetFilters = new Krypton.Toolkit.KryptonButton();
            cbxUsersFilter = new Krypton.Toolkit.KryptonComboBox();
            gbxFilters = new GroupBox();
            tlpFilters = new TableLayoutPanel();
            rbtMaxMoney = new Krypton.Toolkit.KryptonRadioButton();
            rbtDesDate = new Krypton.Toolkit.KryptonRadioButton();
            rbtDesName = new Krypton.Toolkit.KryptonRadioButton();
            rbtMinMoney = new Krypton.Toolkit.KryptonRadioButton();
            btnGenerateTotals = new Krypton.Toolkit.KryptonButton();
            lblTotals = new Krypton.Toolkit.KryptonLabel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            lblSellsTitle = new Krypton.Toolkit.KryptonLabel();
            btnShowDetails = new Krypton.Toolkit.KryptonButton();
            btnSumSellTotals = new Krypton.Toolkit.KryptonButton();
            btnSumFacts = new Krypton.Toolkit.KryptonButton();
            lblSellsTotals = new Krypton.Toolkit.KryptonLabel();
            lblSellFacts = new Krypton.Toolkit.KryptonLabel();
            tlpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbxUsersFilter).BeginInit();
            gbxFilters.SuspendLayout();
            tlpFilters.SuspendLayout();
            SuspendLayout();
            // 
            // tlpHistory
            // 
            tlpHistory.ColumnCount = 8;
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.859375F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.984375F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.078125F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.375F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.625F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.28125F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.625F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.09375F));
            tlpHistory.Controls.Add(dgvSessions, 1, 2);
            tlpHistory.Controls.Add(dgvSales, 4, 2);
            tlpHistory.Controls.Add(lblSince, 3, 5);
            tlpHistory.Controls.Add(dtpSince, 3, 6);
            tlpHistory.Controls.Add(lblTo, 3, 7);
            tlpHistory.Controls.Add(dtpTo, 3, 8);
            tlpHistory.Controls.Add(btnSearch, 3, 9);
            tlpHistory.Controls.Add(btnResetFilters, 3, 10);
            tlpHistory.Controls.Add(cbxUsersFilter, 3, 3);
            tlpHistory.Controls.Add(gbxFilters, 3, 4);
            tlpHistory.Controls.Add(btnGenerateTotals, 1, 1);
            tlpHistory.Controls.Add(lblTotals, 2, 1);
            tlpHistory.Controls.Add(lblTitle, 1, 0);
            tlpHistory.Controls.Add(lblSellsTitle, 4, 0);
            tlpHistory.Controls.Add(btnShowDetails, 4, 1);
            tlpHistory.Controls.Add(btnSumSellTotals, 5, 0);
            tlpHistory.Controls.Add(btnSumFacts, 5, 1);
            tlpHistory.Controls.Add(lblSellsTotals, 6, 0);
            tlpHistory.Controls.Add(lblSellFacts, 6, 1);
            tlpHistory.Dock = DockStyle.Fill;
            tlpHistory.Location = new Point(0, 0);
            tlpHistory.Name = "tlpHistory";
            tlpHistory.RowCount = 14;
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07154655F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07154655F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07154655F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07154655F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 21.8320618F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 5.038168F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 5.34351158F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 4.73282433F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 4.73282433F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 8.091603F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.480916F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 4.4274807F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 6.259542F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 3.34858537F));
            tlpHistory.Size = new Size(1280, 655);
            tlpHistory.TabIndex = 0;
            // 
            // dgvSessions
            // 
            dataGridViewCellStyle17.BackColor = Color.FromArgb(238, 239, 249);
            dgvSessions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            dgvSessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSessions.BorderStyle = BorderStyle.None;
            dgvSessions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle18.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle18.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle18.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.True;
            dgvSessions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            dgvSessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpHistory.SetColumnSpan(dgvSessions, 2);
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = SystemColors.Window;
            dataGridViewCellStyle19.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle19.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle19.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
            dgvSessions.DefaultCellStyle = dataGridViewCellStyle19;
            dgvSessions.Dock = DockStyle.Fill;
            dgvSessions.EnableHeadersVisualStyles = false;
            dgvSessions.Location = new Point(14, 95);
            dgvSessions.MultiSelect = false;
            dgvSessions.Name = "dgvSessions";
            dgvSessions.ReadOnly = true;
            dgvSessions.RowHeadersVisible = false;
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvSessions.RowsDefaultCellStyle = dataGridViewCellStyle20;
            tlpHistory.SetRowSpan(dgvSessions, 11);
            dgvSessions.RowTemplate.Height = 35;
            dgvSessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSessions.Size = new Size(558, 531);
            dgvSessions.TabIndex = 18;
            dgvSessions.SelectionChanged += dgvSessions_SelectionChanged;
            // 
            // dgvSales
            // 
            dataGridViewCellStyle21.BackColor = Color.FromArgb(238, 239, 249);
            dgvSales.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.BorderStyle = BorderStyle.None;
            dgvSales.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle22.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle22.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle22.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle22.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = DataGridViewTriState.True;
            dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpHistory.SetColumnSpan(dgvSales, 3);
            dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = SystemColors.Window;
            dataGridViewCellStyle23.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle23.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle23.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = DataGridViewTriState.False;
            dgvSales.DefaultCellStyle = dataGridViewCellStyle23;
            dgvSales.Dock = DockStyle.Fill;
            dgvSales.EnableHeadersVisualStyles = false;
            dgvSales.Location = new Point(762, 95);
            dgvSales.MultiSelect = false;
            dgvSales.Name = "dgvSales";
            dgvSales.ReadOnly = true;
            dgvSales.RowHeadersVisible = false;
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvSales.RowsDefaultCellStyle = dataGridViewCellStyle24;
            tlpHistory.SetRowSpan(dgvSales, 11);
            dgvSales.RowTemplate.Height = 35;
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.Size = new Size(500, 531);
            dgvSales.TabIndex = 20;
            // 
            // lblSince
            // 
            lblSince.Anchor = AnchorStyles.Bottom;
            lblSince.Location = new Point(644, 337);
            lblSince.Name = "lblSince";
            lblSince.Size = new Size(45, 20);
            lblSince.TabIndex = 14;
            lblSince.Values.Text = "Desde";
            // 
            // dtpSince
            // 
            dtpSince.Anchor = AnchorStyles.Top;
            dtpSince.CalendarFirstDayOfWeek = Day.Monday;
            dtpSince.Location = new Point(591, 363);
            dtpSince.Name = "dtpSince";
            dtpSince.Size = new Size(152, 21);
            dtpSince.TabIndex = 15;
            // 
            // lblTo
            // 
            lblTo.Anchor = AnchorStyles.Bottom;
            lblTo.Location = new Point(646, 403);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(41, 20);
            lblTo.TabIndex = 16;
            lblTo.Values.Text = "Hasta";
            // 
            // dtpTo
            // 
            dtpTo.Anchor = AnchorStyles.Top;
            dtpTo.CalendarFirstDayOfWeek = Day.Monday;
            dtpTo.Location = new Point(591, 429);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(152, 21);
            dtpTo.TabIndex = 17;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Bottom;
            btnSearch.Location = new Point(603, 467);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(127, 40);
            btnSearch.TabIndex = 19;
            btnSearch.Values.DropDownArrowColor = Color.Empty;
            btnSearch.Values.Text = "Buscar";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnResetFilters
            // 
            btnResetFilters.Anchor = AnchorStyles.Bottom;
            btnResetFilters.Location = new Point(603, 516);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(127, 40);
            btnResetFilters.TabIndex = 10;
            btnResetFilters.Values.DropDownArrowColor = Color.Empty;
            btnResetFilters.Values.Text = "Reiniciar Filtros";
            btnResetFilters.Click += btnResetFilters_Click;
            // 
            // cbxUsersFilter
            // 
            cbxUsersFilter.Anchor = AnchorStyles.None;
            cbxUsersFilter.DropDownWidth = 152;
            cbxUsersFilter.Location = new Point(585, 150);
            cbxUsersFilter.Name = "cbxUsersFilter";
            cbxUsersFilter.Size = new Size(164, 22);
            cbxUsersFilter.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxUsersFilter.TabIndex = 24;
            cbxUsersFilter.Text = "USUARIOS";
            // 
            // gbxFilters
            // 
            gbxFilters.Controls.Add(tlpFilters);
            gbxFilters.Dock = DockStyle.Fill;
            gbxFilters.Location = new Point(578, 187);
            gbxFilters.Name = "gbxFilters";
            gbxFilters.Size = new Size(178, 137);
            gbxFilters.TabIndex = 29;
            gbxFilters.TabStop = false;
            // 
            // tlpFilters
            // 
            tlpFilters.ColumnCount = 1;
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFilters.Controls.Add(rbtMaxMoney, 0, 3);
            tlpFilters.Controls.Add(rbtDesDate, 0, 0);
            tlpFilters.Controls.Add(rbtDesName, 0, 1);
            tlpFilters.Controls.Add(rbtMinMoney, 0, 2);
            tlpFilters.Dock = DockStyle.Fill;
            tlpFilters.Location = new Point(3, 19);
            tlpFilters.Name = "tlpFilters";
            tlpFilters.RowCount = 4;
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFilters.Size = new Size(172, 115);
            tlpFilters.TabIndex = 15;
            // 
            // rbtMaxMoney
            // 
            rbtMaxMoney.Anchor = AnchorStyles.None;
            rbtMaxMoney.Location = new Point(3, 89);
            rbtMaxMoney.Name = "rbtMaxMoney";
            rbtMaxMoney.Size = new Size(166, 20);
            rbtMaxMoney.TabIndex = 14;
            rbtMaxMoney.Values.Text = "Ordenar por Mayor Recaudación";
            // 
            // rbtDesDate
            // 
            rbtDesDate.Anchor = AnchorStyles.None;
            rbtDesDate.Checked = true;
            rbtDesDate.Location = new Point(24, 4);
            rbtDesDate.Name = "rbtDesDate";
            rbtDesDate.Size = new Size(124, 20);
            rbtDesDate.TabIndex = 11;
            rbtDesDate.Values.Text = "Ordenar por Fecha";
            // 
            // rbtDesName
            // 
            rbtDesName.Anchor = AnchorStyles.None;
            rbtDesName.Location = new Point(17, 32);
            rbtDesName.Name = "rbtDesName";
            rbtDesName.Size = new Size(138, 20);
            rbtDesName.TabIndex = 12;
            rbtDesName.Values.Text = "Ordenar por Nombre";
            // 
            // rbtMinMoney
            // 
            rbtMinMoney.Anchor = AnchorStyles.None;
            rbtMinMoney.Location = new Point(3, 60);
            rbtMinMoney.Name = "rbtMinMoney";
            rbtMinMoney.Size = new Size(166, 20);
            rbtMinMoney.TabIndex = 13;
            rbtMinMoney.Values.Text = "Ordenar por Menor Recaudación";
            // 
            // btnGenerateTotals
            // 
            btnGenerateTotals.Anchor = AnchorStyles.None;
            btnGenerateTotals.Location = new Point(16, 52);
            btnGenerateTotals.Name = "btnGenerateTotals";
            btnGenerateTotals.Size = new Size(168, 34);
            btnGenerateTotals.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnGenerateTotals.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerateTotals.TabIndex = 30;
            btnGenerateTotals.Values.DropDownArrowColor = Color.Empty;
            btnGenerateTotals.Values.Text = "SUMAR TOTALES";
            btnGenerateTotals.Click += btnGenerateTotals_Click;
            // 
            // lblTotals
            // 
            lblTotals.Anchor = AnchorStyles.Left;
            lblTotals.Location = new Point(193, 68);
            lblTotals.Name = "lblTotals";
            lblTotals.Size = new Size(6, 2);
            lblTotals.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotals.TabIndex = 31;
            lblTotals.Values.Text = "";
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpHistory.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(211, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(163, 29);
            lblTitle.TabIndex = 7;
            lblTitle.Values.Text = "Historial de Cajas";
            // 
            // lblSellsTitle
            // 
            lblSellsTitle.Anchor = AnchorStyles.None;
            lblSellsTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblSellsTitle.Location = new Point(791, 8);
            lblSellsTitle.Name = "lblSellsTitle";
            lblSellsTitle.Size = new Size(71, 29);
            lblSellsTitle.TabIndex = 21;
            lblSellsTitle.Values.Text = "Ventas";
            // 
            // btnShowDetails
            // 
            btnShowDetails.Anchor = AnchorStyles.Bottom;
            btnShowDetails.Location = new Point(763, 49);
            btnShowDetails.Name = "btnShowDetails";
            btnShowDetails.Size = new Size(127, 40);
            btnShowDetails.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnShowDetails.TabIndex = 22;
            btnShowDetails.Values.DropDownArrowColor = Color.Empty;
            btnShowDetails.Values.Text = "DETALLE";
            btnShowDetails.Click += btnShowDetails_Click;
            // 
            // btnSumSellTotals
            // 
            btnSumSellTotals.Anchor = AnchorStyles.None;
            btnSumSellTotals.Location = new Point(906, 6);
            btnSumSellTotals.Name = "btnSumSellTotals";
            btnSumSellTotals.Size = new Size(147, 34);
            btnSumSellTotals.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSumSellTotals.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSumSellTotals.TabIndex = 32;
            btnSumSellTotals.Values.DropDownArrowColor = Color.Empty;
            btnSumSellTotals.Values.Text = "SUMA TOTALES";
            btnSumSellTotals.Click += btnSumSellTotals_Click;
            // 
            // btnSumFacts
            // 
            btnSumFacts.Anchor = AnchorStyles.None;
            btnSumFacts.Location = new Point(906, 52);
            btnSumFacts.Name = "btnSumFacts";
            btnSumFacts.Size = new Size(147, 34);
            btnSumFacts.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSumFacts.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSumFacts.TabIndex = 33;
            btnSumFacts.Values.DropDownArrowColor = Color.Empty;
            btnSumFacts.Values.Text = "SUMA FACT.";
            btnSumFacts.Click += btnSumFacts_Click;
            // 
            // lblSellsTotals
            // 
            lblSellsTotals.Anchor = AnchorStyles.Left;
            lblSellsTotals.Location = new Point(1068, 22);
            lblSellsTotals.Name = "lblSellsTotals";
            lblSellsTotals.Size = new Size(6, 2);
            lblSellsTotals.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSellsTotals.TabIndex = 34;
            lblSellsTotals.Values.Text = "";
            // 
            // lblSellFacts
            // 
            lblSellFacts.Anchor = AnchorStyles.Left;
            lblSellFacts.Location = new Point(1068, 68);
            lblSellFacts.Name = "lblSellFacts";
            lblSellFacts.Size = new Size(6, 2);
            lblSellFacts.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSellFacts.TabIndex = 35;
            lblSellFacts.Values.Text = "";
            // 
            // UserControlHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpHistory);
            Name = "UserControlHistory";
            Size = new Size(1280, 655);
            Load += UserControlHistory_Load;
            tlpHistory.ResumeLayout(false);
            tlpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSales).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbxUsersFilter).EndInit();
            gbxFilters.ResumeLayout(false);
            tlpFilters.ResumeLayout(false);
            tlpFilters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpHistory;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnResetFilters;
        private Krypton.Toolkit.KryptonLabel lblSince;
        private Krypton.Toolkit.KryptonDateTimePicker dtpSince;
        private Krypton.Toolkit.KryptonLabel lblTo;
        private Krypton.Toolkit.KryptonDateTimePicker dtpTo;
        private DataGridView dgvSessions;
        private DataGridView dgvSales;
        private Krypton.Toolkit.KryptonLabel lblSellsTitle;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private Krypton.Toolkit.KryptonButton btnShowDetails;
        private Krypton.Toolkit.KryptonComboBox cbxUsersFilter;
        private GroupBox gbxFilters;
        private TableLayoutPanel tlpFilters;
        private Krypton.Toolkit.KryptonRadioButton rbtMaxMoney;
        private Krypton.Toolkit.KryptonRadioButton rbtDesDate;
        private Krypton.Toolkit.KryptonRadioButton rbtDesName;
        private Krypton.Toolkit.KryptonRadioButton rbtMinMoney;
        private Krypton.Toolkit.KryptonButton btnGenerateTotals;
        private Krypton.Toolkit.KryptonLabel lblTotals;
        private Krypton.Toolkit.KryptonButton btnSumSellTotals;
        private Krypton.Toolkit.KryptonButton btnSumFacts;
        private Krypton.Toolkit.KryptonLabel lblSellsTotals;
        private Krypton.Toolkit.KryptonLabel lblSellFacts;
    }
}
