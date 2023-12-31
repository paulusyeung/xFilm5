#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

#endregion

namespace xFilm5.Cashier
{
    public partial class Desktop : Form
    {
        public Desktop()
        {
            InitializeComponent();
        }

        private void Desktop_Load(object sender, EventArgs e)
        {
            SetTheme();
            LoadJobComplete();
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }

        private void LoadJobComplete()
        {
            wspPane.Controls.Clear();
            xFilm5.Cashier.JobComplete oJobComplete = new xFilm5.Cashier.JobComplete();
            oJobComplete.DockPadding.All = 6;
            oJobComplete.Dock = DockStyle.Fill;
            wspPane.Controls.Add(oJobComplete);
        }

        private void LoadCompletedList()
        {
            wspPane.Controls.Clear();
            xFilm5.Cashier.CompletedList oCompletedList = new xFilm5.Cashier.CompletedList();
            oCompletedList.DockPadding.All = 6;
            oCompletedList.Dock = DockStyle.Fill;
            wspPane.Controls.Add(oCompletedList);
        }

        private void cmdPower_Click(object sender, EventArgs e)
        {
            xFilm5.DAL.Common.Config.CurrentUserId = 0;
            // While setting the IsLoggedOn to false, will redirect to Logon Page.
            this.Context.Session.IsLoggedOn = false;
            VWGContext.Current.HttpContext.Session.Abandon();
            VWGContext.Current.Transfer(new Desktop());
        }

        private void cmdGas_Click(object sender, EventArgs e)
        {
            LoadCompletedList();
        }

        private void cmdHome_Click(object sender, EventArgs e)
        {
            LoadJobComplete();
        }
    }
}