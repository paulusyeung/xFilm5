namespace xFilm5.Customer.JobOrder
{
    partial class JoList
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle6 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle10 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle7 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle8 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle9 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
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
            iconResourceHandle6.File = "16x16.flag_grey.png";
            this.colPriority.Image = iconResourceHandle6;
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
            iconResourceHandle10.File = "16x16.16_find.gif";
            this.cmdLookup.Image = iconResourceHandle10;
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
            this.lblLookup.Text = "Look for:";
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
            this.colWorkshop});
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
            iconResourceHandle7.File = "16x16.folder_grey.png";
            this.colStatus.Image = iconResourceHandle7;
            this.colStatus.Text = "";
            this.colStatus.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colStatus.Width = 24;
            // 
            // colDelivery
            // 
            this.colDelivery.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle8.File = "16x16.jobOrder_delivery.png";
            this.colDelivery.Image = iconResourceHandle8;
            this.colDelivery.Text = "";
            this.colDelivery.Type = Gizmox.WebGUI.Forms.ListViewColumnType.Icon;
            this.colDelivery.Width = 24;
            // 
            // colComment
            // 
            this.colComment.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            iconResourceHandle9.File = "16x16.jobOrder_commentgrey.png";
            this.colComment.Image = iconResourceHandle9;
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
            this.colOrderedBy.Width = 70;
            // 
            // colPrepressOp
            // 
            this.colPrepressOp.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPrepressOp.Image = null;
            this.colPrepressOp.Text = "Prepress Op";
            this.colPrepressOp.Width = 70;
            // 
            // colRetouchBy
            // 
            this.colRetouchBy.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colRetouchBy.Image = null;
            this.colRetouchBy.Text = "Retouch By";
            this.colRetouchBy.Width = 70;
            // 
            // colWorkshop
            // 
            this.colWorkshop.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colWorkshop.Image = null;
            this.colWorkshop.Text = "Workshop";
            this.colWorkshop.Width = 70;
            // 
            // JoList
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

    }
}