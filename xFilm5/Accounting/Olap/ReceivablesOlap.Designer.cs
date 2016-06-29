namespace xFilm5.Accounting.Olap
{
    partial class ReceivablesOlap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceivablesOlap));
            this.olapBox = new Gizmox.WebGUI.Forms.Hosts.AspPageBox();
            this.mainPane = new Gizmox.WebGUI.Forms.Panel();
            this.lksPane = new Gizmox.WebGUI.Forms.Panel();
            this.lblCommonQuery = new Gizmox.WebGUI.Forms.Label();
            this.cboCommonQuery = new Gizmox.WebGUI.Forms.ComboBox();
            this.cmdLookup = new Gizmox.WebGUI.Forms.Button();
            this.txtLookup = new Gizmox.WebGUI.Forms.TextBox();
            this.lblLookup = new Gizmox.WebGUI.Forms.Label();
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.mainPane.SuspendLayout();
            this.lksPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // olapBox
            // 
            this.olapBox.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.olapBox.Location = new System.Drawing.Point(205, 46);
            this.olapBox.Name = "olapBox";
            this.olapBox.Path = null;
            this.olapBox.Size = new System.Drawing.Size(349, 341);
            this.olapBox.TabIndex = 0;
            // 
            // mainPane
            // 
            this.mainPane.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mainPane.Controls.Add(this.olapBox);
            this.mainPane.DockPadding.Top = 3;
            this.mainPane.Location = new System.Drawing.Point(98, 292);
            this.mainPane.Name = "mainPane";
            this.mainPane.Padding = new Gizmox.WebGUI.Forms.Padding(0, 3, 0, 0);
            this.mainPane.Size = new System.Drawing.Size(663, 345);
            this.mainPane.TabIndex = 1;
            // 
            // lksPane
            // 
            this.lksPane.Controls.Add(this.lblCommonQuery);
            this.lksPane.Controls.Add(this.cboCommonQuery);
            this.lksPane.Controls.Add(this.cmdLookup);
            this.lksPane.Controls.Add(this.txtLookup);
            this.lksPane.Controls.Add(this.lblLookup);
            this.lksPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.lksPane.Location = new System.Drawing.Point(0, 0);
            this.lksPane.Name = "lksPane";
            this.lksPane.Size = new System.Drawing.Size(1003, 34);
            this.lksPane.TabIndex = 0;
            // 
            // lblCommonQuery
            // 
            this.lblCommonQuery.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lblCommonQuery.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblCommonQuery.Location = new System.Drawing.Point(731, 9);
            this.lblCommonQuery.Name = "lblCommonQuery";
            this.lblCommonQuery.Size = new System.Drawing.Size(113, 18);
            this.lblCommonQuery.TabIndex = 6;
            this.lblCommonQuery.Text = "Common Query:";
            this.lblCommonQuery.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCommonQuery.Visible = false;
            // 
            // cboCommonQuery
            // 
            this.cboCommonQuery.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cboCommonQuery.Location = new System.Drawing.Point(852, 6);
            this.cboCommonQuery.Name = "cboCommonQuery";
            this.cboCommonQuery.Size = new System.Drawing.Size(150, 21);
            this.cboCommonQuery.TabIndex = 5;
            this.cboCommonQuery.Text = "comboBox1";
            this.cboCommonQuery.Visible = false;
            // 
            // cmdLookup
            // 
            this.cmdLookup.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdLookup.Image"));
            this.cmdLookup.Location = new System.Drawing.Point(183, 6);
            this.cmdLookup.Name = "cmdLookup";
            this.cmdLookup.Size = new System.Drawing.Size(22, 22);
            this.cmdLookup.TabIndex = 4;
            this.cmdLookup.TabStop = false;
            this.cmdLookup.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdLookup.Visible = false;
            // 
            // txtLookup
            // 
            this.txtLookup.Location = new System.Drawing.Point(63, 7);
            this.txtLookup.Name = "txtLookup";
            this.txtLookup.Size = new System.Drawing.Size(120, 20);
            this.txtLookup.TabIndex = 2;
            this.txtLookup.Visible = false;
            // 
            // lblLookup
            // 
            this.lblLookup.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblLookup.Location = new System.Drawing.Point(2, 9);
            this.lblLookup.Name = "lblLookup";
            this.lblLookup.Size = new System.Drawing.Size(61, 18);
            this.lblLookup.TabIndex = 0;
            this.lblLookup.Text = "Look for:";
            this.lblLookup.Visible = false;
            // 
            // ansToolbar
            // 
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = true;
            this.ansToolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.ansToolbar.Location = new System.Drawing.Point(0, 34);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.Size = new System.Drawing.Size(100, 22);
            this.ansToolbar.TabIndex = 5;
            // 
            // InvoiceStats
            // 
            this.Controls.Add(this.mainPane);
            this.Controls.Add(this.ansToolbar);
            this.Controls.Add(this.lksPane);
            this.Size = new System.Drawing.Size(1003, 734);
            this.Text = "OrderList_MasterDetail";
            this.mainPane.ResumeLayout(false);
            this.lksPane.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Hosts.AspPageBox olapBox;
        private Gizmox.WebGUI.Forms.Panel mainPane;
        private Gizmox.WebGUI.Forms.Panel lksPane;
        private Gizmox.WebGUI.Forms.Label lblCommonQuery;
        private Gizmox.WebGUI.Forms.ComboBox cboCommonQuery;
        private Gizmox.WebGUI.Forms.Button cmdLookup;
        private Gizmox.WebGUI.Forms.TextBox txtLookup;
        private Gizmox.WebGUI.Forms.Label lblLookup;
        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;

    }
}