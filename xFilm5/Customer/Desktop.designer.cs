namespace xFilm5.Customer
{
    partial class Desktop
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle9 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle10 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle11 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle12 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle13 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle14 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle15 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle16 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.amsMain = new Gizmox.WebGUI.Forms.MainMenu();
            this.amsFile = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsFileExit = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsView = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewEn = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewCht = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewChs = new Gizmox.WebGUI.Forms.MenuItem();
            this.menuItem1 = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewVista = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewBlack = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsViewWinXP = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsHelp = new Gizmox.WebGUI.Forms.MenuItem();
            this.amsHelpAbout = new Gizmox.WebGUI.Forms.MenuItem();
            this.atsPane = new Gizmox.WebGUI.Forms.Panel();
            this.navPane = new Gizmox.WebGUI.Forms.Panel();
            this.navTabs = new Gizmox.WebGUI.Forms.NavigationTabs();
            this.tabJobOrder = new Gizmox.WebGUI.Forms.TabPage();
            this.tabAccounting = new Gizmox.WebGUI.Forms.TabPage();
            this.tabAdmin = new Gizmox.WebGUI.Forms.TabPage();
            this.tabSettings = new Gizmox.WebGUI.Forms.TabPage();
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.picBgImage = new Gizmox.WebGUI.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // amsMain
            // 
            this.amsMain.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.amsMain.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.amsMain.Location = new System.Drawing.Point(0, 0);
            this.amsMain.MenuItems.AddRange(new Gizmox.WebGUI.Forms.MenuItem[] {
            this.amsFile,
            this.amsView,
            this.amsHelp});
            this.amsMain.Name = "amsMain";
            this.amsMain.Size = new System.Drawing.Size(369, 22);
            this.amsMain.MenuClick += new Gizmox.WebGUI.Forms.MenuEventHandler(this.amsMain_MenuClick);
            // 
            // amsFile
            // 
            this.amsFile.MenuItems.AddRange(new Gizmox.WebGUI.Forms.MenuItem[] {
            this.amsFileExit});
            this.amsFile.Tag = "amsFile";
            this.amsFile.Text = "File";
            // 
            // amsFileExit
            // 
            this.amsFileExit.Tag = "amsFileExit";
            this.amsFileExit.Text = "Exit";
            // 
            // amsView
            // 
            this.amsView.Index = 1;
            this.amsView.MenuItems.AddRange(new Gizmox.WebGUI.Forms.MenuItem[] {
            this.amsViewEn,
            this.amsViewChs,
            this.amsViewCht,
            this.menuItem1,
            this.amsViewVista,
            this.amsViewBlack,
            this.amsViewWinXP});
            this.amsView.Tag = "amsView";
            this.amsView.Text = "View";
            // 
            // amsViewEn
            // 
            iconResourceHandle9.File = "16x16.English16.gif";
            this.amsViewEn.Icon = iconResourceHandle9;
            this.amsViewEn.Tag = "amsViewEn";
            this.amsViewEn.Text = "English";
            // 
            // amsViewCht
            // 
            iconResourceHandle10.File = "16x16.TradChinese16.gif";
            this.amsViewCht.Icon = iconResourceHandle10;
            this.amsViewCht.Tag = "amsViewCht";
            this.amsViewCht.Text = "Traditional Chinese";
            // 
            // amsViewChs
            // 
            iconResourceHandle11.File = "16x16.SimpChinese16.gif";
            this.amsViewChs.Icon = iconResourceHandle11;
            this.amsViewChs.Tag = "amsViewChs";
            this.amsViewChs.Text = "Simplified Chinese";
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // amsViewVista
            // 
            this.amsViewVista.Tag = "amsViewVista";
            this.amsViewVista.Text = "Vista Theme";
            // 
            // amsViewBlack
            // 
            this.amsViewBlack.Tag = "amsViewBlack";
            this.amsViewBlack.Text = "Black Theme";
            // 
            // amsViewWinXP
            // 
            this.amsViewWinXP.Tag = "amsViewWinXP";
            this.amsViewWinXP.Text = "WinXP Theme";
            // 
            // amsHelp
            // 
            this.amsHelp.MenuItems.AddRange(new Gizmox.WebGUI.Forms.MenuItem[] {
            this.amsHelpAbout});
            this.amsHelp.Tag = "amsHelp";
            this.amsHelp.Text = "Help";
            // 
            // amsHelpAbout
            // 
            iconResourceHandle12.File = "16x16.16_info.gif";
            this.amsHelpAbout.Icon = iconResourceHandle12;
            this.amsHelpAbout.Tag = "amsHelpAbout";
            this.amsHelpAbout.Text = "About";
            // 
            // atsPane
            // 
            this.atsPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsPane.Location = new System.Drawing.Point(0, 0);
            this.atsPane.Name = "atsPane";
            this.atsPane.Size = new System.Drawing.Size(520, 28);
            this.atsPane.TabIndex = 0;
            // 
            // navPane
            // 
            this.navPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navPane.Controls.Add(this.navTabs);
            this.navPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.navPane.Location = new System.Drawing.Point(0, 28);
            this.navPane.Name = "navPane";
            this.navPane.Size = new System.Drawing.Size(150, 342);
            this.navPane.TabIndex = 1;
            // 
            // navTabs
            // 
            this.navTabs.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navTabs.Controls.Add(this.tabJobOrder);
            this.navTabs.Controls.Add(this.tabAccounting);
            this.navTabs.Controls.Add(this.tabAdmin);
            this.navTabs.Controls.Add(this.tabSettings);
            this.navTabs.CustomStyle = "Navigation";
            this.navTabs.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.navTabs.Location = new System.Drawing.Point(0, 0);
            this.navTabs.Multiline = false;
            this.navTabs.Name = "navTabs";
            this.navTabs.SelectedIndex = 0;
            this.navTabs.Size = new System.Drawing.Size(150, 342);
            this.navTabs.TabIndex = 0;
            this.navTabs.SelectedIndexChanged += new System.EventHandler(this.navTabs_SelectedIndexChanged);
            // 
            // tabJobOrder
            // 
            iconResourceHandle13.File = "24x24.settings_24x24.gif";
            this.tabJobOrder.Image = iconResourceHandle13;
            this.tabJobOrder.Location = new System.Drawing.Point(4, 22);
            this.tabJobOrder.Name = "tabJobOrder";
            this.tabJobOrder.Size = new System.Drawing.Size(142, 316);
            this.tabJobOrder.TabIndex = 0;
            this.tabJobOrder.Tag = "Job Order";
            this.tabJobOrder.Text = "Job Order";
            // 
            // tabAccounting
            // 
            iconResourceHandle14.File = "24x24.sales_24x24.gif";
            this.tabAccounting.Image = iconResourceHandle14;
            this.tabAccounting.Location = new System.Drawing.Point(4, 22);
            this.tabAccounting.Name = "tabAccounting";
            this.tabAccounting.Size = new System.Drawing.Size(142, 316);
            this.tabAccounting.TabIndex = 1;
            this.tabAccounting.Tag = "Accounting";
            this.tabAccounting.Text = "Accounting";
            // 
            // tabAdmin
            // 
            iconResourceHandle15.File = "24x24.services_24x24.gif";
            this.tabAdmin.Image = iconResourceHandle15;
            this.tabAdmin.Location = new System.Drawing.Point(4, 22);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.Size = new System.Drawing.Size(142, 316);
            this.tabAdmin.TabIndex = 2;
            this.tabAdmin.Tag = "Admin";
            this.tabAdmin.Text = "Admin";
            // 
            // tabSettings
            // 
            iconResourceHandle16.File = "24x24.24_settings.gif";
            this.tabSettings.Image = iconResourceHandle16;
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(142, 316);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Tag = "Settings";
            this.tabSettings.Text = "Settings";
            // 
            // splitter1
            // 
            this.splitter1.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.splitter1.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.splitter1.Location = new System.Drawing.Point(150, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 342);
            this.splitter1.TabIndex = 2;
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.BackColor = System.Drawing.Color.Empty;
            this.wspPane.Controls.Add(this.picBgImage);
            this.wspPane.CustomStyle = "HeaderedPanel";
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(151, 28);
            this.wspPane.Name = "wspPane";
            this.wspPane.PanelType = Gizmox.WebGUI.Forms.PanelType.Titled;
            this.wspPane.Size = new System.Drawing.Size(369, 342);
            this.wspPane.TabIndex = 3;
            // 
            // picBgImage
            // 
            this.picBgImage.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Bottom | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.picBgImage.Image = null;
            this.picBgImage.Location = new System.Drawing.Point(238, 210);
            this.picBgImage.Name = "picBgImage";
            this.picBgImage.Size = new System.Drawing.Size(128, 128);
            this.picBgImage.SizeMode = Gizmox.WebGUI.Forms.PictureBoxSizeMode.StretchImage;
            this.picBgImage.TabIndex = 0;
            // 
            // Desktop
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.navPane);
            this.Controls.Add(this.atsPane);
            this.Menu = this.amsMain;
            this.Size = new System.Drawing.Size(520, 370);
            this.Text = "xFilm5";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.MainMenu amsMain;
        private Gizmox.WebGUI.Forms.MenuItem amsFile;
        private Gizmox.WebGUI.Forms.MenuItem amsFileExit;
        private Gizmox.WebGUI.Forms.MenuItem amsView;
        private Gizmox.WebGUI.Forms.MenuItem amsHelp;
        private Gizmox.WebGUI.Forms.MenuItem amsHelpAbout;
        private Gizmox.WebGUI.Forms.Panel atsPane;
        private Gizmox.WebGUI.Forms.Panel navPane;
        private Gizmox.WebGUI.Forms.Splitter splitter1;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.NavigationTabs navTabs;
        private Gizmox.WebGUI.Forms.TabPage tabAdmin;
        private Gizmox.WebGUI.Forms.TabPage tabAccounting;
        private Gizmox.WebGUI.Forms.TabPage tabJobOrder;
        private Gizmox.WebGUI.Forms.TabPage tabSettings;
        private Gizmox.WebGUI.Forms.PictureBox picBgImage;
        private Gizmox.WebGUI.Forms.MenuItem amsViewEn;
        private Gizmox.WebGUI.Forms.MenuItem amsViewCht;
        private Gizmox.WebGUI.Forms.MenuItem amsViewChs;
        private Gizmox.WebGUI.Forms.MenuItem menuItem1;
        private Gizmox.WebGUI.Forms.MenuItem amsViewVista;
        private Gizmox.WebGUI.Forms.MenuItem amsViewBlack;
        private Gizmox.WebGUI.Forms.MenuItem amsViewWinXP;


    }
}