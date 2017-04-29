#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

namespace xFilm5.Accounting
{
    public partial class InvoiceList : UserControl, IGatewayComponent
    {
        private int _ClientId = 0;
        private int _InvoiceId = 0;
        private int _Mode = 0;  //0 = Client Id, 1=Invoice Number, 2 = Invoice Date

        public InvoiceList()
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

            string filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), _InvoiceId);

            if (File.Exists(filepath))
            {
                FileInfo oFile = new FileInfo(filepath);
                if (oFile.Length > 1024 * 1024 * 32)
                {
                    // use this method for file size over 32MB 
                    WriteFileHelper oWriteFile = new WriteFileHelper();
                    oWriteFile.BufferSize = 65536;
                    oWriteFile.WriteFileToResponseStreamWithForceDownloadHeaders(filepath, _InvoiceId.ToString());
                }
                else
                {
                    HttpResponse response = objContext.Response;    // prefer to use Gizmox instead of: this.Context.HttpContext.Response;

                    response.Buffer = true;
                    response.Clear();
                    response.ClearHeaders();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("content-disposition", "attachment; filename=" + _InvoiceId);
                    response.WriteFile(filepath);
                    response.Flush();
                    response.End();
                }
            }
            else
            {
                objContext.Response.Write(String.Format("<html><body><h>File: {0}, file not found!</h></body></html>", _InvoiceId));
            }

            return objGH;
        }
        #endregion

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            tabAdvanced.Text = oDict.GetWord("selection");
            tabClient.Text = oDict.GetWord("client");

            gbxInvoiceNumber.Text = oDict.GetWord("invoice_number");
            gbxInvoiceDate.Text = oDict.GetWord("invoice_date");

            lblInvoiceNumberFrom.Text = oDict.GetWordWithColon("from");
            lblInvoiceNumberTo.Text = oDict.GetWordWithColon("to");
            lblInvoiceDateFrom.Text = oDict.GetWordWithColon("from");
            lblInvoiceDateTo.Text = oDict.GetWordWithColon("to");

            colInoiceNumber.Text = oDict.GetWord("invoice_number");
            colLN.Text = oDict.GetWord("ln");
            colInvoiceDate.Text = oDict.GetWord("invoice_date");
            colInvoiceAmount.Text = oDict.GetWord("invoice_amount");
            colOrderId.Text = oDict.GetWord("order_id");
            colRemarks.Text = oDict.GetWord("remarks");
            colClientName.Text = oDict.GetWord("client_name");
        }

        private void SetAttribute()
        {
            this.lvwInvoiceList.ListViewItemSorter = new ListViewItemSorter(this.lvwInvoiceList);
            lvwInvoiceList.ItemsPerPage = 25;
            lvwInvoiceList.UseInternalPaging = true;
            lvwInvoiceList.GridLines = true;

            toolTip1.SetToolTip(lvwInvoiceList, "Double click to open record");
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
            string where = String.Empty;
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
            this.ansInvoiceList.MenuHandle = false;
            this.ansInvoiceList.DragHandle = false;
            this.ansInvoiceList.TextAlign = ToolBarTextAlign.Right;
        }

        private void SetFileExplorerAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansInvoiceList.MenuHandle = false;
            this.ansInvoiceList.DragHandle = false;
            this.ansInvoiceList.TextAlign = ToolBarTextAlign.Right;
            this.ansInvoiceList.Buttons.Clear();

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansInvoiceList.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansInvoiceList.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansInvoiceList.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansInvoiceList.Buttons[1].ToolTipText = @"Sorting";
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansInvoiceList.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansInvoiceList.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansInvoiceList.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansInvoiceList.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansInvoiceList.Buttons[3].Visible = false;
            #endregion

            this.ansInvoiceList.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansInvoiceList.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansInvoiceList.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansInvoiceList.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansInvoiceList.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansInvoiceList.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansInvoiceList.Buttons[8].Enabled = false;
            this.ansInvoiceList.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
            #endregion

            this.ansInvoiceList.Buttons.Add(sep);

            this.ansInvoiceList.Buttons.Add(new ToolBarButton("ClientInfo", oDict.GetWord("client_info")));
            this.ansInvoiceList.Buttons[10].Image = new IconResourceHandle("16x16.group.png");

            this.ansInvoiceList.Buttons.Add(new ToolBarButton("JobOrder", oDict.GetWord("joborder")));
            this.ansInvoiceList.Buttons[11].Image = new IconResourceHandle("16x16.folder.png");

            this.ansInvoiceList.Buttons.Add(new ToolBarButton("LogFile", oDict.GetWord("log_file")));
            this.ansInvoiceList.Buttons[12].Image = new IconResourceHandle("16x16.note16.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("RetrieveFile", "Download File"));
            //this.ansFileExplorer.Buttons[12].Image = new IconResourceHandle("16x16.downloads.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("DeleteFile", "Delete File"));
            //this.ansFileExplorer.Buttons[13].Image = new IconResourceHandle("16x16.16_L_remove.gif");

            this.ansInvoiceList.Buttons.Add(sep);

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");

            this.ansInvoiceList.Buttons.Add(cmdPopup);
            #endregion
        }
        #endregion

        #region Bind Invoice List
        private void BindInvoiceList()
        {
            this.lvwInvoiceList.Items.Clear();

            int iCount = 1;
            decimal totalDue = 0; decimal curMonth = 0; decimal lastMonth = 0; decimal os2Months = 0; decimal os3Months = 0; decimal osAmount = 0;

            string sql = BuildSqlQueryString();

            //2016.06.29 paulus: 原本以為有 Timeout error，不過似乎係重複 loop 隻 DAL.Client 搵 ClientName 會出見 Timeout
            //  依家改個 View 直接由 SQL 攞
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandTimeout = Common.Config.CommandTimedOut;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sql;
            //SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd);

            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                osAmount = reader.GetDecimal(5);
                totalDue += osAmount;

                ListViewItem objItem = this.lvwInvoiceList.Items.Add(reader.GetInt32(3).ToString());  // Invoice Number
                #region Aging Icon
                bool paid = reader.GetBoolean(15);
                switch (reader.GetInt32(1))                 // OrderTypeID Icons
                {
                    case 0:
                        objItem.SmallImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("16x16.invoice16_0.png");
                        objItem.LargeImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("Icons.32x32.invoice32_0.png");
                        curMonth += paid ? 0 : osAmount;
                        break;
                    case 1:
                        objItem.SmallImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("16x16.invoice16_1.png");
                        objItem.LargeImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("Icons.32x32.invoice32_1.png");
                        lastMonth += paid ? 0 : osAmount;
                        break;
                    case 2:
                        objItem.SmallImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("16x16.invoice16_2.png");
                        objItem.LargeImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("Icons.32x32.invoice32_2.png");
                        os2Months += paid ? 0 : osAmount;
                        break;
                    case 3:
                        objItem.SmallImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("16x16.invoice16_3.png");
                        objItem.LargeImage = paid ? new IconResourceHandle("16x16.document_text-ok.png") : new IconResourceHandle("Icons.32x32.invoice32_3.png");
                        os3Months += paid ? 0 : osAmount;
                        break;
                }
                #endregion
                objItem.SubItems.Add(iCount.ToString());                            // Line Number
//                objItem.SubItems.Add(reader.GetInt32(1).ToString());  // Aging
                objItem.SubItems.Add(reader.GetString(4));                          // Invoice Date
                objItem.SubItems.Add(reader.GetDecimal(5).ToString("#,##0.00"));    // OS Amount
                objItem.SubItems.Add(reader.GetInt32(9).ToString("###"));                // Order ID
                objItem.SubItems.Add(reader.GetString(8));                          // Remarks
                objItem.SubItems.Add(reader.GetInt32(2).ToString());                // Invoice ID
                objItem.SubItems.Add(reader.GetString(14));                         // ClientName

                iCount++;
            }
            reader.Close();
        }

        private string BuildSqlQueryString()
        {
            string where = String.Empty;
            switch (_Mode)
            {
                case 0:
                    where = String.Format("WHERE [ClientID] = {0}", _ClientId.ToString());
                    break;
                case 1:
                    where = String.Format("WHERE [InvoiceNumber] BETWEEN {0} AND {1}", txtInvoiceNumberFrom.Text.Trim(), txtInvoiceNumberTo.Text.Trim());
                    break;
                case 2:
                    where = String.Format("WHERE [InvoiceDate] BETWEEN '{0}' AND '{1}'", dtpInvoiceDateFrom.Value.ToString("yyyy-MM-dd"), dtpInvoiceDateTo.Value.ToString("yyyy-MM-dd"));
                    break;
            }
            string sql = String.Format(@"
DECLARE @CurMonth int
SET @CurMonth = YEAR(GETDATE()) * 12 + MONTH(GETDATE())
SELECT [ClientID]
	  , CASE (@CurMonth - (YEAR([InvoiceDate]) * 12 + MONTH([InvoiceDate])))
			WHEN 0 THEN 0
			WHEN 1 THEN 1
			WHEN 2 THEN 2
			ELSE 3
	    END AS Aging
      ,[InvoiceID]
      ,[InvoiceNumber]
      ,CONVERT(NVARCHAR(10), [InvoiceDate], 120) AS InvoiceDate
      ,[OsAmount]
      ,[InvoiceAmount]
      ,[Status]
      ,[Remarks]
      ,ISNULL([OrderID], 0)
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      ,[ClientName]
      ,[Paid]
FROM  [dbo].[vwInvoiceList_All]
{0}
ORDER BY [InvoiceNumber] DESC
", where);
            return sql;
        }
        #endregion

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        BindInvoiceList();
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwInvoiceList);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwInvoiceList);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvwInvoiceList.CheckBoxes = !this.lvwInvoiceList.CheckBoxes;
                        this.lvwInvoiceList.MultiSelect = this.lvwInvoiceList.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvwInvoiceList.MultiSelect = !this.lvwInvoiceList.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "clientinfo":
                        #region popup Client record
                        xFilm5.Sales.Client.ClientRecord client = new xFilm5.Sales.Client.ClientRecord();
                        int invoiceId = Convert.ToInt32(lvwInvoiceList.SelectedItem.SubItems[0].ToString());
                        string sql = String.Format("InvoiceNumber = {0}", invoiceId.ToString());
                        Acct_INMaster invoice = Acct_INMaster.LoadWhere(sql);
                        if (invoice != null)
                        {
                            client.ClientId = invoice.ClientID;
                            client.EditMode = Common.Enums.EditMode.Edit;
                            client.ShowDialog();
                        }
                        #endregion
                        break;
                    case "joborder":
                        #region popup order form
                        if (lvwInvoiceList.CheckBoxes && lvwInvoiceList.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwInvoiceList.CheckedItems)
                            {
                                ShowJobOrder(Convert.ToInt32(item.SubItems[4].ToString()));
                            }
                        }
                        else
                        {
                            if (lvwInvoiceList.SelectedIndex >= 0)
                            {
                                ShowJobOrder(Convert.ToInt32(lvwInvoiceList.SelectedItem.SubItems[4].ToString()));
                            }
                        }
                        #endregion
                        break;
                    case "logfile":
                        #region popup Log File
                        if (lvwInvoiceList.CheckBoxes && lvwInvoiceList.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwInvoiceList.CheckedItems)
                            {
                                xFilm5.JobOrder.LogFile logfile = new xFilm5.JobOrder.LogFile();
                                logfile.OrderId = Convert.ToInt32(item.SubItems[4].ToString());
                                logfile.Show();
                            }
                        }
                        else
                        {
                            if (lvwInvoiceList.SelectedIndex >= 0)
                            {
                                xFilm5.JobOrder.LogFile logfile = new xFilm5.JobOrder.LogFile();
                                logfile.OrderId = Convert.ToInt32(lvwInvoiceList.SelectedItem.SubItems[4].ToString());
                                logfile.Show();
                            }
                        }
                        #endregion
                        break;
                    case "popup":
                        ShowRecord();
                        break;
                }
            }
        }

        private void ShowRecord()
        {
            if (lvwInvoiceList.SelectedItem != null)
            {
                _InvoiceId = Convert.ToInt32(lvwInvoiceList.SelectedItem.SubItems[6].ToString());
                if (Helper.InvoiceHelper.IsBilling5(_InvoiceId))
                {
                    #region Popup Billing5
                    var inv5 = new xFilm5.JobOrder.Billing5();
                    inv5.InvoiceId = _InvoiceId;

                    // 2017.04.13 paulus: 如果 posted, admin 可以改單
                    if (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin)
                    {
                        inv5.EditMode = Common.Enums.EditMode.Edit;
                    }
                    else
                    {
                        inv5.EditMode = Common.Enums.EditMode.Read;
                    }

                    inv5.Show();
                    #endregion
                }
                else
                {
                    #region Popup Billing
                    var inv = new xFilm5.JobOrder.Billing();
                    inv.InvoiceId = _InvoiceId;

                    // 2017.04.13 paulus: 如果 posted, admin 可以改單
                    if (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin)
                    {
                        inv.EditMode = Common.Enums.EditMode.Edit;
                    }
                    else
                    {
                        inv.EditMode = Common.Enums.EditMode.Read;
                    }

                    inv.Show();
                    #endregion
                }
            }
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch ((string)e.MenuItem.Tag)
            {
                case "Icon":
                    this.lvwInvoiceList.View = View.SmallIcon;
                    break;
                case "Tile":
                    this.lvwInvoiceList.View = View.LargeIcon;
                    break;
                case "List":
                    this.lvwInvoiceList.View = View.List;
                    break;
                case "Details":
                    this.lvwInvoiceList.View = View.Details;
                    break;
            }
        }

        private void ShowJobOrder(int orderId)
        {
            OrderHeader oOrder = OrderHeader.Load(orderId);
            if (oOrder != null)
            {
                switch (oOrder.ServiceType)
                {
                    case (int)Common.Enums.OrderType.UploadFile:
                        xFilm5.JobOrder.Forms.UploadFile oUploadFile = new xFilm5.JobOrder.Forms.UploadFile();
                        oUploadFile.EditMode = Common.Enums.EditMode.Read;
                        oUploadFile.OrderId = orderId;
                        oUploadFile.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.DirectPrint:
                        xFilm5.JobOrder.Forms.DirectPrint oDirectPrint = new xFilm5.JobOrder.Forms.DirectPrint();
                        oDirectPrint.EditMode = Common.Enums.EditMode.Read;
                        oDirectPrint.OrderId = orderId;
                        oDirectPrint.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.PsFile:
                        xFilm5.JobOrder.Forms.PsFile oPsFile = new xFilm5.JobOrder.Forms.PsFile();
                        oPsFile.EditMode = Common.Enums.EditMode.Read;
                        oPsFile.OrderId = orderId;
                        oPsFile.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.Plate:
                        xFilm5.JobOrder.Forms.Plate oPlate = new xFilm5.JobOrder.Forms.Plate();
                        oPlate.EditMode = Common.Enums.EditMode.Read;
                        oPlate.OrderId = orderId;
                        oPlate.ShowDialog();
                        break;
                }
            }
        }

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

            //if (lvwAgingList.CheckBoxes && lvwAgingList.CheckedIndices.Count > 0)
            //{
            //    foreach (ListViewItem item in lvwAgingList.CheckedItems)
            //    {
            //        bool deleted = false;
            //        filename = item.Text;
            //        filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), filename);

            //        if (File.Exists(filepath))
            //        {
            //            try
            //            {
            //                File.Delete(filepath);
            //                deleted = true;
            //            }
            //            catch { };
            //        }
            //        if (deleted)
            //        {
            //            item.Remove();
            //            result = true;
            //        }
            //    }
            //}
            //else
            //{
            //    if (lvwAgingList.SelectedIndex >= 0)
            //    {
            //        filename = lvwAgingList.SelectedItem.Text;
            //        filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), filename);

            //        if (File.Exists(filepath))
            //        {
            //            try
            //            {
            //                File.Delete(filepath);
            //                lvwAgingList.Items[lvwAgingList.SelectedIndex].Remove();
            //                result = true;
            //            }
            //            catch { };
            //        }
            //    }
            //}

            return result;
        }
        #endregion

        private void ShowClientAging()
        {
            if (ansInvoiceList.Buttons.Count == 0)
            {
                SetFileExplorerAns();
            }
            BindInvoiceList();
        }

        private void tvwClient_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Label.Length > 1)
            {
                Client client = Client.LoadWhere(String.Format("ID = {0}", e.Node.Tag.ToString()));
                if (client != null)
                {
                    _ClientId = client.ID;
                    _Mode = 0;
                    ShowClientAging();
                }
            }
        }

        private void lvwFileExplorer_DoubleClick(object sender, EventArgs e)
        {
            ShowRecord();
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
                    BindInvoiceList();
                    this.Update();
                    break;
            }
        }

        private void cmdFindInvoiceNumber_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtInvoiceNumberFrom.Text)) && !(String.IsNullOrEmpty(txtInvoiceNumberTo.Text)))
            {
                _Mode = 1;
                ShowClientAging();
            }
        }

        private void cmdFindInvoiceDate_Click(object sender, EventArgs e)
        {
            if (dtpInvoiceDateFrom.Value <= dtpInvoiceDateTo.Value)
            {
                _Mode = 2;
                ShowClientAging();
            }
        }

        private void lvwInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwInvoiceList.MultiSelect && lvwInvoiceList.CheckBoxes)
            {
                foreach (ListViewItem item in lvwInvoiceList.SelectedItems)
                {
                    item.Checked = true;
                }
            }
        }
    }
}