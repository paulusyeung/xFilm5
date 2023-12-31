using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Dialogs;
using ClosedXML.Excel;

using xFilm5.DAL;

namespace xFilm5.Accounting
{
    public partial class DNList : UserControl
    {
        private String _BaseSqlSelect = String.Empty;
        private string _BaseSqlOrderBy = String.Empty;
        private String _BaseSqlWhere = String.Empty;
        private string _CurSqlWhere = String.Empty;
        private string _CurSqlOrderBy = String.Empty;
        private string _CurShortcut = String.Empty;

        private ImageList _ImageList = new ImageList();

        private bool _FormLoaded = false;                   // cboCommonQuery_SelectedIndexChanged 喺 SetDropdowns 就 fire，太早了，要 Form Load 之後先准做嘢！

        public DNList()
        {
            InitializeComponent();

            //升級 WebGui 10 之後唔識留空,要用 Margin 手動
            //lvwRtfList.Margin = new Padding(0, 22, 0, 24);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _BaseSqlSelect = @"
SELECT TOP 100 PERCENT
       [HeaderId]               -- 0
      ,[RtfFileName]
      ,[PurchaseOrder]
      ,[CustomerPO]
      ,[OrderedOn]
      ,[OrderedBy]              -- 5
      ,[OriginalPO]
      ,[SalesOrder]
      ,[OriginalSO]
      ,[Remarks]
      ,[CreatedOn]              -- 10
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,(SELECT COUNT(*) FROM SmlRtfExtractToDN WHERE HeaderId = [vwRtfHeaderList_Active].[HeaderId]) AS DNCount
      ,(SELECT COUNT(*) FROM InvoiceItems WHERE SmlRtfHeaderId = [vwRtfHeaderList_Active].[HeaderId]) AS InvoiceCount
      ,ISNULL((SELECT TOP 1 vwInvoiceList.InvoiceNumber FROM vwInvoiceList INNER JOIN InvoiceItems ON vwInvoiceList.HeaderId = InvoiceItems.HeaderId WHERE InvoiceItems.SmlRtfHeaderId = [vwRtfHeaderList_Active].[HeaderId]), '') AS InvoiceNumber
      ,ROW_NUMBER() OVER (ORDER BY [PurchaseOrder] DESC) AS RowNumber
FROM [dbo].[vwRtfHeaderList_Active]
";
            _BaseSqlWhere = "WHERE 1 = 1";
            _BaseSqlOrderBy = "ORDER BY [PurchaseOrder] DESC";
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;

            SetCaptions();
            SetAttribute();
            SetDropdowns();
            SetTheme();
            SetJoDefaultAns();

            SetDgvLayout_RtfHeader();
            dgvRtfList.RowExpanding += new RowExpandingEventHandler(dgvRtfList_RowExpanding);
            dgvRtfList.DoubleClick += new EventHandler(dgvRtfList_DoubleClick);

            BindOrderList();
            _FormLoaded = true;
        }

        void dgvRtfList_RowExpanding(object sender, RowExpandingEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            e.ExpandingRow.ChildGrid.AllowUserToAddRows = false;
            //e.ExpandingRow.ChildGrid.AutoGenerateColumns = false;
            e.ExpandingRow.ChildGrid.BackgroundColor = Color.WhiteSmoke;
            e.ExpandingRow.ChildGrid.ReadOnly = true;
            e.ExpandingRow.ChildGrid.RowHeadersVisible = false;
            e.ExpandingRow.ChildGrid.Columns[0].Visible = false;
            e.ExpandingRow.ChildGrid.Columns[5].Visible = false;
            //e.ExpandingRow.ChildGrid.Columns[8].Visible = false;
            e.ExpandingRow.ChildGrid.Columns[1].Width = 30;
            e.ExpandingRow.ChildGrid.Columns[3].Width = e.ExpandingRow.ChildGrid.Columns[2].Width * 185 / 100;    // 如果用 3 會越 click 越長 :)

            #region 改 row height to 雙行
            foreach (DataGridViewRow row in e.ExpandingRow.ChildGrid.Rows)
            {
                row.Height = 32;
            }
            #endregion

            #region 改個別 cells 的 alignment 同 header text
            foreach (DataGridViewColumn col in e.ExpandingRow.ChildGrid.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                switch (col.Index)
                {
                    case 1:
                        col.HeaderText = oDict.GetWord("rtf_line");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        break;
                    case 2:
                        col.HeaderText = oDict.GetWord("rtf_prod_code");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                        break;
                    case 3:
                        col.HeaderText = oDict.GetWord("rtf_prod_desc");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                        break;
                    case 4:
                        col.HeaderText = oDict.GetWord("rtf_price_hk");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        break;
                    case 5:
                        col.HeaderText = oDict.GetWord("rtf_discount");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        break;
                    case 6:
                        col.HeaderText = oDict.GetWord("rtf_qty");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        break;
                    case 7:
                        col.HeaderText = oDict.GetWord("rtf_amount");
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        break;
                }
            }
            #endregion
        }

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblLookup.Text = oDict.GetWordWithColon("lookup");
            lblCommonQuery.Text = oDict.GetWordWithColon("common_query");
        }

