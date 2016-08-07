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
    public partial class JobOrderAts : UserControl
    {
        public JobOrderAts()
        {
            InitializeComponent();

            SetAtsJobOrder();
        }

        private void SetAtsJobOrder()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.atsJobOrder.MenuHandle = false;
            this.atsJobOrder.DragHandle = false;
            this.atsJobOrder.TextAlign = ToolBarTextAlign.Right;

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            Common.Data.AppendMenuItem_OrderType(ref ddlNew);
            ToolBarButton cmdNew = new ToolBarButton("New", oDict.GetWord("addnew"));
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            this.atsJobOrder.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsMenuClick);

            // Separator
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsJobOrder.Buttons.Add(sep);

            // cmdSearch
            ToolBarButton cmdSearch = new ToolBarButton("Search", oDict.GetWord("search"));
            cmdSearch.Tag = "Search";
            cmdSearch.Image = new IconResourceHandle("16x16.find.png");

            this.atsJobOrder.Buttons.Add(cmdSearch);

            this.atsJobOrder.ButtonClick += new ToolBarButtonClickEventHandler(atsJobOrder_ButtonClick);           
        }

        private void AtsMenuClick(object sender, MenuItemEventArgs e)
        {
            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "uploadfile":
                        xFilm5.JobOrder.Forms.UploadFile oUploadFile = new xFilm5.JobOrder.Forms.UploadFile();
                        oUploadFile.EditMode = Common.Enums.EditMode.Add;
                        oUploadFile.Show();
                        break;
                    case "directprint":
                        xFilm5.JobOrder.Forms.DirectPrint oDirectPrint = new xFilm5.JobOrder.Forms.DirectPrint();
                        oDirectPrint.EditMode = Common.Enums.EditMode.Add;
                        oDirectPrint.Show();
                        break;
                    case "psfile":
                        xFilm5.JobOrder.Forms.PsFile oPsFile = new xFilm5.JobOrder.Forms.PsFile();
                        oPsFile.EditMode = Common.Enums.EditMode.Add;
                        oPsFile.Show();
                        break;
                    case "plate":
                        xFilm5.JobOrder.Forms.Plate5 oPlate = new xFilm5.JobOrder.Forms.Plate5();
                        oPlate.EditMode = Common.Enums.EditMode.Add;
                        oPlate.Show();
                        break;
                    case "film":
                        xFilm5.JobOrder.Forms.Film5 oFilm = new xFilm5.JobOrder.Forms.Film5();
                        oFilm.EditMode = Common.Enums.EditMode.Add;
                        oFilm.Show();
                        break;
                    case "vps":
                        xFilm5.JobOrder.Forms.Vps5 oVps = new xFilm5.JobOrder.Forms.Vps5();
                        oVps.EditMode = Common.Enums.EditMode.Add;
                        oVps.Show();
                        break;
                }
            }
        }

        private void atsJobOrder_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = (string)e.Button.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)e.Button.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
                    case "search":
                        if (xFilm5.Controls.Utility.User.IsStaff(xFilm5.DAL.Common.Config.CurrentUserId))
                        {
                            xFilm5.JobOrder.JoSearch oJoSearch = new xFilm5.JobOrder.JoSearch(controls[0]);
                            oJoSearch.DockPadding.All = 6;
                            oJoSearch.Dock = DockStyle.Fill;
                            oJoSearch.WorkflowFrom = Common.Enums.Workflow.Cancelled;
                            oJoSearch.WorkflowTo = Common.Enums.Workflow.Completed;
                            wspPane.Controls.Add(oJoSearch);
                        }
                        else
                        {
                            if (xFilm5.Controls.Utility.User.IsClient(xFilm5.DAL.Common.Config.CurrentUserId))
                            {
                                xFilm5.Customer.JobOrder.JoSearch cuSearch = new xFilm5.Customer.JobOrder.JoSearch(controls[0]);
                                cuSearch.DockPadding.All = 6;
                                cuSearch.Dock = DockStyle.Fill;
                                cuSearch.WorkflowFrom = Common.Enums.Workflow.Cancelled;
                                cuSearch.WorkflowTo = Common.Enums.Workflow.Completed;
                                wspPane.Controls.Add(cuSearch);
                            }
                        }
                    break;
                }
            }
        }
    }
}