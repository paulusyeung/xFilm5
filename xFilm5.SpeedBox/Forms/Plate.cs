#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Threading;
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace xFilm5.SpeedBox.Forms
{
    public partial class Plate : UserControl
    {
        private String _UploadFileType = @"^.*$";
        private bool _Greyscale, _BlackOverprint, _Spot2CMYK, _DotGain50, _DotGain43, _DotGain40;

        private const String _UserLogin = "User Login", _ChangeTheme = "Change Theme", _SwitchToFilm = "Switch to Film";
        public List<String> UploadedFiles = new List<String>();

        #region public properties
        /// <summary>
        ///     Regular expression match string for valid filename and extension Default: "^.*$",
        ///     means all files. Example: 1. "^.*\.(gif|jpe?g|png)$", means *.gif, *.jpg, *.jpeg,
        ///     *.png  2. ^.*\.(rtf)$" means rtf only
        /// </summary>
        public String UploadedFileType
        {
            get { return _UploadFileType; }
            set { _UploadFileType = value; }
        }

        public bool Greyscale
        {
            get { return _Greyscale; }
            set { _Greyscale = value; }
        }

        public bool BlackOverprint
        {
            get { return _BlackOverprint; }
            set { _BlackOverprint = value; }
        }

        public bool Spot2CMYK
        {
            get { return _Spot2CMYK; }
            set { _Spot2CMYK = value; }
        }

        public bool DotGain50
        {
            get { return _DotGain50; }
            set { _DotGain50 = value; }
        }

        public bool DotGain43
        {
            get { return _DotGain43; }
            set { _DotGain43 = value; }
        }

        public bool DotGain40
        {
            get { return _DotGain40; }
            set { _DotGain40 = value; }
        }
        #endregion

        public Plate()
        {
            InitializeComponent();
        }

        private void Plate_Load(object sender, EventArgs e)
        {
            SetCaptions();
            SetAttributes();
            SetUploader();
        }

        #region Set Cations Attributes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

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

        #region Uploader functions
        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            UploadedFiles.Add(result.TempFileFullName);
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {

            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);
            MessageBox.Show(oDict.GetWord("msg_speedbox_error"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, new EventHandler(cmdSave_Click));
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
            //HACK: this.Close();
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