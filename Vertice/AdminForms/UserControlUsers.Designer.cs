namespace WinForms.AdminForms
{
    partial class UserControlUsers
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
            tlpUsers = new TableLayoutPanel();
            dgvUsers = new DataGridView();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            txtFilter = new Krypton.Toolkit.KryptonTextBox();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            btnUpdatePassword = new Krypton.Toolkit.KryptonButton();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            tlpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // tlpUsers
            // 
            tlpUsers.ColumnCount = 9;
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.171875F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.40625F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.703125F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            tlpUsers.Controls.Add(dgvUsers, 1, 2);
            tlpUsers.Controls.Add(lblTitle, 1, 0);
            tlpUsers.Controls.Add(txtFilter, 1, 1);
            tlpUsers.Controls.Add(btnAdd, 3, 1);
            tlpUsers.Controls.Add(btnUpdatePassword, 4, 1);
            tlpUsers.Controls.Add(btnDelete, 5, 1);
            tlpUsers.Dock = DockStyle.Fill;
            tlpUsers.Location = new Point(0, 0);
            tlpUsers.Name = "tlpUsers";
            tlpUsers.RowCount = 12;
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 14.6564884F));
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 2.90076327F));
            tlpUsers.Size = new Size(1280, 655);
            tlpUsers.TabIndex = 0;
            // 
            // dgvUsers
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpUsers.SetColumnSpan(dgvUsers, 5);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvUsers.DefaultCellStyle = dataGridViewCellStyle3;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.Location = new Point(18, 111);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvUsers.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpUsers.SetRowSpan(dgvUsers, 9);
            dgvUsers.RowTemplate.Height = 35;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(831, 521);
            dgvUsers.TabIndex = 9;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpUsers.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(177, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(87, 29);
            lblTitle.TabIndex = 10;
            lblTitle.Values.Text = "Usuarios";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.None;
            tlpUsers.SetColumnSpan(txtFilter, 2);
            txtFilter.CueHint.CueHintText = "Buscar";
            txtFilter.Location = new Point(49, 69);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(343, 23);
            txtFilter.TabIndex = 11;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(433, 61);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.TabIndex = 20;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar Usuario";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdatePassword
            // 
            btnUpdatePassword.Anchor = AnchorStyles.None;
            btnUpdatePassword.Location = new Point(575, 61);
            btnUpdatePassword.Name = "btnUpdatePassword";
            btnUpdatePassword.Size = new Size(127, 40);
            btnUpdatePassword.TabIndex = 28;
            btnUpdatePassword.Values.DropDownArrowColor = Color.Empty;
            btnUpdatePassword.Values.Text = "Editar Contraseña";
            btnUpdatePassword.Click += btnUpdatePassword_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            btnDelete.Location = new Point(719, 61);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(123, 40);
            btnDelete.TabIndex = 29;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // UserControlUsers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpUsers);
            Name = "UserControlUsers";
            Size = new Size(1280, 655);
            Load += UserControlUsers_Load;
            tlpUsers.ResumeLayout(false);
            tlpUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpUsers;
        private DataGridView dgvUsers;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonTextBox txtFilter;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnUpdatePassword;
        private Krypton.Toolkit.KryptonButton btnDelete;
    }
}
