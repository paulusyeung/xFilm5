#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;

#endregion

namespace xFilm5.Sales.VipPrice
{
    public partial class Product2ClientRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _PricingId = 0;
        private int _ProductId = 0;
        private string _ItemCode = String.Empty;

        public Product2ClientRecord()
        {
            InitializeComponent();
        }

        #region public properties
        public Common.Enums.EditMode EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }
        public int PricingId
        {
            get { return _PricingId; }
            set { _PricingId = value; }
        }
        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            SetDropdowns();

            if (_EditMode != Common.Enums.EditMode.Add)
            {
                ShowItem();
            }
            else
            {
                DAL.Client.LoadCombo(ref cboClient, "Name", false, false, "", "Status >= 1");
                ShowProductInfo();
            }
            SetFormLayout();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblItemCode .Text = oDict.GetWordWithColon("item_code");
            lblItemName.Text = oDict.GetWordWithColon("item_description");
            lblUnitPrice.Text = oDict.GetWordWithColon("unit_price");
            lblUoM.Text = oDict.GetWordWithColon("unit");
        }

        private void SetAnsToolbar()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", oDict.GetWord("save"));
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", System.Web.HttpUtility.UrlDecode(oDict.GetWord("save_close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                this.ansToolbar.Buttons.Add(cmdSave);
                this.ansToolbar.Buttons.Add(cmdSaveClose);
                if (_EditMode != Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdDelete);
                }
            }

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            toolTip1.SetToolTip(txtUnitPrice, "Numeric only");
        }

        private void SetDropdowns()
        {
            //T_BillingCode_Dept.LoadCombo(ref cboDeptCode, "Code", false);
        }

        private void SetFormLayout()
        {
            txtItemCode.ReadOnly = true;
            txtItemName.ReadOnly = true;
            txtUnitPrice.ReadOnly = true;
            txtUoM.ReadOnly = true;

            switch (_EditMode)
            {
                case Common.Enums.EditMode.Add:
                    cboClient.Visible = true;
                    txtClientName.Visible = false;
                    cboClient.Focus();
                    break;
                case Common.Enums.EditMode.Edit:
                    cboClient.Visible = false;
                    txtClientName.Visible = true;
                    txtClientName.ReadOnly = true;
                    txtAlias.Focus();
                    break;
                case Common.Enums.EditMode.Read:
                    txtAlias.ReadOnly = true;
                    txtVipPrice.ReadOnly = true;
                    txtVipDiscount.ReadOnly = true;
                    break;
            }
        }
        #endregion

        #region ShowItem(), SaveItem(), VerifyItem(), DeleteItem()
        private void ShowProductInfo()
        {
            DAL.T_BillingCode_Item item = DAL.T_BillingCode_Item.Load(_ProductId);
            if (item != null)
            {
                txtItemCode.Text = item.ItemCode;
                txtItemName.Text = item.Name;
                txtUnitPrice.Text = item.UnitPrice.ToString("#,##0.00");
                txtUoM.Text = item.UoM;

                if (_EditMode == Common.Enums.EditMode.Add)
                {
                    txtAlias.Text = item.Name;
                    txtVipPrice.Text = item.UnitPrice.ToString("##0.00");
                    txtVipDiscount.Text = "0";
                }
            }
        }

        private void ShowItem()
        {
            int index = 0;
            DAL.ClientPricing item = DAL.ClientPricing.Load(_PricingId);
            if (item != null)
            {
                DAL.Client client = DAL.Client.Load(item.ClientId);
                _ProductId = item.ItemId;

                ShowProductInfo();
                txtClientName.Text = client.Name;
                txtAlias.Text = item.Alias;
                txtVipPrice.Text = item.UnitPrice.ToString("##0.00");
                txtVipDiscount.Text = item.Discount.ToString("##0.00");
            }
        }

        private bool SaveItem()
        {
            bool result = false;
            Decimal vipPrice = 0, vipDiscount = 0;
            DAL.ClientPricing item = null;

            try
            {
                vipPrice = decimal.TryParse(txtVipPrice.Text.Trim().Replace(",", ""), out vipPrice) == true ? vipPrice : 0;
                vipDiscount = decimal.TryParse(txtVipDiscount.Text.Trim().Replace(",", ""), out vipDiscount) == true ? vipDiscount : 0;

                switch ((int)_EditMode)
                {
                    case (int)Common.Enums.EditMode.Add:
                        item = new DAL.ClientPricing();
                        item.ItemId = _ProductId;
                        item.ClientId = Convert.ToInt32(cboClient.SelectedValue.ToString());
                        item.Tag = txtItemCode.Text.Trim();
                        item.Status = (int)DAL.Common.Enums.Status.Active;
                        item.CreatedOn = DateTime.Now;
                        item.CreatedBy = DAL.Common.Config.CurrentUserId;
                        item.Retired = false;
                        break;
                    case (int)Common.Enums.EditMode.Edit:
                        item = DAL.ClientPricing.Load(_PricingId);
                        break;
                }

                item.Alias = txtAlias.Text.Trim();
                item.UnitPrice = vipPrice;
                item.Discount = vipDiscount;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = DAL.Common.Config.CurrentUserId;
                item.Save();

                result = true;
                _PricingId = item.PricingId;
            }
            catch { }

            return result;
        }

        private bool VerifyItem()
        {
            bool result = true;
            string msg = String.Empty;

            #region Check duplicate
            int clientId = Convert.ToInt32(cboClient.SelectedValue.ToString());
            String sql = String.Format("ClientId = {0} AND ItemId = {1}", clientId.ToString(), _ProductId.ToString());
            DAL.ClientPricing cp = DAL.ClientPricing.LoadWhere(sql);
            if (cp != null)
            {
                result = false;
                msg = msg + Environment.NewLine + "Duplicated record";
            }
            #endregion

            if (!result)
            {
                MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

        private bool DeleteItem()
        {
            bool result = false;

            DAL.ClientPricing item = DAL.ClientPricing.Load(_PricingId);
            if (item != null)
            {
                item.Delete();

                result = true;
            }

            return result;
        }
        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Item?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Item And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "delete":
                        MessageBox.Show("Delete Item?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                }
            }
        }

        #region ans Button Clicks: Save, SaveClose, Delete
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (VerifyItem())
                    {
                        if (SaveItem())
                        {
                            MessageBox.Show(String.Format("Item Code {0} is saved!", _ItemCode), "Save Result");
                            if (_EditMode == Common.Enums.EditMode.Add)
                            {
                                _EditMode = Common.Enums.EditMode.Edit;
                                txtClientName.Text = cboClient.SelectedItem.ToString();
                                SetFormLayout();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Record is ReadOnly...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdSaveClose_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (VerifyItem())
                    {
                        if (SaveItem())
                        {
                            MessageBox.Show(String.Format("Item Code {0} is saved!", _ItemCode), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                        }
                        else
                        {
                            MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (DeleteItem())
                    {
                        MessageBox.Show(String.Format("Item Code {0} is deleted.", _ItemCode), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the item status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdCloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}