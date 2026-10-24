namespace WinForms
{
    partial class UserControlLogs
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
            tlpHistory = new TableLayoutPanel();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            btnResetFilters = new Krypton.Toolkit.KryptonButton();
            dtpTo = new Krypton.Toolkit.KryptonDateTimePicker();
            lblTo = new Krypton.Toolkit.KryptonLabel();
            dtpSince = new Krypton.Toolkit.KryptonDateTimePicker();
            lblSince = new Krypton.Toolkit.KryptonLabel();
            dgvLogs = new DataGridView();
            cbxUsersFilter = new Krypton.Toolkit.KryptonComboBox();
            btnSearch = new Krypton.Toolkit.KryptonButton();
            tlpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbxUsersFilter).BeginInit();
            SuspendLayout();
            // 
            // tlpHistory
            // 
            tlpHistory.ColumnCount = 7;
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.578125F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.765625F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.1601238F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2332916F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2332916F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.2865553F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.6693039F));
            tlpHistory.Controls.Add(txtFilter, 1, 1);
            tlpHistory.Controls.Add(lblTitle, 1, 0);
            tlpHistory.Controls.Add(btnResetFilters, 6, 11);
            tlpHistory.Controls.Add(dtpTo, 6, 9);
            tlpHistory.Controls.Add(lblTo, 6, 8);
            tlpHistory.Controls.Add(dtpSince, 6, 7);
            tlpHistory.Controls.Add(lblSince, 6, 6);
            tlpHistory.Controls.Add(dgvLogs, 1, 2);
            tlpHistory.Controls.Add(cbxUsersFilter, 6, 4);
            tlpHistory.Controls.Add(btnSearch, 6, 10);
            tlpHistory.Dock = DockStyle.Fill;
            tlpHistory.Location = new Point(0, 0);
            tlpHistory.Name = "tlpHistory";
            tlpHistory.RowCount = 14;
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.142857F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.142857F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 4.58015251F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 12.3664122F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 6.71755743F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.93893147F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 6.87022924F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 5.64885473F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 4.4274807F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 7.142857F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 8.244275F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 8.091603F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 9.770992F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 3.38235283F));
            tlpHistory.Size = new Size(1280, 655);
            tlpHistory.TabIndex = 1;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpHistory.SetColumnSpan(txtFilter, 2);
            txtFilter.CueHint.CueHintText = "Buscar";
            txtFilter.Location = new Point(117, 59);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(343, 23);
            txtFilter.TabIndex = 8;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpHistory.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(241, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(94, 29);
            lblTitle.TabIndex = 7;
            lblTitle.Values.Text = "Registros";
            // 
            // btnResetFilters
            // 
            btnResetFilters.Anchor = AnchorStyles.None;
            btnResetFilters.Location = new Point(1102, 519);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(127, 40);
            btnResetFilters.TabIndex = 10;
            btnResetFilters.Values.DropDownArrowColor = Color.Empty;
            btnResetFilters.Values.Text = "Reiniciar Filtros";
            btnResetFilters.Click += btnResetFilters_Click;
            // 
            // dtpTo
            // 
            dtpTo.Anchor = AnchorStyles.Top;
            dtpTo.CalendarFirstDayOfWeek = Day.Monday;
            dtpTo.Location = new Point(1055, 415);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(221, 21);
            dtpTo.TabIndex = 17;
            // 
            // lblTo
            // 
            lblTo.Anchor = AnchorStyles.Bottom;
            lblTo.Location = new Point(1145, 389);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(41, 20);
            lblTo.TabIndex = 16;
            lblTo.Values.Text = "Hasta";
            // 
            // dtpSince
            // 
            dtpSince.Anchor = AnchorStyles.Top;
            dtpSince.CalendarFirstDayOfWeek = Day.Monday;
            dtpSince.Location = new Point(1055, 349);
            dtpSince.Name = "dtpSince";
            dtpSince.Size = new Size(221, 21);
            dtpSince.TabIndex = 15;
            // 
            // lblSince
            // 
            lblSince.Anchor = AnchorStyles.Bottom;
            lblSince.Location = new Point(1143, 323);
            lblSince.Name = "lblSince";
            lblSince.Size = new Size(45, 20);
            lblSince.TabIndex = 14;
            lblSince.Values.Text = "Desde";
            // 
            // dgvLogs
            // 
            dataGridViewCellStyle17.BackColor = Color.FromArgb(238, 239, 249);
            dgvLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle18.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle18.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle18.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.True;
            dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpHistory.SetColumnSpan(dgvLogs, 5);
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = SystemColors.Window;
            dataGridViewCellStyle19.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle19.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle19.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
            dgvLogs.DefaultCellStyle = dataGridViewCellStyle19;
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.Location = new Point(36, 97);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvLogs.RowsDefaultCellStyle = dataGridViewCellStyle20;
            tlpHistory.SetRowSpan(dgvLogs, 11);
            dgvLogs.RowTemplate.Height = 35;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(1013, 530);
            dgvLogs.TabIndex = 30;
            // 
            // cbxUsersFilter
            // 
            cbxUsersFilter.Anchor = AnchorStyles.None;
            cbxUsersFilter.DropDownWidth = 152;
            cbxUsersFilter.Location = new Point(1069, 216);
            cbxUsersFilter.Name = "cbxUsersFilter";
            cbxUsersFilter.Size = new Size(194, 22);
            cbxUsersFilter.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxUsersFilter.TabIndex = 31;
            cbxUsersFilter.Text = "USUARIOS";
            cbxUsersFilter.SelectedIndexChanged += cbxUsersFilter_SelectedIndexChanged;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Bottom;
            btnSearch.Location = new Point(1102, 470);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(127, 40);
            btnSearch.TabIndex = 32;
            btnSearch.Values.DropDownArrowColor = Color.Empty;
            btnSearch.Values.Text = "Buscar";
            btnSearch.Click += btnSearch_Click;
            // 
            // UserControlLogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpHistory);
            Name = "UserControlLogs";
            Size = new Size(1280, 655);
            Load += UserControlLogs_Load;
            tlpHistory.ResumeLayout(false);
            tlpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbxUsersFilter).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpHistory;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnResetFilters;
        private Krypton.Toolkit.KryptonDateTimePicker dtpTo;
        private Krypton.Toolkit.KryptonLabel lblTo;
        private Krypton.Toolkit.KryptonDateTimePicker dtpSince;
        private Krypton.Toolkit.KryptonLabel lblSince;
        private DataGridView dgvLogs;
        private Krypton.Toolkit.KryptonComboBox cbxUsersFilter;
        private Krypton.Toolkit.KryptonButton btnSearch;
    }
}
