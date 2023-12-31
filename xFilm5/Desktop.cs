#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Common.Interfaces;
using System.Diagnostics;
using System.Reflection;

using xFilm5.DAL;

#endregion

namespace xFilm5
{
    public partial class Desktop : Form
    {
        private enum AtsStyle { JobOrder, Accounting, Admin, Settings };

        public Desktop()
        {
            InitializeComponent();

            SetTheme();
            SetCaptions();
            SetNavPanes();
            SetAppToolStrip(AtsStyle.JobOrder);

            SetCloseButton();
        }

        #region Close Button
        private void SetCloseButton()
        {
            Button cmdClose = new Button();
            cmdClose.Name = "cmdClose";
            cmdClose.Location = new System.Drawing.Point(this.Width - 43, 3);
            cmdClose.Size = new System.Drawing.Size(38, 38);
            cmdClose.Image = new IconResourceHandle("32x32.shutdown32.png");
            cmdClose.ImageAlign = ContentAlignment.MiddleCenter;
            cmdClose.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            cmdClose.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));

            cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.Controls.Add(cmdClose);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void Shutdown()
        {
            Common.Config.CurrentUserId = 0;

            // set the IsLoggedOn to false will redirect to Logon Page.
            this.Context.Session.IsLoggedOn = false;
            VWGContext.Current.HttpContext.Session.Abandon();
            VWGContext.Current.Transfer(new Desktop());
        }
        #endregion

        private void SetTheme()
        {
            ImageResourceHandle bgImage = new ImageResourceHandle("logo_watermark.png");

            this.picBgImage.Image = bgImage;
//            this.picBgImage.Dock = DockStyle.Fill;
//            this.picBgImage.BackgroundImage = bgImage;
//            this.picBgImage.BackgroundImageLayout = ImageLayout.None;
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(xFilm5.DAL.Common.Config.CurrentWordDict, xFilm5.DAL.Common.Config.CurrentLanguageId);

            tabJobOrder.Text = oDict.GetWord("joborder");
            tabAdmin.Text = oDict.GetWord("admin");
            tabAccounting.Text = oDict.GetWord("accounting");
            tabSettings.Text = oDict.GetWord("settings");

            amsFile.Text = oDict.GetWord("file");
            amsFileExit.Text = oDict.GetWord("exit");
            amsView.Text = oDict.GetWord("view");
            amsViewEn.Text = oDict.GetWord("english");
            amsViewChs.Text = oDict.GetWord("simplifiedchinese");
            amsViewCht.Text = oDict.GetWord("traditionalchinese");
            amsViewVista.Text = oDict.GetWord("vista");
            amsViewBlack.Text = oDict.GetWord("black");
            amsViewWinXP.Text = oDict.GetWord("winxp");
            amsHelp.Text = oDict.GetWord("help");
            amsHelpAbout.Text = oDict.GetWord("about");
        }

