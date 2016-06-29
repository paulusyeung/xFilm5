namespace xFilm5.NavPane
{
    partial class AccountingNav
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
            this.navAccounting = new Gizmox.WebGUI.Forms.TreeView();
            this.SuspendLayout();
            // 
            // navAccounting
            // 
            this.navAccounting.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navAccounting.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.None;
            this.navAccounting.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.navAccounting.Location = new System.Drawing.Point(0, 0);
            this.navAccounting.Name = "navAccounting";
            this.navAccounting.ShowLines = false;
            this.navAccounting.Size = new System.Drawing.Size(391, 306);
            this.navAccounting.TabIndex = 0;
            this.navAccounting.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.navPurchase_AfterSelect);
            // 
            // AccountingNav
            // 
            this.Controls.Add(this.navAccounting);
            this.Size = new System.Drawing.Size(391, 306);
            this.Text = "Accounting";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.TreeView navAccounting;


    }
}