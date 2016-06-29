namespace xFilm5.Sales.PriceList
{
    partial class PriceListAts
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
            this.atsClient = new Gizmox.WebGUI.Forms.ToolBar();
            this.SuspendLayout();
            // 
            // atsClient
            // 
            this.atsClient.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.atsClient.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.atsClient.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.atsClient.DragHandle = true;
            this.atsClient.DropDownArrows = false;
            this.atsClient.ImageList = null;
            this.atsClient.Location = new System.Drawing.Point(0, 0);
            this.atsClient.MenuHandle = true;
            this.atsClient.Name = "atsClient";
            this.atsClient.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.atsClient.ShowToolTips = true;
            this.atsClient.TabIndex = 0;
            // 
            // ClientAts
            // 
            this.Controls.Add(this.atsClient);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "ClientAts";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar atsClient;


    }
}