namespace xFilm5.JobOrder
{
    partial class Billing5
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
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.lvwDetails = new Gizmox.WebGUI.Forms.ListView();
            this.colItemID = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colLN = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colItemCode = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colDescription = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colQty = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colUnitAmt = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colDiscount = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colAmount = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
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
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.wspPane.SuspendLayout();
            this.gbxDelivery.SuspendLayout();
            this.gbxBilling.SuspendLayout();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = true;
            this.ansToolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.Size = new System.Drawing.Size(100, 22);
            this.ansToolbar.TabIndex = 0;
            // 
            // wspPane
            // 
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
            this.wspPane.Padding = new Gizmox.WebGUI.Forms.Padding(6);
            this.wspPane.Size = new System.Drawing.Size(499, 534);
            this.wspPane.TabIndex = 1;
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
            this.lvwDetails.Location = new System.Drawing.Point(10, 202);
            this.lvwDetails.Name = "lvwDetails";
            this.lvwDetails.Size = new System.Drawing.Size(478, 325);
            this.lvwDetails.TabIndex = 14;
            // 
            // colItemID
            // 
            this.colItemID.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colItemID.Text = "Item ID";
            this.colItemID.Visible = false;
            this.colItemID.Width = 80;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 24;
            // 
            // colItemCode
            // 
            this.colItemCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colItemCode.Text = "Code";
            this.colItemCode.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colItemCode.Width = 60;
            // 
            // colDescription
            // 
            this.colDescription.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colDescription.Text = "Description";
            this.colDescription.Width = 150;
            // 
            // colQty
            // 
            this.colQty.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colQty.Text = "Qty";
            this.colQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 40;
            // 
            // colUnitAmt
            // 
            this.colUnitAmt.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colUnitAmt.Text = "Unit Amt.";
            this.colUnitAmt.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colUnitAmt.Width = 64;
            // 
            // colDiscount
            // 
            this.colDiscount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colDiscount.Text = "Discount";
            this.colDiscount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colDiscount.Width = 60;
            // 
            // colAmount
            // 
            this.colAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
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
            this.gbxDelivery.TabStop = false;
            this.gbxDelivery.Text = "Delivery Address";
            // 
            // txtDeliveryAddress
            // 
            this.txtDeliveryAddress.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
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
            this.gbxBilling.TabStop = false;
            this.gbxBilling.Text = "Billing Address";
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.txtBillingAddress.Location = new System.Drawing.Point(3, 16);
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.ReadOnly = true;
            this.txtBillingAddress.Size = new System.Drawing.Size(234, 101);
            this.txtBillingAddress.TabIndex = 0;
            // 
            // chkPickup
            // 
            this.chkPickup.Location = new System.Drawing.Point(428, 10);
            this.chkPickup.Name = "chkPickup";
            this.chkPickup.Size = new System.Drawing.Size(60, 24);
            this.chkPickup.TabIndex = 1;
            this.chkPickup.Text = "Pick Up";
            // 
            // chkDeliverTo
            // 
            this.chkDeliverTo.Location = new System.Drawing.Point(255, 10);
            this.chkDeliverTo.Name = "chkDeliverTo";
            this.chkDeliverTo.Size = new System.Drawing.Size(100, 20);
            this.chkDeliverTo.TabIndex = 0;
            this.chkDeliverTo.Text = "Deliver To:";
            // 
            // Billing5
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.ansToolbar);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(499, 562);
            this.Text = "Job Order > Billing v5";
            this.wspPane.ResumeLayout(false);
            this.gbxDelivery.ResumeLayout(false);
            this.gbxBilling.ResumeLayout(false);
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
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.RichTextBox txtBillingAddress;
        private Gizmox.WebGUI.Forms.RichTextBox txtDeliveryAddress;
        private Gizmox.WebGUI.Forms.ColumnHeader colItemID;


    }
}