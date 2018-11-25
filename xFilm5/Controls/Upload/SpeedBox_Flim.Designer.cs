using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.Controls.Upload
{
    partial class SpeedBox_Film
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeedBox_Film));
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.lblClient = new Gizmox.WebGUI.Forms.Label();
            this.cboClient = new Gizmox.WebGUI.Forms.ComboBox();
            this.txtClientName = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdClientInfo = new Gizmox.WebGUI.Forms.Button();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.chkEmulsionDown = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkEmulsionUp = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkNegative = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkPositive = new Gizmox.WebGUI.Forms.CheckBox();
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
            this.topPanel.Controls.Add(this.chkNegative);
            this.topPanel.Controls.Add(this.chkEmulsionDown);
            this.topPanel.Controls.Add(this.lblClient);
            this.topPanel.Controls.Add(this.cboClient);
            this.topPanel.Controls.Add(this.txtClientName);
            this.topPanel.Controls.Add(this.cmdClientInfo);
            this.topPanel.Controls.Add(this.chkEmulsionUp);
            this.topPanel.Controls.Add(this.chkPositive);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(480, 95);
            this.topPanel.TabIndex = 1;
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
            this.bottomPanel.Location = new System.Drawing.Point(0, 95);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(480, 225);
            this.bottomPanel.TabIndex = 2;
            // 
            // chkEmulsionDown
            // 
            this.chkEmulsionDown.Location = new System.Drawing.Point(348, 64);
            this.chkEmulsionDown.Name = "chkEmulsionDown";
            this.chkEmulsionDown.Size = new System.Drawing.Size(113, 21);
            this.chkEmulsionDown.TabIndex = 24;
            this.chkEmulsionDown.Text = "Emulsion Down";
            this.chkEmulsionDown.Click += new System.EventHandler(this.chkEmulsionDown_Click);
            // 
            // chkEmulsionUp
            // 
            this.chkEmulsionUp.Location = new System.Drawing.Point(247, 64);
            this.chkEmulsionUp.Name = "chkEmulsionUp";
            this.chkEmulsionUp.Size = new System.Drawing.Size(110, 21);
            this.chkEmulsionUp.TabIndex = 23;
            this.chkEmulsionUp.Text = "Emulsion Up";
            this.chkEmulsionUp.Click += new System.EventHandler(this.chkEmusionUp_Click);
            // 
            // chkNegative
            // 
            this.chkNegative.Location = new System.Drawing.Point(121, 64);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(104, 24);
            this.chkNegative.TabIndex = 22;
            this.chkNegative.Text = "Negative";
            this.chkNegative.Click += new System.EventHandler(this.chkNegative_Click);
            // 
            // chkPositive
            // 
            this.chkPositive.Location = new System.Drawing.Point(19, 64);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(110, 21);
            this.chkPositive.TabIndex = 21;
            this.chkPositive.Text = "Positive";
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_Click);
            // 
            // SpeedBox_Film
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
        private CheckBox chkNegative;
        private CheckBox chkEmulsionDown;
        private CheckBox chkEmulsionUp;
        private CheckBox chkPositive;
    }
}