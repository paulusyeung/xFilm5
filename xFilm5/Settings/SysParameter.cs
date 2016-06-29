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

#endregion

namespace xFilm5.Settings
{
    public partial class SysParameter : UserControl
    {
        public SysParameter()
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
            this.txtOwnerAddress.Text = xFilm5.Controls.Utility.Owner.GetOwnerAddress();
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
                address.Address = txtOwnerAddress.Text.Trim();
                address.Save();
            }
        }
        #endregion

        private void Counters_Load(object sender, EventArgs e)
        {
            SetAttributes();
            SetTheme();
            LoadLabels();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveLabel();
            MessageBox.Show("Save successfully!", "Message");
        }
    }
}