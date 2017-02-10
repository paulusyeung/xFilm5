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
    public partial class DNList_v5 : UserControl, IGatewayComponent
    {
        private int _ClientId = 0;
        private int _OrderId = 0;
        private int _Mode = 2;  //0 = Client Id, 1=Invoice Number, 2 = Invoice Date

        private ImageList _OrderTypeImageList = xFilm5.Controls.Utility.ImageEx.OrderTypeImages_16px();
        private ImageList _PriorityImageList = xFilm5.Controls.Utility.ImageEx.PriorityImages_16px();
        private ImageList _WorkflowImageList = xFilm5.Controls.Utility.ImageEx.WorkflowImages_16px();

        public DNList_v5()
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

            SetDgvOrderListHeader();
            dgvDNList.RowExpanding += DgvOrderList_RowExpanding;
            dgvDNList.DoubleClick += DgvOrderList_DoubleClick;

            idxSelect.TabPages.Remove(tabAdvanced);     // 唔要
        }

        #region dgvDNList makeups
        private void SetDgvOrderListHeader()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            dgvDNList.AutoGenerateColumns = false;
            dgvDNList.BackgroundColor = Color.WhiteSmoke;
            dgvDNList.RowHeadersVisible = false;
            dgvDNList.ReadOnly = true;
            dgvDNList.Dock = DockStyle.Fill;
            dgvDNList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDNList.AllowUserToAddRows = false;
            dgvDNList.ItemsPerPage = 25;

            DataGridViewColumn colRowNumber = new DataGridViewTextBoxColumn();
            colRowNumber.Width = 32;
            colRowNumber.HeaderText = "#";
            colRowNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRowNumber.DataPropertyName = "RowNumber";

            DataGridViewColumn colFileTypeIcon = new DataGridViewImageColumn();
            colFileTypeIcon.Width = 24;
            colFileTypeIcon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colFileTypeIcon.Name = "colFileTypeIcon";
            colFileTypeIcon.HeaderText = " ";
            //colFileTypeIcon.SortMode = DataGridViewColumnSortMode.NotSortable;

            DataGridViewColumn colReceiptId = new DataGridViewTextBoxColumn();
            colReceiptId.Name = "colReceiptId";
            colReceiptId.HeaderText = oDict.GetWord("delivery_note");
            colReceiptId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReceiptId.DataPropertyName = "ReceiptHeaderId";
            colReceiptId.Width = 80; // colOrderId5.Width * 90 / 100;

            DataGridViewColumn colClientName = new DataGridViewTextBoxColumn();
            colClientName.Name = "colClientName";
            colClientName.HeaderText = oDict.GetWord("client_name");
            colClientName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colClientName.DataPropertyName = "ClientName";
            colClientName.Width = 150;

            DataGridViewColumn colReceiptDate = new DataGridViewTextBoxColumn();
            colReceiptDate.Name = "colReceiptDate";
            colReceiptDate.HeaderText = oDict.GetWord("date");
            colReceiptDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReceiptDate.DataPropertyName = "ReceiptDate";
            colReceiptDate.Width = 70;
            colReceiptDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colReceiptAmount = new DataGridViewTextBoxColumn();
            colReceiptAmount.HeaderText = oDict.GetWord("amount");
            colReceiptAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReceiptAmount.DataPropertyName = "ReceiptAmount";
            colReceiptAmount.Width = 60;
            colReceiptAmount.DefaultCellStyle.Format = "#,###.00;-#,###.00;\"\"";
            colReceiptAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewColumn colPaid = new DataGridViewCheckBoxColumn();
            colPaid.Name = "colPaid";
            colPaid.HeaderText = oDict.GetWord("paid");
            colPaid.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaid.DataPropertyName = "Paid";
            colPaid.Width = 32;
            colPaid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colPaidOn = new DataGridViewTextBoxColumn();
            colPaidOn.Name = "colPaidOn";
            colPaidOn.HeaderText = oDict.GetWord("paid_on");
            colPaidOn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaidOn.DataPropertyName = "PaidOn";
            //colCreatedOn.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            colPaidOn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedOn = new DataGridViewTextBoxColumn();
            colCreatedOn.HeaderText = oDict.GetWord("created_on");
            colCreatedOn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedOn.DataPropertyName = "CreatedOn";
            //colCreatedOn.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            colCreatedOn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedBy = new DataGridViewTextBoxColumn();
            colCreatedBy.HeaderText = oDict.GetWord("created_by");
            colCreatedBy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedBy.DataPropertyName = "CreatedBy";

            dgvDNList.Columns.AddRange(new[] { colRowNumber, colFileTypeIcon, colReceiptId, colClientName, colReceiptDate, colReceiptAmount, colPaid, colPaidOn, colCreatedOn, colCreatedBy });

            dgvDNList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvOrderList_CellFormatting);
        }

        private void dgvOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                DataRowView drv = (DataRowView)dgvDNList.Rows[e.RowIndex].DataBoundItem;
                if (drv != null)
                {
                    DataRow row = (DataRow)drv.Row;
                    
                    switch (dgvDNList.Columns[e.ColumnIndex].Name)
                    {
                        case "colFileTypeIcon":
                            //String ordertype = row["OrderTypeID"].ToString();
                            #region 根據 order type 劃 icon
                            e.Value = new IconResourceHandle("16x16.document_text.png");
                            //switch (int.Parse(ordertype))
                            //{
                            //    case (int)DAL.Common.Enums.OrderType.UploadFile:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.UploadFile.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.DirectPrint:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.DirectPrint.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.PsFile:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.PsFile.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.Others:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Others.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.Plate:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Plate.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.Plate5:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Plate5.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.Film5:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Film5.ToString("g")];
                            //        break;
                            //    case (int)DAL.Common.Enums.OrderType.Vps5:
                            //        e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Vps5.ToString("g")];
                            //        break;
                            //}
                            #endregion
                            break;
                        case "colReceiptDate":
                            String invDate = row["ReceiptDate"].ToString();
                            e.Value = invDate.Contains("1900") ? "" : invDate;
                            break;
                        case "colPaid":
                            bool paid = (bool)row["Paid"];
                            //e.Value = paid ? "Yes" : "";
                            break;
                        case "colPaidOn":
                            String pDate = row["PaidOn"].ToString();
                            e.Value = pDate.Contains("1900") ? "" : pDate;
                            break;
                    }
                }
            }
        }

        private void DgvOrderList_DoubleClick(object sender, EventArgs e)
        {
            DgvOrderList_ShowRecord();
        }

        private void DgvOrderList_RowExpanding(object sender, RowExpandingEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            e.ExpandingRow.ChildGrid.AllowUserToAddRows = false;
            //e.ExpandingRow.ChildGrid.AutoGenerateColumns = false;
            e.ExpandingRow.ChildGrid.BackgroundColor = Color.WhiteSmoke;
            e.ExpandingRow.ChildGrid.ReadOnly = true;
            e.ExpandingRow.ChildGrid.RowHeadersVisible = false;
            e.ExpandingRow.ChildGrid.ColumnHeadersHeight = 12;

            e.ExpandingRow.ChildGrid.Columns[1].Visible = false;
            //e.ExpandingRow.ChildGrid.Columns[5].Visible = false;
            //e.ExpandingRow.ChildGrid.Columns[8].Visible = false;
            e.ExpandingRow.ChildGrid.Columns[0].Width = 30;
            e.ExpandingRow.ChildGrid.Columns[2].Width = 60;
            e.ExpandingRow.ChildGrid.Columns[3].Width = 300;
            e.ExpandingRow.ChildGrid.Columns[4].Width = 40;
            e.ExpandingRow.ChildGrid.Columns[5].Width = 60;
            //e.ExpandingRow.ChildGrid.Columns[6].Width = 60;
            //e.ExpandingRow.ChildGrid.Columns[3].Width = e.ExpandingRow.ChildGrid.Columns[2].Width * 185 / 100;    // 如果用 3 會越 click 越長 :)

            #region 改 row height to 雙行
            //foreach (DataGridViewRow row in e.ExpandingRow.ChildGrid.Rows)
            //{
            //    row.Height = 32;
            //}
            #endregion

            #region 改個別 cells 的 alignment 同 header text
            foreach (DataGridViewColumn col in e.ExpandingRow.ChildGrid.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                switch (col.Index)
                {
                    case 0:
                        col.HeaderText = "#";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 2:
                        col.HeaderText = oDict.GetWord("size");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 3:
                        DataGridViewHeaderCell hd = new DataGridViewHeaderCell();
                        col.HeaderText = oDict.GetWord("order_id") + ": VPS " + oDict.GetWord("file_name");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                        break;
                    case 4:
                        col.HeaderText = oDict.GetWord("qty");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 5:
                        col.HeaderText = oDict.GetWord("amount");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        break;
                    //case 6:
                    //    col.HeaderText = oDict.GetWord("plate_size");
                    //    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    //    break;
                    //case 7:
                    //    col.HeaderText = oDict.GetWord("created_on");
                    //    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    //    break;
                    //case 8:
                    //    col.HeaderText = oDict.GetWord("printed_on");
                    //    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    //    break;
                }
            }
            #endregion
        }

        private void BindDgvOrderList()
        {
            String sql = BuildSqlQueryString();

            FilterRelation fr = new FilterRelation();
            fr.SourceColumnDataName = "ReceiptHeaderId";
            fr.TargetColumnDataName = "ReceiptHeaderId";

            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);

            //DataSet ds = GetOrderDS();                                  // DataGridView Hierarchy forum 話要用 Dataset，所以 convert object to dataset
            DataSet ds2 = GetDgvOrderItemsDS();

            BindingSource bsMaster = new BindingSource();
            BindingSource bsDetails = new BindingSource();

            bsMaster.DataSource = ds.Tables[0];
            bsDetails.DataSource = ds2.Tables[0];

            dgvDNList.DataSource = bsMaster;                           // bind Header
            dgvDNItems.BindedSource = bsDetails;                          // bind Details
            dgvDNItems.FilteringDataMembers.Add(fr);                      // master details 嘅 relationship

            //SetDgvLayout_Master();
        }

        private DataSet GetDgvOrderItemsDS()
        {
            String sql = String.Format(@"

SELECT TOP 100 PERCENT
	   ROW_NUMBER() OVER (PARTITION BY [ReceiptHeaderId] ORDER By [ReceiptHeaderId], [ItemDescription]) as RowNumber
--       [ClientId]
--      ,[ClientName]
--      ,[ClientAddress]
--      ,[ClientTel]
--      ,[ClientFax]
      ,[ReceiptHeaderId]
--      ,[ReceiptNumber]
--      ,CONVERT(VARCHAR(10), [ReceiptDate], 120) AS ReceiptDate
--      ,[ReceiptAmount]
--      ,[PaymentType]
--      ,[INMasterId]
--      ,[ClientUserId]
--      ,[ClientUserName]
--      ,[Paid]
--      ,CONVERT(VARCHAR(16), [PaidOn], 120) AS PaidOn
--      ,[PaidAmount]
--      ,[PaidRef]
--      ,[Remarks]
--      ,[Status]
--      ,CONVERT(VARCHAR(16), [CreatedOn], 120) AS CreatedOn
--      ,[CreatedBy]
--      ,CONVERT(VARCHAR(16), [ModifiedOn], 120) AS ModifiedOn
--      ,[ModifiedBy]
--      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
--      ,[ItemUoM]
--      ,[ItemUnitAmt]
--      ,[ItemDiscount]
--      ,[ItemAmount]
        ,convert(varchar, cast([ItemAmount] as money), 1) AS ItemAmount
--      ,[OrderPkPrintQueueVpsId]
--      ,[OrderHeaderId]
--	  ,ROW_NUMBER() OVER (ORDER BY [ReceiptHeaderId] DESC) AS RowNumber
FROM [dbo].[vwReceiptDetailsList]
WHERE [ReceiptHeaderId] IN
(
SELECT DISTINCT
      ReceiptHeaderId
FROM [vwReceiptDetailsList]
WHERE [ClientID] = {0}
)
ORDER BY [ReceiptHeaderId], [ItemDescription] 
", _ClientId.ToString());
            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);
            return ds;
        }

        private void DgvOrderList_ShowRecord()
        {
            if (dgvDNList.SelectedRows.Count > 0)
            {
                DataRowView drv = (DataRowView)dgvDNList.SelectedRows[0].DataBoundItem;
                if (drv != null)
                {
                    DataRow row = (DataRow)drv.Row;
                    int id = (int)row["OrderID"];

                    ShowJobOrder(id);
                }
            }
        }
        #endregion

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
            tabClient.Text = oDict.GetWord("client");

            gbxOrderId.Text = oDict.GetWord("order_id");
            gbxOrderedOn.Text = oDict.GetWord("ordered_on");

            lblOrderIdFrom.Text = oDict.GetWordWithColon("from");
            lblOrderIdTo.Text = oDict.GetWordWithColon("to");
            lblOrderedOnFrom.Text = oDict.GetWordWithColon("from");
            lblOrderedOnTo.Text = oDict.GetWordWithColon("to");

            lblMonth.Text = oDict.GetWordWithColon("month");
        }

        private void SetAttribute()
        {
            dgvDNList.Dock = DockStyle.Fill;
            dgvDNList.Visible = true;
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
        }

        private void ResetClientTreeAns()
        {
            this.ansClientTree.MenuHandle = false;
            this.ansClientTree.DragHandle = false;
            this.ansClientTree.TextAlign = ToolBarTextAlign.Right;
        }
        #endregion

        #region Bind Order List
        private string BuildSqlQueryString()
        {
            DateTime selDate = dtpSelectedDate.Value;
            var firstDay = new DateTime(selDate.Year, selDate.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            string where = String.Empty;
            switch (_Mode)
            {
                case 0:
                    //where = String.Format("WHERE [ClientID] = {0}", _ClientId.ToString());
                    where = String.Format("WHERE ([ClientID] = {0}) AND ([ReceiptDate] BETWEEN '{1}' AND '{2}')", _ClientId.ToString(), firstDay.ToString("yyyy-MM-dd"), lastDay.ToString("yyyy-MM-dd"));
                    break;
                case 1:
                    where = String.Format("WHERE [ReceiptHeaderId] BETWEEN {0} AND {1}", txtOrderIdFrom.Text.Trim(), txtOrderIdTo.Text.Trim());
                    break;
                case 2:
                    //where = String.Format("WHERE [ReceiptDate] BETWEEN '{0}' AND '{1}'", dtpOrderedOnFrom.Value.ToString("yyyy-MM-dd"), dtpOrderedOnTo.Value.ToString("yyyy-MM-dd"));
                    where = String.Format("WHERE [ReceiptDate] BETWEEN '{0}' AND '{1}'", firstDay.ToString("yyyy-MM-dd"), lastDay.ToString("yyyy-MM-dd"));
                    break;
            }
            string sql = String.Format(@"
Select *, ROW_NUMBER() OVER (ORDER BY [ReceiptHeaderId] DESC) AS RowNumber
From
(
SELECT TOP 100 PERCENT
       ROW_NUMBER() OVER (PARTITION BY [ReceiptHeaderId] ORDER By [ReceiptHeaderId], [ItemDescription]) as Rn
      ,[ClientId]
      ,[ClientName]
--      ,[ClientAddress]
--      ,[ClientTel]
--      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
--      ,[ReceiptDate]
      ,CONVERT(VARCHAR(10), [ReceiptDate], 120) AS ReceiptDate
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
--      ,[ClientUserId]
--      ,[ClientUserName]
      ,[Paid]
--      ,[PaidOn]
      ,CONVERT(VARCHAR(16), [PaidOn], 120) AS PaidOn
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
--      ,[CreatedOn]
      ,CONVERT(VARCHAR(16), [CreatedOn], 120) AS CreatedOn
      ,[CreatedBy]
--      ,[ModifiedOn]
      ,CONVERT(VARCHAR(16), [ModifiedOn], 120) AS ModifiedOn
      ,[ModifiedBy]
--      ,[ReceiptDetailId]
--      ,[ItemCode]
--      ,[ItemDescription]
--      ,[ItemQty]
--      ,[ItemUoM]
--      ,[ItemUnitAmt]
--      ,[ItemDiscount]
--      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
--	  ,ROW_NUMBER() OVER (ORDER BY [ReceiptHeaderId] DESC) AS RowNumber
FROM [dbo].[vwReceiptDetailsList]
{0}
ORDER BY [ReceiptHeaderId] DESC
) a
WHERE Rn = 1
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
                        //BindOrderList();
                        //this.Update();
                        break;
                    case "columns":
                        //ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwOrderList);
                        //objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        //ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwOrderList);
                        //objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        //this.lvwOrderList.CheckBoxes = !this.lvwOrderList.CheckBoxes;
                        //this.lvwOrderList.MultiSelect = this.lvwOrderList.CheckBoxes;
                        break;
                    case "multiselect":
                        //this.lvwOrderList.MultiSelect = !this.lvwOrderList.MultiSelect;
                        //e.Button.Pushed = true;
                        break;
                    case "clientinfo":
                        #region popup Client record
                        if (dgvDNList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvDNList.SelectedRows[0].DataBoundItem;
                            if (drv != null)
                            {
                                DataRow row = (DataRow)drv.Row;
                                String orderId = row["OrderID"].ToString();
                                String clientId = row["ClientID"].ToString();

                                if (!String.IsNullOrEmpty(clientId))
                                {
                                    xFilm5.Sales.Client.ClientRecord client = new xFilm5.Sales.Client.ClientRecord();
                                    client.ClientId = int.Parse(clientId);
                                    client.EditMode = Common.Enums.EditMode.Edit;
                                    client.Show();
                                }
                            }
                        }
                        #endregion
                        break;
                    case "invoice":
                        #region popup Invoice
                        if (dgvDNList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvDNList.SelectedRows[0].DataBoundItem;
                            if (drv != null)
                            {
                                DataRow row = (DataRow)drv.Row;
                                String orderId = row["OrderID"].ToString();

                                if (!String.IsNullOrEmpty(orderId))
                                {
                                    xFilm5.JobOrder.Billing billing = new xFilm5.JobOrder.Billing();
                                    billing.OrderId = int.Parse(orderId);
                                    billing.EditMode = Common.Enums.EditMode.Read;
                                    billing.Show();
                                }
                            }
                        }
                        #endregion
                        break;
                    case "logfile":
                        #region popup Log File
                        if (dgvDNList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvDNList.SelectedRows[0].DataBoundItem;
                            if (drv != null)
                            {
                                DataRow row = (DataRow)drv.Row;
                                String orderId = row["OrderID"].ToString();

                                if (!String.IsNullOrEmpty(orderId))
                                {
                                    xFilm5.JobOrder.LogFile logfile = new xFilm5.JobOrder.LogFile();
                                    logfile.OrderId = int.Parse(orderId);
                                    logfile.Show();
                                }
                            }
                        }
                        #endregion
                        break;
                    case "popup":
                        #region popup Order
                        if (dgvDNList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvDNList.SelectedRows[0].DataBoundItem;
                            if (drv != null)
                            {
                                DataRow row = (DataRow)drv.Row;
                                String orderId = row["OrderID"].ToString();

                                if (!String.IsNullOrEmpty(orderId))
                                {
                                    ShowJobOrder(int.Parse(orderId));
                                }
                            }
                        }
                        #endregion
                        //if (lvwOrderList.SelectedItem != null)
                        //{
                        //    int orderId = Convert.ToInt32(lvwOrderList.SelectedItem.Text);
                        //    ShowJobOrder(orderId);
                        //}
                        break;
                }
            }
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            //switch ((string)e.MenuItem.Tag)
            //{
            //    case "Icon":
            //        this.lvwOrderList.View = View.SmallIcon;
            //        break;
            //    case "Tile":
            //        this.lvwOrderList.View = View.LargeIcon;
            //        break;
            //    case "List":
            //        this.lvwOrderList.View = View.List;
            //        break;
            //    case "Details":
            //        this.lvwOrderList.View = View.Details;
            //        break;
            //}
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
                    case (int)Common.Enums.OrderType.Vps5:
                        xFilm5.JobOrder.Forms.Vps5 oVps5 = new xFilm5.JobOrder.Forms.Vps5();
                        oVps5.EditMode = Common.Enums.EditMode.Edit;
                        oVps5.OrderId = orderId;
                        oVps5.ShowDialog();
                        break;
                }
            }
        }

        private void ShowClientOrderList()
        {
            BindDgvOrderList();
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
                    ShowClientOrderList();
                }
            }
        }

        private void cmdFindOrderId_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtOrderIdFrom.Text)) && !(String.IsNullOrEmpty(txtOrderIdTo.Text)))
            {
                _Mode = 1;
                ShowClientOrderList();
            }
        }

        private void cmdFindOrderedOn_Click(object sender, EventArgs e)
        {
            if (dtpOrderedOnFrom.Value <= dtpOrderedOnTo.Value)
            {
                _Mode = 2;
                ShowClientOrderList();
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            BindDgvOrderList();
        }
    }
}