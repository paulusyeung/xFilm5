using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using DevExpress.XtraReports.UI;

using xFilm5.DAL;

namespace xFilm5.Accounting.Reports
{
    public partial class Statement : DevExpress.XtraReports.UI.XtraReport
    {
        private int _ClientId = 0; int aging = 0;
        decimal totalDue = 0; decimal curMonth = 0; decimal lastMonth = 0; decimal os2Months = 0; decimal os3Months = 0; decimal osAmount = 0;

        public Statement()
        {
            InitializeComponent();
        }

        #region public properties
        public int ClientId
        {
            set
            {
                _ClientId = value;
            }
        }
        #endregion

        private void Statement_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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
                    this.txtClientId.Text = client.ID.ToString();
                    this.txtClientName.Text = client.Name;
                    this.txtClientAddress.Text = address.Address;
                    this.txtTel.Text = String.Format("Tel.: {0}", address.Tel);
                    this.txtFax.Text = String.Format("Fax: {0}", address.Fax);
                }
                #endregion
            }

            this.txtToday.Text = DateTime.Now.ToString("dd/MM/yyyy");

            #region item databindings
            this.txtDate.DataBindings.Add("Text", DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.txtInvoiceNumber.DataBindings.Add("Text", DataSource, "InvoiceNumber", "{0:###}");
            this.txtOrderNumber.DataBindings.Add("Text", DataSource, "OrderID", "{0:###}");
            this.txtRemarks.DataBindings.Add("Text", DataSource, "Remarks");
            this.txtAmount.DataBindings.Add("Text", DataSource, "OsAmount", "{0:$#,##0.00}");
            #endregion

            //this.txtInvoiceAmount.DataBindings.Add("Text", DataSource, "InvoiceAmount", "{0:c2}");
            this.txtTimeStamp.Text = DateTime.Now.ToString("yyyyMMddHHMM");

            //this.Margins.Top = 113;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            aging = (System.Int32)GetCurrentColumnValue("Aging");
            osAmount = (System.Decimal)GetCurrentColumnValue("OsAmount");

            switch (aging)
            {
                case 0:
                    curMonth += osAmount;
                    break;
                case 1:
                    lastMonth += osAmount;
                    break;
                case 2:
                    os2Months += osAmount;
                    break;
                case 3:
                    os3Months += osAmount;
                    break;
            }
            totalDue += osAmount;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.txt90Days.Text = os3Months.ToString("$#,##0.00");
            this.txt60Days.Text = (os2Months + os3Months).ToString("$#,##0.00");
            this.txt30Days.Text = lastMonth.ToString("$#,##0.00");
            this.txtCurrent.Text = curMonth.ToString("$#,##0.00");
            this.txtTotal.Text = totalDue.ToString("$#,##0.00");
        }

    }
}
