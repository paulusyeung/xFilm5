#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

#endregion

namespace xFilm5.Controls.Email
{
    public partial class DN : Form
    {
        private int _ReceiptHeaderId = 0;

        #region public properties
        public int ReceiptHeaderId
        {
            set { _ReceiptHeaderId = value; }
        }
        #endregion

        public DN()
        {
            InitializeComponent();
        }

        private void DN_Load(object sender, EventArgs e)
        {
            SetCaptions();
            ShowRecord();
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(DAL.Common.Config.CurrentWordDict, DAL.Common.Config.CurrentLanguageId);

            this.Text = oDict.GetWord("send_email");
            lblReceiptNumber.Text = oDict.GetWordWithColon("delivery_note_number");
            lblRecipient.Text = oDict.GetWordWithColon("recipient") +  " (user1@domain.com, user1@domain.com)";
            cmdSendEmail.Text = oDict.GetWord("send_out_email");
        }


        private void ShowRecord()
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                var hdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == _ReceiptHeaderId).SingleOrDefault();
                if (hdr != null)
                {
                    txtReceiptNumber.Text = hdr.ReceiptNumber.ToString();
                    txtRecipient.Text = Helper.ClientHelper.GetEmailRecipient(hdr.ClientId);
                }
            }
            /**
            DAL.ReceiptHeader hdr = DAL.ReceiptHeader.Load(_ReceiptHeaderId);
            if (hdr != null)
            {
                txtReceiptNumber.Text = hdr.ReceiptNumber.ToString();

                if (hdr.ClientUserId != 0)
                {
                    DAL.Client_User client = DAL.Client_User.Load(hdr.ClientUserId);
                    if (client != null)
                    {
                        txtRecipient.Text = String.IsNullOrEmpty(client.Email) ? "" : client.Email;
                    }
                }
                else
                {
                    String sql = String.Format("ClientID = {0} AND PrimaryUser = 1", hdr.ClientId.ToString());
                    DAL.Client_User client = DAL.Client_User.LoadWhere(sql);
                    if (client != null)
                    {
                        txtRecipient.Text = String.IsNullOrEmpty(client.Email) ? "" : client.Email;
                    }
                }
            }
            */
        }

        private void cmdSendEmail_Click(object sender, EventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(DAL.Common.Config.CurrentWordDict, DAL.Common.Config.CurrentLanguageId);

            bool valid = true;
            String[] recipients = txtRecipient.Text.Split(',');
            foreach (String email in recipients)
            {
                if (!EmailEx.IsValidEmail(email.Trim()))
                {
                    MessageBox.Show(String.Format("{0}: {1}", oDict.GetWord("error_invalid_email"), email.Trim()));
                    valid = false;
                }
            }

            if (valid)
            {
                if (EmailEx.SendDNEmail(_ReceiptHeaderId, txtRecipient.Text))
                {
                    MessageBox.Show(oDict.GetWord("done"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                }
                else
                {
                    MessageBox.Show(oDict.GetWord("error_found"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, new EventHandler(cmdCloseForm));
                }
            }
        }

        private void cmdCloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}