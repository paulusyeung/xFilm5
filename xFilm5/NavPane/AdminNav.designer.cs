namespace xFilm5.NavPane
{
    partial class AdminNav
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
            this.navAdmin = new Gizmox.WebGUI.Forms.TreeView();
            this.SuspendLayout();
            // 
            // navAdmin
            // 
            this.navAdmin.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navAdmin.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.None;
            this.navAdmin.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.navAdmin.Location = new System.Drawing.Point(0, 0);
            this.navAdmin.Name = "navAdmin";
            this.navAdmin.ShowLines = false;
            this.navAdmin.Size = new System.Drawing.Size(1276, 995);
            this.navAdmin.TabIndex = 0;
            this.navAdmin.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.navMemberMgmt_AfterSelect);
            // 
            // AdminNav
            // 
            this.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.Controls.Add(this.navAdmin);
            this.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.Size = new System.Drawing.Size(1276, 995);
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.TreeView navAdmin;


    }
}