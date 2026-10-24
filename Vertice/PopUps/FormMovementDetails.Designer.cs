namespace WinForms.PopUps
{
    partial class FormMovementDetails
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
            tlpMovementDetails = new TableLayoutPanel();
            lblTitle = new Krypton.Toolkit.KryptonLabel();
            lblProviderTitle = new Krypton.Toolkit.KryptonLabel();
            lblCuitTitle = new Krypton.Toolkit.KryptonLabel();
            lblDateTitle = new Krypton.Toolkit.KryptonLabel();
            lblNoFile = new Krypton.Toolkit.KryptonLabel();
            lblTotalTitle = new Krypton.Toolkit.KryptonLabel();
            pbxDetail = new PictureBox();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            btnShow = new Krypton.Toolkit.KryptonButton();
            lblProvider = new Krypton.Toolkit.KryptonLabel();
            lblCuit = new Krypton.Toolkit.KryptonLabel();
            lblDate = new Krypton.Toolkit.KryptonLabel();
            lblTotal = new Krypton.Toolkit.KryptonLabel();
            tlpMovementDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxDetail).BeginInit();
            SuspendLayout();
            // 
            // tlpMovementDetails
            // 
            tlpMovementDetails.ColumnCount = 3;
            tlpMovementDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.48088F));
            tlpMovementDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.42474F));
            tlpMovementDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.09437323F));
            tlpMovementDetails.Controls.Add(lblTitle, 0, 0);
            tlpMovementDetails.Controls.Add(lblProviderTitle, 0, 1);
            tlpMovementDetails.Controls.Add(pbxDetail, 1, 1);
            tlpMovementDetails.Controls.Add(lblCuitTitle, 0, 3);
            tlpMovementDetails.Controls.Add(lblDateTitle, 0, 5);
            tlpMovementDetails.Controls.Add(lblTotalTitle, 0, 7);
            tlpMovementDetails.Controls.Add(lblNoFile, 0, 9);
            tlpMovementDetails.Controls.Add(btnShow, 1, 10);
            tlpMovementDetails.Controls.Add(btnCancel, 0, 10);
            tlpMovementDetails.Controls.Add(lblProvider, 0, 2);
            tlpMovementDetails.Controls.Add(lblCuit, 0, 4);
            tlpMovementDetails.Controls.Add(lblDate, 0, 6);
            tlpMovementDetails.Controls.Add(lblTotal, 0, 8);
            tlpMovementDetails.Dock = DockStyle.Fill;
            tlpMovementDetails.Location = new Point(0, 0);
            tlpMovementDetails.Name = "tlpMovementDetails";
            tlpMovementDetails.RowCount = 11;
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tlpMovementDetails.Size = new Size(1100, 700);
            tlpMovementDetails.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            tlpMovementDetails.SetColumnSpan(lblTitle, 2);
            lblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTitle.Location = new Point(446, 3);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(184, 57);
            lblTitle.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.TabIndex = 32;
            lblTitle.Values.Text = "Detalle";
            // 
            // lblProviderTitle
            // 
            lblProviderTitle.Anchor = AnchorStyles.None;
            lblProviderTitle.Location = new Point(93, 80);
            lblProviderTitle.Name = "lblProviderTitle";
            lblProviderTitle.Size = new Size(138, 29);
            lblProviderTitle.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblProviderTitle.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProviderTitle.TabIndex = 11;
            lblProviderTitle.Values.Text = "Proveedor: ";
            // 
            // lblCuitTitle
            // 
            lblCuitTitle.Anchor = AnchorStyles.None;
            lblCuitTitle.Location = new Point(119, 206);
            lblCuitTitle.Name = "lblCuitTitle";
            lblCuitTitle.Size = new Size(86, 29);
            lblCuitTitle.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblCuitTitle.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            lblCuitTitle.TabIndex = 13;
            lblCuitTitle.Values.Text = "CUIT: ";
            // 
            // lblDateTitle
            // 
            lblDateTitle.Anchor = AnchorStyles.None;
            lblDateTitle.Location = new Point(114, 332);
            lblDateTitle.Name = "lblDateTitle";
            lblDateTitle.Size = new Size(96, 29);
            lblDateTitle.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblDateTitle.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            lblDateTitle.TabIndex = 14;
            lblDateTitle.Values.Text = "Fecha: ";
            // 
            // lblNoFile
            // 
            lblNoFile.Anchor = AnchorStyles.None;
            lblNoFile.Location = new Point(24, 587);
            lblNoFile.Name = "lblNoFile";
            lblNoFile.Size = new Size(275, 23);
            lblNoFile.StateCommon.Padding = new Padding(-1, -1, 10, -1);
            lblNoFile.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblNoFile.TabIndex = 16;
            lblNoFile.Values.Text = "NO EXISTE ARCHIVO ADJUNTO";
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.Anchor = AnchorStyles.None;
            lblTotalTitle.Location = new Point(120, 458);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(84, 29);
            lblTotalTitle.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblTotalTitle.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            lblTotalTitle.TabIndex = 12;
            lblTotalTitle.Values.Text = "Total: ";
            // 
            // pbxDetail
            // 
            pbxDetail.Dock = DockStyle.Fill;
            pbxDetail.Location = new Point(327, 66);
            pbxDetail.Name = "pbxDetail";
            tlpMovementDetails.SetRowSpan(pbxDetail, 9);
            pbxDetail.Size = new Size(746, 561);
            pbxDetail.SizeMode = PictureBoxSizeMode.Zoom;
            pbxDetail.TabIndex = 15;
            pbxDetail.TabStop = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(98, 645);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 40);
            btnCancel.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnCancel.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnCancel.TabIndex = 10;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Cerrar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnShow
            // 
            btnShow.Anchor = AnchorStyles.None;
            btnShow.Location = new Point(636, 645);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(127, 40);
            btnShow.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnShow.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold | FontStyle.Italic);
            btnShow.TabIndex = 17;
            btnShow.Values.DropDownArrowColor = Color.Empty;
            btnShow.Values.Text = "ABRIR PDF";
            btnShow.Click += btnShow_Click;
            // 
            // lblProvider
            // 
            lblProvider.Anchor = AnchorStyles.Top;
            lblProvider.Location = new Point(130, 129);
            lblProvider.Name = "lblProvider";
            lblProvider.Size = new Size(64, 23);
            lblProvider.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblProvider.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblProvider.TabIndex = 33;
            lblProvider.Values.Text = "vacío";
            // 
            // lblCuit
            // 
            lblCuit.Anchor = AnchorStyles.Top;
            lblCuit.Location = new Point(130, 255);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(64, 23);
            lblCuit.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblCuit.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblCuit.TabIndex = 34;
            lblCuit.Values.Text = "vacío";
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top;
            lblDate.Location = new Point(130, 381);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(64, 23);
            lblDate.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblDate.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblDate.TabIndex = 35;
            lblDate.Values.Text = "vacío";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top;
            lblTotal.Location = new Point(130, 507);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(64, 23);
            lblTotal.StateCommon.Padding = new Padding(10, -1, 10, -1);
            lblTotal.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTotal.TabIndex = 36;
            lblTotal.Values.Text = "vacío";
            // 
            // FormMovementDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1100, 700);
            Controls.Add(tlpMovementDetails);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMovementDetails";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Detalles";
            tlpMovementDetails.ResumeLayout(false);
            tlpMovementDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxDetail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMovementDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonLabel lblProviderTitle;
        private Krypton.Toolkit.KryptonLabel lblCuitTitle;
        private Krypton.Toolkit.KryptonLabel lblTotalTitle;
        private Krypton.Toolkit.KryptonLabel lblDateTitle;
        private Krypton.Toolkit.KryptonLabel lblNoFile;
        private PictureBox pbxDetail;
        private Krypton.Toolkit.KryptonButton btnShow;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private Krypton.Toolkit.KryptonLabel lblProvider;
        private Krypton.Toolkit.KryptonLabel lblCuit;
        private Krypton.Toolkit.KryptonLabel lblDate;
        private Krypton.Toolkit.KryptonLabel lblTotal;
    }
}