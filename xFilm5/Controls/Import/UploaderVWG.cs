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

using xFilm5.DAL;
#endregion

namespace xFilm5.Controls.Import
{
    public partial class UploaderVWG : Form
    {
        private String _UploadFileType = @"^.*$";

        public List<String> UploadedFiles = new List<String>();

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

        public UploaderVWG()
        {
            InitializeComponent();
        }

        private void Uploader_Load(object sender, EventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            uploadBox.UploadTempFilePath = System.IO.Path.GetTempPath();
            uploadBox.UploadFileTypes = _UploadFileType;                    // default = @"^.*$"
            uploadBox.UploadText = oDict.GetWord("msg_UploaderVWG");        // @"選出檔案或是拖拉檔案至此";
            uploadBox.AllowDrop = true;
            uploadBox.Dock = DockStyle.Fill;
            uploadBox.UploadFileCompleted += new UploadFileCompletedHandler(uploadBox_UploadFileCompleted);
            uploadBox.UploadError +=new UploadErrorHandler(uploadBox_UploadError);
            uploadBox.UploadBatchCompleted += new UploadEventHandler(uploadBox_UploadBatchCompleted);
        }

        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            UploadedFiles.Add(result.TempFileFullName);
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
            MessageBox.Show(String.Format("{0}?", e.Error), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, new EventHandler(cmdSave_Click));
            //MessageBox.Show(e.Error);
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {

            }
        }

        void uploadBox_UploadBatchCompleted(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}