namespace xFilm5.QRStation
{
    partial class Desktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Desktop));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.txtQrCodeData = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lvwLifeCycle = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCreatedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubitemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtClientInfo = new System.Windows.Forms.TextBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlInput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 561);
            this.splitContainer1.SplitterDistance = 81;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.txtQrCodeData);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(784, 81);
            this.pnlInput.TabIndex = 0;
            // 
            // txtQrCodeData
            // 
            this.txtQrCodeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQrCodeData.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQrCodeData.Location = new System.Drawing.Point(0, 0);
            this.txtQrCodeData.Multiline = true;
            this.txtQrCodeData.Name = "txtQrCodeData";
            this.txtQrCodeData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtQrCodeData.Size = new System.Drawing.Size(784, 81);
            this.txtQrCodeData.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlContent);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picPreview);
            this.splitContainer2.Size = new System.Drawing.Size(784, 476);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lvwLifeCycle);
            this.pnlContent.Controls.Add(this.txtClientInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(261, 476);
            this.pnlContent.TabIndex = 0;
            // 
            // lvwLifeCycle
            // 
            this.lvwLifeCycle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwLifeCycle.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colLn,
            this.colCreatedOn,
            this.colSubitemType});
            this.lvwLifeCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwLifeCycle.GridLines = true;
            this.lvwLifeCycle.Location = new System.Drawing.Point(0, 82);
            this.lvwLifeCycle.Name = "lvwLifeCycle";
            this.lvwLifeCycle.Size = new System.Drawing.Size(261, 394);
            this.lvwLifeCycle.TabIndex = 1;
            this.lvwLifeCycle.UseCompatibleStateImageBehavior = false;
            this.lvwLifeCycle.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.DisplayIndex = 3;
            this.colId.Width = 0;
            // 
            // colLn
            // 
            this.colLn.DisplayIndex = 0;
            this.colLn.Text = "#";
            this.colLn.Width = 24;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.DisplayIndex = 1;
            this.colCreatedOn.Text = "Created On";
            this.colCreatedOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCreatedOn.Width = 120;
            // 
            // colSubitemType
            // 
            this.colSubitemType.DisplayIndex = 2;
            this.colSubitemType.Text = "Type";
            this.colSubitemType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colSubitemType.Width = 80;
            // 
            // txtClientInfo
            // 
            this.txtClientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtClientInfo.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientInfo.Location = new System.Drawing.Point(0, 0);
            this.txtClientInfo.Multiline = true;
            this.txtClientInfo.Name = "txtClientInfo";
            this.txtClientInfo.ReadOnly = true;
            this.txtClientInfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtClientInfo.Size = new System.Drawing.Size(261, 82);
            this.txtClientInfo.TabIndex = 0;
            this.txtClientInfo.WordWrap = false;
            // 
            // picPreview
            // 
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(519, 476);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // Desktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Desktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "xFilm5 QR Station";
            this.Load += new System.EventHandler(this.Desktop_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.TextBox txtQrCodeData;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TextBox txtClientInfo;
        private System.Windows.Forms.ListView lvwLifeCycle;
        private System.Windows.Forms.ColumnHeader colLn;
        private System.Windows.Forms.ColumnHeader colCreatedOn;
        private System.Windows.Forms.ColumnHeader colSubitemType;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.PictureBox picPreview;
    }
}

