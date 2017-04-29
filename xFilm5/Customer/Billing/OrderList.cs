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

namespace xFilm5.Customer.Billing
{
    public partial class OrderList : UserControl, IGatewayComponent
    {
        private int _ClientId = 0;
        private int _OrderId = 0;
        private int _Mode = 0;  //0 = Client Id, 1=Invoice Number, 2 = Invoice Date

        public OrderList()
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

            _ClientId = xFilm5.Controls.Utility.User.GetClientId();
            ShowClientAging();
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

            string filepath = String.Format(@"{0}\{1}", Common.Client.DropBox(_ClientId), _OrderId);

            if (File.Exists(filepath))
            {
                FileInfo oFile = new FileInfo(filepath);
                if (oFile.Length > 1024 * 1024 * 32)
                {
                    // use this method for file size over 32MB 
                    WriteFileHelper oWriteFile = new WriteFileHelper();
                    oWriteFile.BufferSize = 65536;
                    oWriteFile.WriteFileToResponseStreamWithForceDownloadHeaders(filepath, _OrderId.ToString());
                }
                else
                {
                    HttpResponse response = objContext.Response;    // prefer to use Gizmox instead of: this.Context.HttpContext.Response;

                    response.Buffer = true;
                    response.Clear();
                    response.ClearHeaders();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("content-disposition", "attachment; filename=" + _OrderId);
                    response.WriteFile(filepath);
                    response.Flush();
                    response.End();
                }
            }
            else
            {
                objContext.Response.Write(String.Format("<html><body><h>File: {0}, file not found!</h></body></html>", _OrderId));
            }

            return objGH;
        }
        #endregion

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            tabAdvanced.Text = oDict.GetWord("selection");
            gbxOrderId.Text = oDict.GetWord("order_id");
            gbxOrderedOn.Text = oDict.GetWord("ordered_on");

            lblOrderIdFrom.Text = oDict.GetWordWithColon("from");
            lblOrderIdTo.Text = oDict.GetWordWithColon("to");
            lblOrderedOnFrom.Text = oDict.GetWordWithColon("from");
            lblOrderedOnTo.Text = oDict.GetWordWithColon("to");

