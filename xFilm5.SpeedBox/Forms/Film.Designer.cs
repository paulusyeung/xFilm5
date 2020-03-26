using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox.Forms
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkColorSeperation = new Gizmox.WebGUI.Forms.CheckBox();
            this.boxOptions = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkNegative = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkEmulsionUp = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkPositive = new Gizmox.WebGUI.Forms.CheckBox();
            this.uploadBox = new Gizmox.WebGUI.Forms.UploadControl();
            this.bottomPanel = new Gizmox.WebGUI.Forms.Panel();
            this.chkEmulsionDown = new Gizmox.WebGUI.Forms.CheckBox();
            this.topPanel = new Gizmox.WebGUI.Forms.Panel();
            this.boxOptions.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkColorSeperation
            // 
            this.chkColorSeperation.AutoSize = true;
            this.chkColorSeperation.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkColorSeperation.Location = new System.Drawing.Point(10, 17);
            this.chkColorSeperation.Name = "chkColorSeperation";
            this.chkColorSeperation.Size = new System.Drawing.Size(123, 18);
            this.chkColorSeperation.TabIndex = 21;
            this.chkColorSeperation.Text = "Colour Seperation";
            this.chkColorSeperation.Click += new System.EventHandler(this.chkColorSeperation_Click);
            // 
            // boxOptions
            // 
            this.boxOptions.Controls.Add(this.chkColorSeperation);
            this.boxOptions.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.boxOptions.Location = new System.Drawing.Point(8, 49);
            this.boxOptions.Name = "boxOptions";
            this.boxOptions.Size = new System.Drawing.Size(394, 50);
            this.boxOptions.TabIndex = 25;
            this.boxOptions.TabStop = false;
            // 
            // chkNegative
            // 
            this.chkNegative.AutoSize = true;
            this.chkNegative.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkNegative.Location = new System.Drawing.Point(107, 15);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(74, 18);
            this.chkNegative.TabIndex = 22;
            this.chkNegative.Text = "Negative";
            this.chkNegative.Click += new System.EventHandler(this.chkNegative_Click);
            // 
            // chkEmulsionUp
            // 
            this.chkEmulsionUp.AutoSize = true;
            this.chkEmulsionUp.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkEmulsionUp.Location = new System.Drawing.Point(195, 15);
            this.chkEmulsionUp.Name = "chkEmulsionUp";
            this.chkEmulsionUp.Size = new System.Drawing.Size(92, 18);
            this.chkEmulsionUp.TabIndex = 23;
            this.chkEmulsionUp.Text = "Emulsion Up";
            this.chkEmulsionUp.Click += new System.EventHandler(this.chkEmusionUp_Click);
            // 
            // chkPositive
            // 
            this.chkPositive.AutoSize = true;
            this.chkPositive.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkPositive.Location = new System.Drawing.Point(19, 15);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(67, 18);
            this.chkPositive.TabIndex = 21;
            this.chkPositive.Text = "Positive";
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_Click);
            // 
            // uploadBox
            // 
            this.uploadBox.Location = new System.Drawing.Point(107, 173);
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
            this.bottomPanel.Margin = new Gizmox.WebGUI.Forms.Padding(0, 110, 0, 0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(412, 370);
            this.bottomPanel.TabIndex = 2;
            // 
            // chkEmulsionDown
            // 
            this.chkEmulsionDown.AutoSize = true;
            this.chkEmulsionDown.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkEmulsionDown.Location = new System.Drawing.Point(291, 15);
            this.chkEmulsionDown.Name = "chkEmulsionDown";
            this.chkEmulsionDown.Size = new System.Drawing.Size(109, 18);
            this.chkEmulsionDown.TabIndex = 24;
            this.chkEmulsionDown.Text = "Emulsion Down";
            this.chkEmulsionDown.Click += new System.EventHandler(this.chkEmulsionDown_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.boxOptions);
            this.topPanel.Controls.Add(this.chkNegative);
            this.topPanel.Controls.Add(this.chkEmulsionDown);
            this.topPanel.Controls.Add(this.chkEmulsionUp);
            this.topPanel.Controls.Add(this.chkPositive);
            this.topPanel.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(412, 110);
            this.topPanel.TabIndex = 1;
            // 
            // Film
            // 
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Size = new System.Drawing.Size(412, 360);
            this.Text = "Film";
            this.Load += new System.EventHandler(this.Film_Load);
            this.boxOptions.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private CheckBox chkColorSeperation;
        private GroupBox boxOptions;
        private CheckBox chkNegative;
        private CheckBox chkEmulsionUp;
        private CheckBox chkPositive;
        private UploadControl uploadBox;
        private Panel bottomPanel;
        private CheckBox chkEmulsionDown;
        private Panel topPanel;
    }
}