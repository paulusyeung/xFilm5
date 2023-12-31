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
using xFilm5.Product;
using xFilm5.DAL;
using xFilm5.JobOrder;

#endregion

namespace xFilm5.AtsPane
{
    public partial class AccountingAts : UserControl
    {
        public AccountingAts()
        {
            InitializeComponent();

            SetAtsAccounting();
        }

        private void SetAtsAccounting()
        {
            this.atsAccounting.MenuHandle = false;
            this.atsAccounting.DragHandle = false;
            this.atsAccounting.TextAlign = ToolBarTextAlign.Right;

/* 2010.05.22 paulus: 冇用

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            ddlNew.MenuItems.Add(new MenuItem("Payment", string.Empty, "Payment"));

            ToolBarButton cmdNew = new ToolBarButton("New", "New");
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            this.atsAccounting.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsMenuClick);

            // cmdImport
            ContextMenu ddlImport = new ContextMenu();
            ddlImport.MenuItems.Add(new MenuItem("Receiving from PPC", string.Empty, "PurImportReceivingPPC"));
            ddlImport.MenuItems.Add(new MenuItem("PO Worksheet", string.Empty, "PurImportWorksheet"));
            ddlImport.MenuItems.Add(new MenuItem("PO Receive Worksheet", string.Empty, "PurImportReceiveWorksheet"));
            ddlImport.MenuItems.Add(new MenuItem("Advanced Shipment Notice", string.Empty, "PurImportAdvancedShipmentNotice"));

            ToolBarButton cmdImport = new ToolBarButton("Import", "Import");
            cmdImport.Style = ToolBarButtonStyle.DropDownButton;
            cmdImport.Image = new IconResourceHandle("16x16.ico_16_4407.gif");
            cmdImport.DropDownMenu = ddlImport;

            this.atsAccounting.Buttons.Add(cmdImport);
            cmdImport.MenuClick += new MenuEventHandler(AtsMenuClick);

            // cmdExport
            ContextMenu ddlExport = new ContextMenu();
            ddlExport.MenuItems.Add(new MenuItem("PO History Data", string.Empty, "PurExportHistoryData"));

            ToolBarButton cmdExport = new ToolBarButton("Export", "Export");
            cmdExport.Style = ToolBarButtonStyle.DropDownButton;
            cmdExport.Image = new IconResourceHandle("16x16.ico_16_exportCustomizations.gif");
            cmdExport.DropDownMenu = ddlExport;

            this.atsAccounting.Buttons.Add(cmdExport);
            cmdExport.MenuClick += new MenuEventHandler(AtsMenuClick);

            // Separator
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsAccounting.Buttons.Add(sep);

            //  cmdReport
            ContextMenu ddlReport = new ContextMenu();
//            ddlReport.MenuItems.Add(new MenuItem("Reprint Invoice", string.Empty, "Reprint_Invoice"));
//            ddlReport.MenuItems.Add(new MenuItem("-"));
            ddlReport.MenuItems.Add(new MenuItem("Statement", string.Empty, "Statement"));

            ToolBarButton cmdReport = new ToolBarButton("Print", "Print");
            cmdReport.Style = ToolBarButtonStyle.DropDownButton;
            cmdReport.Image = new IconResourceHandle("16x16.16_print.gif");
            cmdReport.DropDownMenu = ddlReport;

            this.atsAccounting.Buttons.Add(cmdReport);
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
                    case "purpurchaseorders_multipleloc":
                        //xFilm5.Purchasing.Wizard.ByMultipleLocation wizMultipleLoc = new xFilm5.Purchasing.Wizard.ByMultipleLocation();
                        //wizMultipleLoc.ShowDialog();
                        break;
                    case "purpurchaseorders":
                        //xFilm5.Purchasing.Wizard.PurchaseOrder wizPO = new xFilm5.Purchasing.Wizard.PurchaseOrder();
                        //wizPO.ShowDialog();
                        break;
                }
            }
        }

        private void atsPurchase_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            MessageBox.Show(e.Button.Text);
        }
    }
}