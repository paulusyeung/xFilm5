namespace xFilm5.AtsPane
{
    partial class JobOrderAts
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
            this.atsJobOrder = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsJobOrder
            // 
            this.atsJobOrder.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsJobOrder.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsJobOrder.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsJobOrder.DragHandle = true;
            this.atsJobOrder.DropDownArrows = false;
            this.atsJobOrder.ImageList = null;
            this.atsJobOrder.Location = new System.Drawing.Point(0, 0);
            this.atsJobOrder.MenuHandle = true;
            this.atsJobOrder.Name = "atsJobOrder";
            this.atsJobOrder.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsJobOrder.ShowToolTips = true;
            this.atsJobOrder.TabIndex = 0;
            // 
            // JobOrderAts
            // 
            this.Controls.Add(this.atsJobOrder);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Job Order";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsJobOrder;


    }
}