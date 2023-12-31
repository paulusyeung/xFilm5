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

namespace xFilm5.Admin.Staff
{
    public partial class StaffExRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _UserId = 0;
        private int _ClientId = 3;
        private string _UserName = String.Empty;

        public StaffExRecord()
        {
            InitializeComponent();
        }

        #region public properties
        public Common.Enums.EditMode EditMode
        {
            set
            {
                _EditMode = value;
            }
        }
        public int UserId
        {
            set
            {
                _UserId = value;
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
            ShowStaff();
            if (_EditMode == Common.Enums.EditMode.Edit)
            {
                ShowUser();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblStaffName.Text = oDict.GetWordWithColon("full_name");
            lblAddress.Text = oDict.GetWordWithColon("address");
            lblTel.Text = oDict.GetWordWithColon("tel");
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
            //toolTip1.SetToolTip(nudSecurityLevel, String.Format("Security Level:{0}2 = PrepressOp{0}3 = Supervisor{0}4 = Admin{0}5 = Power User{0}6 = Branch", Environment.NewLine));
            //toolTip1.SetToolTip(lblSecurityLevel, String.Format("Security Level:{0}2 = PrepressOp{0}3 = Supervisor{0}4 = Admin{0}5 = Power User{0}6 = Branch", Environment.NewLine));
        }

        private void SetDropdowns()
        {

        }
        #endregion

        #region ShowUser(), SaveUser(), VerifyUser(), DeleteUser()
        private void ShowStaff()
        {
            DAL.Client_User staff = DAL.Client_User.Load(_UserId);
            if (staff != null)
            {
                txtStaffName.Text = staff.FullName;
            }
        }

        private void ShowUser()
        {
            xFilm5.DAL.User user = xFilm5.DAL.User.Load(_UserId);
            if (user != null)
            {
                DAL.UserPreference pref = DAL.UserPreference.Load(_UserId);
                if (pref != null)
                {
                    txtAddress.Text = pref.GetMetadata("AddressEn");
                    txtAddressCht.Text = pref.GetMetadata("AddressCht");
                    txtAddressChs.Text = pref.GetMetadata("AddressChs");
                    txtTel.Text = pref.GetMetadata("Tel");
                    txtFax.Text = pref.GetMetadata("Fax");
                    txtXprinter.Text = pref.GetMetadata("Xprinter");
                }
            }
        }

        private bool SaveUser()
        {
            bool result = false;
            DAL.User user = null;
            DAL.UserPreference pref = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    user = new DAL.User();
                    user.CreatedOn = DateTime.Now;
                    user.CreatedBy = DAL.Common.Config.CurrentUserId;

                    pref = new DAL.UserPreference();
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    user = DAL.User.Load(_UserId);
                    pref = DAL.UserPreference.Load(_UserId);
                    break;
            }
            try
            {
                user.ModifiedOn = DateTime.Now;
                user.ModifiedBy = DAL.Common.Config.CurrentUserId;
                user.Save();

                pref.SetMetadata("AddressEn", txtAddress.Text.Trim());
                pref.SetMetadata("AddressCht", txtAddressCht.Text.Trim());
                pref.SetMetadata("AddressChs", txtAddressChs.Text.Trim());
                pref.SetMetadata("Tel", txtTel.Text.Trim());
                pref.SetMetadata("Fax", txtFax.Text.Trim());
                pref.SetMetadata("Xprinter", txtXprinter.Text.Trim());
                pref.Save();

                _UserId = user.UserId;
                _UserName = txtStaffName.Text;
                _EditMode = Common.Enums.EditMode.Edit;
                result = true;
            }
            catch { }

            return result;
        }

        private bool VerifyUser()
        {
            bool result = true;

            return result;
        }

        private bool DeleteUser()
        {
            bool result = false;

            DAL.User user = DAL.User.Load(_UserId);
            DAL.UserPreference pref = DAL.UserPreference.Load(_UserId);

            if (user != null)
            {
                if (user.Retired == true)
                {
                    pref.Delete();
                    user.Delete();
                }
                else
                {
                    user.Status = (int)DAL.Common.Enums.Status.Inactive;
                    user.Retired = true;
                    user.RetiredOn = DateTime.Now;
                    user.RetiredBy = DAL.Common.Config.CurrentUserId;
                    user.Save();
                }

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
                        MessageBox.Show("Save User?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save User And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "delete":
                        MessageBox.Show("Delete User?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
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
                    if (SaveUser())
                    {
                        MessageBox.Show(String.Format("User {0} is saved!", _UserName), "Save Result");
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
                    if (SaveUser())
                    {
                        MessageBox.Show(String.Format("User {0} is saved!", _UserName), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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
                    if (DeleteUser())
                    {
                        MessageBox.Show(String.Format("User {0} is deleted.", _UserName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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