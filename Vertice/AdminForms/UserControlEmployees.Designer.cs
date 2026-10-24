namespace WinForms
{
    partial class UserControlEmployees
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
            tlpEmployees = new TableLayoutPanel();
            btnResetFilters = new Krypton.Toolkit.KryptonButton();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            btnUpdate = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            dgvEmployees = new DataGridView();
            gbxFilters = new GroupBox();
            tlpFilters = new TableLayoutPanel();
            rbtDesDate = new Krypton.Toolkit.KryptonRadioButton();
            rbtDesName = new Krypton.Toolkit.KryptonRadioButton();
            tlpEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            gbxFilters.SuspendLayout();
            tlpFilters.SuspendLayout();
            SuspendLayout();
            // 
            // tlpEmployees
            // 
            tlpEmployees.ColumnCount = 7;
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.40625F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.03125F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.421875F));
            tlpEmployees.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.71875F));
            tlpEmployees.Controls.Add(btnResetFilters, 6, 2);
            tlpEmployees.Controls.Add(btnDelete, 5, 1);
            tlpEmployees.Controls.Add(btnUpdate, 4, 1);
            tlpEmployees.Controls.Add(btnAdd, 3, 1);
            tlpEmployees.Controls.Add(txtFilter, 1, 1);
            tlpEmployees.Controls.Add(lblTitle, 1, 0);
            tlpEmployees.Controls.Add(dgvEmployees, 1, 2);
            tlpEmployees.Controls.Add(gbxFilters, 6, 3);
            tlpEmployees.Dock = DockStyle.Fill;
            tlpEmployees.Location = new Point(0, 0);
            tlpEmployees.Name = "tlpEmployees";
            tlpEmployees.RowCount = 7;
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 6.259542F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 7.93893147F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 28.0916023F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 29.7709923F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 6.87022924F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 18.6259537F));
            tlpEmployees.RowStyles.Add(new RowStyle(SizeType.Percent, 2.13740468F));
            tlpEmployees.Size = new Size(1280, 655);
            tlpEmployees.TabIndex = 2;
            // 
            // btnResetFilters
            // 
            btnResetFilters.Anchor = AnchorStyles.Bottom;
            btnResetFilters.Location = new Point(1109, 234);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(127, 40);
            btnResetFilters.TabIndex = 17;
            btnResetFilters.Values.DropDownArrowColor = Color.Empty;
            btnResetFilters.Values.Text = "Reiniciar Filtros";
            btnResetFilters.Click += btnResetFilters_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            btnDelete.Location = new Point(922, 47);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(127, 40);
            btnDelete.TabIndex = 12;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.None;
            btnUpdate.Location = new Point(752, 47);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(127, 40);
            btnUpdate.TabIndex = 11;
            btnUpdate.Values.DropDownArrowColor = Color.Empty;
            btnUpdate.Values.Text = "Editar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(570, 47);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.TabIndex = 10;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar nuevo";
            btnAdd.Click += btnAdd_Click;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(txtFilter, 2);
            txtFilter.CueHint.CueHintText = "Buscar";
            txtFilter.Location = new Point(108, 55);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(343, 23);
            txtFilter.TabIndex = 9;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpEmployees.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(226, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(107, 29);
            lblTitle.TabIndex = 7;
            lblTitle.Values.Text = "Empleados";
            // 
            // dgvEmployees
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvEmployees.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmployees.BorderStyle = BorderStyle.None;
            dgvEmployees.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpEmployees.SetColumnSpan(dgvEmployees, 5);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvEmployees.DefaultCellStyle = dataGridViewCellStyle3;
            dgvEmployees.Dock = DockStyle.Fill;
            dgvEmployees.EnableHeadersVisualStyles = false;
            dgvEmployees.Location = new Point(20, 96);
            dgvEmployees.MultiSelect = false;
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.ReadOnly = true;
            dgvEmployees.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvEmployees.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpEmployees.SetRowSpan(dgvEmployees, 4);
            dgvEmployees.RowTemplate.Height = 35;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.Size = new Size(1042, 540);
            dgvEmployees.TabIndex = 18;
            // 
            // gbxFilters
            // 
            gbxFilters.Controls.Add(tlpFilters);
            gbxFilters.Dock = DockStyle.Fill;
            gbxFilters.Location = new Point(1068, 280);
            gbxFilters.Name = "gbxFilters";
            gbxFilters.Size = new Size(209, 189);
            gbxFilters.TabIndex = 30;
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
            tlpFilters.Size = new Size(203, 167);
            tlpFilters.TabIndex = 15;
            // 
            // rbtDesDate
            // 
            rbtDesDate.Anchor = AnchorStyles.None;
            rbtDesDate.Checked = true;
            rbtDesDate.Location = new Point(39, 31);
            rbtDesDate.Name = "rbtDesDate";
            rbtDesDate.Size = new Size(124, 20);
            rbtDesDate.TabIndex = 11;
            rbtDesDate.Values.Text = "Ordenar por Fecha";
            rbtDesDate.CheckedChanged += rbtDesDate_CheckedChanged;
            // 
            // rbtDesName
            // 
            rbtDesName.Anchor = AnchorStyles.None;
            rbtDesName.Location = new Point(32, 115);
            rbtDesName.Name = "rbtDesName";
            rbtDesName.Size = new Size(138, 20);
            rbtDesName.TabIndex = 12;
            rbtDesName.Values.Text = "Ordenar por Nombre";
            rbtDesName.CheckedChanged += rbtDesName_CheckedChanged;
            // 
            // UserControlEmployees
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpEmployees);
            Name = "UserControlEmployees";
            Size = new Size(1280, 655);
            Load += UserControlEmployees_Load;
            tlpEmployees.ResumeLayout(false);
            tlpEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            gbxFilters.ResumeLayout(false);
            tlpFilters.ResumeLayout(false);
            tlpFilters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpEmployees;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnUpdate;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonButton btnResetFilters;
        private DataGridView dgvEmployees;
        private GroupBox gbxFilters;
        private TableLayoutPanel tlpFilters;
        private Krypton.Toolkit.KryptonRadioButton rbtDesDate;
        private Krypton.Toolkit.KryptonRadioButton rbtDesName;
    }
}
