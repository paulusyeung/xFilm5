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
using Gizmox.WebGUI.Common.Resources;
using System.Threading;

#endregion

namespace xFilm5.SpeedBox
{
    public partial class BaseForm : Form
    {
        private const String _UserLogin = "User Login", _ChangeTheme = "Change Theme", _FactoryReset = "Factory Reset", _SwitchForm = "Switch Form";
        private ContextMenu _Menu = new ContextMenu();

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            Config.LoadOnce_AtAppBegins();
            Context.CurrentTheme = Config.CurrentTheme;

            SetAttributes();

            LoadChildForm();
        }

        #region SetCaptions SetMenu
        private void SetCaptions()
        {

        }

        private void SetAttributes()
        {
            cmdMenu.Image = new ImageResourceHandle("fa-bars.16.png");
            cmdMenu.Visible = true;
        }

        private void SetMenu()
        {
            _Menu.MenuItems.Clear();
            var item1 = new MenuItem(_UserLogin, new EventHandler(menuItem_Click));
            item1.Tag = _UserLogin;
            var item2 = new MenuItem(_ChangeTheme, new EventHandler(menuItem_Click));
            item2.Tag = _ChangeTheme;
            var item3 = new MenuItem(_FactoryReset, new EventHandler(menuItem_Click));
            item3.Tag = _FactoryReset;
            var item4 = new MenuItem(Config.CurrentPage.ToLower() == "film" ? "Switch To Plate" : "Switch To Film", new EventHandler(menuItem_Click));
            item4.Tag = _SwitchForm;

            var sep = new MenuItem();

            _Menu.MenuItems.AddRange(new MenuItem[] { item1, item2, item3, sep, item4 });
            cmdMenu.DropDownMenu = _Menu;
        }
        #endregion

        #region Menu Item Click
        private void menuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            switch (menuItem.Tag.ToString())
            {
                case _UserLogin:
                    var login = new Forms.Login();
                    login.ShowDialog();
                    break;
                case _ChangeTheme:
                    var theme = new Forms.Theme();
                    theme.ShowDialog();
                    break;
                case _FactoryReset:
                    Helper.Common.FactoryReset();
                    Context.Session.IsLoggedOn = false;
                    MessageBox.Show("Current Settings resetted.");
                    break;
                case _SwitchForm:
                    var page = Config.CurrentPage.ToLower();
                    switch (page)
                    {
                        case "film":
                            Config.CurrentPage = "Plate";
                            break;
                        case "plate":
                        default:
                            Config.CurrentPage = "Film";
                            break;
                    }
                    LoadChildForm();
                    break;
            }
        }
        #endregion

        /// <summary>
        /// pnlChild 轉 user control (child form)
        /// </summary>
        private void LoadChildForm()
        {
            pnlChild.Controls.Clear();
            var page = Config.CurrentPage.ToLower();
            switch (page)
            {
                case "film":
                    var film = new Forms.Film();
                    film.Dock = DockStyle.Fill;
                    pnlChild.Controls.Add(film);
                    break;
                case "plate":
                default:
                    var plate = new Forms.Plate();
                    plate.Dock = DockStyle.Fill;
                    pnlChild.Controls.Add(plate);
                    break;
            }
            SetTitle();
            SetMenu();
        }

        private void SetTitle()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            var page = Config.CurrentPage.ToLower();
            lblTitle.Text = page == "film" ? oDict.GetWord("film") : oDict.GetWord("plate");
        }
    }
}