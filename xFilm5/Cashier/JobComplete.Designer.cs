namespace xFilm5.Cashier
{
    partial class JobComplete
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle5 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle6 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.boxInvoiceList = new Gizmox.WebGUI.Forms.GroupBox();
            this.flpInvoiceList = new Gizmox.WebGUI.Forms.FlowLayoutPanel();
            this.cmdDone = new Gizmox.WebGUI.Forms.Button();
            this.lblInvoiceNumber = new Gizmox.WebGUI.Forms.Label();
            this.cmdCancel = new Gizmox.WebGUI.Forms.Button();
            this.txtInvoiceNumber = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdPayLater = new Gizmox.WebGUI.Forms.Button();
            this.cmdPayNow = new Gizmox.WebGUI.Forms.Button();
            this.SuspendLayout();
            // 
            // boxInvoiceList
            // 
            this.boxInvoiceList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.boxInvoiceList.Controls.Add(this.flpInvoiceList);
            this.boxInvoiceList.Controls.Add(this.cmdDone);
            this.boxInvoiceList.Controls.Add(this.lblInvoiceNumber);
            this.boxInvoiceList.Controls.Add(this.cmdCancel);
            this.boxInvoiceList.Controls.Add(this.txtInvoiceNumber);
            this.boxInvoiceList.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxInvoiceList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.boxInvoiceList.Location = new System.Drawing.Point(67, 137);
            this.boxInvoiceList.Name = "boxInvoiceList";
            this.boxInvoiceList.Size = new System.Drawing.Size(546, 429);
            this.boxInvoiceList.TabIndex = 1;
            this.boxInvoiceList.Text = "現付";
            this.boxInvoiceList.Visible = false;
            // 
            // flpInvoiceList
            // 
            this.flpInvoiceList.AutoScroll = true;
            this.flpInvoiceList.BorderColor = System.Drawing.Color.LightGray;
            this.flpInvoiceList.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.FixedSingle;
            this.flpInvoiceList.DockPadding.All = 6;
            this.flpInvoiceList.FlowDirection = Gizmox.WebGUI.Forms.FlowDirection.LeftToRight;
            this.flpInvoiceList.Location = new System.Drawing.Point(22, 74);
            this.flpInvoiceList.Name = "flpInvoiceList";
            this.flpInvoiceList.Size = new System.Drawing.Size(504, 251);
            this.flpInvoiceList.TabIndex = 3;
            this.flpInvoiceList.WrapContents = false;
            // 
            // cmdDone
            // 
            this.cmdDone.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.cmdDone.ForeColor = System.Drawing.Color.Gray;
            iconResourceHandle5.File = "48x48.Okay.png";
            this.cmdDone.Image = iconResourceHandle5;
            this.cmdDone.Location = new System.Drawing.Point(326, 331);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Size = new System.Drawing.Size(200, 76);
            this.cmdDone.TabIndex = 1;
            this.cmdDone.Text = "完成";
            this.cmdDone.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNumber.Location = new System.Drawing.Point(22, 22);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(225, 46);
            this.lblInvoiceNumber.TabIndex = 2;
            this.lblInvoiceNumber.Text = "發票號碼︰";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.ForeColor = System.Drawing.Color.Gray;
            iconResourceHandle6.File = "48x48.Cancel.png";
            this.cmdCancel.Image = iconResourceHandle6;
            this.cmdCancel.Location = new System.Drawing.Point(22, 331);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(200, 76);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 24F);
            this.txtInvoiceNumber.Location = new System.Drawing.Point(253, 22);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(273, 46);
            this.txtInvoiceNumber.TabIndex = 0;
            this.txtInvoiceNumber.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtInvoiceNumber_EnterKeyDown);
            // 
            // cmdPayLater
            // 
            this.cmdPayLater.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.cmdPayLater.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.cmdPayLater.ForeColor = System.Drawing.Color.Green;
            this.cmdPayLater.Location = new System.Drawing.Point(413, 143);
            this.cmdPayLater.Name = "cmdPayLater";
            this.cmdPayLater.Size = new System.Drawing.Size(200, 76);
            this.cmdPayLater.TabIndex = 0;
            this.cmdPayLater.Text = "記帳";
            this.cmdPayLater.Click += new System.EventHandler(this.cmdPayLater_Click);
            // 
            // cmdPayNow
            // 
            this.cmdPayNow.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.cmdPayNow.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.cmdPayNow.ForeColor = System.Drawing.Color.Red;
            this.cmdPayNow.Location = new System.Drawing.Point(67, 143);
            this.cmdPayNow.Name = "cmdPayNow";
            this.cmdPayNow.Size = new System.Drawing.Size(200, 76);
            this.cmdPayNow.TabIndex = 0;
            this.cmdPayNow.Text = "付現";
            this.cmdPayNow.Click += new System.EventHandler(this.cmdPayNow_Click);
            // 
            // JobComplete
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.cmdPayNow);
            this.Controls.Add(this.cmdPayLater);
            this.Controls.Add(this.boxInvoiceList);
            this.DockPadding.All = 6;
            this.Size = new System.Drawing.Size(692, 563);
            this.Text = "JobComplete";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.GroupBox boxInvoiceList;
        private Gizmox.WebGUI.Forms.Button cmdPayLater;
        private Gizmox.WebGUI.Forms.Button cmdPayNow;
        private Gizmox.WebGUI.Forms.Button cmdCancel;
        private Gizmox.WebGUI.Forms.TextBox txtInvoiceNumber;
        private Gizmox.WebGUI.Forms.Label lblInvoiceNumber;
        private Gizmox.WebGUI.Forms.Button cmdDone;
        private Gizmox.WebGUI.Forms.FlowLayoutPanel flpInvoiceList;


    }
}