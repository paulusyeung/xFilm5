#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Dialogs;

using xFilm5.DAL;
using System.IO;
#endregion

namespace xFilm5.Sales.Client
{
    public partial class ClientList : UserControl
    {
        private int _ButtonIndex_Workshop = 0;
        private string _BaseSqlSelect = String.Empty;
        private string _BaseSqlWhere = String.Empty;
        private string _BaseSqlOrderBy = String.Empty;
        private string _CurSqlWhere = String.Empty;
        private string _CurSqlOrderBy = String.Empty;
        private string _CurWorkshop = String.Empty;
        private string _CurShortcut = String.Empty;

        private string _ExportTag = String.Empty;

        public ClientList()
        {
            InitializeComponent();

            //升級 WebGui 10 之後唔識留空,要用 Margin 手動
            lvwClientList.Margin = new Padding(0, 22, 0, 24);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAttribute();
            SetTheme();
            SetJoDefaultAns();

            _BaseSqlSelect = @"
SELECT TOP 100 PERCENT
        [ID]
      , [Name]
      , [PrimaryAddr]
      , [Address]
      , [Tel]
      , [Fax]                       -- 5
      , [ContactPerson]
      , [Mobile]
      , [Pager]
      , ISNULL([SMS], '')
      , ISNULL([SMS_Lang], 0)       -- 10
      , [PrimaryUser]
      , [Email]
      , [Password]
      , [SecurityLevel]
      , [CreditLimit]               -- 15
      , [PaymentTerms]
      , [PaymentType]
      , [PaymentTypeName]
      , [PIN]
      , [Branch]                    -- 20
      , ISNULL([BranchName], '')
      , [CreatedOn]
      , [Status]
FROM    [dbo].[vwClientList]
";
            _BaseSqlWhere = String.Format("WHERE ([Status] = {0} OR [Status] = {1})", ((int)DAL.Common.Enums.Status.Inactive).ToString(), ((int)DAL.Common.Enums.Status.Active).ToString());
            _BaseSqlOrderBy = "ORDER BY [Name]";
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
        }

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblLookup.Text = oDict.GetWordWithColon("lookup");

            colClientName.Text = oDict.GetWord("client_name");
            colLN.Text = oDict.GetWord("ln");
            colClientId.Text = oDict.GetWord("id");
            colClientAddr.Text = oDict.GetWord("address");
            colTel.Text = oDict.GetWord("tel");
            colFax.Text = oDict.GetWord("fax");
            colContact.Text = oDict.GetWord("contact");
            colMobile.Text = oDict.GetWord("mobile");
            colBranch.Text = oDict.GetWord("branch");
            colEmail.Text = oDict.GetWord("email");
            colCreditLimit.Text = oDict.GetWord("credit");
        }

        private void SetAttribute()
        {
            this.lvwClientList.ListViewItemSorter = new ListViewItemSorter(this.lvwClientList);

            toolTip1.SetToolTip(txtLookup, String.Format("Look for targets:{0}ID, Name, Address, Tel, Fax,{0}Mobile, and Contact Person", Environment.NewLine));
            toolTip1.SetToolTip(cmdLookup, String.Format("Look for targets:{0}ID, Name, Address, Tel, Fax,{0}Mobile, and Contact Person", Environment.NewLine));
            toolTip1.SetToolTip(lvwClientList, "Double click to open Client record");
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }
        #endregion

        #region Set Action Strip
        private void SetJoDefaultAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansToolbar.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansToolbar.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansToolbar.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansToolbar.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansToolbar.Buttons[1].ToolTipText = @"Sorting";
            this.ansToolbar.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansToolbar.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansToolbar.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansToolbar.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansToolbar.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansToolbar.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansToolbar.Buttons[3].Visible = false;
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansToolbar.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansToolbar.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansToolbar.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansToolbar.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansToolbar.Buttons[8].Enabled = false;
            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdWorkshop     - Buttons [10]
            ContextMenu ddlWorkshop = new ContextMenu();

            Client_UserCollection oWorkshop = Common.Data.GetWorkshopList();
            if (oWorkshop.Count > 0)
            {
                for (int i = 0; i < oWorkshop.Count; i++)
                {
                    ddlWorkshop.MenuItems.Add(new MenuItem(oWorkshop[i].FullName));
                }
            }

            ToolBarButton cmdWorkshop = new ToolBarButton("Workshop", oDict.GetWord("branch"));
            cmdWorkshop.Style = ToolBarButtonStyle.DropDownButton;
            cmdWorkshop.Image = new IconResourceHandle("16x16.filter_16.png");
            cmdWorkshop.DropDownMenu = ddlWorkshop;
            this.ansToolbar.Buttons.Add(cmdWorkshop);
            cmdWorkshop.MenuClick += new MenuEventHandler(ansWorkshop_MenuClick);
            _ButtonIndex_Workshop = this.ansToolbar.Buttons.Count - 1;
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdExport
            ContextMenu ddlExport = new ContextMenu();

