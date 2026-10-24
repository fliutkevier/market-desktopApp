namespace WinForms.PopUps
{
    partial class FormRecoverElements
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
            tlpRecoverElements = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            cbxType = new Krypton.Toolkit.KryptonComboBox();
            dgvInactives = new DataGridView();
            btnRecover = new Krypton.Toolkit.KryptonButton();
            tlpRecoverElements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cbxType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactives).BeginInit();
            SuspendLayout();
            // 
            // tlpRecoverElements
            // 
            tlpRecoverElements.ColumnCount = 6;
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.923077F));
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0384617F));
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0384617F));
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0384617F));
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.0384617F));
            tlpRecoverElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.92307687F));
            tlpRecoverElements.Controls.Add(lblTitle, 2, 0);
            tlpRecoverElements.Controls.Add(btnCancel, 2, 6);
            tlpRecoverElements.Controls.Add(cbxType, 2, 1);
            tlpRecoverElements.Controls.Add(dgvInactives, 1, 2);
            tlpRecoverElements.Controls.Add(btnRecover, 4, 1);
            tlpRecoverElements.Dock = DockStyle.Fill;
            tlpRecoverElements.Location = new Point(0, 0);
            tlpRecoverElements.Name = "tlpRecoverElements";
            tlpRecoverElements.RowCount = 7;
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 6.929348F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 9.103261F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 25.543478F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 23.369566F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 10.597826F));
            tlpRecoverElements.RowStyles.Add(new RowStyle(SizeType.Percent, 9.918478F));
            tlpRecoverElements.Size = new Size(1200, 683);
            tlpRecoverElements.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpRecoverElements.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(505, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(187, 29);
            lblTitle.TabIndex = 8;
            lblTitle.Values.Text = "Recuperar Registros";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            tlpRecoverElements.SetColumnSpan(btnCancel, 2);
            btnCancel.Location = new Point(535, 627);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 11;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cerrar";
            btnCancel.Click += btnCancel_Click;
            // 
            // cbxType
            // 
            cbxType.Anchor = AnchorStyles.None;
            tlpRecoverElements.SetColumnSpan(cbxType, 2);
            cbxType.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            cbxType.DropDownWidth = 152;
            cbxType.Location = new Point(455, 67);
            cbxType.Name = "cbxType";
            cbxType.Size = new Size(287, 21);
            cbxType.StateCommon.ComboBox.Content.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            cbxType.TabIndex = 25;
            cbxType.Text = "TIPO";
            cbxType.SelectedIndexChanged += cbxType_SelectedIndexChanged;
            // 
            // dgvInactives
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(238, 239, 249);
            dgvInactives.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvInactives.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInactives.BorderStyle = BorderStyle.None;
            dgvInactives.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 25, 70);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvInactives.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvInactives.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpRecoverElements.SetColumnSpan(dgvInactives, 4);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvInactives.DefaultCellStyle = dataGridViewCellStyle3;
            dgvInactives.Dock = DockStyle.Fill;
            dgvInactives.EnableHeadersVisualStyles = false;
            dgvInactives.Location = new Point(26, 112);
            dgvInactives.MultiSelect = false;
            dgvInactives.Name = "dgvInactives";
            dgvInactives.ReadOnly = true;
            dgvInactives.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvInactives.RowsDefaultCellStyle = dataGridViewCellStyle4;
            tlpRecoverElements.SetRowSpan(dgvInactives, 4);
            dgvInactives.RowTemplate.Height = 35;
            dgvInactives.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactives.Size = new Size(1146, 497);
            dgvInactives.TabIndex = 30;
            // 
            // btnRecover
            // 
            btnRecover.Anchor = AnchorStyles.None;
            tlpRecoverElements.SetColumnSpan(btnRecover, 2);
            btnRecover.Location = new Point(980, 58);
            btnRecover.Name = "btnRecover";
            btnRecover.Size = new Size(127, 40);
            btnRecover.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnRecover.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRecover.TabIndex = 31;
            btnRecover.Values.DropDownArrowColor = Color.Empty;
            btnRecover.Values.Text = "Recuperar";
            btnRecover.Click += btnRecover_Click;
            // 
            // FormRecoverElements
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            CancelButton = btnCancel;
            ClientSize = new Size(1200, 683);
            Controls.Add(tlpRecoverElements);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormRecoverElements";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormRecoverElements";
            tlpRecoverElements.ResumeLayout(false);
            tlpRecoverElements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cbxType).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactives).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRecoverElements;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonComboBox cbxType;
        private DataGridView dgvInactives;
        private Krypton.Toolkit.KryptonButton btnRecover;
    }
}