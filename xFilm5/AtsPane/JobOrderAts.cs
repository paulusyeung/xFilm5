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

using xFilm5.DAL;
using System.IO;
using System.Configuration;
using xFilm5.Helper;

#endregion

namespace xFilm5.AtsPane
{
    public partial class JobOrderAts : UserControl
    {
        public JobOrderAts()
        {
            InitializeComponent();

            SetAtsJobOrder();
        }

        private void SetAtsJobOrder()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.atsJobOrder.MenuHandle = false;
            this.atsJobOrder.DragHandle = false;
            this.atsJobOrder.TextAlign = ToolBarTextAlign.Right;

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            Common.Data.AppendMenuItem_OrderType(ref ddlNew);
            ToolBarButton cmdNew = new ToolBarButton("New", oDict.GetWord("addnew"));
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.DropDownMenu = ddlNew;

            this.atsJobOrder.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsMenuClick);

            // Separator
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsJobOrder.Buttons.Add(sep);

            // cmdSearch
            ToolBarButton cmdSearch = new ToolBarButton("Search", oDict.GetWord("search"));
            cmdSearch.Tag = "Search";
            cmdSearch.Image = new IconResourceHandle("16x16.find.png");

            this.atsJobOrder.Buttons.Add(cmdSearch);

            #region 2018.11.16 paulus: SpeedBox
            this.atsJobOrder.Buttons.Add(sep);
            ToolBarButton cmdSpeedBox = new ToolBarButton("SpeedBox", oDict.GetWord("speedbox"));
            cmdSpeedBox.Tag = "SpeedBox";
            cmdSpeedBox.Image = new IconResourceHandle("16x16.fe_cloud_upload_16_white.png");

            this.atsJobOrder.Buttons.Add(cmdSpeedBox);
            #endregion

