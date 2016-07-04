#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

using Microsoft.Win32;

using MarkPasternak.Utility;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Dialogs;

using xFilm5.DAL;

#endregion

namespace xFilm5.Support
{
    public partial class DropBoxExplorer : UserControl, IGatewayComponent
    {
        private int _ClientId = 0;
        private string _filename = String.Empty;

        public DropBoxExplorer()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAttribute();
            SetTheme();
            ResetToolbar();
            BuildClientTree(tvwClient.Nodes);
        }

        #region IGatewayControl Members
        void IGatewayComponent.ProcessRequest(IContext objContext, string strAction)
        {
            // Trt to get the gateway handler
            IGatewayHandler objGatewayHandler = ProcessGatewayRequest(objContext.HttpContext, strAction);

            if (objGatewayHandler != null)
            {
                objGatewayHandler.ProcessGatewayRequest(objContext, this);
            }
        }

        protected override IGatewayHandler ProcessGatewayRequest(HttpContext objContext, string strAction)
        {
            IGatewayHandler objGH = null;

            string filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), _filename);

            if (File.Exists(filepath))
            {
                FileInfo oFile = new FileInfo(filepath);
                if (oFile.Length > 1024 * 1024 * 32)
                {
                    // use this method for file size over 32MB 
                    WriteFileHelper oWriteFile = new WriteFileHelper();
                    oWriteFile.BufferSize = 65536;
                    oWriteFile.WriteFileToResponseStreamWithForceDownloadHeaders(filepath, HttpUtility.UrlEncode(_filename));
                }
                else
                {
                    HttpResponse response = objContext.Response;    // prefer to use Gizmox instead of: this.Context.HttpContext.Response;

                    response.Buffer = true;
                    response.Clear();
                    response.ClearHeaders();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(_filename));
                    response.WriteFile(filepath);
                    response.Flush();
                    response.End();
                }
            }
            else
            {
                objContext.Response.Write(String.Format("<html><body><h>File: {0}, file not found!</h></body></html>", _filename));
            }

            return objGH;
        }
        #endregion

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            colFileName.Text = oDict.GetWord("file_name");
            colFileSize.Text = oDict.GetWord("file_size");
            colFileType.Text = oDict.GetWord("file_type");
            colModifiedOn.Text = oDict.GetWord("modified_on");
        }

        private void SetAttribute()
        {
            this.lvwFileExplorer.ListViewItemSorter = new ListViewItemSorter(this.lvwFileExplorer);

            toolTip1.SetToolTip(lvwFileExplorer, "Double click to download file");
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }
        #endregion

        #region build Client tree
        private void BuildClientTree(TreeNodeCollection root)
        {
            for (int i = 0; i <= 26; i++)
            {
                this.AddNodes(root, i);
            }
        }

        private void AddNodes(TreeNodeCollection oNodes, int row)
        {
            string where = "Status > 0";
            string[] orderby = { "Name" };

            #region create the 1st alpha character
            TreeNode oNode = new TreeNode();
            oNode.Image = new IconResourceHandle("16x16.folder_close.png");
            oNode.ExpandedImage = new IconResourceHandle("16x16.folder_open.png");
            oNode.IsExpanded = false;
            switch (row)
            {
                case 0:
                    oNode.Label = "#";
                    break;
                default:
                    oNode.Label = ((char)(row + 64)).ToString();
                    break;
            }
            oNodes.Add(oNode);
            #endregion

            #region append the Clients with the same Alpha
            switch (row)
            {
                case 0:
                    where = "SUBSTRING([Name], 1, 1) NOT BETWEEN N'A' AND N'Z'";
                    break;
                default:
                    where = String.Format("SUBSTRING([Name], 1, 1) = N'{0}'", ((char)(row + 64)).ToString());
                    break;
            }
            ClientCollection oClients = Client.LoadCollection(where, orderby, true);
            if (oClients.Count > 0)
            {
                oNode.Loaded = true;

                foreach (Client client in oClients)
                {
                    TreeNode endNode = new TreeNode();
                    endNode.Label = client.Name;
                    endNode.Tag = client.ID;
                    endNode.Image = new IconResourceHandle("16x16.group.png");
                    endNode.IsExpanded = false;
                    oNode.Nodes.Add(endNode);
                }
            }
            #endregion
        }
        #endregion

        #region SetToolbar(), ResetToolbar(), SetfileExplorerAns();
        private void ResetToolbar()
        {
            ResetClientTreeAns();
            ResetFileExplorerAns();
        }

        private void ResetClientTreeAns()
        {
            this.ansClientTree.MenuHandle = false;
            this.ansClientTree.DragHandle = false;
            this.ansClientTree.TextAlign = ToolBarTextAlign.Right;
        }

        private void ResetFileExplorerAns()
        {
            this.ansFileExplorer.MenuHandle = false;
            this.ansFileExplorer.DragHandle = false;
            this.ansFileExplorer.TextAlign = ToolBarTextAlign.Right;
        }

        private void SetFileExplorerAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansFileExplorer.MenuHandle = false;
            this.ansFileExplorer.DragHandle = false;
            this.ansFileExplorer.TextAlign = ToolBarTextAlign.Right;
            this.ansFileExplorer.Buttons.Clear();

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansFileExplorer.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansFileExplorer.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansFileExplorer.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansFileExplorer.Buttons[1].ToolTipText = @"Sorting";
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansFileExplorer.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansFileExplorer.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansFileExplorer.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansFileExplorer.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansFileExplorer.Buttons[3].Visible = false;
            #endregion

            this.ansFileExplorer.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansFileExplorer.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansFileExplorer.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansFileExplorer.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansFileExplorer.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansFileExplorer.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansFileExplorer.Buttons[8].Enabled = false;
            this.ansFileExplorer.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
            #endregion

            this.ansFileExplorer.Buttons.Add(sep);

            this.ansFileExplorer.Buttons.Add(new ToolBarButton("ClientInfo", oDict.GetWord("client_info")));
            this.ansFileExplorer.Buttons[10].Image = new IconResourceHandle("16x16.group.png");

            this.ansFileExplorer.Buttons.Add(new ToolBarButton("DropFile", oDict.GetWord("upload_file")));
            this.ansFileExplorer.Buttons[11].Image = new IconResourceHandle("16x16.dropbox_in_16x16.png");

            this.ansFileExplorer.Buttons.Add(new ToolBarButton("RetrieveFile", oDict.GetWord("download_file")));
            this.ansFileExplorer.Buttons[12].Image = new IconResourceHandle("16x16.downloads.png");

            this.ansFileExplorer.Buttons.Add(new ToolBarButton("DeleteFile", oDict.GetWord("delete_file")));
            this.ansFileExplorer.Buttons[13].Image = new IconResourceHandle("16x16.16_L_remove.gif");

            this.ansFileExplorer.Buttons.Add(sep);
        }
        #endregion

        private void BindFileExplorer()
        {
            string dropbox = Common.Client.DropBox(_ClientId);
            RegistryKey rootKey = Registry.ClassesRoot;

            lvwFileExplorer.Items.Clear();
            foreach (string file in Directory.GetFiles(dropbox))
            {
                FileInfo fileinfo = new FileInfo(file);
                string contentType = String.Empty;

                try
                {
                    RegistryKey subKey = rootKey.OpenSubKey(fileinfo.Extension);
                    contentType = (string)subKey.GetValue("Content Type");
                }
                catch
                {
                    contentType = String.Empty;
                }

                ListViewItem listitem = this.lvwFileExplorer.Items.Add(fileinfo.Name);          // File Name
                #region File Icon
                switch (fileinfo.Extension.ToLower())
                {
                    case ".ai":
                        listitem.SmallImage = new IconResourceHandle("FileType.ai16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.ai32.png");
                        break;
                    case ".cdr":
                        listitem.SmallImage = new IconResourceHandle("FileType.cdr16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.cdr32.png");
                        break;
                    case ".dwt":
                        listitem.SmallImage = new IconResourceHandle("FileType.dwt16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.dwt32.png");
                        break;
                    case ".doc":
                    case ".docx":
                        listitem.SmallImage = new IconResourceHandle("FileType.doc16.gif");
                        listitem.LargeImage = new IconResourceHandle("FileType.doc32.gif");
                        break;
                    case ".fh8":
                    case ".fh9":
                        listitem.SmallImage = new IconResourceHandle("FileType.fh16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.fh32.png");
                        break;
                    case ".ind":
                    case ".indd":
                        listitem.SmallImage = new IconResourceHandle("FileType.iddd16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.indd32.png");
                        break;
                    case ".jpg":
                    case ".jpeg":
                        listitem.SmallImage = new IconResourceHandle("FileType.jpg16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.jpg32.png");
                        break;
                    case ".mdb":
                        listitem.SmallImage = new IconResourceHandle("FileType.mdb16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.mdb32.png");
                        break;
                    case ".pdf":
                        listitem.SmallImage = new IconResourceHandle("FileType.pdf16.gif");
                        listitem.LargeImage = new IconResourceHandle("FileType.pdf32.gif");
                        break;
                    case ".pm":
                    case ".pm6":
                    case ".pmd":
                        listitem.SmallImage = new IconResourceHandle("FileType.pmd16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.pmd32.png");
                        break;
                    case ".png":
                        listitem.SmallImage = new IconResourceHandle("FileType.png16.gif");
                        listitem.LargeImage = new IconResourceHandle("FileType.png32.gif");
                        break;
                    case ".psd":
                        listitem.SmallImage = new IconResourceHandle("FileType.psd16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.psd32.png");
                        break;
                    case ".ps":
                        listitem.SmallImage = new IconResourceHandle("FileType.ps16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.ps32.png");
                        break;
                    case ".rar":
                        listitem.SmallImage = new IconResourceHandle("FileType.rar16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.rar32.png");
                        break;
                    case ".sit":
                    case ".sitx":
                        listitem.SmallImage = new IconResourceHandle("FileType.sit16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.sit32.png");
                        break;
                    case ".tif":
                    case ".tiff":
                        listitem.SmallImage = new IconResourceHandle("FileType.tif16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.tif32.png");
                        break;
                    case ".txt":
                        listitem.SmallImage = new IconResourceHandle("FileType.txt16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.txt32.png");
                        break;
                    case ".xls":
                    case ".xlsx":
                        listitem.SmallImage = new IconResourceHandle("FileType.xls16.gif");
                        listitem.LargeImage = new IconResourceHandle("FileType.xls32.gif");
                        break;
                    case ".wma":
                    case ".wmv":
                    case ".avi":
                        listitem.SmallImage = new IconResourceHandle("FileType.wma16.gif");
                        listitem.LargeImage = new IconResourceHandle("FileType.wma32.gif");
                        break;
                    case ".zip":
                        listitem.SmallImage = new IconResourceHandle("FileType.zip16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.zip32.png");
                        break;
                    default:
                        listitem.SmallImage = new IconResourceHandle("FileType.Unknown16.png");
                        listitem.LargeImage = new IconResourceHandle("FileType.Unknown32.png");
                        break;
                }
                #endregion
                listitem.SubItems.Add(fileinfo.Length.ToString("#,##0"));                       // File Size
                listitem.SubItems.Add(contentType);                                             // File Type
                listitem.SubItems.Add(fileinfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));     // Modified On
            }
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        BindFileExplorer();
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwFileExplorer);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwFileExplorer);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvwFileExplorer.CheckBoxes = !this.lvwFileExplorer.CheckBoxes;
                        this.lvwFileExplorer.MultiSelect = this.lvwFileExplorer.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvwFileExplorer.MultiSelect = !this.lvwFileExplorer.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "clientinfo":
                        #region popup Client record
                        xFilm5.Sales.Client.ClientRecord client = new xFilm5.Sales.Client.ClientRecord();
                        client.ClientId = _ClientId;
                        client.EditMode = Common.Enums.EditMode.Edit;
                        client.ShowDialog();
                        #endregion
                        break;
                    case "dropfile":
                        UseUploaderVWG();
                        break;
                    case "retrievefile":
                        #region download file
                        if (lvwFileExplorer.CheckBoxes && lvwFileExplorer.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwFileExplorer.CheckedItems)
                            {
                                _filename = item.Text;
                                Link.Open(new GatewayReference(this, "RetrieveFile"));
                            }
                        }
                        else
                        {
                            if (lvwFileExplorer.SelectedIndex >= 0)
                            {
                                _filename = lvwFileExplorer.SelectedItem.Text;
                                Link.Open(new GatewayReference(this, "RetrieveFile"));
                            }
                        }
                        #endregion
                        break;
                    case "deletefile":
                        MessageBox.Show("Delete file?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                }
            }
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch ((string)e.MenuItem.Tag)
            {
                case "Icon":
                    this.lvwFileExplorer.View = View.SmallIcon;
                    break;
                case "Tile":
                    this.lvwFileExplorer.View = View.LargeIcon;
                    break;
                case "List":
                    this.lvwFileExplorer.View = View.List;
                    break;
                case "Details":
                    this.lvwFileExplorer.View = View.Details;
                    break;
            }
        }

        #region Uploader
        private void UseUploaderClassic()
        {
            fileUpload.Title = "DropBox > Drop File";
            fileUpload.Multiselect = true;
            fileUpload.MaxFileSize = Common.Config.MaxFileSize;
            fileUpload.ShowDialog();
        }

        private void UseUploaderVWG()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
            xFilm5.Controls.Import.UploaderVWG uploader = new xFilm5.Controls.Import.UploaderVWG();

            uploader.Text = oDict.GetWord("dropbox");
            //uploader.UploadedFileType = @"^.*\.(gif|jpe?g|png)$";   // accept gif, jpg, jpeg, png
            uploader.FormClosed += new Form.FormClosedEventHandler(uploader_FormClosed);
            uploader.ShowDialog();
        }

        private void uploader_FormClosed(object sender, FormClosedEventArgs e)
        {
            string FileName = string.Empty;
            string FullName = string.Empty;
            string dropbox = Common.Client.DropBox(_ClientId);

            xFilm5.Controls.Import.UploaderVWG uploader = (xFilm5.Controls.Import.UploaderVWG)sender;
            if (uploader.UploadedFiles.Count > 0)
            {
                foreach (String filepath in uploader.UploadedFiles)
                {
                    #region process uploaded file: 把已經上載到 temporary folder 的檔案抄至指定的檔案夾
                    if (File.Exists(filepath))
                    {
                        //String filename = Path.GetFileName(filepath);       // VWG Uploader 會加個 Guid 在檔案名之前，要將佢去掉
                        //int idx = filename.IndexOf('_');
                        //filename = filename.Substring(idx + 1);
                        String filename = Path.GetFileName(filepath).Substring(37);     // 32 + 4 + 1
                        String dest = Path.Combine(dropbox, filename);
                        File.Copy(filepath, dest, true);
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region ans Button Clicks: cmdDelete
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (DeleteFile())
                    {
                        MessageBox.Show("File deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This file is protected...You can not delete this file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("File is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region DeleteFile()
        private bool DeleteFile()
        {
            string filename = String.Empty;
            string filepath = String.Empty;
            bool result = false;

            if (lvwFileExplorer.CheckBoxes && lvwFileExplorer.CheckedIndices.Count > 0)
            {
                foreach (ListViewItem item in lvwFileExplorer.CheckedItems)
                {
                    bool deleted = false;
                    filename = item.Text;
                    filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), filename);

                    if (File.Exists(filepath))
                    {
                        try
                        {
                            File.Delete(filepath);
                            deleted = true;
                        }
                        catch { };
                    }
                    if (deleted)
                    {
                        item.Remove();
                        result = true;
                    }
                }
            }
            else
            {
                if (lvwFileExplorer.SelectedIndex >= 0)
                {
                    filename = lvwFileExplorer.SelectedItem.Text;
                    filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), filename);

                    if (File.Exists(filepath))
                    {
                        try
                        {
                            File.Delete(filepath);
                            lvwFileExplorer.Items[lvwFileExplorer.SelectedIndex].Remove();
                            result = true;
                        }
                        catch { };
                    }
                }
            }

            return result;
        }
        #endregion

        private void ShowClientDropBox()
        {
            if (ansFileExplorer.Buttons.Count == 0)
            {
                SetFileExplorerAns();
            }
            BindFileExplorer();
        }

        private void tvwClient_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Label.Length > 1)
            {
                Client client = Client.LoadWhere(String.Format("ID = {0}", e.Node.Tag.ToString()));
                if (client != null)
                {
                    _ClientId = client.ID;
                    ShowClientDropBox();
                }
            }
        }

        private void lvwFileExplorer_DoubleClick(object sender, EventArgs e)
        {
            if (lvwFileExplorer.SelectedItem != null)
            {
                _filename = lvwFileExplorer.SelectedItem.Text;
                Link.Open(new GatewayReference(this, "RerieveFile"));
            }
        }

        private void fileUpload_FileOk(object sender, CancelEventArgs e)
        {
            string FileName = string.Empty;
            string FullName = string.Empty;
            string dropbox = Common.Client.DropBox(_ClientId);

            OpenFileDialog oFileDialog = sender as OpenFileDialog;

            switch (oFileDialog.DialogResult)
            {
                case DialogResult.OK:
                    for (int i = 0; i < oFileDialog.Files.Count; i++)
                    {
                        HttpPostedFileHandle file = oFileDialog.Files[i] as HttpPostedFileHandle;
                        if (file.ContentLength > 0)
                        {
                            FileName = Path.GetFileName(file.PostedFileName);
                            FullName = Path.Combine(dropbox, FileName);
                            file.SaveAs(FullName);
                        }
                    }
                    BindFileExplorer();
                    this.Update();
                    break;
            }
        }

        private void lvwFileExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwFileExplorer.MultiSelect && lvwFileExplorer.CheckBoxes)
            {
                foreach (ListViewItem item in lvwFileExplorer.SelectedItems)
                {
                    item.Checked = true;
                }
            }

        }
    }
}