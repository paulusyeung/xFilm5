using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using DevExpress.XtraReports.UI;
using System.Linq;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Reports
{
    public partial class Invoice : DevExpress.XtraReports.UI.XtraReport
    {
        private int _ClientId = 0; int aging = 0, _Year = 2017, _Month = 1;
        private Decimal _TotalAmount = 0;
        private String _InvoiceNumber = "";

        decimal totalDue = 0; decimal curMonth = 0;
        decimal lastMonth = 0; decimal os2Months = 0; decimal os3Months = 0; decimal osAmount = 0;

        #region public properties
        public int ClientId
        {
            set { _ClientId = value; }
        }
        public int Year
        {
            set { _Year = value; }
        }
        public int Month
        {
            set { _Month = value; }
        }
        public String InvoiceNumber
        {
            set { _InvoiceNumber = value; }
        }
        public Decimal TotalAmount
        {
            set { _TotalAmount = value; }
        }
        #endregion

        public Invoice()
        {
            InitializeComponent();
        }

        private void Statement_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //GroupFooter1.Visible = false;
            this.Watermark.Text = "RE-PRINT";
            this.Watermark.ShowBehind = true;

            _ClientId = (int)GetCurrentColumnValue("ClientId");

            using (var ctx = new EF6.xFilmEntities())
            {
                this.lblCompanyName.Text = OwnerHelper.GetOwnerName();
                this.lblCompanyAddress.Text = OwnerHelper.GetOwnerAddress();

                var client = ctx.Client.Where(x => x.ID == _ClientId).SingleOrDefault();
                if (client != null)
                {
                    #region fill client info
                    var address = ctx.Client_AddressBook.Where(x => x.ClientID == _ClientId && x.PrimaryAddr == true).SingleOrDefault();
                    if (address != null)
                    {
                        this.txtInvoiceNumber.Text = client.ID.ToString();
                        this.txtClientName.Text = client.Name;
                        this.txtClientAddress.Text = address.Address;
                        this.txtTel.Text = String.Format("Tel.: {0}", address.Tel);
                        this.txtFax.Text = String.Format("Fax: {0}", address.Fax);
                    }
                    #endregion
                }
            }

            /**
            Client owner = Client.Load(xFilm5.Controls.Utility.Owner.GetOwnerId());

            this.lblCompanyName.Text = xFilm5.Controls.Utility.Owner.GetOwnerName();
            this.lblCompanyAddress.Text = xFilm5.Controls.Utility.Owner.GetOwnerAddress();

            Client client = Client.Load(_ClientId);
            if (client != null)
            {
                #region fill client info
                Client_AddressBook address = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", _ClientId.ToString()));
                if (address != null)
                {
                    this.txtInvoiceNumber.Text = client.ID.ToString();
                    this.txtClientName.Text = client.Name;
                    this.txtClientAddress.Text = address.Address;
                    this.txtTel.Text = String.Format("Tel.: {0}", address.Tel);
                    this.txtFax.Text = String.Format("Fax: {0}", address.Fax);
                }
                #endregion
            }
            */

            //DateTime billedOn = (new DateTime(_Year, _Month, 1)).AddMonths(1).AddDays(-1);      // default 係月底
            this.txtInvoiceDate.DataBindings.Add("Text", DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.txtInvoiceAmount.DataBindings.Add("Text", DataSource, "InvoiceAmount", "{0:$#,###.00;($#,###.00); }");
            this.txtInvoiceNumber.DataBindings.Add("Text", DataSource, "InvoiceNumber");

            #region item databindings
            //this.txtDate.DataBindings.Add("Text", DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            //this.txtDNNumber.DataBindings.Add("Text", DataSource, "InvoiceNumber", "{0:###}");
            this.txtOrderNumber.DataBindings.Add("Text", DataSource, "OrderHeaderId", "{0:###}");
            this.txtRemarks.DataBindings.Add("Text", DataSource, "ItemDescription");
            this.txtAmount.DataBindings.Add("Text", DataSource, "ItemAmount", "{0:$#,###.00;($#,###.00); }");
            #endregion

            //this.txtInvoiceAmount.DataBindings.Add("Text", DataSource, "InvoiceAmount", "{0:c2}");
            this.txtTimeStamp.Text = DateTime.Now.ToString("yyyyMMddHHMM");

            //this.Margins.Top = 113;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //String description = (System.String)GetCurrentColumnValue("ItemDescription");
            //txtRemarks.Text = description.Substring(10);

            int pqId = (int)GetCurrentColumnValue("OrderPkPrintQueueVpsId");
            using (var ctx = new EF6.xFilmEntities())
            {
                var receiptDtl = ctx.ReceiptDetail.Where(x => x.OrderPkPrintQueueVpsId == pqId).SingleOrDefault();
                if (receiptDtl != null)
                {
                    txtDate.Text = receiptDtl.ReceiptHeader.ReceiptDate.Value.ToString("yyyy-MM-dd");
                    txtDNNumber.Text = receiptDtl.ReceiptHeader.ReceiptNumber;
                }
            }


            osAmount = (System.Decimal)GetCurrentColumnValue("ItemAmount");
            curMonth += osAmount;
            totalDue += osAmount;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.txtTotal.Text = totalDue.ToString("$#,##0.00");
        }

    }
}
