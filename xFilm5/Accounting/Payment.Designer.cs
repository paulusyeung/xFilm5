namespace xFilm5.Accounting
{
    partial class Payment
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspClientRecord = new Gizmox.WebGUI.Forms.Panel();
            this.boxPayment = new Gizmox.WebGUI.Forms.GroupBox();
            this.txtReference = new Gizmox.WebGUI.Forms.TextBox();
            this.lblReference = new Gizmox.WebGUI.Forms.Label();
            this.txtPaidAmount = new Gizmox.WebGUI.Forms.TextBox();
            this.lblPaymentAmount = new Gizmox.WebGUI.Forms.Label();
            this.boxSelected = new Gizmox.WebGUI.Forms.GroupBox();
            this.lblPcs = new Gizmox.WebGUI.Forms.Label();
            this.txtTotalAmount = new Gizmox.WebGUI.Forms.TextBox();
            this.txtInvoices = new Gizmox.WebGUI.Forms.TextBox();
            this.lblInviceAmt = new Gizmox.WebGUI.Forms.Label();
            this.lblInvoices = new Gizmox.WebGUI.Forms.Label();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.lblPaidOn = new Gizmox.WebGUI.Forms.Label();
            this.datPaidOn = new Gizmox.WebGUI.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 0;
            // 
            // wspClientRecord
            // 
            this.wspClientRecord.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspClientRecord.Controls.Add(this.boxPayment);
            this.wspClientRecord.Controls.Add(this.boxSelected);
            this.wspClientRecord.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspClientRecord.DockPadding.All = 6;
            this.wspClientRecord.Location = new System.Drawing.Point(0, 28);
            this.wspClientRecord.Name = "wspClientRecord";
            this.wspClientRecord.Size = new System.Drawing.Size(405, 215);
            this.wspClientRecord.TabIndex = 1;
            // 
            // boxPayment
            // 
            this.boxPayment.Controls.Add(this.datPaidOn);
            this.boxPayment.Controls.Add(this.lblPaidOn);
            this.boxPayment.Controls.Add(this.txtReference);
            this.boxPayment.Controls.Add(this.lblReference);
            this.boxPayment.Controls.Add(this.txtPaidAmount);
            this.boxPayment.Controls.Add(this.lblPaymentAmount);
            this.boxPayment.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxPayment.Location = new System.Drawing.Point(7, 86);
            this.boxPayment.Name = "boxPayment";
            this.boxPayment.Size = new System.Drawing.Size(389, 94);
            this.boxPayment.TabIndex = 1;
            this.boxPayment.Text = "Payment";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(113, 40);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(270, 20);
            this.txtReference.TabIndex = 3;
            // 
            // lblReference
            // 
            this.lblReference.Location = new System.Drawing.Point(13, 40);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(100, 20);
            this.lblReference.TabIndex = 2;
            this.lblReference.Text = "Reference:";
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(113, 16);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(100, 20);
            this.txtPaidAmount.TabIndex = 1;
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.Location = new System.Drawing.Point(13, 16);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(100, 20);
            this.lblPaymentAmount.TabIndex = 0;
            this.lblPaymentAmount.Text = "Amount:";
            // 
            // boxSelected
            // 
            this.boxSelected.Controls.Add(this.lblPcs);
            this.boxSelected.Controls.Add(this.txtTotalAmount);
            this.boxSelected.Controls.Add(this.txtInvoices);
            this.boxSelected.Controls.Add(this.lblInviceAmt);
            this.boxSelected.Controls.Add(this.lblInvoices);
            this.boxSelected.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxSelected.Location = new System.Drawing.Point(7, 9);
            this.boxSelected.Name = "boxSelected";
            this.boxSelected.Size = new System.Drawing.Size(389, 70);
            this.boxSelected.TabIndex = 0;
            this.boxSelected.Text = "Selected";
            // 
            // lblPcs
            // 
            this.lblPcs.Location = new System.Drawing.Point(219, 16);
            this.lblPcs.Name = "lblPcs";
            this.lblPcs.Size = new System.Drawing.Size(29, 20);
            this.lblPcs.TabIndex = 2;
            this.lblPcs.Text = "PCS";
            this.lblPcs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(113, 39);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmount.TabIndex = 4;
            this.txtTotalAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtInvoices
            // 
            this.txtInvoices.Location = new System.Drawing.Point(113, 16);
            this.txtInvoices.Name = "txtInvoices";
            this.txtInvoices.ReadOnly = true;
            this.txtInvoices.Size = new System.Drawing.Size(100, 20);
            this.txtInvoices.TabIndex = 1;
            this.txtInvoices.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // lblInviceAmt
            // 
            this.lblInviceAmt.Location = new System.Drawing.Point(13, 39);
            this.lblInviceAmt.Name = "lblInviceAmt";
            this.lblInviceAmt.Size = new System.Drawing.Size(100, 20);
            this.lblInviceAmt.TabIndex = 3;
            this.lblInviceAmt.Text = "Total Amt.:";
            // 
            // lblInvoices
            // 
            this.lblInvoices.Location = new System.Drawing.Point(13, 16);
            this.lblInvoices.Name = "lblInvoices";
            this.lblInvoices.Size = new System.Drawing.Size(100, 20);
            this.lblInvoices.TabIndex = 0;
            this.lblInvoices.Text = "Invoices:";
            // 
            // lblPaidOn
            // 
            this.lblPaidOn.Location = new System.Drawing.Point(13, 64);
            this.lblPaidOn.Name = "lblPaidOn";
            this.lblPaidOn.Size = new System.Drawing.Size(100, 20);
            this.lblPaidOn.TabIndex = 4;
            this.lblPaidOn.Text = "Paid On:";
            // 
            // datPaidOn
            // 
            this.datPaidOn.CalendarFirstDayOfWeek = Gizmox.WebGUI.Forms.Day.Default;
            this.datPaidOn.CustomFormat = "dd/MM/yyyy";
            this.datPaidOn.Format = Gizmox.WebGUI.Forms.DateTimePickerFormat.Custom;
            this.datPaidOn.Location = new System.Drawing.Point(113, 64);
            this.datPaidOn.Name = "datPaidOn";
            this.datPaidOn.Size = new System.Drawing.Size(100, 20);
            this.datPaidOn.TabIndex = 5;
            // 
            // Payment
            // 
            this.Controls.Add(this.wspClientRecord);
            this.Controls.Add(this.ansToolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(405, 243);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspClientRecord;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.GroupBox boxSelected;
        private Gizmox.WebGUI.Forms.TextBox txtTotalAmount;
        private Gizmox.WebGUI.Forms.TextBox txtInvoices;
        private Gizmox.WebGUI.Forms.Label lblInviceAmt;
        private Gizmox.WebGUI.Forms.Label lblInvoices;
        private Gizmox.WebGUI.Forms.GroupBox boxPayment;
        private Gizmox.WebGUI.Forms.Label lblPaymentAmount;
        private Gizmox.WebGUI.Forms.Label lblPcs;
        private Gizmox.WebGUI.Forms.TextBox txtReference;
        private Gizmox.WebGUI.Forms.Label lblReference;
        private Gizmox.WebGUI.Forms.TextBox txtPaidAmount;
        private Gizmox.WebGUI.Forms.DateTimePicker datPaidOn;
        private Gizmox.WebGUI.Forms.Label lblPaidOn;


    }
}