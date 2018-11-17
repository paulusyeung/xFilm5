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
using System.Threading;
#endregion

namespace xFilm5.Controls.Upload
{
    public partial class SpeedBox : Form
    {
        private String _UploadFileType = @"^.*$";

        public List<String> UploadedFiles = new List<String>();

        private int _ClientId = 0;

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

        public new int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }

        public SpeedBox()
        {
            InitializeComponent();
        }

        private void Uploader_Load(object sender, EventArgs e)
        {
            SetCaptions();
            SetAttributes();
            SetDropdowns();
            if (xFilm5.Controls.Utility.User.IsClient())
            {
                SetClientForm();
            }

            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            uploadBox.UploadTempFilePath = System.IO.Path.GetTempPath();
            uploadBox.UploadFileTypes = _UploadFileType;                    // default = @"^.*$"
            uploadBox.UploadText = oDict.GetWord("msg_UploaderVWG");        // @"選出檔案或是拖拉檔案至此";
            uploadBox.AllowDrop = true;
            uploadBox.Dock = DockStyle.Fill;
            uploadBox.UploadFileCompleted += new UploadFileCompletedHandler(uploadBox_UploadFileCompleted);
            uploadBox.UploadError += new UploadErrorHandler(uploadBox_UploadError);
            uploadBox.UploadBatchCompleted += new UploadEventHandler(uploadBox_UploadBatchCompleted);
        }

        #region prepare Client Name comboBox
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblClient.Text = oDict.GetWordWithColon("client");
        }

        private void SetAttributes()
        {
            if (Common.Config.IamStaff)
            {
                cboClient.Visible = true;
            }
            else
            { 
                txtClientName.Visible = true;
            }
        }

        private void SetDropdowns()
        {
            Client.LoadCombo(ref cboClient, "Name", false, false, "", "Status >= 1");
            _ClientId = Convert.ToInt32(cboClient.SelectedValue.ToString());
        }

        private void SetClientForm()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            _ClientId = xFilm5.Controls.Utility.User.GetClientId();
            Client client = Client.Load(_ClientId);
            if (client != null)
            {
                lblClient.Text = String.Format(oDict.GetWord("client") + " ( {0} ):", client.ID.ToString());
                txtClientName.Text = client.Name;
                txtClientName.Visible = true;

                cmdClientInfo.Visible = false;
            }
        }
        #endregion

        private void uploadBox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            UploadFileResult result = e.Result;
            UploadedFiles.Add(result.TempFileFullName);
        }

        private void uploadBox_UploadError(object sender, UploadErrorEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
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

        private void cboClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _ClientId = (int)cboClient.SelectedValue;
            }
            catch
            {
                _ClientId = 0;
            }
        }

        private void cmdClientInfo_Click(object sender, EventArgs e)
        {
            Sales.Client.ClientRecord clientRec = new xFilm5.Sales.Client.ClientRecord();
            clientRec.ClientId = _ClientId;
            clientRec.EditMode = Common.Enums.EditMode.Edit;
            clientRec.ShowDialog();
        }
    }
}