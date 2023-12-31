namespace xFilm5.Cashier
{
    partial class CompletedList
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
            this.boxUnlock = new Gizmox.WebGUI.Forms.GroupBox();
            this.lblInvoiceNumber = new Gizmox.WebGUI.Forms.Label();
            this.txtPassword = new Gizmox.WebGUI.Forms.TextBox();
            this.splitContainer1 = new Gizmox.WebGUI.Forms.SplitContainer();
            this.lvwPayNow = new Gizmox.WebGUI.Forms.ListView();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colDateTime = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceNumber = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colInvoiceAmount = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.ansPayNow = new Gizmox.WebGUI.Forms.ToolBar();
            this.ansPayLater = new Gizmox.WebGUI.Forms.ToolBar();
            this.lvwPayLater = new Gizmox.WebGUI.Forms.ListView();
            this.columnHeader1 = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.columnHeader2 = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.columnHeader3 = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.columnHeader4 = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // boxUnlock
            // 
            this.boxUnlock.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.boxUnlock.Controls.Add(this.lblInvoiceNumber);
            this.boxUnlock.Controls.Add(this.txtPassword);
            this.boxUnlock.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxUnlock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.boxUnlock.Location = new System.Drawing.Point(137, 143);
            this.boxUnlock.Name = "boxUnlock";
            this.boxUnlock.Size = new System.Drawing.Size(546, 82);
            this.boxUnlock.TabIndex = 1;
            this.boxUnlock.Text = "Locked";
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNumber.Location = new System.Drawing.Point(22, 22);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(225, 46);
            this.lblInvoiceNumber.TabIndex = 2;
            this.lblInvoiceNumber.Text = "輸入密碼︰";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 24F);
            this.txtPassword.Location = new System.Drawing.Point(253, 22);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(273, 46);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtPassword_EnterKeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Clear;
            this.splitContainer1.FixedPanel = Gizmox.WebGUI.Forms.FixedPanel.None;
            this.splitContainer1.Location = new System.Drawing.Point(15, 231);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Gizmox.WebGUI.Forms.Orientation.Vertical;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ansPayNow);
            this.splitContainer1.Panel1.Controls.Add(this.lvwPayNow);
            this.splitContainer1.Panel1MinSize = 420;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvwPayLater);
            this.splitContainer1.Panel2.Controls.Add(this.ansPayLater);
            this.splitContainer1.Size = new System.Drawing.Size(780, 384);
            this.splitContainer1.SplitterDistance = 420;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.Visible = false;
            // 
            // lvwPayNow
            // 
            this.lvwPayNow.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colLN,
            this.colDateTime,
            this.colInvoiceNumber,
            this.colInvoiceAmount});
            this.lvwPayNow.DataMember = null;
            this.lvwPayNow.ItemsPerPage = 20;
            this.lvwPayNow.Location = new System.Drawing.Point(3, 141);
            this.lvwPayNow.Name = "lvwPayNow";
            this.lvwPayNow.Size = new System.Drawing.Size(370, 183);
            this.lvwPayNow.TabIndex = 0;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colLN.Width = 40;
            // 
            // colDateTime
            // 
            this.colDateTime.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colDateTime.Image = null;
            this.colDateTime.Text = "Time";
            this.colDateTime.Width = 100;
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceNumber.Image = null;
            this.colInvoiceNumber.Text = "Invoice No.";
            this.colInvoiceNumber.Width = 100;
            // 
            // colInvoiceAmount
            // 
            this.colInvoiceAmount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colInvoiceAmount.Image = null;
            this.colInvoiceAmount.Text = "Invoice Amt.";
            this.colInvoiceAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colInvoiceAmount.Width = 100;
            // 
            // ansPayNow
            // 
            this.ansPayNow.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansPayNow.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansPayNow.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansPayNow.DragHandle = true;
            this.ansPayNow.DropDownArrows = false;
            this.ansPayNow.ImageList = null;
            this.ansPayNow.Location = new System.Drawing.Point(0, 0);
            this.ansPayNow.MenuHandle = true;
            this.ansPayNow.Name = "ansPayNow";
            this.ansPayNow.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansPayNow.ShowToolTips = true;
            this.ansPayNow.TabIndex = 1;
            // 
            // ansPayLater
            // 
            this.ansPayLater.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansPayLater.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansPayLater.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansPayLater.DragHandle = true;
            this.ansPayLater.DropDownArrows = false;
            this.ansPayLater.ImageList = null;
            this.ansPayLater.Location = new System.Drawing.Point(0, 0);
            this.ansPayLater.MenuHandle = true;
            this.ansPayLater.Name = "ansPayLater";
            this.ansPayLater.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansPayLater.ShowToolTips = true;
            this.ansPayLater.TabIndex = 0;
            // 
            // lvwPayLater
            // 
            this.lvwPayLater.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvwPayLater.DataMember = null;
            this.lvwPayLater.ItemsPerPage = 20;
            this.lvwPayLater.Location = new System.Drawing.Point(13, 101);
            this.lvwPayLater.Name = "lvwPayLater";
            this.lvwPayLater.Size = new System.Drawing.Size(370, 183);
            this.lvwPayLater.TabIndex = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.columnHeader1.Image = null;
            this.columnHeader1.Text = "#";
            this.columnHeader1.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.columnHeader2.Image = null;
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.columnHeader3.Image = null;
            this.columnHeader3.Text = "Invoice No.";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.columnHeader4.Image = null;
            this.columnHeader4.Text = "Invoice Amt.";
            this.columnHeader4.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 100;
            // 
            // CompletedList
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.boxUnlock);
            this.Size = new System.Drawing.Size(825, 656);
            this.Text = "CompletedList";
            this.Load += new System.EventHandler(this.CompletedList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.GroupBox boxUnlock;
        private Gizmox.WebGUI.Forms.Label lblInvoiceNumber;
        private Gizmox.WebGUI.Forms.TextBox txtPassword;
        private Gizmox.WebGUI.Forms.SplitContainer splitContainer1;
        private Gizmox.WebGUI.Forms.ListView lvwPayNow;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.ColumnHeader colDateTime;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceNumber;
        private Gizmox.WebGUI.Forms.ColumnHeader colInvoiceAmount;
        private Gizmox.WebGUI.Forms.ToolBar ansPayNow;
        private Gizmox.WebGUI.Forms.ListView lvwPayLater;
        private Gizmox.WebGUI.Forms.ColumnHeader columnHeader1;
        private Gizmox.WebGUI.Forms.ColumnHeader columnHeader2;
        private Gizmox.WebGUI.Forms.ColumnHeader columnHeader3;
        private Gizmox.WebGUI.Forms.ColumnHeader columnHeader4;
        private Gizmox.WebGUI.Forms.ToolBar ansPayLater;


    }
}