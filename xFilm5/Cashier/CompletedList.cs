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

#endregion

namespace xFilm5.Cashier
{
    public partial class CompletedList : UserControl
    {
        public CompletedList()
        {
            InitializeComponent();
        }

        private void CompletedList_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void SetPayNowAns()
        {
            this.ansPayNow.MenuHandle = false;
            this.ansPayNow.DragHandle = false;
            this.ansPayNow.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            ToolBarButton cmdTotalAmount = new ToolBarButton("TotalAmount", "付現︰Total Amount = {0}");
            cmdTotalAmount.Tag = "TotalAmount";
//            cmdTotalAmount.Image = new IconResourceHandle("16x16.jobOrder_billing.png");

            this.ansPayNow.Buttons.Add(cmdTotalAmount);
            this.ansPayNow.Buttons.Add(sep);
        }

        private void BindPayNowList()
        {
            decimal totalAmount = 0;
            string sql = String.Format("CONVERT(NVARCHAR(10), TxDate, 120) = '{0}' AND TxType = {1}", DateTime.Now.ToString("yyyy-MM-dd"), ((int)xFilm5.DAL.Common.Enums.PosTxType.PayNow).ToString());
            string[] orderBy = { "TxDate" };
            PosTxCollection items = PosTx.LoadCollection(sql, orderBy, true);
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ListViewItem objItem = this.lvwPayNow.Items.Add((i + 1).ToString());  // Line Number
                    objItem.SubItems.Add(items[i].TxDate.ToString("yyyy-MM-dd HH:mm"));
                    objItem.SubItems.Add(items[i].TxRef);
                    objItem.SubItems.Add(items[i].TxAmount.ToString("$#,##0.00"));

                    totalAmount += items[i].TxAmount;
                }
                ansPayNow.Buttons[0].Text = String.Format(ansPayNow.Buttons[0].Text, totalAmount.ToString("$#,##0.00"));
            }
        }

        private void SetPayLaterAns()
        {
            this.ansPayLater.MenuHandle = false;
            this.ansPayLater.DragHandle = false;
            this.ansPayLater.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            ToolBarButton cmdTotalAmount = new ToolBarButton("TotalAmount", "記帳︰Total Amount = {0}");
            cmdTotalAmount.Tag = "TotalAmount";
//            cmdTotalAmount.Image = new IconResourceHandle("16x16.jobOrder_billing.png");

            this.ansPayLater.Buttons.Add(cmdTotalAmount);
            this.ansPayLater.Buttons.Add(sep);
        }

        private void BindPayLaterList()
        {
            decimal totalAmount = 0;
            string sql = String.Format("CONVERT(NVARCHAR(10), TxDate, 120) = '{0}' AND TxType = {1}", DateTime.Now.ToString("yyyy-MM-dd"), ((int)xFilm5.DAL.Common.Enums.PosTxType.PayLater).ToString());
            string[] orderBy = { "TxDate" };
            PosTxCollection items = PosTx.LoadCollection(sql, orderBy, true);
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ListViewItem objItem = this.lvwPayLater.Items.Add((i + 1).ToString());  // Line Number
                    objItem.SubItems.Add(items[i].TxDate.ToString("yyyy-MM-dd HH:mm"));
                    objItem.SubItems.Add(items[i].TxRef);
                    objItem.SubItems.Add(items[i].TxAmount.ToString("$#,##0.00"));

                    totalAmount += items[i].TxAmount;
                }
                ansPayLater.Buttons[0].Text = String.Format(ansPayLater.Buttons[0].Text, totalAmount.ToString("$#,##0.00"));
            }
        }

        private void ShowCompletedList()
        {
            this.BackColor = Color.WhiteSmoke;
            this.boxUnlock.Visible = false;

            splitContainer1.Visible = true;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.SplitterDistance = 420;
            lvwPayNow.Dock = DockStyle.Fill;
            lvwPayLater.Dock = DockStyle.Fill;

            SetPayNowAns();
            BindPayNowList();

            SetPayLaterAns();
            BindPayLaterList();
        }

        private bool ValidatePassword(string password)
        {
            bool result = false;

            Client_User user = Client_User.Load(xFilm5.DAL.Common.Config.CurrentUserId);
            if (user != null)
            {
                if (user.Password == password)
                {
                    result = true;
                }
            }

            return result;
        }

        private void txtPassword_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            string password = txtPassword.Text.Trim();
            if (ValidatePassword(password))
            {
                ShowCompletedList();
            }
            else
            {
                txtPassword.Text = String.Empty;
                txtPassword.Focus();
            }
        }
    }
}