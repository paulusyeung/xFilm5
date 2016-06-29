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
#endregion

namespace xFilm5.Customer.Staff
{
    public partial class StaffList : UserControl
    {
        private int _ButtonIndex_Workshop = 0;
        private string _BaseSqlSelect = String.Empty;
        private string _BaseSqlWhere = String.Empty;
        private string _BaseSqlOrderBy = String.Empty;
        private string _CurSqlWhere = String.Empty;
        private string _CurSqlOrderBy = String.Empty;
        private string _CurWorkshop = String.Empty;
        private string _CurShortcut = String.Empty;

        public StaffList()
        {
            InitializeComponent();
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
      ,[ClientID]
      ,[PrimaryUser]
      ,ISNULL([FullName], '')
      ,ISNULL([Password], '')
      ,ISNULL([SecurityLevel], 0)   -- 5
      ,ISNULL([Email], '')
FROM    [dbo].[Client_User]
";
            _BaseSqlWhere = String.Format("WHERE [ClientID] = {0}", xFilm5.Controls.Utility.User.GetClientId().ToString());
            _BaseSqlOrderBy = "ORDER BY [FullName]";
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;

            BindClientList();
        }

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblLookup.Text = oDict.GetWord("lookup");

            colFullName.Text = oDict.GetWord("full_name");
            colLN.Text = oDict.GetWord("ln");
            colEmail.Text = oDict.GetWord("email");
            colPassword.Text = oDict.GetWord("password");
        }

        private void SetAttribute()
        {
            this.lvwClientList.ListViewItemSorter = new ListViewItemSorter(this.lvwClientList);

            toolTip1.SetToolTip(txtLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
            toolTip1.SetToolTip(cmdLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
            toolTip1.SetToolTip(lvwClientList, "Double click to open Item record");
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
            //ContextMenu ddlWorkshop = new ContextMenu();

            //Client_UserCollection oWorkshop = Common.Data.GetWorkshopList();
            //if (oWorkshop.Count > 0)
            //{
            //    for (int i = 0; i < oWorkshop.Count; i++)
            //    {
            //        ddlWorkshop.MenuItems.Add(new MenuItem(oWorkshop[i].FullName));
            //    }
            //}

            //ToolBarButton cmdWorkshop = new ToolBarButton("Workshop", "Branch");
            //cmdWorkshop.Style = ToolBarButtonStyle.DropDownButton;
            //cmdWorkshop.Image = new IconResourceHandle("16x16.filter_16.png");
            //cmdWorkshop.DropDownMenu = ddlWorkshop;
            //this.ansToolbar.Buttons.Add(cmdWorkshop);
            //cmdWorkshop.MenuClick += new MenuEventHandler(ansWorkshop_MenuClick);
            //_ButtonIndex_Workshop = this.ansToolbar.Buttons.Count - 1;
            #endregion

            //this.ansToolbar.Buttons.Add(sep);

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
                ListViewItem objItem = this.lvwClientList.Items.Add(reader.GetString(3));  // Full Name
                #region Icon
                bool primaryRec = reader.GetBoolean(2);
                if (primaryRec)
                {
                    objItem.SmallImage = new IconResourceHandle("16x16.staff16_key.png");
                    objItem.LargeImage = new IconResourceHandle("Icons.32x32.staff32_key.png");
                }
                else
                {
                    objItem.SmallImage = new IconResourceHandle("16x16.staffSingle_16.png");
                    objItem.LargeImage = new IconResourceHandle("Icons.32x32.staffSingle_32.png");
                }
                #endregion
                objItem.SubItems.Add(reader.GetInt32(0).ToString());  // ID
                objItem.SubItems.Add(iCount.ToString());    // Line Number
                objItem.SubItems.Add(reader.GetString(6));  // Email
                objItem.SubItems.Add(reader.GetString(4));  // Password

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
                        _CurSqlWhere = _BaseSqlWhere + " AND SUBSTRING([FullName], 1, 1) NOT BETWEEN N'A' AND N'Z'";
                        break;
                    case "All":
                        _CurSqlWhere = _BaseSqlWhere;
                        break;
                    default:
                        _CurSqlWhere = _BaseSqlWhere + String.Format(" AND SUBSTRING([FullName], 1, 1) = N'{0}'", _CurShortcut);
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
                    _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([BranchName] = N'{0}')", _CurWorkshop);
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
            txtLookup.Text = String.Empty;
            ansToolbar.Buttons[_ButtonIndex_Workshop].Text = "Branch";
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
            _CurWorkshop = String.Empty;
            _CurShortcut = String.Empty;
        }

        private void RefreshForm()
        {
            txtLookup.Text = String.Empty;
            ansToolbar.Buttons[_ButtonIndex_Workshop].Text = "Branch";
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
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ") +
                               String.Format(" ([FullName] LIKE N'%{0}%' OR [Email] LIKE N'%{0}%')", target);
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
                        break;
                    case "multiselect":
                        this.lvwClientList.MultiSelect = !this.lvwClientList.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                }
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
            if (lvwClientList.SelectedItem != null)
            {
                int itemId = Convert.ToInt32(lvwClientList.SelectedItem.SubItems[1].Text);
                StaffRecord record = new StaffRecord();
                record.ClientId = itemId; // Convert.ToInt32(lvwClientList.SelectedItem.Text);
                record.EditMode = Common.Enums.EditMode.Edit;
                record.Show();
            }
        }
    }
}