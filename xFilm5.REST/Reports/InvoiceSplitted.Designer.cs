namespace xFilm5.REST.Reports
{
    partial class InvoiceSplitted
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.txtRemarks = new DevExpress.XtraReports.UI.XRLabel();
            this.txtAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.txtOrderNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDNNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDate = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lblCompanyAddress = new DevExpress.XtraReports.UI.XRRichText();
            this.lblCompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lblPoweredBy = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTimeStamp = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txtTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.gh1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.txtClientAddress = new DevExpress.XtraReports.UI.XRRichText();
            this.txtFax = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtClientName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoice = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPageNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtInvoiceDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.txtInvoiceAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDNNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOrderNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblRemarks = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompanyAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtRemarks,
            this.txtAmount,
            this.txtOrderNumber,
            this.txtDNNumber,
            this.txtDate});
            this.Detail.HeightF = 24F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // txtRemarks
            // 
            this.txtRemarks.CanGrow = false;
            this.txtRemarks.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.LocationFloat = new DevExpress.Utils.PointFloat(300F, 0F);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtRemarks.SizeF = new System.Drawing.SizeF(350F, 20F);
            this.txtRemarks.Text = "txtRemarks";
            this.txtRemarks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtAmount
            // 
            this.txtAmount.CanGrow = false;
            this.txtAmount.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.LocationFloat = new DevExpress.Utils.PointFloat(650F, 0F);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtAmount.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtAmount.Text = "txtAmount";
            this.txtAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.CanGrow = false;
            this.txtOrderNumber.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNumber.LocationFloat = new DevExpress.Utils.PointFloat(200F, 0F);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtOrderNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtOrderNumber.Text = "txtOrderNumber";
            this.txtOrderNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtDNNumber
            // 
            this.txtDNNumber.CanGrow = false;
            this.txtDNNumber.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNNumber.LocationFloat = new DevExpress.Utils.PointFloat(100F, 0F);
            this.txtDNNumber.Name = "txtDNNumber";
            this.txtDNNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDNNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtDNNumber.Text = "txtDNNumber";
            this.txtDNNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtDate
            // 
            this.txtDate.CanGrow = false;
            this.txtDate.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.txtDate.Name = "txtDate";
            this.txtDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDate.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtDate.Text = "txtDate";
            this.txtDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCompanyAddress,
            this.lblCompanyName});
            this.PageHeader.HeightF = 82.33334F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCompanyAddress
            // 
            this.lblCompanyAddress.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyAddress.LocationFloat = new DevExpress.Utils.PointFloat(500F, 0F);
            this.lblCompanyAddress.Name = "lblCompanyAddress";
            this.lblCompanyAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblCompanyAddress.SizeF = new System.Drawing.SizeF(249F, 75F);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCompanyName.SizeF = new System.Drawing.SizeF(300F, 75F);
            this.lblCompanyName.Text = "NuStar Production Company";
            this.lblCompanyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPoweredBy,
            this.txtTimeStamp});
            this.PageFooter.HeightF = 41F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblPoweredBy
            // 
            this.lblPoweredBy.Font = new System.Drawing.Font("Arial Narrow", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoweredBy.LocationFloat = new DevExpress.Utils.PointFloat(629F, 25F);
            this.lblPoweredBy.Name = "lblPoweredBy";
            this.lblPoweredBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPoweredBy.SizeF = new System.Drawing.SizeF(120F, 16F);
            this.lblPoweredBy.Text = "by DirectOutput.com.hk";
            this.lblPoweredBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // txtTimeStamp
            // 
            this.txtTimeStamp.Font = new System.Drawing.Font("Arial Narrow", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeStamp.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.txtTimeStamp.Name = "txtTimeStamp";
            this.txtTimeStamp.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTimeStamp.SizeF = new System.Drawing.SizeF(120F, 16F);
            this.txtTimeStamp.Text = "txtTimeStamp";
            this.txtTimeStamp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtTotal,
            this.lblTotal});
            this.GroupFooter1.HeightF = 67F;
            this.GroupFooter1.KeepTogether = true;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.GroupFooter1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // txtTotal
            // 
            this.txtTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtTotal.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.LocationFloat = new DevExpress.Utils.PointFloat(650F, 32F);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTotal.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtTotal.Text = "txtTotal";
            this.txtTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Gray;
            this.lblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.LocationFloat = new DevExpress.Utils.PointFloat(650F, 12F);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTotal.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblTotal.Text = "Total Due";
            this.lblTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 36F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 36F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // gh1
            // 
            this.gh1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDate,
            this.lblDNNumber,
            this.lblOrderNumber,
            this.lblAmount,
            this.lblRemarks,
            this.xrPanel1,
            this.lblInvoice,
            this.lblInvoiceNumber,
            this.lblInvoiceDate,
            this.lblPageNumber,
            this.txtInvoiceNumber,
            this.txtInvoiceDate,
            this.xrPageInfo1,
            this.txtInvoiceAmount,
            this.lblInvoiceAmount});
            this.gh1.HeightF = 201.4584F;
            this.gh1.Name = "gh1";
            this.gh1.RepeatEveryPage = true;
            this.gh1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.gh1_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtClientAddress,
            this.txtFax,
            this.txtTel,
            this.txtClientName});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.541671F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(300F, 117F);
            // 
            // txtClientAddress
            // 
            this.txtClientAddress.BorderWidth = 0F;
            this.txtClientAddress.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientAddress.LocationFloat = new DevExpress.Utils.PointFloat(4F, 25F);
            this.txtClientAddress.Name = "txtClientAddress";
            this.txtClientAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.txtClientAddress.SizeF = new System.Drawing.SizeF(292F, 67F);
            // 
            // txtFax
            // 
            this.txtFax.BorderWidth = 0F;
            this.txtFax.CanGrow = false;
            this.txtFax.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.LocationFloat = new DevExpress.Utils.PointFloat(150F, 93F);
            this.txtFax.Name = "txtFax";
            this.txtFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtFax.SizeF = new System.Drawing.SizeF(146F, 20F);
            this.txtFax.Text = "txtFax";
            this.txtFax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtTel
            // 
            this.txtTel.BorderWidth = 0F;
            this.txtTel.CanGrow = false;
            this.txtTel.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTel.LocationFloat = new DevExpress.Utils.PointFloat(4F, 93F);
            this.txtTel.Name = "txtTel";
            this.txtTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTel.SizeF = new System.Drawing.SizeF(146F, 20F);
            this.txtTel.Text = "txtTel";
            this.txtTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtClientName
            // 
            this.txtClientName.BorderWidth = 0F;
            this.txtClientName.CanGrow = false;
            this.txtClientName.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.LocationFloat = new DevExpress.Utils.PointFloat(4F, 4F);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtClientName.SizeF = new System.Drawing.SizeF(292F, 20F);
            this.txtClientName.Text = "txtClientName";
            this.txtClientName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblInvoice
            // 
            this.lblInvoice.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoice.LocationFloat = new DevExpress.Utils.PointFloat(0F, 138.5417F);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInvoice.SizeF = new System.Drawing.SizeF(750F, 29F);
            this.lblInvoice.Text = "INVOICE";
            this.lblInvoice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(499F, 13.54167F);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInvoiceNumber.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblInvoiceNumber.StylePriority.UseTextAlignment = false;
            this.lblInvoiceNumber.Text = "Invoice Number:";
            this.lblInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceDate.LocationFloat = new DevExpress.Utils.PointFloat(499F, 56.41667F);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInvoiceDate.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblInvoiceDate.StylePriority.UseTextAlignment = false;
            this.lblInvoiceDate.Text = "Invoice Date:";
            this.lblInvoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageNumber.LocationFloat = new DevExpress.Utils.PointFloat(499F, 77.54167F);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPageNumber.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblPageNumber.StylePriority.UseTextAlignment = false;
            this.lblPageNumber.Text = "Page Number:";
            this.lblPageNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(625F, 13.54167F);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceNumber.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtInvoiceNumber.Text = "txtInvoiceNumber";
            this.txtInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceDate.LocationFloat = new DevExpress.Utils.PointFloat(625F, 56.41667F);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceDate.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtInvoiceDate.Text = "txtInvoiceDate";
            this.txtInvoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "{0} of {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(625F, 77.54167F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtInvoiceAmount
            // 
            this.txtInvoiceAmount.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceAmount.LocationFloat = new DevExpress.Utils.PointFloat(625F, 34.54167F);
            this.txtInvoiceAmount.Name = "txtInvoiceAmount";
            this.txtInvoiceAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceAmount.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtInvoiceAmount.Text = "txtInvoiceAmount";
            this.txtInvoiceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceAmount.LocationFloat = new DevExpress.Utils.PointFloat(499.0001F, 34.54167F);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInvoiceAmount.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblInvoiceAmount.StylePriority.UseTextAlignment = false;
            this.lblInvoiceAmount.Text = "Invoice Amount:";
            this.lblInvoiceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Gray;
            this.lblDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 177.2917F);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblDate.Text = "Date";
            this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDNNumber
            // 
            this.lblDNNumber.BackColor = System.Drawing.Color.Gray;
            this.lblDNNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDNNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNNumber.ForeColor = System.Drawing.Color.White;
            this.lblDNNumber.LocationFloat = new DevExpress.Utils.PointFloat(100F, 177.2917F);
            this.lblDNNumber.Name = "lblDNNumber";
            this.lblDNNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDNNumber.SizeF = new System.Drawing.SizeF(102.5F, 20F);
            this.lblDNNumber.Text = "DN No.";
            this.lblDNNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.BackColor = System.Drawing.Color.Gray;
            this.lblOrderNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblOrderNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.White;
            this.lblOrderNumber.LocationFloat = new DevExpress.Utils.PointFloat(202.5F, 177.2917F);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOrderNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblOrderNumber.Text = "Order No.";
            this.lblOrderNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.Color.Gray;
            this.lblAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblAmount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.White;
            this.lblAmount.LocationFloat = new DevExpress.Utils.PointFloat(652.5F, 177.2917F);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmount.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblRemarks
            // 
            this.lblRemarks.BackColor = System.Drawing.Color.Gray;
            this.lblRemarks.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblRemarks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.White;
            this.lblRemarks.LocationFloat = new DevExpress.Utils.PointFloat(302.5F, 177.2917F);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblRemarks.SizeF = new System.Drawing.SizeF(350F, 20F);
            this.lblRemarks.Text = "Remarks";
            this.lblRemarks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // InvoiceSplitted
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.GroupFooter1,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.gh1});
            this.Margins = new System.Drawing.Printing.Margins(36, 36, 36, 36);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.InvoiceSplitted_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.lblCompanyAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel lblCompanyName;
        private DevExpress.XtraReports.UI.XRLabel lblTotal;
        private DevExpress.XtraReports.UI.XRLabel txtTotal;
        private DevExpress.XtraReports.UI.XRLabel lblPoweredBy;
        private DevExpress.XtraReports.UI.XRLabel txtTimeStamp;
        private DevExpress.XtraReports.UI.XRLabel txtRemarks;
        private DevExpress.XtraReports.UI.XRLabel txtAmount;
        private DevExpress.XtraReports.UI.XRLabel txtOrderNumber;
        private DevExpress.XtraReports.UI.XRLabel txtDNNumber;
        private DevExpress.XtraReports.UI.XRLabel txtDate;
        private DevExpress.XtraReports.UI.XRRichText lblCompanyAddress;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.GroupHeaderBand gh1;
        private DevExpress.XtraReports.UI.XRLabel lblDate;
        private DevExpress.XtraReports.UI.XRLabel lblDNNumber;
        private DevExpress.XtraReports.UI.XRLabel lblOrderNumber;
        private DevExpress.XtraReports.UI.XRLabel lblAmount;
        private DevExpress.XtraReports.UI.XRLabel lblRemarks;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRRichText txtClientAddress;
        private DevExpress.XtraReports.UI.XRLabel txtFax;
        private DevExpress.XtraReports.UI.XRLabel txtTel;
        private DevExpress.XtraReports.UI.XRLabel txtClientName;
        private DevExpress.XtraReports.UI.XRLabel lblInvoice;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceDate;
        private DevExpress.XtraReports.UI.XRLabel lblPageNumber;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceAmount;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceAmount;
    }
}
