using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Dialogs;

using MarkPasternak.Utility;

using xFilm5.DAL;
using xFilm5.Settings;

namespace xFilm5.JobOrder
{
    public partial class JoSearch : UserControl, IGatewayComponent
    {
        private int _ButtonIndex_Workshop = 0;
        private int _ButtonIndex_OrderType = 0;
        private Common.Enums.Workflow _WorkflowFrom = Common.Enums.Workflow.Queuing;
        private Common.Enums.Workflow _WorkflowTo = Common.Enums.Workflow.Completed;    // completed on: Today
        private string _BaseSqlSelect = String.Empty;
        private string _BaseSqlWhere = String.Empty;
        private string _BaseSqlOrderBy = String.Empty;
        private string _CurSqlWhere = String.Empty;
        private string _CurSqlOrderBy = String.Empty;
        private string _CurWorkshop = String.Empty;
        private string _CurOrderType = String.Empty;

        public JoSearch(Control toolBar)
        {
            InitializeComponent();
        }

        #region public Properties
        public Common.Enums.Workflow WorkflowFrom
        {
            get
            {
                return _WorkflowFrom;
            }
            set
            {
                _WorkflowFrom = value;
            }
        }

        public Common.Enums.Workflow WorkflowTo
        {
            get
            {
                return _WorkflowTo;
            }
            set
            {
                _WorkflowTo = value;
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

            OrderHeader oOrder = OrderHeader.Load(Convert.ToInt32(strAction));
            if (oOrder != null)
            {
                if (oOrder.Attachment)
                {
                    string filename = oOrder.AttachmentURL;
                    string filepath = String.Format(@"{0}\{1}", Common.Client.InBox(oOrder.ClientID), filename);

                    if (File.Exists(filepath))
                    {
                        FileInfo oFile = new FileInfo(filepath);
                        if (oFile.Length > 1024 * 1024 * 32)
                        {
                            // use this method for file size over 32MB 
                            WriteFileHelper oWriteFile = new WriteFileHelper();
                            oWriteFile.BufferSize = 65536;
                            oWriteFile.WriteFileToResponseStreamWithForceDownloadHeaders(filepath, HttpUtility.UrlEncode(filename));
                        }
                        else
                        {
                            HttpResponse response = objContext.Response;    // prefer to use Gizmox instead of: this.Context.HttpContext.Response;

                            response.Buffer = true;
                            response.Clear();
                            response.ClearHeaders();
                            response.ContentType = "application/octet-stream";
                            response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
                            response.WriteFile(filepath);
                            response.Flush();
                            response.End();
                        }
                    }
                    else
                    {
                        objContext.Response.Write(String.Format("<html><body><h>Order ID: {0}, attachment not found!</h></body></html>", strAction));
                    }
                }
                else
                {
                    objContext.Response.Write(String.Format("<html><body><h>Order ID: {0}, no attachment!</h></body></html>", strAction));
                }
            }

            return objGH;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAttribute();
            SetTheme();
            SetJoDefaultAns();

            _BaseSqlSelect = @"
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
";
            _BaseSqlWhere = String.Format("WHERE ([StatusID] BETWEEN {0} AND {1})", _WorkflowFrom.ToString("d"), _WorkflowTo.ToString("d"));
            _BaseSqlOrderBy = "ORDER BY [OrderID]";
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;

//            BindJoList();
        }

        #region Set Attributes, Themes
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblLookup.Text = oDict.GetWordWithColon("lookup");

            colOrderId.Text = oDict.GetWord("order_id");
            colLN.Text = oDict.GetWord("ln");
            colClientName.Text = oDict.GetWord("client_name");
            colAttachment.Text = oDict.GetWord("attachment");
            colRemarks.Text = oDict.GetWord("remarks");
            colReceivedOn.Text = oDict.GetWord("received_on");
            colOrderedBy.Text = oDict.GetWord("ordered_by");
            colPrepressOp.Text = oDict.GetWord("prepress_op");
            colRetouchBy.Text = oDict.GetWord("retouch_by");
            colCompletedOn.Text = oDict.GetWord("completed_on");
            colWorkshop.Text = oDict.GetWord("workshop");
            colInvoiceNumber.Text = oDict.GetWord("invoice_number");
            colInvoiceDate.Text = oDict.GetWord("invoice_date");
            colInvoiceAmount.Text = oDict.GetWord("invoice_amount");
            colPaid.Text = oDict.GetWord("paid");
        }

