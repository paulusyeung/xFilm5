#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

#endregion

namespace xFilm5.Help
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
//            string curpath = System.Web.HttpContext.Current.Server.MapPath("~/bin/nxJewel.dll");
//            FileVersionInfo oRuntimeInfo = FileVersionInfo.GetVersionInfo( curpath);
//            this.mobjLabelVersion.Text = String.Format(this.mobjLabelVersion.Text, oRuntimeInfo.FileVersion);

            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.mobjLabelVersion.Text = String.Format(this.mobjLabelVersion.Text, version.ToString());
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}