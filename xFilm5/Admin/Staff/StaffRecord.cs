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

#endregion

namespace xFilm5.Admin.Staff
{
    public partial class StaffRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _UserId = 0;
        private int _ClientId = 3;
        private string _UserName = String.Empty;

        public StaffRecord()
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
            if (_EditMode == Common.Enums.EditMode.Edit)
            {
                Helper.UserHelper.Migrate2UserEx(_UserId);
                ShowUser();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblStaffName.Text = oDict.GetWordWithColon("full_name");
            lblEmail.Text = oDict.GetWordWithColon("email");
            lblPassword.Text = oDict.GetWordWithColon("password");
            lblSecurityLevel.Text = oDict.GetWordWithColon("security_level");
            lblBranch.Text = oDict.GetWordWithColon("branch");
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

            // cmdExtend
            ToolBarButton cmdExtend = new ToolBarButton("Extended", oDict.GetWord("extended_info"));
            cmdExtend.Tag = "Extended";
            cmdExtend.Image = new IconResourceHandle("16x16.mi_info_outline_16_blue.png");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                this.ansToolbar.Buttons.Add(cmdSave);
                this.ansToolbar.Buttons.Add(cmdSaveClose);
                if (_EditMode != Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdDelete);

                    // 2017.04.26 paulus: 暫時唔用
                    //ansToolbar.Buttons.Add(sep);
                    //ansToolbar.Buttons.Add(cmdExtend);
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
            Client_User.LoadCombo(ref cboBranch, "FullName", false, true, "HQ (總店)", "SecurityLevel = 6");
            xFilm5.DAL.Common.Data.LoadCombo_UserRole(ref cboUserRole);
        }
        #endregion

        #region ShowUser(), SaveUser(), VerifyUser(), DeleteUser()
        private void ShowUser()
        {
            int index = 0;
            xFilm5.DAL.Client_User user = xFilm5.DAL.Client_User.Load(_UserId);
            if (user != null)
            {
                txtStaffName.Text = user.FullName;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;

                #region User Role
                switch (user.SecurityLevel)
                {
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Customer:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Customer.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Operator:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Operator.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Sales:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Sales.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Account:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Account.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Admin:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Admin.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Workshop:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Workshop.ToString("g");
                        break;
                    case (int)xFilm5.DAL.Common.Enums.UserRole.Cashier:
                        cboUserRole.Text = xFilm5.DAL.Common.Enums.UserRole.Cashier.ToString("g");
                        break;
                }
                #endregion

                Client_User branch = Client_User.Load(user.Branch);
                if (branch != null)
                {
                    index = cboBranch.FindString(branch.FullName, 1);
                    if (index >= 0 && index < cboBranch.Items.Count)
                    {
                        cboBranch.SelectedIndex = index;
                    }
                    else
                    {
                        cboBranch.SelectedIndex = 0;
                    }
                }
                else
                {
                    cboBranch.SelectedIndex = 0;
                }
                _UserName = user.FullName;

                if (user.PrimaryUser)
                {
                    foreach (ToolBarButton cmd in ansToolbar.Buttons)
                    {
                        if (cmd.Tag.ToString().ToLower() == "delete")
                        {
                            cmd.Visible = false;
                        }
                    }
                }

                #region 2017.04.24 paulus: hide extended info button if not workshop
                if (user.SecurityLevel != (int)DAL.Common.Enums.UserRole.Workshop)
                {
                    for (int i = 0; i < ansToolbar.Buttons.Count; i++)
                    {
                        var item = ansToolbar.Buttons[i];
                        if (item.Style != ToolBarButtonStyle.Separator)
                        {
                            if (item.Tag.ToString().ToLower() == "extended")
                            {
                                ansToolbar.Buttons[i - 1].Visible = false;      // hide seperator
                                item.Visible = false;                           // hide extended info button
                            }
                        }
                    }
                }
                #endregion
            }
        }

        private bool SaveUser()
        {
            bool result = false;
            xFilm5.DAL.Client_User user = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    user = new xFilm5.DAL.Client_User();
                    user.ClientID = _ClientId;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    user = xFilm5.DAL.Client_User.Load(_UserId);
                    break;
            }
            try
            {
                user.FullName = txtStaffName.Text.Trim();
                user.Email = txtEmail.Text.Trim();
                user.Password = txtPassword.Text.Trim();
                user.SecurityLevel = Convert.ToInt16(cboUserRole.SelectedIndex + 1);
                user.Branch = (int)cboBranch.SelectedValue;

                user.Save();

                _UserId = user.ID;
                _UserName = user.FullName;
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

            xFilm5.DAL.Client_User user = xFilm5.DAL.Client_User.Load(_UserId);
            if (user != null)
            {
                user.Delete();

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
                    case "extended":
                        StaffExRecord ex = new StaffExRecord();
                        ex.UserId = _UserId;
                        ex.EditMode = Helper.UserHelper.IsUserExist(_UserId) ? Common.Enums.EditMode.Edit : Common.Enums.EditMode.Add;
                        ex.ShowDialog();
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