namespace xFilm5.QRCoder
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblTiffFile = new System.Windows.Forms.Label();
            this.txtTiffFile = new System.Windows.Forms.TextBox();
            this.cmdTiffFile = new System.Windows.Forms.Button();
            this.lblQRCodeText = new System.Windows.Forms.Label();
            this.txtQRCodeText = new System.Windows.Forms.TextBox();
            this.cmdEncode = new System.Windows.Forms.Button();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.cmdMerge = new System.Windows.Forms.Button();
            this.lblQRCode = new System.Windows.Forms.Label();
            this.cmdGenTiff = new System.Windows.Forms.Button();
            this.cboQRCodeSize = new System.Windows.Forms.ComboBox();
            this.cboTiffResolution = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTiffFile
            // 
            this.lblTiffFile.AutoSize = true;
            this.lblTiffFile.Location = new System.Drawing.Point(13, 13);
            this.lblTiffFile.Name = "lblTiffFile";
            this.lblTiffFile.Size = new System.Drawing.Size(51, 13);
            this.lblTiffFile.TabIndex = 0;
            this.lblTiffFile.Text = "TIFF File:";
            // 
            // txtTiffFile
            // 
            this.txtTiffFile.Location = new System.Drawing.Point(13, 30);
            this.txtTiffFile.Name = "txtTiffFile";
            this.txtTiffFile.Size = new System.Drawing.Size(320, 20);
            this.txtTiffFile.TabIndex = 1;
            // 
            // cmdTiffFile
            // 
            this.cmdTiffFile.Location = new System.Drawing.Point(339, 29);
            this.cmdTiffFile.Name = "cmdTiffFile";
            this.cmdTiffFile.Size = new System.Drawing.Size(63, 23);
            this.cmdTiffFile.TabIndex = 2;
            this.cmdTiffFile.Text = "Browse";
            this.cmdTiffFile.UseVisualStyleBackColor = true;
            this.cmdTiffFile.Click += new System.EventHandler(this.cmdTiffFile_Click);
            // 
            // lblQRCodeText
            // 
            this.lblQRCodeText.AutoSize = true;
            this.lblQRCodeText.Location = new System.Drawing.Point(13, 57);
            this.lblQRCodeText.Name = "lblQRCodeText";
            this.lblQRCodeText.Size = new System.Drawing.Size(78, 13);
            this.lblQRCodeText.TabIndex = 3;
            this.lblQRCodeText.Text = "QR Code Text:";
            // 
            // txtQRCodeText
            // 
            this.txtQRCodeText.Location = new System.Drawing.Point(13, 73);
            this.txtQRCodeText.Multiline = true;
            this.txtQRCodeText.Name = "txtQRCodeText";
            this.txtQRCodeText.Size = new System.Drawing.Size(320, 69);
            this.txtQRCodeText.TabIndex = 4;
            // 
            // cmdEncode
            // 
            this.cmdEncode.Location = new System.Drawing.Point(339, 102);
            this.cmdEncode.Name = "cmdEncode";
            this.cmdEncode.Size = new System.Drawing.Size(63, 21);
            this.cmdEncode.TabIndex = 5;
            this.cmdEncode.Text = "Encode";
            this.cmdEncode.UseVisualStyleBackColor = true;
            this.cmdEncode.Click += new System.EventHandler(this.cmdEncode_Click);
            // 
            // picQRCode
            // 
            this.picQRCode.Location = new System.Drawing.Point(15, 168);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(317, 171);
            this.picQRCode.TabIndex = 6;
            this.picQRCode.TabStop = false;
            // 
            // cmdMerge
            // 
            this.cmdMerge.Location = new System.Drawing.Point(339, 224);
            this.cmdMerge.Name = "cmdMerge";
            this.cmdMerge.Size = new System.Drawing.Size(63, 21);
            this.cmdMerge.TabIndex = 7;
            this.cmdMerge.Text = "Merge";
            this.cmdMerge.UseVisualStyleBackColor = true;
            this.cmdMerge.Click += new System.EventHandler(this.cmdMerge_Click);
            // 
            // lblQRCode
            // 
            this.lblQRCode.AutoSize = true;
            this.lblQRCode.Location = new System.Drawing.Point(13, 150);
            this.lblQRCode.Name = "lblQRCode";
            this.lblQRCode.Size = new System.Drawing.Size(54, 13);
            this.lblQRCode.TabIndex = 8;
            this.lblQRCode.Text = "QR Code:";
            // 
            // cmdGenTiff
            // 
            this.cmdGenTiff.Location = new System.Drawing.Point(339, 195);
            this.cmdGenTiff.Name = "cmdGenTiff";
            this.cmdGenTiff.Size = new System.Drawing.Size(63, 23);
            this.cmdGenTiff.TabIndex = 9;
            this.cmdGenTiff.Text = "Gen TIFF";
            this.cmdGenTiff.UseVisualStyleBackColor = true;
            this.cmdGenTiff.Click += new System.EventHandler(this.cmdGenTiff_Click);
            // 
            // cboQRCodeSize
            // 
            this.cboQRCodeSize.FormattingEnabled = true;
            this.cboQRCodeSize.Items.AddRange(new object[] {
            "150",
            "300",
            "450",
            "600"});
            this.cboQRCodeSize.Location = new System.Drawing.Point(341, 75);
            this.cboQRCodeSize.Name = "cboQRCodeSize";
            this.cboQRCodeSize.Size = new System.Drawing.Size(60, 21);
            this.cboQRCodeSize.TabIndex = 10;
            this.cboQRCodeSize.Text = "600";
            // 
            // cboTiffResolution
            // 
            this.cboTiffResolution.FormattingEnabled = true;
            this.cboTiffResolution.Items.AddRange(new object[] {
            "96",
            "300",
            "600",
            "1200",
            "2400"});
            this.cboTiffResolution.Location = new System.Drawing.Point(339, 168);
            this.cboTiffResolution.Name = "cboTiffResolution";
            this.cboTiffResolution.Size = new System.Drawing.Size(63, 21);
            this.cboTiffResolution.TabIndex = 11;
            this.cboTiffResolution.Text = "2400";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 351);
            this.Controls.Add(this.cboTiffResolution);
            this.Controls.Add(this.cboQRCodeSize);
            this.Controls.Add(this.cmdGenTiff);
            this.Controls.Add(this.lblQRCode);
            this.Controls.Add(this.cmdMerge);
            this.Controls.Add(this.picQRCode);
            this.Controls.Add(this.cmdEncode);
            this.Controls.Add(this.txtQRCodeText);
            this.Controls.Add(this.lblQRCodeText);
            this.Controls.Add(this.cmdTiffFile);
            this.Controls.Add(this.txtTiffFile);
            this.Controls.Add(this.lblTiffFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "xFilm3.QRCoder";
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTiffFile;
        private System.Windows.Forms.TextBox txtTiffFile;
        private System.Windows.Forms.Button cmdTiffFile;
        private System.Windows.Forms.Label lblQRCodeText;
        private System.Windows.Forms.TextBox txtQRCodeText;
        private System.Windows.Forms.Button cmdEncode;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Button cmdMerge;
        private System.Windows.Forms.Label lblQRCode;
        private System.Windows.Forms.Button cmdGenTiff;
        private System.Windows.Forms.ComboBox cboQRCodeSize;
        private System.Windows.Forms.ComboBox cboTiffResolution;
    }
}

