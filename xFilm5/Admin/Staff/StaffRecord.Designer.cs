namespace xFilm5.Admin.Staff
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
            this.cboUserRole = new Gizmox.WebGUI.Forms.ComboBox();
            this.cboBranch = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblBranch = new Gizmox.WebGUI.Forms.Label();
            this.lblSecurityLevel = new Gizmox.WebGUI.Forms.Label();
            this.txtPassword = new Gizmox.WebGUI.Forms.TextBox();
            this.lblPassword = new Gizmox.WebGUI.Forms.Label();
            this.txtEmail = new Gizmox.WebGUI.Forms.TextBox();
            this.lblEmail = new Gizmox.WebGUI.Forms.Label();
            this.txtStaffName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblStaffName = new Gizmox.WebGUI.Forms.Label();
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
            this.wspClientRecord.Controls.Add(this.cboUserRole);
            this.wspClientRecord.Controls.Add(this.cboBranch);
            this.wspClientRecord.Controls.Add(this.lblBranch);
            this.wspClientRecord.Controls.Add(this.lblSecurityLevel);
            this.wspClientRecord.Controls.Add(this.txtPassword);
            this.wspClientRecord.Controls.Add(this.lblPassword);
            this.wspClientRecord.Controls.Add(this.txtEmail);
            this.wspClientRecord.Controls.Add(this.lblEmail);
            this.wspClientRecord.Controls.Add(this.txtStaffName);
            this.wspClientRecord.Controls.Add(this.lblStaffName);
            this.wspClientRecord.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspClientRecord.DockPadding.All = 6;
            this.wspClientRecord.Location = new System.Drawing.Point(0, 28);
            this.wspClientRecord.Name = "wspClientRecord";
            this.wspClientRecord.Size = new System.Drawing.Size(405, 215);
            this.wspClientRecord.TabIndex = 1;
            // 
            // cboUserRole
            // 
            this.cboUserRole.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboUserRole.Location = new System.Drawing.Point(98, 79);
            this.cboUserRole.Name = "cboUserRole";
            this.cboUserRole.Size = new System.Drawing.Size(100, 21);
            this.cboUserRole.TabIndex = 7;
            // 
            // cboBranch
            // 
            this.cboBranch.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboBranch.Location = new System.Drawing.Point(98, 105);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(100, 21);
            this.cboBranch.TabIndex = 9;
            // 
            // lblBranch
            // 
            this.lblBranch.Location = new System.Drawing.Point(6, 108);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(90, 21);
            this.lblBranch.TabIndex = 8;
            this.lblBranch.Text = "Branch:";
            // 
            // lblSecurityLevel
            // 
            this.lblSecurityLevel.Location = new System.Drawing.Point(6, 82);
            this.lblSecurityLevel.Name = "lblSecurityLevel";
            this.lblSecurityLevel.Size = new System.Drawing.Size(90, 21);
            this.lblSecurityLevel.TabIndex = 6;
            this.lblSecurityLevel.Text = "Security Level:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 56);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(6, 56);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(90, 21);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(98, 31);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(297, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(6, 31);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(90, 21);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // txtStaffName
            // 
            this.txtStaffName.Location = new System.Drawing.Point(98, 6);
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(297, 20);
            this.txtStaffName.TabIndex = 1;
            // 
            // lblStaffName
            // 
            this.lblStaffName.Location = new System.Drawing.Point(6, 6);
            this.lblStaffName.Name = "lblStaffName";
            this.lblStaffName.Size = new System.Drawing.Size(90, 21);
            this.lblStaffName.TabIndex = 0;
            this.lblStaffName.Text = "Staff Name:";
            // 
            // StaffRecord
            // 
            this.Controls.Add(this.wspClientRecord);
            this.Controls.Add(this.ansToolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(405, 243);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff Record";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspClientRecord;
        private Gizmox.WebGUI.Forms.TextBox txtStaffName;
        private Gizmox.WebGUI.Forms.Label lblStaffName;
        private Gizmox.WebGUI.Forms.Label lblSecurityLevel;
        private Gizmox.WebGUI.Forms.TextBox txtPassword;
        private Gizmox.WebGUI.Forms.Label lblPassword;
        private Gizmox.WebGUI.Forms.TextBox txtEmail;
        private Gizmox.WebGUI.Forms.Label lblEmail;
        private Gizmox.WebGUI.Forms.ComboBox cboBranch;
        private Gizmox.WebGUI.Forms.Label lblBranch;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.ComboBox cboUserRole;


    }
}