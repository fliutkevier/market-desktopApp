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
            lblUpdatesTitle = new Krypton.Toolkit.KryptonLabel();
            lblVersion = new Krypton.Toolkit.KryptonLabel();
            btnCheckUpdates = new Krypton.Toolkit.KryptonButton();
            lblUpdateStatus = new Label();
            pbUpdate = new ProgressBar();
            btnInstallUpdate = new Krypton.Toolkit.KryptonButton();
            btnPreviousVersions = new Krypton.Toolkit.KryptonButton();
            lblPinTitle = new Krypton.Toolkit.KryptonLabel();
            txtMobilePin = new Krypton.Toolkit.KryptonTextBox();
            btnGeneratePin = new Krypton.Toolkit.KryptonButton();
            btnSavePin = new Krypton.Toolkit.KryptonButton();
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
            tlpSettings.Controls.Add(lblUpdatesTitle, 3, 1);
            tlpSettings.Controls.Add(lblVersion, 3, 2);
            tlpSettings.Controls.Add(btnCheckUpdates, 3, 3);
            tlpSettings.Controls.Add(lblUpdateStatus, 3, 4);
            tlpSettings.Controls.Add(pbUpdate, 3, 5);
            tlpSettings.Controls.Add(btnInstallUpdate, 3, 6);
            tlpSettings.Controls.Add(btnPreviousVersions, 4, 6);
            tlpSettings.Controls.Add(lblPinTitle, 0, 6);
            tlpSettings.Controls.Add(txtMobilePin, 1, 6);
            tlpSettings.Controls.Add(btnGeneratePin, 0, 7);
            tlpSettings.Controls.Add(btnSavePin, 1, 7);
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
            btnPendingInvoices.Location = new Point(100, 182);
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
            // lblUpdatesTitle
            // 
            lblUpdatesTitle.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(lblUpdatesTitle, 2);
            lblUpdatesTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblUpdatesTitle.Location = new Point(927, 103);
            lblUpdatesTitle.Name = "lblUpdatesTitle";
            lblUpdatesTitle.Size = new Size(194, 36);
            lblUpdatesTitle.StateNormal.ShortText.Font = new Font("Microsoft Tai Le", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUpdatesTitle.TabIndex = 27;
            lblUpdatesTitle.Values.Text = "Actualizaciones";
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(lblVersion, 2);
            lblVersion.Location = new Point(943, 191);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(162, 23);
            lblVersion.StateNormal.Padding = new Padding(10, 1, 10, 1);
            lblVersion.StateNormal.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVersion.TabIndex = 28;
            lblVersion.Values.Text = "Versión instalada:";
            // 
            // btnCheckUpdates
            // 
            btnCheckUpdates.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(btnCheckUpdates, 2);
            btnCheckUpdates.Location = new Point(868, 263);
            btnCheckUpdates.Name = "btnCheckUpdates";
            btnCheckUpdates.Size = new Size(312, 40);
            btnCheckUpdates.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCheckUpdates.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCheckUpdates.TabIndex = 29;
            btnCheckUpdates.Values.DropDownArrowColor = Color.Empty;
            btnCheckUpdates.Values.Text = "BUSCAR ACTUALIZACIONES";
            btnCheckUpdates.Click += btnCheckUpdates_Click;
            // 
            // lblUpdateStatus
            // 
            tlpSettings.SetColumnSpan(lblUpdateStatus, 2);
            lblUpdateStatus.Dock = DockStyle.Fill;
            lblUpdateStatus.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUpdateStatus.ForeColor = Color.FromArgb(20, 25, 70);
            lblUpdateStatus.Location = new Point(771, 324);
            lblUpdateStatus.Name = "lblUpdateStatus";
            lblUpdateStatus.Size = new Size(506, 81);
            lblUpdateStatus.TabIndex = 30;
            lblUpdateStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbUpdate
            // 
            pbUpdate.Anchor = AnchorStyles.None;
            tlpSettings.SetColumnSpan(pbUpdate, 2);
            pbUpdate.Location = new Point(868, 432);
            pbUpdate.Name = "pbUpdate";
            pbUpdate.Size = new Size(312, 26);
            pbUpdate.TabIndex = 31;
            pbUpdate.Visible = false;
            // 
            // btnInstallUpdate
            // 
            btnInstallUpdate.Anchor = AnchorStyles.None;
            btnInstallUpdate.Location = new Point(806, 506);
            btnInstallUpdate.Name = "btnInstallUpdate";
            btnInstallUpdate.Size = new Size(180, 40);
            btnInstallUpdate.StateCommon.Back.Color1 = Color.FromArgb(178, 233, 178);
            btnInstallUpdate.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInstallUpdate.TabIndex = 32;
            btnInstallUpdate.Values.DropDownArrowColor = Color.Empty;
            btnInstallUpdate.Values.Text = "INSTALAR";
            btnInstallUpdate.Visible = false;
            btnInstallUpdate.Click += btnInstallUpdate_Click;
            // 
            // btnPreviousVersions
            // 
            btnPreviousVersions.Anchor = AnchorStyles.None;
            btnPreviousVersions.Location = new Point(1034, 506);
            btnPreviousVersions.Name = "btnPreviousVersions";
            btnPreviousVersions.Size = new Size(236, 40);
            btnPreviousVersions.StateCommon.Back.Color1 = Color.FromArgb(230, 233, 240);
            btnPreviousVersions.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPreviousVersions.TabIndex = 33;
            btnPreviousVersions.Values.DropDownArrowColor = Color.Empty;
            btnPreviousVersions.Values.Text = "VERSIONES ANTERIORES";
            btnPreviousVersions.Click += btnPreviousVersions_Click;
            //
            // lblPinTitle
            //
            lblPinTitle.Anchor = AnchorStyles.None;
            lblPinTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblPinTitle.Location = new Point(35, 506);
            lblPinTitle.Name = "lblPinTitle";
            lblPinTitle.Size = new Size(186, 36);
            lblPinTitle.StateNormal.ShortText.Font = new Font("Microsoft Tai Le", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPinTitle.TabIndex = 34;
            lblPinTitle.Values.Text = "PIN del celular";
            //
            // txtMobilePin
            //
            txtMobilePin.Anchor = AnchorStyles.Left;
            txtMobilePin.Location = new Point(259, 511);
            txtMobilePin.MaxLength = 12;
            txtMobilePin.Name = "txtMobilePin";
            txtMobilePin.Size = new Size(131, 27);
            txtMobilePin.StateCommon.Content.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMobilePin.TabIndex = 35;
            txtMobilePin.TextAlign = HorizontalAlignment.Center;
            //
            // btnGeneratePin
            //
            btnGeneratePin.Anchor = AnchorStyles.None;
            btnGeneratePin.Location = new Point(62, 587);
            btnGeneratePin.Name = "btnGeneratePin";
            btnGeneratePin.Size = new Size(131, 40);
            btnGeneratePin.StateCommon.Back.Color1 = Color.FromArgb(230, 233, 240);
            btnGeneratePin.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGeneratePin.TabIndex = 36;
            btnGeneratePin.Values.DropDownArrowColor = Color.Empty;
            btnGeneratePin.Values.Text = "GENERAR";
            btnGeneratePin.Click += btnGeneratePin_Click;
            //
            // btnSavePin
            //
            btnSavePin.Anchor = AnchorStyles.Left;
            btnSavePin.Location = new Point(259, 587);
            btnSavePin.Name = "btnSavePin";
            btnSavePin.Size = new Size(131, 40);
            btnSavePin.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSavePin.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSavePin.TabIndex = 37;
            btnSavePin.Values.DropDownArrowColor = Color.Empty;
            btnSavePin.Values.Text = "GUARDAR";
            btnSavePin.Click += btnSavePin_Click;
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
        private Krypton.Toolkit.KryptonLabel lblUpdatesTitle;
        private Krypton.Toolkit.KryptonLabel lblVersion;
        private Krypton.Toolkit.KryptonButton btnCheckUpdates;
        private Label lblUpdateStatus;
        private ProgressBar pbUpdate;
        private Krypton.Toolkit.KryptonButton btnInstallUpdate;
        private Krypton.Toolkit.KryptonButton btnPreviousVersions;
        private Krypton.Toolkit.KryptonLabel lblPinTitle;
        private Krypton.Toolkit.KryptonTextBox txtMobilePin;
        private Krypton.Toolkit.KryptonButton btnGeneratePin;
        private Krypton.Toolkit.KryptonButton btnSavePin;
    }
}
