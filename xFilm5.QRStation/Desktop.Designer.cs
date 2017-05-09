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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtQrCodeData = new System.Windows.Forms.TextBox();
            this.pnlCounter = new System.Windows.Forms.Panel();
            this.lblBadCount = new System.Windows.Forms.Label();
            this.lblGoodCount = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtClientInfo = new System.Windows.Forms.TextBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lvwLifeCycle = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCreatedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubitemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblOrderNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlContent.SuspendLayout();
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
            this.pnlInput.Controls.Add(this.panel1);
            this.pnlInput.Controls.Add(this.pnlCounter);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(784, 81);
            this.pnlInput.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtQrCodeData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 81);
            this.panel1.TabIndex = 2;
            // 
            // txtQrCodeData
            // 
            this.txtQrCodeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQrCodeData.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQrCodeData.Location = new System.Drawing.Point(0, 0);
            this.txtQrCodeData.Multiline = true;
            this.txtQrCodeData.Name = "txtQrCodeData";
            this.txtQrCodeData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtQrCodeData.Size = new System.Drawing.Size(703, 81);
            this.txtQrCodeData.TabIndex = 1;
            // 
            // pnlCounter
            // 
            this.pnlCounter.Controls.Add(this.lblBadCount);
            this.pnlCounter.Controls.Add(this.lblGoodCount);
            this.pnlCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCounter.Location = new System.Drawing.Point(703, 0);
            this.pnlCounter.Name = "pnlCounter";
            this.pnlCounter.Size = new System.Drawing.Size(81, 81);
            this.pnlCounter.TabIndex = 1;
            // 
            // lblBadCount
            // 
            this.lblBadCount.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBadCount.ForeColor = System.Drawing.Color.Red;
            this.lblBadCount.Location = new System.Drawing.Point(6, 41);
            this.lblBadCount.Name = "lblBadCount";
            this.lblBadCount.Size = new System.Drawing.Size(70, 36);
            this.lblBadCount.TabIndex = 1;
            this.lblBadCount.Text = "99";
            this.lblBadCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGoodCount
            // 
            this.lblGoodCount.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoodCount.ForeColor = System.Drawing.Color.Green;
            this.lblGoodCount.Location = new System.Drawing.Point(6, 4);
            this.lblGoodCount.Name = "lblGoodCount";
            this.lblGoodCount.Size = new System.Drawing.Size(70, 36);
            this.lblGoodCount.TabIndex = 0;
            this.lblGoodCount.Text = "99";
            this.lblGoodCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picPreview);
            this.splitContainer2.Size = new System.Drawing.Size(784, 476);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.TabIndex = 0;
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
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pnlContent);
            this.splitContainer3.Size = new System.Drawing.Size(261, 476);
            this.splitContainer3.SplitterDistance = 167;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblOrderNumber);
            this.panel2.Controls.Add(this.txtClientInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 167);
            this.panel2.TabIndex = 3;
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
            this.txtClientInfo.TabIndex = 1;
            this.txtClientInfo.WordWrap = false;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lvwLifeCycle);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(261, 305);
            this.pnlContent.TabIndex = 4;
            // 
            // lvwLifeCycle
            // 
            this.lvwLifeCycle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwLifeCycle.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colLn,
            this.colCreatedOn,
            this.colSubitemType,
            this.colColor});
            this.lvwLifeCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwLifeCycle.GridLines = true;
            this.lvwLifeCycle.Location = new System.Drawing.Point(0, 0);
            this.lvwLifeCycle.Name = "lvwLifeCycle";
            this.lvwLifeCycle.Size = new System.Drawing.Size(261, 305);
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
            // colColor
            // 
            this.colColor.Text = "Color";
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrderNumber.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblOrderNumber.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.Black;
            this.lblOrderNumber.Location = new System.Drawing.Point(0, 85);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(261, 82);
            this.lblOrderNumber.TabIndex = 2;
            this.lblOrderNumber.Text = "0";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCounter.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel pnlCounter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtQrCodeData;
        private System.Windows.Forms.Label lblBadCount;
        private System.Windows.Forms.Label lblGoodCount;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtClientInfo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ListView lvwLifeCycle;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colLn;
        private System.Windows.Forms.ColumnHeader colCreatedOn;
        private System.Windows.Forms.ColumnHeader colSubitemType;
        private System.Windows.Forms.ColumnHeader colColor;
        private System.Windows.Forms.Label lblOrderNumber;
    }
}

