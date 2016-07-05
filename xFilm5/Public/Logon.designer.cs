namespace xFilm5.Public
{
    partial class Logon
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblUserName = new Gizmox.WebGUI.Forms.Label();
            this.lblPassword = new Gizmox.WebGUI.Forms.Label();
            this.errorProvider = new Gizmox.WebGUI.Forms.ErrorProvider(this.components);
            this.txtUserName = new Gizmox.WebGUI.Forms.TextBox();
            this.txtPassword = new Gizmox.WebGUI.Forms.TextBox();
            this.btnLogon = new Gizmox.WebGUI.Forms.Button();
            this.lblErrorMessage = new Gizmox.WebGUI.Forms.Label();
            this.lblVersionNumber = new Gizmox.WebGUI.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.lblUserName.Location = new System.Drawing.Point(112, 31);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(81, 17);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.TabStop = false;
            this.lblUserName.Text = "Logon User:";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.lblPassword.Location = new System.Drawing.Point(112, 54);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(81, 17);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.TabStop = false;
            this.lblPassword.Text = "Password:";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 3;
            this.errorProvider.BlinkStyle = Gizmox.WebGUI.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            this.errorProvider.DataMember = "";
            this.errorProvider.DataSource = "";
            this.errorProvider.Icon = null;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.txtUserName.Location = new System.Drawing.Point(199, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(200, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtUserName_EnterKeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.txtPassword.Location = new System.Drawing.Point(199, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtPassword_EnterKeyDown);
            // 
            // btnLogon
            // 
            this.btnLogon.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.btnLogon.Location = new System.Drawing.Point(199, 112);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(75, 23);
            this.btnLogon.TabIndex = 6;
            this.btnLogon.Text = "Logon";
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.lblErrorMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(112, 171);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(287, 66);
            this.lblErrorMessage.TabIndex = 7;
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblVersionNumber
            // 
            this.lblVersionNumber.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.Top;
            this.lblVersionNumber.Location = new System.Drawing.Point(196, 138);
            this.lblVersionNumber.Name = "lblVersionNumber";
            this.lblVersionNumber.Size = new System.Drawing.Size(100, 23);
            this.lblVersionNumber.TabIndex = 8;
            this.lblVersionNumber.Text = "[Version Number]";
            // 
            // Logon
            // 
            //this.AcceptButton = this.btnLogon;
            this.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.Controls.Add(this.lblVersionNumber);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnLogon);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.Size = new System.Drawing.Size(503, 282);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logon";
            this.Load += new System.EventHandler(this.Logon_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Label lblUserName;
        private Gizmox.WebGUI.Forms.Label lblPassword;
        private Gizmox.WebGUI.Forms.ErrorProvider errorProvider;
        private Gizmox.WebGUI.Forms.TextBox txtUserName;
        private Gizmox.WebGUI.Forms.TextBox txtPassword;
        private Gizmox.WebGUI.Forms.Button btnLogon;
        private Gizmox.WebGUI.Forms.Label lblErrorMessage;
        private Gizmox.WebGUI.Forms.Label lblVersionNumber;
    }
}