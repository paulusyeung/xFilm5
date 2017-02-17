using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.Controls.Email
{
    partial class DN
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
            this.lblRecipient = new Gizmox.WebGUI.Forms.Label();
            this.txtRecipient = new Gizmox.WebGUI.Forms.TextBox();
            this.lblReceiptNumber = new Gizmox.WebGUI.Forms.Label();
            this.txtReceiptNumber = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdSendEmail = new Gizmox.WebGUI.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRecipient
            // 
            this.lblRecipient.Location = new System.Drawing.Point(12, 58);
            this.lblRecipient.Name = "lblRecipient";
            this.lblRecipient.Size = new System.Drawing.Size(339, 13);
            this.lblRecipient.TabIndex = 0;
            this.lblRecipient.Text = "Recipient: (user1@domain.com, user2@domain.com)";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Location = new System.Drawing.Point(12, 74);
            this.txtRecipient.Multiline = true;
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtRecipient.Size = new System.Drawing.Size(339, 118);
            this.txtRecipient.TabIndex = 1;
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.Location = new System.Drawing.Point(12, 17);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(339, 13);
            this.lblReceiptNumber.TabIndex = 0;
            this.lblReceiptNumber.Text = "Recipient Number:";
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.Location = new System.Drawing.Point(12, 33);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(339, 18);
            this.txtReceiptNumber.TabIndex = 2;
            // 
            // cmdSendEmail
            // 
            this.cmdSendEmail.Location = new System.Drawing.Point(183, 203);
            this.cmdSendEmail.Name = "cmdSendEmail";
            this.cmdSendEmail.Size = new System.Drawing.Size(168, 28);
            this.cmdSendEmail.TabIndex = 3;
            this.cmdSendEmail.Text = "Send Email";
            this.cmdSendEmail.Click += new System.EventHandler(this.cmdSendEmail_Click);
            // 
            // DN
            // 
            this.Controls.Add(this.cmdSendEmail);
            this.Controls.Add(this.txtReceiptNumber);
            this.Controls.Add(this.lblReceiptNumber);
            this.Controls.Add(this.txtRecipient);
            this.Controls.Add(this.lblRecipient);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(360, 240);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Delivery Note";
            this.Load += new System.EventHandler(this.DN_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Label lblRecipient;
        private TextBox txtRecipient;
        private Label lblReceiptNumber;
        private TextBox txtReceiptNumber;
        private Button cmdSendEmail;
    }
}