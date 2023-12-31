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

namespace xFilm5.JobOrder
{
    public partial class Billing : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _InvoiceId = 0;
        private int _InvoiceNumber = 0;
        private bool _ShowItemPane = true;

        public Billing()
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
        public int OrderId
        {
            set
            {
                _OrderId = value;
            }
        }
        public Common.Enums.EditMode EditMode
        {
            set
            {
                _EditMode = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            SetDropdowns();
            ShowInvoice();
            SetEditModeLayout();
            this.Update();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            chkDeliverTo.Text = oDict.GetWordWithColon("deliver_to");
            chkPickup.Text = oDict.GetWord("pick_up");
            gbxBilling.Text = oDict.GetWord("billing_address");
            gbxDelivery.Text = oDict.GetWord("delivery_address");
            gbxItemPane.Text = oDict.GetWord("invoice_item");

            lblOrderId.Text = oDict.GetWord("order_id");
            lblInvoiceNumber.Text = oDict.GetWord("invoice_number");
            lblInvoiceDate.Text = oDict.GetWord("invoice_date");
            lblInvoiceAmount.Text = oDict.GetWord("invoice_amount");
            lblPaidBy.Text = oDict.GetWord("paid_by");

            colLN.Text = oDict.GetWord("ln");
            colItemCode.Text = oDict.GetWord("item_code");
            colDescription.Text = oDict.GetWord("item_description");
            colQty.Text = oDict.GetWord("qty");
            colUnitAmt.Text = oDict.GetWord("unit_amount");
            colDiscount.Text = oDict.GetWord("discount");
            colAmount.Text = oDict.GetWord("amount");

            lblRemarks.Text = oDict.GetWordWithColon("remarks");

            lblItemId.Text = oDict.GetWord("item_id");
            lblItemCode.Text = oDict.GetWord("item_code");
            lblItemDescription.Text = oDict.GetWord("item_description");
            lblItemQty.Text = oDict.GetWord("item_qty");
            lblItemDiscount.Text = oDict.GetWord("item_discount");
            lblItemAmount.Text = oDict.GetWord("amount");
            lblItemUnitAmt.Text = oDict.GetWord("unit_amount");
            cmdDeleteItem.Text = oDict.GetWord("delete");
            cmdSaveItem.Text = oDict.GetWord("save");

            boxPayment.Text = oDict.GetWord("payment");
            lblPaid.Text = oDict.GetWordWithColon("paid");
            lblPaidOn.Text = oDict.GetWordWithColon("paid_on");
            lblReference.Text = oDict.GetWordWithColon("reference");
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

            // cmdRefresh
            ToolBarButton cmdRefresh = new ToolBarButton("Refresh", oDict.GetWord("refresh"));
            cmdRefresh.Tag = "Refresh";
            cmdRefresh.Image = new IconResourceHandle("16x16.16_L_refresh.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            // cmdPost
            ToolBarButton cmdPost = new ToolBarButton("Post", oDict.GetWord("post"));
            cmdPost.Tag = "Post";
            cmdPost.Image = new IconResourceHandle("16x16.16_closeObj.gif");

            // cmdCancel
            ToolBarButton cmdCancel = new ToolBarButton("Cancel", oDict.GetWord("cancel"));
            cmdCancel.Tag = "Cancel";
            cmdCancel.Image = new IconResourceHandle("16x16.16_deactivate.gif");
            cmdCancel.Visible = true;

            // cmdPrint
            ToolBarButton cmdPrint = new ToolBarButton("Print", oDict.GetWord("print"));
            cmdPrint.Tag = "Print";
            cmdPrint.Image = new IconResourceHandle("16x16.16_print.gif");

            // cmdShowOrder
            ToolBarButton cmdShowOrder = new ToolBarButton("ShowOrder", oDict.GetWord("show_order"));
            cmdShowOrder.Tag = "ShowOrder";
            cmdShowOrder.Image = new IconResourceHandle("16x16.folder.png");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                this.ansToolbar.Buttons.Add(cmdSave);
                this.ansToolbar.Buttons.Add(cmdSaveClose);
                this.ansToolbar.Buttons.Add(sep);
                if (_EditMode != Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdDelete);
                }
                this.ansToolbar.Buttons.Add(cmdPost);
                this.ansToolbar.Buttons.Add(sep);
            }
            this.ansToolbar.Buttons.Add(cmdPrint);
            this.ansToolbar.Buttons.Add(cmdShowOrder);
            this.ansToolbar.Buttons.Add(cmdRefresh);

            #region 如果係 EditMode.Read 可以 cancel, 如果係 EditMode.Edit && Admin User 亦可以 cancel
            if ((_EditMode == Common.Enums.EditMode.Read) ||
                (_EditMode == Common.Enums.EditMode.Edit))
            {
                if ((xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Account) ||
                    (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin))
                {
                    this.ansToolbar.Buttons.Add(cmdCancel);
                    cmdSave.Visible = false;
                }
            }
            #endregion

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            this.lvwDetails.ListViewItemSorter = new ListViewItemSorter(this.lvwDetails);
            //this.lvwDetails.AutoColumnGeneration = false;
            this.lvwDetails.AutoGenerateColumns = false;

            toolTip1.SetToolTip(lvwDetails, "Double click to edit item");
            ToggleItemPane();
        }

