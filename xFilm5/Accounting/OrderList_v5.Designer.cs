namespace xFilm5.Accounting
{
    partial class OrderList_v5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderList_v5));
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
            this.dgvOrderList = new Gizmox.WebGUI.Forms.DataGridView();
            this.dgvOrderItems = new Gizmox.WebGUI.Forms.HierarchicInfo();
            this.ansOrderList = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.dgvRtfItems = new Gizmox.WebGUI.Forms.HierarchicInfo();
            this.navPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idxSelect)).BeginInit();
            this.idxSelect.SuspendLayout();
            this.tabClient.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.gbxOrderedOn.SuspendLayout();
            this.gbxOrderId.SuspendLayout();
            this.wspPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).BeginInit();
            this.SuspendLayout();
            // 
            // navPane
            // 
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
            this.idxSelect.Controls.Add(this.tabClient);
            this.idxSelect.Controls.Add(this.tabAdvanced);
            this.idxSelect.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.idxSelect.Location = new System.Drawing.Point(0, 28);
            this.idxSelect.Name = "idxSelect";
            this.idxSelect.SelectedIndex = 0;
            this.idxSelect.Size = new System.Drawing.Size(200, 278);
            this.idxSelect.TabIndex = 2;
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.tvwClient);
            this.tabClient.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tabClient.Location = new System.Drawing.Point(4, 22);
            this.tabClient.Name = "tabClient";
            this.tabClient.Size = new System.Drawing.Size(192, 238);
            this.tabClient.TabIndex = 0;
            this.tabClient.Text = "Client";
            // 
            // tvwClient
            // 
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
            this.tabAdvanced.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
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
            this.gbxOrderedOn.TabStop = false;
            this.gbxOrderedOn.Text = "Ordered On";
            // 
            // cmdFindOrderedOn
            // 
            this.cmdFindOrderedOn.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdFindOrderedOn.Image"));
            this.cmdFindOrderedOn.Location = new System.Drawing.Point(144, 44);
            this.cmdFindOrderedOn.Name = "cmdFindOrderedOn";
            this.cmdFindOrderedOn.Size = new System.Drawing.Size(20, 20);
            this.cmdFindOrderedOn.TabIndex = 4;
            this.cmdFindOrderedOn.Click += new System.EventHandler(this.cmdFindOrderedOn_Click);
            // 
            // dtpOrderedOnTo
            // 
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
            this.gbxOrderId.TabStop = false;
            this.gbxOrderId.Text = "Order ID";
            // 
            // cmdFindOrderId
            // 
            this.cmdFindOrderId.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdFindOrderId.Image"));
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
            this.ansClientTree.DragHandle = true;
            this.ansClientTree.DropDownArrows = true;
            this.ansClientTree.ImageSize = new System.Drawing.Size(16, 16);
            this.ansClientTree.Location = new System.Drawing.Point(0, 0);
            this.ansClientTree.MenuHandle = true;
            this.ansClientTree.Name = "ansClientTree";
            this.ansClientTree.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansClientTree.ShowToolTips = true;
            this.ansClientTree.Size = new System.Drawing.Size(100, 22);
            this.ansClientTree.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 306);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // wspPane
            // 
            this.wspPane.Controls.Add(this.dgvOrderList);
            this.wspPane.Controls.Add(this.ansOrderList);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(201, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(540, 306);
            this.wspPane.TabIndex = 2;
            // 
            // dgvOrderList
            // 
            this.dgvOrderList.ExpansionIndicatorColumnVisible = true;
            this.dgvOrderList.HierarchicInfos.Add(this.dgvOrderItems);
            this.dgvOrderList.Location = new System.Drawing.Point(25, 178);
            this.dgvOrderList.Name = "dgvOrderList";
            this.dgvOrderList.Size = new System.Drawing.Size(262, 103);
            this.dgvOrderList.TabIndex = 6;
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.BindedSource = null;
            this.dgvOrderItems.HierarchyName = null;
            // 
            // ansOrderList
            // 
            this.ansOrderList.ButtonSize = new System.Drawing.Size(22, 22);
            this.ansOrderList.DragHandle = true;
            this.ansOrderList.DropDownArrows = true;
            this.ansOrderList.ImageSize = new System.Drawing.Size(16, 16);
            this.ansOrderList.Location = new System.Drawing.Point(0, 0);
            this.ansOrderList.MenuHandle = true;
            this.ansOrderList.Name = "ansOrderList";
            this.ansOrderList.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansOrderList.ShowToolTips = true;
            this.ansOrderList.Size = new System.Drawing.Size(540, 28);
            this.ansOrderList.TabIndex = 0;
            // 
            // dgvRtfItems
            // 
            this.dgvRtfItems.BindedSource = null;
            this.dgvRtfItems.HierarchyName = null;
            // 
            // OrderList_v5
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.navPane);
            this.Size = new System.Drawing.Size(741, 306);
            this.Text = "DropBoxExplorer";
            this.navPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idxSelect)).EndInit();
            this.idxSelect.ResumeLayout(false);
            this.tabClient.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.gbxOrderedOn.ResumeLayout(false);
            this.gbxOrderId.ResumeLayout(false);
            this.wspPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).EndInit();
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
        private Gizmox.WebGUI.Forms.DataGridView dgvOrderList;
        private Gizmox.WebGUI.Forms.HierarchicInfo dgvRtfItems;
        private Gizmox.WebGUI.Forms.HierarchicInfo dgvOrderItems;
    }
}