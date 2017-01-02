namespace xFilm5.JobOrder.Reports5
{
    partial class DN_80mm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DN_80mm));
            DevExpress.XtraPrinting.BarCode.Code39Generator code39Generator1 = new DevExpress.XtraPrinting.BarCode.Code39Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.txtItemAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemQty = new DevExpress.XtraReports.UI.XRLabel();
            this.txtItemDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lblAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.lblQty = new DevExpress.XtraReports.UI.XRLabel();
            this.lblShipTo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBillTo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTransactionDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTransactionNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDeliveryNote = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCompanyAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.txtInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtInvoiceDate = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliveryTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBillingTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDeliveryAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBillingAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBarcode = new DevExpress.XtraReports.UI.XRBarCode();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.txtTimeStamp = new DevExpress.XtraReports.UI.XRLabel();
            this.txtInvoiceAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtItemAmount,
            this.txtItemQty,
            this.txtItemDescription});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 28F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // txtItemAmount
            // 
            this.txtItemAmount.Dpi = 254F;
            this.txtItemAmount.Font = new System.Drawing.Font("Arial Unicode MS", 7F);
            this.txtItemAmount.LocationFloat = new DevExpress.Utils.PointFloat(591.0574F, 0F);
            this.txtItemAmount.Name = "txtItemAmount";
            this.txtItemAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtItemAmount.SizeF = new System.Drawing.SizeF(112.3652F, 28F);
            this.txtItemAmount.StylePriority.UseFont = false;
            this.txtItemAmount.Text = "amount";
            this.txtItemAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtItemQty
            // 
            this.txtItemQty.Dpi = 254F;
            this.txtItemQty.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.txtItemQty.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtItemQty.SizeF = new System.Drawing.SizeF(56.1731F, 28F);
            this.txtItemQty.StylePriority.UseFont = false;
            this.txtItemQty.StylePriority.UseTextAlignment = false;
            this.txtItemQty.Text = "qty";
            this.txtItemQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Dpi = 254F;
            this.txtItemDescription.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.txtItemDescription.LocationFloat = new DevExpress.Utils.PointFloat(68.99523F, 0F);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtItemDescription.SizeF = new System.Drawing.SizeF(509.2401F, 28F);
            this.txtItemDescription.StylePriority.UseFont = false;
            this.txtItemDescription.Text = "description";
            this.txtItemDescription.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAmount,
            this.lblDescription,
            this.lblQty,
            this.lblShipTo,
            this.lblBillTo,
            this.lblTransactionDate,
            this.lblTransactionNumber,
            this.lblDeliveryNote,
            this.txtCompanyAddress,
            this.xrPictureBox1,
            this.txtInvoiceNumber,
            this.txtInvoiceDate,
            this.txtDeliveryTel,
            this.txtBillingTel,
            this.txtDeliveryAddress,
            this.txtBillingAddress});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 815.8364F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
            this.lblAmount.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblAmount.BorderWidth = 2F;
            this.lblAmount.Dpi = 254F;
            this.lblAmount.Font = new System.Drawing.Font("Arial Unicode MS", 7F);
            this.lblAmount.LocationFloat = new DevExpress.Utils.PointFloat(591.0574F, 763.6219F);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblAmount.SizeF = new System.Drawing.SizeF(112.3652F, 49.24689F);
            this.lblAmount.StylePriority.UseBorderDashStyle = false;
            this.lblAmount.StylePriority.UseBorders = false;
            this.lblAmount.StylePriority.UseBorderWidth = false;
            this.lblAmount.StylePriority.UseFont = false;
            this.lblAmount.StylePriority.UseTextAlignment = false;
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
            this.lblDescription.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblDescription.BorderWidth = 2F;
            this.lblDescription.Dpi = 254F;
            this.lblDescription.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.lblDescription.LocationFloat = new DevExpress.Utils.PointFloat(68.99525F, 763.6219F);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDescription.SizeF = new System.Drawing.SizeF(509.2401F, 49.24689F);
            this.lblDescription.StylePriority.UseBorderDashStyle = false;
            this.lblDescription.StylePriority.UseBorders = false;
            this.lblDescription.StylePriority.UseBorderWidth = false;
            this.lblDescription.StylePriority.UseFont = false;
            this.lblDescription.StylePriority.UseTextAlignment = false;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblQty
            // 
            this.lblQty.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
            this.lblQty.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblQty.BorderWidth = 2F;
            this.lblQty.Dpi = 254F;
            this.lblQty.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.lblQty.LocationFloat = new DevExpress.Utils.PointFloat(1.3975E-05F, 763.6219F);
            this.lblQty.Name = "lblQty";
            this.lblQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblQty.SizeF = new System.Drawing.SizeF(56.1731F, 49.24689F);
            this.lblQty.StylePriority.UseBorderDashStyle = false;
            this.lblQty.StylePriority.UseBorders = false;
            this.lblQty.StylePriority.UseBorderWidth = false;
            this.lblQty.StylePriority.UseFont = false;
            this.lblQty.StylePriority.UseTextAlignment = false;
            this.lblQty.Text = "Qty";
            this.lblQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblShipTo
            // 
            this.lblShipTo.Dpi = 254F;
            this.lblShipTo.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipTo.LocationFloat = new DevExpress.Utils.PointFloat(351.7113F, 447.1538F);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblShipTo.SizeF = new System.Drawing.SizeF(254F, 50F);
            this.lblShipTo.StylePriority.UseFont = false;
            this.lblShipTo.Text = "SHIP TO:";
            // 
            // lblBillTo
            // 
            this.lblBillTo.Dpi = 254F;
            this.lblBillTo.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillTo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 447.1538F);
            this.lblBillTo.Name = "lblBillTo";
            this.lblBillTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblBillTo.SizeF = new System.Drawing.SizeF(254F, 50F);
            this.lblBillTo.StylePriority.UseFont = false;
            this.lblBillTo.Text = "BILL TO:";
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.Dpi = 254F;
            this.lblTransactionDate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 346.3714F);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTransactionDate.SizeF = new System.Drawing.SizeF(254F, 50F);
            this.lblTransactionDate.StylePriority.UseFont = false;
            this.lblTransactionDate.Text = "Date Time:";
            // 
            // lblTransactionNumber
            // 
            this.lblTransactionNumber.Dpi = 254F;
            this.lblTransactionNumber.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionNumber.LocationFloat = new DevExpress.Utils.PointFloat(0F, 296.3714F);
            this.lblTransactionNumber.Name = "lblTransactionNumber";
            this.lblTransactionNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTransactionNumber.SizeF = new System.Drawing.SizeF(254F, 50F);
            this.lblTransactionNumber.StylePriority.UseFont = false;
            this.lblTransactionNumber.Text = "Transaction #:";
            // 
            // lblDeliveryNote
            // 
            this.lblDeliveryNote.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDeliveryNote.BorderWidth = 1F;
            this.lblDeliveryNote.Dpi = 254F;
            this.lblDeliveryNote.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeliveryNote.LocationFloat = new DevExpress.Utils.PointFloat(0F, 197.8269F);
            this.lblDeliveryNote.Name = "lblDeliveryNote";
            this.lblDeliveryNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDeliveryNote.SizeF = new System.Drawing.SizeF(703.4227F, 64.16498F);
            this.lblDeliveryNote.StylePriority.UseBorders = false;
            this.lblDeliveryNote.StylePriority.UseBorderWidth = false;
            this.lblDeliveryNote.StylePriority.UseFont = false;
            this.lblDeliveryNote.StylePriority.UseTextAlignment = false;
            this.lblDeliveryNote.Text = "DELIVERY NOTE";
            this.lblDeliveryNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.Dpi = 254F;
            this.txtCompanyAddress.Font = new System.Drawing.Font("Arial Narrow", 6F);
            this.txtCompanyAddress.LocationFloat = new DevExpress.Utils.PointFloat(351.7114F, 0F);
            this.txtCompanyAddress.Multiline = true;
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.txtCompanyAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtCompanyAddress.SizeF = new System.Drawing.SizeF(351.7114F, 197.8269F);
            this.txtCompanyAddress.StylePriority.UseFont = false;
            this.txtCompanyAddress.Text = "txtCompanyAddress";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(351.7114F, 197.8269F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.CanGrow = false;
            this.txtInvoiceNumber.Dpi = 254F;
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(351.7113F, 296.3714F);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtInvoiceNumber.SizeF = new System.Drawing.SizeF(351.71F, 50F);
            this.txtInvoiceNumber.StylePriority.UseFont = false;
            this.txtInvoiceNumber.StylePriority.UseTextAlignment = false;
            this.txtInvoiceNumber.Text = "txtInvoiceNumber";
            this.txtInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.CanGrow = false;
            this.txtInvoiceDate.Dpi = 254F;
            this.txtInvoiceDate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceDate.LocationFloat = new DevExpress.Utils.PointFloat(351.7113F, 346.3714F);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtInvoiceDate.SizeF = new System.Drawing.SizeF(351.71F, 50.79993F);
            this.txtInvoiceDate.StylePriority.UseFont = false;
            this.txtInvoiceDate.StylePriority.UseTextAlignment = false;
            this.txtInvoiceDate.Text = "txtInvoiceDate";
            this.txtInvoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtDeliveryTel
            // 
            this.txtDeliveryTel.CanGrow = false;
            this.txtDeliveryTel.Dpi = 254F;
            this.txtDeliveryTel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeliveryTel.LocationFloat = new DevExpress.Utils.PointFloat(351.7113F, 699.9999F);
            this.txtDeliveryTel.Name = "txtDeliveryTel";
            this.txtDeliveryTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtDeliveryTel.SizeF = new System.Drawing.SizeF(351.71F, 50.79993F);
            this.txtDeliveryTel.StylePriority.UseFont = false;
            this.txtDeliveryTel.Text = "txtDeliveryTel";
            this.txtDeliveryTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtBillingTel
            // 
            this.txtBillingTel.CanGrow = false;
            this.txtBillingTel.Dpi = 254F;
            this.txtBillingTel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingTel.LocationFloat = new DevExpress.Utils.PointFloat(0F, 699.9999F);
            this.txtBillingTel.Name = "txtBillingTel";
            this.txtBillingTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtBillingTel.SizeF = new System.Drawing.SizeF(350F, 50.79993F);
            this.txtBillingTel.StylePriority.UseFont = false;
            this.txtBillingTel.Text = "txtBillingTel";
            this.txtBillingTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtDeliveryAddress
            // 
            this.txtDeliveryAddress.CanGrow = false;
            this.txtDeliveryAddress.Dpi = 254F;
            this.txtDeliveryAddress.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.txtDeliveryAddress.LocationFloat = new DevExpress.Utils.PointFloat(351.7114F, 500F);
            this.txtDeliveryAddress.Multiline = true;
            this.txtDeliveryAddress.Name = "txtDeliveryAddress";
            this.txtDeliveryAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtDeliveryAddress.SizeF = new System.Drawing.SizeF(350F, 200F);
            this.txtDeliveryAddress.StylePriority.UseFont = false;
            this.txtDeliveryAddress.Text = "txtDeliveryAddress";
            this.txtDeliveryAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.CanGrow = false;
            this.txtBillingAddress.Dpi = 254F;
            this.txtBillingAddress.Font = new System.Drawing.Font("Arial Unicode MS", 6F);
            this.txtBillingAddress.LocationFloat = new DevExpress.Utils.PointFloat(0F, 500F);
            this.txtBillingAddress.Multiline = true;
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtBillingAddress.SizeF = new System.Drawing.SizeF(350F, 200F);
            this.txtBillingAddress.StylePriority.UseFont = false;
            this.txtBillingAddress.Text = "txtBillingAddress";
            this.txtBillingAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.txtBarcode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtBarcode.Dpi = 254F;
            this.txtBarcode.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.LocationFloat = new DevExpress.Utils.PointFloat(6.055832E-05F, 50.80003F);
            this.txtBarcode.Module = 2.54F;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.txtBarcode.ShowText = false;
            this.txtBarcode.SizeF = new System.Drawing.SizeF(703.4227F, 80F);
            this.txtBarcode.StylePriority.UseBorders = false;
            this.txtBarcode.StylePriority.UseFont = false;
            this.txtBarcode.StylePriority.UseTextAlignment = false;
            code39Generator1.WideNarrowRatio = 3F;
            this.txtBarcode.Symbology = code39Generator1;
            this.txtBarcode.Text = "123456";
            this.txtBarcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtTimeStamp,
            this.txtInvoiceAmount,
            this.txtBarcode});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 164.9924F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtTimeStamp
            // 
            this.txtTimeStamp.Dpi = 254F;
            this.txtTimeStamp.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeStamp.LocationFloat = new DevExpress.Utils.PointFloat(6.527244E-05F, 130.8F);
            this.txtTimeStamp.Name = "txtTimeStamp";
            this.txtTimeStamp.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtTimeStamp.SizeF = new System.Drawing.SizeF(703.4227F, 34.19235F);
            this.txtTimeStamp.Text = "txtTimeStamp";
            this.txtTimeStamp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // txtInvoiceAmount
            // 
            this.txtInvoiceAmount.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.txtInvoiceAmount.Dpi = 254F;
            this.txtInvoiceAmount.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceAmount.LocationFloat = new DevExpress.Utils.PointFloat(449.4227F, 0F);
            this.txtInvoiceAmount.Name = "txtInvoiceAmount";
            this.txtInvoiceAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtInvoiceAmount.SizeF = new System.Drawing.SizeF(254F, 50.8F);
            this.txtInvoiceAmount.StylePriority.UseBorders = false;
            this.txtInvoiceAmount.StylePriority.UseFont = false;
            this.txtInvoiceAmount.Text = "txtInvoiceAmount";
            this.txtInvoiceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 287F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 81F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // DN_80mm
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 287, 81);
            this.PageHeight = 2969;
            this.PageWidth = 720;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.RollPaper = true;
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Invoice_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRBarCode txtBarcode;
        private DevExpress.XtraReports.UI.XRLabel txtDeliveryAddress;
        private DevExpress.XtraReports.UI.XRLabel txtBillingAddress;
        private DevExpress.XtraReports.UI.XRLabel txtBillingTel;
        private DevExpress.XtraReports.UI.XRLabel txtDeliveryTel;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceDate;
        private DevExpress.XtraReports.UI.XRLabel txtItemQty;
        private DevExpress.XtraReports.UI.XRLabel txtItemDescription;
        private DevExpress.XtraReports.UI.XRLabel txtItemAmount;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceAmount;
        private DevExpress.XtraReports.UI.XRLabel txtTimeStamp;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRLabel lblShipTo;
        private DevExpress.XtraReports.UI.XRLabel lblBillTo;
        private DevExpress.XtraReports.UI.XRLabel lblTransactionDate;
        private DevExpress.XtraReports.UI.XRLabel lblTransactionNumber;
        private DevExpress.XtraReports.UI.XRLabel lblDeliveryNote;
        private DevExpress.XtraReports.UI.XRLabel txtCompanyAddress;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel lblAmount;
        private DevExpress.XtraReports.UI.XRLabel lblDescription;
        private DevExpress.XtraReports.UI.XRLabel lblQty;
    }
}
