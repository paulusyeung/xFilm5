namespace xFilm5.Customer.Billing
{
    partial class DNList_v5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DNList_v5));
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.dgvDNList = new Gizmox.WebGUI.Forms.DataGridView();
            this.dgvDNItems = new Gizmox.WebGUI.Forms.HierarchicInfo();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.dgvRtfItems = new Gizmox.WebGUI.Forms.HierarchicInfo();
            this.splitContainer1 = new Gizmox.WebGUI.Forms.SplitContainer();
            this.pnlToolStrip = new Gizmox.WebGUI.Forms.Panel();
            this.toolStrip1 = new Gizmox.WebGUI.Forms.ToolStrip();
            this.lblPrint = new Gizmox.WebGUI.Forms.ToolStripLabel();
            this.cmdA5 = new Gizmox.WebGUI.Forms.ToolStripButton();
            this.cmd80mm = new Gizmox.WebGUI.Forms.ToolStripButton();
            this.cmdEmail = new Gizmox.WebGUI.Forms.ToolStripButton();
            this.toolStripSeparator1 = new Gizmox.WebGUI.Forms.ToolStripSeparator();
            this.cmdExcel = new Gizmox.WebGUI.Forms.ToolStripButton();
            this.lblMonth = new Gizmox.WebGUI.Forms.Label();
            this.cmdRefresh = new Gizmox.WebGUI.Forms.Button();
            this.dtpSelectedDate = new Gizmox.WebGUI.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDNList)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 306);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // wspPane
            // 
            this.wspPane.Location = new System.Drawing.Point(333, 126);
            this.wspPane.Margin = new Gizmox.WebGUI.Forms.Padding(0, 30, 0, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(376, 176);
            this.wspPane.TabIndex = 2;
            // 
            // dgvDNList
            // 
            this.dgvDNList.ExpansionIndicatorColumnVisible = true;
            this.dgvDNList.HierarchicInfos.Add(this.dgvDNItems);
            this.dgvDNList.Location = new System.Drawing.Point(98, 150);
            this.dgvDNList.Name = "dgvDNList";
            this.dgvDNList.Size = new System.Drawing.Size(262, 103);
            this.dgvDNList.TabIndex = 6;
            // 
            // dgvDNItems
            // 
            this.dgvDNItems.BindedSource = null;
            this.dgvDNItems.HierarchyName = null;
            // 
            // dgvRtfItems
            // 
            this.dgvRtfItems.BindedSource = null;
            this.dgvRtfItems.HierarchyName = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AutoValidate = Gizmox.WebGUI.Forms.AutoValidate.EnablePreventFocusChange;
            this.splitContainer1.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = Gizmox.WebGUI.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(1, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Gizmox.WebGUI.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlToolStrip);
            this.splitContainer1.Panel1MinSize = 28;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDNList);
            this.splitContainer1.Size = new System.Drawing.Size(740, 306);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.toolStrip1);
            this.pnlToolStrip.Controls.Add(this.lblMonth);
            this.pnlToolStrip.Controls.Add(this.cmdRefresh);
            this.pnlToolStrip.Controls.Add(this.dtpSelectedDate);
            this.pnlToolStrip.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(740, 28);
            this.pnlToolStrip.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = Gizmox.WebGUI.Forms.DockStyle.None;
            this.toolStrip1.DockPadding.Right = 8;
            this.toolStrip1.Items.AddRange(new Gizmox.WebGUI.Forms.ToolStripItem[] {
            this.lblPrint,
            this.cmdA5,
            this.cmd80mm,
            this.cmdEmail,
            this.toolStripSeparator1,
            this.cmdExcel});
            this.toolStrip1.Location = new System.Drawing.Point(211, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new Gizmox.WebGUI.Forms.Padding(0, 0, 8, 0);
            this.toolStrip1.Size = new System.Drawing.Size(540, 24);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblPrint
            // 
            this.lblPrint.Name = "lblPrint";
            this.lblPrint.Size = new System.Drawing.Size(33, 13);
            this.lblPrint.Text = "Print:";
            // 
            // cmdA5
            // 
            this.cmdA5.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdA5.Image"));
            this.cmdA5.Name = "cmdA5";
            this.cmdA5.Size = new System.Drawing.Size(40, 20);
            this.cmdA5.Text = "A5";
            this.cmdA5.ToolTipText = "InkJet Printer";
            // 
            // cmd80mm
            // 
            this.cmd80mm.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmd80mm.Image"));
            this.cmd80mm.Name = "cmd80mm";
            this.cmd80mm.Size = new System.Drawing.Size(63, 20);
            this.cmd80mm.Text = "Receipt";
            this.cmd80mm.ToolTipText = "Receipt Printer";
            // 
            // cmdEmail
            // 
            this.cmdEmail.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdEmail.Image"));
            this.cmdEmail.Name = "cmdEmail";
            this.cmdEmail.Size = new System.Drawing.Size(51, 20);
            this.cmdEmail.Text = "Email";
            this.cmdEmail.ToolTipText = "Send Email";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Alignment = Gizmox.WebGUI.Forms.ToolStripItemAlignment.Right;
            this.cmdExcel.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdExcel.Image"));
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(23, 20);
            this.cmdExcel.ToolTipText = "Export Excel";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.Location = new System.Drawing.Point(11, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(49, 20);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "Month:";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdRefresh.Image"));
            this.cmdRefresh.Location = new System.Drawing.Point(160, 0);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(20, 20);
            this.cmdRefresh.TabIndex = 4;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // dtpSelectedDate
            // 
            this.dtpSelectedDate.CustomFormat = "MMM, yyyy";
            this.dtpSelectedDate.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.dtpSelectedDate.Location = new System.Drawing.Point(60, 0);
            this.dtpSelectedDate.Name = "dtpSelectedDate";
            this.dtpSelectedDate.Size = new System.Drawing.Size(100, 21);
            this.dtpSelectedDate.TabIndex = 1;
            // 
            // DNList_v5
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.splitter1);
            this.Size = new System.Drawing.Size(741, 306);
            this.Text = "DropBoxExplorer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDNList)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Gizmox.WebGUI.Forms.Splitter splitter1;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.DataGridView dgvDNList;
        private Gizmox.WebGUI.Forms.HierarchicInfo dgvRtfItems;
        private Gizmox.WebGUI.Forms.HierarchicInfo dgvDNItems;
        private Gizmox.WebGUI.Forms.SplitContainer splitContainer1;
        private Gizmox.WebGUI.Forms.Panel pnlToolStrip;
        private Gizmox.WebGUI.Forms.DateTimePicker dtpSelectedDate;
        private Gizmox.WebGUI.Forms.Button cmdRefresh;
        private Gizmox.WebGUI.Forms.Label lblMonth;
        private Gizmox.WebGUI.Forms.ToolStrip toolStrip1;
        private Gizmox.WebGUI.Forms.ToolStripLabel lblPrint;
        private Gizmox.WebGUI.Forms.ToolStripButton cmdA5;
        private Gizmox.WebGUI.Forms.ToolStripButton cmd80mm;
        private Gizmox.WebGUI.Forms.ToolStripButton cmdEmail;
        private Gizmox.WebGUI.Forms.ToolStripSeparator toolStripSeparator1;
        private Gizmox.WebGUI.Forms.ToolStripButton cmdExcel;
    }
}