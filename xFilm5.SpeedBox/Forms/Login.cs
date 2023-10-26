#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using xFilm5.EF6;

#endregion

namespace xFilm5.SpeedBox.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SetCaptions();
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            lblUserName.Text = oDict.GetWordWithColon("logon_user");
            lblPassword.Text = oDict.GetWordWithColon("password");
            cmdLogin.Text = oDict.GetWord("logon");
        }

        public bool Verify()
        {
            bool result = false;

            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "Cannot be blank!");
            }
            else
            {
                if (String.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider.SetError(txtPassword, "Cannot be blank!");
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        private bool AuthLogon()
        {
            bool varified = Verify();

            if (varified)
            {
                try
                {
                    using (var ctx = new xFilmEntities())
                    {
                        var user = ctx.Client_User.Where(x => x.Email == txtUserName.Text.Trim() && x.Password == txtPassword.Text.Trim()).SingleOrDefault();
                        if (user != null)
                        {
                            var client = ctx.Client.Where(x => x.ID == user.ClientID && x.Status >= 1).SingleOrDefault();
                            if (client != null)
                            {
                                #region Valid login, set environment defaults

                                this.Context.Session.IsLoggedOn = true;

                                Config.CurrentUserId = user.ID;

                                // The below code will logout the loggedin user when idle for the time specified
                                if (ConfigurationManager.AppSettings["sessionTimeout"] != null)
                                {
                                    this.Context.HttpContext.Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["sessionTimeout"]);
                                }

                                #endregion
                            }
                            else
                            {
                                // When client is retired, prompt user the error message.
                                // To Do: We can try to limited the times of attempt to 5 or less.
                                //MessageBox.Show("Access denied...Please try again!");
                                this.Context.Session.IsLoggedOn = false;
                            }
                        }
                        else
                        {
                            // When user inputs incorrect staff number or password, prompt user the error message.
                            // To Do: We can try to limited the times of attempt to 5 or less.
                            //MessageBox.Show("Incorrect User Name or Password...Please try again!");
                            this.Context.Session.IsLoggedOn = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Context.Session.IsLoggedOn = false;
                }
            }
            else
            {
                this.Context.Session.IsLoggedOn = false;
            }

            return this.Context.Session.IsLoggedOn;
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            if (AuthLogon())
            {
                Close();
            }
            else
            {
                MessageBox.Show("Login failed...Please try again");
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}