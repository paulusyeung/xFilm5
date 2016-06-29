using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using xFilm5.DAL;

namespace xFilm5.Controls.Reporting
{
    public class Loader
    {
        public static void Invoice(int invoiceId)
        {
            Acct_INMaster invoice = Acct_INMaster.Load(invoiceId);
            if (invoice != null)
            {
                DataTable dt = DataSource.Invoice(invoiceId);

                xFilm5.Accounting.Reports.Invoice report1 = new xFilm5.Accounting.Reports.Invoice();
                report1.InvoiceId = invoiceId;
                report1.PageTitle = "CUSTOMER COPY";
                report1.DataSource = dt;
                report1.CreateDocument();

                xFilm5.Accounting.Reports.Invoice report2 = new xFilm5.Accounting.Reports.Invoice();
                report2.InvoiceId = invoiceId;
                report2.PageTitle = "ADMIN COPY";
                report2.DataSource = dt;
                report2.CreateDocument();

                report1.Pages.AddRange(report2.Pages);
                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "invoice";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void Statement(int clientId)
        {
            Client client = Client.Load(clientId);
            if (client != null)
            {
                DataTable dt = DataSource.Statement(clientId);

                xFilm5.Accounting.Reports.Statement report1 = new xFilm5.Accounting.Reports.Statement();
                report1.ClientId = clientId;
                report1.DataSource = dt;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "statement";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void PaymentNotice(int clientId, List<int> invoices)
        {
            if (invoices.Count > 0)
            {
                DataTable dt = DataSource.PaymentNotice(invoices);

                xFilm5.Accounting.Reports.PaymentNotice report1 = new xFilm5.Accounting.Reports.PaymentNotice();
                report1.DataSource = dt;
                report1.ClientId = clientId;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "payment_notice";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }
    }
}
