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

namespace xFilm5.Sales.Client
{
    public partial class ClientRecord : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _ClientId = 0;
        private int _ClientUserId = 0;
        private int _ClientAddressId = 0;

        public ClientRecord()
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
                return _ClientId;
            }
            set
            {
                _ClientId = value;
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
                ShowClient();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblClientId.Text = oDict.GetWordWithColon("client_code");
            lblClientName.Text = oDict.GetWordWithColon("client_name");
            lblAddress.Text = oDict.GetWordWithColon("address");
            lblTel.Text = oDict.GetWordWithColon("tel");
            lblFax.Text = oDict.GetWordWithColon("fax");
            lblContact.Text = oDict.GetWordWithColon("contact");
            lblMobile.Text = oDict.GetWordWithColon("mobile");
            lblSMS.Text = oDict.GetWordWithColon("sms");
            lblSmsLanguage.Text = oDict.GetWordWithColon("sms_language");
            lblEmail.Text = oDict.GetWordWithColon("email");
            lblPassword.Text = oDict.GetWordWithColon("password");
            lblBranch.Text = oDict.GetWordWithColon("branch");
            lblCreatedOn.Text = oDict.GetWordWithColon("created_on");
            lblCreditLimit.Text = oDict.GetWordWithColon("credit");
            lblDays.Text = oDict.GetWord("days");
            lblPaymentTerms.Text = oDict.GetWordWithColon("payment_terms");
            lblDiscount.Text = oDict.GetWord("percent_discount");
            lblPaymentType.Text = oDict.GetWordWithColon("payment_type");
            lblBilling.Text = oDict.GetWordWithColon("used_in_billing");
            lblPIN.Text = oDict.GetWordWithColon("pin");
            lblDropBox.Text = oDict.GetWordWithColon("used_in_dropbox");
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

            // cmdSuspend
            ToolBarButton cmdSuspend = new ToolBarButton("Suspend", oDict.GetWord("suspend"));
            cmdSuspend.Tag = "Suspend";
            cmdSuspend.Image = new IconResourceHandle("16x16.vcr_pause_dark_16.png");

            // cmdUnsuspend
            ToolBarButton cmdUnsuspend = new ToolBarButton("Unsuspend", oDict.GetWord("unsuspend"));
            cmdUnsuspend.Tag = "Unsuspend";
            cmdUnsuspend.Image = new IconResourceHandle("16x16.vcr_play_dark_16.png");

            // cmdVipPrice
            ToolBarButton cmdVipPrice = new ToolBarButton("VipPrice", oDict.GetWord("vip_price"));
            cmdVipPrice.Tag = "VipPrice";
            cmdVipPrice.Image = new IconResourceHandle("16x16.icons5_Color_VIP_16px.png");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                this.ansToolbar.Buttons.Add(cmdSave);
                this.ansToolbar.Buttons.Add(cmdSaveClose);
                if (_EditMode != Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdDelete);

                    if ((xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Account) ||
                        (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin))
                    {
                        if (xFilm5.Controls.Utility.Client.IsSuspended(_ClientId))
                        {
                            this.ansToolbar.Buttons.Add(cmdUnsuspend);
                        }
                        else
                        {
                            this.ansToolbar.Buttons.Add(cmdSuspend);
                        }
                    }
                    this.ansToolbar.Buttons.Add(cmdVipPrice);
                }
            }

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            if (_EditMode == Common.Enums.EditMode.Add)
            {
                txtClientId.Text = "To Be Generated";

                lblCreatedOn.Visible = false;
                txtCreatedOn.Visible = false;
            }
            txtPaymentTerms.Validator = new TextBoxValidation("String(value).match(/^[-]{0,1}[0-9]{1,9}$/)", "Bad value", "0-9");
            toolTip1.SetToolTip(txtPaymentTerms, "Numeric only");
        }

        private void SetDropdowns()
        {
            Client_User.LoadCombo(ref cboBranch, "FullName", false, false, "", "SecurityLevel = 6");
            Common.Data.LoadCombo_CreditLimmit(ref cboCreditLimit);
            Common.Data.LoadCombo_Language(ref cboSmsLanguage);
            T_PaymentType.LoadCombo(ref cboPaymentType, "Name", false);
        }
        #endregion

        #region ShowClient(), SaveClient(), VerifyClient(), DeleteClient()
        private void ShowClient()
        {
            int index = 0;
            xFilm5.DAL.Client oClient = xFilm5.DAL.Client.LoadWhere(String.Format("ID = {0}", _ClientId.ToString()));
            if (oClient != null)
            {
                Client_AddressBook oAddress = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", _ClientId.ToString()));
                Client_User oUser = Client_User.LoadWhere(String.Format("ClientID = {0} AND PrimaryUser = 1", _ClientId.ToString()));
                Client_User oBranch = Client_User.Load(oClient.Branch);
                T_PaymentType oPaymentType = T_PaymentType.Load(oClient.PaymentType);

                _ClientId = oClient.ID;
                txtClientId.Text = oClient.ID.ToString();
                txtClientName.Text = oClient.Name;

                if (oAddress != null)
                {
                    _ClientAddressId = oAddress.ID;

                    txtAddress.Text = oAddress.Address;
                    txtTel.Text = oAddress.Tel;
                    txtFax.Text = oAddress.Fax;
                    txtContact.Text = oAddress.ContactPerson;
                    txtMobile.Text = oAddress.Mobile;
                    txtSMS.Text = oAddress.SMS;
                    txtCreatedOn.Text = oAddress.CreatedOn.ToString("yyyy-MM-dd");

                    if (oAddress.SMS_Lang >= 0 && oAddress.SMS_Lang < cboSmsLanguage.Items.Count)
                    {
                        cboSmsLanguage.SelectedIndex = oAddress.SMS_Lang;
                    }
                }
                if (oUser != null)
                {
                    _ClientUserId = oUser.ID;

                    txtEmail.Text = oUser.Email;
                    txtPassword.Text = oUser.Password;
                }
                txtPaymentTerms.Text = oClient.PaymentTerms.ToString("#,##0.##");
                txtPIN.Text = oClient.PIN;

                if (oBranch != null)
                {
                    index = cboBranch.FindString(oBranch.FullName, 0);
                    if (index >= 0 && index < cboBranch.Items.Count)
                    {
                        cboBranch.SelectedIndex = index;
                    }
                }

                index = cboCreditLimit.FindString(oClient.PaymentTerms.ToString(), 0);
                if (index >= 0 && index < cboCreditLimit.Items.Count)
                {
                    cboCreditLimit.SelectedIndex = index;
                }

                if (oPaymentType != null)
                {
                    index = cboPaymentType.FindString(oPaymentType.Name, 0);
                    if (index >= 0 && index < cboPaymentType.Items.Count)
                    {
                        cboPaymentType.SelectedIndex = index;
                    }
                }
                
            }
        }

        private bool SaveClient()
        {
            bool result = false;
            xFilm5.DAL.Client client = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    client = new xFilm5.DAL.Client();
                    client.Status = (int)Common.Enums.Status.Active;
                    client.CreatedOn = DateTime.Now;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    client = xFilm5.DAL.Client.Load(_ClientId);
                    break;
            }
            try
            {
                client.Name = txtClientName.Text.Trim();
                client.CreditLimit = Convert.ToInt16(cboCreditLimit.SelectedItem.ToString());
                if (String.IsNullOrEmpty(txtPaymentTerms.Text.Trim()))
                {
                    client.PaymentTerms = 0;
                }
                else
                {
                    client.PaymentTerms = Convert.ToInt16(txtPaymentTerms.Text.Trim());
                }
                client.PaymentType = Convert.ToInt16(cboPaymentType.SelectedValue.ToString());
                client.PIN = txtPIN.Text.Trim();
                client.Branch = Convert.ToInt16(cboBranch.SelectedValue.ToString());
                client.Save();

                _ClientId = client.ID;
                if (SaveAddress() && SaveUser())
                {
                    result = true;
                }
            }
            catch { }

            return result;
        }

        private bool SaveAddress()
        {
            bool result = false;
            Client_AddressBook address = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    address = new xFilm5.DAL.Client_AddressBook();
                    address.ClientID = _ClientId;
                    address.PrimaryAddr = true;
                    address.CreatedOn = DateTime.Now;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    address = xFilm5.DAL.Client_AddressBook.Load(_ClientAddressId);
                    break;
            }
            try
            {
                address.Name = txtClientName.Text.Trim();
                address.Address = txtAddress.Text.Trim();
                address.Tel = txtTel.Text.Trim();
                address.Fax = txtFax.Text.Trim();
                address.ContactPerson = txtContact.Text.Trim();
                address.Mobile = txtMobile.Text.Trim();
                address.SMS = txtSMS.Text.Trim();
                address.SMS_Lang = cboSmsLanguage.SelectedIndex;
                address.Save();

                _ClientAddressId = address.ID;
                result = true;
            }
            catch { }

            return result;
        }

        private bool SaveUser()
        {
            bool result = false;
            Client_User user = null;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    user = new Client_User();
                    user.ClientID = _ClientId;
                    user.PrimaryUser = true;
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    user = xFilm5.DAL.Client_User.Load(_ClientUserId);
                    break;
            }
            try
            {
                user.FullName = txtContact.Text.Trim();
                user.Email = txtEmail.Text.Trim();
                user.Password = txtPassword.Text.Trim();
                user.SecurityLevel = Convert.ToInt16(DAL.Common.Enums.UserRole.Customer);
                user.Save();

                _ClientUserId = user.ID;
                result = true;
            }
            catch { }

            return result;
        }

        private bool VerifyClient()
        {
            bool result = true;

            return result;
        }

        private bool DeleteClient()
        {
            bool result = false;

            xFilm5.DAL.Client client = xFilm5.DAL.Client.Load(_ClientId);
            if (client != null)
            {
                client.Status = (int)Common.Enums.Status.Inactive;
                client.Save();

                result = true;
            }

            return result;
        }

        private bool SuspendClient()
        {
            bool result = false;

            xFilm5.DAL.Client client = xFilm5.DAL.Client.Load(_ClientId);
            if (client != null)
            {
                client.Status = (int)Common.Enums.Status.Inactive;
                client.Save();

                result = true;
            }

            return result;
        }

        private bool UnsuspendClient()
        {
            bool result = false;

            xFilm5.DAL.Client client = xFilm5.DAL.Client.Load(_ClientId);
            if (client != null)
            {
                client.Status = (int)Common.Enums.Status.Active;
                client.Save();

                result = true;
            }

            return result;
        }
        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show(String.Format("{0}?", oDict.GetWord("save")), "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show(String.Format("{0}?", oDict.GetWord("save_close")), "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "delete":
                        MessageBox.Show(String.Format("{0}?", oDict.GetWord("delete")), "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                    case "suspend":
                        MessageBox.Show(String.Format("{0}?", oDict.GetWord("suspend")), "Suspend Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSuspend_Click));
                        break;
                    case "unsuspend":
                        MessageBox.Show(String.Format("{0}?", oDict.GetWord("unsuspend")), "Unsuspend Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdUnsuspend_Click));
                        break;
                    case "vipprice":
                        Sales.VipPrice.Client2ProductList c2p = new VipPrice.Client2ProductList();
                        c2p.ClientId = _ClientId;
                        c2p.ShowDialog();
                        break;
                }
            }
        }

        #region ans Button Clicks: Save, SaveClose, Cancel, Retouch, Printing, Proofing, Ready, Dispatch, Completed
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveClient())
                    {
                        MessageBox.Show(String.Format("Client ID {0} is saved!", _ClientId.ToString()), "Save Result");
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
                    if (SaveClient())
                    {
                        MessageBox.Show(String.Format("Client ID {0} is saved!", _ClientId.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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
                    if (DeleteClient())
                    {
                        MessageBox.Show(String.Format("Client ID {0} is deleted.", _ClientId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the client status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdSuspend_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SuspendClient())
                    {
                        MessageBox.Show(String.Format("Client ID {0} is suspended.", _ClientId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the client status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdUnsuspend_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (UnsuspendClient())
                    {
                        MessageBox.Show(String.Format("Client ID {0} is unsuspended.", _ClientId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the client status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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