        private void SetAttribute()
        {
            toolTip1.SetToolTip(txtLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
            toolTip1.SetToolTip(cmdLookup, String.Format("Look for targets:{0}Item Code and Item Name", Environment.NewLine));
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }

        private void SetDropdowns()
        {
            xFilm5.Controls.Utility.ComboEx.LoadCombo_MonthlyQuery(ref cboCommonQuery);
        }

        private void SetDgvLayout_RtfHeader()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            _ImageList.Images.Add("rtf", new IconResourceHandle("16x16.rtf_16x16.png"));
            _ImageList.Images.Add("xls", new IconResourceHandle("16x16.16_excel.gif"));

            dgvRtfList.AutoGenerateColumns = false;
            dgvRtfList.BackgroundColor = Color.WhiteSmoke;
            dgvRtfList.RowHeadersVisible = false;
            dgvRtfList.ReadOnly = true;
            dgvRtfList.Dock = DockStyle.Fill;
            dgvRtfList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRtfList.AllowUserToAddRows = false;

            DataGridViewColumn colFileTypeIcon = new DataGridViewImageColumn();
            colFileTypeIcon.Width = 24;
            colFileTypeIcon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colFileTypeIcon.Name = "colFileTypeIcon";
            colFileTypeIcon.HeaderText = " ";
            //colFileTypeIcon.SortMode = DataGridViewColumnSortMode.NotSortable;

            //DataGridViewColumn colPurchaseOrder = new DataGridViewIconTextBoxColumn();
            DataGridViewColumn colPurchaseOrder = new DataGridViewTextBoxColumn();
            colPurchaseOrder.Name = "colPurchaseOrder";
            colPurchaseOrder.HeaderText = oDict.GetWord("rtf_purchase_order");
            colPurchaseOrder.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPurchaseOrder.DataPropertyName = "PurchaseOrder";
            colPurchaseOrder.Width = colPurchaseOrder.Width * 90 / 100;

            DataGridViewColumn colRowNumber = new DataGridViewTextBoxColumn();
            colRowNumber.Width = 32;
            colRowNumber.HeaderText = "#";
            colRowNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRowNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRowNumber.DataPropertyName = "RowNumber";

            DataGridViewColumn colCustomerPO = new DataGridViewTextBoxColumn();
            colCustomerPO.HeaderText = oDict.GetWord("rtf_customer_po");
            colCustomerPO.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCustomerPO.DataPropertyName = "CustomerPO";

            DataGridViewColumn colOrderedBy = new DataGridViewTextBoxColumn();
            colOrderedBy.HeaderText = oDict.GetWord("rtf_ordered_by");
            colOrderedBy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOrderedBy.DataPropertyName = "OrderedBy";

            DataGridViewColumn colOrderedOn = new DataGridViewTextBoxColumn();
            colOrderedOn.Name = "colOrderedOn";
            colOrderedOn.HeaderText = oDict.GetWord("rtf_ordered_on");
            colOrderedOn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOrderedOn.DataPropertyName = "OrderedOn";
            colOrderedOn.DefaultCellStyle.Format = "yyyy-MM-dd";
            colOrderedOn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colOriginalPO = new DataGridViewTextBoxColumn();
            colOriginalPO.HeaderText = oDict.GetWord("rtf_original_po");
            colOriginalPO.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOriginalPO.DataPropertyName = "OriginalPO";

            DataGridViewColumn colSalesOrders = new DataGridViewTextBoxColumn();
            colSalesOrders.HeaderText = oDict.GetWord("rtf_sales_order");
            colSalesOrders.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSalesOrders.DataPropertyName = "SalesOrder";

            DataGridViewColumn colOriginalSO = new DataGridViewTextBoxColumn();
            colOriginalSO.HeaderText = oDict.GetWord("rtf_original_so");
            colOriginalSO.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOriginalSO.DataPropertyName = "OriginalSO";

            DataGridViewColumn colIsLabelPrinted = new DataGridViewImageColumn();
            colIsLabelPrinted.Name = "colIsLabelPrinted";
            colIsLabelPrinted.Width = 30;
            colIsLabelPrinted.HeaderCell = xFilm5.Controls.DataGridViewExtension.LoadIcon("16x16.labels_16x16.png");

            DataGridViewColumn colInvoiceNumber = new DataGridViewTextBoxColumn();
            colInvoiceNumber.HeaderText = oDict.GetWord("invoice_ref");
            colInvoiceNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colInvoiceNumber.DataPropertyName = "InvoiceNumber";
            colInvoiceNumber.Width = colInvoiceNumber.Width * 80 / 100;
            colInvoiceNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedOn = new DataGridViewTextBoxColumn();
            colCreatedOn.HeaderText = oDict.GetWord("created_on");
            colCreatedOn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedOn.DataPropertyName = "CreatedOn";
            colCreatedOn.DefaultCellStyle.Format = "yyyy-MM-dd hh:mm";
            colCreatedOn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn colCreatedBy = new DataGridViewTextBoxColumn();
            colCreatedBy.HeaderText = oDict.GetWord("created_by");
            colCreatedBy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCreatedBy.DataPropertyName = "CreatedBy";

            dgvRtfList.Columns.AddRange(new[] { colFileTypeIcon, colPurchaseOrder, colRowNumber, colCustomerPO, colOrderedBy, colOrderedOn, colOriginalPO, colSalesOrders, colOriginalSO, colIsLabelPrinted, colInvoiceNumber, colCreatedOn, colCreatedBy });

            dgvRtfList.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvRtfList_CellFormatting);
        }

        void dgvRtfList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                DataRowView drv = (DataRowView)dgvRtfList.Rows[e.RowIndex].DataBoundItem;
                if (drv != null)
                {
                    DataRow row = (DataRow)drv.Row;
                    String filename = row["RtfFileName"].ToString();
                    switch (dgvRtfList.Columns[e.ColumnIndex].Name)
                    {
                        case "colFileTypeIcon":
                            #region 根據 RtfFileName file type 劃 icon
                            switch (Path.GetExtension(filename).ToLower())
                            {
                                case ".rtf":
                                    e.Value = _ImageList.Images["rtf"];
                                    break;
                                case ".xls":
                                case ".xlsx":
                                    e.Value = _ImageList.Images["xls"];
                                    break;
                            }
                            #endregion
                            break;
                        case "colPurchaseOrder":
                            #region 根據 RtfFileName file type 劃 icon，不過，我搞唔掂啲 visual behavious，暫時放棄
                            //DataGridViewIconTextBoxCell iconTextBox = (DataGridViewIconTextBoxCell)dgvRtfList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            //switch (Path.GetExtension(filename).ToLower())
                            //{
                            //    case ".rtf":
                            //        iconTextBox.Image = _ImageList.Images["rtf"];
                            //        break;
                            //    case ".xls":
                            //    case ".xlsx":
                            //        iconTextBox.Image = _ImageList.Images["xls"];
                            //        break;
                            //}
                            #endregion
                            //if (!(Job.Book.Controls.Utility.SmlRtf.IsPurchaseOrderContainsItems((String)e.Value))) e.CellStyle.BackColor = Color.Red;
                            break;
                        case "colOrderedOn":
                            //e.Value = Job.Book.DAL.Common.DateTimeHelper.DateTimeToString((DateTime)row["OrderedOn"], false);
                            break;
                        case "colIsLabelPrinted":
                            #region 根據 DNCount 劃圈圈
                            String po = row["PurchaseOrder"].ToString();
                            int dnCount = (int)row["DNCount"];

                            if (dnCount > 0)
                                e.Value = new IconResourceHandle("16x16.ico_18_role_g.gif");
                            else
                                e.Value = new IconResourceHandle("16x16.ico_18_role_x.gif");
                            #endregion
                            break;
                    }
                }
            }
        }
        #endregion

        #region Set Action Strip
        private void SetJoDefaultAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansToolbar.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansToolbar.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansToolbar.Buttons[0].Enabled = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansToolbar.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansToolbar.Buttons[1].ToolTipText = @"Sorting";
            this.ansToolbar.Buttons[1].Enabled = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansToolbar.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansToolbar.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansToolbar.Buttons[2].Enabled = false;
            this.ansToolbar.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansToolbar.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansToolbar.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansToolbar.Buttons[3].Visible = false;
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            cmdViews.Enabled = false;
            this.ansToolbar.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansToolbar.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansToolbar.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansToolbar.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansToolbar.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansToolbar.Buttons[8].Enabled = false;
            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
            #endregion

            this.ansToolbar.Buttons.Add(sep);

            #region cmdAttachment
            ContextMenu ddlAttachment = new ContextMenu();
            ddlAttachment.MenuItems.Add(new MenuItem(oDict.GetWord("sml_rtf"), String.Empty, "rtf"));
            ddlAttachment.MenuItems[0].Icon = new IconResourceHandle("16x16.rtf_16x16.png");
            ddlAttachment.MenuItems.Add(new MenuItem(oDict.GetWord("sml_excel"), String.Empty, "xls"));
            ddlAttachment.MenuItems[1].Icon = new IconResourceHandle("16x16.16_excel.gif");

            ToolBarButton cmdAttachment = new ToolBarButton("Attachment", oDict.GetWord("attachment"));
            cmdAttachment.Image = new IconResourceHandle("16x16.eye.png");
            cmdAttachment.Style = ToolBarButtonStyle.DropDownButton;
            cmdAttachment.DropDownMenu = ddlAttachment;
            this.ansToolbar.Buttons.Add(cmdAttachment);
            cmdAttachment.MenuClick += new MenuEventHandler(ansAttachment_MenuClick);
            #endregion

            #region Print, Export, Print PO, Delete
            ToolBarButton cmdPrint = new ToolBarButton("Print", oDict.GetWord("print_invoice"));
            cmdPrint.Image = new IconResourceHandle("16x16.16_print.gif");
            ToolBarButton cmdExport = new ToolBarButton("Export", oDict.GetWord("print") + " Labels");
            cmdExport.Image = new IconResourceHandle("16x16.labels_16x16.png");
            ToolBarButton cmdPrintRoll = new ToolBarButton("PrintRollPaper", oDict.GetWord("print") + " Labels");
            cmdPrintRoll.Image = new IconResourceHandle("16x16.roll_16x16.png");
            ToolBarButton cmdPrintPO = new ToolBarButton("PrintPO", oDict.GetWord("print_po"));
            cmdPrintPO.Image = new IconResourceHandle("16x16.16_print.gif");
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");
            ToolBarButton cmdCompleted = new ToolBarButton("Completed", oDict.GetWord("marks_as_completed"));
            cmdCompleted.Image = new IconResourceHandle("16x16.ico_16_4212_d.gif");

            this.ansToolbar.Buttons.Add(cmdPrint);
            this.ansToolbar.Buttons.Add(cmdExport);
            this.ansToolbar.Buttons.Add(cmdPrintRoll);
            this.ansToolbar.Buttons.Add(cmdPrintPO);
            this.ansToolbar.Buttons.Add(sep);

            //if (Job.Book.Controls.Utility.User.AllowedToDelete())
            //{
            //    this.ansToolbar.Buttons.Add(cmdDelete);
            //    this.ansToolbar.Buttons.Add(sep);
            //    //this.ansToolbar.Buttons.Add(cmdCompleted);
            //}
            #endregion

            //Job.Book.Controls.UserSecurity us = new UserSecurity("sml_RtfList");
            //cmdAttachment.Enabled = us.AllowUpdate;
            //cmdPrint.Enabled = us.AllowCreate;
            //cmdExport.Enabled = us.AllowRead;
            //cmdDelete.Enabled = us.AllowDelete;
            //cmdCompleted.Enabled = UserSecurityControls.ReWritable;

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");

            //this.ansToolbar.Buttons.Add(cmdPopup);
            #endregion

        }
        #endregion

        #region Bind Rtf List
        private void BindOrderList()
        {
            string sql = BuildSqlQueryString();

            BindRtfList(sql);
        }

        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();

            #region prepare the where clause
            if (!(String.IsNullOrEmpty(_CurShortcut)))
            {
                switch (_CurShortcut)
                {
                    case "9":
                        _CurSqlWhere = _BaseSqlWhere + " AND SUBSTRING([PurchaseOrder], 1, 1) NOT BETWEEN N'A' AND N'Z'";
                        break;
                    case "All":
                        _CurSqlWhere = _BaseSqlWhere;
                        break;
                    default:
                        _CurSqlWhere = _BaseSqlWhere + String.Format(" AND SUBSTRING([PurchaseOrder], 1, 1) = N'{0}'", _CurShortcut);
                        break;
                }
            }
            #endregion

            string datetimeRange = GetDateTimeRange();

            if (datetimeRange.Length > 0)
            {
                _CurSqlWhere += string.Format(" AND {0} ", datetimeRange);
            }

            sql.Append(_BaseSqlSelect + Environment.NewLine);
            sql.Append(_CurSqlWhere + Environment.NewLine);
            sql.Append(_CurSqlOrderBy);

            return sql.ToString();
        }

        private string GetDateTimeRange()
        {
            string range = string.Empty;

            switch (cboCommonQuery.SelectedIndex)
            {
                //case 1:         // 最近7日內的訂單
                //    range = String.Format(" [Retired] = 0 AND [JobNumber] > 0 AND [Status] >= {0} AND [OrderedOn] <= '{1}' AND [OrderedOn] >= '{2}'",
                //        ((int)Common.Enums.Status.Active).ToString(),
                //        DateTime.Now.ToString(Common.DateTimeHelper.GetDateFormat()),
                //        DateTime.Now.AddDays(-7).ToString(Common.DateTimeHelper.GetDateFormat()));
                //    break;
                //case 2:         // 最近30日內的訂單
                //    range = String.Format(" [Retired] = 0 AND [JobNumber] > 0 AND [Status] >= {0} AND [OrderedOn] <= '{1}' AND [OrderedOn] >= '{2}'",
                //        ((int)Common.Enums.Status.Active).ToString(),
                //        DateTime.Now.ToString(Common.DateTimeHelper.GetDateFormat()),
                //        DateTime.Now.AddDays(-30).ToString(Common.DateTimeHelper.GetDateFormat()));
                //    break;
                //case 3:         // 要求7日內送達的訂單
                //    range = String.Format(" [Retired] = 0 AND [JobNumber] > 0 AND [Status] >= {0} AND [RequiredOn] >= '{1}' AND [RequiredOn] <= '{2}'",
                //        ((int)Common.Enums.Status.Active).ToString(),
                //        DateTime.Now.ToString(Common.DateTimeHelper.GetDateFormat()),
                //        DateTime.Now.AddDays(7).ToString(Common.DateTimeHelper.GetDateFormat()));
                //    break;
                //case 4:         // 要求30日內送達的訂單
                //    range = String.Format(" [Retired] = 0 AND [JobNumber] > 0 AND [Status] >= {0} AND [RequiredOn] >= '{1}' AND [RequiredOn] <= '{2}'",
                //        ((int)Common.Enums.Status.Active).ToString(),
                //        DateTime.Now.ToString(Common.DateTimeHelper.GetDateFormat()),
                //        DateTime.Now.AddDays(30).ToString(Common.DateTimeHelper.GetDateFormat()));
                //    break;
                case 1:         // 最近30日內的訂單
                    range = String.Format(" ([CreatedOn] <= '{0}' AND [CreatedOn] >= '{1}')",
                        DateTime.Now.AddDays(1).ToString(Common.DateTimeHelper.GetDateFormat()),
                        DateTime.Now.AddDays(-30).ToString(Common.DateTimeHelper.GetDateFormat()));
                    break;
                case 2:         // 最近60日內的訂單
                    range = String.Format(" ([CreatedOn] <= '{0}' AND [CreatedOn] >= '{1}')",
                        DateTime.Now.AddDays(1).ToString(Common.DateTimeHelper.GetDateFormat()),
                        DateTime.Now.AddDays(-60).ToString(Common.DateTimeHelper.GetDateFormat()));
                    break;
                case 3:         // 最近90日內的訂單
                    range = String.Format(" ([CreatedOn] <= '{0}' AND [CreatedOn] >= '{1}')",
                        DateTime.Now.AddDays(1).ToString(Common.DateTimeHelper.GetDateFormat()),
                        DateTime.Now.AddDays(-90).ToString(Common.DateTimeHelper.GetDateFormat()));
                    break;
                case 4:
                default:
                    range = string.Empty;
                    break;
            }

            return range;
        }

        private void BindRtfList(String sql)
        {
            FilterRelation fr = new FilterRelation();
            fr.SourceColumnDataName = "PurchaseOrder";
            fr.TargetColumnDataName = "PurchaseOrder";

            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);

            //DataSet ds = GetOrderDS();                                  // DataGridView Hierarchy forum 話要用 Dataset，所以 convert object to dataset
            DataSet ds2 = GetRtfItemsDS();

            BindingSource bsMaster = new BindingSource();
            BindingSource bsDetails = new BindingSource();

            bsMaster.DataSource = ds.Tables[0];
            bsDetails.DataSource = ds2.Tables[0];

            dgvRtfList.DataSource = bsMaster;                           // bind Header
            dgvRtfItems.BindedSource = bsDetails;                          // bind Details
            dgvRtfItems.FilteringDataMembers.Add(fr);                      // master details 嘅 relationship

            //SetDgvLayout_Master();
        }

        private DataSet GetRtfItemsDS()
        {
            String sql = String.Format(@"
SELECT TOP 100 percent
       [PurchaseOrder]
      ,[LineNumber]
      ,[ProductCode]
      ,[ProductDescription]
      ,[Price]
      ,[Discount]
      ,[Qty]
      ,[Amount]
FROM [vwRtfItemList]
WHERE [PurchaseOrder] IN
(
SELECT DISTINCT
      PurchaseOrder
FROM vwRtfHeaderList_Active
{0}
)
ORDER BY [PurchaseOrder], [LineNumber] ", _CurSqlWhere);
            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);
            return ds;
        }
        #endregion

        private void ResetForm()
        {
            txtLookup.Text = String.Empty;
            cboCommonQuery.SelectedIndex = 0;

            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
            _CurShortcut = String.Empty;
        }

        private void RefreshForm()
        {
            txtLookup.Text = String.Empty;

            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
        }

        private void DoLookup()
        {
            string target = txtLookup.Text.Trim();
            if (!(String.IsNullOrEmpty(target)))
            {
                ResetForm();
                txtLookup.Text = target;
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ") +
                               String.Format(" ([PurchaseOrder] LIKE N'%{0}%' OR [CustomerPO] LIKE N'%{0}%' OR [OriginalPO] LIKE N'%{0}%' OR [SalesOrder] LIKE N'%{0}%' OR [OriginalSO] LIKE N'%{0}%' OR CONVERT(NVARCHAR(10), [OrderedOn], 120) LIKE N'%{0}%')", target);
                BindOrderList();
                this.Update();
            }
        }

        private void DoQuery()
        {
            txtLookup.Text = String.Empty;

            string query = GetDateTimeRange();

            if (query.Length > 0)
            {
                _CurSqlWhere = String.Format("WHERE {0}", query);
            }
            else
            {
                _CurSqlWhere = String.Empty;
            }

            BindOrderList();
            this.Update();
        }

        private void ShowRecord()
        {
            if (dgvRtfList.SelectedRows.Count > 0)
            {
                DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                if (drv != null)
                {
                    DataRow row = (DataRow)drv.Row;
                    Guid id = (Guid)row["HeaderId"];
                    DownloadAttachment(id);
                }
            }
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        RefreshForm();
                        BindOrderList();
                        this.Update();
                        break;
                    //case "columns":
                    //    ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwRtfList);
                    //    objListViewColumnOptions.ShowDialog();
                    //    break;
                    //case "sorting":
                    //    ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwRtfList);
                    //    objListViewSortingOptions.ShowDialog();
                    //    break;
                    //case "checkbox":
                    //    this.lvwRtfList.CheckBoxes = !this.lvwRtfList.CheckBoxes;
                    //    this.lvwRtfList.MultiSelect = this.lvwRtfList.CheckBoxes;
                    //    break;
                    //case "multiselect":
                    //    this.lvwRtfList.MultiSelect = !this.lvwRtfList.MultiSelect;
                    //    e.Button.Pushed = true;
                    //    break;
                    case "print":
                        //MessageBox.Show(String.Format("{0} {1}?", oDict.GetWord("print_order"), oDict.GetWord("msg_the_selected_item(s)")),
                        //    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPrint_Click));
                        cmdPrint_Click();
                        break;
                    case "export":
                        //MessageBox.Show(String.Format("{0} {1}?", oDict.GetWord("export"), oDict.GetWord("msg_the_selected_item(s)")),
                        //    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdExport_Click));
                        cmdExport();
                        break;
                    case "printrollpaper":
                        //MessageBox.Show(String.Format("{0} {1}?", oDict.GetWord("export"), oDict.GetWord("msg_the_selected_item(s)")),
                        //    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdExport_Click));
                        cmdPrintRollPaper();
                        break;
                    case "printpo":
                        cmdPrintPO_Click();
                        break;
                    case "delete":
                        MessageBox.Show(String.Format("{0} {1}?", oDict.GetWord("delete"), oDict.GetWord("msg_the_selected_item(s)")),
                            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                    case "completed":
                        //Forms.JobCompleteWizard wiz = new Job.Book.JobOrder.Forms.JobCompleteWizard();
                        //wiz.ShowDialog();
                        break;
                    case "popup":
                        ShowRecord();
                        break;
                }
            }
        }

        #region ans Button Clicks: Print, Export, Delete

        private void cmdPrint_Click()
        {
            List<String> orders = new List<String>();

            if (dgvRtfList.SelectedRows.Count > 0)
            {
                if (dgvRtfList.SelectedRows.Count == 1)
                {
                    DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                    if (drv != null)
                    {
                        DataRow row = (DataRow)drv.Row;
                        String po = (String)row["PurchaseOrder"].ToString();
                        orders.Add(po);
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                    {
                        DataRowView drv = (DataRowView)item.DataBoundItem;
                        if (drv != null)
                        {
                            DataRow row = (DataRow)drv.Row;
                            String po = (String)row["PurchaseOrder"].ToString();
                            orders.Add(po);
                        }
                    }
                }
            }


            //RTF.PrintInvoice print = new RTF.PrintInvoice();
            //print.Orders = orders;
            //print.ShowDialog();
        }

        private void cmdPrintPO_Click()
        {
            List<String> orders = new List<String>();

            if (dgvRtfList.SelectedRows.Count > 0)
            {
                if (dgvRtfList.SelectedRows.Count == 1)
                {
                    DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                    if (drv != null)
                    {
                        DataRow row = (DataRow)drv.Row;
                        String po = (String)row["PurchaseOrder"].ToString();
                        DownloadRtf_PO(po);
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                    {
                        DataRowView drv = (DataRowView)item.DataBoundItem;
                        if (drv != null)
                        {
                            DataRow row = (DataRow)drv.Row;
                            String po = (String)row["PurchaseOrder"].ToString();
                            DownloadRtf_PO(po);
                        }
                    }
                }
            }

        }

        private void cmdExport()
        {
            List<String> orders = new List<String>();

            if (dgvRtfList.SelectedRows.Count > 0)
            {
                if (dgvRtfList.SelectedRows.Count == 1)
                {
                    DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                    if (drv != null)
                    {
                        DataRow row = (DataRow)drv.Row;
                        String po = row["PurchaseOrder"].ToString();
                        orders.Add(po);
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                    {
                        DataRowView drv = (DataRowView)item.DataBoundItem;
                        if (drv != null)
                        {
                            DataRow row = (DataRow)drv.Row;
                            String po = row["PurchaseOrder"].ToString();
                            orders.Add(po);
                        }
                    }
                }
            }

            //RTF.ExportExcel export = new RTF.ExportExcel();
            //export.Orders = orders;
            //export.ShowDialog();
        }

        private void cmdPrintRollPaper()
        {
            List<String> orders = new List<String>();

            if (dgvRtfList.SelectedRows.Count > 0)
            {
                if (dgvRtfList.SelectedRows.Count == 1)
                {
                    DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                    if (drv != null)
                    {
                        DataRow row = (DataRow)drv.Row;
                        String po = row["PurchaseOrder"].ToString();
                        orders.Add(po);
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                    {
                        DataRowView drv = (DataRowView)item.DataBoundItem;
                        if (drv != null)
                        {
                            DataRow row = (DataRow)drv.Row;
                            String po = row["PurchaseOrder"].ToString();
                            orders.Add(po);
                        }
                    }
                }
            }

            //RTF.ExportExcel export = new RTF.ExportExcel();
            //export.Orders = orders;
            //export.RollPaper = true;
            //export.ShowDialog();
        }

//        private DataTable GetTable(String tableName)
//        {
//            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

//            DataTable table = new DataTable();
//            table.TableName = tableName;

//            #region 首先加上 header
//            table.Columns.Add(oDict.GetWord("ordered_on"), typeof(DateTime));               // 1    15
//            table.Columns.Add(oDict.GetWord("order_number"), typeof(string));               // 2    1
//            table.Columns.Add(oDict.GetWord("customer_name"), typeof(string));              // 3    2
//            table.Columns.Add(oDict.GetWord("order_title"), typeof(string));                // 4    3
//            table.Columns.Add(oDict.GetWord("product_code"), typeof(string));               // 5    4
//            table.Columns.Add(oDict.GetWord("job_title"), typeof(string));                  // 6
//            table.Columns.Add(oDict.GetWord("payment_terms"), typeof(string));              // 7    8
//            table.Columns.Add(oDict.GetWord("qty"), typeof(int));                           // 8    7
////            table.Columns.Add(oDict.GetWord("customer_ref"), typeof(string));               // 5      jobOrder.CustomerRef
////            table.Columns.Add(oDict.GetWord("output_ref"), typeof(string));                 // 9      jobOrder.OutputRef
////            table.Columns.Add(oDict.GetWord("product_style"), typeof(string));              // 11     jobOrder.ProductStyle
//            table.Columns.Add(oDict.GetWord("color"), typeof(string));                      // 9
//            table.Columns.Add(oDict.GetWord("set"), typeof(string));                        // 10
//            table.Columns.Add(oDict.GetWord("machine_number"), typeof(string));             // 11
//            table.Columns.Add(oDict.GetWord("required_on"), typeof(DateTime));              // 12   16
//            table.Columns.Add(oDict.GetWord("invoice_ref"), typeof(string));                // 13   10
//            table.Columns.Add(oDict.GetWord("invoice_amount"), typeof(Decimal));            // 14   12
//            table.Columns.Add(oDict.GetWord("ordered_by"), typeof(string));                 // 15   13
//            table.Columns.Add(oDict.GetWord("created_by"), typeof(string));                 // 16   14
//            table.Columns.Add(oDict.GetWord("modified_on"), typeof(DateTime));              // 17   18
//            table.Columns.Add(oDict.GetWord("modified_by"), typeof(string));                // 18   17
//            table.Columns.Add(oDict.GetWord("completed_on"), typeof(DateTime));             // 19
//            #endregion

//            #region 加上 Selected Job List 內容
//            foreach (ListViewItem item in lvwRtfList.Items)
//            {
//                if (item.Checked)
//                {
//                    Guid itemId = new Guid(item.SubItems[1].Text);
//                    DAL.JobOrder jobOrder = DAL.JobOrder.Load(itemId);
//                    if (jobOrder != null)
//                    {
//                        DAL.UserInfo orderedBy = DAL.UserInfo.Load(jobOrder.CreatedBy);
//                        DAL.UserInfo modifiedBy = DAL.UserInfo.Load(jobOrder.ModifiedBy);

//                        String prodDetails = Regex.Replace(jobOrder.ProductDetails, "<.*?>", string.Empty);
//                        String[] prodDetailsLines = prodDetails.Split('\n');
//                        String jobTitle = extract2Lines(prodDetailsLines, "名稱");
//                        String color = extract1Line(prodDetailsLines, "顏色");   //oDict.GetWord("color"));
//                        String set = extract1Line(prodDetailsLines, "石數");   //oDict.GetWord("set"));
//                        String machineNumber = getMachineNumber(jobOrder.OrderId);
//                        if (jobOrder.CompletedOn.ToString("yyyy") == "1900")
//                        {
//                            table.Rows.Add(jobOrder.OrderedOn, String.Format("{0}-{1}", jobOrder.OrderNumber, jobOrder.JobNumber),
//                                jobOrder.CustomerName, jobOrder.OrderTitle, jobOrder.ProductCode, jobTitle, jobOrder.PaymentTerms,
//                                jobOrder.Qty, color, set, machineNumber, jobOrder.RequiredOn.Date, jobOrder.InvoiceRef, jobOrder.InvoiceAmount, jobOrder.OrderedBy,
//                                orderedBy.UserAlias, jobOrder.ModifiedOn, modifiedBy.UserAlias);
//                        }
//                        else
//                        {
//                            table.Rows.Add(jobOrder.OrderedOn, String.Format("{0}-{1}", jobOrder.OrderNumber, jobOrder.JobNumber),
//                                jobOrder.CustomerName, jobOrder.OrderTitle, jobOrder.ProductCode, jobTitle, jobOrder.PaymentTerms,
//                                jobOrder.Qty, color, set, machineNumber, jobOrder.RequiredOn.Date, jobOrder.InvoiceRef, jobOrder.InvoiceAmount, jobOrder.OrderedBy,
//                                orderedBy.UserAlias, jobOrder.ModifiedOn, modifiedBy.UserAlias,
//                                jobOrder.CompletedOn);
//                        }
//                    }
//                }
//            }
//            #endregion

//            return table;
//        }

        //private String extract1Line(String[] source, String target)
        //{
        //    nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
        //    String result = String.Empty;

        //    int pos = 0;
        //    foreach (String line in source)
        //    {
        //        if (line.Contains(target))
        //        {
        //            if (line.IndexOf('：') != -1) pos = line.IndexOf('：') + 1;
        //            result = (line.Substring(pos)).Trim();
        //        }
        //    }

        //    return result;
        //}

        //private String extract2Lines(String[] source, String target)
        //{
        //    nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
        //    String result = String.Empty;

        //    int i = 0, pos = 0;
        //    foreach (String line in source)
        //    {
        //        if (line.Contains(target))
        //        {
        //            if (line.IndexOf('：') != -1) pos = line.IndexOf('：') + 1;
        //            result = (line.Substring(pos)).Trim() + " \r" + source[i + 1];
        //        }
        //        i++;
        //    }

        //    return result;
        //}

        //private String getMachineNumber(Guid OrderId)
        //{
        //    String result = String.Empty;

        //    string sql = "OrderId = '" + OrderId.ToString() + "'";

        //    DAL.JobSchedule orderSchedule = DAL.JobSchedule.LoadWhere(sql);
        //    if (orderSchedule != null)
        //    {
        //        result = orderSchedule.MachineNumber;
        //    }

        //    return result;
        //}

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (dgvRtfList.SelectedRows.Count > 0)
                {
                    if (dgvRtfList.SelectedRows.Count == 1)
                    {
                        DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                        if (drv != null)
                        {
                            DataGridViewRow curRow = dgvRtfList.SelectedRows[0];
                            DataRow row = (DataRow)drv.Row;
                            String po = row["PurchaseOrder"].ToString();

                            //if (Job.Book.Controls.Utility.SmlRtf.Delete(po))
                            //{
                            //    dgvRtfList.Rows.Remove(curRow);
                            //}
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                        {
                            DataRowView drv = (DataRowView)item.DataBoundItem;
                            if (drv != null)
                            {
                                DataGridViewRow curRow = dgvRtfList.SelectedRows[0];
                                DataRow row = (DataRow)drv.Row;
                                String po = row["PurchaseOrder"].ToString();

                                //if (Job.Book.Controls.Utility.SmlRtf.Delete(po))
                                //{
                                //    dgvRtfList.Rows.Remove(curRow);
                                //}
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            //switch ((string)e.MenuItem.Tag)
            //{
            //    case "Icon":
            //        this.lvwRtfList.View = View.SmallIcon;
            //        break;
            //    case "Tile":
            //        this.lvwRtfList.View = View.LargeIcon;
            //        break;
            //    case "List":
            //        this.lvwRtfList.View = View.List;
            //        break;
            //    case "Details":
            //        this.lvwRtfList.View = View.Details;
            //        break;
            //}
        }

        private void ansAttachment_MenuClick(object sender, MenuItemEventArgs e)
        {
            if ((e.MenuItem.Tag.ToString() == "rtf") || (e.MenuItem.Tag.ToString() == "xls"))
            {
                if (dgvRtfList.SelectedRows.Count > 0)
                {
                    if (dgvRtfList.SelectedRows.Count == 1)
                    {
                        DataRowView drv = (DataRowView)dgvRtfList.SelectedRows[0].DataBoundItem;
                        if (drv != null)
                        {
                            DataRow row = (DataRow)drv.Row;
                            Guid id = (Guid)row["HeaderId"];
                            DownloadAttachment(id);
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dgvRtfList.SelectedRows)
                        {
                            DataRowView drv = (DataRowView)item.DataBoundItem;
                            if (drv != null)
                            {
                                DataRow row = (DataRow)drv.Row;
                                Guid id = (Guid)row["HeaderId"];
                                DownloadAttachment(id);
                            }
                        }
                    }
                }

            }
        }

        private void DownloadAttachment(Guid rtfHeaderId)
        {
            //SmlRtfHeader rtfHeader = SmlRtfHeader.Load(rtfHeaderId);
            //if (rtfHeader != null)
            //{
            //    String filename = rtfHeader.RtfFileName;
            //    String filePath = Path.Combine(Job.Book.Controls.Utility.SmlRtf.RTFFolder(), filename);
            //    if (File.Exists(filePath))
            //    {
            //        Job.Book.Controls.FileDownloadGateway dl = new Job.Book.Controls.FileDownloadGateway();
            //        dl.Filename = filename;
            //        dl.SetContentType(Job.Book.Controls.DownloadContentType.MicrosoftWord);
            //        dl.StartFileDownload(this, filePath);
            //    }
            //}
        }

        private void DownloadRtf(String po)
        {
            //String filename = String.Format("{0}.rtf", po);
            //String filePath = Path.Combine(Job.Book.Controls.Utility.SmlRtf.RTFFolder(), filename);
            //if (File.Exists(filePath))
            //{
            //    Job.Book.Controls.FileDownloadGateway dl = new Job.Book.Controls.FileDownloadGateway();
            //    dl.Filename = filename;
            //    dl.SetContentType(Job.Book.Controls.DownloadContentType.MicrosoftWord);
            //    dl.StartFileDownload(this, filePath);
            //}
        }

        /// <summary>
        /// RTF PO 係 SML RTF 不過冇咗啲 Price, Discount, Amount, 同埋 Total Amount 嘅資料
        /// </summary>
        /// <param name="po"></param>
        private void DownloadRtf_PO(String po)
        {
            //String rtfFilePath = Path.Combine(Job.Book.Controls.Utility.SmlRtf.RTFFolder(), String.Format("{0}.rtf", po));
            //String poFileName = String.Format("{0}X.rtf", po);
            //String poFilePath = Path.Combine(Job.Book.Controls.Utility.SmlRtf.RTFFolder(), poFileName);

            //// 唔理 server 有冇 RTF PO，首先生一個，然後下載，咁樣唔怕 SML RTF 曾經改過 
            //if (File.Exists(rtfFilePath))
            //{
            //    Job.Book.Controls.Utility.SmlRtf.RemoveMoneyBoxes(rtfFilePath, poFilePath);

            //    Job.Book.Controls.FileDownloadGateway dl = new Job.Book.Controls.FileDownloadGateway();
            //    dl.Filename = poFileName;
            //    dl.SetContentType(Job.Book.Controls.DownloadContentType.MicrosoftWord);
            //    dl.StartFileDownload(this, poFilePath);
            //}
        }

        private void ShortcutButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            txtLookup.Text = String.Empty;
            cboCommonQuery.SelectedIndex = 0;

            _CurShortcut = button.Tag.ToString();
            BindOrderList();
            this.Update();
        }

        private void cmdLookup_Click(object sender, EventArgs e)
        {
            DoLookup();
        }

        private void txtLookup_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoLookup();
        }

        private void lvwClientList_DoubleClick(object sender, EventArgs e)
        {
            ShowRecord();
        }

        void dgvRtfList_DoubleClick(object sender, EventArgs e)
        {
            ShowRecord();
        }

        private void cboCommonQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_FormLoaded)
            {
                if (cboCommonQuery.SelectedIndex > 0)
                {
                    DoQuery();
                }
            }
        }
    }
}