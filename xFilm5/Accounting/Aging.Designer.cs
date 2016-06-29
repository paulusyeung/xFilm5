namespace xFilm5.Accounting
{
    partial class Aging
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
            this.navPane = new Gizmox.WebGUI.Forms.Panel();
            this.ansClientTree = new Gizmox.WebGUI.Forms.ToolBar();
            this.tvwClient = new Gizmox.WebGUI.Forms.TreeView();
            this.splitter1 = new Gizmox.WebGUI.Forms.Splitter();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.lvwAgingList = new Gizmox.WebGUI.Forms.ListView();
            this.colInoiceNumber = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceDate = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colOrderId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colRemarks = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.wspFooter = new Gizmox.WebGUI.Forms.Panel();
            this.txt3Months = new Gizmox.WebGUI.Forms.TextBox();
            this.txt2Months = new Gizmox.WebGUI.Forms.TextBox();
            this.txtLastMonth = new Gizmox.WebGUI.Forms.TextBox();
            this.txtCurMonth = new Gizmox.WebGUI.Forms.TextBox();
            this.txtTotalDue = new Gizmox.WebGUI.Forms.TextBox();
            this.lbl3Months = new Gizmox.WebGUI.Forms.Label();
            this.lbl2Months = new Gizmox.WebGUI.Forms.Label();
            this.lblLastMonth = new Gizmox.WebGUI.Forms.Label();
            this.lblCurMonth = new Gizmox.WebGUI.Forms.Label();
            this.lblTotal = new Gizmox.WebGUI.Forms.Label();
            this.ansFileExplorer = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // navPane
            // 
            this.navPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navPane.Controls.Add(this.ansClientTree);
            this.navPane.Controls.Add(this.tvwClient);
            this.navPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.navPane.Location = new System.Drawing.Point(0, 0);
            this.navPane.Name = "navPane";
            this.navPane.Size = new System.Drawing.Size(200, 306);
            this.navPane.TabIndex = 0;
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
            // tvwClient
            // 
            this.tvwClient.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.tvwClient.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.tvwClient.Location = new System.Drawing.Point(0, 0);
            this.tvwClient.Name = "tvwClient";
            this.tvwClient.Size = new System.Drawing.Size(200, 306);
            this.tvwClient.TabIndex = 0;
            this.tvwClient.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.tvwClient_AfterSelect);
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
            this.wspPane.Controls.Add(this.lvwAgingList);
            this.wspPane.Controls.Add(this.wspFooter);
            this.wspPane.Controls.Add(this.ansFileExplorer);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(201, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(540, 306);
            this.wspPane.TabIndex = 2;
            // 
            // lvwAgingList
            // 
            this.lvwAgingList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwAgingList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colInoiceNumber,
            this.colLN,
            this.colInvoiceDate,
            this.colInvoiceAmount,
            this.colOrderId,
            this.colRemarks,
            this.colInvoiceId});
            this.lvwAgingList.DataMember = null;
            this.lvwAgingList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwAgingList.ItemsPerPage = 20;
            this.lvwAgingList.Location = new System.Drawing.Point(0, 28);
            this.lvwAgingList.MultiSelect = false;
            this.lvwAgingList.Name = "lvwAgingList";
            this.lvwAgingList.Size = new System.Drawing.Size(540, 238);
            this.lvwAgingList.TabIndex = 2;
            this.lvwAgingList.SelectedIndexChanged += new System.EventHandler(this.lvwAgingList_SelectedIndexChanged);
            this.lvwAgingList.DoubleClick += new System.EventHandler(this.lvwFileExplorer_DoubleClick);
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
            // wspFooter
            // 
            this.wspFooter.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspFooter.Controls.Add(this.txt3Months);
            this.wspFooter.Controls.Add(this.txt2Months);
            this.wspFooter.Controls.Add(this.txtLastMonth);
            this.wspFooter.Controls.Add(this.txtCurMonth);
            this.wspFooter.Controls.Add(this.txtTotalDue);
            this.wspFooter.Controls.Add(this.lbl3Months);
            this.wspFooter.Controls.Add(this.lbl2Months);
            this.wspFooter.Controls.Add(this.lblLastMonth);
            this.wspFooter.Controls.Add(this.lblCurMonth);
            this.wspFooter.Controls.Add(this.lblTotal);
            this.wspFooter.Dock = Gizmox.WebGUI.Forms.DockStyle.Bottom;
            this.wspFooter.Location = new System.Drawing.Point(0, 266);
            this.wspFooter.Name = "wspFooter";
            this.wspFooter.Size = new System.Drawing.Size(540, 40);
            this.wspFooter.TabIndex = 1;
            this.wspFooter.Visible = false;
            // 
            // txt3Months
            // 
            this.txt3Months.Location = new System.Drawing.Point(406, 20);
            this.txt3Months.Name = "txt3Months";
            this.txt3Months.Size = new System.Drawing.Size(100, 20);
            this.txt3Months.TabIndex = 9;
            this.txt3Months.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txt2Months
            // 
            this.txt2Months.Location = new System.Drawing.Point(306, 20);
            this.txt2Months.Name = "txt2Months";
            this.txt2Months.Size = new System.Drawing.Size(100, 20);
            this.txt2Months.TabIndex = 8;
            this.txt2Months.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtLastMonth
            // 
            this.txtLastMonth.Location = new System.Drawing.Point(206, 20);
            this.txtLastMonth.Name = "txtLastMonth";
            this.txtLastMonth.Size = new System.Drawing.Size(100, 20);
            this.txtLastMonth.TabIndex = 7;
            this.txtLastMonth.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtCurMonth
            // 
            this.txtCurMonth.Location = new System.Drawing.Point(106, 20);
            this.txtCurMonth.Name = "txtCurMonth";
            this.txtCurMonth.Size = new System.Drawing.Size(100, 20);
            this.txtCurMonth.TabIndex = 6;
            this.txtCurMonth.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalDue
            // 
            this.txtTotalDue.Location = new System.Drawing.Point(6, 20);
            this.txtTotalDue.Name = "txtTotalDue";
            this.txtTotalDue.Size = new System.Drawing.Size(100, 20);
            this.txtTotalDue.TabIndex = 5;
            this.txtTotalDue.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            // 
            // lbl3Months
            // 
            this.lbl3Months.Location = new System.Drawing.Point(406, 0);
            this.lbl3Months.Name = "lbl3Months";
            this.lbl3Months.Size = new System.Drawing.Size(100, 20);
            this.lbl3Months.TabIndex = 4;
            this.lbl3Months.Text = "3 Months";
            this.lbl3Months.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lbl2Months
            // 
            this.lbl2Months.Location = new System.Drawing.Point(306, 0);
            this.lbl2Months.Name = "lbl2Months";
            this.lbl2Months.Size = new System.Drawing.Size(100, 20);
            this.lbl2Months.TabIndex = 3;
            this.lbl2Months.Text = "2 Months";
            this.lbl2Months.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblLastMonth
            // 
            this.lblLastMonth.Location = new System.Drawing.Point(206, 0);
            this.lblLastMonth.Name = "lblLastMonth";
            this.lblLastMonth.Size = new System.Drawing.Size(100, 20);
            this.lblLastMonth.TabIndex = 2;
            this.lblLastMonth.Text = "Last Month";
            this.lblLastMonth.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCurMonth
            // 
            this.lblCurMonth.Location = new System.Drawing.Point(106, 0);
            this.lblCurMonth.Name = "lblCurMonth";
            this.lblCurMonth.Size = new System.Drawing.Size(100, 20);
            this.lblCurMonth.TabIndex = 1;
            this.lblCurMonth.Text = "Current Month";
            this.lblCurMonth.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(6, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 20);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total Due";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ansFileExplorer
            // 
            this.ansFileExplorer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansFileExplorer.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansFileExplorer.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansFileExplorer.DragHandle = true;
            this.ansFileExplorer.DropDownArrows = false;
            this.ansFileExplorer.ImageList = null;
            this.ansFileExplorer.Location = new System.Drawing.Point(0, 0);
            this.ansFileExplorer.MenuHandle = true;
            this.ansFileExplorer.Name = "ansFileExplorer";
            this.ansFileExplorer.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansFileExplorer.ShowToolTips = true;
            this.ansFileExplorer.TabIndex = 0;
            // 
            // Aging
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
        private Gizmox.WebGUI.Forms.ToolBar ansFileExplorer;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.ListView lvwAgingList;
        private Gizmox.WebGUI.Forms.ColumnHeader colInoiceNumber;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.Panel wspFooter;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceDate;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceAmount;
        private Gizmox.WebGUI.Forms.ColumnHeader colOrderId;
        private Gizmox.WebGUI.Forms.ColumnHeader colRemarks;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceId;
        private Gizmox.WebGUI.Forms.Label lbl2Months;
        private Gizmox.WebGUI.Forms.Label lblLastMonth;
        private Gizmox.WebGUI.Forms.Label lblCurMonth;
        private Gizmox.WebGUI.Forms.Label lblTotal;
        private Gizmox.WebGUI.Forms.TextBox txtTotalDue;
        private Gizmox.WebGUI.Forms.Label lbl3Months;
        private Gizmox.WebGUI.Forms.TextBox txt3Months;
        private Gizmox.WebGUI.Forms.TextBox txt2Months;
        private Gizmox.WebGUI.Forms.TextBox txtLastMonth;
        private Gizmox.WebGUI.Forms.TextBox txtCurMonth;


    }
}