namespace xFilm5.JobOrder
{
    partial class Billing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.gbxItemPane = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdDeleteItem = new Gizmox.WebGUI.Forms.Button();
            this.cmdSaveItem = new Gizmox.WebGUI.Forms.Button();
            this.txtItemAmount = new Gizmox.WebGUI.Forms.TextBox();
            this.txtItemDiscount = new Gizmox.WebGUI.Forms.TextBox();
            this.txtItemUnitAmt = new Gizmox.WebGUI.Forms.TextBox();
            this.txtItemQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtItemDescription = new Gizmox.WebGUI.Forms.TextBox();
            this.cboItemCode = new Gizmox.WebGUI.Forms.ComboBox();
            this.txtItemId = new Gizmox.WebGUI.Forms.TextBox();
            this.lblItemAmount = new Gizmox.WebGUI.Forms.Label();
            this.lblItemDiscount = new Gizmox.WebGUI.Forms.Label();
            this.lblItemUnitAmt = new Gizmox.WebGUI.Forms.Label();
            this.lblItemQty = new Gizmox.WebGUI.Forms.Label();
            this.lblItemDescription = new Gizmox.WebGUI.Forms.Label();
            this.lblItemCode = new Gizmox.WebGUI.Forms.Label();
            this.lblItemId = new Gizmox.WebGUI.Forms.Label();
            this.txtRemarks = new Gizmox.WebGUI.Forms.TextBox();
            this.lblRemarks = new Gizmox.WebGUI.Forms.Label();
            this.lvwDetails = new Gizmox.WebGUI.Forms.ListView();
            this.colItemID = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colItemCode = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colDescription = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colQty = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colUnitAmt = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colDiscount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.cboPaidBy = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblPaidBy = new Gizmox.WebGUI.Forms.Label();
            this.txtInvoiceAmt = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInvoiceAmount = new Gizmox.WebGUI.Forms.Label();
            this.txtInvoiceDate = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInvoiceDate = new Gizmox.WebGUI.Forms.Label();
            this.txtInvoiceNumber = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInvoiceNumber = new Gizmox.WebGUI.Forms.Label();
            this.txtOrderId = new Gizmox.WebGUI.Forms.TextBox();
            this.lblOrderId = new Gizmox.WebGUI.Forms.Label();
            this.gbxDelivery = new Gizmox.WebGUI.Forms.GroupBox();
            this.txtDeliveryAddress = new Gizmox.WebGUI.Forms.RichTextBox();
            this.gbxBilling = new Gizmox.WebGUI.Forms.GroupBox();
            this.txtBillingAddress = new Gizmox.WebGUI.Forms.RichTextBox();
            this.chkPickup = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDeliverTo = new Gizmox.WebGUI.Forms.CheckBox();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.boxPayment = new Gizmox.WebGUI.Forms.GroupBox();
            this.lblPaid = new Gizmox.WebGUI.Forms.Label();
            this.chkPaid = new Gizmox.WebGUI.Forms.CheckBox();
            this.lblPaidOn = new Gizmox.WebGUI.Forms.Label();
            this.txtPaidOn = new Gizmox.WebGUI.Forms.TextBox();
            this.lblReference = new Gizmox.WebGUI.Forms.Label();
            this.txtReference = new Gizmox.WebGUI.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 0;
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Controls.Add(this.boxPayment);
            this.wspPane.Controls.Add(this.gbxItemPane);
            this.wspPane.Controls.Add(this.txtRemarks);
            this.wspPane.Controls.Add(this.lblRemarks);
            this.wspPane.Controls.Add(this.lvwDetails);
            this.wspPane.Controls.Add(this.cboPaidBy);
            this.wspPane.Controls.Add(this.lblPaidBy);
            this.wspPane.Controls.Add(this.txtInvoiceAmt);
            this.wspPane.Controls.Add(this.lblInvoiceAmount);
            this.wspPane.Controls.Add(this.txtInvoiceDate);
            this.wspPane.Controls.Add(this.lblInvoiceDate);
            this.wspPane.Controls.Add(this.txtInvoiceNumber);
            this.wspPane.Controls.Add(this.lblInvoiceNumber);
            this.wspPane.Controls.Add(this.txtOrderId);
            this.wspPane.Controls.Add(this.lblOrderId);
            this.wspPane.Controls.Add(this.gbxDelivery);
            this.wspPane.Controls.Add(this.gbxBilling);
            this.wspPane.Controls.Add(this.chkPickup);
            this.wspPane.Controls.Add(this.chkDeliverTo);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.DockPadding.All = 6;
            this.wspPane.Location = new System.Drawing.Point(0, 28);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(499, 534);
            this.wspPane.TabIndex = 1;
            // 
            // gbxItemPane
            // 
            this.gbxItemPane.Controls.Add(this.cmdDeleteItem);
            this.gbxItemPane.Controls.Add(this.cmdSaveItem);
            this.gbxItemPane.Controls.Add(this.txtItemAmount);
            this.gbxItemPane.Controls.Add(this.txtItemDiscount);
            this.gbxItemPane.Controls.Add(this.txtItemUnitAmt);
            this.gbxItemPane.Controls.Add(this.txtItemQty);
            this.gbxItemPane.Controls.Add(this.txtItemDescription);
            this.gbxItemPane.Controls.Add(this.cboItemCode);
            this.gbxItemPane.Controls.Add(this.txtItemId);
            this.gbxItemPane.Controls.Add(this.lblItemAmount);
            this.gbxItemPane.Controls.Add(this.lblItemDiscount);
            this.gbxItemPane.Controls.Add(this.lblItemUnitAmt);
            this.gbxItemPane.Controls.Add(this.lblItemQty);
            this.gbxItemPane.Controls.Add(this.lblItemDescription);
            this.gbxItemPane.Controls.Add(this.lblItemCode);
            this.gbxItemPane.Controls.Add(this.lblItemId);
            this.gbxItemPane.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxItemPane.Location = new System.Drawing.Point(6, 333);
            this.gbxItemPane.Name = "gbxItemPane";
            this.gbxItemPane.Size = new System.Drawing.Size(486, 114);
            this.gbxItemPane.TabIndex = 19;
            this.gbxItemPane.Text = "Invoice Item";
            // 
            // cmdDeleteItem
            // 
            this.cmdDeleteItem.ClickOnce = true;
            this.cmdDeleteItem.Location = new System.Drawing.Point(361, 71);
            this.cmdDeleteItem.Name = "cmdDeleteItem";
            this.cmdDeleteItem.Size = new System.Drawing.Size(60, 22);
            this.cmdDeleteItem.TabIndex = 15;
            this.cmdDeleteItem.Text = "Delete";
            this.cmdDeleteItem.Visible = false;
            this.cmdDeleteItem.Click += new System.EventHandler(this.cmdDeleteItem_Click);
            // 
            // cmdSaveItem
            // 
            this.cmdSaveItem.ClickOnce = true;
            this.cmdSaveItem.Location = new System.Drawing.Point(421, 71);
            this.cmdSaveItem.Name = "cmdSaveItem";
            this.cmdSaveItem.Size = new System.Drawing.Size(60, 22);
            this.cmdSaveItem.TabIndex = 14;
            this.cmdSaveItem.Text = "Save";
            this.cmdSaveItem.Click += new System.EventHandler(this.cmdSaveItem_Click);
            // 
            // txtItemAmount
            // 
            this.txtItemAmount.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemAmount.Location = new System.Drawing.Point(421, 44);
            this.txtItemAmount.Name = "txtItemAmount";
            this.txtItemAmount.ReadOnly = true;
            this.txtItemAmount.Size = new System.Drawing.Size(60, 20);
            this.txtItemAmount.TabIndex = 13;
            this.txtItemAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtItemDiscount
            // 
            this.txtItemDiscount.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemDiscount.Location = new System.Drawing.Point(361, 44);
            this.txtItemDiscount.Name = "txtItemDiscount";
            this.txtItemDiscount.Size = new System.Drawing.Size(60, 20);
            this.txtItemDiscount.TabIndex = 12;
            this.txtItemDiscount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtItemUnitAmt
            // 
            this.txtItemUnitAmt.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemUnitAmt.Location = new System.Drawing.Point(301, 44);
            this.txtItemUnitAmt.Name = "txtItemUnitAmt";
            this.txtItemUnitAmt.Size = new System.Drawing.Size(60, 20);
            this.txtItemUnitAmt.TabIndex = 11;
            this.txtItemUnitAmt.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtItemQty
            // 
            this.txtItemQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemQty.Location = new System.Drawing.Point(261, 44);
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Size = new System.Drawing.Size(40, 20);
            this.txtItemQty.TabIndex = 10;
            this.txtItemQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemDescription.Location = new System.Drawing.Point(111, 44);
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtItemDescription.Size = new System.Drawing.Size(150, 48);
            this.txtItemDescription.TabIndex = 9;
            // 
            // cboItemCode
            // 
            this.cboItemCode.BackColor = System.Drawing.Color.LightYellow;
            this.cboItemCode.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboItemCode.Location = new System.Drawing.Point(28, 44);
            this.cboItemCode.Name = "cboItemCode";
            this.cboItemCode.Size = new System.Drawing.Size(84, 21);
            this.cboItemCode.TabIndex = 8;
            this.cboItemCode.SelectedIndexChanged += new System.EventHandler(this.cboItemCode_SelectedIndexChanged);
            // 
            // txtItemId
            // 
            this.txtItemId.BackColor = System.Drawing.Color.LightYellow;
            this.txtItemId.Location = new System.Drawing.Point(4, 44);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.Size = new System.Drawing.Size(24, 20);
            this.txtItemId.TabIndex = 7;
            this.txtItemId.Visible = false;
            // 
            // lblItemAmount
            // 
            this.lblItemAmount.Location = new System.Drawing.Point(421, 20);
            this.lblItemAmount.Name = "lblItemAmount";
            this.lblItemAmount.Size = new System.Drawing.Size(60, 20);
            this.lblItemAmount.TabIndex = 6;
            this.lblItemAmount.Text = "Amount";
            this.lblItemAmount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemDiscount
            // 
            this.lblItemDiscount.Location = new System.Drawing.Point(361, 20);
            this.lblItemDiscount.Name = "lblItemDiscount";
            this.lblItemDiscount.Size = new System.Drawing.Size(60, 20);
            this.lblItemDiscount.TabIndex = 5;
            this.lblItemDiscount.Text = "Discount";
            this.lblItemDiscount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemUnitAmt
            // 
            this.lblItemUnitAmt.Location = new System.Drawing.Point(301, 20);
            this.lblItemUnitAmt.Name = "lblItemUnitAmt";
            this.lblItemUnitAmt.Size = new System.Drawing.Size(60, 20);
            this.lblItemUnitAmt.TabIndex = 4;
            this.lblItemUnitAmt.Text = "Unit Amt.";
            this.lblItemUnitAmt.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemQty
            // 
            this.lblItemQty.Location = new System.Drawing.Point(261, 20);
            this.lblItemQty.Name = "lblItemQty";
            this.lblItemQty.Size = new System.Drawing.Size(40, 20);
            this.lblItemQty.TabIndex = 3;
            this.lblItemQty.Text = "Qty";
            this.lblItemQty.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.Location = new System.Drawing.Point(111, 20);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(150, 20);
            this.lblItemDescription.TabIndex = 2;
            this.lblItemDescription.Text = "Description";
            this.lblItemDescription.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Location = new System.Drawing.Point(28, 20);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(84, 20);
            this.lblItemCode.TabIndex = 1;
            this.lblItemCode.Text = "Code";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItemId
            // 
            this.lblItemId.Location = new System.Drawing.Point(4, 20);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(24, 20);
            this.lblItemId.TabIndex = 0;
            this.lblItemId.Text = "ID";
            this.lblItemId.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblItemId.Visible = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(87, 307);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(401, 20);
            this.txtRemarks.TabIndex = 16;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(10, 307);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(77, 20);
            this.lblRemarks.TabIndex = 15;
            this.lblRemarks.Text = "Remarks:";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvwDetails
            // 
            this.lvwDetails.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colItemID,
            this.colLN,
            this.colItemCode,
            this.colDescription,
            this.colQty,
            this.colUnitAmt,
            this.colDiscount,
            this.colAmount});
            this.lvwDetails.DataMember = null;
            this.lvwDetails.GridLines = true;
            this.lvwDetails.ItemsPerPage = 20;
            this.lvwDetails.Location = new System.Drawing.Point(10, 202);
            this.lvwDetails.Name = "lvwDetails";
            this.lvwDetails.Size = new System.Drawing.Size(478, 100);
            this.lvwDetails.TabIndex = 14;
            this.lvwDetails.DoubleClick += new System.EventHandler(this.lvwDetails_DoubleClick);
            // 
            // colItemID
            // 
            this.colItemID.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colItemID.Image = null;
            this.colItemID.Text = "Item ID";
            this.colItemID.Visible = false;
            this.colItemID.Width = 80;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 24;
            // 
            // colItemCode
            // 
            this.colItemCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colItemCode.Image = null;
            this.colItemCode.Text = "Code";
            this.colItemCode.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colItemCode.Width = 60;
            // 
            // colDescription
            // 
            this.colDescription.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colDescription.Image = null;
            this.colDescription.Text = "Description";
            this.colDescription.Width = 150;
            // 
            // colQty
            // 
            this.colQty.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colQty.Image = null;
            this.colQty.Text = "Qty";
            this.colQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 40;
            // 
            // colUnitAmt
            // 
            this.colUnitAmt.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colUnitAmt.Image = null;
            this.colUnitAmt.Text = "Unit Amt.";
            this.colUnitAmt.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colUnitAmt.Width = 64;
            // 
            // colDiscount
            // 
            this.colDiscount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colDiscount.Image = null;
            this.colDiscount.Text = "Discount";
            this.colDiscount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colDiscount.Width = 60;
            // 
            // colAmount
            // 
            this.colAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colAmount.Image = null;
            this.colAmount.Text = "Amount";
            this.colAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colAmount.Width = 60;
            // 
            // cboPaidBy
            // 
            this.cboPaidBy.BackColor = System.Drawing.Color.LightYellow;
            this.cboPaidBy.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboPaidBy.Location = new System.Drawing.Point(398, 173);
            this.cboPaidBy.Name = "cboPaidBy";
            this.cboPaidBy.Size = new System.Drawing.Size(90, 21);
            this.cboPaidBy.TabIndex = 13;
            // 
            // lblPaidBy
            // 
            this.lblPaidBy.Location = new System.Drawing.Point(398, 150);
            this.lblPaidBy.Name = "lblPaidBy";
            this.lblPaidBy.Size = new System.Drawing.Size(94, 20);
            this.lblPaidBy.TabIndex = 12;
            this.lblPaidBy.Text = "Paid By";
            this.lblPaidBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtInvoiceAmt
            // 
            this.txtInvoiceAmt.Location = new System.Drawing.Point(301, 173);
            this.txtInvoiceAmt.Name = "txtInvoiceAmt";
            this.txtInvoiceAmt.Size = new System.Drawing.Size(90, 20);
            this.txtInvoiceAmt.TabIndex = 11;
            this.txtInvoiceAmt.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.Location = new System.Drawing.Point(301, 150);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Size = new System.Drawing.Size(90, 20);
            this.lblInvoiceAmount.TabIndex = 10;
            this.lblInvoiceAmount.Text = "Invoice Amount";
            this.lblInvoiceAmount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.Location = new System.Drawing.Point(204, 173);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Size = new System.Drawing.Size(90, 20);
            this.txtInvoiceDate.TabIndex = 9;
            this.txtInvoiceDate.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.Location = new System.Drawing.Point(204, 150);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(90, 20);
            this.lblInvoiceDate.TabIndex = 8;
            this.lblInvoiceDate.Text = "Invoice Date";
            this.lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(107, 173);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(90, 20);
            this.txtInvoiceNumber.TabIndex = 7;
            this.txtInvoiceNumber.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Location = new System.Drawing.Point(107, 150);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(90, 20);
            this.lblInvoiceNumber.TabIndex = 6;
            this.lblInvoiceNumber.Text = "Invoice No.";
            this.lblInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(10, 173);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(90, 20);
            this.txtOrderId.TabIndex = 5;
            this.txtOrderId.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // lblOrderId
            // 
            this.lblOrderId.Location = new System.Drawing.Point(10, 150);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(90, 20);
            this.lblOrderId.TabIndex = 4;
            this.lblOrderId.Text = "Order ID";
            this.lblOrderId.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // gbxDelivery
            // 
            this.gbxDelivery.Controls.Add(this.txtDeliveryAddress);
            this.gbxDelivery.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxDelivery.Location = new System.Drawing.Point(251, 30);
            this.gbxDelivery.Name = "gbxDelivery";
            this.gbxDelivery.Size = new System.Drawing.Size(240, 120);
            this.gbxDelivery.TabIndex = 3;
            this.gbxDelivery.Text = "Delivery Address";
            // 
            // txtDeliveryAddress
            // 
            this.txtDeliveryAddress.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.txtDeliveryAddress.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.txtDeliveryAddress.Html = "";
            this.txtDeliveryAddress.Location = new System.Drawing.Point(3, 16);
            this.txtDeliveryAddress.Name = "txtDeliveryAddress";
            this.txtDeliveryAddress.ReadOnly = true;
            this.txtDeliveryAddress.Size = new System.Drawing.Size(234, 101);
            this.txtDeliveryAddress.TabIndex = 0;
            // 
            // gbxBilling
            // 
            this.gbxBilling.Controls.Add(this.txtBillingAddress);
            this.gbxBilling.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxBilling.Location = new System.Drawing.Point(6, 30);
            this.gbxBilling.Name = "gbxBilling";
            this.gbxBilling.Size = new System.Drawing.Size(240, 120);
            this.gbxBilling.TabIndex = 2;
            this.gbxBilling.Text = "Billing Address";
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.txtBillingAddress.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.txtBillingAddress.Html = "";
            this.txtBillingAddress.Location = new System.Drawing.Point(3, 16);
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.ReadOnly = true;
            this.txtBillingAddress.Size = new System.Drawing.Size(234, 101);
            this.txtBillingAddress.TabIndex = 0;
            // 
            // chkPickup
            // 
            this.chkPickup.Checked = false;
            this.chkPickup.CheckState = Gizmox.WebGUI.Forms.CheckState.Unchecked;
            this.chkPickup.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Standard;
            this.chkPickup.Location = new System.Drawing.Point(428, 10);
            this.chkPickup.Name = "chkPickup";
            this.chkPickup.Size = new System.Drawing.Size(60, 24);
            this.chkPickup.TabIndex = 1;
            this.chkPickup.Text = "Pick Up";
            this.chkPickup.ThreeState = false;
            // 
            // chkDeliverTo
            // 
            this.chkDeliverTo.Checked = false;
            this.chkDeliverTo.CheckState = Gizmox.WebGUI.Forms.CheckState.Unchecked;
            this.chkDeliverTo.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Standard;
            this.chkDeliverTo.Location = new System.Drawing.Point(255, 10);
            this.chkDeliverTo.Name = "chkDeliverTo";
            this.chkDeliverTo.Size = new System.Drawing.Size(100, 20);
            this.chkDeliverTo.TabIndex = 0;
            this.chkDeliverTo.Text = "Deliver To:";
            this.chkDeliverTo.ThreeState = false;
            // 
            // boxPayment
            // 
            this.boxPayment.Controls.Add(this.txtReference);
            this.boxPayment.Controls.Add(this.lblReference);
            this.boxPayment.Controls.Add(this.txtPaidOn);
            this.boxPayment.Controls.Add(this.lblPaidOn);
            this.boxPayment.Controls.Add(this.chkPaid);
            this.boxPayment.Controls.Add(this.lblPaid);
            this.boxPayment.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxPayment.Location = new System.Drawing.Point(6, 454);
            this.boxPayment.Name = "boxPayment";
            this.boxPayment.Size = new System.Drawing.Size(485, 71);
            this.boxPayment.TabIndex = 20;
            this.boxPayment.Text = "Payment";
            this.boxPayment.Visible = false;
            // 
            // lblPaid
            // 
            this.lblPaid.Location = new System.Drawing.Point(12, 16);
            this.lblPaid.Name = "lblPaid";
            this.lblPaid.Size = new System.Drawing.Size(100, 20);
            this.lblPaid.TabIndex = 0;
            this.lblPaid.Text = "Paid:";
            // 
            // chkPaid
            // 
            this.chkPaid.Checked = false;
            this.chkPaid.CheckState = Gizmox.WebGUI.Forms.CheckState.Unchecked;
            this.chkPaid.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Standard;
            this.chkPaid.Location = new System.Drawing.Point(112, 16);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Size = new System.Drawing.Size(104, 24);
            this.chkPaid.TabIndex = 1;
            this.chkPaid.ThreeState = false;
            // 
            // lblPaidOn
            // 
            this.lblPaidOn.Location = new System.Drawing.Point(12, 40);
            this.lblPaidOn.Name = "lblPaidOn";
            this.lblPaidOn.Size = new System.Drawing.Size(100, 23);
            this.lblPaidOn.TabIndex = 2;
            this.lblPaidOn.Text = "Paid On:";
            // 
            // txtPaidOn
            // 
            this.txtPaidOn.Location = new System.Drawing.Point(112, 40);
            this.txtPaidOn.Name = "txtPaidOn";
            this.txtPaidOn.Size = new System.Drawing.Size(100, 20);
            this.txtPaidOn.TabIndex = 3;
            // 
            // lblReference
            // 
            this.lblReference.Location = new System.Drawing.Point(236, 16);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 20);
            this.lblReference.TabIndex = 4;
            this.lblReference.Text = "Reference:";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(236, 40);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(243, 20);
            this.txtReference.TabIndex = 5;
            // 
            // Billing
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.ansToolbar);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(499, 562);
            this.Text = "Job Order > Billing";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.GroupBox gbxDelivery;
        private Gizmox.WebGUI.Forms.GroupBox gbxBilling;
        private Gizmox.WebGUI.Forms.CheckBox chkPickup;
        private Gizmox.WebGUI.Forms.CheckBox chkDeliverTo;
        private Gizmox.WebGUI.Forms.TextBox txtOrderId;
        private Gizmox.WebGUI.Forms.Label lblOrderId;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceNumber;
        private Gizmox.WebGUI.Forms.Label lblInvoiceNumber;
        private Gizmox.WebGUI.Forms.ComboBox cboPaidBy;
        private Gizmox.WebGUI.Forms.Label lblPaidBy;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceAmt;
        private Gizmox.WebGUI.Forms.Label lblInvoiceAmount;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceDate;
        private Gizmox.WebGUI.Forms.Label lblInvoiceDate;
        private Gizmox.WebGUI.Forms.ListView lvwDetails;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.ColumnHeader colItemCode;
        private Gizmox.WebGUI.Forms.ColumnHeader colDescription;
        private Gizmox.WebGUI.Forms.ColumnHeader colQty;
        private Gizmox.WebGUI.Forms.ColumnHeader colUnitAmt;
        private Gizmox.WebGUI.Forms.ColumnHeader colDiscount;
        private Gizmox.WebGUI.Forms.ColumnHeader colAmount;
        private Gizmox.WebGUI.Forms.TextBox txtRemarks;
        private Gizmox.WebGUI.Forms.Label lblRemarks;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.RichTextBox txtBillingAddress;
        private Gizmox.WebGUI.Forms.RichTextBox txtDeliveryAddress;
        private Gizmox.WebGUI.Forms.ColumnHeader colItemID;
        private Gizmox.WebGUI.Forms.GroupBox gbxItemPane;
        private Gizmox.WebGUI.Forms.Label lblItemId;
        private Gizmox.WebGUI.Forms.Label lblItemDescription;
        private Gizmox.WebGUI.Forms.Label lblItemCode;
        private Gizmox.WebGUI.Forms.TextBox txtItemId;
        private Gizmox.WebGUI.Forms.Label lblItemAmount;
        private Gizmox.WebGUI.Forms.Label lblItemDiscount;
        private Gizmox.WebGUI.Forms.Label lblItemUnitAmt;
        private Gizmox.WebGUI.Forms.Label lblItemQty;
        private Gizmox.WebGUI.Forms.TextBox txtItemAmount;
        private Gizmox.WebGUI.Forms.TextBox txtItemDiscount;
        private Gizmox.WebGUI.Forms.TextBox txtItemUnitAmt;
        private Gizmox.WebGUI.Forms.TextBox txtItemQty;
        private Gizmox.WebGUI.Forms.TextBox txtItemDescription;
        private Gizmox.WebGUI.Forms.ComboBox cboItemCode;
        private Gizmox.WebGUI.Forms.Button cmdSaveItem;
        private Gizmox.WebGUI.Forms.Button cmdDeleteItem;
        private Gizmox.WebGUI.Forms.GroupBox boxPayment;
        private Gizmox.WebGUI.Forms.Label lblReference;
        private Gizmox.WebGUI.Forms.TextBox txtPaidOn;
        private Gizmox.WebGUI.Forms.Label lblPaidOn;
        private Gizmox.WebGUI.Forms.CheckBox chkPaid;
        private Gizmox.WebGUI.Forms.Label lblPaid;
        private Gizmox.WebGUI.Forms.TextBox txtReference;


    }
}