        private void SetAppToolStrip(AtsStyle index)
        {
            this.atsPane.Controls.Clear();

            switch (index)
            {
                case AtsStyle.JobOrder:
                    xFilm5.AtsPane.JobOrderAts oAtsJO = new xFilm5.AtsPane.JobOrderAts();
                    oAtsJO.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsJO);
                    navTabs.SelectedIndex = 0;
                    break;
                case AtsStyle.Accounting:
                    xFilm5.AtsPane.AccountingAts oAtsAcct = new xFilm5.AtsPane.AccountingAts();
                    oAtsAcct.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsAcct);
                    navTabs.SelectedIndex = 1;
                    break;
                case AtsStyle.Admin:
                    xFilm5.AtsPane.AdminAts oAtsAdmin = new xFilm5.AtsPane.AdminAts();
                    oAtsAdmin.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsAdmin);
                    navTabs.SelectedIndex = 2;
                    break;
                case AtsStyle.Settings:
                    xFilm5.AtsPane.SettingsAts oAtsSettings = new xFilm5.AtsPane.SettingsAts();
                    oAtsSettings.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsSettings);
                    navTabs.SelectedIndex = 3;
                    break;
            }
        }

        // 如果 NavMenu.xml 內沒有對應的 NavTab 資料，會自動刪除此 NavTab
        #region Set Navigation Panes
        private void SetNavPanes()
        {
            SetNavCoding();
            SetNavOrder();
            SetNavAdmin();
            SetNavSettings();
        }

        private void SetNavCoding()
        {
            xFilm5.NavPane.AccountingNav navAccounting = new xFilm5.NavPane.AccountingNav();

            TreeView tvwCoding = (TreeView)navAccounting.Controls[0];
            if (tvwCoding.Nodes.Count == 0)
            {
                navTabs.TabPages.Remove(tabAccounting);
            }
            else
            {
                navAccounting.Dock = DockStyle.Fill;
                tabAccounting.Controls.Add(navAccounting);
            }
        }

        private void SetNavOrder()
        {
            xFilm5.NavPane.JobOrderNav navOrder = new xFilm5.NavPane.JobOrderNav();

            TreeView tvwOrder = (TreeView)navOrder.Controls[0];
            if (tvwOrder.Nodes.Count == 0)
            {
                navTabs.TabPages.Remove(tabJobOrder);
            }
            else
            {
                navOrder.Dock = DockStyle.Fill;
                tabJobOrder.Controls.Add(navOrder);
            }
        }

        private void SetNavAdmin()
        {
            xFilm5.NavPane.AdminNav navAdmin = new xFilm5.NavPane.AdminNav();

            TreeView tvwAdmin = (TreeView)navAdmin.Controls[0];
            if (tvwAdmin.Nodes.Count == 0)
            {
                navTabs.TabPages.Remove(tabAdmin);
            }
            else
            {
                navAdmin.Dock = DockStyle.Fill;
                tabAdmin.Controls.Add(navAdmin);
            }
        }

        private void SetNavSettings()
        {
            xFilm5.NavPane.SettingsNav navSettings = new xFilm5.NavPane.SettingsNav();

            TreeView tvwSettings = (TreeView)navSettings.Controls[0];
            if (tvwSettings.Nodes.Count == 0)
            {
                navTabs.TabPages.Remove(tabSettings);
            }
            else
            {
                navSettings.Dock = DockStyle.Fill;
                tabSettings.Controls.Add(navSettings);
            }
        }
        #endregion

        private void amsMain_MenuClick(object objSource, MenuItemEventArgs objArgs)
        {
            MenuItemEventArgs oArg = (MenuItemEventArgs)objArgs;
            string strAction = oArg.MenuItem.Tag as string;
            if (strAction != null)
            {
                switch (strAction)
                {
                    case "amsFileExit":
                        xFilm5.DAL.Common.Config.CurrentUserId = 0;
                        // While setting the IsLoggedOn to false, will redirect to Logon Page.
                        this.Context.Session.IsLoggedOn = false;
                        VWGContext.Current.HttpContext.Session.Abandon();
                        VWGContext.Current.Transfer(new Desktop());
                        break;
                    case "Print":
                        // MessageBox.Show(((Gizmox.WebGUI.Common.Interfaces.ISessionRegistry)this.Context.Session).Count.ToString());
                        break;
                    case "amsViewEn": // English
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "en-US";
                        VWGContext.Current.Transfer(new Desktop());
                        break;
                    case "amsViewChs": // Simplified Chinese
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHS";
                        VWGContext.Current.Transfer(new Desktop());
                        break;
                    case "amsViewCht": // Tradictional Chinese
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHT");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHT";
                        VWGContext.Current.Transfer(new Desktop());
                        break;
                    case "amsViewWinXP":
                        this.Context.CurrentTheme = "iOS";          // Theme.Default;
                        break;
                    case "amsViewVista":
                        this.Context.CurrentTheme = "Vista";        // new Theme("Vista");
                        break;
                    case "amsViewBlack":
                        this.Context.CurrentTheme = "Graphite";     // new Theme("Black");
                        break;
                    case "amsHelpAbout":
                        Help.About oAbout = new xFilm5.Help.About();
                        oAbout.ShowDialog();
                        break;
                    default:
                        //                        MessageBox.Show(strAction);
                        break;
                }
            }
        }

        #region Deselect selected TreeNodes on switching navTabs
        private void DeSelectTreeNodes()
        {
            Control[] jobOrder = this.Form.Controls.Find("navJobOrder", true);
            if (jobOrder.Length > 0)
            {
                TreeView tvJobOrder = (TreeView)jobOrder[0];
                tvJobOrder.SelectedNode = null;
            }
            Control[] acct = this.Form.Controls.Find("navAccounting", true);
            if (acct.Length > 0)
            {
                TreeView tvAcct = (TreeView)acct[0];
                tvAcct.SelectedNode = null;
            }
            Control[] admin = this.Form.Controls.Find("navAdmin", true);
            if (admin.Length > 0)
            {
                TreeView tvAdmin = (TreeView)admin[0];
                tvAdmin.SelectedNode = null;
            }
            Control[] settings = this.Form.Controls.Find("navSettings", true);
            if (settings.Length > 0)
            {
                TreeView tvInvt = (TreeView)settings[0];
                tvInvt.SelectedNode = null;
            }
        }
        #endregion

        private void navTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeSelectTreeNodes();

            switch (((string)navTabs.SelectedItem.Tag).ToLower())
            {
                case "job order":
                    SetAppToolStrip(AtsStyle.JobOrder);
                    break;
                case "accounting":
                    SetAppToolStrip(AtsStyle.Accounting);
                    break;
                case "admin":
                    SetAppToolStrip(AtsStyle.Admin);
                    break;
                case "settings":
                    SetAppToolStrip(AtsStyle.Settings);
                    break;
            }
        }
    }
}