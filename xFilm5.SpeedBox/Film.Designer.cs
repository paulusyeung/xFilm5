using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox
{
    partial class Film
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
            this.chkNegative = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkColorSeperation = new Gizmox.WebGUI.Forms.CheckBox();
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.boxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkEmulsionDown = new Gizmox.WebGUI.Forms.CheckBox();
            this.cmdMenu = new Gizmox.WebGUI.Forms.Button();
            this.chkEmulsionUp = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkPositive = new Gizmox.WebGUI.Forms.CheckBox();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.lblTitle = new Gizmox.WebGUI.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.boxOptions.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkNegative
            // 
            this.chkNegative.Location = new System.Drawing.Point(130, 62);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(104, 24);
            this.chkNegative.TabIndex = 22;
            this.chkNegative.Text = "Negative";
            this.chkNegative.Click += new System.EventHandler(this.chkNegative_Click);
            // 
            // chkColorSeperation
            // 
            this.chkColorSeperation.Location = new System.Drawing.Point(15, 17);
            this.chkColorSeperation.Name = "chkColorSeperation";
            this.chkColorSeperation.Size = new System.Drawing.Size(422, 21);
            this.chkColorSeperation.TabIndex = 21;
            this.chkColorSeperation.Text = "Colour Seperation";
            this.chkColorSeperation.Click += new System.EventHandler(this.chkColorSeperation_Click);
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(130, 204);
            this.uploadBox.Name = "uploadBox";
            this.uploadBox.Size = new System.Drawing.Size(214, 139);
            this.uploadBox.TabIndex = 0;
            this.uploadBox.UploadMaxFileSize = ((long)(0));
            this.uploadBox.UploadMinFileSize = ((long)(0));
            this.uploadBox.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uploadBox);
            this.bottomPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomPanel.Margin = new Gizmox.WebGUI.Forms.Padding(0, 160, 0, 0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(480, 400);
            this.bottomPanel.TabIndex = 2;
            // 
            // boxOptions
            // 
            this.boxOptions.Controls.Add(this.chkColorSeperation);
            this.boxOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxOptions.Location = new System.Drawing.Point(15, 98);
            this.boxOptions.Name = "boxOptions";
            this.boxOptions.Size = new System.Drawing.Size(452, 50);
            this.boxOptions.TabIndex = 25;
            this.boxOptions.TabStop = false;
            // 
            // chkEmulsionDown
            // 
            this.chkEmulsionDown.Location = new System.Drawing.Point(355, 64);
            this.chkEmulsionDown.Name = "chkEmulsionDown";
            this.chkEmulsionDown.Size = new System.Drawing.Size(112, 21);
            this.chkEmulsionDown.TabIndex = 24;
            this.chkEmulsionDown.Text = "Emulsion Down";
            this.chkEmulsionDown.Click += new System.EventHandler(this.chkEmulsionDown_Click);
            // 
            // cmdMenu
            // 
            this.cmdMenu.Location = new System.Drawing.Point(453, 2);
            this.cmdMenu.Name = "cmdMenu";
            this.cmdMenu.Size = new System.Drawing.Size(22, 22);
            this.cmdMenu.TabIndex = 2;
            this.cmdMenu.TabStop = false;
            this.cmdMenu.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdMenu.UseVisualStyleBackColor = true;
            // 
            // chkEmulsionUp
            // 
            this.chkEmulsionUp.Location = new System.Drawing.Point(239, 64);
            this.chkEmulsionUp.Name = "chkEmulsionUp";
            this.chkEmulsionUp.Size = new System.Drawing.Size(101, 21);
            this.chkEmulsionUp.TabIndex = 23;
            this.chkEmulsionUp.Text = "Emulsion Up";
            this.chkEmulsionUp.Click += new System.EventHandler(this.chkEmusionUp_Click);
            // 
            // chkPositive
            // 
            this.chkPositive.Location = new System.Drawing.Point(30, 64);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(90, 21);
            this.chkPositive.TabIndex = 21;
            this.chkPositive.Text = "Positive";
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.boxOptions);
            this.topPanel.Controls.Add(this.chkNegative);
            this.topPanel.Controls.Add(this.chkEmulsionDown);
            this.topPanel.Controls.Add(this.cmdMenu);
            this.topPanel.Controls.Add(this.chkEmulsionUp);
            this.topPanel.Controls.Add(this.chkPositive);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(480, 160);
            this.topPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F);
            this.lblTitle.Location = new System.Drawing.Point(25, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(144, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Film";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Film
            // 
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Size = new System.Drawing.Size(480, 400);
            this.Text = "Film";
            this.Load += new System.EventHandler(this.Film_Load);
            this.bottomPanel.ResumeLayout(false);
            this.boxOptions.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private CheckBox chkNegative;
        private CheckBox chkColorSeperation;
        private UploadControl uploadBox;
        private Panel bottomPanel;
        private GroupBox boxOptions;
        private CheckBox chkEmulsionDown;
        private Button cmdMenu;
        private CheckBox chkEmulsionUp;
        private CheckBox chkPositive;
        private Panel topPanel;
        private Label lblTitle;
    }
}