            ddlExport.MenuItems.Add(new MenuItem(oDict.GetWord("export_email"), string.Empty, "Email"));
            ddlExport.MenuItems.Add(new MenuItem(oDict.GetWord("export_details"), string.Empty, "Details"));

            ToolBarButton cmdExport = new ToolBarButton("Export", oDict.GetWord("export"));
            cmdExport.Style = ToolBarButtonStyle.DropDownButton;
            cmdExport.Image = new IconResourceHandle("16x16.export.png");
            cmdExport.DropDownMenu = ddlExport;

            if ((xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Account) ||
                (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin))
            {
                this.ansToolbar.Buttons.Add(cmdExport);
                cmdExport.MenuClick += new MenuEventHandler(ansExport_MenuClick);

                this.ansToolbar.Buttons.Add(sep);
            }
            #endregion

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");

            this.ansToolbar.Buttons.Add(cmdPopup);
            #endregion
        }
        #endregion

        #region Bind Job Order List
        private void BindClientList()
        {
            this.lvwClientList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                ListViewItem objItem = this.lvwClientList.Items.Add(reader.GetString(1));  // Client Name
                #region Client Type Icon
                switch (reader.GetInt16(17))    // payment type
                {
                    case 0:     // On Account
                        objItem.SmallImage = new IconResourceHandle("16x16.group.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.32x32.client_32x32.png");
                        break;
                    case 1:     // C.O.D.
                    default:
                        objItem.SmallImage = new IconResourceHandle("16x16.client_Cash_16x16.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.32x32.client_Cash_32x32.png");
                        break;
                }
                #endregion
                #region SMS icon
                if (String.IsNullOrEmpty(reader.GetString(9)))
                {
                    objItem.SubItems.Add(String.Empty);  // Client Name
                }
                else
                {
                    objItem.SubItems.Add(new IconResourceHandle("16x16.SMS_16x16.png").ToString());  // Client Name
                }
                #endregion
                #region Status icon
                switch (reader.GetInt16(23))
                {
                    case (int)DAL.Common.Enums.Status.Active:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.flag_green.png").ToString());
                        break;
                    case (int)DAL.Common.Enums.Status.Inactive:
                    default:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.flag_grey.png").ToString());
                        break;
                }
                #endregion
                objItem.SubItems.Add(iCount.ToString());    // Line Number
                objItem.SubItems.Add(reader.GetInt32(0).ToString());  // Client ID
                objItem.SubItems.Add(reader.GetString(3));  // Address
                objItem.SubItems.Add(reader.GetString(4));  // Tel
                objItem.SubItems.Add(reader.GetString(5));  // Fax
//                objItem.SubItems.Add(Common.DateTimeHelper.DateTimeToString(reader.GetString(8), true));  // Completed On
                objItem.SubItems.Add(reader.GetString(6)); // Contact Person
                objItem.SubItems.Add(reader.GetString(7)); // Mobile
                objItem.SubItems.Add(reader.GetString(21)); // Branch
                objItem.SubItems.Add(reader.GetString(12)); // Email
                objItem.SubItems.Add(reader.GetInt16(15).ToString()); // Credit Limit

                iCount++;
            }
            reader.Close();
        }

        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();

            #region prepare the where clause
            if (!(String.IsNullOrEmpty(_CurShortcut)))
            {
                switch (_CurShortcut)
                {
                    case "9":
                        _CurSqlWhere = _BaseSqlWhere + " AND (SUBSTRING([Name], 1, 1) NOT BETWEEN N'A' AND N'Z')";
                        break;
                    case "All":
                        _CurSqlWhere = _BaseSqlWhere;
                        break;
                    default:
                        _CurSqlWhere = _BaseSqlWhere + String.Format(" AND (SUBSTRING([Name], 1, 1) = N'{0}')", _CurShortcut);
                        break;
                }
                if (!(String.IsNullOrEmpty(_CurWorkshop)))
                {
                    if (String.IsNullOrEmpty(_CurSqlWhere))
                    {
                        _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([BranchName] = N'{0}')", _CurWorkshop);
                    }
                    else
                    {
                        _CurSqlWhere = _CurSqlWhere + String.Format(" AND ([BranchName] = N'{0}')", _CurWorkshop);
                    }
                }
            }
            else
            {
                if (!(String.IsNullOrEmpty(_CurWorkshop)))
                {
                    _CurSqlWhere = _CurSqlWhere + String.Format(" AND ([BranchName] = N'{0}')", _CurWorkshop);
                }
            }
            #endregion

            sql.Append(_BaseSqlSelect + Environment.NewLine);
            sql.Append(_CurSqlWhere + Environment.NewLine);
            sql.Append(_CurSqlOrderBy);

            return sql.ToString();
        }
        #endregion

        private void ResetForm()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            txtLookup.Text = String.Empty;
            ansToolbar.Buttons[_ButtonIndex_Workshop].Text = oDict.GetWord("branch");
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
            _CurWorkshop = String.Empty;
            _CurShortcut = String.Empty;
        }

        private void RefreshForm()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            txtLookup.Text = String.Empty;
            ansToolbar.Buttons[_ButtonIndex_Workshop].Text = oDict.GetWord("branch");
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
            _CurWorkshop = String.Empty;
