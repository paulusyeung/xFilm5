using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.SpeedBox.Forms
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.lblUserName = new Gizmox.WebGUI.Forms.Label();
            this.txtUserName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblPassword = new Gizmox.WebGUI.Forms.Label();
            this.txtPassword = new Gizmox.WebGUI.Forms.TextBox();
            this.cmdCancel = new Gizmox.WebGUI.Forms.Button();
            this.cmdLogin = new Gizmox.WebGUI.Forms.Button();
            this.errorProvider = new Gizmox.WebGUI.Forms.ErrorProvider(this.components);
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUserName.Location = new System.Drawing.Point(20, 28);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(35, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(99, 25);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(199, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPassword.Location = new System.Drawing.Point(20, 54);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(35, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(99, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(199, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdCancel.Location = new System.Drawing.Point(68, 104);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdLogin
            // 
            this.cmdLogin.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdLogin.Location = new System.Drawing.Point(195, 104);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(75, 23);
            this.cmdLogin.TabIndex = 5;
            this.cmdLogin.Text = "OK";
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 3;
            // 
            // Login
            // 
            this.AcceptButton = this.cmdLogin;
            this.Controls.Add(this.cmdLogin);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(320, 160);
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private Label lblUserName;
        private TextBox txtUserName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button cmdCancel;
        private Button cmdLogin;
        private ErrorProvider errorProvider;
    }
}