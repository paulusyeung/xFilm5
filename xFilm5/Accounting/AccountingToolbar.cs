using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using xFilm5.DAL;

namespace xFilm5.Product
{
    public class ProductToolbar
    {
        private Control atsPaneCtrl;

        public ProductToolbar(Control toolBar, ref ToolBar tbProducts)
        {
            atsPaneCtrl = toolBar;
            ClearToolBar();
            AddItemToToolBar(tbProducts);
        }

        private void ClearToolBar()
        {
            atsPaneCtrl.Controls.Clear();
        }

        private void AddItemToToolBar(ToolBar tbProducts)
        {
            tbProducts.MenuHandle = false;
            tbProducts.DragHandle = false;
            tbProducts.TextAlign = ToolBarTextAlign.Right;

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            ddlNew.MenuItems.Add(new MenuItem("Receivables", string.Empty, "Acct_Receivables"));
            ddlNew.MenuItems.Add(new MenuItem("Payment Received", string.Empty, "Acct_PaymentReceived"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem("Invoice Records", string.Empty, "Acct_InvoiceRecords"));
            ddlNew.MenuItems.Add(new MenuItem("Invoice List", string.Empty, "Acct_InvoiceList"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem("Order Records", string.Empty, "Acct_OrderRecords"));

            ToolBarButton cmdNew = new ToolBarButton("New", "New");
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            tbProducts.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(cmdMenuClick);

            // cmdImport
            ContextMenu ddlImport = new ContextMenu();
            ddlImport.MenuItems.Add(new MenuItem("Product Code [Excel]", string.Empty, "ProductCodeImport"));
            ddlImport.MenuItems.Add(new MenuItem("Barcode Code [UIFD]", string.Empty, "BarcodeImport"));
            ddlImport.MenuItems[1].Enabled = false;

            ToolBarButton cmdImport = new ToolBarButton("Import", "Import");
            cmdImport.Style = ToolBarButtonStyle.DropDownButton;
            cmdImport.Image = new IconResourceHandle("16x16.ico_16_4407.gif");
            cmdImport.DropDownMenu = ddlImport;

            tbProducts.Buttons.Add(cmdImport);
            cmdImport.MenuClick += new MenuEventHandler(cmdMenuClick);

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            tbProducts.Buttons.Add(sep);

            //cmdReports
            ContextMenu ddlReports = new ContextMenu();
            ddlReports.MenuItems.Add(new MenuItem("Product Master List", string.Empty, "Product Master List"));
            ddlReports.MenuItems.Add(new MenuItem("Product Barcode List", string.Empty, "Product Barcode List"));
            ddlReports.MenuItems.Add(new MenuItem("-"));
            ddlReports.MenuItems.Add(new MenuItem("Dimension List", string.Empty, "Dimension List"));
            ddlReports.MenuItems.Add(new MenuItem("Currency List", string.Empty, "Currency List"));
            ddlReports.MenuItems.Add(new MenuItem("Payment List", string.Empty, "Payment List"));

            ToolBarButton cmdReports = new ToolBarButton("Reports", "Reports");
            cmdReports.Style = ToolBarButtonStyle.DropDownButton;
            cmdReports.Image = new IconResourceHandle("16x16.16_reports.gif");
            cmdReports.DropDownMenu = ddlReports;

            tbProducts.Buttons.Add(cmdReports);
            cmdReports.MenuClick += new MenuEventHandler(cmdMenuClick);

            tbProducts.Buttons.Add(sep);

            if (atsPaneCtrl != null)
            {
                atsPaneCtrl.Controls.Add(tbProducts);
            }
        }

        void cmdMenuClick(object sender, MenuItemEventArgs e)
        {
            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "class6 list":
                        //xFilm5.Product.Reports.RptClass6List c6List = new xFilm5.Product.Reports.RptClass6List();
                        //c6List.ShowDialog();
                        break;
                    case "currency list":
                        //xFilm5.SystemSettings.Reports.RptCurrencyList currencyList = new xFilm5.SystemSettings.Reports.RptCurrencyList();
                        //currencyList.ShowDialog();
                        break;
                    case "dimension list":
                        //xFilm5.Product.Reports.RptDimensionList dimList = new xFilm5.Product.Reports.RptDimensionList();
                        //dimList.ShowDialog();
                        break;
                    case "payment list":
                        //xFilm5.Product.Reports.RptPaymentList paymentList = new xFilm5.Product.Reports.RptPaymentList();
                        //paymentList.ShowDialog();
                        break;
                    case "a1combin list":
                        //xFilm5.Product.Reports.RptAppendix1CombinList a1combinList = new xFilm5.Product.Reports.RptAppendix1CombinList();
                        //a1combinList.ShowDialog();
                        break;
                    case "a2combin list":
                        //xFilm5.Product.Reports.RptAppendix2CombinList a2combinList = new xFilm5.Product.Reports.RptAppendix2CombinList();
                        //a2combinList.ShowDialog();
                        break;
                    case "a3combin list":
                        //xFilm5.Product.Reports.RptAppendix3CombinList a3combinList = new xFilm5.Product.Reports.RptAppendix3CombinList();
                        //a3combinList.ShowDialog();
                        break;
                }
            }
        }
    }
}
