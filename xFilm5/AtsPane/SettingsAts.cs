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

#endregion

namespace xFilm5.AtsPane
{
    public partial class SettingsAts : UserControl
    {
        public SettingsAts()
        {
            InitializeComponent();

            SetAtsSettings();
        }

        private void SetAtsSettings()
        {
            this.atsSettings.MenuHandle = false;
            this.atsSettings.DragHandle = false;
            this.atsSettings.TextAlign = ToolBarTextAlign.Right;

            ContextMenu ddlNew = new ContextMenu();
            ddlNew.MenuItems.Add(new MenuItem("Backup", string.Empty, "Backup"));
            ddlNew.MenuItems.Add(new MenuItem("Restore", string.Empty, "Restore"));

            ToolBarButton cmdNew = new ToolBarButton("Database", "Database");
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            this.atsSettings.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsSettings_MenuClick);
/*
            ContextMenu ddlReports = new ContextMenu();
            ddlReports.MenuItems.Add(new MenuItem("Staff Reports", string.Empty, "Staff Reports"));
            ddlReports.MenuItems.Add(new MenuItem("Workplace Reports", string.Empty, "Workplace Reports"));
            ddlReports.MenuItems.Add(new MenuItem("Supplier Reports", string.Empty, "Supplier Reports"));

            ToolBarButton cmdReports = new ToolBarButton("Reports", "Reports");
            cmdReports.Style = ToolBarButtonStyle.DropDownButton;
            cmdReports.Image = new IconResourceHandle("16x16.16_reports.gif");
            cmdReports.DropDownMenu = ddlReports;

            this.atsSettings.Buttons.Add(cmdReports);
            cmdReports.MenuClick += new MenuEventHandler(AtsSettings_MenuClick);


            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsSettings.Buttons.Add(sep);
 */
        }

        private void AtsSettings_MenuClick(object sender, MenuItemEventArgs e)
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
                    case "staff":
                        //Staff.StaffCode staffCode = new xFilm5.Staff.StaffCode();
                        //staffCode.ShowDialog();  
                        break;
                    case "staffdept":
                        //xFilm5.Staff.StaffDeptWizard wizDept = new xFilm5.Staff.StaffDeptWizard();
                        //wizDept.ShowDialog(); 
                        break;
                    case "staffgroup":
                        //xFilm5.Staff.StaffGroupWizard wizGroup = new xFilm5.Staff.StaffGroupWizard();
                        //wizGroup.ShowDialog(); 
                        break;
                }
            }

        }
    }
}