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

namespace xFilm5.Sales.PriceList
{
    public partial class PriceListRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _ItemId = 0;
        private string _ItemCode = String.Empty;

        public PriceListRecord()
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
        public int ClientId
        {
            get
            {
                return _ItemId;
            }
            set
            {
                _ItemId = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            SetDropdowns();
            if (_EditMode == Common.Enums.EditMode.Edit)
            {
                ShowItem();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblItemCode .Text = oDict.GetWordWithColon("item_code");
            lblItemName.Text = oDict.GetWordWithColon("item_description");
            lblUnitPrice.Text = oDict.GetWordWithColon("unit_price");
            lblUoM.Text = oDict.GetWordWithColon("unit");
            lblDeptCode.Text = oDict.GetWordWithColon("category");
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
            T_BillingCode_Dept.LoadCombo(ref cboDeptCode, "Code", false);
        }
        #endregion

        #region ShowItem(), SaveItem(), VerifyItem(), DeleteItem()
        private void ShowItem()
        {
            int index = 0;
            xFilm5.DAL.T_BillingCode_Item item = xFilm5.DAL.T_BillingCode_Item.LoadWhere(String.Format("ID = {0} AND Retired = 0", _ItemId.ToString()));
            if (item != null)
            {
                txtItemCode.Text = item.ItemCode;
                txtItemName.Text = item.Name;
                txtUnitPrice.Text = item.UnitPrice.ToString("##0.00");
                txtUoM.Text = item.UoM;

                xFilm5.DAL.T_BillingCode_Dept dept = xFilm5.DAL.T_BillingCode_Dept.Load(item.DeptID);
                if (dept != null)
                {
                    index = cboDeptCode.FindString(dept.Code, 1);
                    if (index >= 0 && index < cboDeptCode.Items.Count)
                    {
                        cboDeptCode.SelectedIndex = index;
                    }
                }
                _ItemCode = item.ItemCode;
            }
        }

        private bool SaveItem()
        {
            bool result = false;
            xFilm5.DAL.T_BillingCode_Item item = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    item = new xFilm5.DAL.T_BillingCode_Item();
                    item.Retired = false;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    item = xFilm5.DAL.T_BillingCode_Item.Load(_ItemId);
                    break;
            }
            try
            {
                item.ItemCode = txtItemCode.Text.Trim();
                item.Name = txtItemName.Text.Trim();
                item.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                item.UoM = txtUoM.Text.Trim();

                xFilm5.DAL.T_BillingCode_Dept dept = xFilm5.DAL.T_BillingCode_Dept.Load(Convert.ToInt32(cboDeptCode.SelectedValue.ToString()));
                if (dept != null)
                {
                    item.DeptID = dept.ID;
                    item.GroupCode = dept.Code;
                }
                item.Save();

                _ItemId = item.ID;
                result = true;
            }
            catch { }

            return result;
        }

        private bool VerifyItem()
        {
            bool result = true;

            return result;
        }

        private bool DeleteItem()
        {
            bool result = false;

            xFilm5.DAL.T_BillingCode_Item item = xFilm5.DAL.T_BillingCode_Item.Load(_ItemId);
            if (item != null)
            {
                item.Retired = true;
                item.Save();

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
                    if (SaveItem())
                    {
                        MessageBox.Show(String.Format("Item Code {0} is saved!", _ItemCode), "Save Result");
                        if (_EditMode == Common.Enums.EditMode.Add)
                        {
                            _EditMode = Common.Enums.EditMode.Edit;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (SaveItem())
                    {
                        MessageBox.Show(String.Format("Item Code {0} is saved!", _ItemCode), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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