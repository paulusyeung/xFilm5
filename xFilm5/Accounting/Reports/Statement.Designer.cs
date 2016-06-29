namespace xFilm5.Accounting.Reports
{
    partial class Statement
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
            this.txtInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDate = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lblCompanyAddress = new DevExpress.XtraReports.UI.XRRichText();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblRemarks = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOrderNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.txtToday = new DevExpress.XtraReports.UI.XRLabel();
            this.txtClientId = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPageNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblStatementOn = new DevExpress.XtraReports.UI.XRLabel();
            this.lblClientId = new DevExpress.XtraReports.UI.XRLabel();
            this.lblStatement = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.txtClientAddress = new DevExpress.XtraReports.UI.XRRichText();
            this.txtFax = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTel = new DevExpress.XtraReports.UI.XRLabel();
            this.txtClientName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lblPoweredBy = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTimeStamp = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txtTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCurrent = new DevExpress.XtraReports.UI.XRLabel();
            this.txt30Days = new DevExpress.XtraReports.UI.XRLabel();
            this.txt60Days = new DevExpress.XtraReports.UI.XRLabel();
            this.txt90Days = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl90Days = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl60Days = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl30Days = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCurrent = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
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
            this.txtInvoiceNumber,
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
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.CanGrow = false;
            this.txtInvoiceNumber.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(100F, 0F);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtInvoiceNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtInvoiceNumber.Text = "txtInvoiceNumber";
            this.txtInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrPageInfo1,
            this.lblRemarks,
            this.lblAmount,
            this.lblOrderNumber,
            this.lblInvoiceNumber,
            this.lblDate,
            this.txtToday,
            this.txtClientId,
            this.lblPageNumber,
            this.lblStatementOn,
            this.lblClientId,
            this.lblStatement,
            this.xrPanel1,
            this.lblCompanyName});
            this.PageHeader.HeightF = 299F;
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
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "{0} of {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(625F, 150F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblRemarks
            // 
            this.lblRemarks.BackColor = System.Drawing.Color.Gray;
            this.lblRemarks.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblRemarks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.White;
            this.lblRemarks.LocationFloat = new DevExpress.Utils.PointFloat(300F, 275F);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblRemarks.SizeF = new System.Drawing.SizeF(350F, 20F);
            this.lblRemarks.Text = "Remarks";
            this.lblRemarks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.Color.Gray;
            this.lblAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblAmount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.White;
            this.lblAmount.LocationFloat = new DevExpress.Utils.PointFloat(650F, 275F);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmount.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.BackColor = System.Drawing.Color.Gray;
            this.lblOrderNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblOrderNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.White;
            this.lblOrderNumber.LocationFloat = new DevExpress.Utils.PointFloat(200F, 275F);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOrderNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblOrderNumber.Text = "Order No.";
            this.lblOrderNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.BackColor = System.Drawing.Color.Gray;
            this.lblInvoiceNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNumber.ForeColor = System.Drawing.Color.White;
            this.lblInvoiceNumber.LocationFloat = new DevExpress.Utils.PointFloat(100F, 275F);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInvoiceNumber.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblInvoiceNumber.Text = "Invoice No.";
            this.lblInvoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Gray;
            this.lblDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 275F);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblDate.Text = "Date";
            this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtToday
            // 
            this.txtToday.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToday.LocationFloat = new DevExpress.Utils.PointFloat(625F, 129F);
            this.txtToday.Name = "txtToday";
            this.txtToday.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtToday.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtToday.Text = "txtToday";
            this.txtToday.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtClientId
            // 
            this.txtClientId.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientId.LocationFloat = new DevExpress.Utils.PointFloat(625F, 108F);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtClientId.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.txtClientId.Text = "txtClientId";
            this.txtClientId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageNumber.LocationFloat = new DevExpress.Utils.PointFloat(500F, 150F);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPageNumber.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblPageNumber.Text = "Page Number:";
            this.lblPageNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblStatementOn
            // 
            this.lblStatementOn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatementOn.LocationFloat = new DevExpress.Utils.PointFloat(500F, 129F);
            this.lblStatementOn.Name = "lblStatementOn";
            this.lblStatementOn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblStatementOn.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblStatementOn.Text = "Statement As On:";
            this.lblStatementOn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblClientId
            // 
            this.lblClientId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientId.LocationFloat = new DevExpress.Utils.PointFloat(500F, 108F);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblClientId.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.lblClientId.Text = "Client ID:";
            this.lblClientId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblStatement
            // 
            this.lblStatement.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatement.LocationFloat = new DevExpress.Utils.PointFloat(0F, 233F);
            this.lblStatement.Name = "lblStatement";
            this.lblStatement.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblStatement.SizeF = new System.Drawing.SizeF(750F, 29F);
            this.lblStatement.Text = "Statement";
            this.lblStatement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 104F);
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
            this.txtCurrent,
            this.txt30Days,
            this.txt60Days,
            this.txt90Days,
            this.lbl90Days,
            this.lbl60Days,
            this.lbl30Days,
            this.lblCurrent,
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
            // txtCurrent
            // 
            this.txtCurrent.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtCurrent.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrent.LocationFloat = new DevExpress.Utils.PointFloat(550F, 32F);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtCurrent.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txtCurrent.Text = "txtCurrent";
            this.txtCurrent.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txt30Days
            // 
            this.txt30Days.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txt30Days.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt30Days.LocationFloat = new DevExpress.Utils.PointFloat(450F, 32F);
            this.txt30Days.Name = "txt30Days";
            this.txt30Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt30Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txt30Days.Text = "txt30Days";
            this.txt30Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txt60Days
            // 
            this.txt60Days.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txt60Days.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt60Days.LocationFloat = new DevExpress.Utils.PointFloat(350F, 32F);
            this.txt60Days.Name = "txt60Days";
            this.txt60Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt60Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txt60Days.Text = "txt60Days";
            this.txt60Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txt90Days
            // 
            this.txt90Days.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txt90Days.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt90Days.LocationFloat = new DevExpress.Utils.PointFloat(250F, 32F);
            this.txt90Days.Name = "txt90Days";
            this.txt90Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt90Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.txt90Days.Text = "txt90Days";
            this.txt90Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbl90Days
            // 
            this.lbl90Days.BackColor = System.Drawing.Color.Gray;
            this.lbl90Days.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl90Days.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl90Days.ForeColor = System.Drawing.Color.White;
            this.lbl90Days.LocationFloat = new DevExpress.Utils.PointFloat(250F, 12F);
            this.lbl90Days.Name = "lbl90Days";
            this.lbl90Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl90Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lbl90Days.Text = "Over 2 Months";
            this.lbl90Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl60Days
            // 
            this.lbl60Days.BackColor = System.Drawing.Color.Gray;
            this.lbl60Days.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl60Days.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl60Days.ForeColor = System.Drawing.Color.White;
            this.lbl60Days.LocationFloat = new DevExpress.Utils.PointFloat(350F, 12F);
            this.lbl60Days.Name = "lbl60Days";
            this.lbl60Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl60Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lbl60Days.Text = "2 Months";
            this.lbl60Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl30Days
            // 
            this.lbl30Days.BackColor = System.Drawing.Color.Gray;
            this.lbl30Days.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl30Days.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl30Days.ForeColor = System.Drawing.Color.White;
            this.lbl30Days.LocationFloat = new DevExpress.Utils.PointFloat(450F, 12F);
            this.lbl30Days.Name = "lbl30Days";
            this.lbl30Days.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl30Days.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lbl30Days.Text = "Last Month";
            this.lbl30Days.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCurrent
            // 
            this.lblCurrent.BackColor = System.Drawing.Color.Gray;
            this.lblCurrent.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblCurrent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.White;
            this.lblCurrent.LocationFloat = new DevExpress.Utils.PointFloat(550F, 12F);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCurrent.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblCurrent.Text = "Current Month";
            this.lblCurrent.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // Statement
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.GroupFooter1,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new System.Drawing.Printing.Margins(36, 36, 36, 36);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Statement_BeforePrint);
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
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRLabel txtClientName;
        private DevExpress.XtraReports.UI.XRLabel txtFax;
        private DevExpress.XtraReports.UI.XRLabel txtTel;
        private DevExpress.XtraReports.UI.XRLabel lblStatement;
        private DevExpress.XtraReports.UI.XRLabel txtClientId;
        private DevExpress.XtraReports.UI.XRLabel lblPageNumber;
        private DevExpress.XtraReports.UI.XRLabel lblStatementOn;
        private DevExpress.XtraReports.UI.XRLabel lblClientId;
        private DevExpress.XtraReports.UI.XRLabel txtToday;
        private DevExpress.XtraReports.UI.XRLabel lblRemarks;
        private DevExpress.XtraReports.UI.XRLabel lblAmount;
        private DevExpress.XtraReports.UI.XRLabel lblOrderNumber;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel lblDate;
        private DevExpress.XtraReports.UI.XRLabel lbl30Days;
        private DevExpress.XtraReports.UI.XRLabel lblCurrent;
        private DevExpress.XtraReports.UI.XRLabel lblTotal;
        private DevExpress.XtraReports.UI.XRLabel txtCurrent;
        private DevExpress.XtraReports.UI.XRLabel txt30Days;
        private DevExpress.XtraReports.UI.XRLabel txt60Days;
        private DevExpress.XtraReports.UI.XRLabel txt90Days;
        private DevExpress.XtraReports.UI.XRLabel lbl90Days;
        private DevExpress.XtraReports.UI.XRLabel lbl60Days;
        private DevExpress.XtraReports.UI.XRLabel txtTotal;
        private DevExpress.XtraReports.UI.XRLabel lblPoweredBy;
        private DevExpress.XtraReports.UI.XRLabel txtTimeStamp;
        private DevExpress.XtraReports.UI.XRLabel txtRemarks;
        private DevExpress.XtraReports.UI.XRLabel txtAmount;
        private DevExpress.XtraReports.UI.XRLabel txtOrderNumber;
        private DevExpress.XtraReports.UI.XRLabel txtInvoiceNumber;
        private DevExpress.XtraReports.UI.XRLabel txtDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRRichText lblCompanyAddress;
        private DevExpress.XtraReports.UI.XRRichText txtClientAddress;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
