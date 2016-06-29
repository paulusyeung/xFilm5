namespace xFilm5.Sales.PriceList
{
    partial class PriceListRecord
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspClientRecord = new Gizmox.WebGUI.Forms.Panel();
            this.cboDeptCode = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblDeptCode = new Gizmox.WebGUI.Forms.Label();
            this.txtUoM = new Gizmox.WebGUI.Forms.TextBox();
            this.lblUoM = new Gizmox.WebGUI.Forms.Label();
            this.txtUnitPrice = new Gizmox.WebGUI.Forms.TextBox();
            this.lblUnitPrice = new Gizmox.WebGUI.Forms.Label();
            this.txtItemName = new Gizmox.WebGUI.Forms.TextBox();
            this.lblItemName = new Gizmox.WebGUI.Forms.Label();
            this.txtItemCode = new Gizmox.WebGUI.Forms.TextBox();
            this.lblItemCode = new Gizmox.WebGUI.Forms.Label();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 0;
            // 
            // wspClientRecord
            // 
            this.wspClientRecord.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspClientRecord.Controls.Add(this.cboDeptCode);
            this.wspClientRecord.Controls.Add(this.lblDeptCode);
            this.wspClientRecord.Controls.Add(this.txtUoM);
            this.wspClientRecord.Controls.Add(this.lblUoM);
            this.wspClientRecord.Controls.Add(this.txtUnitPrice);
            this.wspClientRecord.Controls.Add(this.lblUnitPrice);
            this.wspClientRecord.Controls.Add(this.txtItemName);
            this.wspClientRecord.Controls.Add(this.lblItemName);
            this.wspClientRecord.Controls.Add(this.txtItemCode);
            this.wspClientRecord.Controls.Add(this.lblItemCode);
            this.wspClientRecord.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspClientRecord.DockPadding.All = 6;
            this.wspClientRecord.Location = new System.Drawing.Point(0, 28);
            this.wspClientRecord.Name = "wspClientRecord";
            this.wspClientRecord.Size = new System.Drawing.Size(405, 215);
            this.wspClientRecord.TabIndex = 1;
            // 
            // cboDeptCode
            // 
            this.cboDeptCode.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboDeptCode.Location = new System.Drawing.Point(98, 108);
            this.cboDeptCode.Name = "cboDeptCode";
            this.cboDeptCode.Size = new System.Drawing.Size(100, 21);
            this.cboDeptCode.TabIndex = 17;
            // 
            // lblDeptCode
            // 
            this.lblDeptCode.Location = new System.Drawing.Point(6, 108);
            this.lblDeptCode.Name = "lblDeptCode";
            this.lblDeptCode.Size = new System.Drawing.Size(90, 21);
            this.lblDeptCode.TabIndex = 16;
            this.lblDeptCode.Text = "Dept. Code:";
            // 
            // txtUoM
            // 
            this.txtUoM.Location = new System.Drawing.Point(98, 82);
            this.txtUoM.Name = "txtUoM";
            this.txtUoM.Size = new System.Drawing.Size(100, 20);
            this.txtUoM.TabIndex = 7;
            // 
            // lblUoM
            // 
            this.lblUoM.Location = new System.Drawing.Point(6, 82);
            this.lblUoM.Name = "lblUoM";
            this.lblUoM.Size = new System.Drawing.Size(90, 21);
            this.lblUoM.TabIndex = 6;
            this.lblUoM.Text = "Unit:";
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(98, 56);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(100, 20);
            this.txtUnitPrice.TabIndex = 5;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.Location = new System.Drawing.Point(6, 56);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(90, 21);
            this.lblUnitPrice.TabIndex = 4;
            this.lblUnitPrice.Text = "Unit Price:";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(98, 31);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(297, 20);
            this.txtItemName.TabIndex = 3;
            // 
            // lblItemName
            // 
            this.lblItemName.Location = new System.Drawing.Point(6, 31);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(90, 21);
            this.lblItemName.TabIndex = 2;
            this.lblItemName.Text = "Item Name:";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(98, 6);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(297, 20);
            this.txtItemCode.TabIndex = 1;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Location = new System.Drawing.Point(6, 6);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(90, 21);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "Item Code:";
            // 
            // PriceListRecord
            // 
            this.Controls.Add(this.wspClientRecord);
            this.Controls.Add(this.ansToolbar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(405, 243);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Record";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspClientRecord;
        private Gizmox.WebGUI.Forms.TextBox txtItemCode;
        private Gizmox.WebGUI.Forms.Label lblItemCode;
        private Gizmox.WebGUI.Forms.TextBox txtUoM;
        private Gizmox.WebGUI.Forms.Label lblUoM;
        private Gizmox.WebGUI.Forms.TextBox txtUnitPrice;
        private Gizmox.WebGUI.Forms.Label lblUnitPrice;
        private Gizmox.WebGUI.Forms.TextBox txtItemName;
        private Gizmox.WebGUI.Forms.Label lblItemName;
        private Gizmox.WebGUI.Forms.ComboBox cboDeptCode;
        private Gizmox.WebGUI.Forms.Label lblDeptCode;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;


    }
}