        private void SetDropdowns()
        {
            T_PaymentType.LoadCombo(ref cboPaidBy, "Name", false);
            T_BillingCode_Item.LoadCombo(ref cboItemCode, "ItemCode", false, true, "", "GroupCode = 'V2'");
        }

        private void SetEditModeLayout()
        {
            switch (_EditMode)
            {
                case Common.Enums.EditMode.Read:

                    foreach (ToolBarButton cmdButton in ansToolbar.Buttons)
                    {
                        if (cmdButton.Tag != null)
                        {
                            if (cmdButton.Tag.ToString().ToLower() == "cancel" ||
                                cmdButton.Tag.ToString().ToLower() == "print" ||
                                cmdButton.Tag.ToString().ToLower() == "showorder" ||
                                cmdButton.Tag.ToString().ToLower() == "refresh")
                            {
                                cmdButton.Visible = true;
                            }
                            else
                            {
                                cmdButton.Visible = false;
                            }
                        }
                    }
                    break;
                case Common.Enums.EditMode.Add:
                case Common.Enums.EditMode.Edit:
                    if (_InvoiceNumber == 0)
                    {   // 未 Post, 可以 edit details
                        ToggleItemPane();
                    }
                    else
                    {   // 2017.04.13 paulus: 如果 posted, admin 可以改單
                        if (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin)
                        {
                            ToggleItemPane();
                        }
                    }
                    break;
            }
        }
        #endregion

        #region ShowInvoice(), ShowOrderInfo(), ShowInvoiceInfo(), ShowInvoiceItems()
        private void ShowInvoice()
        {
            OrderHeader order = null;
            Acct_INMaster invoice = null;

            switch (_EditMode)
            {
                case Common.Enums.EditMode.Add:
                case Common.Enums.EditMode.Edit:
                    // 2017.04.13 paulus: 如果 posted, admin 可以改單
                    //order = OrderHeader.Load(_OrderId);
                    //break;
                case Common.Enums.EditMode.Read:
                    if (_InvoiceId == 0)
                    {
                        order = OrderHeader.Load(_OrderId);
                    }
                    else
                    {
                        invoice = Acct_INMaster.Load(_InvoiceId);
                        order = OrderHeader.Load(invoice.OrderID);
                    }
                    break;
            }
            if (order != null)
            {
                ShowOrderInfo(order);
                ShowInvoiceInfo(order);
            }
        }

        private void ShowOrderInfo(OrderHeader order)
        {
            _OrderId = order.ID;

            Order_Details orderDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", _OrderId.ToString()));
            Client client = Client.Load(order.ClientID);
            Client_AddressBook billing = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", client.ID.ToString()));

            txtBillingAddress.Html = AddressBox(client.Name, billing.Address, billing.Tel, billing.Fax);
            switch (orderDetails.DeliveryMethod)
            {
                case (int)Common.Enums.DeliveryMethod.DeliverTo:
                    Client_AddressBook delivery = Client_AddressBook.Load(orderDetails.DeliveryAddr);
                    if (delivery != null)
                    {
                        txtDeliveryAddress.Html = AddressBox(client.Name, delivery.Address, delivery.Tel, delivery.Fax);
                    }
                    chkDeliverTo.Checked = true;
                    break;
                case (int)Common.Enums.DeliveryMethod.PickUp:
                    chkPickup.Checked = true;
                    break;
            }
            txtOrderId.Text = _OrderId.ToString();
        }

