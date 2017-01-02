using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using xFilm5.DAL;

namespace xFilm5.JobOrder.Reports5
{
    public partial class DN_80mm : DevExpress.XtraReports.UI.XtraReport
    {
        private int _ReceiptId = 0;
        private string _PageTitle = String.Empty;
        private int recNumber = 1;

        public DN_80mm()
        {
            InitializeComponent();
        }

        #region public properties
        public int ReceiptId
        {
            set
            {
                _ReceiptId = value;
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
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            #region Set Captions
            lblDeliveryNote.Text = oDict.GetWord("delivery_note");
            lblTransactionNumber.Text = oDict.GetWordWithColon("transaction#");
            lblTransactionDate.Text = oDict.GetWordWithColon("date_time");
            lblBillTo.Text = oDict.GetWordWithColon("bill_to").ToUpper();
            lblShipTo.Text = oDict.GetWordWithColon("ship_to").ToUpper();
            lblQty.Text = oDict.GetWord("qty");
            lblDescription.Text = oDict.GetWord("item_description");
            lblAmount.Text = oDict.GetWord("amount");
            #endregion

            String sql = String.Empty;

            DAL.ReceiptHeader receipt = DAL.ReceiptHeader.Load(_ReceiptId);
            if (receipt != null)
            {
                #region 如果仲未確認，加水印: DRAFT
                if (receipt.Status == (int)Common.Enums.Status.Draft)
                {
                    this.Watermark.Text = "DRAFT";
                    this.Watermark.ShowBehind = true;

                    this.txtBarcode.Visible = false;
                    this.txtInvoiceNumber.Visible = false;
                    this.txtInvoiceDate.Visible = false;
                }
                #endregion

                #region bill to address info
                Client_AddressBook client = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", receipt.ClientId.ToString()));
                if (client != null)
                {
                    this.txtBillingAddress.Text = client.Name + Environment.NewLine + client.Address;
                    this.txtBillingTel.Text = String.Format("Tel.: {0}", client.Tel);
                }
                #endregion
                 
                #region HACK: 攞第一隻 dbo.OrderHeader 做代表
                sql = String.Format("ReceiptHeaderId = {0}", _ReceiptId.ToString());
                DAL.ReceiptDetail receiptDtl = DAL.ReceiptDetail.LoadWhere(sql);
                DAL.OrderPkPrintQueueVps pk = DAL.OrderPkPrintQueueVps.Load(receiptDtl.OrderPkPrintQueueVpsId);
                #endregion

                OrderHeader order = OrderHeader.Load(pk.OrderHeaderId);
                if (order != null)
                {
                    #region Woorkshop Address as Company Address
                    txtCompanyAddress.Text = xFilm5.Controls.Utility.Owner.GetWorkshopAddress(order.ProofingOp).Replace(@"\n", Environment.NewLine);
                    #endregion

                    #region ship to address info
                    Order_Details orderDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", order.ID.ToString()));
                    if (orderDetails != null)
                    {
                        switch (orderDetails.DeliveryMethod)
                        {
                            case (int)Common.Enums.DeliveryMethod.DeliverTo:
                                Client_AddressBook delivery = Client_AddressBook.Load(orderDetails.DeliveryAddr);
                                if (delivery != null)
                                {
                                    txtDeliveryAddress.Text = delivery.Name + Environment.NewLine + delivery.Address.Replace(@"\n", Environment.NewLine);
                                    txtDeliveryTel.Text = String.Format("Tel.: {0}", delivery.Tel);
                                }
                                break;
                            case (int)Common.Enums.DeliveryMethod.PickUp:
                                txtDeliveryAddress.Text = String.Format("*** {0} ***", oDict.GetWord("pick_up").ToUpper());
                                txtDeliveryTel.Visible = false;
                                break;
                        }
                    }
                    #endregion

                    //if (order.Status == (int)Common.Enums.Workflow.Completed)
                    //{
                    //    this.Watermark.Text = "RE-PRINT";
                    //    this.Watermark.ShowBehind = true;
                    //}
                }

                // 原本想印埋個 Staff Full Name，不過如果中文名就唔掂
                //DAL.Client_User creaatedBy = DAL.Client_User.Load(receipt.CreatedBy);
                //txtBarcode.Text = creaatedBy != null ? String.Format("{0} {1}", receipt.ReceiptNumber, creaatedBy.FullName) : receipt.ReceiptNumber;
            }

            this.txtBarcode.DataBindings.Add("Text", DataSource, "ReceiptNumber");
            this.txtInvoiceNumber.DataBindings.Add("Text", DataSource, "ReceiptNumber");
            this.txtInvoiceDate.DataBindings.Add("Text", DataSource, "ReceiptDate", "{0:yyyy-MM-dd HH:mm:ss}");
            //this.txtOrderID.DataBindings.Add("Text", DataSource, "OrderId");
            //this.txtOrderedBy.DataBindings.Add("Text", DataSource, "OrderedBy");
            //this.txtTimeIn.DataBindings.Add ("Text", DataSource , "ReceivedOn", "{0:yyyy-MM-dd HH:MM}");

            #region item databindings
            //this.txtItemCode.DataBindings.Add("Text", DataSource, "ItemCode");
            this.txtItemDescription.DataBindings.Add("Text", DataSource, "ItemDescription");
            this.txtItemQty.DataBindings.Add("Text", DataSource, "ItemQty", "{0:n0}");
            //this.txtItemUoM.DataBindings.Add("Text", DataSource, "ItemUoM");
            //this.txtItemUnitAmt.DataBindings.Add("Text", DataSource, "ItemUnitAmt", "{0:n2}");
            //this.txtItemDiscount.DataBindings.Add("Text", DataSource, "ItemDiscount", "{0:###}");
            this.txtItemAmount.DataBindings.Add("Text", DataSource, "ItemAmount", "{0:n2}");
            #endregion

            this.txtInvoiceAmount.DataBindings.Add("Text", DataSource, "ReceiptAmount", "{0:c2}");
            this.txtTimeStamp.Text = DateTime.Now.ToString("yyyyMMddHHmmss");

            // 2016.10.22 paulus: upgrade 之後唔 work，要喺 designer 改 Margin
            this.Margins.Top = 113;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.txtItemCode.Visible = false;
            //this.txtItem.Text = recNumber.ToString();
            recNumber++;
        }

    }
}
