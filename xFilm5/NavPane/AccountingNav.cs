#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using xFilm5.Product;

#endregion

namespace xFilm5.NavPane
{
    public partial class AccountingNav : UserControl
    {
        public AccountingNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("accounting", this.navAccounting.Nodes);
        }

        private void navPurchase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navAccounting.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navAccounting.SelectedNode.Tag);
                ShowAppToolStrip((string)navAccounting.SelectedNode.Tag);
            }
        }

        #region Show private AppToolStrip
        private void ShowAppToolStrip(string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);
                if (controls.Length > 0)
                {
                    Panel atsPane = (Panel)controls[0];
// use the same AtsPane.AccountingAts
//                    atsPane.Controls.Clear();

                    switch (Tag.ToLower())
                    {
                        case "acct_receivables":
                            //xFilm5.Accounting.AgingAts oAging = new xFilm5.Accounting.AgingAts();
                            //oAging.Dock = DockStyle.Fill;
                            //atsPane.Controls.Clear();
                            //atsPane.Controls.Add(oAging);
                            break;
                    }
                }
            }
        }
        #endregion

        #region Show private Workspace
        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
                    case "acct_receivables":
                        xFilm5.Accounting.Aging oAging = new xFilm5.Accounting.Aging();
                        oAging.DockPadding.All = 6;
                        oAging.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAging);
                        break;
                    case "acct_invoicelist":
                        xFilm5.Accounting.InvoiceList oInvoiceList = new xFilm5.Accounting.InvoiceList();
                        oInvoiceList.DockPadding.All = 6;
                        oInvoiceList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oInvoiceList);
                        break;
                    case "acct_orderlist":
                        xFilm5.Accounting.OrderList_v5 oOrderList = new xFilm5.Accounting.OrderList_v5();
                        //xFilm5.Accounting.OrderList oOrderList = new xFilm5.Accounting.OrderList();
                        oOrderList.DockPadding.All = 6;
                        oOrderList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oOrderList);
                        break;
                    case "acct_dnlist":
                        xFilm5.Accounting.DNList_v5 oDNList = new xFilm5.Accounting.DNList_v5();
                        //xFilm5.Accounting.DNList oDNList = new xFilm5.Accounting.DNList();
                        oDNList.DockPadding.All = 6;
                        oDNList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oDNList);
                        break;
                    case "olap_receivables":
                        Accounting.Olap.ReceivablesOlap oReceivables = new Accounting.Olap.ReceivablesOlap();
                        //xFilm5.Controls.Reporting.OlapViewer oReceivables = new xFilm5.Controls.Reporting.OlapViewer();
                        oReceivables.DockPadding.All = 6;
                        oReceivables.Dock = DockStyle.Fill;
                        //oReceivables.AspxPagePath = @"Accounting\Olap\ReceivablesOlapPage.aspx";
                        wspPane.Controls.Add(oReceivables);
                        break;
                    case "olap_salesbycustomer":
                        Accounting.Olap.SalesByCustomerOlap oSalesByCustomer = new Accounting.Olap.SalesByCustomerOlap();
                        //xFilm5.Controls.Reporting.OlapViewer oSalesByCustomer = new xFilm5.Controls.Reporting.OlapViewer();
                        oSalesByCustomer.DockPadding.All = 6;
                        oSalesByCustomer.Dock = DockStyle.Fill;
                        //oSalesByCustomer.AspxPagePath = @"Accounting\Olap\SalesByCustomer.aspx";
                        wspPane.Controls.Add(oSalesByCustomer);
                        break;
                    case "olap_salesbyproduct":
                        Accounting.Olap.SalesByProductOlap oSalesByProduct = new Accounting.Olap.SalesByProductOlap();
                        //xFilm5.Controls.Reporting.OlapViewer oSalesByProduct = new xFilm5.Controls.Reporting.OlapViewer();
                        oSalesByProduct.DockPadding.All = 6;
                        oSalesByProduct.Dock = DockStyle.Fill;
                        //oSalesByProduct.AspxPagePath = @"Accounting\Olap\SalesByProduct.aspx";
                        wspPane.Controls.Add(oSalesByProduct);
                        break;
                    case "chart_revenuebybranch":
                        xFilm5.Controls.Reporting.OlapViewer oRevenueByBranch = new xFilm5.Controls.Reporting.OlapViewer();
                        oRevenueByBranch.DockPadding.All = 6;
                        oRevenueByBranch.Dock = DockStyle.Fill;
                        oRevenueByBranch.AspxPagePath = @"Accounting\Chart\RevenueByBranch.aspx";
                        wspPane.Controls.Add(oRevenueByBranch);
                        break;
                    case "chart_clienttrend":
                        xFilm5.Controls.Reporting.OlapViewer oClientTrend = new xFilm5.Controls.Reporting.OlapViewer();
                        oClientTrend.DockPadding.All = 6;
                        oClientTrend.Dock = DockStyle.Fill;
                        oClientTrend.AspxPagePath = @"Accounting\Chart\ClientTrend.aspx";
                        wspPane.Controls.Add(oClientTrend);
                        break;
                }
            }
        }
        #endregion
    }
}