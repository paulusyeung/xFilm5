namespace xFilm5.Admin.Staff
{
    partial class StaffListAts
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
            this.atsStaff = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsStaff
            // 
            this.atsStaff.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsStaff.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsStaff.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsStaff.DragHandle = true;
            this.atsStaff.DropDownArrows = false;
            this.atsStaff.ImageList = null;
            this.atsStaff.Location = new System.Drawing.Point(0, 0);
            this.atsStaff.MenuHandle = true;
            this.atsStaff.Name = "atsStaff";
            this.atsStaff.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsStaff.ShowToolTips = true;
            this.atsStaff.TabIndex = 0;
            // 
            // StaffListAts
            // 
            this.Controls.Add(this.atsStaff);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "ClientAts";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsStaff;


    }
}