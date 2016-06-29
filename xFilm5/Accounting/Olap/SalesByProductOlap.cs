#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using xFilm5.DAL;
//using Job.Book.Controls;
using System.IO;

#endregion

namespace xFilm5.Accounting.Olap
{
    public partial class SalesByProductOlap : UserControl
    {
        public SalesByProductOlap()
        {
            InitializeComponent();

            this.mainPane.Margin = new Padding(0, 0, 0, 4);
            this.olapBox.BackgroundImage = new ImageResourceHandle("loading.gif");
            this.olapBox.BackgroundImageLayout = ImageLayout.None;      // 唔可以居中，有機會穿崩
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            String aspx = Path.Combine(Gizmox.WebGUI.Hosting.HostContext.Current.Request.ApplicationPath, "Accounting/Olap/SalesByProductOlapPage.apsx");
            this.olapBox.Path = @"Accounting\Olap\SalesByProductOlapPage.aspx";

            this.mainPane.Dock = DockStyle.Fill;
            this.olapBox.Dock = DockStyle.Fill;

            this.SetJoDefaultAns();
            this.ansToolbar.Visible = false;    //唔用，改行用 AspPageBox 自己嘅 ASPxMenu
        }

        #region Set Action Strip
        private void SetJoDefaultAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            #region 唔用，改行用 AspPageBox 自己嘅 ASPxMenu

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            sep.Visible = false;

            #region cmdButtons   - Buttons [0~3]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansToolbar.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansToolbar.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansToolbar.Buttons[0].Visible = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansToolbar.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansToolbar.Buttons[1].ToolTipText = @"Sorting";
            this.ansToolbar.Buttons[1].Visible = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansToolbar.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansToolbar.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansToolbar.Buttons[2].Visible = false;
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
            cmdViews.Visible = false;
            this.ansToolbar.Buttons.Add(cmdViews);
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansToolbar.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansToolbar.Buttons[7].Visible = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansToolbar.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansToolbar.Buttons[8].Visible = false;
            this.ansToolbar.Buttons[8].Enabled = false;
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdAttachment
            ContextMenu ddlAttachment = new ContextMenu();
            //ddlAttachment.MenuItems.Add(new MenuItem(oDict.GetWord("product_code"), String.Empty, Utility.JobOrder.JobAttachmentType.Product));
            //ddlAttachment.MenuItems.Add(new MenuItem(oDict.GetWord("customer_ref"), String.Empty, Utility.JobOrder.JobAttachmentType.Customer));
            //ddlAttachment.MenuItems[0].Icon = new IconResourceHandle("16x16.attach.png");
            //ddlAttachment.MenuItems[1].Icon = new IconResourceHandle("16x16.attach.png");

            ToolBarButton cmdAttachment = new ToolBarButton("Attachment", oDict.GetWord("attachment"));
            cmdAttachment.Image = new IconResourceHandle("16x16.eye.png");
            cmdAttachment.Style = ToolBarButtonStyle.DropDownButton;
            cmdAttachment.DropDownMenu = ddlAttachment;
            cmdAttachment.Visible = false;
            this.ansToolbar.Buttons.Add(cmdAttachment);
            #endregion

            #region Print, Export, Delete
            ToolBarButton cmdPrint = new ToolBarButton("Print", oDict.GetWord("print_order"));
            cmdPrint.Image = new IconResourceHandle("16x16.16_print.gif");
            cmdPrint.Visible = false;
            ToolBarButton cmdExport = new ToolBarButton("Export", oDict.GetWord("export"));
            cmdExport.Image = new IconResourceHandle("16x16.16_excel.gif");
            cmdExport.Visible = false;
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");
            cmdDelete.Visible = false;

            this.ansToolbar.Buttons.Add(cmdPrint);
            this.ansToolbar.Buttons.Add(cmdExport);
            this.ansToolbar.Buttons.Add(sep);

            //if (Job.Book.Controls.Utility.User.AllowedToDelete())
            //{
            //    this.ansToolbar.Buttons.Add(cmdDelete);
            //    this.ansToolbar.Buttons.Add(sep);
            //}
            #endregion

            #endregion 唔用，改行用 AspPageBox 自己嘅 ASPxMenu

            ToolBarButton cmdExportToExcel = new ToolBarButton("ExportToExcel", oDict.GetWord("export_to_excel"));
            cmdExportToExcel.Image = new IconResourceHandle("16x16.16_excel.gif");
            this.ansToolbar.Buttons.Add(cmdExportToExcel);

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);

            this.olapBox.Text = oDict.GetWord("export_to_excel");       //推送去 AspPageBox
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        RefreshForm();
                        break;
                    case "columns":
                        break;
                    case "sorting":
                        break;
                    case "checkbox":
                        break;
                    case "multiselect":
                        break;
                    case "print":
                        break;
                    case "export":
                        break;
                    case "delete":
                        break;
                }
            }
        }

        #endregion

        private void RefreshForm()
        {
            this.olapBox.Update();
        }
    }
}