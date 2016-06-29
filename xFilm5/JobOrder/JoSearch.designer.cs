namespace xFilm5.JobOrder
{
    partial class JoSearch
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle1 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle2 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle3 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle4 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle5 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.colOrderId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPriority = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.cboClient = new Gizmox.WebGUI.Forms.ComboBox();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.splitContainer = new Gizmox.WebGUI.Forms.SplitContainer();
            this.ansJoDefault = new Gizmox.WebGUI.Forms.ToolBar();
            this.lksPane = new Gizmox.WebGUI.Forms.Panel();
            this.cmdLookup = new Gizmox.WebGUI.Forms.Button();
            this.lblClient = new Gizmox.WebGUI.Forms.Label();
            this.txtLookup = new Gizmox.WebGUI.Forms.TextBox();
            this.lblLookup = new Gizmox.WebGUI.Forms.Label();
            this.lvwJoDefault = new Gizmox.WebGUI.Forms.ListView();
            this.colStatus = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colDelivery = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colComment = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colClientName = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colAttachment = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colRemarks = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colReceivedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colCompletedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colOrderType = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colOrderedBy = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPrepressOp = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colRetouchBy = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colWorkshop = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceNumber = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceDate = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPaid = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPaymentType = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPaidOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPaidAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colCreatedBy = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colCreatedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colModifiedBy = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colModifiedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // colOrderId
            // 
            this.colOrderId.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colOrderId.Image = null;
            this.colOrderId.Text = "Order ID";
            this.colOrderId.Width = 90;
            // 
            // colPriority
            // 
            this.colPriority.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle1.File = "16x16.flag_grey.png";
            this.colPriority.Image = iconResourceHandle1;
            this.colPriority.Text = "";
            this.colPriority.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colPriority.Width = 24;
            // 
            // cboClient
            // 
            this.cboClient.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cboClient.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboClient.DropDownWidth = 140;
            this.cboClient.Location = new System.Drawing.Point(891, 7);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(140, 21);
            this.cboClient.TabIndex = 1;
            this.cboClient.Visible = false;
            this.cboClient.SelectedIndexChanged += new System.EventHandler(this.cboClient_SelectedIndexChanged);
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 30;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.splitContainer.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Clear;
            this.splitContainer.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = Gizmox.WebGUI.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = Gizmox.WebGUI.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ansJoDefault);
            this.splitContainer.Panel1.Controls.Add(this.lksPane);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lvwJoDefault);
            this.splitContainer.Size = new System.Drawing.Size(1031, 405);
            this.splitContainer.SplitterDistance = 60;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // ansJoDefault
            // 
            this.ansJoDefault.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansJoDefault.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansJoDefault.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansJoDefault.DragHandle = true;
            this.ansJoDefault.DropDownArrows = false;
            this.ansJoDefault.ImageList = null;
            this.ansJoDefault.Location = new System.Drawing.Point(0, 34);
            this.ansJoDefault.MenuHandle = true;
            this.ansJoDefault.Name = "ansJoDefault";
            this.ansJoDefault.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansJoDefault.ShowToolTips = true;
            this.ansJoDefault.TabIndex = 1;
            // 
            // lksPane
            // 
            this.lksPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lksPane.AutoSizeMode = Gizmox.WebGUI.Forms.AutoSizeMode.GrowAndShrink;
            this.lksPane.Controls.Add(this.cmdLookup);
            this.lksPane.Controls.Add(this.lblClient);
            this.lksPane.Controls.Add(this.txtLookup);
            this.lksPane.Controls.Add(this.cboClient);
            this.lksPane.Controls.Add(this.lblLookup);
            this.lksPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.lksPane.Location = new System.Drawing.Point(0, 0);
            this.lksPane.Name = "lksPane";
            this.lksPane.Size = new System.Drawing.Size(1031, 34);
            this.lksPane.TabIndex = 0;
            // 
            // cmdLookup
            // 
            iconResourceHandle2.File = "16x16.find.png";
            this.cmdLookup.Image = iconResourceHandle2;
            this.cmdLookup.Location = new System.Drawing.Point(183, 6);
            this.cmdLookup.Name = "cmdLookup";
            this.cmdLookup.Size = new System.Drawing.Size(22, 22);
            this.cmdLookup.TabIndex = 4;
            this.cmdLookup.TabStop = false;
            this.cmdLookup.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdLookup.Click += new System.EventHandler(this.cmdLookup_Click);
            // 
            // lblClient
            // 
            this.lblClient.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lblClient.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblClient.Location = new System.Drawing.Point(847, 10);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(44, 17);
            this.lblClient.TabIndex = 3;
            this.lblClient.Text = "Client:";
            this.lblClient.Visible = false;
            // 
            // txtLookup
            // 
            this.txtLookup.Location = new System.Drawing.Point(63, 7);
            this.txtLookup.Name = "txtLookup";
            this.txtLookup.Size = new System.Drawing.Size(120, 20);
            this.txtLookup.TabIndex = 2;
            this.txtLookup.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtLookup_EnterKeyDown);
            // 
            // lblLookup
            // 
            this.lblLookup.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblLookup.Location = new System.Drawing.Point(2, 9);
            this.lblLookup.Name = "lblLookup";
            this.lblLookup.Size = new System.Drawing.Size(61, 18);
            this.lblLookup.TabIndex = 0;
            this.lblLookup.Text = "Search:";
            // 
            // lvwJoDefault
            // 
            this.lvwJoDefault.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwJoDefault.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colOrderId,
            this.colPriority,
            this.colStatus,
            this.colDelivery,
            this.colComment,
            this.colLN,
            this.colClientName,
            this.colAttachment,
            this.colRemarks,
            this.colReceivedOn,
            this.colCompletedOn,
            this.colOrderType,
            this.colOrderedBy,
            this.colPrepressOp,
            this.colRetouchBy,
            this.colWorkshop,
            this.colInvoiceNumber,
            this.colInvoiceDate,
            this.colInvoiceAmount,
            this.colPaid,
            this.colPaymentType,
            this.colPaidOn,
            this.colPaidAmount,
            this.colCreatedBy,
            this.colCreatedOn,
            this.colModifiedBy,
            this.colModifiedOn});
            this.lvwJoDefault.DataMember = null;
            this.lvwJoDefault.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwJoDefault.GridLines = true;
            this.lvwJoDefault.ItemsPerPage = 500;
            this.lvwJoDefault.Location = new System.Drawing.Point(0, 0);
            this.lvwJoDefault.MultiSelect = false;
            this.lvwJoDefault.Name = "lvwJoDefault";
            this.lvwJoDefault.Size = new System.Drawing.Size(1031, 344);
            this.lvwJoDefault.TabIndex = 0;
            this.lvwJoDefault.UseInternalPaging = true;
            this.lvwJoDefault.SelectedIndexChanged += new System.EventHandler(this.lvwJoDefault_SelectedIndexChanged);
            this.lvwJoDefault.DoubleClick += new System.EventHandler(this.lvwJoDefault_DoubleClick_1);
            // 
            // colStatus
            // 
            this.colStatus.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle3.File = "16x16.folder_grey.png";
            this.colStatus.Image = iconResourceHandle3;
            this.colStatus.Text = "";
            this.colStatus.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colStatus.Width = 24;
            // 
            // colDelivery
            // 
            this.colDelivery.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle4.File = "16x16.jobOrder_delivery.png";
            this.colDelivery.Image = iconResourceHandle4;
            this.colDelivery.Text = "";
            this.colDelivery.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colDelivery.Width = 24;
            // 
            // colComment
            // 
            this.colComment.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle5.File = "16x16.jobOrder_commentgrey.png";
            this.colComment.Image = iconResourceHandle5;
            this.colComment.Text = "";
            this.colComment.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colComment.Width = 24;
            // 
            // colClientName
            // 
            this.colClientName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colClientName.Image = null;
            this.colClientName.Text = "Client Name";
            this.colClientName.Width = 120;
            // 
            // colAttachment
            // 
            this.colAttachment.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colAttachment.Image = null;
            this.colAttachment.Text = "Attachment";
            this.colAttachment.Width = 150;
            // 
            // colRemarks
            // 
            this.colRemarks.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colRemarks.Image = null;
            this.colRemarks.Text = "Remarks";
            this.colRemarks.Visible = false;
            this.colRemarks.Width = 150;
            // 
            // colReceivedOn
            // 
            this.colReceivedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colReceivedOn.Image = null;
            this.colReceivedOn.Text = "Received On";
            this.colReceivedOn.Width = 100;
            // 
            // colCompletedOn
            // 
            this.colCompletedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colCompletedOn.Image = null;
            this.colCompletedOn.Text = "Completed On";
            this.colCompletedOn.Width = 100;
            // 
            // colOrderType
            // 
            this.colOrderType.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colOrderType.Image = null;
            this.colOrderType.Text = "Order Type";
            this.colOrderType.Visible = false;
            this.colOrderType.Width = 80;
            // 
            // colOrderedBy
            // 
            this.colOrderedBy.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colOrderedBy.Image = null;
            this.colOrderedBy.Text = "Ordered By";
            this.colOrderedBy.Visible = false;
            this.colOrderedBy.Width = 70;
            // 
            // colPrepressOp
            // 
            this.colPrepressOp.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPrepressOp.Image = null;
            this.colPrepressOp.Text = "Prepress Op";
            this.colPrepressOp.Visible = false;
            this.colPrepressOp.Width = 70;
            // 
            // colRetouchBy
            // 
            this.colRetouchBy.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colRetouchBy.Image = null;
            this.colRetouchBy.Text = "Retouch By";
            this.colRetouchBy.Visible = false;
            this.colRetouchBy.Width = 70;
            // 
            // colWorkshop
            // 
            this.colWorkshop.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colWorkshop.Image = null;
            this.colWorkshop.Text = "Workshop";
            this.colWorkshop.Width = 70;
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceNumber.Image = null;
            this.colInvoiceNumber.Text = "Invoice No.";
            this.colInvoiceNumber.Width = 60;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceDate.Image = null;
            this.colInvoiceDate.Text = "Invoice Date";
            this.colInvoiceDate.Width = 100;
            // 
            // colInvoiceAmount
            // 
            this.colInvoiceAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceAmount.Image = null;
            this.colInvoiceAmount.Text = "Invoice Amt.";
            this.colInvoiceAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colInvoiceAmount.Width = 70;
            // 
            // colPaid
            // 
            this.colPaid.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPaid.Image = null;
            this.colPaid.Text = "Paid";
            this.colPaid.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colPaid.Width = 30;
            // 
            // colPaymentType
            // 
            this.colPaymentType.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPaymentType.Image = null;
            this.colPaymentType.Text = "Payment Type";
            this.colPaymentType.Visible = false;
            this.colPaymentType.Width = 100;
            // 
            // colPaidOn
            // 
            this.colPaidOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPaidOn.Image = null;
            this.colPaidOn.Text = "Paid On";
            this.colPaidOn.Visible = false;
            this.colPaidOn.Width = 100;
            // 
            // colPaidAmount
            // 
            this.colPaidAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPaidAmount.Image = null;
            this.colPaidAmount.Text = "Paid Amt.";
            this.colPaidAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colPaidAmount.Visible = false;
            this.colPaidAmount.Width = 70;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colCreatedBy.Image = null;
            this.colCreatedBy.Text = "Created By";
            this.colCreatedBy.Visible = false;
            this.colCreatedBy.Width = 80;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colCreatedOn.Image = null;
            this.colCreatedOn.Text = "Created On";
            this.colCreatedOn.Visible = false;
            this.colCreatedOn.Width = 100;
            // 
            // colModifiedBy
            // 
            this.colModifiedBy.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colModifiedBy.Image = null;
            this.colModifiedBy.Text = "Modified By";
            this.colModifiedBy.Visible = false;
            this.colModifiedBy.Width = 80;
            // 
            // colModifiedOn
            // 
            this.colModifiedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colModifiedOn.Image = null;
            this.colModifiedOn.Text = "Modified On";
            this.colModifiedOn.Visible = false;
            this.colModifiedOn.Width = 100;
            // 
            // JoSearch
            // 
            this.Controls.Add(this.splitContainer);
            this.Size = new System.Drawing.Size(1031, 405);
            this.Text = "Job Order Default Workspace";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ColumnHeader colOrderId;
        private Gizmox.WebGUI.Forms.ColumnHeader colPriority;
        private Gizmox.WebGUI.Forms.ComboBox cboClient;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.SplitContainer splitContainer;
        private Gizmox.WebGUI.Forms.ToolBar ansJoDefault;
        private Gizmox.WebGUI.Forms.Panel lksPane;
        private Gizmox.WebGUI.Forms.Button cmdLookup;
        private Gizmox.WebGUI.Forms.Label lblClient;
        private Gizmox.WebGUI.Forms.TextBox txtLookup;
        private Gizmox.WebGUI.Forms.Label lblLookup;
        private Gizmox.WebGUI.Forms.ListView lvwJoDefault;
        private Gizmox.WebGUI.Forms.ColumnHeader colClientName;
        private Gizmox.WebGUI.Forms.ColumnHeader colAttachment;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderType;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colPrepressOp;
        private Gizmox.WebGUI.Forms.ColumnHeader colRetouchBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colRemarks;
        private Gizmox.WebGUI.Forms.ColumnHeader colReceivedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colCompletedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colWorkshop;
        private Gizmox.WebGUI.Forms.ColumnHeader colStatus;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.ColumnHeader colDelivery;
        private Gizmox.WebGUI.Forms.ColumnHeader colComment;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceNumber;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceDate;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaymentType;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaidOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaidAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colModifiedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaid;

    }
}