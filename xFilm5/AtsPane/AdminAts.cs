#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;
#endregion

namespace xFilm5.AtsPane
{
    public partial class AdminAts : UserControl
    {
        public AdminAts()
        {
            InitializeComponent();

            SetAtsAdmin();
        }

        private void SetAtsAdmin()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.atsAdmin.MenuHandle = false;
            this.atsAdmin.DragHandle = false;
            this.atsAdmin.TextAlign = ToolBarTextAlign.Right;

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            if (xFilm5.Controls.Utility.User.IsStaff())
            {
                ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("staff"), string.Empty, "New_Staff"));
            }
            else
            {
                if (xFilm5.Controls.Utility.User.IsClient())
                {
                    ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("address_book"), string.Empty, "cu_NewAddress"));
                    ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("user"), string.Empty, "cu_NewUser"));
                }
            }

            ToolBarButton cmdNew = new ToolBarButton("New", oDict.GetWord("addnew"));
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            this.atsAdmin.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsMenuClick);
/*
            // cmdImport
            ContextMenu ddlImport = new ContextMenu();
            ddlImport.MenuItems.Add(new MenuItem("Member", string.Empty, "ImportMember"));

            ToolBarButton cmdImport = new ToolBarButton("Import", "Import");
            cmdImport.Style = ToolBarButtonStyle.DropDownButton;
            cmdImport.Image = new IconResourceHandle("16x16.ico_16_4407.gif");
            cmdImport.DropDownMenu = ddlImport;

            this.atsAdmin.Buttons.Add(cmdImport);
            cmdImport.MenuClick += new MenuEventHandler(AtsMenuClick);

            // cmdExport
            ContextMenu ddlExport = new ContextMenu();
            ddlExport.MenuItems.Add(new MenuItem("Member", string.Empty, "ExportMember"));

            ToolBarButton cmdExport = new ToolBarButton("Export", "Export");
            cmdExport.Style = ToolBarButtonStyle.DropDownButton;
            cmdExport.Image = new IconResourceHandle("16x16.ico_16_exportCustomizations.gif");
            cmdExport.DropDownMenu = ddlExport;

            this.atsAdmin.Buttons.Add(cmdExport);
            cmdExport.MenuClick += new MenuEventHandler(AtsMenuClick);
            
            // Separator
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsAdmin.Buttons.Add(sep);

            //  cmdReport
            ContextMenu ddlReport = new ContextMenu();
            ddlReport.MenuItems.Add(new MenuItem("Top VIP Spending by VENDOR", string.Empty, "top_vip_spending_by_vendor"));

            ToolBarButton cmdReport = new ToolBarButton("Report", "Report");
            cmdReport.Style = ToolBarButtonStyle.DropDownButton;
            cmdReport.Image = new IconResourceHandle("16x16.16_reports.gif");
            cmdReport.DropDownMenu = ddlReport;

            this.atsAdmin.Buttons.Add(cmdReport);
            cmdReport.MenuClick += new MenuEventHandler(AtsMenuClick);
 */
        }

        private void AtsMenuClick(object sender, MenuItemEventArgs e)
        {
            //Control[] controls = this.Form.Controls.Find("wspPane", true);
            //if (controls.Length > 0)
            //{
            //    Panel wspPane = (Panel)controls[0];
            //    wspPane.Text = (string)e.MenuItem.Text;
            //}

            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "cu_newaddress":
                        xFilm5.Customer.Address.AddressRecord cuAddress = new xFilm5.Customer.Address.AddressRecord();
                        cuAddress.EditMode = xFilm5.DAL.Common.Enums.EditMode.Add;
                        cuAddress.ClientId = xFilm5.Controls.Utility.User.GetClientId();
                        cuAddress.ShowDialog();
                        break;
                    case "cu_newuser":
                        xFilm5.Customer.Staff.StaffRecord cuStaff = new xFilm5.Customer.Staff.StaffRecord();
                        cuStaff.EditMode = xFilm5.DAL.Common.Enums.EditMode.Add;
                        cuStaff.ClientId = xFilm5.Controls.Utility.User.GetClientId();
                        cuStaff.ShowDialog();
                        break;
                    case "new_staff":
                        xFilm5.Admin.Staff.StaffRecord staff = new xFilm5.Admin.Staff.StaffRecord();
                        staff.EditMode = xFilm5.DAL.Common.Enums.EditMode.Add;
                        staff.ShowDialog();
                        break;
                    case "memberaddresstype":
                        //xFilm5.Member.MemberAddressTypeWizard wizAddressType = new xFilm5.Member.MemberAddressTypeWizard();
                        //wizAddressType.ShowDialog();
                        break;
                }
            }
        }

        private void AtsMemberMgmt_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            MessageBox.Show(e.Button.Text);
        }
    }
}