//            _CurShortcut = String.Empty;
        }

        private void DoLookup()
        {
            string target = txtLookup.Text.Trim();
            if (!(String.IsNullOrEmpty(target)))
            {
                ResetForm();
                txtLookup.Text = target;
                _CurSqlWhere = _BaseSqlWhere + " AND " +
                               String.Format(" (CONVERT(NVARCHAR(6), [ID]) LIKE '%{0}%' OR [Name] LIKE N'%{0}%' OR [Address] LIKE N'%{0}%' OR [Tel] LIKE N'%{0}%' OR [Fax] LIKE N'%{0}%' OR [Mobile] LIKE N'%{0}%' OR [ContactPerson] LIKE N'%{0}%')", target);
                BindClientList();
                this.Update();
            }
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        RefreshForm();
                        BindClientList();
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwClientList);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwClientList);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvwClientList.CheckBoxes = !this.lvwClientList.CheckBoxes;
                        this.lvwClientList.MultiSelect = this.lvwClientList.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvwClientList.MultiSelect = !this.lvwClientList.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "popup":
                        ShowRecord();
                        break;
                }
            }
        }

        private void ShowRecord()
        {
            if (lvwClientList.SelectedItem != null)
            {
                int clientId = Convert.ToInt32(lvwClientList.SelectedItem.SubItems[4].Text);
                ClientRecord record = new ClientRecord();
                record.ClientId = clientId; // Convert.ToInt32(lvwClientList.SelectedItem.Text);
                record.EditMode = Common.Enums.EditMode.Edit;
                record.Show();
            }
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch ((string)e.MenuItem.Tag)
            {
                case "Icon":
                    this.lvwClientList.View = View.SmallIcon;
                    break;
                case "Tile":
                    this.lvwClientList.View = View.LargeIcon;
                    break;
                case "List":
                    this.lvwClientList.View = View.List;
                    break;
                case "Details":
                    this.lvwClientList.View = View.Details;
                    break;
            }
        }

        private void ansWorkshop_MenuClick(object sender, MenuItemEventArgs e)
        {
            // show he selected Workshop as Ans Button text
            ToolBarButton oSender = (ToolBarButton)sender;
            oSender.Text = e.MenuItem.Text;
            _CurWorkshop = e.MenuItem.Text;
            BindClientList();
            this.Update();
        }

        private void ansExport_MenuClick(object sender, MenuItemEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            ToolBarButton oSender = (ToolBarButton)sender;

            _ExportTag = (string)e.MenuItem.Tag;
            MessageBox.Show(String.Format("{0} {1}?", oDict.GetWord("export"), oDict.GetWord("msg_the_selected_item(s)")),
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdExport_Click));
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            string exportFileName = String.Empty;
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                switch (_ExportTag.ToLower())
                {
                    case "email":
                        exportFileName = "ClientEmailList.csv";
                        xFilm5.Controls.Export.CsvLoader.Client_Email(ref this.lvwClientList, exportFileName);
                        break;
                    case "details":
                        exportFileName = "ClientDetailList.csv";
                        xFilm5.Controls.Export.CsvLoader.Client_Details(ref this.lvwClientList, exportFileName);
                        break;
                }

                xFilm5.Controls.FileDownloadGateway dl = new xFilm5.Controls.FileDownloadGateway();
                dl.Filename = exportFileName;
                dl.SetContentType(xFilm5.Controls.DownloadContentType.PlainText);
                dl.StartFileDownload(this, Path.Combine(DAL.Common.Config.OutBox, exportFileName));
            }
        }

        private void ShortcutButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            _CurShortcut = button.Tag.ToString();
            BindClientList();
            this.Update();
        }

        private void cmdLookup_Click(object sender, EventArgs e)
        {
            DoLookup();
        }

        private void txtLookup_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoLookup();
        }

        private void lvwClientList_DoubleClick(object sender, EventArgs e)
        {
            ShowRecord();
        }

        private void lvwClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwClientList.MultiSelect && lvwClientList.CheckBoxes)
            {
                foreach (ListViewItem item in lvwClientList.SelectedItems)
                {
                    item.Checked = true;
                }
            }
        }
    }
}