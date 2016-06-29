namespace xFilm5.AtsPane
{
    partial class AdminAts
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
            this.atsAdmin = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsAdmin
            // 
            this.atsAdmin.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsAdmin.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsAdmin.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsAdmin.DragHandle = true;
            this.atsAdmin.DropDownArrows = false;
            this.atsAdmin.ImageList = null;
            this.atsAdmin.Location = new System.Drawing.Point(0, 0);
            this.atsAdmin.MenuHandle = false;
            this.atsAdmin.Name = "atsAdmin";
            this.atsAdmin.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsAdmin.ShowToolTips = true;
            this.atsAdmin.TabIndex = 0;
            // 
            // AdminAts
            // 
            this.Controls.Add(this.atsAdmin);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsAdmin;


    }
}