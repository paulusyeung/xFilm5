using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox.Forms
{
    partial class Theme
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
            this.cmdCancel = new Gizmox.WebGUI.Forms.Button();
            this.cmdOK = new Gizmox.WebGUI.Forms.Button();
            this.radLight = new Gizmox.WebGUI.Forms.RadioButton();
            this.radDark = new Gizmox.WebGUI.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdCancel.Location = new System.Drawing.Point(41, 95);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdOK.Location = new System.Drawing.Point(162, 95);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // radLight
            // 
            this.radLight.AutoSize = true;
            this.radLight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.radLight.Location = new System.Drawing.Point(51, 34);
            this.radLight.Name = "radLight";
            this.radLight.Size = new System.Drawing.Size(52, 18);
            this.radLight.TabIndex = 2;
            this.radLight.Text = "Light";
            // 
            // radDark
            // 
            this.radDark.AutoSize = true;
            this.radDark.Font = new System.Drawing.Font("Tahoma", 9F);
            this.radDark.Location = new System.Drawing.Point(171, 34);
            this.radDark.Name = "radDark";
            this.radDark.Size = new System.Drawing.Size(49, 18);
            this.radDark.TabIndex = 3;
            this.radDark.Text = "Dark";
            // 
            // Theme
            // 
            this.Controls.Add(this.radDark);
            this.Controls.Add(this.radLight);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(300, 160);
            this.Text = "Theme";
            this.Load += new System.EventHandler(this.Theme_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Button cmdCancel;
        private Button cmdOK;
        private RadioButton radLight;
        private RadioButton radDark;
    }
}