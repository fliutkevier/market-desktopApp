namespace WinForms.AdminForms
{
    partial class UserControlSettings
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
            tlpSettings = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            btnRecicle = new Krypton.Toolkit.KryptonButton();
            btnPendingInvoices = new Krypton.Toolkit.KryptonButton();
            lblPathTitle = new Krypton.Toolkit.KryptonLabel();
            btnBrowsePath = new Krypton.Toolkit.KryptonButton();
            lblApiPath = new Krypton.Toolkit.KryptonLabel();
            cbxPrinters = new Krypton.Toolkit.KryptonComboBox();
            btnSavePrinter = new Krypton.Toolkit.KryptonButton();
            tlpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cbxPrinters).BeginInit();
            SuspendLayout();
            // 
            // tlpSettings
            // 
            tlpSettings.ColumnCount = 5;
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpSettings.Controls.Add(lblTitle, 0, 0);
            tlpSettings.Controls.Add(btnRecicle, 0, 1);
            tlpSettings.Controls.Add(btnPendingInvoices, 0, 2);
            tlpSettings.Controls.Add(lblPathTitle, 0, 3);
            tlpSettings.Controls.Add(btnBrowsePath, 1, 3);
            tlpSettings.Controls.Add(lblApiPath, 0, 4);
            tlpSettings.Controls.Add(cbxPrinters, 0, 5);
            tlpSettings.Controls.Add(btnSavePrinter, 1, 5);
            tlpSettings.Dock = DockStyle.Fill;
            tlpSettings.Location = new Point(0, 0);
            tlpSettings.Name = "tlpSettings";
            tlpSettings.RowCount = 8;
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.Size = new Size(1280, 655);
            tlpSettings.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(90, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(76, 29);
            lblTitle.TabIndex = 8;
            lblTitle.Values.Text = "Ajustes";
            // 
            // btnRecicle
            // 
            btnRecicle.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(btnRecicle, 2);
            btnRecicle.Location = new Point(100, 101);
            btnRecicle.Name = "btnRecicle";
            btnRecicle.Size = new Size(312, 40);
            btnRecicle.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnRecicle.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRecicle.TabIndex = 9;
            btnRecicle.Values.DropDownArrowColor = Color.Empty;
            btnRecicle.Values.Text = "PAPELERA DE RECICLAJE";
            btnRecicle.Click += btnRecicle_Click;
            //
            // btnPendingInvoices
            //
            btnPendingInvoices.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(btnPendingInvoices, 2);
            btnPendingInvoices.Location = new Point(100, 183);
            btnPendingInvoices.Name = "btnPendingInvoices";
            btnPendingInvoices.Size = new Size(312, 40);
            btnPendingInvoices.StateCommon.Back.Color1 = Color.FromArgb(255, 214, 153);
            btnPendingInvoices.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPendingInvoices.TabIndex = 10;
            btnPendingInvoices.Values.DropDownArrowColor = Color.Empty;
            btnPendingInvoices.Values.Text = "FACTURACIÓN PENDIENTE (ARCA)";
            btnPendingInvoices.Click += btnPendingInvoices_Click;
            //
            // lblPathTitle
            // 
            lblPathTitle.Anchor = AnchorStyles.None;
            lblPathTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblPathTitle.Location = new Point(25, 265);
            lblPathTitle.Name = "lblPathTitle";
            lblPathTitle.Size = new Size(206, 36);
            lblPathTitle.StateNormal.ShortText.Font = new Font("Microsoft Tai Le", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPathTitle.TabIndex = 11;
            lblPathTitle.Values.Text = "Ruta de servidor";
            // 
            // btnBrowsePath
            // 
            btnBrowsePath.Anchor = AnchorStyles.Left;
            btnBrowsePath.Location = new Point(259, 263);
            btnBrowsePath.Name = "btnBrowsePath";
            btnBrowsePath.Size = new Size(131, 40);
            btnBrowsePath.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnBrowsePath.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBrowsePath.TabIndex = 12;
            btnBrowsePath.Values.DropDownArrowColor = Color.Empty;
            btnBrowsePath.Values.Text = "EXAMINAR";
            btnBrowsePath.Click += btnBrowsePath_Click;
            // 
            // lblApiPath
            // 
            lblApiPath.Anchor = AnchorStyles.Top;
            tlpSettings.SetColumnSpan(lblApiPath, 2);
            lblApiPath.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblApiPath.Location = new Point(237, 327);
            lblApiPath.Name = "lblApiPath";
            lblApiPath.Size = new Size(38, 21);
            lblApiPath.StateCommon.LongText.Color1 = Color.FromArgb(128, 255, 128);
            lblApiPath.StateNormal.ShortText.Color1 = Color.FromArgb(0, 192, 0);
            lblApiPath.StateNormal.ShortText.Font = new Font("Microsoft Tai Le", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblApiPath.TabIndex = 13;
            lblApiPath.Values.Text = "path";
            // 
            // cbxPrinters
            // 
            cbxPrinters.Anchor = AnchorStyles.None;
            cbxPrinters.DropDownWidth = 152;
            cbxPrinters.Location = new Point(46, 434);
            cbxPrinters.Name = "cbxPrinters";
            cbxPrinters.Size = new Size(164, 22);
            cbxPrinters.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cbxPrinters.TabIndex = 25;
            cbxPrinters.Text = "IMPRESORAS";
            // 
            // btnSavePrinter
            // 
            btnSavePrinter.Anchor = AnchorStyles.Left;
            btnSavePrinter.Location = new Point(259, 425);
            btnSavePrinter.Name = "btnSavePrinter";
            btnSavePrinter.Size = new Size(131, 40);
            btnSavePrinter.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSavePrinter.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSavePrinter.TabIndex = 26;
            btnSavePrinter.Values.DropDownArrowColor = Color.Empty;
            btnSavePrinter.Values.Text = "GUARDAR";
            btnSavePrinter.Click += btnSavePrinter_Click;
            // 
            // UserControlSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpSettings);
            Name = "UserControlSettings";
            Size = new Size(1280, 655);
            Load += UserControlSettings_Load;
            tlpSettings.ResumeLayout(false);
            tlpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cbxPrinters).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpSettings;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonButton btnRecicle;
        private Krypton.Toolkit.KryptonButton btnPendingInvoices;
        private Krypton.Toolkit.KryptonLabel lblPathTitle;
        private Krypton.Toolkit.KryptonButton btnBrowsePath;
        private Krypton.Toolkit.KryptonLabel lblApiPath;
        private Krypton.Toolkit.KryptonComboBox cbxPrinters;
        private Krypton.Toolkit.KryptonButton btnSavePrinter;
    }
}
