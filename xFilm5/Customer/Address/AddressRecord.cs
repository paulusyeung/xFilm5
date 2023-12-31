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

namespace xFilm5.Customer.Address
{
    public partial class AddressRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _ItemId = 0;
        private string _ItemName = String.Empty;

        public AddressRecord()
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

            lblAddressName.Text = oDict.GetWordWithColon("address_name");
            lblAddress.Text = oDict.GetWordWithColon("address");
            lblTel.Text = oDict.GetWordWithColon("tel");
            lblFax.Text = oDict.GetWordWithColon("fax");
            lblContact.Text = oDict.GetWordWithColon("contact");
            lblMobile.Text = oDict.GetWordWithColon("mobile");
            lblPager.Text = oDict.GetWordWithColon("pager");
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
                    // 如果是 PrimaryRec 則不容許刪除
                    if (!(xFilm5.Controls.Utility.ClientAddress.IsPrimary(_ItemId)))
                    {
                        this.ansToolbar.Buttons.Add(cmdDelete);
                    }
                }
            }

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            toolTip1.SetToolTip(txtTel, "Numeric only");
        }

        private void SetDropdowns()
        {
        }
        #endregion

        #region ShowItem(), SaveItem(), VerifyItem(), DeleteItem()
        private void ShowItem()
        {
            xFilm5.DAL.Client_AddressBook item = xFilm5.DAL.Client_AddressBook.Load(_ItemId);
            if (item != null)
            {
                txtAddressName.Text = item.Name;
                txtAddress.Text = item.Address;
                txtTel.Text = item.Tel;
                txtFax.Text = item.Fax;
                txtContact.Text = item.ContactPerson;
                txtMobile.Text = item.Mobile;
                txtPager.Text = item.Pager;

                _ItemId = item.ID;
                _ItemName = item.Name;
            }
        }

        private bool SaveItem()
        {
            bool result = false;
            xFilm5.DAL.Client_AddressBook item = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    item = new xFilm5.DAL.Client_AddressBook();
                    item.ClientID = xFilm5.Controls.Utility.User.GetClientId();
                    item.PrimaryAddr = false;
                    item.CreatedOn = DateTime.Now;
                    break;

                case (int)Common.Enums.EditMode.Edit:
                    item = xFilm5.DAL.Client_AddressBook.Load(_ItemId);

                    #region 如果是 PrimaryRec 把 Client.Name 也改
                    if (xFilm5.Controls.Utility.ClientAddress.IsPrimary(_ItemId))
                    {
                        DAL.Client client = DAL.Client.Load(item.ClientID);
                        if (client != null)
                        {
                            client.Name = item.Name;
                            client.Save();
                        }
                    }
                    #endregion
                    break;
            }
            try
            {
                item.Name = txtAddressName.Text.Trim();
                item.Address = txtAddress.Text.Trim();
                item.Tel = txtTel.Text.Trim();
                item.Fax = txtFax.Text.Trim();
                item.ContactPerson = txtContact.Text.Trim();
                item.Mobile = txtMobile.Text.Trim();
                item.Pager = txtPager.Text.Trim();

                item.Save();

                _ItemId = item.ID;
                _ItemName = item.Name;
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

            xFilm5.DAL.Client_AddressBook item = xFilm5.DAL.Client_AddressBook.Load(_ItemId);
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
                    if (SaveItem())
                    {
                        MessageBox.Show(String.Format("Item {0} is saved!", _ItemName), "Save Result");
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
                        MessageBox.Show(String.Format("Item {0} is saved!", _ItemName), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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
                        MessageBox.Show(String.Format("Item {0} is deleted.", _ItemName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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