            colOrderId.Text = oDict.GetWord("order_id");
            colLN.Text = oDict.GetWord("ln");
            colClientName.Text = oDict.GetWord("client_name");
            colAttachment.Text = oDict.GetWord("attachment");
            colReceivedOn.Text = oDict.GetWord("received_on");
            colCompletedOn.Text = oDict.GetWord("completed_on");
            colWorkshop.Text = oDict.GetWord("workshop");
            colInvoiceNumber.Text = oDict.GetWord("invoice_number");
            colInvoiceDate.Text = oDict.GetWord("invoice_date");
            colInvoiceAmount.Text = oDict.GetWord("invoice_amount");
            colPaid.Text = oDict.GetWord("paid");
            colRemarks.Text = oDict.GetWord("remarks");
        }
        private void SetAttribute()
        {
            this.lvwOrderList.ListViewItemSorter = new ListViewItemSorter(this.lvwOrderList);

            toolTip1.SetToolTip(lvwOrderList, "Double click to open record");
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
            this.ansOrderList.MenuHandle = false;
            this.ansOrderList.DragHandle = false;
            this.ansOrderList.TextAlign = ToolBarTextAlign.Right;
        }

        private void SetFileExplorerAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansOrderList.MenuHandle = false;
            this.ansOrderList.DragHandle = false;
            this.ansOrderList.TextAlign = ToolBarTextAlign.Right;
            this.ansOrderList.Buttons.Clear();

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansOrderList.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansOrderList.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansOrderList.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansOrderList.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansOrderList.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansOrderList.Buttons[1].ToolTipText = @"Sorting";
            this.ansOrderList.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansOrderList.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansOrderList.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansOrderList.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansOrderList.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansOrderList.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansOrderList.Buttons[3].Visible = false;
            #endregion

            this.ansOrderList.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansOrderList.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansOrderList.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansOrderList.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansOrderList.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansOrderList.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansOrderList.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansOrderList.Buttons[8].Enabled = false;
            this.ansOrderList.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
            #endregion

            this.ansOrderList.Buttons.Add(sep);

            //this.ansOrderList.Buttons.Add(new ToolBarButton("ClientInfo", "Client Info"));
            //this.ansOrderList.Buttons[10].Image = new IconResourceHandle("16x16.group.png");

            this.ansOrderList.Buttons.Add(new ToolBarButton("Invoice", oDict.GetWord("invoice")));
            this.ansOrderList.Buttons[10].Image = new IconResourceHandle("16x16.jobOrder_Invoice.png");

            this.ansOrderList.Buttons.Add(new ToolBarButton("LogFile", oDict.GetWord("log_file")));
            this.ansOrderList.Buttons[11].Image = new IconResourceHandle("16x16.note16.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("RetrieveFile", "Download File"));
            //this.ansFileExplorer.Buttons[12].Image = new IconResourceHandle("16x16.downloads.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("DeleteFile", "Delete File"));
            //this.ansFileExplorer.Buttons[13].Image = new IconResourceHandle("16x16.16_L_remove.gif");

            this.ansOrderList.Buttons.Add(sep);
        }
        #endregion

        #region Bind Order List
        private void BindOrderList()
        {
            this.lvwOrderList.Items.Clear();

            int iCount = 1;
            decimal totalDue = 0; decimal curMonth = 0; decimal lastMonth = 0; decimal os2Months = 0; decimal os3Months = 0; decimal osAmount = 0;

            string sql = BuildSqlQueryString();
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                ListViewItem objItem = this.lvwOrderList.Items.Add(reader.GetInt32(0).ToString());  // Order Number
                #region Order Type Icon
                switch (reader.GetInt32(9))                 // OrderTypeID Icons
                {
                    case (int)Common.Enums.OrderType.UploadFile:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.UploadFile_16.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.UploadFile_32.png");
                        break;
                    case (int)Common.Enums.OrderType.DirectPrint:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.DirectPrint_16.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.DirectPrint_32.png");
                        break;
                    case (int)Common.Enums.OrderType.PsFile:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.PsFile_16.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.PsFile_32.png");
                        break;
                    case (int)Common.Enums.OrderType.Plate:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.Others_16.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.Others_32.png");
                        break;
                    case (int)Common.Enums.OrderType.Plate5:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.folder_p.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.folder_p.png");
                        break;
                    case (int)Common.Enums.OrderType.Film5:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.folder_f.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.folder_f.png");
                        break;
                    case (int)Common.Enums.OrderType.Others:
                        objItem.SmallImage = new IconResourceHandle("JobOrder.Others_16.png");
                        objItem.LargeImage = new IconResourceHandle("JobOrder.Others_32.png");
                        break;
                }
                #endregion
                #region Priority Icon
                switch (reader.GetInt32(3))                 // PriorityID Icons
                {
                    case (int)Common.Enums.Priority.Regular:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.flag_green.png").ToString());
                        break;
                    case (int)Common.Enums.Priority.Express:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.flag_yellow.png").ToString());
                        break;
                    case (int)Common.Enums.Priority.Rush:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.flag_red.png").ToString());
                        break;
                }
                #endregion
                #region Status Icon
                switch (reader.GetInt32(15))
                {
                    case (int)Common.Enums.Workflow.Queuing:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_queuing.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Retouch:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_retouch.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Printing:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_printing.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.ProofingOutgoing:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_proofingout.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.ProofingIncoming:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_proofingin.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Ready:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_ready.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Dispatch:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_dispatch.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Completed:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_completed.png").ToString());
                        break;
                    case (int)Common.Enums.Workflow.Cancelled:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.folder_cancelled.png").ToString());
                        break;
                }
                #endregion
                #region Deliver Icon
                switch (reader.GetInt32(17))
                {
                    case (int)Common.Enums.DeliveryMethod.PickUp:
                        objItem.SubItems.Add(String.Empty);
                        break;
                    case (int)Common.Enums.DeliveryMethod.DeliverTo:
                        objItem.SubItems.Add(new IconResourceHandle("16x16.jobOrder_deliverto.png").ToString());
                        break;
                }
                #endregion
                #region Comment Icon
                if (String.IsNullOrEmpty(reader.GetString(18)))
                {
                    objItem.SubItems.Add(String.Empty);
                }
                else
                {
                    objItem.SubItems.Add(new IconResourceHandle("16x16.jobOrder_comment.png").ToString());
                }
                #endregion
                objItem.SubItems.Add(iCount.ToString());    // Line Number
                objItem.SubItems.Add(reader.GetString(2));  // Client Name
                objItem.SubItems.Add(reader.GetString(5));  // Attachment
                objItem.SubItems.Add(reader.GetString(6));  // Remarks
                objItem.SubItems.Add(reader.GetString(7));  // Received On
                #region Completed On
                string completedOn = reader.GetString(8);
                if (!(String.IsNullOrEmpty(completedOn)))
                {
                    if (completedOn.Substring(0, 4) == "1900")
                    {
                        completedOn = String.Empty;
                    }
                }
                objItem.SubItems.Add(completedOn);
                #endregion
                objItem.SubItems.Add(reader.GetString(10)); // Order Type
                objItem.SubItems.Add(reader.GetString(11)); // Order By
                objItem.SubItems.Add(reader.GetString(12)); // Prepress Op
                objItem.SubItems.Add(reader.GetString(13)); // Retouch By
                objItem.SubItems.Add(reader.GetString(14)); // Workshop
                objItem.SubItems.Add(reader.GetInt32(19).ToString("###")); // Invoice Number
                objItem.SubItems.Add(reader.GetString(20)); // Invoice Date
                #region Invoice Amount
                decimal invoiceAmt = reader.GetDecimal(21);
                if (invoiceAmt == 0)
                {
                    objItem.SubItems.Add(String.Empty);
                }
                else
                {
                    objItem.SubItems.Add(invoiceAmt.ToString("#,###.00"));
                }
                #endregion
                #region Paid Icon
                if (reader.GetBoolean(24))
                {
                    objItem.SubItems.Add(new IconResourceHandle("16x16.tick14.png").ToString());
                }
                else
                {
                    objItem.SubItems.Add(String.Empty);
                }
                #endregion

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
                    where = String.Format("WHERE [ClientID] = {0} AND [OrderID] BETWEEN {1} AND {2}",
                        _ClientId.ToString(),
                        txtOrderIdFrom.Text.Trim(),
                        txtOrderIdTo.Text.Trim());
                    break;
                case 2:
                    where = String.Format("WHERE [ClientID] = {0} AND [DateReceived] BETWEEN '{1}' AND '{2}'",
                        _ClientId.ToString(),
                        dtpOrderedOnFrom.Value.ToString("yyyy-MM-dd"),
                        dtpOrderedOnTo.Value.ToString("yyyy-MM-dd"));
                    break;
            }
            string sql = String.Format(@"
SELECT TOP 100 PERCENT [OrderID], [ClientID], [ClientName], [PriorityID], [Priority]
        ,[Attachment], [Remarks], [DateReceived], ISNULL([DateCompleted], ''), [OrderTypeID]
        ,[OrderType], [OrderedBy], ISNULL([PrePressOp], ''), ISNULL([RetouchBy], ''), ISNULL([Workshop], '')
        ,[StatusID], [Status], ISNULL([DeliveryMethod], 1), ISNULL([Comment], '')
        ,ISNULL([InvoiceNumber], '') AS InvoiceNumber
        ,ISNULL([InvoiceDate], '')
        ,ISNULL([InvoiceAmount], 0)
        ,ISNULL([PaymentType], 0)
        ,ISNULL([PaymentTypeName], '')
        ,ISNULL([Paid], 0)
        ,ISNULL([PaidOn], '')
        ,ISNULL([PaidAmount], 0)
        ,ISNULL([PaidRef], '')
        ,ISNULL([CreatedBy], '')
        ,ISNULL([CreatedOn], '')
        ,ISNULL([LastModifiedBy], '')
        ,ISNULL([LastModifiedOn], '')
        ,ISNULL([InvoiceStatus], 0)
FROM    [dbo].[vwOrderList_Invoice]
{0}
ORDER BY [OrderID] DESC
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
                        BindOrderList();
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwOrderList);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwOrderList);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvwOrderList.CheckBoxes = !this.lvwOrderList.CheckBoxes;
                        this.lvwOrderList.MultiSelect = this.lvwOrderList.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvwOrderList.MultiSelect = !this.lvwOrderList.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "clientinfo":
                        #region popup Client record
                        if (lvwOrderList.CheckBoxes && lvwOrderList.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwOrderList.CheckedItems)
                            {
                                OrderHeader order = OrderHeader.Load(Convert.ToInt32(item.Text));
                                if (order != null)
                                {
                                    xFilm5.Sales.Client.ClientRecord client = new xFilm5.Sales.Client.ClientRecord();
                                    client.ClientId = order.ClientID;
                                    client.EditMode = Common.Enums.EditMode.Edit;
                                    client.Show();
                                }
                            }
                        }
                        else
                        {
                            if (lvwOrderList.SelectedIndex >= 0)
                            {
                                OrderHeader order = OrderHeader.Load(Convert.ToInt32(lvwOrderList.SelectedItem.Text));
                                if (order != null)
                                {
                                    xFilm5.Sales.Client.ClientRecord client = new xFilm5.Sales.Client.ClientRecord();
                                    client.ClientId = order.ClientID;
                                    client.EditMode = Common.Enums.EditMode.Edit;
                                    client.Show();
                                }
                            }
                        }
                        #endregion
                        break;
                    case "invoice":
                        #region popup Invoice
                        if (lvwOrderList.CheckBoxes && lvwOrderList.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwOrderList.CheckedItems)
                            {
                                xFilm5.JobOrder.Billing billing = new xFilm5.JobOrder.Billing();
                                billing.OrderId = Convert.ToInt32(item.Text);
                                billing.EditMode = Common.Enums.EditMode.Read;
                                billing.Show();
                            }
                        }
                        else
                        {
                            if (lvwOrderList.SelectedIndex >= 0)
                            {
                                xFilm5.JobOrder.Billing billing = new xFilm5.JobOrder.Billing();
                                billing.OrderId = Convert.ToInt32(lvwOrderList.SelectedItem.Text);
                                billing.EditMode = Common.Enums.EditMode.Read;
                                billing.Show();
                            }
                        }
                        #endregion
                        break;
                    case "logfile":
                        #region popup Log File
                        if (lvwOrderList.CheckBoxes && lvwOrderList.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwOrderList.CheckedItems)
                            {
                                xFilm5.JobOrder.LogFile logfile = new xFilm5.JobOrder.LogFile();
                                logfile.OrderId = Convert.ToInt32(item.Text);
                                logfile.Show();
                            }
                        }
                        else
                        {
                            if (lvwOrderList.SelectedIndex >= 0)
                            {
                                xFilm5.JobOrder.LogFile logfile = new xFilm5.JobOrder.LogFile();
                                logfile.OrderId = Convert.ToInt32(lvwOrderList.SelectedItem.Text);
                                logfile.Show();
                            }
                        }
                        #endregion
                        break;
                }
            }
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch ((string)e.MenuItem.Tag)
            {
                case "Icon":
                    this.lvwOrderList.View = View.SmallIcon;
                    break;
                case "Tile":
                    this.lvwOrderList.View = View.LargeIcon;
                    break;
                case "List":
                    this.lvwOrderList.View = View.List;
                    break;
                case "Details":
                    this.lvwOrderList.View = View.Details;
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
                    case (int)Common.Enums.OrderType.Plate5:
                        xFilm5.JobOrder.Forms.Plate5 oPlate5 = new xFilm5.JobOrder.Forms.Plate5();
                        oPlate5.EditMode = Common.Enums.EditMode.Edit;
                        oPlate5.OrderId = orderId;
                        oPlate5.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.Film5:
                        xFilm5.JobOrder.Forms.Film5 oFilm5 = new xFilm5.JobOrder.Forms.Film5();
                        oFilm5.EditMode = Common.Enums.EditMode.Edit;
                        oFilm5.OrderId = orderId;
                        oFilm5.ShowDialog();
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
            if (ansOrderList.Buttons.Count == 0)
            {
                SetFileExplorerAns();
            }
            BindOrderList();
        }

        private void lvwOrderList_DoubleClick(object sender, EventArgs e)
        {
            if (lvwOrderList.SelectedItem != null)
            {
                int orderId = Convert.ToInt32(lvwOrderList.SelectedItem.Text);
                ShowJobOrder(orderId);
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
                    BindOrderList();
                    this.Update();
                    break;
            }
        }

        private void cmdFindOrderId_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtOrderIdFrom.Text)) && !(String.IsNullOrEmpty(txtOrderIdTo.Text)))
            {
                _Mode = 1;
                ShowClientAging();
            }
        }

        private void cmdFindOrderedOn_Click(object sender, EventArgs e)
        {
            if (dtpOrderedOnFrom.Value <= dtpOrderedOnTo.Value)
            {
                _Mode = 2;
                ShowClientAging();
            }
        }

        private void lvwOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwOrderList.MultiSelect && lvwOrderList.CheckBoxes)
            {
                foreach (ListViewItem item in lvwOrderList.SelectedItems)
                {
                    item.Checked = true;
                }
            }
        }
    }
}