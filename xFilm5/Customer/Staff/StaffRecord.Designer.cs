namespace xFilm5.Customer.Staff
{
    partial class StaffRecord
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
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspClientRecord = new Gizmox.WebGUI.Forms.Panel();
            this.lblPassword = new Gizmox.WebGUI.Forms.Label();
            this.txtPassword = new Gizmox.WebGUI.Forms.TextBox();
            this.txtEmail = new Gizmox.WebGUI.Forms.TextBox();
            this.lblEmail = new Gizmox.WebGUI.Forms.Label();
            this.txtUserName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblUserName = new Gizmox.WebGUI.Forms.Label();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 0;
            // 
            // wspClientRecord
            // 
            this.wspClientRecord.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspClientRecord.Controls.Add(this.lblPassword);
            this.wspClientRecord.Controls.Add(this.txtPassword);
            this.wspClientRecord.Controls.Add(this.txtEmail);
            this.wspClientRecord.Controls.Add(this.lblEmail);
            this.wspClientRecord.Controls.Add(this.txtUserName);
            this.wspClientRecord.Controls.Add(this.lblUserName);
            this.wspClientRecord.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspClientRecord.DockPadding.All = 6;
            this.wspClientRecord.Location = new System.Drawing.Point(0, 28);
            this.wspClientRecord.Name = "wspClientRecord";
            this.wspClientRecord.Size = new System.Drawing.Size(405, 260);
            this.wspClientRecord.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(6, 58);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(90, 21);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 58);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(98, 32);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(297, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(6, 32);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(90, 21);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(98, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(297, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(6, 6);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(90, 21);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name:";
            // 
            // StaffRecord
            // 
            this.Controls.Add(this.wspClientRecord);
            this.Controls.Add(this.ansToolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(405, 288);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Record";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspClientRecord;
        private Gizmox.WebGUI.Forms.TextBox txtUserName;
        private Gizmox.WebGUI.Forms.Label lblUserName;
        private Gizmox.WebGUI.Forms.Label lblEmail;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.Label lblPassword;
        private Gizmox.WebGUI.Forms.TextBox txtPassword;
        private Gizmox.WebGUI.Forms.TextBox txtEmail;


    }
}