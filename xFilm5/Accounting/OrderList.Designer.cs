namespace xFilm5.Accounting
{
    partial class OrderList
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle6 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.navPane = new Gizmox.WebGUI.Forms.Panel();
            this.idxSelect = new Gizmox.WebGUI.Forms.TabControl();
            this.tabClient = new Gizmox.WebGUI.Forms.TabPage();
            this.tvwClient = new Gizmox.WebGUI.Forms.TreeView();
            this.tabAdvanced = new Gizmox.WebGUI.Forms.TabPage();
            this.gbxOrderedOn = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdFindOrderedOn = new Gizmox.WebGUI.Forms.Button();
            this.dtpOrderedOnTo = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.lblOrderedOnTo = new Gizmox.WebGUI.Forms.Label();
            this.dtpOrderedOnFrom = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.lblOrderedOnFrom = new Gizmox.WebGUI.Forms.Label();
            this.gbxOrderId = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdFindOrderId = new Gizmox.WebGUI.Forms.Button();
            this.txtOrderIdTo = new Gizmox.WebGUI.Forms.TextBox();
            this.lblOrderIdTo = new Gizmox.WebGUI.Forms.Label();
            this.txtOrderIdFrom = new Gizmox.WebGUI.Forms.TextBox();
            this.lblOrderIdFrom = new Gizmox.WebGUI.Forms.Label();
            this.ansClientTree = new Gizmox.WebGUI.Forms.ToolBar();
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.lvwOrderList = new Gizmox.WebGUI.Forms.ListView();
            this.colOrderId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colPriority = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colStatus = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colDelivery = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colComment = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
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
            this.ansOrderList = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // navPane
            // 
            this.navPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navPane.Controls.Add(this.idxSelect);
            this.navPane.Controls.Add(this.ansClientTree);
            this.navPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.navPane.Location = new System.Drawing.Point(0, 0);
            this.navPane.Name = "navPane";
            this.navPane.Size = new System.Drawing.Size(200, 306);
            this.navPane.TabIndex = 0;
            // 
            // idxSelect
            // 
            this.idxSelect.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.idxSelect.Controls.Add(this.tabClient);
            this.idxSelect.Controls.Add(this.tabAdvanced);
            this.idxSelect.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.idxSelect.Location = new System.Drawing.Point(0, 28);
            this.idxSelect.Multiline = false;
            this.idxSelect.Name = "idxSelect";
            this.idxSelect.SelectedIndex = 0;
            this.idxSelect.Size = new System.Drawing.Size(200, 278);
            this.idxSelect.TabIndex = 2;
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.tvwClient);
            this.tabClient.Location = new System.Drawing.Point(4, 22);
            this.tabClient.Name = "tabClient";
            this.tabClient.Size = new System.Drawing.Size(192, 252);
            this.tabClient.TabIndex = 0;
            this.tabClient.Text = "Client";
            // 
            // tvwClient
            // 
            this.tvwClient.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.tvwClient.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tvwClient.Location = new System.Drawing.Point(0, 0);
            this.tvwClient.Name = "tvwClient";
            this.tvwClient.Size = new System.Drawing.Size(192, 252);
            this.tvwClient.TabIndex = 0;
            this.tvwClient.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.tvwClient_AfterSelect);
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.gbxOrderedOn);
            this.tabAdvanced.Controls.Add(this.gbxOrderId);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(192, 252);
            this.tabAdvanced.TabIndex = 0;
            this.tabAdvanced.Text = "Advanced";
            // 
            // gbxOrderedOn
            // 
            this.gbxOrderedOn.Controls.Add(this.cmdFindOrderedOn);
            this.gbxOrderedOn.Controls.Add(this.dtpOrderedOnTo);
            this.gbxOrderedOn.Controls.Add(this.lblOrderedOnTo);
            this.gbxOrderedOn.Controls.Add(this.dtpOrderedOnFrom);
            this.gbxOrderedOn.Controls.Add(this.lblOrderedOnFrom);
            this.gbxOrderedOn.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxOrderedOn.Location = new System.Drawing.Point(7, 88);
            this.gbxOrderedOn.Name = "gbxOrderedOn";
            this.gbxOrderedOn.Size = new System.Drawing.Size(179, 74);
            this.gbxOrderedOn.TabIndex = 1;
            this.gbxOrderedOn.Text = "Ordered On";
            // 
            // cmdFindOrderedOn
            // 
            iconResourceHandle1.File = "16x16.btn_on_lookup.gif";
            this.cmdFindOrderedOn.Image = iconResourceHandle1;
            this.cmdFindOrderedOn.Location = new System.Drawing.Point(144, 44);
            this.cmdFindOrderedOn.Name = "cmdFindOrderedOn";
            this.cmdFindOrderedOn.Size = new System.Drawing.Size(20, 20);
            this.cmdFindOrderedOn.TabIndex = 4;
            this.cmdFindOrderedOn.Click += new System.EventHandler(this.cmdFindOrderedOn_Click);
            // 
            // dtpOrderedOnTo
            // 
            this.dtpOrderedOnTo.CalendarFirstDayOfWeek = Gizmox.WebGUI.Forms.Day.Default;
            this.dtpOrderedOnTo.CustomFormat = "yyyy-MM-dd";
            this.dtpOrderedOnTo.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderedOnTo.Location = new System.Drawing.Point(44, 44);
            this.dtpOrderedOnTo.Name = "dtpOrderedOnTo";
            this.dtpOrderedOnTo.Size = new System.Drawing.Size(100, 20);
            this.dtpOrderedOnTo.TabIndex = 3;
            // 
            // lblOrderedOnTo
            // 
            this.lblOrderedOnTo.Location = new System.Drawing.Point(7, 44);
            this.lblOrderedOnTo.Name = "lblOrderedOnTo";
            this.lblOrderedOnTo.Size = new System.Drawing.Size(37, 20);
            this.lblOrderedOnTo.TabIndex = 2;
            this.lblOrderedOnTo.Text = "To:";
            this.lblOrderedOnTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpOrderedOnFrom
            // 
            this.dtpOrderedOnFrom.CalendarFirstDayOfWeek = Gizmox.WebGUI.Forms.Day.Default;
            this.dtpOrderedOnFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpOrderedOnFrom.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderedOnFrom.Location = new System.Drawing.Point(44, 20);
            this.dtpOrderedOnFrom.Name = "dtpOrderedOnFrom";
            this.dtpOrderedOnFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpOrderedOnFrom.TabIndex = 1;
            // 
            // lblOrderedOnFrom
            // 
            this.lblOrderedOnFrom.Location = new System.Drawing.Point(7, 20);
            this.lblOrderedOnFrom.Name = "lblOrderedOnFrom";
            this.lblOrderedOnFrom.Size = new System.Drawing.Size(37, 20);
            this.lblOrderedOnFrom.TabIndex = 0;
            this.lblOrderedOnFrom.Text = "From:";
            this.lblOrderedOnFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbxOrderId
            // 
            this.gbxOrderId.Controls.Add(this.cmdFindOrderId);
            this.gbxOrderId.Controls.Add(this.txtOrderIdTo);
            this.gbxOrderId.Controls.Add(this.lblOrderIdTo);
            this.gbxOrderId.Controls.Add(this.txtOrderIdFrom);
            this.gbxOrderId.Controls.Add(this.lblOrderIdFrom);
            this.gbxOrderId.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxOrderId.Location = new System.Drawing.Point(7, 7);
            this.gbxOrderId.Name = "gbxOrderId";
            this.gbxOrderId.Size = new System.Drawing.Size(179, 74);
            this.gbxOrderId.TabIndex = 0;
            this.gbxOrderId.Text = "Order ID";
            // 
            // cmdFindOrderId
            // 
            iconResourceHandle2.File = "16x16.btn_on_lookup.gif";
            this.cmdFindOrderId.Image = iconResourceHandle2;
            this.cmdFindOrderId.Location = new System.Drawing.Point(144, 42);
            this.cmdFindOrderId.Name = "cmdFindOrderId";
            this.cmdFindOrderId.Size = new System.Drawing.Size(20, 20);
            this.cmdFindOrderId.TabIndex = 4;
            this.cmdFindOrderId.Click += new System.EventHandler(this.cmdFindOrderId_Click);
            // 
            // txtOrderIdTo
            // 
            this.txtOrderIdTo.Location = new System.Drawing.Point(44, 42);
            this.txtOrderIdTo.Name = "txtOrderIdTo";
            this.txtOrderIdTo.Size = new System.Drawing.Size(100, 20);
            this.txtOrderIdTo.TabIndex = 3;
            // 
            // lblOrderIdTo
            // 
            this.lblOrderIdTo.Location = new System.Drawing.Point(7, 42);
            this.lblOrderIdTo.Name = "lblOrderIdTo";
            this.lblOrderIdTo.Size = new System.Drawing.Size(37, 20);
            this.lblOrderIdTo.TabIndex = 2;
            this.lblOrderIdTo.Text = "To:";
            this.lblOrderIdTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOrderIdFrom
            // 
            this.txtOrderIdFrom.Location = new System.Drawing.Point(44, 19);
            this.txtOrderIdFrom.Name = "txtOrderIdFrom";
            this.txtOrderIdFrom.Size = new System.Drawing.Size(100, 20);
            this.txtOrderIdFrom.TabIndex = 1;
            // 
            // lblOrderIdFrom
            // 
            this.lblOrderIdFrom.Location = new System.Drawing.Point(7, 20);
            this.lblOrderIdFrom.Name = "lblOrderIdFrom";
            this.lblOrderIdFrom.Size = new System.Drawing.Size(37, 20);
            this.lblOrderIdFrom.TabIndex = 0;
            this.lblOrderIdFrom.Text = "From:";
            this.lblOrderIdFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ansClientTree
            // 
            this.ansClientTree.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansClientTree.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansClientTree.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansClientTree.DragHandle = true;
            this.ansClientTree.DropDownArrows = false;
            this.ansClientTree.ImageList = null;
            this.ansClientTree.Location = new System.Drawing.Point(0, 0);
            this.ansClientTree.MenuHandle = true;
            this.ansClientTree.Name = "ansClientTree";
            this.ansClientTree.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansClientTree.ShowToolTips = true;
            this.ansClientTree.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.splitter1.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 306);
            this.splitter1.TabIndex = 1;
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Controls.Add(this.lvwOrderList);
            this.wspPane.Controls.Add(this.ansOrderList);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(201, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(540, 306);
            this.wspPane.TabIndex = 2;
            // 
            // lvwOrderList
            // 
            this.lvwOrderList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwOrderList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
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
            this.lvwOrderList.DataMember = null;
            this.lvwOrderList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwOrderList.GridLines = true;
            this.lvwOrderList.ItemsPerPage = 500;
            this.lvwOrderList.Location = new System.Drawing.Point(0, 28);
            this.lvwOrderList.MultiSelect = false;
            this.lvwOrderList.Name = "lvwOrderList";
            this.lvwOrderList.Size = new System.Drawing.Size(540, 278);
            this.lvwOrderList.TabIndex = 0;
            this.lvwOrderList.UseInternalPaging = true;
            this.lvwOrderList.SelectedIndexChanged += new System.EventHandler(this.lvwOrderList_SelectedIndexChanged);
            this.lvwOrderList.DoubleClick += new System.EventHandler(this.lvwOrderList_DoubleClick);
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
            iconResourceHandle3.File = "16x16.flag_grey.png";
            this.colPriority.Image = iconResourceHandle3;
            this.colPriority.Text = "";
            this.colPriority.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colPriority.Width = 24;
            // 
            // colStatus
            // 
            this.colStatus.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle4.File = "16x16.folder_grey.png";
            this.colStatus.Image = iconResourceHandle4;
            this.colStatus.Text = "";
            this.colStatus.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colStatus.Width = 24;
            // 
            // colDelivery
            // 
            this.colDelivery.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle5.File = "16x16.jobOrder_delivery.png";
            this.colDelivery.Image = iconResourceHandle5;
            this.colDelivery.Text = "";
            this.colDelivery.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colDelivery.Width = 24;
            // 
            // colComment
            // 
            this.colComment.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle6.File = "16x16.jobOrder_commentgrey.png";
            this.colComment.Image = iconResourceHandle6;
            this.colComment.Text = "";
            this.colComment.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colComment.Width = 24;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 30;
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
            // ansOrderList
            // 
            this.ansOrderList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansOrderList.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansOrderList.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansOrderList.DragHandle = true;
            this.ansOrderList.DropDownArrows = false;
            this.ansOrderList.ImageList = null;
            this.ansOrderList.Location = new System.Drawing.Point(0, 0);
            this.ansOrderList.MenuHandle = true;
            this.ansOrderList.Name = "ansOrderList";
            this.ansOrderList.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansOrderList.ShowToolTips = true;
            this.ansOrderList.TabIndex = 0;
            // 
            // OrderList
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.navPane);
            this.Size = new System.Drawing.Size(741, 306);
            this.Text = "DropBoxExplorer";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel navPane;
        private Gizmox.WebGUI.Forms.Splitter splitter1;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.TreeView tvwClient;
        private Gizmox.WebGUI.Forms.ToolBar ansClientTree;
        private Gizmox.WebGUI.Forms.ToolBar ansOrderList;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.TabControl idxSelect;
        private Gizmox.WebGUI.Forms.TabPage tabClient;
        private Gizmox.WebGUI.Forms.TabPage tabAdvanced;
        private Gizmox.WebGUI.Forms.GroupBox gbxOrderId;
        private Gizmox.WebGUI.Forms.Label lblOrderIdTo;
        private Gizmox.WebGUI.Forms.TextBox txtOrderIdFrom;
        private Gizmox.WebGUI.Forms.Label lblOrderIdFrom;
        private Gizmox.WebGUI.Forms.Button cmdFindOrderId;
        private Gizmox.WebGUI.Forms.TextBox txtOrderIdTo;
        private Gizmox.WebGUI.Forms.GroupBox gbxOrderedOn;
        private Gizmox.WebGUI.Forms.Button cmdFindOrderedOn;
        private Gizmox.WebGUI.Forms.DateTimePicker dtpOrderedOnTo;
        private Gizmox.WebGUI.Forms.Label lblOrderedOnTo;
        private Gizmox.WebGUI.Forms.DateTimePicker dtpOrderedOnFrom;
        private Gizmox.WebGUI.Forms.Label lblOrderedOnFrom;
        private Gizmox.WebGUI.Forms.ListView lvwOrderList;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderId;
        private Gizmox.WebGUI.Forms.ColumnHeader colPriority;
        private Gizmox.WebGUI.Forms.ColumnHeader colStatus;
        private Gizmox.WebGUI.Forms.ColumnHeader colDelivery;
        private Gizmox.WebGUI.Forms.ColumnHeader colComment;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.ColumnHeader colClientName;
        private Gizmox.WebGUI.Forms.ColumnHeader colAttachment;
        private Gizmox.WebGUI.Forms.ColumnHeader colRemarks;
        private Gizmox.WebGUI.Forms.ColumnHeader colReceivedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colCompletedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderType;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colPrepressOp;
        private Gizmox.WebGUI.Forms.ColumnHeader colRetouchBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colWorkshop;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceNumber;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceDate;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaid;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaymentType;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaidOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colPaidAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colModifiedBy;
        private Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn;


    }
}