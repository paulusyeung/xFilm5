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
using System.Threading;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace xFilm5.SpeedBox
{
    public partial class Plate : Form
    {
        private String _UploadFileType = @"^.*$";
        private bool _Greyscale, _BlackOverprint, _Spot2CMYK, _DotGain50, _DotGain43, _DotGain40;

        private const String _UserLogin = "User Login", _ChangeTheme = "Change Theme", _SwitchToFilm = "Switch to Film";
        public List<String> UploadedFiles = new List<String>();

        public Plate()
        {
            InitializeComponent();
        }

        private void Plate_Load(object sender, EventArgs e)
        {
            #region if current default page == Film, goto Film()
            if (Config.CurrentPage == "Film")
            {
                var page = new Film();
                page.ShowPopup();
            }
            #endregion

            Config.LoadOnce_AtAppBegins();
            Context.CurrentTheme = Config.CurrentTheme;

            SetCaptions();
            SetAttributes();
            SetUploader();
            SetMenu();
        }

        #region Set Cations Attributes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            lblTitle.Text = oDict.GetWord("plate");

            chkGreyscale.Text = oDict.GetWord("greyscale");
            chkBlackOverprint.Text = oDict.GetWord("black_overprint");
            chkSpot2CMYK.Text = oDict.GetWord("spot_to_cmyk");

            chkDotGain50.Text = oDict.GetWord("dot_gain_50");
            chkDotGain43.Text = oDict.GetWord("dot_gain_43");
            chkDotGain40.Text = oDict.GetWord("dot_gain_40");
        }

        private void SetAttributes()
        {
            chkDotGain50.Checked = true;
            _DotGain50 = true;

            cmdMenu.Image = new ImageResourceHandle("fa-bars.16.png");
            cmdMenu.Visible = true;
        }

        private void SetMenu()
        {
            var menu = new ContextMenu();
            var item1 = new MenuItem(_UserLogin, new EventHandler(menuItem_Click));
            var item2 = new MenuItem(_ChangeTheme, new EventHandler(menuItem_Click));
            var item3 = new MenuItem(_SwitchToFilm, new EventHandler(menuItem_Click));

            menu.MenuItems.AddRange(new MenuItem[] { item1, item2, item3 });
            cmdMenu.DropDownMenu = menu;
        }

        private void SetUploader()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            uploadBox.UploadTempFilePath = System.IO.Path.GetTempPath();
            uploadBox.UploadFileTypes = _UploadFileType;                    // default = @"^.*$"
            uploadBox.UploadText = oDict.GetWord("msg_UploaderVWG");        // @"選出檔案或是拖拉檔案至此";
            uploadBox.AllowDrop = true;
            uploadBox.Dock = DockStyle.Fill;
            uploadBox.UploadFileCompleted += new UploadFileCompletedHandler(uploadBox_UploadFileCompleted);
            uploadBox.UploadError += new UploadErrorHandler(uploadBox_UploadError);
            uploadBox.UploadBatchCompleted += new UploadEventHandler(uploadBox_UploadBatchCompleted);
        }
        #endregion

        #region Menu Item Click
        private void menuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            //MessageBox.Show(string.Format("Menu Item '{0}' has been clicked!", menuItem.Text));
            switch (menuItem.Text)
            {
                case _UserLogin:
                    var login = new Login();
                    login.ShowDialog();
                    break;
                case _ChangeTheme:
                    var theme = new Theme();
                    theme.ShowDialog();
                    break;
                case _SwitchToFilm:
                    Config.CurrentPage = "Film";
                    var film = new Film();
                    film.ShowPopup();
                    break;
            }
        }
        #endregion

        #region Uploader functions
        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            UploadedFiles.Add(result.TempFileFullName);
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {

            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);
            MessageBox.Show(this, oDict.GetWord("msg_speedbox_error"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, new EventHandler(cmdSave_Click));
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);     // modal form 唔識停，VWG bug
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {

            }
        }

        void uploadBox_UploadBatchCompleted(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Options Checkboxes Click
        private void chkDotGain40_Click(object sender, EventArgs e)
        {
            if (chkDotGain40.Checked)
            {
                chkDotGain50.Checked = false;
                chkDotGain43.Checked = false;

                _DotGain50 = false;
                _DotGain43 = false;
                _DotGain40 = true;
            }
        }

        private void chkSpot2CMYK_Click(object sender, EventArgs e)
        {
            _Spot2CMYK = chkSpot2CMYK.Checked;
        }

        private void chkDotGain43_Click(object sender, EventArgs e)
        {
            if (chkDotGain43.Checked)
            {
                chkDotGain50.Checked = false;
                chkDotGain40.Checked = false;

                _DotGain50 = false;
                _DotGain43 = true;
                _DotGain40 = false;
            }
        }

        private void chkDotGain50_Click(object sender, EventArgs e)
        {
            if (chkDotGain50.Checked)
            {
                chkDotGain43.Checked = false;
                chkDotGain40.Checked = false;

                _DotGain50 = true;
                _DotGain43 = false;
                _DotGain40 = false;
            }
        }

        private void chkGreyscale_Click(object sender, EventArgs e)
        {
            _Greyscale = chkGreyscale.Checked;
        }

        private void chkBlackOverprint_Click(object sender, EventArgs e)
        {
            _BlackOverprint = chkBlackOverprint.Checked;
        }

        private void cboClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}