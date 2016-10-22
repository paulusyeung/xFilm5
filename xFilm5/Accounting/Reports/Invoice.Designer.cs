namespace xFilm5.Accounting.Reports
{
    partial class Invoice
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.Code39Generator code39Generator1 = new DevExpress.XtraPrinting.BarCode.Code39Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.txtItemAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemDiscount = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemUoM = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemUnitAmt = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemQty = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItem = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemCode = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.txtInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtInvoiceDate = new DevExpress.XtraReports.UI.XRLabel();
            this.txtPaidBy = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCheckedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.txtPrepressOp = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTimeOut = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTimeIn = new DevExpress.XtraReports.UI.XRLabel();
            this.txtOrderedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.txtOrderID = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliveryFax = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliveryTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBillingFax = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBillingTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliveryAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBillingAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.txtPickup = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliverTo = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBarcode = new DevExpress.XtraReports.UI.XRBarCode();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.txtTimeStamp = new DevExpress.XtraReports.UI.XRLabel();
            this.txtPageTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.txtInvoiceAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.txtRemarks = new DevExpress.XtraReports.UI.XRLabel();
            this.lblRemarks = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtItemAmount,
            this.txtItemDiscount,
            this.txtItemUoM,
            this.txtItemUnitAmt,
            this.txtItemQty,
            this.txtItemDescription,
            this.txtItem,
            this.txtItemCode});
            this.Detail.HeightF = 21F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // txtItemAmount
            // 
            this.txtItemAmount.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemAmount.LocationFloat = new DevExpress.Utils.PointFloat(700F, 0F);
            this.txtItemAmount.Name = "txtItemAmount";
            this.txtItemAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemAmount.SizeF = new System.Drawing.SizeF(75F, 20F);
            this.txtItemAmount.Text = "txtItemAmount";
            this.txtItemAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtItemDiscount
            // 
            this.txtItemDiscount.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemDiscount.LocationFloat = new DevExpress.Utils.PointFloat(650F, 0F);
            this.txtItemDiscount.Name = "txtItemDiscount";
            this.txtItemDiscount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemDiscount.SizeF = new System.Drawing.SizeF(50F, 20F);
            this.txtItemDiscount.Text = "txtItemDiscount";
            this.txtItemDiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtItemUoM
            // 
            this.txtItemUoM.CanGrow = false;
            this.txtItemUoM.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemUoM.LocationFloat = new DevExpress.Utils.PointFloat(608F, 0F);
            this.txtItemUoM.Name = "txtItemUoM";
            this.txtItemUoM.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemUoM.SizeF = new System.Drawing.SizeF(42F, 20F);
            this.txtItemUoM.Text = "txtItemUoM";
            this.txtItemUoM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtItemUnitAmt
            // 
            this.txtItemUnitAmt.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemUnitAmt.LocationFloat = new DevExpress.Utils.PointFloat(542F, 0F);
            this.txtItemUnitAmt.Name = "txtItemUnitAmt";
            this.txtItemUnitAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemUnitAmt.SizeF = new System.Drawing.SizeF(66F, 20F);
            this.txtItemUnitAmt.Text = "txtItemUnitAmt";
            this.txtItemUnitAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtItemQty
            // 
            this.txtItemQty.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemQty.LocationFloat = new DevExpress.Utils.PointFloat(500F, 0F);
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemQty.SizeF = new System.Drawing.SizeF(42F, 20F);
            this.txtItemQty.Text = "txtItemQty";
            this.txtItemQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemDescription.LocationFloat = new DevExpress.Utils.PointFloat(125F, 0F);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemDescription.SizeF = new System.Drawing.SizeF(375F, 20F);
            this.txtItemDescription.Text = "txtItemDescription";
            this.txtItemDescription.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtItem
            // 
            this.txtItem.CanGrow = false;
            this.txtItem.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItem.LocationFloat = new DevExpress.Utils.PointFloat(67F, 0F);
            this.txtItem.Name = "txtItem";
            this.txtItem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItem.SizeF = new System.Drawing.SizeF(58F, 20F);
            this.txtItem.Text = "txtItem";
            this.txtItem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtItemCode.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtItemCode.SizeF = new System.Drawing.SizeF(59F, 20F);
            this.txtItemCode.Text = "txtItemCode";
            this.txtItemCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtInvoiceNumber,
            this.txtInvoiceDate,
            this.txtPaidBy,
            this.txtCheckedBy,
            this.txtPrepressOp,
            this.txtTimeOut,
            this.txtTimeIn,
            this.txtOrderedBy,
            this.txtOrderID,
            this.txtDeliveryFax,
            this.txtDeliveryTel,
            this.txtBillingFax,
            this.txtBillingTel,
            this.txtDeliveryAddress,
            this.txtBillingAddress,
            this.txtPickup,
            this.txtDeliverTo,
            this.txtBarcode});
            this.PageHeader.HeightF = 226F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.CanGrow = false;
            this.txtInvoiceNumber.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(675F, 42F);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceNumber.SizeF = new System.Drawing.SizeF(108F, 20F);
            this.txtInvoiceNumber.Text = "txtInvoiceNumber";
            this.txtInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.CanGrow = false;
            this.txtInvoiceDate.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtInvoiceDate.LocationFloat = new DevExpress.Utils.PointFloat(675F, 83F);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceDate.SizeF = new System.Drawing.SizeF(108F, 20F);
            this.txtInvoiceDate.Text = "txtInvoiceDate";
            this.txtInvoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtPaidBy
            // 
            this.txtPaidBy.CanGrow = false;
            this.txtPaidBy.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtPaidBy.LocationFloat = new DevExpress.Utils.PointFloat(675F, 117F);
            this.txtPaidBy.Name = "txtPaidBy";
            this.txtPaidBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtPaidBy.SizeF = new System.Drawing.SizeF(108F, 20F);
            this.txtPaidBy.Text = "txtPaidBy";
            this.txtPaidBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtCheckedBy
            // 
            this.txtCheckedBy.CanGrow = false;
            this.txtCheckedBy.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtCheckedBy.LocationFloat = new DevExpress.Utils.PointFloat(675F, 175F);
            this.txtCheckedBy.Name = "txtCheckedBy";
            this.txtCheckedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtCheckedBy.SizeF = new System.Drawing.SizeF(108F, 20F);
            this.txtCheckedBy.Text = "txtCheckedBy";
            this.txtCheckedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtPrepressOp
            // 
            this.txtPrepressOp.CanGrow = false;
            this.txtPrepressOp.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtPrepressOp.LocationFloat = new DevExpress.Utils.PointFloat(542F, 175F);
            this.txtPrepressOp.Name = "txtPrepressOp";
            this.txtPrepressOp.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtPrepressOp.SizeF = new System.Drawing.SizeF(108F, 20F);
            this.txtPrepressOp.Text = "txtPrepressOp";
            this.txtPrepressOp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.CanGrow = false;
            this.txtTimeOut.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtTimeOut.LocationFloat = new DevExpress.Utils.PointFloat(417F, 175F);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTimeOut.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtTimeOut.Text = "txtTimeOut";
            this.txtTimeOut.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtTimeIn
            // 
            this.txtTimeIn.CanGrow = false;
            this.txtTimeIn.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtTimeIn.LocationFloat = new DevExpress.Utils.PointFloat(292F, 175F);
            this.txtTimeIn.Name = "txtTimeIn";
            this.txtTimeIn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTimeIn.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtTimeIn.Text = "txtTimeIn";
            this.txtTimeIn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtOrderedBy
            // 
            this.txtOrderedBy.CanGrow = false;
            this.txtOrderedBy.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtOrderedBy.LocationFloat = new DevExpress.Utils.PointFloat(167F, 175F);
            this.txtOrderedBy.Name = "txtOrderedBy";
            this.txtOrderedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtOrderedBy.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtOrderedBy.Text = "txtOrderedBy";
            this.txtOrderedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtOrderID
            // 
            this.txtOrderID.CanGrow = false;
            this.txtOrderID.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtOrderID.LocationFloat = new DevExpress.Utils.PointFloat(67F, 175F);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtOrderID.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtOrderID.Text = "txtOrderID";
            this.txtOrderID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtDeliveryFax
            // 
            this.txtDeliveryFax.CanGrow = false;
            this.txtDeliveryFax.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtDeliveryFax.LocationFloat = new DevExpress.Utils.PointFloat(500F, 117F);
            this.txtDeliveryFax.Name = "txtDeliveryFax";
            this.txtDeliveryFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDeliveryFax.SizeF = new System.Drawing.SizeF(150F, 21F);
            this.txtDeliveryFax.Text = "txtDeliveryFax";
            this.txtDeliveryFax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtDeliveryTel
            // 
            this.txtDeliveryTel.CanGrow = false;
            this.txtDeliveryTel.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtDeliveryTel.LocationFloat = new DevExpress.Utils.PointFloat(358F, 117F);
            this.txtDeliveryTel.Name = "txtDeliveryTel";
            this.txtDeliveryTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDeliveryTel.SizeF = new System.Drawing.SizeF(142F, 21F);
            this.txtDeliveryTel.Text = "txtDeliveryTel";
            this.txtDeliveryTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtBillingFax
            // 
            this.txtBillingFax.CanGrow = false;
            this.txtBillingFax.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtBillingFax.LocationFloat = new DevExpress.Utils.PointFloat(208F, 117F);
            this.txtBillingFax.Name = "txtBillingFax";
            this.txtBillingFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtBillingFax.SizeF = new System.Drawing.SizeF(142F, 21F);
            this.txtBillingFax.Text = "txtBillingFax";
            this.txtBillingFax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtBillingTel
            // 
            this.txtBillingTel.CanGrow = false;
            this.txtBillingTel.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtBillingTel.LocationFloat = new DevExpress.Utils.PointFloat(67F, 117F);
            this.txtBillingTel.Name = "txtBillingTel";
            this.txtBillingTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtBillingTel.SizeF = new System.Drawing.SizeF(141F, 21F);
            this.txtBillingTel.Text = "txtBillingTel";
            this.txtBillingTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtDeliveryAddress
            // 
            this.txtDeliveryAddress.CanGrow = false;
            this.txtDeliveryAddress.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeliveryAddress.LocationFloat = new DevExpress.Utils.PointFloat(358F, 25F);
            this.txtDeliveryAddress.Multiline = true;
            this.txtDeliveryAddress.Name = "txtDeliveryAddress";
            this.txtDeliveryAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDeliveryAddress.SizeF = new System.Drawing.SizeF(300F, 92F);
            this.txtDeliveryAddress.Text = "txtDeliveryAddress";
            this.txtDeliveryAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.CanGrow = false;
            this.txtBillingAddress.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingAddress.LocationFloat = new DevExpress.Utils.PointFloat(67F, 25F);
            this.txtBillingAddress.Multiline = true;
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtBillingAddress.SizeF = new System.Drawing.SizeF(291F, 92F);
            this.txtBillingAddress.Text = "txtBillingAddress";
            this.txtBillingAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtPickup
            // 
            this.txtPickup.Font = new System.Drawing.Font("Arial", 8F);
            this.txtPickup.LocationFloat = new DevExpress.Utils.PointFloat(592F, 8F);
            this.txtPickup.Name = "txtPickup";
            this.txtPickup.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtPickup.SizeF = new System.Drawing.SizeF(17F, 17F);
            this.txtPickup.Text = "X";
            this.txtPickup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtDeliverTo
            // 
            this.txtDeliverTo.Font = new System.Drawing.Font("Arial", 8F);
            this.txtDeliverTo.LocationFloat = new DevExpress.Utils.PointFloat(358F, 0F);
            this.txtDeliverTo.Name = "txtDeliverTo";
            this.txtDeliverTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDeliverTo.SizeF = new System.Drawing.SizeF(17F, 17F);
            this.txtDeliverTo.Text = "X";
            this.txtDeliverTo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtBarcode
            // 
            this.txtBarcode.LocationFloat = new DevExpress.Utils.PointFloat(650F, 0F);
            this.txtBarcode.Module = 1F;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.txtBarcode.ShowText = false;
            this.txtBarcode.SizeF = new System.Drawing.SizeF(150F, 25F);
            code39Generator1.CalcCheckSum = false;
            code39Generator1.WideNarrowRatio = 3F;
            this.txtBarcode.Symbology = code39Generator1;
            this.txtBarcode.Text = "123456";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtTimeStamp,
            this.txtPageTitle,
            this.xrLine1,
            this.txtInvoiceAmount,
            this.txtRemarks,
            this.lblRemarks});
            this.PageFooter.HeightF = 133F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtTimeStamp
            // 
            this.txtTimeStamp.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeStamp.LocationFloat = new DevExpress.Utils.PointFloat(308F, 95F);
            this.txtTimeStamp.Name = "txtTimeStamp";
            this.txtTimeStamp.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTimeStamp.SizeF = new System.Drawing.SizeF(184F, 20F);
            this.txtTimeStamp.Text = "txtTimeStamp";
            this.txtTimeStamp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // txtPageTitle
            // 
            this.txtPageTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageTitle.ForeColor = System.Drawing.Color.Red;
            this.txtPageTitle.LocationFloat = new DevExpress.Utils.PointFloat(308F, 75F);
            this.txtPageTitle.Name = "txtPageTitle";
            this.txtPageTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtPageTitle.SizeF = new System.Drawing.SizeF(184F, 20F);
            this.txtPageTitle.Text = "txtPageTitle";
            this.txtPageTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(675F, 87F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine1.SizeF = new System.Drawing.SizeF(100F, 8F);
            // 
            // txtInvoiceAmount
            // 
            this.txtInvoiceAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtInvoiceAmount.Font = new System.Drawing.Font("MingLiU", 9F);
            this.txtInvoiceAmount.LocationFloat = new DevExpress.Utils.PointFloat(675F, 67F);
            this.txtInvoiceAmount.Name = "txtInvoiceAmount";
            this.txtInvoiceAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceAmount.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtInvoiceAmount.Text = "txtInvoiceAmount";
            this.txtInvoiceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.CanGrow = false;
            this.txtRemarks.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.txtRemarks.LocationFloat = new DevExpress.Utils.PointFloat(133F, 33F);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtRemarks.SizeF = new System.Drawing.SizeF(492F, 20F);
            this.txtRemarks.Text = "txtRemarks";
            this.txtRemarks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.LocationFloat = new DevExpress.Utils.PointFloat(67F, 33F);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblRemarks.SizeF = new System.Drawing.SizeF(66F, 20F);
            this.lblRemarks.Text = "Remarks:";
            this.lblRemarks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 113F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // Invoice
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 113, 0);
            this.PageHeight = 583;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A5;
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Invoice_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRBarCode txtBarcode;
        private DevExpress.XtraReports.UI.XRLabel txtPickup;
        private DevExpress.XtraReports.UI.XRLabel txtDeliverTo;
        private DevExpress.XtraReports.UI.XRLabel txtDeliveryAddress;
        private DevExpress.XtraReports.UI.XRLabel txtBillingAddress;
        private DevExpress.XtraReports.UI.XRLabel txtBillingFax;
        private DevExpress.XtraReports.UI.XRLabel txtBillingTel;
        private DevExpress.XtraReports.UI.XRLabel txtOrderedBy;
        private DevExpress.XtraReports.UI.XRLabel txtOrderID;
        private DevExpress.XtraReports.UI.XRLabel txtDeliveryFax;
        private DevExpress.XtraReports.UI.XRLabel txtDeliveryTel;
        private DevExpress.XtraReports.UI.XRLabel txtPrepressOp;
        private DevExpress.XtraReports.UI.XRLabel txtTimeOut;
        private DevExpress.XtraReports.UI.XRLabel txtTimeIn;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceDate;
        private DevExpress.XtraReports.UI.XRLabel txtPaidBy;
        private DevExpress.XtraReports.UI.XRLabel txtCheckedBy;
        private DevExpress.XtraReports.UI.XRLabel txtItem;
        private DevExpress.XtraReports.UI.XRLabel txtItemCode;
        private DevExpress.XtraReports.UI.XRLabel txtItemQty;
        private DevExpress.XtraReports.UI.XRLabel txtItemDescription;
        private DevExpress.XtraReports.UI.XRLabel txtItemAmount;
        private DevExpress.XtraReports.UI.XRLabel txtItemDiscount;
        private DevExpress.XtraReports.UI.XRLabel txtItemUoM;
        private DevExpress.XtraReports.UI.XRLabel txtItemUnitAmt;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceAmount;
        private DevExpress.XtraReports.UI.XRLabel txtRemarks;
        private DevExpress.XtraReports.UI.XRLabel lblRemarks;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel txtTimeStamp;
        private DevExpress.XtraReports.UI.XRLabel txtPageTitle;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
