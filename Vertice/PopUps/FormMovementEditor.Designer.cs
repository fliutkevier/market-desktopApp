namespace WinForms.PopUps
{
    partial class FormMovementEditor
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
            tlpMovementsEditor = new TableLayoutPanel();
            cbxProviders = new Krypton.Toolkit.KryptonComboBox();
            btnNewProvider = new Krypton.Toolkit.KryptonButton();
            txtTotal = new Krypton.Toolkit.KryptonTextBox();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            lblArchive = new Krypton.Toolkit.KryptonLabel();
            btnSelectArchive = new Krypton.Toolkit.KryptonButton();
            chkToday = new Krypton.Toolkit.KryptonCheckBox();
            dtpDate = new Krypton.Toolkit.KryptonDateTimePicker();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnAdd = new Krypton.Toolkit.KryptonButton();
            tlpMovementsEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cbxProviders).BeginInit();
            SuspendLayout();
            // 
            // tlpMovementsEditor
            // 
            tlpMovementsEditor.ColumnCount = 2;
            tlpMovementsEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMovementsEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMovementsEditor.Controls.Add(cbxProviders, 0, 1);
            tlpMovementsEditor.Controls.Add(btnNewProvider, 1, 1);
            tlpMovementsEditor.Controls.Add(txtTotal, 0, 2);
            tlpMovementsEditor.Controls.Add(lblTitle, 0, 0);
            tlpMovementsEditor.Controls.Add(lblArchive, 0, 3);
            tlpMovementsEditor.Controls.Add(btnSelectArchive, 1, 3);
            tlpMovementsEditor.Controls.Add(chkToday, 0, 4);
            tlpMovementsEditor.Controls.Add(dtpDate, 0, 5);
            tlpMovementsEditor.Controls.Add(btnCancel, 0, 6);
            tlpMovementsEditor.Controls.Add(btnAdd, 1, 6);
            tlpMovementsEditor.Dock = DockStyle.Fill;
            tlpMovementsEditor.Location = new Point(0, 0);
            tlpMovementsEditor.Name = "tlpMovementsEditor";
            tlpMovementsEditor.RowCount = 7;
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tlpMovementsEditor.Size = new Size(443, 450);
            tlpMovementsEditor.TabIndex = 0;
            // 
            // cbxProviders
            // 
            cbxProviders.Anchor = AnchorStyles.None;
            cbxProviders.DropDownWidth = 152;
            cbxProviders.Location = new Point(28, 85);
            cbxProviders.Name = "cbxProviders";
            cbxProviders.Size = new Size(164, 22);
            cbxProviders.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxProviders.TabIndex = 0;
            cbxProviders.Text = "PROVEEDOR";
            // 
            // btnNewProvider
            // 
            btnNewProvider.Anchor = AnchorStyles.None;
            btnNewProvider.Location = new Point(245, 76);
            btnNewProvider.Name = "btnNewProvider";
            btnNewProvider.Size = new Size(174, 40);
            btnNewProvider.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnNewProvider.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewProvider.TabIndex = 1;
            btnNewProvider.Values.DropDownArrowColor = Color.Empty;
            btnNewProvider.Values.Text = "PROVEEDOR NUEVO";
            btnNewProvider.Click += btnNewProvider_Click;
            // 
            // txtTotal
            // 
            txtTotal.Anchor = AnchorStyles.None;
            tlpMovementsEditor.SetColumnSpan(txtTotal, 2);
            txtTotal.CueHint.CueHintText = "$ Total Gastado";
            txtTotal.CueHint.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtTotal.CueHint.Padding = new Padding(0);
            txtTotal.CueHint.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            txtTotal.Location = new Point(87, 149);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(268, 22);
            txtTotal.StateActive.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtTotal.StateActive.Content.Padding = new Padding(0);
            txtTotal.StateCommon.Border.Color1 = Color.FromArgb(48, 159, 250);
            txtTotal.StateCommon.Border.Width = 2;
            txtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 11.75F, FontStyle.Bold);
            txtTotal.TabIndex = 2;
            txtTotal.TextAlign = HorizontalAlignment.Center;
            txtTotal.KeyPress += txtTotal_KeyPress;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpMovementsEditor.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(132, 17);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(179, 29);
            lblTitle.TabIndex = 2;
            lblTitle.Values.Text = "Agregar Proveedor";
            // 
            // lblArchive
            // 
            lblArchive.Anchor = AnchorStyles.Right;
            lblArchive.Location = new Point(147, 213);
            lblArchive.Name = "lblArchive";
            lblArchive.Size = new Size(71, 21);
            lblArchive.StateCommon.Padding = new Padding(-1, -1, 8, -1);
            lblArchive.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblArchive.TabIndex = 45;
            lblArchive.Values.Text = "Factura";
            // 
            // btnSelectArchive
            // 
            btnSelectArchive.Anchor = AnchorStyles.Left;
            btnSelectArchive.Location = new Point(224, 204);
            btnSelectArchive.Name = "btnSelectArchive";
            btnSelectArchive.Size = new Size(185, 40);
            btnSelectArchive.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSelectArchive.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelectArchive.TabIndex = 3;
            btnSelectArchive.Values.DropDownArrowColor = Color.Empty;
            btnSelectArchive.Values.Text = "Seleccionar Archivo";
            btnSelectArchive.Click += btnSelectArchive_Click;
            // 
            // chkToday
            // 
            chkToday.Anchor = AnchorStyles.None;
            chkToday.Checked = true;
            chkToday.CheckState = CheckState.Checked;
            tlpMovementsEditor.SetColumnSpan(chkToday, 2);
            chkToday.Location = new Point(151, 277);
            chkToday.Name = "chkToday";
            chkToday.Size = new Size(141, 21);
            chkToday.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkToday.TabIndex = 4;
            chkToday.Values.Text = "¿Fecha de Hoy?";
            chkToday.CheckedChanged += chkToday_CheckedChanged;
            // 
            // dtpDate
            // 
            dtpDate.Anchor = AnchorStyles.None;
            tlpMovementsEditor.SetColumnSpan(dtpDate, 2);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(163, 340);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(116, 23);
            dtpDate.StateActive.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            dtpDate.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpDate.TabIndex = 5;
            dtpDate.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(47, 397);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 7;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cancelar";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(268, 397);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 40);
            btnAdd.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnAdd.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnAdd.TabIndex = 6;
            btnAdd.Values.DropDownArrowColor = Color.Empty;
            btnAdd.Values.Text = "Agregar";
            btnAdd.Click += btnAdd_Click;
            // 
            // FormMovementEditor
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(443, 450);
            Controls.Add(tlpMovementsEditor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMovementEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMovementEditor";
            Load += FormMovementEditor_Load;
            tlpMovementsEditor.ResumeLayout(false);
            tlpMovementsEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cbxProviders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMovementsEditor;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonComboBox cbxProviders;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnNewProvider;
        private Krypton.Toolkit.KryptonButton btnSelectArchive;
        private Krypton.Toolkit.KryptonLabel lblArchive;
        private Krypton.Toolkit.KryptonTextBox txtTotal;
        private Krypton.Toolkit.KryptonCheckBox chkToday;
        private Krypton.Toolkit.KryptonDateTimePicker dtpDate;
    }
}