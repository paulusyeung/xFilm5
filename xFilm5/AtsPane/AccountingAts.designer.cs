namespace xFilm5.AtsPane
{
    partial class AccountingAts
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
            this.atsAccounting = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsAccounting
            // 
            this.atsAccounting.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsAccounting.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsAccounting.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsAccounting.DragHandle = true;
            this.atsAccounting.DropDownArrows = false;
            this.atsAccounting.ImageList = null;
            this.atsAccounting.Location = new System.Drawing.Point(0, 0);
            this.atsAccounting.MenuHandle = true;
            this.atsAccounting.Name = "atsAccounting";
            this.atsAccounting.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsAccounting.ShowToolTips = true;
            this.atsAccounting.TabIndex = 0;
            // 
            // AccountingAts
            // 
            this.Controls.Add(this.atsAccounting);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Accounting";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsAccounting;


    }
}