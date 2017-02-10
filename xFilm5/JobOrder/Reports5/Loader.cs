using Gizmox.WebGUI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using xFilm5.DAL;

namespace xFilm5.JobOrder.Reports5
{
    public class Loader
    {
        public static void Invoice(int invoiceId)
        {
            Acct_INMaster invoice = Acct_INMaster.Load(invoiceId);
            if (invoice != null)
            {
                DataTable dt = DataSource.Inv5(invoiceId);

                xFilm5.JobOrder.Reports5.Invoice_A4 report1 = new xFilm5.JobOrder.Reports5.Invoice_A4();
                report1.InvoiceId = invoiceId;
                report1.PageTitle = "CUSTOMER COPY";
                report1.DataSource = dt;
                report1.CreateDocument();

                xFilm5.JobOrder.Reports5.Invoice_A4 report2 = new xFilm5.JobOrder.Reports5.Invoice_A4();
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

        public static void INV_A4(int invoiceId)
        {
            Acct_INMaster invoice = Acct_INMaster.Load(invoiceId);
            if (invoice != null)
            {
                DataTable dt = DataSource.DN(invoiceId);

                xFilm5.JobOrder.Reports5.INV_A4 report1 = new xFilm5.JobOrder.Reports5.INV_A4();
                report1.ReceiptId = invoiceId;
                report1.DataSource = dt;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "invoice_a4";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void INV_80mm(int invoiceId)
        {
            Acct_INMaster invoice = Acct_INMaster.Load(invoiceId);
            if (invoice != null)
            {
                DataTable dt = DataSource.Inv5(invoiceId);

                xFilm5.JobOrder.Reports5.INV_80mm report1 = new xFilm5.JobOrder.Reports5.INV_80mm();
                report1.InvoiceId = invoiceId;
                report1.DataSource = dt;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "invoice";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void DN_80mm(int receiptId)
        {
            ReceiptHeader receipt = ReceiptHeader.Load(receiptId);
            if (receipt != null)
            {
                DataTable dt = DataSource.DN(receiptId);

                xFilm5.JobOrder.Reports5.DN_80mm report1 = new xFilm5.JobOrder.Reports5.DN_80mm();
                report1.ReceiptId = receiptId;
                report1.DataSource = dt;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                report1.ExportToPdf(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "delivery_note";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void DN_80mm_Html(int receiptId)
        {
            ReceiptHeader receipt = ReceiptHeader.Load(receiptId);
            if (receipt != null)
            {
                DataTable dt = DataSource.DN(receiptId);

                xFilm5.JobOrder.Reports5.DN_80mm report1 = new xFilm5.JobOrder.Reports5.DN_80mm();
                report1.ReceiptId = receiptId;
                report1.DataSource = dt;
                report1.CreateDocument();

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                report1.ExportToHtml(memStream);

                xFilm5.Controls.Reporting.Viewer viewer = new xFilm5.Controls.Reporting.Viewer();
                viewer.ReportName = "delivery_note";
                viewer.BinarySource = memStream;
                viewer.Show();
            }
        }

        public static void HtmlEmail(int receiptId)
        {
            string strMailContent = "Welcome new user";
            string fromAddress = "yourname@yoursite.com";
            string toAddress = "newuser@hisdomain.com";
            string contentId = "image1";
            //string path = Server.MapPath(@"images/Logo.jpg"); // my logo is placed in images folder

            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            mailMessage.Bcc.Add("inkrajesh@hotmail.com"); // put your id here
            mailMessage.Subject = "Welcome new User";

            //LinkedResource logo = new LinkedResource(path);
            //logo.ContentId = "companylogo";
            ImageResourceHandle pic = new ImageResourceHandle("companylogo.png");
            LinkedResource img = new LinkedResource(pic.ToStream(), "image/png");
            img.ContentId = "companylogo";
            // done HTML formatting in the next line to display my logo
            AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + strMailContent, null, MediaTypeNames.Text.Html);
            //av1.LinkedResources.Add(logo);
            av1.LinkedResources.Add(img);

            mailMessage.AlternateViews.Add(av1);
            mailMessage.IsBodyHtml = true;
            SmtpClient mailSender = new SmtpClient("localhost"); //use this if you are in the development server
            mailSender.Send(mailMessage);
        }
    }
}