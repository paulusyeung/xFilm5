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

namespace xFilm5.SpeedBox.Forms
{
    public partial class Film : UserControl
    {
        private String _UploadFileType = @"^.*$";
        private bool _Positive, _Negative, _EmulsionUp, _EmulsionDown, _ColorSeperation;

        private const String _UserLogin = "User Login", _ChangeTheme = "Change Theme", _SwitchToPlate = "Switch to Plate";
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

        public bool Positive
        {
            get { return _Positive; }
            set { _Positive = value; }
        }

        public bool Negative
        {
            get { return _Negative; }
            set { _Negative = value; }
        }

        public bool EmulsionUp
        {
            get { return _EmulsionUp; }
            set { _EmulsionUp = value; }
        }

        public bool EmulsionDown
        {
            get { return _EmulsionDown; }
            set { _EmulsionDown = value; }
        }

        public bool ColorSeperation
        {
            get { return _ColorSeperation; }
            set { _ColorSeperation = value; }
        }
        #endregion

        public Film()
        {
            InitializeComponent();
        }

        private void Film_Load(object sender, EventArgs e)
        {
            SetCaptions();
            SetAttributes();
            SetUploader();

#if (DEBUG)
            txtFooter.Visible = true;
#else
            txtFooter.Visible = false;
#endif
        }

        #region prepare Client Name comboBox
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

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
        }

        private void SetUploader()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            uploadBox.UploadTempFilePath = System.IO.Path.GetTempPath();
            uploadBox.UploadFileTypes = _UploadFileType;                    // default = @"^.*$"
            uploadBox.UploadText = oDict.GetWord("msg_UploaderVWG");        // @"選出檔案或是拖拉檔案至此";
            uploadBox.AllowDrop = true;
            uploadBox.Dock = DockStyle.Fill;
            //uploadBox.UploadFileCompleted += new UploadFileCompletedHandler(uploadBox_UploadFileCompleted);
            //uploadBox.UploadError += new UploadErrorHandler(uploadBox_UploadError);
            //uploadBox.UploadBatchCompleted += new UploadEventHandler(uploadBox_UploadBatchCompleted);
            //uploadBox.UploadBatchStarting += new UploadEventHandler(uploadBox_UploadBatchStarting);
        }

        private void uploadBox_UploadBatchStarting(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Uploader functions
        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            UploadedFiles.Add(result.TempFileFullName);

            //txtFooter.Text += result.TempFileFullName + Environment.NewLine;
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {

            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);
            //MessageBox.Show(this, oDict.GetWord("msg_speedbox_error"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, new EventHandler(cmdSave_Click));
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
            int counter = 1;
            txtFooter.Text = String.Empty;
            foreach (var file in UploadedFiles)
            {
                System.Diagnostics.Debug.WriteLine(file);
                txtFooter.Text += counter.ToString() + ": " + file + Environment.NewLine;
                ++counter;
            }
            UploadedFiles.Clear();
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