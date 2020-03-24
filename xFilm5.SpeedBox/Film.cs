#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace xFilm5.SpeedBox
{
    public partial class Film : Form
    {
        private String _UploadFileType = @"^.*$";
        private bool _Positive, _Negative, _EmulsionUp, _EmulsionDown, _ColorSeperation;
        private int _ClientId = 0;

        private const String _UserLogin = "User Login", _ChangeTheme = "Change Theme", _SwitchToPlate = "Switch to Plate";
        public List<String> UploadedFiles = new List<String>();

        public Film()
        {
            InitializeComponent();
        }

        private void Film_Load(object sender, EventArgs e)
        {
            #region if current default page == Plate, goto Plate()
            if (Config.CurrentPage == "Plate")
            {
                var page = new Plate();
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

        #region prepare Client Name comboBox
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            lblTitle.Text = oDict.GetWord("film");
            chkPositive.Text = oDict.GetWord("positive");
            chkNegative.Text = oDict.GetWord("negative");
            chkEmulsionUp.Text = oDict.GetWord("emulsion_up");
            chkEmulsionDown.Text = oDict.GetWord("emulsion_down");
            chkColorSeperation.Text = oDict.GetWord("color_seperation");
        }

        private void SetAttributes()
        {
            chkPositive.Checked = true;
            chkEmulsionUp.Checked = true;
            _Positive = true;
            _EmulsionUp = true;

            chkColorSeperation.Checked = false;

            cmdMenu.Image = new ImageResourceHandle("fa-bars.16.png");
            cmdMenu.Visible = true;
        }

        private void SetMenu()
        {
            var menu = new ContextMenu();
            var item1 = new MenuItem(_UserLogin, new EventHandler(menuItem_Click));
            var item2 = new MenuItem(_ChangeTheme, new EventHandler(menuItem_Click));
            var item3 = new MenuItem(_SwitchToPlate, new EventHandler(menuItem_Click));

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
                case _SwitchToPlate:
                    Config.CurrentPage = "Plate";
                    var plate = new Plate();
                    plate.ShowPopup();
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

        #region Film options button clicks
        private void chkPositive_Click(object sender, EventArgs e)
        {
            if (chkPositive.Checked)
            {
                chkNegative.Checked = false;

                _Positive = true;
                _Negative = false;
            }
        }

        private void chkNegative_Click(object sender, EventArgs e)
        {
            if (chkNegative.Checked)
            {
                chkPositive.Checked = false;

                _Positive = false;
                _Negative = true;
            }
        }

        private void chkEmusionUp_Click(object sender, EventArgs e)
        {
            if (chkEmulsionUp.Checked)
            {
                chkEmulsionDown.Checked = false;

                _EmulsionUp = true;
                _EmulsionDown = false;
            }
        }

        private void chkEmulsionDown_Click(object sender, EventArgs e)
        {
            if (chkEmulsionDown.Checked)
            {
                chkEmulsionUp.Checked = false;

                _EmulsionUp = false;
                _EmulsionDown = true;
            }
        }

        private void chkColorSeperation_Click(object sender, EventArgs e)
        {
            _ColorSeperation = chkColorSeperation.Checked;
        }
        #endregion
    }
}