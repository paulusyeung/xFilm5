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
using System.Linq;
using xFilm5.Helper;

#endregion

namespace xFilm5.Customer.Staff
{
    public partial class StaffRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _ItemId = 0;
        private string _ItemName = String.Empty;

        public StaffRecord()
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
                if (Helper.UserHelper.IsPrimaryUser(_ItemId))
                {
                    Helper.UserHelper.Migrate2UserEx(_ItemId);
                    gbxExtendedIInfo.Visible = true;
                }
                ShowItem();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblUserName.Text = oDict.GetWordWithColon("full_name");
            lblEmail.Text = oDict.GetWordWithColon("email");
            lblPassword.Text = oDict.GetWordWithColon("password");

            gbxExtendedIInfo.Text = oDict.GetWord("extended_info");
            gbxReceipt.Text = oDict.GetWord("receipt");
            lblRGrouping.Text = oDict.GetWordWithColon("receipt_grouping");
            lblRSmallFont.Text = oDict.GetWordWithColon("receipt_smallfont");
            lblRSlip.Text = oDict.GetWordWithColon("receipt_slip");
            lblREmail.Text = oDict.GetWordWithColon("receipt_email");
            gbxNotification.Text = oDict.GetWord("notification");
            lblNEmail.Text = oDict.GetWordWithColon("notification_email");
            lblNMobileApp.Text = oDict.GetWordWithColon("notification_mobileapp");
            lblNOnOrder.Text = oDict.GetWordWithColon("notification_onorder");
            lblNOnReady.Text = oDict.GetWordWithColon("notification_onready");

            lblEmails.Text = oDict.GetWordWithColon("email_recipient");
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
                    // 如果是 PrimaryUser 則不容許刪除
                    if (!(xFilm5.Controls.Utility.ClientUser.IsPrimary(_ItemId)))
                    {
                        this.ansToolbar.Buttons.Add(cmdDelete);
                    }
                }
            }

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            //toolTip1.SetToolTip(txtTel, "Numeric only");

            // default hidden, 當 primary user 時才顯示
            gbxExtendedIInfo.Visible = false;
        }

        private void SetDropdowns()
        {
        }
        #endregion

        #region ShowItem(), SaveItem(), VerifyItem(), DeleteItem()
        private void ShowItem()
        {
            xFilm5.DAL.Client_User item = xFilm5.DAL.Client_User.Load(_ItemId);
            if (item != null)
            {
                txtUserName.Text = item.FullName;
                txtEmail.Text = item.Email;
                txtPassword.Text = item.Password;

                _ItemId = item.ID;
                _ItemName = item.FullName;

                if (Helper.UserHelper.IsPrimaryUser(_ItemId))
                {
                    var p = new Helper.UserExHelper.Preferences();
                    var clientId = Helper.UserHelper.GetClientId(_ItemId);
                    p.Read(_ItemId);

                    chkRGrouping.Checked = p.Receipt.Grouping;
                    chkRSmallFont.Checked = p.Receipt.SmallFont;
                    chkRSlip.Checked = p.Receipt.Slip;
                    chkREmail.Checked = p.Receipt.Email;
                    chkNEmail.Checked = p.Notification.Email;
                    chkNOurApp.Checked = p.Notification.MobileApp;
                    chkOnOrder.Checked = p.Notification.OnOrder;
                    chkOnReady.Checked = p.Notification.OnReady;
                    txtEmails.Text = p.EmailRecipient;
                }
            }
        }

        private bool SaveItem()
        {
            bool result = false;
            xFilm5.DAL.Client_User item = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    item = new xFilm5.DAL.Client_User();
                    item.ClientID = xFilm5.Controls.Utility.User.GetClientId();
                    item.PrimaryUser = false;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    item = xFilm5.DAL.Client_User.Load(_ItemId);
                    break;
            }
            try
            {
                item.FullName = txtUserName.Text.Trim();
                item.Email = txtEmail.Text.Trim();
                item.Password = txtPassword.Text.Trim();
                item.SecurityLevel = Convert.ToInt16(DAL.Common.Enums.UserRole.Customer);

                item.Save();

                if (Helper.UserHelper.IsPrimaryUser(_ItemId)) UserEx_UpdRec();

                _ItemId = item.ID;
                _ItemName = item.FullName;
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

            xFilm5.DAL.Client_User item = xFilm5.DAL.Client_User.Load(_ItemId);
            if (item != null)
            {
                item.Delete();

                if (Helper.UserHelper.IsPrimaryUser(_ItemId)) UserEx_DelRec();

                result = true;
            }

            return result;
        }

        private void UserEx_UpdRec()
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                var cUser = ctx.Client_User.Where(x => x.ID == _ItemId).SingleOrDefault();
                var u = ctx.User.Where(x => x.UserId == _ItemId).SingleOrDefault();
                if (u != null)
                {
                    u.UserType = CommonHelper.Config.IamStaff ? (int)UserHelper.UserType.Staff : (int)UserHelper.UserType.Customer;

                    u.Alias = cUser.FullName;
                    u.LoginName = cUser.Email;
                    u.LoginPassword = cUser.Password;
                    u.ModifiedOn = DateTime.Now;
                    u.ModifiedBy = CommonHelper.Config.CurrentUserId;
                    ctx.SaveChanges();

                    var p = new Helper.UserExHelper.Preferences();
                    p.Receipt.Grouping = chkRGrouping.Checked;
                    p.Receipt.SmallFont = chkRSmallFont.Checked;
                    p.Receipt.Slip = chkRSlip.Checked;
                    p.Receipt.Email = chkREmail.Checked;
                    p.Notification.Email = chkNEmail.Checked;
                    p.Notification.MobileApp = chkNOurApp.Checked;
                    p.Notification.OnOrder = chkOnOrder.Checked;
                    p.Notification.OnReady = chkOnReady.Checked;
                    p.EmailRecipient = txtEmails.Text.Trim();
                    p.Save(_ItemId);
                }
            }
        }

        private void UserEx_DelRec()
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                var u = ctx.User.Where(x => x.UserId == _ItemId).SingleOrDefault();
                if (u != null)
                {
                    if (u.Retired)
                    {
                        var p = ctx.UserPreference.Where(x => x.UserId == _ItemId).Single();
                        ctx.UserPreference.Attach(p);
                        ctx.UserPreference.Remove(p);
                        ctx.User.Attach(u);
                        ctx.User.Remove(u);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        u.Status = (int)CommonHelper.Enums.Status.Inactive;
                        u.Retired = true;
                        u.RetiredOn = DateTime.Now;
                        u.RetiredBy = CommonHelper.Config.CurrentUserId;
                        ctx.SaveChanges();
                    }
                }
            }
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