        private void ShowInvoiceInfo(OrderHeader order)
        {
            Client client = Client.Load(order.ClientID);
            Acct_INMaster invoice = Acct_INMaster.LoadWhere(String.Format("OrderId = {0}", _OrderId.ToString()));
            if (invoice == null)
            {
                CreateNewInvoice(client, order, ref invoice);
            }
            if (invoice != null)
            {
                ShowInvoiceInfo(invoice);
            }
        }

        private void ShowInvoiceInfo(Acct_INMaster invoice)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            _InvoiceId = invoice.ID;
            _InvoiceNumber = invoice.InvoiceNumber;

            int index = 0;
            switch (invoice.Status)
            {
                case (int)Common.Enums.Status.Draft:
                    txtInvoiceNumber.Text = oDict.GetWord(Common.Enums.Status.Draft.ToString());
                    txtInvoiceAmt.Text = invoice.InvoiceAmount.ToString("#,##0.00");
                    break;
                case (int)Common.Enums.Status.Active:
                    txtInvoiceNumber.Text = invoice.InvoiceNumber.ToString();
                    txtInvoiceDate.Text = invoice.InvoiceDate.ToString("yyyy-MM-dd");
                    txtInvoiceAmt.Text = invoice.InvoiceAmount.ToString("#,##0.00");
                    #region show payment
                    chkPaid.Checked = invoice.Paid;
                    txtPaidOn.Text = xFilm5.DAL.Common.DateTimeHelper.DateTimeToString(invoice.PaidOn, false);
                    txtReference.Text = invoice.PaidRef;
                    boxPayment.Visible = true;
                    #endregion
                    break;
            }
            T_PaymentType paidBy = T_PaymentType.Load(invoice.PaymentType);
            if (paidBy != null)
            {
                index = cboPaidBy.FindString(paidBy.Name, 0);
                if (index >= 0 && index < cboPaidBy.Items.Count)
                {
                    cboPaidBy.SelectedIndex = index;
                }
            }
            txtRemarks.Text = invoice.Remarks;

