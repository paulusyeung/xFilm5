using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox.Forms
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.chkDotGain40 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkSpot2CMYK = new Gizmox.WebGUI.Forms.CheckBox();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.chkDotGain43 = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkDotGain50 = new Gizmox.WebGUI.Forms.CheckBox();
            this.boxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkGreyscale = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkBlackOverprint = new Gizmox.WebGUI.Forms.CheckBox();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.bottomPanel.SuspendLayout();
            this.boxOptions.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(105, 150);
            this.uploadBox.Name = "uploadBox";
            this.uploadBox.Size = new System.Drawing.Size(214, 139);
            this.uploadBox.TabIndex = 0;
            this.uploadBox.UploadMaxFileSize = ((long)(0));
            this.uploadBox.UploadMinFileSize = ((long)(0));
            this.uploadBox.UploadTempFilePath = "C:\\Users\\paulus\\AppData\\Local\\Temp\\";
            // 
            // chkDotGain40
            // 
            this.chkDotGain40.AutoSize = true;
            this.chkDotGain40.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkDotGain40.Location = new System.Drawing.Point(281, 17);
            this.chkDotGain40.Name = "chkDotGain40";
            this.chkDotGain40.Size = new System.Drawing.Size(103, 18);
            this.chkDotGain40.TabIndex = 21;
            this.chkDotGain40.Text = "Dot Gain 40%";
            this.chkDotGain40.Click += new System.EventHandler(this.chkDotGain40_Click);
            // 
            // chkSpot2CMYK
            // 
            this.chkSpot2CMYK.AutoSize = true;
            this.chkSpot2CMYK.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkSpot2CMYK.Location = new System.Drawing.Point(291, 17);
            this.chkSpot2CMYK.Name = "chkSpot2CMYK";
            this.chkSpot2CMYK.Size = new System.Drawing.Size(103, 18);
            this.chkSpot2CMYK.TabIndex = 23;
            this.chkSpot2CMYK.Text = "Spot to CMYK";
            this.chkSpot2CMYK.Click += new System.EventHandler(this.chkSpot2CMYK_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.uploadBox);
            this.bottomPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomPanel.Margin = new Gizmox.WebGUI.Forms.Padding(0, 110, 0, 0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(412, 360);
            this.bottomPanel.TabIndex = 2;
            // 
            // chkDotGain43
            // 
            this.chkDotGain43.AutoSize = true;
            this.chkDotGain43.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkDotGain43.Location = new System.Drawing.Point(147, 17);
            this.chkDotGain43.Name = "chkDotGain43";
            this.chkDotGain43.Size = new System.Drawing.Size(103, 18);
            this.chkDotGain43.TabIndex = 21;
            this.chkDotGain43.Text = "Dot Gain 43%";
            this.chkDotGain43.Click += new System.EventHandler(this.chkDotGain43_Click);
            // 
            // chkDotGain50
            // 
            this.chkDotGain50.AutoSize = true;
            this.chkDotGain50.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkDotGain50.Location = new System.Drawing.Point(10, 17);
            this.chkDotGain50.Name = "chkDotGain50";
            this.chkDotGain50.Size = new System.Drawing.Size(103, 18);
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
            this.boxOptions.Location = new System.Drawing.Point(8, 41);
            this.boxOptions.Name = "boxOptions";
            this.boxOptions.Size = new System.Drawing.Size(396, 51);
            this.boxOptions.TabIndex = 25;
            this.boxOptions.TabStop = false;
            // 
            // chkGreyscale
            // 
            this.chkGreyscale.AutoSize = true;
            this.chkGreyscale.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkGreyscale.Location = new System.Drawing.Point(20, 17);
            this.chkGreyscale.Name = "chkGreyscale";
            this.chkGreyscale.Size = new System.Drawing.Size(77, 18);
            this.chkGreyscale.TabIndex = 21;
            this.chkGreyscale.Text = "Greyscale";
            this.chkGreyscale.Click += new System.EventHandler(this.chkGreyscale_Click);
            // 
            // chkBlackOverprint
            // 
            this.chkBlackOverprint.AutoSize = true;
            this.chkBlackOverprint.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkBlackOverprint.Location = new System.Drawing.Point(155, 17);
            this.chkBlackOverprint.Name = "chkBlackOverprint";
            this.chkBlackOverprint.Size = new System.Drawing.Size(108, 18);
            this.chkBlackOverprint.TabIndex = 22;
            this.chkBlackOverprint.Text = "Black Overprint";
            this.chkBlackOverprint.Click += new System.EventHandler(this.chkBlackOverprint_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.boxOptions);
            this.topPanel.Controls.Add(this.chkGreyscale);
            this.topPanel.Controls.Add(this.chkSpot2CMYK);
            this.topPanel.Controls.Add(this.chkBlackOverprint);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(412, 110);
            this.topPanel.TabIndex = 1;
            // 
            // Plate
            // 
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Size = new System.Drawing.Size(412, 360);
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
        private Panel topPanel;
    }
}