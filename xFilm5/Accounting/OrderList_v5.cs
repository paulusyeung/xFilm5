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
    public partial class OrderList_v5 : UserControl, IGatewayComponent
    {
        private int _ClientId = 0;
        private int _OrderId = 0;
        private int _Mode = 0;  //0 = Client Id, 1=Invoice Number, 2 = Invoice Date

        private ImageList _OrderTypeImageList = xFilm5.Controls.Utility.ImageEx.OrderTypeImages_16px();
        private ImageList _PriorityImageList = xFilm5.Controls.Utility.ImageEx.PriorityImages_16px();
        private ImageList _WorkflowImageList = xFilm5.Controls.Utility.ImageEx.WorkflowImages_16px();

        public OrderList_v5()
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
            dgvOrderList.RowExpanding += DgvOrderList_RowExpanding;
            dgvOrderList.DoubleClick += DgvOrderList_DoubleClick;
        }

        #region dgvOrderList makeups
        private void SetDgvOrderListHeader()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            dgvOrderList.AutoGenerateColumns = false;
            dgvOrderList.BackgroundColor = Color.WhiteSmoke;
            dgvOrderList.RowHeadersVisible = false;
            dgvOrderList.ReadOnly = true;
            dgvOrderList.Dock = DockStyle.Fill;
            dgvOrderList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderList.AllowUserToAddRows = false;
            dgvOrderList.ItemsPerPage = 25;

            DataGridViewColumn colFileTypeIcon = new DataGridViewImageColumn();
            colFileTypeIcon.Width = 24;
            colFileTypeIcon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colFileTypeIcon.Name = "colFileTypeIcon";
            colFileTypeIcon.HeaderText = " ";
            //colFileTypeIcon.SortMode = DataGridViewColumnSortMode.NotSortable;

            DataGridViewColumn colOrderId5 = new DataGridViewTextBoxColumn();
            colOrderId5.Name = "colOrderId";
            colOrderId5.HeaderText = oDict.GetWord("order_id");
            colOrderId5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOrderId5.DataPropertyName = "OrderID";
            colOrderId5.Width = 60; // colOrderId5.Width * 90 / 100;

            DataGridViewColumn colRowNumber = new DataGridViewTextBoxColumn();
            colRowNumber.Width = 32;
            colRowNumber.HeaderText = "#";
            colRowNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRowNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRowNumber.DataPropertyName = "RowNumber";

            DataGridViewColumn colPriority5 = new DataGridViewImageColumn();
            colPriority5.Name = "colPriority";
            colPriority5.Width = 24;
            colPriority5.HeaderCell = xFilm5.Controls.DataGridViewExtension.LoadIcon("16x16.flag_grey.png");

            DataGridViewColumn colStatus5 = new DataGridViewImageColumn();
            colStatus5.Name = "colStatus";
            colStatus5.Width = 24;
            colStatus5.HeaderCell = xFilm5.Controls.DataGridViewExtension.LoadIcon("16x16.folder_grey.png");

            DataGridViewColumn colDelivery5 = new DataGridViewImageColumn();
            colDelivery5.Name = "colDelivery";
            colDelivery5.Width = 24;
            colDelivery5.HeaderCell = xFilm5.Controls.DataGridViewExtension.LoadIcon("16x16.jobOrder_delivery.png");

            DataGridViewColumn colComment5 = new DataGridViewImageColumn();
            colComment5.Name = "colComment";
            colComment5.Width = 24;
            colComment5.HeaderCell = xFilm5.Controls.DataGridViewExtension.LoadIcon("16x16.jobOrder_commentgrey.png");

            DataGridViewColumn colClientName5 = new DataGridViewTextBoxColumn();
            colClientName5.HeaderText = oDict.GetWord("client_name");
            colClientName5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colClientName5.DataPropertyName = "ClientName";

            DataGridViewColumn colAttachment5 = new DataGridViewTextBoxColumn();
            colAttachment5.HeaderText = oDict.GetWord("attachment");
            colAttachment5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAttachment5.DataPropertyName = "Attachment";

            DataGridViewColumn colRemarks5 = new DataGridViewTextBoxColumn();
            colRemarks5.Name = "colRemarks5";
            colRemarks5.HeaderText = oDict.GetWord("remarks");
            colRemarks5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRemarks5.DataPropertyName = "Remarks";

            DataGridViewColumn colReceivedOn5 = new DataGridViewTextBoxColumn();
            colReceivedOn5.HeaderText = oDict.GetWord("received_on");
            colReceivedOn5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReceivedOn5.DataPropertyName = "DateReceived";

            DataGridViewColumn colCompletedOn5 = new DataGridViewTextBoxColumn();
            colCompletedOn5.Name = "colCompletedOn";
            colCompletedOn5.HeaderText = oDict.GetWord("completed_on");
            colCompletedOn5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCompletedOn5.DataPropertyName = "DateCompleted";

            DataGridViewColumn colWorkshop5 = new DataGridViewTextBoxColumn();
            colWorkshop5.HeaderText = oDict.GetWord("workshop");
            colWorkshop5.Width = 60;
            colWorkshop5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colWorkshop5.DataPropertyName = "Workshop";

            DataGridViewColumn colInvoiceNumber5 = new DataGridViewTextBoxColumn();
            colInvoiceNumber5.HeaderText = oDict.GetWord("invoice_number");
            colInvoiceNumber5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colInvoiceNumber5.DataPropertyName = "InvoiceNumber";
            colInvoiceNumber5.Width = 60;   // colInvoiceNumber.Width * 80 / 100;
            colInvoiceNumber5.DefaultCellStyle.Format = "###";
            colInvoiceNumber5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colInvoiceDate5 = new DataGridViewTextBoxColumn();
            colInvoiceDate5.Name = "colInvoiceDate";
            colInvoiceDate5.HeaderText = oDict.GetWord("invoice_date");
            colInvoiceDate5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colInvoiceDate5.DataPropertyName = "InvoiceDate";
            //colInvoiceDate5.DefaultCellStyle.Format = "yyyy-MM-dd hh:mm";
            colInvoiceDate5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colInvoiceAmount5 = new DataGridViewTextBoxColumn();
            colInvoiceAmount5.HeaderText = oDict.GetWord("invoice_amount");
            colInvoiceAmount5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colInvoiceAmount5.DataPropertyName = "InvoiceAmount";
            colInvoiceAmount5.DefaultCellStyle.Format = "#,###.00;-#,###.00;\"\"";  // "positive;negative;zero"
            colInvoiceAmount5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewColumn colPaid5 = new DataGridViewTextBoxColumn();
            colPaid5.Name = "colPaid";
            colPaid5.HeaderText = oDict.GetWord("paid");
            colPaid5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaid5.DataPropertyName = "Paid";
            colPaid5.Width = 32;    // colInvoiceNumber.Width * 80 / 100;
            colPaid5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedOn = new DataGridViewTextBoxColumn();
            colCreatedOn.HeaderText = oDict.GetWord("created_on");
            colCreatedOn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedOn.DataPropertyName = "CreatedOn";
            colCreatedOn.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            colCreatedOn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedBy = new DataGridViewTextBoxColumn();
            colCreatedBy.HeaderText = oDict.GetWord("created_by");
            colCreatedBy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedBy.DataPropertyName = "CreatedBy";

            dgvOrderList.Columns.AddRange(new[] { colFileTypeIcon, colOrderId5, colRowNumber, colPriority5, colStatus5, colDelivery5, colComment5, colClientName5, colAttachment5, colRemarks5, colReceivedOn5, colCompletedOn5, colWorkshop5, colInvoiceNumber5, colInvoiceDate5, colInvoiceAmount5, colPaid5, colCreatedOn, colCreatedBy });

            dgvOrderList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvOrderList_CellFormatting);
        }

        private void dgvOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                DataRowView drv = (DataRowView)dgvOrderList.Rows[e.RowIndex].DataBoundItem;
                if (drv != null)
                {
                    DataRow row = (DataRow)drv.Row;
                    

                    switch (dgvOrderList.Columns[e.ColumnIndex].Name)
                    {
                        case "colFileTypeIcon":
                            String ordertype = row["OrderTypeID"].ToString();
                            #region 根據 order type 劃 icon
                            switch (int.Parse(ordertype))
                            {
                                case (int)DAL.Common.Enums.OrderType.UploadFile:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.UploadFile.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.DirectPrint:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.DirectPrint.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.PsFile:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.PsFile.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.Others:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Others.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.Plate:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Plate.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.Plate5:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Plate5.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.Film5:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Film5.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.OrderType.Vps5:
                                    e.Value = _OrderTypeImageList.Images[DAL.Common.Enums.OrderType.Vps5.ToString("g")];
                                    break;
                            }
                            #endregion
                            break;
                        case "colPriority":
                            int priority = (int)row["PriorityID"];
                            #region 根據 Priority 劃 icon
                            switch (priority)
                            {
                                case (int)DAL.Common.Enums.Priority.Rush:
                                    e.Value = _PriorityImageList.Images[DAL.Common.Enums.Priority.Rush.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Priority.Express:
                                    e.Value = _PriorityImageList.Images[DAL.Common.Enums.Priority.Express.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Priority.Regular:
                                    e.Value = _PriorityImageList.Images[DAL.Common.Enums.Priority.Regular.ToString("g")];
                                    break;
                            }
                            #endregion
                            break;
                        case "colStatus":
                            int status = (int)row["StatusID"];
                            #region 根據 Status 劃 icon
                            switch (status)
                            {
                                case (int)DAL.Common.Enums.Workflow.Queuing:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Queuing.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Retouch:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Retouch.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Printing:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Printing.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.ProofingOutgoing:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.ProofingOutgoing.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.ProofingIncoming:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.ProofingIncoming.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Ready:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Ready.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Dispatch:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Dispatch.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Completed:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Completed.ToString("g")];
                                    break;
                                case (int)DAL.Common.Enums.Workflow.Cancelled:
                                    e.Value = _WorkflowImageList.Images[DAL.Common.Enums.Workflow.Cancelled.ToString("g")];
                                    break;
                            }
                            #endregion
                            break;
                        case "colDelivery":
                            #region 根據 Delivery 劃圈圈
                            int delivery = (int)row["DeliveryMethod"];

                            switch (delivery)
                            {
                                case (int)Common.Enums.DeliveryMethod.PickUp:
                                    e.Value = null;
                                    break;
                                case (int)Common.Enums.DeliveryMethod.DeliverTo:
                                    e.Value = new IconResourceHandle("16x16.jobOrder_deliverto.png");
                                    break;
                            }
                            #endregion
                            break;
                        case "colComment":
                            #region 根據 Comment 劃圈圈
                            String comment = row["Comment"].ToString();

                            if (String.IsNullOrEmpty(comment))
                            {
                                e.Value = null;
                            }
                            else
                            {
                                e.Value = new IconResourceHandle("16x16.jobOrder_comment.png");
                            }
                            #endregion
                            break;
                        case "colCompletedOn":
                            String completedOn = row["DateCompleted"].ToString();
                            e.Value = completedOn.Contains("1900") ? "" : completedOn;
                            break;
                        case "colInvoiceDate":
                            String invDate = row["InvoiceDate"].ToString();
                            e.Value = invDate.Contains("1900") ? "" : invDate;
                            break;
                        case "colPaid":
                            bool paid = (bool)row["Paid"];
                            e.Value = paid ? "Yes" : "";
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
            e.ExpandingRow.ChildGrid.Columns[2].Width = 250;
            e.ExpandingRow.ChildGrid.Columns[3].Width = 30;
            e.ExpandingRow.ChildGrid.Columns[4].Width = 30;
            e.ExpandingRow.ChildGrid.Columns[5].Width = 30;
            e.ExpandingRow.ChildGrid.Columns[6].Width = 60;
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
                        col.HeaderText = "VPS " + oDict.GetWord("file_name");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                        break;
                    case 3:
                        DataGridViewHeaderCell hd = new DataGridViewHeaderCell();
                        col.HeaderText = "P";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 4:
                        col.HeaderText = "CIP3";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 5:
                        col.HeaderText = "Bp";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 6:
                        col.HeaderText = oDict.GetWord("plate_size");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 7:
                        col.HeaderText = oDict.GetWord("created_on");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 8:
                        col.HeaderText = oDict.GetWord("printed_on");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                }
            }
            #endregion
        }

        private void BindDgvOrderList()
        {
            String sql = BuildSqlQueryString();

            FilterRelation fr = new FilterRelation();
            fr.SourceColumnDataName = "OrderID";
            fr.TargetColumnDataName = "OrderHeaderId";

            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);

            //DataSet ds = GetOrderDS();                                  // DataGridView Hierarchy forum 話要用 Dataset，所以 convert object to dataset
            DataSet ds2 = GetDgvOrderItemsDS();

            BindingSource bsMaster = new BindingSource();
            BindingSource bsDetails = new BindingSource();

            bsMaster.DataSource = ds.Tables[0];
            bsDetails.DataSource = ds2.Tables[0];

            dgvOrderList.DataSource = bsMaster;                           // bind Header
            dgvOrderItems.BindedSource = bsDetails;                          // bind Details
            dgvOrderItems.FilteringDataMembers.Add(fr);                      // master details 嘅 relationship

            //SetDgvLayout_Master();
        }

        private DataSet GetDgvOrderItemsDS()
        {
            String sql = String.Format(@"
SELECT TOP 100 percent
ROW_NUMBER() OVER (PARTITION BY OrderHeaderId ORDER By OrderHeaderId, [VpsFileName]) as ROW
--       [ClientID]
--      ,[OrderID]
--      ,[CupsJobID]
--      ,[CupsJobTitle]
      ,[OrderHeaderId]
--      ,[BlueprintOrdered]
--      ,[PrintQueueID]
--      ,[Status]
--      ,[Retired]
--      ,[VpsPrintQueueID]
      ,[VpsFileName]
--      ,[VpsPlateOrdered]
--      ,[VpsRetired]
--      ,[OrderPkPrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[PlateSize]
--      ,[IsReady]
--      ,[IsReceived]
--      ,[IsBilled]
      ,CONVERT(VARCHAR(16), [VpsCreatedOn], 120) AS VpsCreatedOn
--      ,[pkCreatedOn]
      ,CONVERT(VARCHAR(16), [PrintedOn], 120) AS PrintedOn
--      ,[pkCreatedBy]
--      ,[pkModifiedOn]
--      ,[pkModifiedBy]
--      ,[pkRetired]
--      ,[pkRetiredOn]
--      ,[pkRetiredBy]

FROM [vwPrintQueueVpsList_Ordered]
WHERE [OrderHeaderId] IN
(
SELECT DISTINCT
      OrderID
FROM [vwOrderList_Invoice]
WHERE [ClientID] = {0}
)
ORDER BY [OrderHeaderId], [VpsFileName] 
", _ClientId.ToString());
            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);
            return ds;
        }

        private void DgvOrderList_ShowRecord()
        {
            if (dgvOrderList.SelectedRows.Count > 0)
            {
                DataRowView drv = (DataRowView)dgvOrderList.SelectedRows[0].DataBoundItem;
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
        }

        private void SetAttribute()
        {
            dgvOrderList.Dock = DockStyle.Fill;
            dgvOrderList.Visible = true;
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

            this.ansOrderList.Buttons.Add(new ToolBarButton("ClientInfo", oDict.GetWord("client_info")));
            this.ansOrderList.Buttons[10].Image = new IconResourceHandle("16x16.group.png");

            this.ansOrderList.Buttons.Add(new ToolBarButton("Invoice", oDict.GetWord("invoice")));
            this.ansOrderList.Buttons[11].Image = new IconResourceHandle("16x16.jobOrder_Invoice.png");

            this.ansOrderList.Buttons.Add(new ToolBarButton("LogFile", oDict.GetWord("log_file")));
            this.ansOrderList.Buttons[12].Image = new IconResourceHandle("16x16.note16.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("RetrieveFile", "Download File"));
            //this.ansFileExplorer.Buttons[12].Image = new IconResourceHandle("16x16.downloads.png");

            //this.ansFileExplorer.Buttons.Add(new ToolBarButton("DeleteFile", "Delete File"));
            //this.ansFileExplorer.Buttons[13].Image = new IconResourceHandle("16x16.16_L_remove.gif");

            this.ansOrderList.Buttons.Add(sep);

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");

            this.ansOrderList.Buttons.Add(cmdPopup);
            #endregion
        }
        #endregion

        #region Bind Order List


        private string BuildSqlQueryString()
        {
            string where = String.Empty;
            switch (_Mode)
            {
                case 0:
                    where = String.Format("WHERE [ClientID] = {0}", _ClientId.ToString());
                    break;
                case 1:
                    where = String.Format("WHERE [OrderID] BETWEEN {0} AND {1}", txtOrderIdFrom.Text.Trim(), txtOrderIdTo.Text.Trim());
                    break;
                case 2:
                    where = String.Format("WHERE [DateReceived] BETWEEN '{0}' AND '{1}'", dtpOrderedOnFrom.Value.ToString("yyyy-MM-dd"), dtpOrderedOnTo.Value.ToString("yyyy-MM-dd"));
                    break;
            }
            string sql = String.Format(@"
SELECT TOP 100 PERCENT [OrderID], [ClientID], [ClientName], [PriorityID], [Priority]
        ,[Attachment], [Remarks], [DateReceived], ISNULL([DateCompleted], '') AS DateCompleted, [OrderTypeID]
        ,[OrderType], [OrderedBy], ISNULL([PrePressOp], ''), ISNULL([RetouchBy], ''), ISNULL([Workshop], '') AS Workshop
        ,[StatusID], [Status], ISNULL([DeliveryMethod], 1) AS DeliveryMethod, ISNULL([Comment], '') AS Comment
        ,ISNULL([InvoiceNumber], '') AS InvoiceNumber
        ,ISNULL([InvoiceDate], '') AS InvoiceDate
        ,ISNULL([InvoiceAmount], 0) AS InvoiceAmount
        ,ISNULL([PaymentType], 0)
        ,ISNULL([PaymentTypeName], '')
        ,ISNULL([Paid], 0) AS Paid
        ,ISNULL([PaidOn], '')
        ,ISNULL([PaidAmount], 0)
        ,ISNULL([PaidRef], '')
        ,ISNULL([CreatedBy], '') AS CreatedBy
        ,ISNULL([CreatedOn], '') AS CreatedOn
        ,ISNULL([LastModifiedBy], '')
        ,ISNULL([LastModifiedOn], '')
        ,ISNULL([InvoiceStatus], 0)
        ,ROW_NUMBER() OVER (ORDER BY [OrderID] DESC) AS RowNumber
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
                        if (dgvOrderList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvOrderList.SelectedRows[0].DataBoundItem;
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
                        if (dgvOrderList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvOrderList.SelectedRows[0].DataBoundItem;
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
                        if (dgvOrderList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvOrderList.SelectedRows[0].DataBoundItem;
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
                        if (dgvOrderList.SelectedRows.Count > 0)
                        {
                            DataRowView drv = (DataRowView)dgvOrderList.SelectedRows[0].DataBoundItem;
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
            if (ansOrderList.Buttons.Count == 0)
            {
                SetFileExplorerAns();
            }
            //BindOrderList();
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
    }
}