        private void SetAttribute()
        {
            this.lvwJoDefault.ListViewItemSorter = new ListViewItemSorter(this.lvwJoDefault);

            //if (!(_WorkflowFrom == Common.Enums.Workflow.Completed || _WorkflowTo == Common.Enums.Workflow.Completed))
            //{
            //    this.colCompletedOn.Visible = false;
            //}

            toolTip1.SetToolTip(txtLookup, String.Format("Look for targets:{0}Order ID, Client Name, Attachment, Remarks,{0}Received On, and Ordered By", Environment.NewLine));
            toolTip1.SetToolTip(cmdLookup, String.Format("Look for targets:{0}Order ID, Client Name, Attachment, Remarks,{0}Received On, and Ordered By", Environment.NewLine));
            toolTip1.SetToolTip(lvwJoDefault, "Double click to open Job Order");
        }

        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }
        #endregion

        #region Set Action Strip
        private void SetJoDefaultAns()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansJoDefault.MenuHandle = false;
            this.ansJoDefault.DragHandle = false;
            this.ansJoDefault.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]
            this.ansJoDefault.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansJoDefault.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansJoDefault.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansJoDefault.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansJoDefault.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansJoDefault.Buttons[1].ToolTipText = @"Sorting";
            this.ansJoDefault.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansJoDefault.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansJoDefault.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansJoDefault.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansJoDefault.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansJoDefault.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansJoDefault.Buttons[3].Visible = false;
            #endregion

            this.ansJoDefault.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]
            ContextMenu ddlViews = new ContextMenu();
            Common.Data.AppendMenuItem_AppViews(ref ddlViews);
            ToolBarButton cmdViews = new ToolBarButton("Views", oDict.GetWord("views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansJoDefault.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);
            #endregion

            this.ansJoDefault.Buttons.Add(sep);

            #region cmdRefresh, cmdPreference       - Buttons[7~8]
            this.ansJoDefault.Buttons.Add(new ToolBarButton("Refresh", oDict.GetWord("refresh")));
            this.ansJoDefault.Buttons[7].Image = new IconResourceHandle("16x16.16_L_refresh.gif");
            this.ansJoDefault.Buttons.Add(new ToolBarButton("Preference", oDict.GetWord("preference")));
            this.ansJoDefault.Buttons[8].Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            this.ansJoDefault.Buttons[8].Enabled = false;
            this.ansJoDefault.ButtonClick += new ToolBarButtonClickEventHandler(ansJoDefault_ButtonClick);
            #endregion

            this.ansJoDefault.Buttons.Add(sep);

            #region cmdWorkshop     - Buttons [10]
            ContextMenu ddlWorkshop = new ContextMenu();

            Client_UserCollection oWorkshop = Common.Data.GetWorkshopList();
            if (oWorkshop.Count > 0)
            {
                for (int i = 0; i < oWorkshop.Count; i++)
                {
                    ddlWorkshop.MenuItems.Add(new MenuItem(oWorkshop[i].FullName));
                }
            }

            ToolBarButton cmdWorkshop = new ToolBarButton("Workshop", oDict.GetWord("workshop"));
            cmdWorkshop.Style = ToolBarButtonStyle.DropDownButton;
            cmdWorkshop.Image = new IconResourceHandle("16x16.filter_16.png");
            cmdWorkshop.DropDownMenu = ddlWorkshop;
            this.ansJoDefault.Buttons.Add(cmdWorkshop);
            cmdWorkshop.MenuClick += new MenuEventHandler(ansWorkshop_MenuClick);
            _ButtonIndex_Workshop = this.ansJoDefault.Buttons.Count - 1;
            #endregion

            #region cmdOrderType      - Button [11]
            ContextMenu ddlOrderType = new ContextMenu();
            Common.Data.AppendMenuItem_OrderType(ref ddlOrderType);
            ToolBarButton cmdOrderType = new ToolBarButton("OrderType", oDict.GetWord("order_type"));
            cmdOrderType.Style = ToolBarButtonStyle.DropDownButton;
            cmdOrderType.Image = new IconResourceHandle("16x16.filter_16.png");
            cmdOrderType.DropDownMenu = ddlOrderType;
            this.ansJoDefault.Buttons.Add(cmdOrderType);
            cmdOrderType.MenuClick += new MenuEventHandler(ansOrderType_MenuClick);
            _ButtonIndex_OrderType = this.ansJoDefault.Buttons.Count - 1;
            #endregion

            this.ansJoDefault.Buttons.Add(sep);

            #region prepare workflow command buttons
            // cmdRetouch
            ToolBarButton cmdRetouch = new ToolBarButton("Retouch", oDict.GetWord("retouch"));
            cmdRetouch.Tag = "Retouch";
            cmdRetouch.Image = new IconResourceHandle("16x16.folder_retouch.png");

            // cmdPrinting
            ToolBarButton cmdPrinting = new ToolBarButton("Printing", oDict.GetWord("printing"));
            cmdPrinting.Tag = "Printing";
            cmdPrinting.Image = new IconResourceHandle("16x16.folder_printing.png");

            // cmdChecked
            ToolBarButton cmdChecked = new ToolBarButton("Checked", oDict.GetWord("checked"));
            cmdChecked.Tag = "Checked";
            cmdChecked.Image = new IconResourceHandle("16x16.jobOrder_checked.png");

            // cmdFill
            ToolBarButton cmdFill = new ToolBarButton("Fill", oDict.GetWord("fill"));
            cmdFill.Tag = "Fill";
            cmdFill.Image = new IconResourceHandle("16x16.jobOrder_fill.png");

            // cmdOut
            ToolBarButton cmdOut = new ToolBarButton("Out", oDict.GetWord("Proofing_(Out)"));
            cmdOut.Tag = "Out";
            cmdOut.Image = new IconResourceHandle("16x16.folder_proofingout.png");

            // cmdIn
            ToolBarButton cmdIn = new ToolBarButton("In", oDict.GetWord("Proofing_(In)"));
            cmdIn.Tag = "In";
            cmdIn.Image = new IconResourceHandle("16x16.folder_proofingin.png");

            // cmdDispatch
            ToolBarButton cmdDispatch = new ToolBarButton("Dispatch", oDict.GetWord("dispatch"));
            cmdDispatch.Tag = "Dispatch";
            cmdDispatch.Image = new IconResourceHandle("16x16.folder_dispatch.png");

            // cmdBilling
            ToolBarButton cmdBilling = new ToolBarButton("Billing", oDict.GetWord("billing"));
            cmdBilling.Tag = "Billing";
            cmdBilling.Image = new IconResourceHandle("16x16.jobOrder_billing.png");

            // cmdCompleted
            ToolBarButton cmdCompleted = new ToolBarButton("Completed", oDict.GetWord("completed"));
            cmdCompleted.Tag = "Completed";
            cmdCompleted.Image = new IconResourceHandle("16x16.folder_completed.png");

            // cmdInvoice
            ToolBarButton cmdInvoice = new ToolBarButton("Invoice", oDict.GetWord("invoice"));
            cmdInvoice.Tag = "Invoice";
            cmdInvoice.Image = new IconResourceHandle("16x16.jobOrder_invoice.png");

            // cmdDownload
            ToolBarButton cmdDownload = new ToolBarButton("Download", oDict.GetWord("download"));
            cmdDownload.Tag = "Download";
            cmdDownload.Image = new IconResourceHandle("16x16.jobOrder_download.png");

            // cmdComment
            ToolBarButton cmdComment = new ToolBarButton("Comment", oDict.GetWord("comment"));
            cmdComment.Tag = "Comment";
            cmdComment.Image = new IconResourceHandle("16x16.jobOrder_comment.png");
            #endregion

            #region add workflow command buttons
            switch (_WorkflowFrom)
            {
                case Common.Enums.Workflow.Queuing:
                    this.ansJoDefault.Buttons.Add(cmdRetouch);
                    this.ansJoDefault.Buttons.Add(cmdDownload);
                    break;
                case Common.Enums.Workflow.Retouch:
                    this.ansJoDefault.Buttons.Add(cmdPrinting);
                    this.ansJoDefault.Buttons.Add(cmdDownload);
                    break;
                case Common.Enums.Workflow.Printing:
                    this.ansJoDefault.Buttons.Add(cmdChecked);
                    this.ansJoDefault.Buttons.Add(cmdFill);
                    break;
                case Common.Enums.Workflow.ProofingOutgoing:
                    this.ansJoDefault.Buttons.Add(cmdIn);
                    this.ansJoDefault.Buttons.Add(cmdChecked);
                    break;
                case Common.Enums.Workflow.ProofingIncoming:
                    this.ansJoDefault.Buttons.Add(cmdChecked);
                    break;
                case Common.Enums.Workflow.Ready:
                    this.ansJoDefault.Buttons.Add(cmdDispatch);
                    this.ansJoDefault.Buttons.Add(cmdBilling);
                    break;
                case Common.Enums.Workflow.Dispatch:
                    this.ansJoDefault.Buttons.Add(cmdCompleted);
                    this.ansJoDefault.Buttons.Add(cmdBilling);
                    break;
                case Common.Enums.Workflow.Completed:
                    this.ansJoDefault.Buttons.Add(cmdInvoice);
                    break;
            }
            this.ansJoDefault.Buttons.Add(cmdComment);
            #endregion

            #region cmdJobOrder, cmdLogFile
            // cmdJobOrder
            ToolBarButton cmdJobOrder = new ToolBarButton("JobOrder", oDict.GetWord("joborder"));
            cmdJobOrder.Tag = "JobOrder";
            cmdJobOrder.Image = new IconResourceHandle("16x16.folder.png");

            // cmdLogFile
            ToolBarButton cmdLogFile = new ToolBarButton("LogFile", oDict.GetWord("log_file"));
            cmdLogFile.Tag = "LogFile";
            cmdLogFile.Image = new IconResourceHandle("16x16.note16.png");

            this.ansJoDefault.Buttons.Add(cmdJobOrder);
            this.ansJoDefault.Buttons.Add(cmdInvoice);
            this.ansJoDefault.Buttons.Add(cmdLogFile);
            #endregion

            #region cmdPopup
            ToolBarButton cmdPopup = new ToolBarButton("Popup", oDict.GetWord("popup"));
            cmdPopup.Image = new IconResourceHandle("16x16.popup_16x16.gif");

            this.ansJoDefault.Buttons.Add(cmdPopup);
            #endregion
        }
        #endregion

        #region Action Strip Clicks
        private void ansJoDefault_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        ResetForm();
                        BindJoList();
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvwJoDefault);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvwJoDefault);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvwJoDefault.CheckBoxes = !this.lvwJoDefault.CheckBoxes;
                        this.lvwJoDefault.MultiSelect = this.lvwJoDefault.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvwJoDefault.MultiSelect = !this.lvwJoDefault.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "comment":
                        #region popup Order Comment
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                Comment oComment = new Comment();
                                oComment.OrderId = Convert.ToInt32(item.Text);
                                oComment.EditMode = Common.Enums.EditMode.Edit;
                                oComment.Show();
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                Comment oComment = new Comment();
                                oComment.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                                oComment.EditMode = Common.Enums.EditMode.Edit;
                                oComment.Show();
                            }
                        }
                        #endregion
                        break;
                    case "download":
                        #region download Attachment
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                Link.Open(new GatewayReference(this, item.Text));
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                Link.Open(new GatewayReference(this, lvwJoDefault.SelectedItem.Text));
                            }
                        }
                        #endregion
                        break;
                    case "billing":
                        #region popup billing form
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                Billing billing = new Billing();
                                billing.OrderId = Convert.ToInt32(item.Text);
                                billing.EditMode = Common.Invoice.GetEditMode(Convert.ToInt32(item.Text));
                                billing.Show();
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                Billing billing = new Billing();
                                billing.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                                billing.EditMode = Common.Invoice.GetEditMode(Convert.ToInt32(lvwJoDefault.SelectedItem.Text));
                                billing.Show();
                            }
                        }
                        #endregion
                        break;
                    case "retouch":
                        MessageBox.Show("Move Order to Retouch?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdRetouch_Click));
                        break;
                    case "printing":
                        MessageBox.Show("Move Order to Printing?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPrinting_Click));
                        break;
                    case "checked":
                        MessageBox.Show("Selected Orders are checked OK?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdChecked_Click));
                        break;
                    case "in":
                        MessageBox.Show("Move Order to Proofing (In)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdIn_Click));
                        break;
                    case "dispatch":
                        MessageBox.Show("Move Order to Dispatch?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDispatch_Click));
                        break;
                    case "completed":
                        MessageBox.Show("Move Order to Completed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdCompleted_Click));
                        break;
                    case "joborder":
                        #region popup order form
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                ShowJobOrder(Convert.ToInt32(item.Text));
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                ShowJobOrder(Convert.ToInt32(lvwJoDefault.SelectedItem.Text));
                            }
                        }
                        #endregion
                        break;
                    case "invoice":
                        #region popup invoice form
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                Billing billing = new Billing();
                                billing.OrderId = Convert.ToInt32(item.Text);
                                billing.EditMode = Common.Enums.EditMode.Read;
                                billing.Show();
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                Billing billing = new Billing();
                                billing.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                                billing.EditMode = Common.Enums.EditMode.Read;
                                billing.Show();
                            }
                        }
                        #endregion
                        break;
                    case "logfile":
                        #region popup invoice form
                        if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                        {
                            foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                            {
                                LogFile logfile = new LogFile();
                                logfile.OrderId = Convert.ToInt32(item.Text);
                                logfile.Show();
                            }
                        }
                        else
                        {
                            if (lvwJoDefault.SelectedIndex >= 0)
                            {
                                LogFile logfile = new LogFile();
                                logfile.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                                logfile.Show();
                            }
                        }
                        #endregion
                        break;
                    case "popup":
                        if (lvwJoDefault.SelectedItem != null)
                        {
                            int orderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                            ShowJobOrder(orderId);
                        }
                        break;
                }
            }
        }

        #region ans Button Clicks: Retouch, Printing, Proofing, Ready, Dispatch, Completed
        private void cmdRetouch_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToRetouch(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToRetouch(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove ();
                        }
                    }
                }
            }
        }

        private void cmdPrinting_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToPrinting(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToPrinting(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                        }
                    }
                }
            }
        }

        private void cmdChecked_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (((Form)sender).DialogResult == DialogResult.Yes)
                {
                    if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                    {
                        foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                        {
                            Order_Details oDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", Convert.ToInt32(item.Text)));
                            if (oDetails != null)
                            {
                                if (oDetails.Proofing)
                                {
                                    if (Common.Order.MoveToProofingOutgoing(Convert.ToInt32(item.Text)))
                                    {
                                        item.Remove();
                                    }
                                }
                                else
                                {
                                    if (Common.Order.MoveToReady(Convert.ToInt32(item.Text)))
                                    {
                                        item.Remove();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (lvwJoDefault.SelectedIndex >= 0)
                        {
                            Order_Details oDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", Convert.ToInt32(lvwJoDefault.SelectedItem.Text)));
                            if (oDetails != null)
                            {
                                if (oDetails.Proofing)
                                {
                                    if (Common.Order.MoveToProofingOutgoing(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                                    {
                                        lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                                    }
                                }
                                else
                                {
                                    if (Common.Order.MoveToReady(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                                    {
                                        lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToProofingIncoming(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToProofingIncoming(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                        }
                    }
                }
            }
        }

        private void cmdReady_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToReady(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToReady(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                        }
                    }
                }
            }
        }

        private void cmdDispatch_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToDispatch(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToDispatch(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                        }
                    }
                }
            }
        }

        private void cmdCompleted_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (lvwJoDefault.CheckBoxes && lvwJoDefault.CheckedIndices.Count > 0)
                {
                    foreach (ListViewItem item in lvwJoDefault.CheckedItems)
                    {
                        if (Common.Order.MoveToCompleted(Convert.ToInt32(item.Text)))
                        {
                            item.Remove();
                        }
                    }
                }
                else
                {
                    if (lvwJoDefault.SelectedIndex >= 0)
                    {
                        if (Common.Order.MoveToCompleted(Convert.ToInt32(lvwJoDefault.SelectedItem.Text)))
                        {
                            lvwJoDefault.Items[lvwJoDefault.SelectedIndex].Remove();
                        }
                    }
                }
            }
        }
        #endregion

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch ((string)e.MenuItem.Tag)
            {
                case "Icon":
                    this.lvwJoDefault.View = View.SmallIcon;
                    break;
                case "Tile":
                    this.lvwJoDefault.View = View.LargeIcon;
                    break;
                case "List":
                    this.lvwJoDefault.View = View.List;
                    break;
                case "Details":
                    this.lvwJoDefault.View = View.Details;
                    break;
            }
        }

        private void ansWorkshop_MenuClick(object sender, MenuItemEventArgs e)
        {
            // show he selected Workshop as Ans Button text
            ToolBarButton oSender = (ToolBarButton)sender;
            oSender.Text = e.MenuItem.Text;
            _CurWorkshop = e.MenuItem.Text;
            if (String.IsNullOrEmpty(_CurOrderType))
            {
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([Workshop] = N'{0}')", _CurWorkshop);
            }
            else
            {
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([Workshop] = N'{0}' AND [OrderType] = '{1}')", _CurWorkshop, _CurOrderType);
            }
            BindJoList();
            this.Update();
        }

        private void ansOrderType_MenuClick(object sender, MenuItemEventArgs e)
        {
            // show he selected Workshop as Ans Button text
            ToolBarButton oSender = (ToolBarButton)sender;
            oSender.Text = e.MenuItem.Text;
            _CurOrderType = (string)e.MenuItem.Tag;
            if (String.IsNullOrEmpty(_CurWorkshop))
            {
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([OrderType] = '{0}')", _CurOrderType);
            }
            else
            {
                _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([Workshop] = N'{0}' AND [OrderType] = '{1}')", _CurWorkshop, _CurOrderType);
            }
            BindJoList();
            this.Update();
        }
        #endregion

        private void BindClientList()
        {
            cboClient.Items.Clear();

            // cboClient is hidden, no need to bind data
            //
            //string sql = "SELECT DISTINCT [ClientName] FROM [dbo].[vwOrderList_Invoice]" + Environment.NewLine + _CurSqlWhere + Environment.NewLine + "ORDER BY [ClientName]";
            //SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql);

            //while (reader.Read())
            //{
            //    cboClient.Items.Add(reader.GetString(0));
            //}
            //reader.Close();
        }

        #region Bind Job Order List
        private void BindJoList()
        {
            this.lvwJoDefault.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();

            // 2010.08.08 paulus: 增長 Timeout 時間
            //SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 300;   // 5 分鐘
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd);


            while (reader.Read())
            {
                ListViewItem objItem = this.lvwJoDefault.Items.Add(reader.GetInt32(0).ToString());  // Order Number
                #region Order Type Icon
                switch (reader.GetInt32(9))                 // OrderTypeID Icons
                {
                    case (int)Common.Enums.OrderType.UploadFile:
                        objItem.SmallImage = new IconResourceHandle("Icons.JobOrder.UploadFile_16.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.JobOrder.UploadFile_32.png");
                        break;
                    case (int)Common.Enums.OrderType.DirectPrint:
                        objItem.SmallImage = new IconResourceHandle("Icons.JobOrder.DirectPrint_16.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.JobOrder.DirectPrint_32.png");
                        break;
                    case (int)Common.Enums.OrderType.PsFile:
                        objItem.SmallImage = new IconResourceHandle("Icons.JobOrder.PsFile_16.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.JobOrder.PsFile_32.png");
                        break;
                    case (int)Common.Enums.OrderType.Plate:
                        objItem.SmallImage = new IconResourceHandle("Icons.JobOrder.Others_16.png");
                        objItem.LargeImage = new IconResourceHandle("Icons.JobOrder.Others_32.png");
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

            BindClientList();
        }

        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(_BaseSqlSelect + Environment.NewLine);
            sql.Append(_CurSqlWhere + Environment.NewLine);
            sql.Append(_CurSqlOrderBy);

            return sql.ToString();
        }
        #endregion

        private void ResetForm()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            txtLookup.Text = String.Empty;
            cboClient.Items.Clear();
            cboClient.Text = String.Empty;
            ansJoDefault.Buttons[_ButtonIndex_Workshop].Text = oDict.GetWord("workshop");
            ansJoDefault.Buttons[_ButtonIndex_OrderType].Text = oDict.GetWord("order_type");
            _CurSqlWhere = _BaseSqlWhere;
            _CurSqlOrderBy = _BaseSqlOrderBy;
            _CurWorkshop = String.Empty;
            _CurOrderType = String.Empty;
        }

        private void DoLookup()
        {
            string target = txtLookup.Text.Trim();
            if (!(String.IsNullOrEmpty(target)))
            {
                ResetForm();
                txtLookup.Text = target;
                _BaseSqlWhere = String.Format("WHERE ([StatusID] BETWEEN {0} AND {1})", Common.Enums.Workflow.Cancelled.ToString("d"), Common.Enums.Workflow.Completed.ToString("d")) +
                                String.Format(" AND ([OrderID] LIKE '%{0}%' OR [ClientName] LIKE N'%{0}%' OR [Attachment] LIKE N'%{0}%' OR [DateReceived] LIKE '%{0}%' OR [OrderedBy] LIKE N'%{0}%' OR [InvoiceNumber] LIKE '%{0}%' )", target);
                _CurSqlWhere = _BaseSqlWhere;
                BindJoList();
                this.Update();
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
                        oUploadFile.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                        oUploadFile.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.DirectPrint:
                        xFilm5.JobOrder.Forms.DirectPrint oDirectPrint = new xFilm5.JobOrder.Forms.DirectPrint();
                        oDirectPrint.EditMode = Common.Enums.EditMode.Read;
                        oDirectPrint.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                        oDirectPrint.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.PsFile:
                        xFilm5.JobOrder.Forms.PsFile oPsFile = new xFilm5.JobOrder.Forms.PsFile();
                        oPsFile.EditMode = Common.Enums.EditMode.Read;
                        oPsFile.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                        oPsFile.ShowDialog();
                        break;
                    case (int)Common.Enums.OrderType.Plate:
                        xFilm5.JobOrder.Forms.Plate oPlate = new xFilm5.JobOrder.Forms.Plate();
                        oPlate.EditMode = Common.Enums.EditMode.Read;
                        oPlate.OrderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                        oPlate.ShowDialog();
                        break;
                }
            }
        }

        private void cboClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            string client = (string)cboClient.SelectedItem;
            ResetForm();
            _CurSqlWhere = _BaseSqlWhere + String.Format(" AND ([ClientName] = N'{0}')", client);
            BindJoList();
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

        private void lvwJoDefault_DoubleClick_1(object sender, EventArgs e)
        {
            if (lvwJoDefault.SelectedItem != null)
            {
                int orderId = Convert.ToInt32(lvwJoDefault.SelectedItem.Text);
                ShowJobOrder(orderId);
            }
        }

        private void lvwJoDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwJoDefault.MultiSelect && lvwJoDefault.CheckBoxes)
            {
                foreach (ListViewItem item in lvwJoDefault.SelectedItems)
                {
                    item.Checked = true;
                }
            }
        }
    }
}