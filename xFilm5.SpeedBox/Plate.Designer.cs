using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox
{
    partial class Plate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plate));
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.chkDotGain40 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkSpot2CMYK = new Gizmox.WebGUI.Forms.CheckBox();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.chkDotGain43 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDotGain50 = new Gizmox.WebGUI.Forms.CheckBox();
            this.boxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkGreyscale = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkBlackOverprint = new Gizmox.WebGUI.Forms.CheckBox();
            this.cmdMenu = new Gizmox.WebGUI.Forms.Button();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.lblTitle = new Gizmox.WebGUI.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.boxOptions.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(134, 168);
            this.uploadBox.Name = "uploadBox";
            this.uploadBox.Size = new System.Drawing.Size(214, 139);
            this.uploadBox.TabIndex = 0;
            this.uploadBox.UploadMaxFileSize = ((long)(0));
            this.uploadBox.UploadMinFileSize = ((long)(0));
            this.uploadBox.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
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
            // chkSpot2CMYK
            // 
            this.chkSpot2CMYK.Location = new System.Drawing.Point(312, 66);
            this.chkSpot2CMYK.Name = "chkSpot2CMYK";
            this.chkSpot2CMYK.Size = new System.Drawing.Size(110, 21);
            this.chkSpot2CMYK.TabIndex = 23;
            this.chkSpot2CMYK.Text = "Spot to CMYK";
            this.chkSpot2CMYK.Click += new System.EventHandler(this.chkSpot2CMYK_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uploadBox);
            this.bottomPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomPanel.Margin = new Gizmox.WebGUI.Forms.Padding(0, 150, 0, 0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(480, 400);
            this.bottomPanel.TabIndex = 2;
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
            // chkGreyscale
            // 
            this.chkGreyscale.Location = new System.Drawing.Point(19, 66);
            this.chkGreyscale.Name = "chkGreyscale";
            this.chkGreyscale.Size = new System.Drawing.Size(110, 21);
            this.chkGreyscale.TabIndex = 21;
            this.chkGreyscale.Text = "Greyscale";
            this.chkGreyscale.Click += new System.EventHandler(this.chkGreyscale_Click);
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
            // cmdMenu
            // 
            this.cmdMenu.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdMenu.Image"));
            this.cmdMenu.Location = new System.Drawing.Point(451, 2);
            this.cmdMenu.Name = "cmdMenu";
            this.cmdMenu.Size = new System.Drawing.Size(24, 24);
            this.cmdMenu.TabIndex = 2;
            this.cmdMenu.TabStop = false;
            this.cmdMenu.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.boxOptions);
            this.topPanel.Controls.Add(this.chkGreyscale);
            this.topPanel.Controls.Add(this.chkSpot2CMYK);
            this.topPanel.Controls.Add(this.chkBlackOverprint);
            this.topPanel.Controls.Add(this.cmdMenu);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(480, 150);
            this.topPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F);
            this.lblTitle.Location = new System.Drawing.Point(14, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(144, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Plate";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Plate
            // 
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Size = new System.Drawing.Size(480, 400);
            this.Text = "Plate";
            this.Load += new System.EventHandler(this.Plate_Load);
            this.bottomPanel.ResumeLayout(false);
            this.boxOptions.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private UploadControl uploadBox;
        private CheckBox chkDotGain40;
        private CheckBox chkSpot2CMYK;
        private Panel bottomPanel;
        private CheckBox chkDotGain43;
        private CheckBox chkDotGain50;
        private GroupBox boxOptions;
        private CheckBox chkGreyscale;
        private CheckBox chkBlackOverprint;
        private Button cmdMenu;
        private Panel topPanel;
        private Label lblTitle;
    }
}