            this.atsJobOrder.ButtonClick += new ToolBarButtonClickEventHandler(atsJobOrder_ButtonClick);           
        }

        private void AtsMenuClick(object sender, MenuItemEventArgs e)
        {
            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "uploadfile":
                        xFilm5.JobOrder.Forms.UploadFile oUploadFile = new xFilm5.JobOrder.Forms.UploadFile();
                        oUploadFile.EditMode = Common.Enums.EditMode.Add;
                        oUploadFile.Show();
                        break;
                    case "directprint":
                        xFilm5.JobOrder.Forms.DirectPrint oDirectPrint = new xFilm5.JobOrder.Forms.DirectPrint();
                        oDirectPrint.EditMode = Common.Enums.EditMode.Add;
                        oDirectPrint.Show();
                        break;
                    case "psfile":
                        xFilm5.JobOrder.Forms.PsFile oPsFile = new xFilm5.JobOrder.Forms.PsFile();
                        oPsFile.EditMode = Common.Enums.EditMode.Add;
                        oPsFile.Show();
                        break;
                    case "plate":
                        xFilm5.JobOrder.Forms.Plate5 oPlate = new xFilm5.JobOrder.Forms.Plate5();
                        oPlate.EditMode = Common.Enums.EditMode.Add;
                        oPlate.Show();
                        break;
                    case "film":
                        xFilm5.JobOrder.Forms.Film5 oFilm = new xFilm5.JobOrder.Forms.Film5();
                        oFilm.EditMode = Common.Enums.EditMode.Add;
                        oFilm.Show();
                        break;
                    case "vps":
                        xFilm5.JobOrder.Forms.Vps5 oVps = new xFilm5.JobOrder.Forms.Vps5();
                        oVps.EditMode = Common.Enums.EditMode.Add;
                        oVps.Show();
                        break;
                }
            }
        }

        #region Speed Box uploader
        private void UseUploaderVWG()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
            xFilm5.Controls.Upload.SpeedBox speedBox = new xFilm5.Controls.Upload.SpeedBox();

            speedBox.Text = oDict.GetWord("speedbox");
            speedBox.UploadedFileType = @"^.*\.(ps|pdf)$";   // accept ps, pdf
            speedBox.FormClosed += new Form.FormClosedEventHandler(uploader_FormClosed);
            speedBox.ShowDialog();
        }

        private void uploader_FormClosed(object sender, FormClosedEventArgs e)
        {
            string FileName = string.Empty;
            string FullName = string.Empty;
            //string dropbox = Common.Client.DropBox(202020);

            xFilm5.Controls.Upload.SpeedBox speedBox = (xFilm5.Controls.Upload.SpeedBox)sender;
            var clientId = speedBox.ClientId;
            if (clientId != 0)
            {
                //string dropbox = Common.Client.DropBox(clientId);
                if (speedBox.UploadedFiles.Count > 0)
                {
                    foreach (String filepath in speedBox.UploadedFiles)
                    {
                        #region process uploaded file: 把已經上載到 temporary folder 的檔案抄至指定的檔案夾
                        if (File.Exists(filepath))
                        {
                            String tempfilename = Path.GetFileName(filepath);                           // VWG Uploader 會加個 Guid 在檔案名之前，要將佢去掉先至係真正嘅檔案名
                            String filename = tempfilename.Substring(tempfilename.IndexOf('_') + 1);

                            #region 先從 local 抄去 shared netwrok drive
                            #region 讀入 SpeedBox network impersonation
                            String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                            String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                            String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];

                            String tempPath = String.Format("{0}{1}", serverUri, ConfigurationManager.AppSettings["SpeedBox_TempFolder"]);
                            String tempFilePath = Path.Combine(tempPath, tempfilename);
                            #endregion

                            using (new Impersonation(serverUri, userName, userPassword))
                            {
                                File.Copy(filepath, tempFilePath, true);
                                File.Delete(filepath);                      // Copy is synchronous operation, delete 應該唔會有 error

                                Helper.BotHelper.PostSpeedBox(clientId, tempfilename, filename);      // 再交俾 BotServer 處理
                            }
                            #endregion
                        }
                        #endregion
                    }
                    nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
                    MessageBox.Show(oDict.GetWord("msg_speedbox_ok"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdPrompt_Click));
                }
            }
        }

        private void cmdPrompt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        private void atsJobOrder_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            #region 2018.11.17 paulus: 避免 speedbox 清除咗原來嘅畫面，唔跳去 ShowWorkspace()
            var tag = (string)e.Button.Tag;
            if (!string.IsNullOrEmpty(tag))
            {
                if (tag.ToLower() == "speedbox")
                {
                    UseUploaderVWG();
                    return;
                }
            }
            #endregion

            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = (string)e.Button.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)e.Button.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
                    case "search":
                        if (xFilm5.Controls.Utility.User.IsStaff(xFilm5.DAL.Common.Config.CurrentUserId))
                        {
                            xFilm5.JobOrder.JoSearch oJoSearch = new xFilm5.JobOrder.JoSearch(controls[0]);
                            oJoSearch.DockPadding.All = 6;
                            oJoSearch.Dock = DockStyle.Fill;
                            oJoSearch.WorkflowFrom = Common.Enums.Workflow.Cancelled;
                            oJoSearch.WorkflowTo = Common.Enums.Workflow.Completed;
                            wspPane.Controls.Add(oJoSearch);
                        }
                        else
                        {
                            if (xFilm5.Controls.Utility.User.IsClient(xFilm5.DAL.Common.Config.CurrentUserId))
                            {
                                xFilm5.Customer.JobOrder.JoSearch cuSearch = new xFilm5.Customer.JobOrder.JoSearch(controls[0]);
                                cuSearch.DockPadding.All = 6;
                                cuSearch.Dock = DockStyle.Fill;
                                cuSearch.WorkflowFrom = Common.Enums.Workflow.Cancelled;
                                cuSearch.WorkflowTo = Common.Enums.Workflow.Completed;
                                wspPane.Controls.Add(cuSearch);
                            }
                        }
                    break;
                    case "speedbox":
                        UseUploaderVWG();
                        break;
                }
            }
        }
    }
}