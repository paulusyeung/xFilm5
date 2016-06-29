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

#endregion

namespace xFilm5.NavPane
{
    public partial class SettingsNav : UserControl
    {
        public SettingsNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("settings", this.navSettings.Nodes);
        }

        private void navSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navSettings.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navSettings.SelectedNode.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                switch (Tag.ToLower())
                {
                    case "system_parameters":
                        xFilm5.Settings.SysParameter sysParam = new xFilm5.Settings.SysParameter();
                        sysParam.DockPadding.All = 6;
                        sysParam.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(sysParam);
                        break;
                }
            }
        }
    }
}