#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;
using xFilm5.Helper;

#endregion

namespace xFilm5.Admin
{
    public partial class FCMConsole : UserControl
    {
        public FCMConsole()
        {
            InitializeComponent();
        }

        #region Set Attributes
        private void SetAttributes()
        {
            txtNextOrderNumber.Validator = TextBoxValidation.FloatValidator;
            txtNextContractNumber.Validator = TextBoxValidation.FloatValidator;
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }
        #endregion

        #region Load Labels
        private void LoadLabels()
        {
            LoadLabel();
        }

        private void LoadLabel()
        {
            this.txtOwnerName.Text = xFilm5.Controls.Utility.Owner.GetOwnerName();
            this.txtMessage.Text = xFilm5.Controls.Utility.Owner.GetOwnerAddress();
        }
        #endregion

        #region Save Labels
        private void SaveLabel()
        {
            Client client = Client.Load(xFilm5.Controls.Utility.Owner.GetOwnerId());
            if (client != null)
            {
                client.Name = txtOwnerName.Text.Trim();
                client.Save();
            }

            string sql = String.Format("ClientID = {0} AND PrimaryAddr = 1", client.ID.ToString());
            Client_AddressBook address = Client_AddressBook.LoadWhere(sql);
            if (address != null)
            {
                address.Address = txtMessage.Text.Trim();
                address.Save();
            }
        }
        #endregion

        private void Counters_Load(object sender, EventArgs e)
        {
            SetAttributes();
            SetCaptions();
            SetTheme();
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblRecipient.Text = oDict.GetWordWithColon("fcm_recipient");
            lblMessage.Text = oDict.GetWordWithColon("fcm_message");

            radEveryone.Text = oDict.GetWord("fcm_everyone");
            radStaffOnly.Text = oDict.GetWord("fcm_staffonly");

            cmdSave.Text = oDict.GetWord("fcm_send_message");
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            var topic = "";
            topic = radEveryone.Checked ? "everyone" : "staffonly";
            var msg = txtMessage.Text.Trim().Replace("/", ".").Replace("\"", "");

            if (msg != String.Empty)
            {
                var result = BotHelper.PostBroadcastFcm(topic, msg);
                if (result)
                    MessageBox.Show("Sent successfully!", "Message");
                else
                    MessageBox.Show("Sent failed!", "Message");
            }
        }
    }
}