            ShowInvoiceItems(ref invoice);
        }

        private void ShowInvoiceItems()
        {
            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            if (invoice != null)
            {
                ShowInvoiceItems(ref invoice);
            }
        }

        private void ShowInvoiceItems(ref Acct_INMaster invoice)
        {
            this.lvwDetails.Items.Clear();
            string[] orderby = { "ID" };

            Acct_INDetailsCollection items = Acct_INDetails.LoadCollection(String.Format("INMasterID = {0}", invoice.ID.ToString()), orderby, true);
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ListViewItem objItem = this.lvwDetails.Items.Add(items[i].ID.ToString());
                    objItem.SubItems.Add((i +1).ToString());
                    objItem.SubItems.Add(items[i].BillingCode);
                    objItem.SubItems.Add(items[i].Description);
                    objItem.SubItems.Add(items[i].Qty.ToString("#,##0"));
                    objItem.SubItems.Add(items[i].UnitAmount.ToString("#,##0.00"));
                    objItem.SubItems.Add(items[i].Discount.ToString("#,##0.00"));
                    objItem.SubItems.Add(items[i].Amount.ToString("#,##0.00"));
                }
            }
        }

        private void CreateNewInvoice(Client client, OrderHeader order, ref Acct_INMaster invoice)
        {
            invoice = new Acct_INMaster();
            invoice.OrderID = _OrderId;
            invoice.ClientID = client.ID;
            invoice.PaymentType = client.PaymentType;
            invoice.Remarks = "File Name: " + order.AttachmentURL;
            invoice.CreatedBy = Common.Config.CurrentUserId;
            invoice.CreatedOn = DateTime.Now;
            invoice.LastModifiedBy = Common.Config.CurrentUserId;
            invoice.LastModifiedOn = DateTime.Now;
            invoice.Status = (int)Common.Enums.Status.Draft;
            invoice.Save();
        }

        private string AddressBox(string name, string address, string tel, string fax)
        {
            return String.Format(@"
<table style='width: 100%;' border='0'>
	<tr>
		<td><font style='font-family: Arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold;'>{0}</font></td>
	</tr>
	<tr>
		<td><font style='font-family: Arial, Helvetica, sans-serif; font-size: 8pt;'>{1}</font></td>
	</tr>
	<tr>
		<td>
		<table style='width: 100%' border='0'>
			<tr>
				<td style='width: 50%; vertical-align: top;'><font style='font-family: Arial, Helvetica, sans-serif; font-size: 8pt;'>Tel. {2}</font></td>
				<td style='width: 50%; vertical-align: top;'><font style='font-family: Arial, Helvetica, sans-serif; font-size: 8pt;'>Fax. {3}</font></td>
			</tr>
		</table>
		</td>
	</tr>
</table>
", name.Trim(), address.Trim(), tel.Trim(), fax.Trim());
        }
        #endregion

        #region SaveInvoice(), DeleteInvoice(), CancelInvoice(), PostInvoice()
        private bool SaveInvoice()
        {
            bool result = false;

            Acct_INMaster invoice = null;

            switch (_EditMode)
            {
                case Common.Enums.EditMode.Add:
                    break;
                case Common.Enums.EditMode.Edit:
                    #region save changes
                    try
                    {
                        invoice = Acct_INMaster.Load(_InvoiceId);
                        invoice.PaymentType = (int)cboPaidBy.SelectedValue;
                        invoice.LastModifiedBy = Common.Config.CurrentUserId;
                        invoice.LastModifiedOn = DateTime.Now;
                        invoice.Save();

                        result = true;
                    }
                    catch { }
                    #endregion
                    break;
            }

            return result;
        }

        private void SaveInvoiceAmount()
        {
            decimal totalamount = 0;
            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            Acct_INDetailsCollection items = Acct_INDetails.LoadCollection(String.Format("INMasterID = {0}", _InvoiceId.ToString()));
            if (items.Count > 0)
            {
                foreach (Acct_INDetails item in items)
                {
                    totalamount += (item.Amount);
                }
                invoice.InvoiceAmount = totalamount;
                invoice.Save();

                txtInvoiceAmt.Text = totalamount.ToString("#,##0.00");
            }
        }

        private bool CancelInvoice()
        {
            bool result = false;

            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            if (invoice != null)
            {
                if ((invoice.Status >= (int)Common.Enums.Status.Active) && (invoice.Paid != true))
                {
                    try
                    {
                        invoice.Status = (int)Common.Enums.Status.Inactive;
                        invoice.LastModifiedOn = DateTime.Now;
                        invoice.LastModifiedBy = Common.Config.CurrentUserId;
                        invoice.Save();

                        OrderHeader order = OrderHeader.Load(invoice.OrderID);
                        if (order != null)
                        {
                            order.Status = (int)Common.Enums.Workflow.Cancelled;
                            order.Save();
                        }

                        result = true;
                    }
                    catch { }
                }
                else if ((invoice.Paid != true) && (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin))
                {

                }
            }

            return result;
        }

        private bool DeleteInvoice()
        {
            bool result = false;

            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            if (invoice != null)
            {
                if (invoice.Status == (int)Common.Enums.Status.Draft)
                {
                    #region Draft 人人都可以 cancel 張 invoice
                    try
                    {
                        Acct_INDetailsCollection items = Acct_INDetails.LoadCollection(String.Format("INMasterID = {0}", _InvoiceId.ToString()));
                        foreach (Acct_INDetails item in items)
                        {
                            item.Delete();
                        }
                        invoice.Delete();

                        result = true;
                    }
                    catch { }
                    #endregion
                }
                else
                {
                    #region 已經 post 咗就要 admin 先可以 cancel
                    if (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin)
                    {
                        try
                        {
                            Acct_INDetailsCollection items = Acct_INDetails.LoadCollection(String.Format("INMasterID = {0}", _InvoiceId.ToString()));
                            foreach (Acct_INDetails item in items)
                            {
                                item.Delete();
                            }
                            invoice.Delete();

                            result = true;
                        }
                        catch { }
                    }
                    #endregion
                }
            }

            return result;
        }

        private bool PostInvoice()
        {
            bool result = false;

            Acct_INMaster invoice = Acct_INMaster.Load(_InvoiceId);
            if (invoice != null)
            {
                if (invoice.Status == (int)Common.Enums.Status.Draft)
                {
                    try
                    {
                        if (Common.Invoice.Post(ref invoice))
                        {
                            txtInvoiceNumber.Text = invoice.InvoiceNumber.ToString();
                            txtInvoiceDate.Text = invoice.InvoiceDate.ToString("yyyy-MM-dd");
                            this.Update();

                            _EditMode = Common.Enums.EditMode.Read;
                            _InvoiceNumber = invoice.InvoiceNumber;

                            result = true;
                        }
                    }
                    catch { }
                }
            }

            return result;
        }
        #endregion

        #region ToggleItemPane(), ResetItemPane()
        private void ToggleItemPane()
        {
            if (_ShowItemPane)
            {
                gbxItemPane.Visible = false;
                this.Update();
                _ShowItemPane = false;
            }
            else
            {
                gbxItemPane.Visible = true;
                ResetItemPanel();
                this.Update();
                _ShowItemPane = true;
            }
        }

        private void ResetItemPanel()
        {
            txtItemId.Text = String.Empty;
            cboItemCode.SelectedIndex = 0;
            txtItemDescription.Text = String.Empty;
            txtItemQty.Text = String.Empty;
            txtItemUnitAmt.Text = String.Empty;
            txtItemDiscount.Text = String.Empty;
            txtItemAmount.Text = String.Empty;

            cmdSaveItem.Enabled = true;
            cmdDeleteItem.Enabled = true;
            cmdDeleteItem.Visible = false;
        }
        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Invoice?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Invoice And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "refresh":
                        // 因為 lvwDetails 有時會失蹤，所以多加了這個功能
                        this.Update();
                        break;
                    case "cancel":
                        MessageBox.Show("Cancel Invoice?", "Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdCancel_Click));
                        break;
                    case "delete":
                        MessageBox.Show("Delete Invoice?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                    case "post":
                        MessageBox.Show("Post this Invoice?", "Post Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPost_Click));
                        break;
                    case "print":
                        // popup invoice
                        xFilm5.Controls.Reporting.Loader.Invoice(_InvoiceId);
                        break;
                    case "showorder":
                        #region popup Order Form
                        OrderHeader oOrder = OrderHeader.Load(_OrderId);
                        if (oOrder != null)
                        {
                            switch (oOrder.ServiceType)
                            {
                                case (int)Common.Enums.OrderType.UploadFile:
                                    xFilm5.JobOrder.Forms.UploadFile oUploadFile = new xFilm5.JobOrder.Forms.UploadFile();
                                    oUploadFile.EditMode = Common.Enums.EditMode.Read;
                                    oUploadFile.OrderId = _OrderId;
                                    oUploadFile.Show();
                                    break;
                                case (int)Common.Enums.OrderType.DirectPrint:
                                    xFilm5.JobOrder.Forms.DirectPrint oDirectPrint = new xFilm5.JobOrder.Forms.DirectPrint();
                                    oDirectPrint.EditMode = Common.Enums.EditMode.Read;
                                    oDirectPrint.OrderId = _OrderId;
                                    oDirectPrint.Show();
                                    break;
                                case (int)Common.Enums.OrderType.PsFile:
                                    xFilm5.JobOrder.Forms.PsFile oPsFile = new xFilm5.JobOrder.Forms.PsFile();
                                    oPsFile.EditMode = Common.Enums.EditMode.Read;
                                    oPsFile.OrderId = _OrderId;
                                    oPsFile.Show();
                                    break;
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
                    if (SaveInvoice())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is saved!", _InvoiceNumber.ToString()), "Save Result");
                        if (_EditMode == Common.Enums.EditMode.Add)
                        {
                            _EditMode = Common.Enums.EditMode.Edit;
                        }
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
                    if (SaveInvoice())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is saved!", _InvoiceNumber.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (CancelInvoice())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is cancelled.", _InvoiceNumber.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the record status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (DeleteInvoice())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is deleted.", _InvoiceNumber.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not delete this record!\nPlease review the record status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdPost_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (PostInvoice())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is posted.", _InvoiceNumber.ToString()), "Post Result");
                        SetEditModeLayout();
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not post this record!\nPlease review the record status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void cmdToggleItemPane_Click(object sender, EventArgs e)
        {
            ToggleItemPane();
        }

        private void cboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                T_BillingCode_Item item = T_BillingCode_Item.Load((int)cboItemCode.SelectedValue);
                if (item != null)
                {
                    txtItemDescription.Text = item.Name;
                    txtItemUnitAmt.Text = item.UnitPrice.ToString("##0.00");
                }
            }
            catch { }
        }

        private void cmdSaveItem_Click(object sender, EventArgs e)
        {
            Acct_INDetails item = null;
            int qty = 0;
            decimal unitamt = 0;
            decimal discount = 0;

            if (String.IsNullOrEmpty(txtItemId.Text))
            {
                item = new Acct_INDetails();
                item.INMasterID = _InvoiceId;
            }
            else
            {
                item = Acct_INDetails.Load(Convert.ToInt32(txtItemId.Text));
            }
            try
            {
                if (!(String.IsNullOrEmpty(txtItemQty.Text.Trim())))
                {
                    qty = Convert.ToInt32(txtItemQty.Text.Trim());
                }
                if (!(String.IsNullOrEmpty(txtItemUnitAmt.Text.Trim())))
                {
                    unitamt = Convert.ToDecimal(txtItemUnitAmt.Text.Trim());
                }
                if (!(String.IsNullOrEmpty(txtItemDiscount.Text.Trim())))
                {
                    discount = Convert.ToDecimal(txtItemDiscount.Text.Trim());
                }
                item.BillingCode = cboItemCode.Text;
                item.Description = txtItemDescription.Text.Trim();
                item.Qty = Convert.ToInt16(qty);
                item.UnitAmount = unitamt;
                item.Discount = discount;
                item.Amount = Math.Round((qty * unitamt * (100 - discount) / 100), 2);
                item.Save();

                SaveInvoiceAmount();
                ShowInvoiceItems();
                ResetItemPanel();
                this.Update();
            }
            catch { }
        }

        private void cmdDeleteItem_Click(object sender, EventArgs e)
        {
            int itemId = 0;

            try
            {
                itemId = Convert.ToInt32(txtItemId.Text);
                Acct_INDetails item = Acct_INDetails.Load(itemId);
                if (item != null)
                {
                    item.Delete();
                    SaveInvoiceAmount();
                    ShowInvoiceItems();
                    ResetItemPanel();
                    this.Update();
                }
            }
            catch { }
        }

        private void lvwDetails_DoubleClick(object sender, EventArgs e)
        {
            int index = 0;
            if (lvwDetails.SelectedItem != null && _EditMode != Common.Enums.EditMode.Read)
            {
                int itemId = Convert.ToInt32(lvwDetails.SelectedItem.Text);
                Acct_INDetails item = Acct_INDetails.Load(itemId);
                if (item != null)
                {
                    if (!(_ShowItemPane))
                    {
                        ToggleItemPane();
                    }
                    txtItemId.Text = item.ID.ToString();
                    index = cboItemCode.FindString(item.BillingCode, 0);
                    if (index >= 0 && index < cboItemCode.Items.Count)
                    {
                        cboItemCode.SelectedIndex = index;
                    }
                    txtItemDescription.Text = item.Description;
                    txtItemQty.Text = item.Qty.ToString("##0");
                    txtItemUnitAmt.Text = item.UnitAmount.ToString("##0.00");
                    txtItemDiscount.Text = item.Discount.ToString("##0.00");
                    txtItemAmount.Text = item.Amount.ToString("##0.00");
                    cboItemCode.Focus();
                    cmdDeleteItem.Visible = true;
                    this.Update();
                }
            }
        }
    }
}