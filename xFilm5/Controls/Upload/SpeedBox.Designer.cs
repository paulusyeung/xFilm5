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
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.cmdClientInfo = new Gizmox.WebGUI.Forms.Button();
            this.txtClientName = new Gizmox.WebGUI.Forms.TextBox();
            this.cboClient = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblClient = new Gizmox.WebGUI.Forms.Label();
            this.topPanel.SuspendLayout();
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
            this.topPanel.Controls.Add(this.lblClient);
            this.topPanel.Controls.Add(this.cboClient);
            this.topPanel.Controls.Add(this.txtClientName);
            this.topPanel.Controls.Add(this.cmdClientInfo);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(480, 82);
            this.topPanel.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uploadBox);
            this.bottomPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 82);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(480, 238);
            this.bottomPanel.TabIndex = 2;
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
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(114, 30);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(327, 20);
            this.txtClientName.TabIndex = 1;
            this.txtClientName.Visible = false;
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
            // lblClient
            // 
            this.lblClient.Location = new System.Drawing.Point(12, 30);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(87, 21);
            this.lblClient.TabIndex = 0;
            this.lblClient.Text = "Client:";
            this.lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpeedBox
            // 
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(480, 320);
            this.Text = "Uploader";
            this.Load += new System.EventHandler(this.Uploader_Load);
            this.topPanel.ResumeLayout(false);
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
    }
}