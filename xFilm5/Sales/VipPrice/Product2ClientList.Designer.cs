using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace xFilm5.Sales.VipPrice
{
    partial class Product2ClientList
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
            this.colClientName = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colLN = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colIAliasName = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colVipPrice = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colUnit = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colPricingId = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.lvwClientList = new Gizmox.WebGUI.Forms.ListView();
            this.colVipDiscount = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
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
            // colClientName
            // 
            this.colClientName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colClientName.Text = "Client Name";
            this.colClientName.Width = 140;
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
            // lvwClientList
            // 
            this.lvwClientList.AutoGenerateColumns = false;
            this.lvwClientList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colClientName,
            this.colLN,
            this.colIAliasName,
            this.colVipPrice,
            this.colVipDiscount,
            this.colUnit,
            this.colPricingId});
            this.lvwClientList.DataMember = null;
            this.lvwClientList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwClientList.GridLines = true;
            this.lvwClientList.ItemsPerPage = 500;
            this.lvwClientList.Location = new System.Drawing.Point(0, 42);
            this.lvwClientList.MultiSelect = false;
            this.lvwClientList.Name = "lvwClientList";
            this.lvwClientList.Size = new System.Drawing.Size(640, 438);
            this.lvwClientList.TabIndex = 3;
            // 
            // colVipDiscount
            // 
            this.colVipDiscount.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colVipDiscount.Text = "VIP Discount";
            this.colVipDiscount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colVipDiscount.Width = 80;
            // 
            // Product2ClientList
            // 
            this.Controls.Add(this.lvwClientList);
            this.Controls.Add(this.ansToolbar);
            this.Size = new System.Drawing.Size(640, 480);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIP Price";
            this.Load += new System.EventHandler(this.Product2ClientList_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private ToolBar ansToolbar;
        private ColumnHeader colClientName;
        private ColumnHeader colLN;
        private ColumnHeader colIAliasName;
        private ColumnHeader colVipPrice;
        private ColumnHeader colUnit;
        private ColumnHeader colPricingId;
        private ListView lvwClientList;
        private ToolTip toolTip1;
        private ColumnHeader colVipDiscount;
    }
}