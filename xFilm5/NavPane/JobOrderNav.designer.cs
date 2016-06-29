namespace xFilm5.NavPane
{
    partial class JobOrderNav
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
            this.navJobOrder = new Gizmox.WebGUI.Forms.TreeView();
            this.SuspendLayout();
            // 
            // navJobOrder
            // 
            this.navJobOrder.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navJobOrder.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.None;
            this.navJobOrder.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.navJobOrder.Location = new System.Drawing.Point(0, 0);
            this.navJobOrder.Name = "navJobOrder";
            this.navJobOrder.ShowLines = false;
            this.navJobOrder.Size = new System.Drawing.Size(391, 306);
            this.navJobOrder.TabIndex = 0;
            this.navJobOrder.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.navInvt_AfterSelect);
            // 
            // JobOrderNav
            // 
            this.Controls.Add(this.navJobOrder);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Job Order";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.TreeView navJobOrder;


    }
}