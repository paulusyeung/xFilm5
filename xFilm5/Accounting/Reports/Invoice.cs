using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using xFilm5.DAL;

namespace xFilm5.Accounting.Reports
{
    public partial class Invoice : DevExpress.XtraReports.UI.XtraReport
    {
        private int _InvoiceId = 0;
        private string _PageTitle = String.Empty;
        private int recNumber = 1;

        public Invoice()
        {
            InitializeComponent();
        }

        #region public properties
        public int InvoiceId
        {
            set
            {
                _InvoiceId = value;
            }
        }
        public string PageTitle
        {
            set
            {
                _PageTitle = value;
            }
        }
        #endregion

        private void Invoice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            if (invoice != null)
            {
                if (invoice.Status == (int)Common.Enums.Status.Draft)
                {
                    this.Watermark.Text = "DRAFT";
                    this.Watermark.ShowBehind = true;

                    this.txtBarcode.Visible = false;
                    this.txtInvoiceNumber.Visible = false;
                    this.txtInvoiceDate.Visible = false;
                }

                #region billing address info
                Client_AddressBook client = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", invoice.ClientID.ToString()));
                if (client != null)
                {
                    this.txtBillingAddress.Text = client.Name + Environment.NewLine + client.Address;
                    this.txtBillingTel.Text = String.Format("Tel.: {0}", client.Tel);
                    this.txtBillingFax.Text = String.Format("Fax: {0}", client.Fax);
                }
                #endregion

                OrderHeader order = OrderHeader.Load(invoice.OrderID);
                if (order != null)
                {
                    #region PrepressOp, CheckedBy, TimeOut
                    txtPrepressOp.Text = String.Empty;
                    txtCheckedBy.Text = String.Empty;
                    txtTimeOut.Text = String.Empty;
                    Order_Journal journal = Order_Journal.LoadWhere(String.Format("OrderID = {0} AND Status = 3", order.ID.ToString()));
                    if (journal != null)
                    {
                        Client_User prepressOp = Client_User.Load(journal.UserID);
                        if (prepressOp != null)
                        {
                            txtPrepressOp.Text = prepressOp.FullName;
                        }
                    }
                    journal = Order_Journal.LoadWhere(String.Format("OrderID = {0} AND Status = 7", order.ID.ToString()));
                    if (journal != null)
                    {
                        Client_User checkedBy = Client_User.Load(journal.UserID);
                        if (checkedBy != null)
                        {
                            txtCheckedBy.Text = checkedBy.FullName;
                            txtTimeOut.Text = journal.DateUpdated.ToString("yyyy-MM-dd HH:MM");
                        }
                    }
                    #endregion

                    #region delivery address info
                    txtDeliverTo.Text = String.Empty;
                    txtPickup.Text = String.Empty;
                    Order_Details orderDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", order.ID.ToString()));
                    if (orderDetails != null)
                    {
                        switch (orderDetails.DeliveryMethod)
                        {
                            case (int)Common.Enums.DeliveryMethod.DeliverTo:
                                txtDeliverTo.Text = "X";
                                Client_AddressBook delivery = Client_AddressBook.Load(orderDetails.DeliveryAddr);
                                if (delivery != null)
                                {
                                    txtDeliveryAddress.Text = delivery.Name + Environment.NewLine + delivery.Address;
                                    txtDeliveryTel.Text = String.Format("Tel.: {0}", delivery.Tel);
                                    txtDeliveryFax.Text = String.Format("Fax: {0}", delivery.Fax);
                                }
                                break;
                            case (int)Common.Enums.DeliveryMethod.PickUp:
                                txtPickup.Text = "X";
                                txtDeliveryAddress.Visible = false;
                                txtDeliveryTel.Visible = false;
                                txtDeliveryFax.Visible = false;
                                break;
                        }
                    }
                    #endregion

                    if (order.Status == (int)Common.Enums.Workflow.Completed)
                    {
                        this.Watermark.Text = "RE-PRINT";
                        this.Watermark.ShowBehind = true;
                    }
                }
            }

            this.txtBarcode.DataBindings.Add("Text", DataSource, "InvoiceNumber");
            this.txtInvoiceNumber.DataBindings.Add("Text", DataSource, "InvoiceNumber");
            this.txtInvoiceDate.DataBindings.Add("Text", DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.txtPaidBy.DataBindings.Add("Text", DataSource, "PaidBy");
            this.txtOrderID.DataBindings.Add("Text", DataSource, "OrderId");
            this.txtOrderedBy.DataBindings.Add("Text", DataSource, "OrderedBy");
            this.txtTimeIn.DataBindings.Add ("Text", DataSource , "ReceivedOn", "{0:yyyy-MM-dd HH:MM}");

            #region item databindings
            this.txtItemCode.DataBindings.Add("Text", DataSource, "ItemCode");
            this.txtItemDescription.DataBindings.Add("Text", DataSource, "ItemDescription");
            this.txtItemQty.DataBindings.Add("Text", DataSource, "ItemQty", "{0:n0}");
            this.txtItemUoM.DataBindings.Add("Text", DataSource, "ItemUoM");
            this.txtItemUnitAmt.DataBindings.Add("Text", DataSource, "ItemUnitAmt", "{0:n2}");
            this.txtItemDiscount.DataBindings.Add("Text", DataSource, "ItemDiscount", "{0:###}");
            this.txtItemAmount.DataBindings.Add("Text", DataSource, "ItemAmount", "{0:n2}");
            #endregion

            this.txtRemarks.DataBindings.Add("Text", DataSource, "Remarks");
            this.txtInvoiceAmount.DataBindings.Add("Text", DataSource, "InvoiceAmount", "{0:c2}");
            this.txtPageTitle.Text = _PageTitle;
            this.txtTimeStamp.Text = DateTime.Now.ToString("yyyyMMddHHMM");

            this.Margins.Top = 113;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.txtItemCode.Visible = false;
            this.txtItem.Text = recNumber.ToString();
            recNumber++;
        }

    }
}
