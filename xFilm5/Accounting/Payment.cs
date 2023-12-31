#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;
using xFilm5.Helper;

#endregion

namespace xFilm5.Accounting
{
    public partial class Payment : Form
    {
        private List<int> _Invoices = new List<int>();
        private decimal _TotalAmount = 0;

        public Payment()
        {
            InitializeComponent();
        }

        #region public properties
        public List<int> Invoices
        {
            set
            {
                _Invoices = value;
            }
        }

        public decimal TotalAmount
        {
            set
            {
                _TotalAmount = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();

            ShowPayment();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            boxSelected.Text = oDict.GetWord("selected");
            boxPayment.Text = oDict.GetWord("payment");

            lblInvoices.Text = oDict.GetWordWithColon("invoice");
            lblInviceAmt.Text = oDict.GetWordWithColon("total_amount");
            lblPaymentAmount.Text = oDict.GetWordWithColon("amount");
            lblReference.Text = oDict.GetWordWithColon("reference");
            lblPaidOn.Text = oDict.GetWordWithColon("paid_on");
        }

        private void SetAnsToolbar()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", oDict.GetWord("save"));
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", System.Web.HttpUtility.UrlDecode(oDict.GetWord("save_close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            // cmdPrint
            ToolBarButton cmdPrint = new ToolBarButton("Print", oDict.GetWord("print"));
            cmdPrint.Tag = "Print";
            cmdPrint.Image = new IconResourceHandle("16x16.16_print.gif");

//            this.ansToolbar.Buttons.Add(cmdSave);
            this.ansToolbar.Buttons.Add(cmdSaveClose);
            this.ansToolbar.Buttons.Add(sep);
            this.ansToolbar.Buttons.Add(cmdPrint);

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            //toolTip1.SetToolTip(nudSecurityLevel, String.Format("Security Level:{0}2 = PrepressOp{0}3 = Supervisor{0}4 = Admin{0}5 = Power User{0}6 = Branch", Environment.NewLine));
            //toolTip1.SetToolTip(lblSecurityLevel, String.Format("Security Level:{0}2 = PrepressOp{0}3 = Supervisor{0}4 = Admin{0}5 = Power User{0}6 = Branch", Environment.NewLine));
        }
        #endregion

        #region Show(), Save(), Verify()
        private void ShowPayment()
        {
            txtInvoices.Text = _Invoices.Count.ToString("##0");
            txtTotalAmount.Text = _TotalAmount.ToString("#,##0.00");

            txtPaidAmount.Text = _TotalAmount.ToString();
        }

        private bool SavePayment()
        {
            bool result = false;
            string sql = String.Empty;

            if (VerifyPayment())
            {
                decimal amountRemain = _TotalAmount; decimal amountPaid = _TotalAmount; decimal osAmount = 0;

                foreach (int invoice in _Invoices)
                {
                    sql = String.Format("InvoiceNumber = {0}", invoice.ToString());
                    Acct_INMaster paidInvoice = Acct_INMaster.LoadWhere(sql);
                    if ((paidInvoice != null) && (amountRemain > 0))
                    {
                        osAmount = paidInvoice.InvoiceAmount - paidInvoice.PaidAmount;
                        if (amountRemain >= osAmount)
                        {
                            #region full payment
                            paidInvoice.Paid = true;
                            paidInvoice.PaidAmount = paidInvoice.InvoiceAmount;
                            paidInvoice.PaidOn = datPaidOn.Value;
                            paidInvoice.PaidRef = String.Format("{0} - {1}/{2}", txtReference.Text.Trim(), amountPaid.ToString("$#,##0.00"), osAmount.ToString("$#,##0.00"));
                            paidInvoice.Save();
                            #endregion
                            amountRemain = amountRemain - osAmount;

                            // 2017.05.31 paulus: v5 Update related RceiptHeader
                            InvoiceHelper.SetInvoiceToPaid(paidInvoice.ID, paidInvoice.PaidOn, paidInvoice.PaidRef);
                        }
                        else
                        {
                            #region paitial payment
                            paidInvoice.Paid = false;
                            paidInvoice.PaidAmount = paidInvoice.PaidAmount + amountRemain;
                            paidInvoice.PaidOn = datPaidOn.Value;
                            paidInvoice.PaidRef = String.Format("{0} - {1}/{2}", txtReference.Text.Trim(), amountPaid.ToString("$#,##0.00"), amountRemain.ToString("$#,##0.00"));
                            paidInvoice.Save();
                            #endregion
                            amountRemain = 0;
                        }
                    }
                }
                result = true;
            }

            return result;
        }

        private bool VerifyPayment()
        {
            bool result = true;
            string msg = String.Empty;

            try
            {
                decimal paidAmount = Convert.ToDecimal(txtPaidAmount.Text.Trim());
                if (paidAmount > _TotalAmount)
                {
                    result = false;
                    msg = msg + Environment.NewLine + "Too much...";
                }
            }
            catch { }


            if (!result)
            {
                MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return result;
        }
        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "print":
                        #region popup Payment List
                        if (_Invoices.Count > 0)
                        {
                            string sql = String.Format("InvoiceNumber = {0}", _Invoices[0].ToString());
                            DAL.Acct_INMaster inv = DAL.Acct_INMaster.LoadWhere(sql);
                            if (inv != null)
                            {
                                xFilm5.Controls.Reporting.Loader.PaymentNotice(inv.ClientID, _Invoices);
                            }
                        }
                        #endregion
                        break;
                }
            }
        }

        #region ans Button Clicks: Save, SaveClose, Delete
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SavePayment())
                    {
                        MessageBox.Show(String.Format("Payment {0} is saved!", ""), "Save Result");
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is ReadOnly...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdSaveClose_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SavePayment())
                    {
                        MessageBox.Show(String.Format("Payment {0} is saved!", ""), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdCloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}