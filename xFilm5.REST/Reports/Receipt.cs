using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using xFilm5.EF6;
using System.Linq;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Reports
{
    public partial class Receipt : DevExpress.XtraReports.UI.XtraReport
    {
        private int _ReceiptId = 0;
        private int _LangId = 1;
        private string _PageTitle = String.Empty;
        private int recNumber = 1;

        public Receipt()
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
            String sql = String.Empty;

            using (var ctx = new xFilmEntities())
            {
                var receipt = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == _ReceiptId).SingleOrDefault();
                if (receipt != null)
                {
                    #region 如果仲未確認，加水印: DRAFT; 如果超過 12 小時，加水印: RE-PRINT
                    if (receipt.Status == (int)CommonHelper.Enums.Status.Draft)
                    {
                        this.Watermark.Text = "DRAFT";
                        this.Watermark.ShowBehind = true;

                        this.txtBarcode.Visible = false;
                        this.txtInvoiceNumber.Visible = false;
                        this.txtInvoiceDate.Visible = false;
                    }
                    else
                    {
                        var timespan = DateTime.Now - receipt.CreatedOn;
                        if (timespan.Hours > 12)
                        {
                            this.Watermark.Text = "RE-PRINT";
                            this.Watermark.ShowBehind = true;

                            //this.txtBarcode.Visible = false;
                            //this.txtInvoiceNumber.Visible = false;
                            //this.txtInvoiceDate.Visible = false;
                        }
                    }
                    #endregion

                    #region bill to address info
                    var client = ctx.Client_AddressBook.Where(x => x.ClientID == receipt.ClientId && x.PrimaryAddr == true).SingleOrDefault();
                    if (client != null)
                    {
                        this.txtBillingAddress.Text = client.Name + Environment.NewLine + client.Address;
                        this.txtBillingTel.Text = String.Format("Tel.: {0}", client.Tel);

                        _LangId = ClientHelper.GetDefaultLanguageId(client.ID);
                        SetCaptions(_LangId);
                    }
                    #endregion

                    #region HACK: 攞第一隻 dbo.OrderHeader 做代表
                    var receiptDtl = ctx.ReceiptDetail.Where(x => x.ReceiptHeaderId == _ReceiptId).FirstOrDefault();
                    var pk = ctx.OrderPkPrintQueueVps.Where(x => x.OrderPkPrintQueueVpsId == receiptDtl.OrderPkPrintQueueVpsId).SingleOrDefault();
                    //sql = String.Format("ReceiptHeaderId = {0}", _ReceiptId.ToString());
                    //DAL.ReceiptDetail receiptDtl = DAL.ReceiptDetail.LoadWhere(sql);
                    //DAL.OrderPkPrintQueueVps pk = DAL.OrderPkPrintQueueVps.Load(receiptDtl.OrderPkPrintQueueVpsId);
                    #endregion

                    var order = ctx.OrderHeader.Where(x => x.ID == pk.OrderHeaderId).SingleOrDefault();
                    if (order != null)
                    {
                        #region Woorkshop Address as Company Address
                        txtCompanyAddress.Text = OwnerHelper.GetWorkshopAddress(order.ProofingOp.Value).Replace(@"\n", Environment.NewLine);
                        #endregion

                        #region ship to address info
                        var orderDetails = ctx.Order_Details.Where(x => x.OrderID == order.ID).SingleOrDefault();
                        if (orderDetails != null)
                        {
                            switch (orderDetails.DeliveryMethod)
                            {
                                case (int)CommonHelper.Enums.DeliveryMethod.DeliverTo:
                                    var delivery = ctx.Client_AddressBook.Where(x => x.ID == orderDetails.DeliveryAddr).SingleOrDefault();
                                    if (delivery != null)
                                    {
                                        txtDeliveryAddress.Text = delivery.Name + Environment.NewLine + delivery.Address.Replace(@"\n", Environment.NewLine);
                                        txtDeliveryTel.Text = String.Format("Tel.: {0}", delivery.Tel);
                                    }
                                    break;
                                case (int)CommonHelper.Enums.DeliveryMethod.PickUp:
                                    var pickup = _LangId == 3 ? "Pick Up" : (_LangId == 2 ? "来取" : "來取");
                                    txtDeliveryAddress.Text = String.Format("*** {0} ***", pickup);
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

        private void SetCaptions(int langId)
        {
            #region Set Captions
            lblDeliveryNote.Text = langId == 3 ? "DELIVERY NOTE" : (langId == 2 ? "收货单" : "收貨單");              // oDict.GetWordWithColon("delivery_note");
            lblTransactionNumber.Text = langId == 3 ? "Transaction #:" : (langId == 2 ? "票据号码：" : "票據號碼：");      // oDict.GetWordWithColon("transaction#");
            lblTransactionDate.Text = langId == 3 ? "Date Time:" : (langId == 2 ? "票据日期：" : "票據日期：");            // oDict.GetWordWithColon("date_time");
            lblBillTo.Text = langId == 3 ? "BILL TO:" : (langId == 2 ? "收单地址：" : "收單地址：");                        // oDict.GetWordWithColon("bill_to").ToUpper();
            lblShipTo.Text = langId == 3 ? "SHIP TO:" : (langId == 2 ? "收货地址：" : "收貨地址：");                        // oDict.GetWordWithColon("ship_to").ToUpper();
            lblQty.Text = langId == 3 ? "Qty" : (langId == 2 ? "数量" : "數量");                                      // oDict.GetWord("qty");
            lblDescription.Text = langId == 3 ? "Item Description" : (langId == 2 ? "名称" : "名稱");                 // oDict.GetWord("item_description");
            lblAmount.Text = langId == 3 ? "Amount" : (langId == 2 ? "单价" : "單價");                                // oDict.GetWord("amount");
            #endregion
        }
    }
}
