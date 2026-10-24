namespace WinForms.AdminForms
{
    partial class FormAdminDashboard
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
            components = new System.ComponentModel.Container();
            pnlNavbar = new Panel();
            tlpNavbar = new TableLayoutPanel();
            btnInitSession = new Krypton.Toolkit.KryptonButton();
            btnLogOutNavbar = new Krypton.Toolkit.KryptonButton();
            btnHistoryNavbar = new Krypton.Toolkit.KryptonButton();
            btnProductsNavbar = new Krypton.Toolkit.KryptonButton();
            btnSellNavbar = new Krypton.Toolkit.KryptonButton();
            btnHome = new Krypton.Toolkit.KryptonButton();
            pnlFooter = new Panel();
            tlpFooter = new TableLayoutPanel();
            lblTime = new Krypton.Toolkit.KryptonLabel();
            lblUserFooter = new Krypton.Toolkit.KryptonLabel();
            pnlContent = new Panel();
            pnlMenu = new Panel();
            tlpMenu = new TableLayoutPanel();
            btnLogOutMenu = new Krypton.Toolkit.KryptonButton();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnInitSellMenu = new Krypton.Toolkit.KryptonButton();
            btnProductsMenu = new Krypton.Toolkit.KryptonButton();
            btnSalesHistory = new Krypton.Toolkit.KryptonButton();
            btnEmployeesMenu = new Krypton.Toolkit.KryptonButton();
            btnPurchaseHistory = new Krypton.Toolkit.KryptonButton();
            btnPaymentMethodsMenu = new Krypton.Toolkit.KryptonButton();
            btnProvidersMenu = new Krypton.Toolkit.KryptonButton();
            btnLogsMenu = new Krypton.Toolkit.KryptonButton();
            btnUsersMenu = new Krypton.Toolkit.KryptonButton();
            btnSettings = new Krypton.Toolkit.KryptonButton();
            lblUsername = new Krypton.Toolkit.KryptonLabel();
            clockTimer = new System.Windows.Forms.Timer(components);
            pnlNavbar.SuspendLayout();
            tlpNavbar.SuspendLayout();
            pnlFooter.SuspendLayout();
            tlpFooter.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlMenu.SuspendLayout();
            tlpMenu.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pnlNavbar
            // 
            pnlNavbar.Controls.Add(tlpNavbar);
            pnlNavbar.Dock = DockStyle.Top;
            pnlNavbar.Location = new Point(0, 0);
            pnlNavbar.Name = "pnlNavbar";
            pnlNavbar.Size = new Size(1280, 40);
            pnlNavbar.TabIndex = 0;
            // 
            // tlpNavbar
            // 
            tlpNavbar.BackColor = Color.DarkGray;
            tlpNavbar.ColumnCount = 6;
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpNavbar.Controls.Add(btnInitSession, 4, 0);
            tlpNavbar.Controls.Add(btnLogOutNavbar, 5, 0);
            tlpNavbar.Controls.Add(btnHistoryNavbar, 3, 0);
            tlpNavbar.Controls.Add(btnProductsNavbar, 2, 0);
            tlpNavbar.Controls.Add(btnSellNavbar, 1, 0);
            tlpNavbar.Controls.Add(btnHome, 0, 0);
            tlpNavbar.Dock = DockStyle.Fill;
            tlpNavbar.Location = new Point(0, 0);
            tlpNavbar.Name = "tlpNavbar";
            tlpNavbar.RowCount = 1;
            tlpNavbar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNavbar.Size = new Size(1280, 40);
            tlpNavbar.TabIndex = 0;
            // 
            // btnInitSession
            // 
            btnInitSession.Anchor = AnchorStyles.None;
            btnInitSession.Location = new Point(886, 5);
            btnInitSession.Name = "btnInitSession";
            btnInitSession.Size = new Size(145, 30);
            btnInitSession.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnInitSession.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnInitSession.TabIndex = 41;
            btnInitSession.Values.DropDownArrowColor = Color.Empty;
            btnInitSession.Values.Text = "ABRIR CAJA [F4]";
            btnInitSession.Click += btnInitSession_Click;
            // 
            // btnLogOutNavbar
            // 
            btnLogOutNavbar.Anchor = AnchorStyles.None;
            btnLogOutNavbar.Location = new Point(1083, 5);
            btnLogOutNavbar.Name = "btnLogOutNavbar";
            btnLogOutNavbar.Size = new Size(179, 30);
            btnLogOutNavbar.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnLogOutNavbar.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogOutNavbar.TabIndex = 40;
            btnLogOutNavbar.Values.DropDownArrowColor = Color.Empty;
            btnLogOutNavbar.Values.Text = "CERRAR SESIÓN [F10]";
            btnLogOutNavbar.Click += btnLogOutNavbar_Click;
            // 
            // btnHistoryNavbar
            // 
            btnHistoryNavbar.Anchor = AnchorStyles.None;
            btnHistoryNavbar.Location = new Point(677, 5);
            btnHistoryNavbar.Name = "btnHistoryNavbar";
            btnHistoryNavbar.Size = new Size(136, 30);
            btnHistoryNavbar.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnHistoryNavbar.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistoryNavbar.TabIndex = 40;
            btnHistoryNavbar.Values.DropDownArrowColor = Color.Empty;
            btnHistoryNavbar.Values.Text = "HISTORIAL [F3]";
            btnHistoryNavbar.Click += btnHistoryNavbar_Click;
            // 
            // btnProductsNavbar
            // 
            btnProductsNavbar.Anchor = AnchorStyles.None;
            btnProductsNavbar.Location = new Point(457, 5);
            btnProductsNavbar.Name = "btnProductsNavbar";
            btnProductsNavbar.Size = new Size(150, 30);
            btnProductsNavbar.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnProductsNavbar.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProductsNavbar.TabIndex = 40;
            btnProductsNavbar.Values.DropDownArrowColor = Color.Empty;
            btnProductsNavbar.Values.Text = "PRODUCTOS [F2]";
            btnProductsNavbar.Click += btnProductsNavbar_Click;
            // 
            // btnSellNavbar
            // 
            btnSellNavbar.Anchor = AnchorStyles.None;
            btnSellNavbar.Location = new Point(256, 5);
            btnSellNavbar.Name = "btnSellNavbar";
            btnSellNavbar.Size = new Size(127, 30);
            btnSellNavbar.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSellNavbar.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSellNavbar.TabIndex = 40;
            btnSellNavbar.Values.DropDownArrowColor = Color.Empty;
            btnSellNavbar.Values.Text = "VENDER [F1]";
            btnSellNavbar.Click += btnSellNavbar_Click;
            // 
            // btnHome
            // 
            btnHome.Anchor = AnchorStyles.None;
            btnHome.Location = new Point(61, 5);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(90, 30);
            btnHome.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnHome.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHome.TabIndex = 39;
            btnHome.Values.DropDownArrowColor = Color.Empty;
            btnHome.Values.Text = "INICIO";
            btnHome.Click += btnHome_Click_1;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(tlpFooter);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 695);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1280, 25);
            pnlFooter.TabIndex = 2;
            // 
            // tlpFooter
            // 
            tlpFooter.ColumnCount = 3;
            tlpFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.90625F));
            tlpFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.09375F));
            tlpFooter.Controls.Add(lblTime, 2, 0);
            tlpFooter.Controls.Add(lblUserFooter, 0, 0);
            tlpFooter.Dock = DockStyle.Fill;
            tlpFooter.Location = new Point(0, 0);
            tlpFooter.Name = "tlpFooter";
            tlpFooter.RowCount = 1;
            tlpFooter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFooter.Size = new Size(1280, 25);
            tlpFooter.TabIndex = 0;
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Top;
            lblTime.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblTime.Location = new Point(1174, 3);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(6, 2);
            lblTime.TabIndex = 12;
            lblTime.Values.Text = "";
            // 
            // lblUserFooter
            // 
            lblUserFooter.Anchor = AnchorStyles.None;
            lblUserFooter.Location = new Point(159, 3);
            lblUserFooter.Name = "lblUserFooter";
            lblUserFooter.Size = new Size(66, 19);
            lblUserFooter.StateCommon.Padding = new Padding(1, 1, 10, 1);
            lblUserFooter.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserFooter.TabIndex = 13;
            lblUserFooter.Values.Text = "usuario:";
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(pnlMenu);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 40);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1280, 655);
            pnlContent.TabIndex = 3;
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(tlpMenu);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(1280, 655);
            pnlMenu.TabIndex = 0;
            // 
            // tlpMenu
            // 
            tlpMenu.BackColor = SystemColors.ActiveBorder;
            tlpMenu.ColumnCount = 1;
            tlpMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMenu.Controls.Add(btnLogOutMenu, 0, 2);
            tlpMenu.Controls.Add(tableLayoutPanel2, 0, 1);
            tlpMenu.Controls.Add(lblUsername, 0, 0);
            tlpMenu.Dock = DockStyle.Fill;
            tlpMenu.Location = new Point(0, 0);
            tlpMenu.Name = "tlpMenu";
            tlpMenu.RowCount = 3;
            tlpMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 7.633588F));
            tlpMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 85.64886F));
            tlpMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 6.87022924F));
            tlpMenu.Size = new Size(1280, 655);
            tlpMenu.TabIndex = 0;
            // 
            // btnLogOutMenu
            // 
            btnLogOutMenu.Anchor = AnchorStyles.None;
            btnLogOutMenu.Location = new Point(561, 617);
            btnLogOutMenu.Name = "btnLogOutMenu";
            btnLogOutMenu.Size = new Size(157, 30);
            btnLogOutMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnLogOutMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogOutMenu.TabIndex = 40;
            btnLogOutMenu.Values.DropDownArrowColor = Color.Empty;
            btnLogOutMenu.Values.Text = "CERRAR SESIÓN";
            btnLogOutMenu.Click += btnLogOutMenu_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.Controls.Add(btnInitSellMenu, 0, 0);
            tableLayoutPanel2.Controls.Add(btnProductsMenu, 1, 0);
            tableLayoutPanel2.Controls.Add(btnSalesHistory, 2, 0);
            tableLayoutPanel2.Controls.Add(btnLogsMenu, 2, 2);
            tableLayoutPanel2.Controls.Add(btnUsersMenu, 2, 1);
            tableLayoutPanel2.Controls.Add(btnSettings, 1, 3);
            tableLayoutPanel2.Controls.Add(btnPaymentMethodsMenu, 0, 2);
            tableLayoutPanel2.Controls.Add(btnProvidersMenu, 0, 1);
            tableLayoutPanel2.Controls.Add(btnEmployeesMenu, 1, 2);
            tableLayoutPanel2.Controls.Add(btnPurchaseHistory, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 52);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 31.0713425F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 31.0713425F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 31.0713425F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 6.785981F));
            tableLayoutPanel2.Size = new Size(1274, 554);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // btnInitSellMenu
            // 
            btnInitSellMenu.Anchor = AnchorStyles.None;
            btnInitSellMenu.Location = new Point(52, 10);
            btnInitSellMenu.Name = "btnInitSellMenu";
            btnInitSellMenu.Size = new Size(320, 151);
            btnInitSellMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnInitSellMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInitSellMenu.TabIndex = 42;
            btnInitSellMenu.Values.DropDownArrowColor = Color.Empty;
            btnInitSellMenu.Values.Text = "CARGAR VENTAS";
            btnInitSellMenu.Click += btnInitSellMenu_Click_1;
            // 
            // btnProductsMenu
            // 
            btnProductsMenu.Anchor = AnchorStyles.None;
            btnProductsMenu.Location = new Point(476, 10);
            btnProductsMenu.Name = "btnProductsMenu";
            btnProductsMenu.Size = new Size(320, 151);
            btnProductsMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnProductsMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProductsMenu.TabIndex = 43;
            btnProductsMenu.Values.DropDownArrowColor = Color.Empty;
            btnProductsMenu.Values.Text = "PRODUCTOS";
            btnProductsMenu.Click += btnProductsMenu_Click_1;
            // 
            // btnSalesHistory
            // 
            btnSalesHistory.Anchor = AnchorStyles.None;
            btnSalesHistory.Location = new Point(901, 10);
            btnSalesHistory.Name = "btnSalesHistory";
            btnSalesHistory.Size = new Size(320, 151);
            btnSalesHistory.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSalesHistory.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalesHistory.TabIndex = 44;
            btnSalesHistory.Values.DropDownArrowColor = Color.Empty;
            btnSalesHistory.Values.Text = "HISTORIAL DE VENTAS";
            btnSalesHistory.Click += btnSalesHistory_Click_1;
            // 
            // btnEmployeesMenu
            // 
            btnEmployeesMenu.Anchor = AnchorStyles.None;
            btnEmployeesMenu.Location = new Point(476, 354);
            btnEmployeesMenu.Name = "btnEmployeesMenu";
            btnEmployeesMenu.Size = new Size(320, 151);
            btnEmployeesMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnEmployeesMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEmployeesMenu.TabIndex = 46;
            btnEmployeesMenu.Values.DropDownArrowColor = Color.Empty;
            btnEmployeesMenu.Values.Text = "EMPLEADOS";
            btnEmployeesMenu.Click += btnEmployeesMenu_Click_1;
            // 
            // btnPurchaseHistory
            // 
            btnPurchaseHistory.Anchor = AnchorStyles.None;
            btnPurchaseHistory.Location = new Point(476, 182);
            btnPurchaseHistory.Name = "btnPurchaseHistory";
            btnPurchaseHistory.Size = new Size(320, 151);
            btnPurchaseHistory.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnPurchaseHistory.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPurchaseHistory.TabIndex = 49;
            btnPurchaseHistory.Values.DropDownArrowColor = Color.Empty;
            btnPurchaseHistory.Values.Text = "HISTORIAL DE COMPRAS";
            btnPurchaseHistory.Click += btnPurchaseHistory_Click_1;
            // 
            // btnPaymentMethodsMenu
            // 
            btnPaymentMethodsMenu.Anchor = AnchorStyles.None;
            btnPaymentMethodsMenu.Location = new Point(52, 354);
            btnPaymentMethodsMenu.Name = "btnPaymentMethodsMenu";
            btnPaymentMethodsMenu.Size = new Size(320, 151);
            btnPaymentMethodsMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnPaymentMethodsMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPaymentMethodsMenu.TabIndex = 50;
            btnPaymentMethodsMenu.Values.DropDownArrowColor = Color.Empty;
            btnPaymentMethodsMenu.Values.Text = "MÉTODOS DE PAGO";
            btnPaymentMethodsMenu.Click += btnPaymentMethodsMenu_Click;
            // 
            // btnProvidersMenu
            // 
            btnProvidersMenu.Anchor = AnchorStyles.None;
            btnProvidersMenu.Location = new Point(52, 182);
            btnProvidersMenu.Name = "btnProvidersMenu";
            btnProvidersMenu.Size = new Size(320, 151);
            btnProvidersMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnProvidersMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProvidersMenu.TabIndex = 45;
            btnProvidersMenu.Values.DropDownArrowColor = Color.Empty;
            btnProvidersMenu.Values.Text = "PROVEEDORES";
            btnProvidersMenu.Click += btnProvidersMenu_Click_1;
            // 
            // btnLogsMenu
            // 
            btnLogsMenu.Anchor = AnchorStyles.None;
            btnLogsMenu.Location = new Point(901, 354);
            btnLogsMenu.Name = "btnLogsMenu";
            btnLogsMenu.Size = new Size(320, 151);
            btnLogsMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnLogsMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogsMenu.TabIndex = 47;
            btnLogsMenu.Values.DropDownArrowColor = Color.Empty;
            btnLogsMenu.Values.Text = "REGISTROS";
            btnLogsMenu.Click += btnLogsMenu_Click_1;
            // 
            // btnUsersMenu
            // 
            btnUsersMenu.Anchor = AnchorStyles.None;
            btnUsersMenu.Location = new Point(901, 182);
            btnUsersMenu.Name = "btnUsersMenu";
            btnUsersMenu.Size = new Size(320, 151);
            btnUsersMenu.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnUsersMenu.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUsersMenu.TabIndex = 51;
            btnUsersMenu.Values.DropDownArrowColor = Color.Empty;
            btnUsersMenu.Values.Text = "USUARIOS";
            btnUsersMenu.Click += btnUsersMenu_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.None;
            btnSettings.Location = new Point(557, 520);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(157, 30);
            btnSettings.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btnSettings.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSettings.TabIndex = 52;
            btnSettings.Values.DropDownArrowColor = Color.Empty;
            btnSettings.Values.Text = "AJUSTES";
            btnSettings.Click += btnSettings_Click;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Top;
            lblUsername.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            lblUsername.Location = new Point(598, 3);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(84, 29);
            lblUsername.TabIndex = 11;
            lblUsername.Values.Text = "Usuario: ";
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += clockTimer_Tick;
            // 
            // FormAdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContent);
            Controls.Add(pnlFooter);
            Controls.Add(pnlNavbar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAdminDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel Administrador";
            WindowState = FormWindowState.Maximized;
            Load += FormAdminDashboard_Load;
            pnlNavbar.ResumeLayout(false);
            tlpNavbar.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            tlpFooter.ResumeLayout(false);
            tlpFooter.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            tlpMenu.ResumeLayout(false);
            tlpMenu.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlNavbar;
        private TableLayoutPanel tlpNavbar;
        private Krypton.Toolkit.KryptonButton btnLogOut;
        private Krypton.Toolkit.KryptonButton btnProviders;
        private Krypton.Toolkit.KryptonButton btnProducts;
        private Panel pnlFooter;
        private Panel pnlContent;
        private Panel pnlMenu;
        private TableLayoutPanel tlpMenu;
        private TableLayoutPanel tableLayoutPanel2;
        private Krypton.Toolkit.KryptonLabel lblUsername;
        private TableLayoutPanel tlpFooter;
        private Krypton.Toolkit.KryptonLabel lblTime;
        private System.Windows.Forms.Timer clockTimer;
        private Krypton.Toolkit.KryptonLabel lblUserFooter;
        private Krypton.Toolkit.KryptonButton btnPaymentMethods;
        private Krypton.Toolkit.KryptonButton btnPurchaseHistory;
        private Krypton.Toolkit.KryptonButton btnSellNavbar;
        private Krypton.Toolkit.KryptonButton btnHome;
        private Krypton.Toolkit.KryptonButton btnProductsNavbar;
        private Krypton.Toolkit.KryptonButton btnLogOutNavbar;
        private Krypton.Toolkit.KryptonButton btnHistoryNavbar;
        private Krypton.Toolkit.KryptonButton btnLogOutMenu;
        private Krypton.Toolkit.KryptonButton btnInitSession;
        private Krypton.Toolkit.KryptonButton btnInitSellMenu;
        private Krypton.Toolkit.KryptonButton btnProductsMenu;
        private Krypton.Toolkit.KryptonButton btnSalesHistory;
        private Krypton.Toolkit.KryptonButton btnProvidersMenu;
        private Krypton.Toolkit.KryptonButton btnEmployeesMenu;
        private Krypton.Toolkit.KryptonButton btnLogsMenu;
        private Krypton.Toolkit.KryptonButton btnPaymentMethodsMenu;
        private Krypton.Toolkit.KryptonButton btnUsersMenu;
        private Krypton.Toolkit.KryptonButton btnSettings;
    }
}