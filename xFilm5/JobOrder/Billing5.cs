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
using System.Linq;
using xFilm5.Controls.Email;

#endregion

namespace xFilm5.JobOrder
{
    public partial class Billing5 : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private List<int> _OrderIdList = new List<int>();
        private int _InvoiceId = 0;
        private int _InvoiceNumber = 0;
        private bool _ShowItemPane = true;
        private int _ClientId = 0;
        private Decimal _TotalAmount = 0;
        private int _ReceiptHeaderId = 0;
        private bool _CashOrder = false;

        public Billing5()
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
        public List<int> OrderIdList
        {
            set { _OrderIdList = value; }
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

            _OrderId = _OrderIdList.Count > 0 ? _OrderIdList[0] : 0;
            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            SetDropdowns();
            ShowInvoice();
            SetEditModeLayout();
            this.Update();

            if (_EditMode ==  Common.Enums.EditMode.Add) AddOrders();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            chkDeliverTo.Text = oDict.GetWordWithColon("deliver_to");
            chkPickup.Text = oDict.GetWord("pick_up");
            gbxBilling.Text = oDict.GetWord("billing_address");
            gbxDelivery.Text = oDict.GetWord("delivery_address");

            lblOrderId.Text = oDict.GetWord("order_id");
            //lblInvoiceNumber.Text = oDict.GetWord("invoice_number");
            //lblInvoiceDate.Text = oDict.GetWord("invoice_date");
            //lblInvoiceAmount.Text = oDict.GetWord("invoice_amount");
            lblInvoiceNumber.Text = oDict.GetWord("number");
            lblInvoiceDate.Text = oDict.GetWord("date");
            lblInvoiceAmount.Text = oDict.GetWord("total_amount");
            lblPaidBy.Text = oDict.GetWord("paid_by");

            colLN.Text = oDict.GetWord("ln");
            colItemCode.Text = oDict.GetWord("item_code");
            colDescription.Text = oDict.GetWord("item_description");
            colQty.Text = oDict.GetWord("qty");
            colUnitAmt.Text = oDict.GetWord("unit_amount");
            colDiscount.Text = oDict.GetWord("discount");
            colAmount.Text = oDict.GetWord("amount");
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

            // cmdMonthly
            ToolBarButton cmdMonthly = new ToolBarButton("Monthly", oDict.GetWord("paid_monthly"));
            cmdMonthly.Tag = "Monthly";
            cmdMonthly.Image = new IconResourceHandle("16x16.icons5_office_Calendar_31_16px_1.png");

            // cmdCash
            ToolBarButton cmdCash = new ToolBarButton("Cash", oDict.GetWord("paid_cash"));
            cmdCash.Tag = "Cash";
            cmdCash.Image = new IconResourceHandle("16x16.icon5_office_US_Dollar_16px.png");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                //this.ansToolbar.Buttons.Add(cmdMonthly);
                //this.ansToolbar.Buttons.Add(cmdCash);
                //this.ansToolbar.Buttons.Add(sep);
                if (_EditMode == Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdMonthly);
                    this.ansToolbar.Buttons.Add(cmdCash);
                }
                else
                {
                    this.ansToolbar.Buttons.Add(cmdPrint);
                    this.ansToolbar.Buttons.Add(sep);
                    this.ansToolbar.Buttons.Add(cmdCancel);
                }
                //this.ansToolbar.Buttons.Add(cmdPost);
                //this.ansToolbar.Buttons.Add(sep);
            }
            //this.ansToolbar.Buttons.Add(cmdPrint);
            //this.ansToolbar.Buttons.Add(cmdShowOrder);
            //this.ansToolbar.Buttons.Add(cmdRefresh);
            //if (_EditMode == Common.Enums.EditMode.Read)
            //{
            //    if ((xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Account) ||
            //        (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin))
            //    {
            //        this.ansToolbar.Buttons.Add(cmdCancel);
            //    }
            //}

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
                    //if (_InvoiceNumber == 0)
                    //{   // 未 Post, 可以 edit details
                    //    ToggleItemPane();
                    //}

                    txtInvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    lblOrderId.Visible = false;
                    txtOrderId.Visible = false;
                    lblPaidBy.Visible = _EditMode == Common.Enums.EditMode.Edit ? true : false;
                    cboPaidBy.Visible = _EditMode == Common.Enums.EditMode.Edit ? true : false;
                    this.Update();
                    _ShowItemPane = false;
                    break;
            }
        }
        #endregion

        #region Plate5 Auto Gen items
        private void AddOrders()
        {
            foreach (int orderId in _OrderIdList)
            {
                AddOrderItems(orderId);
            }
            txtInvoiceAmt.Text = _TotalAmount.ToString("#,##0.00");
        }

        private void AddOrderItems(int orderId)
        {
            String sql = String.Format("OrderHeaderId = {0}", orderId.ToString());
            String[] orderby = { "PrintQueueVpsId" };
            DAL.OrderPkPrintQueueVpsCollection pkPq = DAL.OrderPkPrintQueueVps.LoadCollection(sql, orderby, true);
            if (pkPq.Count > 0)
            {
                for (int i = 0; i < pkPq.Count; i++)
                {
                    DAL.OrderPkPrintQueueVps pk = pkPq[i];
                    if (pk.CheckedPlate) AddItem_Plate(pk);
                    if (pk.CheckedCip3) AddItem_Cip3(pk);
                    if (pk.CheckedBlueprint) AddItem_Blueprint(pk);
                }
                //foreach (DAL.OrderPkPrintQueueVps pk in pkPq)
                //{
                //    if (pk.CheckedPlate) AddItem_Plate(pk);
                //    if (pk.CheckedCip3) AddItem_Cip3(pk);
                //    if (pk.CheckedBlueprint) AddItem_Blueprint(pk);
                //}
            }
        }

        private void AddItem_Plate(DAL.OrderPkPrintQueueVps pk)
        {
            String sql = String.Empty;

            DAL.PrintQueue_VPS pqVps = DAL.PrintQueue_VPS.Load(pk.PrintQueueVpsId);
            if (pqVps != null)
            {
                DAL.PrintQueue pq = DAL.PrintQueue.Load(pqVps.PrintQueueID);

                var orderHdr = DAL.OrderHeader.Load(pk.OrderHeaderId);
                switch (orderHdr.ServiceType)
                {
                    case (int)Common.Enums.OrderType.Plate5:
                        #region Plate 5: pq.PlateSize = item code
                        sql = String.Format("ItemCode = '{0}'", pq.PlateSize);
                        DAL.T_BillingCode_Item item = DAL.T_BillingCode_Item.LoadWhere(sql);

                        if (item != null)
                        {
                            sql = String.Format("ClientId = {0} AND ItemId = {1}", _ClientId.ToString(), item.ID.ToString());
                            DAL.ClientPricing vip = DAL.ClientPricing.LoadWhere(sql);

                            String description = pqVps.VpsFileName.Substring(0, pqVps.VpsFileName.Length - 4);
                            description = String.Format("{0}: {1}", pk.OrderHeaderId.ToString(), description);
                            Decimal price = vip != null ? vip.UnitPrice : item.UnitPrice;
                            Decimal discount = vip != null ? vip.Discount : 0;
                            Decimal amount = price * (100 - discount) / 100;
                            _TotalAmount = _TotalAmount + amount;

                            ListViewItem objItem = this.lvwDetails.Items.Add(pk.OrderPkPrintQueueVpsId.ToString());
                            objItem.SubItems.Add((lvwDetails.Items.Count).ToString());
                            objItem.SubItems.Add(item.ItemCode);
                            objItem.SubItems.Add(description);
                            objItem.SubItems.Add("1");
                            objItem.SubItems.Add(price.ToString("#,##0.00"));
                            objItem.SubItems.Add(discount.ToString("#,##0.00"));
                            objItem.SubItems.Add(amount.ToString("#,##0.00"));
                        }
                        #endregion
                        break;
                    case (int)Common.Enums.OrderType.Film5:
                        #region Film 5: pq.PlateSize = F9x9, where F = item code
                        sql = String.Format("ItemCode = '{0}'", pq.PlateSize.Substring(0, 1));
                        DAL.T_BillingCode_Item itemF = DAL.T_BillingCode_Item.LoadWhere(sql);

                        if (itemF != null)
                        {
                            sql = String.Format("ClientId = {0} AND Tag = '{1}'", _ClientId.ToString(), pq.PlateSize.Substring(0, 1));
                            DAL.ClientPricing vip = DAL.ClientPricing.LoadWhere(sql);

                            Decimal price = vip != null ? vip.UnitPrice : itemF.UnitPrice;
                            Decimal discount = vip != null ? vip.Discount : 0;
                            Decimal amount = price * (100 - discount) / 100;

                            sql = String.Format("ItemCode = '{0}00'", pq.PlateSize.Substring(0, 1));
                            var itemMm = DAL.T_BillingCode_Item.LoadWhere(sql); // 最低收費
                            sql = String.Format("ClientId = {0} AND ItemId = {1}", _ClientId.ToString(), itemMm.ID.ToString());
                            var itemMmVip = DAL.ClientPricing.LoadWhere(sql);

                            Decimal unitPriceMm = (itemMmVip != null) ? itemMmVip.UnitPrice : itemMm.UnitPrice;

                            // 如果 長 X 闊 X 菲林價 少於 minimum，收 minimum
                            var sides = pq.PlateSize.Substring(1).Split('x');
                            Decimal xLength = 0, yLength = 0;
                            xLength = Decimal.TryParse(sides[0], out xLength) ? xLength : 0;
                            yLength = Decimal.TryParse(sides[1], out yLength) ? yLength : 0;
                            bool minCharge = unitPriceMm > Math.Round(xLength * yLength * amount);

                            String description = pqVps.VpsFileName.Substring(0, pqVps.VpsFileName.Length - 4);
                            description = String.Format("{0}: {1}", pk.OrderHeaderId.ToString(), description);

                            Decimal prx = minCharge ? unitPriceMm : price;
                            Decimal disc = discount;
                            //Decimal amount = minCharge ? itemMm.UnitPrice : (int)Math.Ceiling(xLength * yLength * itemF.UnitPrice);
                            Decimal amt = minCharge ? unitPriceMm : Math.Round(xLength * yLength * amount);
                            _TotalAmount = _TotalAmount + amt;

                            ListViewItem objItem = this.lvwDetails.Items.Add(pk.OrderPkPrintQueueVpsId.ToString());
                            objItem.SubItems.Add((lvwDetails.Items.Count).ToString());
                            objItem.SubItems.Add(pq.PlateSize);
                            objItem.SubItems.Add(description);
                            objItem.SubItems.Add("1");
                            objItem.SubItems.Add(prx.ToString("#,##0.00"));
                            objItem.SubItems.Add(disc.ToString("#,##0.00"));
                            objItem.SubItems.Add(amt.ToString("#,##0.00"));
                        }
                        #endregion
                        break;
                }
            }
        }

        private void AddItem_Cip3(DAL.OrderPkPrintQueueVps pk)
        { }
        private void AddItem_Blueprint(DAL.OrderPkPrintQueueVps pk)
        {
            String sql = String.Empty;

            DAL.PrintQueue_VPS pqVps = DAL.PrintQueue_VPS.Load(pk.PrintQueueVpsId);
            if (pqVps != null)
            {
                DAL.PrintQueue pq = DAL.PrintQueue.Load(pqVps.PrintQueueID);

                sql = String.Format("ItemCode = '{0}-BP'", pq.PlateSize);
                DAL.T_BillingCode_Item item = DAL.T_BillingCode_Item.LoadWhere(sql);

                if (item != null)
                {
                    sql = String.Format("ClientId = {0} AND ItemId = {1}", _ClientId.ToString(), item.ID.ToString());
                    DAL.ClientPricing vip = DAL.ClientPricing.LoadWhere(sql);

                    String description = pqVps.VpsFileName.Substring(0, pqVps.VpsFileName.LastIndexOf("("));
                    description = String.Format("{0}: {1}", pk.OrderHeaderId.ToString(), description);
                    if (!IsBpItemAlreadyAdded(description))
                    {
                        Decimal price = vip != null ? vip.UnitPrice : item.UnitPrice;
                        Decimal discount = vip != null ? vip.Discount : 0;
                        Decimal amount = price * (100 - discount) / 100;
                        _TotalAmount = _TotalAmount + amount;

                        ListViewItem objItem = this.lvwDetails.Items.Add(pk.OrderPkPrintQueueVpsId.ToString());
                        objItem.SubItems.Add((lvwDetails.Items.Count).ToString());
                        objItem.SubItems.Add(item.ItemCode);
                        objItem.SubItems.Add(description);
                        objItem.SubItems.Add("1");
                        objItem.SubItems.Add(price.ToString("#,##0.00"));
                        objItem.SubItems.Add(discount.ToString("#,##0.00"));
                        objItem.SubItems.Add(amount.ToString("#,##0.00"));
                    }
                }
            }
        }

        private bool IsBpItemAlreadyAdded(String itemDescription)
        {
            bool result = false;

            for (int i = 0; i < lvwDetails.Items.Count; i++)
            {
                if (lvwDetails.Items[i].SubItems[colDescription.Index].Text == itemDescription)
                {
                    result = true;
                }
            }

            return result;
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
                    String sql = String.Format("INMasterID = {0}", _InvoiceId.ToString());
                    String[] orderby = { "OrderPkPrintQueueVpsId" };

                    DAL.Acct_INDetailsCollection items = DAL.Acct_INDetails.LoadCollection(sql, orderby, true);
                    if (items.Count > 0)
                    {
                        var pq = DAL.OrderPkPrintQueueVps.Load(items[0].OrderPkPrintQueueVpsId);
                        if (pq != null)
                        {
                            _OrderId = pq.OrderHeaderId;
                        }
                    }

                    order = OrderHeader.Load(_OrderId);
                    break;
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

            _ClientId = client.ID;
        }

        private void ShowInvoiceInfo(OrderHeader order)
        {
            Client client = Client.Load(order.ClientID);
            Acct_INMaster invoice;
            if (_InvoiceId != 0)
            {
                invoice = DAL.Acct_INMaster.Load(_InvoiceId);
            }
            else
            {
                invoice = DAL.Acct_INMaster.LoadWhere(String.Format("OrderId = {0}", _OrderId.ToString()));
            }
            if (invoice == null)
            {
                CreateNewInvoice(client, order, ref invoice);
            }
            else //if (invoice != null)
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

        #region x5 之前: SaveInvoice(), DeleteInvoice(), CancelInvoice(), PostInvoice()
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
                // 2017.04.29 paulus: paid 都可以 cancel
                //if ((invoice.Status >= (int)Common.Enums.Status.Active) && (invoice.Paid != true))
                if (invoice.Status >= (int)Common.Enums.Status.Active)
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

        #region Save Inv5
        private bool SaveInv5()
        {
            bool result = false;

            if (SaveReceipt())                                  // 首先 update dbo.ReceiptHeader & dbo.ReceiptDetails
            {
                int inv5HdrId = SaveInv5Header();               // dbo.Acct_INMaster

                if (inv5HdrId != 0)
                {
                    result = SaveInv5Details(inv5HdrId);        // dbo.Acct_INDetails

                    SaveIsBilled4Pk(inv5HdrId);                 // dbo.OrderPkPrintQueueVps.IsBilled = true

                    if (result)
                    {
                        WritePqLifeCycle(DAL.Common.Enums.PrintQSubitemType.Invoice);       // dbo.PrintQueue_LifeCycle

                        // 由 xFilm5.Bot 負責打印小票據
                        if (Helper.ClientHelper.IsReceiptSlip(_ClientId))
                            Helper.InvoiceHelper.PrintToXprinter(inv5HdrId);

                        // 2017.05.14 paulus: 新增功能，可以 email 張單
                        EmailReceipt(_ReceiptHeaderId);
                    }
                }
            }

            return result;
        }

        private int SaveInv5Header()
        {
            int result = 0;
            try
            {
                DAL.ReceiptHeader receiptHdr = DAL.ReceiptHeader.Load(_ReceiptHeaderId);
                if (receiptHdr != null)
                {
                    DAL.Acct_INMaster inv5Hdr = new Acct_INMaster();

                    inv5Hdr.InvoiceNumber = xFilm5.Controls.Utility.System.GetNextInvoiceNumber();
                    inv5Hdr.InvoiceDate = receiptHdr.ReceiptDate;
                    inv5Hdr.InvoiceAmount = receiptHdr.ReceiptAmount;
                    inv5Hdr.ClientID = receiptHdr.ClientId;
                    inv5Hdr.PaymentType = 1;                    // cash
                    inv5Hdr.Paid = true;
                    inv5Hdr.PaidOn = DateTime.Now;
                    inv5Hdr.PaidAmount = receiptHdr.ReceiptAmount;
                    inv5Hdr.CreatedOn = DateTime.Now;
                    inv5Hdr.CreatedBy = DAL.Common.Config.CurrentUserId;
                    inv5Hdr.LastModifiedOn = DateTime.Now;
                    inv5Hdr.LastModifiedBy = DAL.Common.Config.CurrentUserId;
                    inv5Hdr.Status = (int)DAL.Common.Enums.Status.Active;
                    inv5Hdr.Save();

                    receiptHdr.INMasterId = inv5Hdr.ID;
                    receiptHdr.Paid = true;
                    receiptHdr.PaidOn = DateTime.Now;
                    receiptHdr.PaidAmount = receiptHdr.ReceiptAmount;
                    receiptHdr.Save();

                    result = inv5Hdr.ID;
                    _InvoiceId = inv5Hdr.ID;
                    _InvoiceNumber = inv5Hdr.InvoiceNumber;
                }
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        private bool SaveInv5Details(int inv5HdrId)
        {
            bool result = false;
            DAL.ReceiptHeader receiptHdr = DAL.ReceiptHeader.LoadWhere(String.Format("INMasterID = {0}", inv5HdrId.ToString()));
            if (receiptHdr != null)
            {
                DAL.ReceiptDetailCollection receiptDtls = DAL.ReceiptDetail.LoadCollection(String.Format("ReceiptHeaderId = {0}", receiptHdr.ReceiptHeaderId.ToString()));
                if (receiptDtls.Count > 0)
                {
                    foreach (DAL.ReceiptDetail receiptDtl in receiptDtls)
                    {
                        DAL.Acct_INDetails inv5Dtl = new Acct_INDetails();
                        inv5Dtl.INMasterID = inv5HdrId;
                        inv5Dtl.OrderPkPrintQueueVpsId = receiptDtl.OrderPkPrintQueueVpsId;
                        inv5Dtl.BillingCode = receiptDtl.BillingCode;
                        inv5Dtl.Description = receiptDtl.Description;
                        inv5Dtl.Qty = receiptDtl.Qty;
                        inv5Dtl.UnitAmount = receiptDtl.UnitAmount;
                        inv5Dtl.Discount = receiptDtl.Discount;
                        inv5Dtl.Amount = receiptDtl.Amount;
                        inv5Dtl.Save();
                    }
                    result = true;
                }
            }

            return result;
        }

        private bool SaveIsBilled4Pk(int inv5HdrId)
        {
            bool result = false;

            String sql = String.Format("INMasterID = {0}", inv5HdrId.ToString());
            DAL.Acct_INDetailsCollection details = DAL.Acct_INDetails.LoadCollection(sql);
            if (details.Count > 0)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    DAL.OrderPkPrintQueueVps pk = DAL.OrderPkPrintQueueVps.Load(details[i].OrderPkPrintQueueVpsId);
                    if (pk != null)
                    {
                        pk.IsBilled = true;
                        pk.ModifiedOn = DateTime.Now;
                        pk.ModifiedBy = DAL.Common.Config.CurrentUserId;
                        pk.Save();
                    }
                }
                result = true;
            }

            return result;
        }
        #endregion

        #region Save Receipt5
        private bool SaveReceipt()
        {
            bool result = false;

            int receiptId = SaveReceiptHeader();            // dbo.ReceiptHeader

            if (receiptId != 0)
            {
                result = SaveReceiptDetails(receiptId);     // dbo.ReceiptDetail

                SaveIsReceived4Pk(receiptId);               // dbo.OrderPkPrintQueueVps.IsReceived = true, 就算 result  false，都有可能有 dbo.ReceiptDetail

                if (result)
                {
                    xFilm5.Controls.Utility.JobOrder.SetAsCompleted(_OrderIdList);      // dbo.Order.Status = Completed
                    xFilm5.Controls.Utility.JobOrder.WriteJournal(_OrderIdList,         // dbo.Order_Journal
                        DAL.Common.Enums.Workflow.Completed);

                    WritePqLifeCycle(DAL.Common.Enums.PrintQSubitemType.Receipt);       // dbo.PrintQueue_LifeCycle

                    if (!(_CashOrder))
                    {
                        //Reports5.DN_POS80 pos80 = new Reports5.DN_POS80();
                        //pos80.Print(receiptId);

                        //Reports5.Loader.DN_80mm(receiptId);

                        // 由 xFilm5.Bot 負責打印小票據
                        if (Helper.ClientHelper.IsReceiptSlip(_ClientId))
                            Helper.BotHelper.PostXprinter(receiptId);

                        // 2017.05.14 paulus: 新增功能，可以 email 張單
                        EmailReceipt(receiptId);
                    }
                }

                _ReceiptHeaderId = receiptId;               // ReceiptNumber = dbo.ReceiptHeader.ReceiptHeaderId
            }

            return result;
        }

        private int SaveReceiptHeader()
        {
            int result = 0;
            try
            {
                Decimal totalAmount = 0;
                totalAmount = Decimal.TryParse(txtInvoiceAmt.Text.Trim().Replace(",", ""), out totalAmount) == true ? totalAmount : 0;

                DAL.ReceiptHeader hdr = new DAL.ReceiptHeader();

                hdr.ClientId = _ClientId;
                hdr.PaymentType = 0;        // 月結
                hdr.ReceiptNumber = String.Empty;
                hdr.ReceiptDate = DateTime.Now;
                hdr.ReceiptAmount = totalAmount;
                hdr.Paid = false;
                hdr.Status = (int)DAL.Common.Enums.Status.Active;
                hdr.CreatedOn = DateTime.Now;
                hdr.CreatedBy = DAL.Common.Config.CurrentUserId;
                hdr.ModifiedOn = DateTime.Now;
                hdr.ModifiedBy = DAL.Common.Config.CurrentUserId;

                hdr.Save();

                // 將個 ReceiptHeaderId 攞嚟當作 ReceiptNumber
                hdr.ReceiptNumber = hdr.ReceiptHeaderId.ToString();
                hdr.Save();

                result = hdr.ReceiptHeaderId;
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        private bool SaveReceiptDetails(int receiptHeaderId)
        {
            bool result = false;

            for (int i = 0; i < lvwDetails.Items.Count; i++)
            {
                ListViewItem item = lvwDetails.Items[i];

                try
                {
                    #region Save dbo.ReceiptDetails
                    int qty = 1;
                    Decimal unitPrice = 0, discount = 0, amount = 0;

                    String orderPkPqId = item.Text;

                    String itemCode = item.SubItems[colItemCode.Index].Text;
                    String description = item.SubItems[colDescription.Index].Text;
                    qty = Int32.TryParse(item.SubItems[colQty.Index].Text, out qty) == true ? qty : 1;
                    unitPrice = Decimal.TryParse(item.SubItems[colUnitAmt.Index].Text.Replace(",", ""), out unitPrice) == true ? unitPrice : 0;
                    discount = Decimal.TryParse(item.SubItems[colDiscount.Index].Text.Replace(",", ""), out discount) == true ? discount : 0;
                    amount = Decimal.TryParse(item.SubItems[colAmount.Index].Text.Replace(",", ""), out amount) == true ? amount : 0;

                    DAL.ReceiptDetail dtl = new ReceiptDetail();

                    dtl.ReceiptHeaderId = receiptHeaderId;
                    dtl.OrderPkPrintQueueVpsId = Convert.ToInt32(orderPkPqId);
                    dtl.BillingCode = itemCode;
                    dtl.Description = description;
                    dtl.Qty = Convert.ToInt16(qty);
                    dtl.UnitAmount = unitPrice;
                    dtl.Discount = discount;
                    dtl.Amount = amount;

                    dtl.Save();
                    #endregion

                    result = true;
                }
                catch
                {
                    result = false;
                }
                if (!result) break;     // 碰到 error 就 halt
            }

            return result;
        }

        private bool SaveIsReceived4Pk(int receiptHeaderId)
        {
            bool result = false;

            String sql = String.Format("ReceiptHeaderId = {0}", receiptHeaderId.ToString());
            DAL.ReceiptDetailCollection details = DAL.ReceiptDetail.LoadCollection(sql);
            if (details.Count > 0)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    DAL.OrderPkPrintQueueVps pk = DAL.OrderPkPrintQueueVps.Load(details[i].OrderPkPrintQueueVpsId);
                    if (pk != null)
                    {
                        pk.IsReceived = true;
                        pk.ModifiedOn = DateTime.Now;
                        pk.ModifiedBy = DAL.Common.Config.CurrentUserId;
                        pk.Save();
                    }
                }
                result = true;
            }

            return result;
        }

        private bool WritePqLifeCycle(DAL.Common.Enums.PrintQSubitemType type)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                for (int i = 0; i < lvwDetails.Items.Count; i++)
                {
                    ListViewItem item = lvwDetails.Items[i];

                    #region Save dbo.ReceiptDetails
                    int orderPkPqId = 0;
                    orderPkPqId = int.TryParse(item.Text, out orderPkPqId) == true ? orderPkPqId : 0;

                    // 2018.09.21 paulus: 搞錯，應該要用 PrintQueue_Vps.ID
                    //result = xFilm5.Controls.Utility.PrintQueue_LifeCycle.WriteLogWithVpsId(orderPkPqId, type);
                    //
                    var pkVps = ctx.OrderPkPrintQueueVps.Where(x => x.OrderPkPrintQueueVpsId == orderPkPqId).SingleOrDefault();
                    if (pkVps != null)
                    {
                        result = xFilm5.Controls.Utility.PrintQueue_LifeCycle.WriteLogWithVpsId(pkVps.PrintQueueVpsId.Value, type);
                    }
                    #endregion

                    if (!result) break;     // 碰到 error 就 halt
                }
            }

            return result;
        }

        /// <summary>
        /// 2017.05.14 paulus: 新增功能，可以 email 張單
        /// </summary>
        private void EmailReceipt(int receiptId)
        {
            if (Helper.ClientHelper.IsReceiptEmail(_ClientId))
            {
                var p = Helper.ClientHelper.GetEmailRecipient(_ClientId);
                if (p != "")
                {
                    EmailEx.SendDNEmail(receiptId, p);
                }
            }
        }
        #endregion

        #region ToggleItemPane(), ResetItemPane()
        private void ToggleItemPane()
        {
            if (_ShowItemPane)
            {
                this.Update();
                _ShowItemPane = false;
            }
            else
            {
                this.Update();
                _ShowItemPane = true;
            }
        }

        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

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
                        #region 月結 or 現金
                        if (Helper.InvoiceHelper.IsCash(_InvoiceId))
                        {
                            Helper.InvoiceHelper.PrintToXprinter(_InvoiceId);
                        }
                        else
                        {
                            // HACK: 月結
                        }
                        #endregion
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
                    case "monthly":
                        MessageBox.Show(oDict.GetWord("paid_monthly") + "?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPaidMonthly_Click));
                        break;
                    case "cash":
                        MessageBox.Show(oDict.GetWord("paid_cash") + "?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPaidCash_Click));
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

        private void cmdPaidCash_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                _CashOrder = true;

                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveInv5())
                    {
                        MessageBox.Show(String.Format("Invoice No. {0} is saved!", _InvoiceNumber.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is ReadOnly...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdPaidMonthly_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                _CashOrder = false;

                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveReceipt())
                    {
                        MessageBox.Show(String.Format("Receipt No. {0} is saved!", _ReceiptHeaderId.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is ReadOnly...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
    }
}