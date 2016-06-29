using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Web.Security;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Authentication;
using Gizmox.WebGUI.Common.Interfaces;

using xFilm5.DAL;

namespace xFilm5.Public
{
    public partial class Logon : LogonForm
    {
        public Logon()
        {
            InitializeComponent();
            SetCaptions();
            VersionNumber();

#if (DEBUG)
            txtUserName.Text = "paulus@nustar.com";
            txtPassword.Text = "py9546";
#endif
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(xFilm5.DAL.Common.Config.CurrentWordDict, xFilm5.DAL.Common.Config.CurrentLanguageId);

            lblUserName.Text = oDict.GetWordWithColon("logon_user");
            lblPassword.Text = oDict.GetWordWithColon("password");
            btnLogon.Text = oDict.GetWord("logon");
        }

        private void VersionNumber()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.lblVersionNumber.Text = version.ToString();
        }

        public bool Verify()
        {
            bool result = true;
            if (txtUserName.Text.Length == 0)
            {
                errorProvider.SetError(txtUserName, "Cannot be blank!");
            }
            else
            {
                errorProvider.SetError(txtUserName, string.Empty);
                result = result & true;
            }

            if (txtPassword.Text.Length == 0)
            {
                errorProvider.SetError(txtPassword, "Cannot be blank!");
            }
            else
            {
                errorProvider.SetError(txtPassword, string.Empty);
                result = result & true;
            }

            return result;
        }

        private bool AuthLogon()
        {
            if (Verify())
            {
                string sql = "Email = '" + txtUserName.Text.Trim().Replace("'", "") + "' AND Password = '" + txtPassword.Text.Trim().Replace("'", "") + "'";
                xFilm5.DAL.Client_User oUser = xFilm5.DAL.Client_User.LoadWhere(sql);
                if (oUser != null)
                {
                    Client oClient = Client.LoadWhere(String.Format("ID = {0} AND Status >= 1", oUser.ClientID.ToString()));
                    if (oClient != null)
                    {
                        this.Context.Session.IsLoggedOn = true;

                        Common.Config.CurrentUserId = oUser.ID;

                        // The below code will logout the loggedin user when idle for the time specified
                        if (ConfigurationManager.AppSettings["sessionTimeout"] != null)
                        {
                            this.Context.HttpContext.Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["sessionTimeout"]);
                        }

                        // 2010.05.18 paulus: redirect
                        if (xFilm5.Controls.Utility.User.IsClient(oUser.ID))
                        {
                            VWGContext.Current.Transfer(new Customer.Desktop());
                        }
                        else
                        {
                            if (xFilm5.Controls.Utility.User.IsCashier(oUser.ID))
                            {
                                VWGContext.Current.Transfer(new Cashier.Desktop());
                            }
                            else
                            {
                                if (xFilm5.Controls.Utility.User.IsStaff(oUser.ID))
                                {
                                    VWGContext.Current.Transfer(new Desktop());
                                }
                            }
                        }
                    }
                    else
                    {
                        // When client is retired, prompt user the error message.
                        // To Do: We can try to limited the times of attempt to 5 or less.
                        this.lblErrorMessage.Text = "Access denied...Please try again!";
                        this.Context.Session.IsLoggedOn = false;
                    }
                }
                else
                {
                    // When user inputs incorrect staff number or password, prompt user the error message.
                    // To Do: We can try to limited the times of attempt to 5 or less.
                    this.lblErrorMessage.Text = "Incorrect User Name or Password...Please try again!";
                    this.Context.Session.IsLoggedOn = false;
                }
            }
            else
            {
                this.Context.Session.IsLoggedOn = false;
            }

            return this.Context.Session.IsLoggedOn;
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            DoLogon();
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void txtPassword_EnterKeyDown(object sender, KeyEventArgs e)
        {
            DoLogon();
        }

        private void txtUserName_EnterKeyDown(object sender, KeyEventArgs e)
        {
            DoLogon();
        }

        private void DoLogon()
        {
            if (AuthLogon())
            {
                // Close the Logon form
                this.Close();
            }
        }
    }
}