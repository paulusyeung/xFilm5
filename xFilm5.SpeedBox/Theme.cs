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

namespace xFilm5.SpeedBox
{
    public partial class Theme : Form
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void Theme_Load(object sender, EventArgs e)
        {
            SetAttributes();
        }

        private void SetAttributes()
        {
            if (Context.CurrentTheme == "Vista")
            {
                radLight.Checked = true;
            }
            else
            {
                radDark.Checked = true;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (radLight.Checked)
            {
                Context.CurrentTheme = "Vista";
                Config.CurrentTheme = "Vista";
            }
            else
            {
                Context.CurrentTheme = "Graphite";
                Config.CurrentTheme = "Graphite";
            }
            this.Close();
        }
    }
}