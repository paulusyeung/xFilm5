using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.Controls.Upload
{
    partial class SpeedBox
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

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeedBox));
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.boxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkDotGain40 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDotGain43 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDotGain50 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkGreyscale = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkSpot2CMYK = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkBlackOverprint = new Gizmox.WebGUI.Forms.CheckBox();
            this.lblClient = new Gizmox.WebGUI.Forms.Label();
            this.cboClient = new Gizmox.WebGUI.Forms.ComboBox();
            this.txtClientName = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdClientInfo = new Gizmox.WebGUI.Forms.Button();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.boxOptions.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(126, 51);
            this.uploadBox.Name = "uploadBox";
            this.uploadBox.Size = new System.Drawing.Size(214, 139);
            this.uploadBox.TabIndex = 0;
            this.uploadBox.UploadMaxFileSize = ((long)(0));
            this.uploadBox.UploadMinFileSize = ((long)(0));
            this.uploadBox.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.boxOptions);
            this.topPanel.Controls.Add(this.chkGreyscale);
            this.topPanel.Controls.Add(this.chkSpot2CMYK);
            this.topPanel.Controls.Add(this.chkBlackOverprint);
            this.topPanel.Controls.Add(this.lblClient);
            this.topPanel.Controls.Add(this.cboClient);
            this.topPanel.Controls.Add(this.txtClientName);
            this.topPanel.Controls.Add(this.cmdClientInfo);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(480, 155);
            this.topPanel.TabIndex = 1;
            // 
            // boxOptions
            // 
            this.boxOptions.Controls.Add(this.chkDotGain40);
            this.boxOptions.Controls.Add(this.chkDotGain43);
            this.boxOptions.Controls.Add(this.chkDotGain50);
            this.boxOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxOptions.Location = new System.Drawing.Point(9, 90);
            this.boxOptions.Name = "boxOptions";
            this.boxOptions.Size = new System.Drawing.Size(457, 51);
            this.boxOptions.TabIndex = 25;
            this.boxOptions.TabStop = false;
            // 
            // chkDotGain40
            // 
            this.chkDotGain40.Location = new System.Drawing.Point(303, 17);
            this.chkDotGain40.Name = "chkDotGain40";
            this.chkDotGain40.Size = new System.Drawing.Size(144, 21);
            this.chkDotGain40.TabIndex = 21;
            this.chkDotGain40.Text = "Dot Gain 40%";
            this.chkDotGain40.Click += new System.EventHandler(this.chkDotGain40_Click);
            // 
            // chkDotGain43
            // 
            this.chkDotGain43.Location = new System.Drawing.Point(160, 17);
            this.chkDotGain43.Name = "chkDotGain43";
            this.chkDotGain43.Size = new System.Drawing.Size(133, 21);
            this.chkDotGain43.TabIndex = 21;
            this.chkDotGain43.Text = "Dot Gain 43%";
            this.chkDotGain43.Click += new System.EventHandler(this.chkDotGain43_Click);
            // 
            // chkDotGain50
            // 
            this.chkDotGain50.Location = new System.Drawing.Point(10, 17);
            this.chkDotGain50.Name = "chkDotGain50";
            this.chkDotGain50.Size = new System.Drawing.Size(144, 21);
            this.chkDotGain50.TabIndex = 21;
            this.chkDotGain50.Text = "Dot Gain 50%";
            this.chkDotGain50.Click += new System.EventHandler(this.chkDotGain50_Click);
            // 
            // chkGreyscale
            // 
            this.chkGreyscale.Location = new System.Drawing.Point(19, 66);
            this.chkGreyscale.Name = "chkGreyscale";
            this.chkGreyscale.Size = new System.Drawing.Size(110, 21);
            this.chkGreyscale.TabIndex = 21;
            this.chkGreyscale.Text = "Greyscale";
            this.chkGreyscale.Click += new System.EventHandler(this.chkGreyscale_Click);
            // 
            // chkSpot2CMYK
            // 
            this.chkSpot2CMYK.Location = new System.Drawing.Point(312, 66);
            this.chkSpot2CMYK.Name = "chkSpot2CMYK";
            this.chkSpot2CMYK.Size = new System.Drawing.Size(110, 21);
            this.chkSpot2CMYK.TabIndex = 23;
            this.chkSpot2CMYK.Text = "Spot to CMYK";
            this.chkSpot2CMYK.Click += new System.EventHandler(this.chkSpot2CMYK_Click);
            // 
            // chkBlackOverprint
            // 
            this.chkBlackOverprint.Location = new System.Drawing.Point(169, 66);
            this.chkBlackOverprint.Name = "chkBlackOverprint";
            this.chkBlackOverprint.Size = new System.Drawing.Size(104, 24);
            this.chkBlackOverprint.TabIndex = 22;
            this.chkBlackOverprint.Text = "Black Overprint";
            this.chkBlackOverprint.Click += new System.EventHandler(this.chkBlackOverprint_Click);
            // 
            // lblClient
            // 
            this.lblClient.Location = new System.Drawing.Point(12, 30);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(87, 21);
            this.lblClient.TabIndex = 0;
            this.lblClient.Text = "Client:";
            this.lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboClient
            // 
            this.cboClient.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboClient.Location = new System.Drawing.Point(114, 30);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(327, 21);
            this.cboClient.TabIndex = 1;
            this.cboClient.Visible = false;
            this.cboClient.SelectedIndexChanged += new System.EventHandler(this.cboClient_SelectedIndexChanged);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(114, 30);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(327, 20);
            this.txtClientName.TabIndex = 1;
            this.txtClientName.Visible = false;
            // 
            // cmdClientInfo
            // 
            this.cmdClientInfo.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdClientInfo.Image"));
            this.cmdClientInfo.Location = new System.Drawing.Point(444, 29);
            this.cmdClientInfo.Name = "cmdClientInfo";
            this.cmdClientInfo.Size = new System.Drawing.Size(24, 24);
            this.cmdClientInfo.TabIndex = 2;
            this.cmdClientInfo.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdClientInfo.Click += new System.EventHandler(this.cmdClientInfo_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uploadBox);
            this.bottomPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 155);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(480, 240);
            this.bottomPanel.TabIndex = 2;
            // 
            // SpeedBox
            // 
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(480, 395);
            this.Text = "Uploader";
            this.Load += new System.EventHandler(this.Uploader_Load);
            this.topPanel.ResumeLayout(false);
            this.boxOptions.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UploadControl uploadBox;
        private Panel topPanel;
        private Panel bottomPanel;
        private Label lblClient;
        private ComboBox cboClient;
        private TextBox txtClientName;
        private Button cmdClientInfo;
        private CheckBox chkGreyscale;
        private CheckBox chkSpot2CMYK;
        private CheckBox chkBlackOverprint;
        private GroupBox boxOptions;
        private CheckBox chkDotGain40;
        private CheckBox chkDotGain43;
        private CheckBox chkDotGain50;
    }
}