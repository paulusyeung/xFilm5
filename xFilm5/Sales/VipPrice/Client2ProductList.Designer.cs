using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.Sales.VipPrice
{
    partial class Client2ProductList
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

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.colItemCode = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colLN = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colIAliasName = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colVipPrice = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colVipDiscount = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colUnit = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colPricingId = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.lvwProductList = new Gizmox.WebGUI.Forms.ListView();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = true;
            this.ansToolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.Size = new System.Drawing.Size(640, 42);
            this.ansToolbar.TabIndex = 0;
            // 
            // colItemCode
            // 
            this.colItemCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colItemCode.Text = "Item Code";
            this.colItemCode.Width = 140;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 30;
            // 
            // colIAliasName
            // 
            this.colIAliasName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colIAliasName.Text = "Alias Name";
            this.colIAliasName.Width = 200;
            // 
            // colVipPrice
            // 
            this.colVipPrice.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colVipPrice.Text = "VIP Price";
            this.colVipPrice.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colVipPrice.Width = 80;
            // 
            // colVipDiscount
            // 
            this.colVipDiscount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colVipDiscount.Text = "VIP Discount";
            this.colVipDiscount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colVipDiscount.Width = 80;
            // 
            // colUnit
            // 
            this.colUnit.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colUnit.Text = "Unit";
            this.colUnit.Width = 80;
            // 
            // colPricingId
            // 
            this.colPricingId.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPricingId.Text = "Pricng ID";
            this.colPricingId.Visible = false;
            this.colPricingId.Width = 100;
            // 
            // lvwProductList
            // 
            this.lvwProductList.AutoGenerateColumns = false;
            this.lvwProductList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colItemCode,
            this.colLN,
            this.colIAliasName,
            this.colVipPrice,
            this.colVipDiscount,
            this.colUnit,
            this.colPricingId});
            this.lvwProductList.DataMember = null;
            this.lvwProductList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwProductList.GridLines = true;
            this.lvwProductList.ItemsPerPage = 500;
            this.lvwProductList.Location = new System.Drawing.Point(0, 42);
            this.lvwProductList.MultiSelect = false;
            this.lvwProductList.Name = "lvwProductList";
            this.lvwProductList.Size = new System.Drawing.Size(640, 438);
            this.lvwProductList.TabIndex = 3;
            // 
            // Client2ProductList
            // 
            this.Controls.Add(this.lvwProductList);
            this.Controls.Add(this.ansToolbar);
            this.Size = new System.Drawing.Size(640, 480);
            this.Text = "VIP Price";
            this.Load += new System.EventHandler(this.Client2ProductList_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar ansToolbar;
        private ColumnHeader colItemCode;
        private ColumnHeader colLN;
        private ColumnHeader colIAliasName;
        private ColumnHeader colVipPrice;
        private ColumnHeader colVipDiscount;
        private ColumnHeader colUnit;
        private ColumnHeader colPricingId;
        private ListView lvwProductList;
        private ToolTip toolTip1;
    }
}