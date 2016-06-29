namespace xFilm5.Accounting
{
    partial class AgingAts
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
            this.atsAging = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsAging
            // 
            this.atsAging.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsAging.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsAging.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsAging.DragHandle = true;
            this.atsAging.DropDownArrows = false;
            this.atsAging.ImageList = null;
            this.atsAging.Location = new System.Drawing.Point(0, 0);
            this.atsAging.MenuHandle = true;
            this.atsAging.Name = "atsAging";
            this.atsAging.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsAging.ShowToolTips = true;
            this.atsAging.TabIndex = 0;
            // 
            // AgingAts
            // 
            this.Controls.Add(this.atsAging);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "ClientAts";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsAging;


    }
}