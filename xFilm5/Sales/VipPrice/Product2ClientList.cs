#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using xFilm5.DAL;
using Gizmox.WebGUI.Common.Resources;
using System.Data.SqlClient;

#endregion

namespace xFilm5.Sales.VipPrice
{
    public partial class Product2ClientList : Form
    {
        private int _ProductId = 0;

        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        public Product2ClientList()
        {
            InitializeComponent();
        }

        private void Product2ClientList_Load(object sender, EventArgs e)
        {
            SetCaptions();
            SetAttribute();
            SetTheme();
            SetDefaultAns();
            BindVipPriceList();
        }

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            colClientName.Text = oDict.GetWord("client_name");
            colLN.Text = oDict.GetWord("ln");
            colIAliasName.Text = oDict.GetWord("alias");
            colVipPrice.Text = oDict.GetWord("vip_price");
            colVipDiscount.Text = oDict.GetWord("vip_discount") + " (%)";
            colUnit.Text = oDict.GetWord("unit");
        }

        private void SetAttribute()
        {
            this.lvwClientList.ListViewItemSorter = new ListViewItemSorter(this.lvwClientList);

            //toolTip1.SetToolTip(txtLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
            //toolTip1.SetToolTip(cmdLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
            toolTip1.SetToolTip(lvwClientList, "Double click to open Item record");
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }
        #endregion

        #region Set Action Strip
        private void SetDefaultAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.Height = 22;
            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdAddNew
            ToolBarButton cmdAddNew = new ToolBarButton("AddNew", oDict.GetWord("addnew"));
            cmdAddNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdAddNew.Tag = "AddNew";

            this.ansToolbar.Buttons.Add(cmdAddNew);
            #endregion

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");
            cmdPopup.Tag = "popup";

            this.ansToolbar.Buttons.Add(cmdPopup);
            #endregion

            ansToolbar.ButtonClick += ansToolbar_ButtonClick;
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "addnew":
                        Product2ClientRecord p2cRecAddNew = new Product2ClientRecord();
                        p2cRecAddNew.EditMode = Common.Enums.EditMode.Add;
                        p2cRecAddNew.ProductId = _ProductId;
                        p2cRecAddNew.ShowDialog();
                        break;
                    case "popup":
                        ShowRecord();
                        break;
                }
            }
        }
        #endregion

        #region Bind Job Order List
        private void BindVipPriceList()
        {
            this.lvwClientList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                ListViewItem objItem = this.lvwClientList.Items.Add(reader.GetString(1));  // Client Name
                #region Icon
                objItem.SmallImage = new IconResourceHandle("16x16.group.png");
                objItem.LargeImage = new IconResourceHandle("Icons.32x32.client_32x32.png");
                #endregion
                //objItem.SubItems.Add(reader.GetString(1));                          // Client Name
                objItem.SubItems.Add(iCount.ToString());                            // Line Number
                objItem.SubItems.Add(reader.GetString(11));                         // Alias Name
                objItem.SubItems.Add(reader.GetDecimal(13).ToString("#,##0.00"));   // VIP Price
                objItem.SubItems.Add(reader.GetDecimal(14).ToString("#,##0.00"));   // VIP Discount
                objItem.SubItems.Add(reader.GetString(8));                          // Unit
                objItem.SubItems.Add(reader.GetInt32(10).ToString());               // Pricing Id

                iCount++;
            }
            reader.Close();
        }

        private string BuildSqlQueryString()
        {
            String sql = String.Format(@"
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ClientId]
      ,[ClientName]
      ,[ClientStatus]
      ,[ProductId]
      ,[ProductCode]
      ,[GroupCode]              -- 5
      ,[ProductName]
      ,[UnitPrice]
      ,[Unit]
      ,[ProductRetiered]
      ,[PricingId]              -- 10
      ,[Alias]
      ,[Tag]
      ,[VipPrice]
      ,[VipDiscount]
      ,[PricingStatus]          -- 15
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[Retired]                -- 20
FROM [dbo].[vwVipPriceList]
WHERE [ProductId] = {0}
ORDER BY [ClientName]
", _ProductId.ToString());

            return sql;
        }
        #endregion

        private void ShowRecord()
        {
            if (lvwClientList.SelectedItem != null)
            {
                int itemId = Convert.ToInt32(lvwClientList.SelectedItem.SubItems[colPricingId.Index].Text);

                Product2ClientRecord p2cRecEdit = new Product2ClientRecord();
                p2cRecEdit.EditMode = Common.Enums.EditMode.Edit;
                p2cRecEdit.PricingId = itemId;
                p2cRecEdit.ShowDialog();
            }
        }

        private void lvwClientList_DoubleClick(object sender, EventArgs e)
        {
            ShowRecord();
        }
    }
}