namespace xFilm5.Customer.Billing
{
    partial class InvoiceList
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle3 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle4 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.navPane = new Gizmox.WebGUI.Forms.Panel();
            this.idxSelect = new Gizmox.WebGUI.Forms.TabControl();
            this.tabAdvanced = new Gizmox.WebGUI.Forms.TabPage();
            this.gbxInvoiceDate = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdFindInvoiceDate = new Gizmox.WebGUI.Forms.Button();
            this.dtpInvoiceDateTo = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.lblInvoiceDateTo = new Gizmox.WebGUI.Forms.Label();
            this.dtpInvoiceDateFrom = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.lblInvoiceDateFrom = new Gizmox.WebGUI.Forms.Label();
            this.gbxInvoiceNumber = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdFindInvoiceNumber = new Gizmox.WebGUI.Forms.Button();
            this.txtInvoiceNumberTo = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInvoiceNumberTo = new Gizmox.WebGUI.Forms.Label();
            this.txtInvoiceNumberFrom = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInvoiceNumberFrom = new Gizmox.WebGUI.Forms.Label();
            this.ansClientTree = new Gizmox.WebGUI.Forms.ToolBar();
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.lvwInvoiceList = new Gizmox.WebGUI.Forms.ListView();
            this.colInoiceNumber = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceDate = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colOrderId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colRemarks = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.ansInvoiceList = new Gizmox.WebGUI.Forms.ToolBar();
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
            this.idxSelect.Controls.Add(this.tabAdvanced);
            this.idxSelect.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.idxSelect.Location = new System.Drawing.Point(0, 28);
            this.idxSelect.Multiline = false;
            this.idxSelect.Name = "idxSelect";
            this.idxSelect.SelectedIndex = 0;
            this.idxSelect.Size = new System.Drawing.Size(200, 278);
            this.idxSelect.TabIndex = 2;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.gbxInvoiceDate);
            this.tabAdvanced.Controls.Add(this.gbxInvoiceNumber);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(192, 252);
            this.tabAdvanced.TabIndex = 0;
            this.tabAdvanced.Text = "Selection";
            // 
            // gbxInvoiceDate
            // 
            this.gbxInvoiceDate.Controls.Add(this.cmdFindInvoiceDate);
            this.gbxInvoiceDate.Controls.Add(this.dtpInvoiceDateTo);
            this.gbxInvoiceDate.Controls.Add(this.lblInvoiceDateTo);
            this.gbxInvoiceDate.Controls.Add(this.dtpInvoiceDateFrom);
            this.gbxInvoiceDate.Controls.Add(this.lblInvoiceDateFrom);
            this.gbxInvoiceDate.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxInvoiceDate.Location = new System.Drawing.Point(7, 88);
            this.gbxInvoiceDate.Name = "gbxInvoiceDate";
            this.gbxInvoiceDate.Size = new System.Drawing.Size(179, 74);
            this.gbxInvoiceDate.TabIndex = 1;
            this.gbxInvoiceDate.Text = "Invoice Date";
            // 
            // cmdFindInvoiceDate
            // 
            iconResourceHandle3.File = "16x16.btn_on_lookup.gif";
            this.cmdFindInvoiceDate.Image = iconResourceHandle3;
            this.cmdFindInvoiceDate.Location = new System.Drawing.Point(144, 44);
            this.cmdFindInvoiceDate.Name = "cmdFindInvoiceDate";
            this.cmdFindInvoiceDate.Size = new System.Drawing.Size(20, 20);
            this.cmdFindInvoiceDate.TabIndex = 4;
            this.cmdFindInvoiceDate.Click += new System.EventHandler(this.cmdFindInvoiceDate_Click);
            // 
            // dtpInvoiceDateTo
            // 
            this.dtpInvoiceDateTo.CalendarFirstDayOfWeek = Gizmox.WebGUI.Forms.Day.Default;
            this.dtpInvoiceDateTo.CustomFormat = "yyyy-MM-dd";
            this.dtpInvoiceDateTo.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDateTo.Location = new System.Drawing.Point(44, 44);
            this.dtpInvoiceDateTo.Name = "dtpInvoiceDateTo";
            this.dtpInvoiceDateTo.Size = new System.Drawing.Size(100, 20);
            this.dtpInvoiceDateTo.TabIndex = 3;
            // 
            // lblInvoiceDateTo
            // 
            this.lblInvoiceDateTo.Location = new System.Drawing.Point(7, 44);
            this.lblInvoiceDateTo.Name = "lblInvoiceDateTo";
            this.lblInvoiceDateTo.Size = new System.Drawing.Size(37, 20);
            this.lblInvoiceDateTo.TabIndex = 2;
            this.lblInvoiceDateTo.Text = "To:";
            this.lblInvoiceDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpInvoiceDateFrom
            // 
            this.dtpInvoiceDateFrom.CalendarFirstDayOfWeek = Gizmox.WebGUI.Forms.Day.Default;
            this.dtpInvoiceDateFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpInvoiceDateFrom.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDateFrom.Location = new System.Drawing.Point(44, 20);
            this.dtpInvoiceDateFrom.Name = "dtpInvoiceDateFrom";
            this.dtpInvoiceDateFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpInvoiceDateFrom.TabIndex = 1;
            // 
            // lblInvoiceDateFrom
            // 
            this.lblInvoiceDateFrom.Location = new System.Drawing.Point(7, 20);
            this.lblInvoiceDateFrom.Name = "lblInvoiceDateFrom";
            this.lblInvoiceDateFrom.Size = new System.Drawing.Size(37, 20);
            this.lblInvoiceDateFrom.TabIndex = 0;
            this.lblInvoiceDateFrom.Text = "From:";
            this.lblInvoiceDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbxInvoiceNumber
            // 
            this.gbxInvoiceNumber.Controls.Add(this.cmdFindInvoiceNumber);
            this.gbxInvoiceNumber.Controls.Add(this.txtInvoiceNumberTo);
            this.gbxInvoiceNumber.Controls.Add(this.lblInvoiceNumberTo);
            this.gbxInvoiceNumber.Controls.Add(this.txtInvoiceNumberFrom);
            this.gbxInvoiceNumber.Controls.Add(this.lblInvoiceNumberFrom);
            this.gbxInvoiceNumber.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxInvoiceNumber.Location = new System.Drawing.Point(7, 7);
            this.gbxInvoiceNumber.Name = "gbxInvoiceNumber";
            this.gbxInvoiceNumber.Size = new System.Drawing.Size(179, 74);
            this.gbxInvoiceNumber.TabIndex = 0;
            this.gbxInvoiceNumber.Text = "Invoice No.";
            // 
            // cmdFindInvoiceNumber
            // 
            iconResourceHandle4.File = "16x16.btn_on_lookup.gif";
            this.cmdFindInvoiceNumber.Image = iconResourceHandle4;
            this.cmdFindInvoiceNumber.Location = new System.Drawing.Point(144, 42);
            this.cmdFindInvoiceNumber.Name = "cmdFindInvoiceNumber";
            this.cmdFindInvoiceNumber.Size = new System.Drawing.Size(20, 20);
            this.cmdFindInvoiceNumber.TabIndex = 4;
            this.cmdFindInvoiceNumber.Click += new System.EventHandler(this.cmdFindInvoiceNumber_Click);
            // 
            // txtInvoiceNumberTo
            // 
            this.txtInvoiceNumberTo.Location = new System.Drawing.Point(44, 42);
            this.txtInvoiceNumberTo.Name = "txtInvoiceNumberTo";
            this.txtInvoiceNumberTo.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceNumberTo.TabIndex = 3;
            // 
            // lblInvoiceNumberTo
            // 
            this.lblInvoiceNumberTo.Location = new System.Drawing.Point(7, 42);
            this.lblInvoiceNumberTo.Name = "lblInvoiceNumberTo";
            this.lblInvoiceNumberTo.Size = new System.Drawing.Size(37, 20);
            this.lblInvoiceNumberTo.TabIndex = 2;
            this.lblInvoiceNumberTo.Text = "To:";
            this.lblInvoiceNumberTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInvoiceNumberFrom
            // 
            this.txtInvoiceNumberFrom.Location = new System.Drawing.Point(44, 19);
            this.txtInvoiceNumberFrom.Name = "txtInvoiceNumberFrom";
            this.txtInvoiceNumberFrom.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceNumberFrom.TabIndex = 1;
            // 
            // lblInvoiceNumberFrom
            // 
            this.lblInvoiceNumberFrom.Location = new System.Drawing.Point(7, 20);
            this.lblInvoiceNumberFrom.Name = "lblInvoiceNumberFrom";
            this.lblInvoiceNumberFrom.Size = new System.Drawing.Size(37, 20);
            this.lblInvoiceNumberFrom.TabIndex = 0;
            this.lblInvoiceNumberFrom.Text = "From:";
            this.lblInvoiceNumberFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.wspPane.Controls.Add(this.lvwInvoiceList);
            this.wspPane.Controls.Add(this.ansInvoiceList);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(201, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(540, 306);
            this.wspPane.TabIndex = 2;
            // 
            // lvwInvoiceList
            // 
            this.lvwInvoiceList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwInvoiceList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colInoiceNumber,
            this.colLN,
            this.colInvoiceDate,
            this.colInvoiceAmount,
            this.colOrderId,
            this.colRemarks,
            this.colInvoiceId});
            this.lvwInvoiceList.DataMember = null;
            this.lvwInvoiceList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwInvoiceList.ItemsPerPage = 20;
            this.lvwInvoiceList.Location = new System.Drawing.Point(0, 28);
            this.lvwInvoiceList.MultiSelect = false;
            this.lvwInvoiceList.Name = "lvwInvoiceList";
            this.lvwInvoiceList.Size = new System.Drawing.Size(540, 278);
            this.lvwInvoiceList.TabIndex = 2;
            this.lvwInvoiceList.SelectedIndexChanged += new System.EventHandler(this.lvwInvoiceList_SelectedIndexChanged);
            this.lvwInvoiceList.DoubleClick += new System.EventHandler(this.lvwFileExplorer_DoubleClick);
            // 
            // colInoiceNumber
            // 
            this.colInoiceNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInoiceNumber.Image = null;
            this.colInoiceNumber.Text = "Invoice No.";
            this.colInoiceNumber.Width = 100;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.Width = 30;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceDate.Image = null;
            this.colInvoiceDate.Text = "Invoice Date";
            this.colInvoiceDate.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colInvoiceDate.Width = 80;
            // 
            // colInvoiceAmount
            // 
            this.colInvoiceAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceAmount.Image = null;
            this.colInvoiceAmount.Text = "Invoice Amt.";
            this.colInvoiceAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colInvoiceAmount.Width = 80;
            // 
            // colOrderId
            // 
            this.colOrderId.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colOrderId.Image = null;
            this.colOrderId.Text = "Order ID";
            this.colOrderId.Width = 80;
            // 
            // colRemarks
            // 
            this.colRemarks.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colRemarks.Image = null;
            this.colRemarks.Text = "Remarks";
            this.colRemarks.Width = 200;
            // 
            // colInvoiceId
            // 
            this.colInvoiceId.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceId.Image = null;
            this.colInvoiceId.Text = "Invoice ID";
            this.colInvoiceId.Visible = false;
            this.colInvoiceId.Width = 80;
            // 
            // ansInvoiceList
            // 
            this.ansInvoiceList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansInvoiceList.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansInvoiceList.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansInvoiceList.DragHandle = true;
            this.ansInvoiceList.DropDownArrows = false;
            this.ansInvoiceList.ImageList = null;
            this.ansInvoiceList.Location = new System.Drawing.Point(0, 0);
            this.ansInvoiceList.MenuHandle = true;
            this.ansInvoiceList.Name = "ansInvoiceList";
            this.ansInvoiceList.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansInvoiceList.ShowToolTips = true;
            this.ansInvoiceList.TabIndex = 0;
            // 
            // InvoiceList
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
        private Gizmox.WebGUI.Forms.ToolBar ansClientTree;
        private Gizmox.WebGUI.Forms.ToolBar ansInvoiceList;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.ListView lvwInvoiceList;
        private Gizmox.WebGUI.Forms.ColumnHeader colInoiceNumber;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceDate;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderId;
        private Gizmox.WebGUI.Forms.ColumnHeader colRemarks;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceId;
        private Gizmox.WebGUI.Forms.TabControl idxSelect;
        private Gizmox.WebGUI.Forms.TabPage tabAdvanced;
        private Gizmox.WebGUI.Forms.GroupBox gbxInvoiceNumber;
        private Gizmox.WebGUI.Forms.Label lblInvoiceNumberTo;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceNumberFrom;
        private Gizmox.WebGUI.Forms.Label lblInvoiceNumberFrom;
        private Gizmox.WebGUI.Forms.Button cmdFindInvoiceNumber;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceNumberTo;
        private Gizmox.WebGUI.Forms.GroupBox gbxInvoiceDate;
        private Gizmox.WebGUI.Forms.Button cmdFindInvoiceDate;
        private Gizmox.WebGUI.Forms.DateTimePicker dtpInvoiceDateTo;
        private Gizmox.WebGUI.Forms.Label lblInvoiceDateTo;
        private Gizmox.WebGUI.Forms.DateTimePicker dtpInvoiceDateFrom;
        private Gizmox.WebGUI.Forms.Label lblInvoiceDateFrom;


    }
}