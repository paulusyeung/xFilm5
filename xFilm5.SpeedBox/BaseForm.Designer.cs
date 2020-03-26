using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox
{
    partial class BaseForm
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
            this.pnlHeader = new Gizmox.WebGUI.Forms.Panel();
            this.pnlChild = new Gizmox.WebGUI.Forms.Panel();
            this.lblTitle = new Gizmox.WebGUI.Forms.Label();
            this.cmdMenu = new Gizmox.WebGUI.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.cmdMenu);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(412, 40);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlChild
            // 
            this.pnlChild.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlChild.Location = new System.Drawing.Point(0, 40);
            this.pnlChild.Name = "pnlChild";
            this.pnlChild.Size = new System.Drawing.Size(412, 360);
            this.pnlChild.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F);
            this.lblTitle.Location = new System.Drawing.Point(16, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(144, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Film";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdMenu
            // 
            this.cmdMenu.Location = new System.Drawing.Point(380, 2);
            this.cmdMenu.Name = "cmdMenu";
            this.cmdMenu.Size = new System.Drawing.Size(22, 22);
            this.cmdMenu.TabIndex = 2;
            this.cmdMenu.TabStop = false;
            this.cmdMenu.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            // 
            // BaseForm
            // 
            this.Controls.Add(this.pnlChild);
            this.Controls.Add(this.pnlHeader);
            this.Size = new System.Drawing.Size(412, 400);
            this.Text = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private Panel pnlHeader;
        private Panel pnlChild;
        private Label lblTitle;
        private Button cmdMenu;
    }
}