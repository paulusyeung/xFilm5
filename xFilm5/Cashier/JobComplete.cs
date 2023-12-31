#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;

#endregion

namespace xFilm5.Cashier
{
    public partial class JobComplete : UserControl
    {
        private enum EditMode { None, PayNow, PayLater };
        private EditMode _Mode = EditMode.None;
        private List<string> _Invoices = new List<string>();

        public JobComplete()
        {
            InitializeComponent();
        }

        private void ShowPaymentBox(EditMode mode)
        {
            switch (mode)
            {
                case EditMode.None:
                    boxInvoiceList.Text = String.Empty;
                    boxInvoiceList.ForeColor = Color.Black;
                    boxInvoiceList.Visible = false;

                    _Invoices.Clear();
                    flpInvoiceList.Controls.Clear();
                    txtInvoiceNumber.Text = String.Empty;
                    cmdPayNow.Visible = true;
                    cmdPayLater.Visible = true;
                    break;
                case EditMode.PayNow:
                    boxInvoiceList.Text = "付現";
                    boxInvoiceList.ForeColor = Color.Red;
                    boxInvoiceList.Visible = true;
                    cmdPayNow.Visible = false;
                    cmdPayLater.Visible = false;
                    txtInvoiceNumber.Focus();
                    break;
                case EditMode.PayLater:
                    boxInvoiceList.Text = "記帳";
                    boxInvoiceList.ForeColor = Color.Green;
                    boxInvoiceList.Visible = true;
                    cmdPayNow.Visible = false;
                    cmdPayLater.Visible = false;
                    txtInvoiceNumber.Focus();
                    break;
            }
            _Mode = mode;
        }

        private void AddInvoice(string invoiceNumber)
        {
            if (VerifyInvoice(invoiceNumber))
            {
                flpInvoiceList.Controls.Add(GetInvoiceItemPane(invoiceNumber));
                _Invoices.Add(invoiceNumber);
            }
        }

        private bool VerifyInvoice(string invoiceNumber)
        {
            bool result = false; ;

            string sql = String.Format("InvoiceNumber = {0}", invoiceNumber);
            Acct_INMaster invoice = Acct_INMaster.LoadWhere(sql);
            if (invoice != null)
            {
                if (invoice.Status == (int)xFilm5.DAL.Common.Enums.Status.Active)
                {
                    result = true;
                }
            }

            return result;
        }

        private void SaveInvoiceList()
        {
            foreach (string item in _Invoices)
            {
                string sql = String.Format("InvoiceNumber = {0}", item);
                Acct_INMaster inv = Acct_INMaster.LoadWhere(sql);
                if (inv != null)
                {
                    xFilm5.DAL.Common.Order.MoveToCompleted(inv.OrderID);

                    #region log PosTx
                    PosTx pos = new PosTx();
                    pos.TxDate = DateTime.Now;
                    pos.TxType = (int)_Mode;
                    pos.TxRef = item;
                    pos.TxAmount = inv.InvoiceAmount;
                    pos.CreatedBy = xFilm5.DAL.Common.Config.CurrentUserId;
                    pos.Save();
                    #endregion
                }
            }
        }

        private Panel GetInvoiceItemPane(string invoiceNumber)
        {
            int sizeWidth = 230; int sizeHeight = 90;
            string sql = String.Format("InvoiceNumber = {0}", invoiceNumber);
            Acct_INMaster invoice = Acct_INMaster.LoadWhere(sql);

            Panel itemPane = new Panel();
            itemPane.Size = new Size(sizeWidth, sizeHeight);
            itemPane.BackColor = Color.FromArgb(229, 230, 204);
            itemPane.Margin = new Padding(2);

            #region add line number
            Button cmdLN = new Button();
            cmdLN.Location = new Point(4, 4);
            cmdLN.Size = new Size(64, 64);
            cmdLN.Font = new Font("Verdana", 24);
            cmdLN.ForeColor = Color.FromArgb(166, 155, 99);
            cmdLN.Text = (_Invoices.Count + 1).ToString();
            itemPane.Controls.Add(cmdLN);
            #endregion

            #region add invoice number
            Label itemInvoiceNumber = new Label();
            itemInvoiceNumber.Location = new Point(80, 4);
            itemInvoiceNumber.Size = new Size(140, 30);
            itemInvoiceNumber.TextAlign = ContentAlignment.MiddleLeft;
            itemInvoiceNumber.Font = new Font("Verdana", 24);
            itemInvoiceNumber.ForeColor = Color.FromArgb(166, 155, 99);
            itemInvoiceNumber.Text = invoiceNumber;
            itemPane.Controls.Add(itemInvoiceNumber);
            #endregion

            #region add invoice amount
            Label itemInvoiceAmount = new Label();
            itemInvoiceAmount.Location = new Point(80, 36);
            itemInvoiceAmount.Size = new Size(140, 30);
            itemInvoiceAmount.TextAlign = ContentAlignment.MiddleLeft;
            itemInvoiceAmount.Font = new Font("Verdana", 24);
            itemInvoiceAmount.ForeColor = Color.FromArgb(166, 155, 99);
            itemInvoiceAmount.Text = invoice.InvoiceAmount.ToString("$#,##0.00");
            itemPane.Controls.Add(itemInvoiceAmount);
            #endregion

            #region add delete button
            #endregion

            return itemPane;
        }

        private void cmdPayNow_Click(object sender, EventArgs e)
        {
            ShowPaymentBox(EditMode.PayNow);
        }

        private void cmdPayLater_Click(object sender, EventArgs e)
        {
            ShowPaymentBox(EditMode.PayLater);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ShowPaymentBox(EditMode.None);
        }

        private void cmdDone_Click(object sender, EventArgs e)
        {
            SaveInvoiceList();

            ShowPaymentBox(EditMode.None);
        }

        private void txtInvoiceNumber_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            string invoiceNumber = txtInvoiceNumber.Text.Trim();
            if (invoiceNumber != String.Empty)
            {
                AddInvoice(invoiceNumber);
                txtInvoiceNumber.Text = String.Empty;
                txtInvoiceNumber.Focus();
            }
        }
    }
}