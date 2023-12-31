using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using MarkPasternak.Utility;

using xFilm5.DAL;
using System.Data.SqlClient;
using RestSharp;
using System.Configuration;

namespace xFilm5.JobOrder.Forms
{
    public partial class Plate5: Form, IGatewayComponent
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _ClientId = 0;
        private string _Comment = String.Empty;

        public Plate5()
        {
            InitializeComponent();
        }

        #region Public Properties
        public Common.Enums.EditMode EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }

        public int OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                _OrderId = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            SetDropdowns();
            SetVpsListView();
            ControlsDisabledByDefault();

            // Client (客) 同 nuStar 職員 用同一張 form，nuStar 職員可以選 Client
            if (xFilm5.Controls.Utility.User.IsClient())
            {
                SetClientForm();
                chkDeliverTo.Checked = false;
                if (_EditMode == Common.Enums.EditMode.Add) LoadVpsList_Available();
                SetDefaultWorkshop();
            }

            // Add New 同 Edit 同一張 form
            if ((_EditMode == Common.Enums.EditMode.Edit) || (_EditMode == Common.Enums.EditMode.Read))
            {
                ShowOrder();
            }
            DoVpsProofed();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            gbxHeader.Text = oDict.GetWord("order_id");
            lblClient.Text = oDict.GetWordWithColon("client");
            lblPriority.Text = oDict.GetWordWithColon("priority");
            lblWorkshop.Text = oDict.GetWordWithColon("workshop");

            lblTotalPages.Text = oDict.GetWordWithColon("total_pages");
            lblRemarks.Text = oDict.GetWordWithColon("remarks");
            chkPickUp.Text = oDict.GetWord("pick_up");
            chkDeliverTo.Text = oDict.GetWordWithColon("deliver_to");
            //lblPages.Text = oDict.GetWord("pages");

            colVpsFileName.Text = "VPS " + oDict.GetWord("file_name");
            colPlateSize.Text = oDict.GetWord("plate_size");
            colCreatedOn.Text = oDict.GetWord("created_on");
            colPrintedOn.Text = oDict.GetWord("printed_on");

            toolTip1.SetToolTip(cmdClientInfo, "Show client record");
            toolTip1.SetToolTip(cmdNewAddress, "Add new address");
            toolTip1.SetToolTip(butPlate, oDict.GetWord("plate"));
            toolTip1.SetToolTip(butBlueprint, oDict.GetWord("blueprints"));
        }

        private void SetAnsToolbar()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            #region prepare the Buttons
            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", oDict.GetWord("save"));
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", System.Web.HttpUtility.UrlDecode(oDict.GetWord("save_close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("cancel"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

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
            ToolBarButton cmdCompleted = new ToolBarButton("Compeleted", oDict.GetWord("completed"));
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

            #region create the Buttons
            if (_EditMode != Common.Enums.EditMode.Read)
            {
                if (xFilm5.Controls.Utility.User.IsClient())
                {
                    if (_EditMode == Common.Enums.EditMode.Add)
                    {
                        this.ansToolbar.Buttons.Add(cmdSave);
                        //this.ansToolbar.Buttons.Add(cmdSaveClose);
                    }
                    else
                    {
                        switch (Common.Order.Status(_OrderId))
                        {
                            case Common.Enums.Workflow.Queuing:
                                this.ansToolbar.Buttons.Add(cmdDelete);
                                break;
                        }
                        this.ansToolbar.Buttons.Add(cmdComment);
                    }
                }
                else
                {
                    this.ansToolbar.Buttons.Add(cmdSave);
                    //this.ansToolbar.Buttons.Add(cmdSaveClose);
                    if (_EditMode != Common.Enums.EditMode.Add)
                    {
                        #region Admin 或者 Account 可以 delete
                        if ((xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Admin) ||
                            (xFilm5.Controls.Utility.User.UserRole() == (int)DAL.Common.Enums.UserRole.Account))
                        {
                            this.ansToolbar.Buttons.Add(cmdDelete);
                        }
                        #endregion
                        this.ansToolbar.Buttons.Add(sep);
                        #region 根據 Order.Status 顯示不同的 button
                        switch (Common.Order.Status(_OrderId))
                        {
                            case Common.Enums.Workflow.Queuing:
                                this.ansToolbar.Buttons.Add(cmdRetouch);
                                this.ansToolbar.Buttons.Add(cmdDownload);
                                break;
                            case Common.Enums.Workflow.Retouch:
                                this.ansToolbar.Buttons.Add(cmdPrinting);
                                this.ansToolbar.Buttons.Add(cmdDownload);
                                break;
                            case Common.Enums.Workflow.Printing:
                                this.ansToolbar.Buttons.Add(cmdChecked);
                                this.ansToolbar.Buttons.Add(cmdFill);
                                break;
                            case Common.Enums.Workflow.ProofingOutgoing:
                                this.ansToolbar.Buttons.Add(cmdIn);
                                break;
                            case Common.Enums.Workflow.ProofingIncoming:
                                this.ansToolbar.Buttons.Add(cmdChecked);
                                break;
                            case Common.Enums.Workflow.Ready:
                                //this.ansToolbar.Buttons.Add(cmdDispatch);         2018.05.08 paulus: 冇咗 displatch, 直跳 completed
                                this.ansToolbar.Buttons.Add(cmdBilling);
                                break;
                            case Common.Enums.Workflow.Dispatch:
                                this.ansToolbar.Buttons.Add(cmdCompleted);
                                this.ansToolbar.Buttons.Add(cmdBilling);
                                break;
                            case Common.Enums.Workflow.Completed:
                                this.ansToolbar.Buttons.Add(cmdInvoice);
                                break;
                        }
                        #endregion
                        this.ansToolbar.Buttons.Add(cmdComment);
                    }
                }
            }
            #endregion
            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
            if ((Common.Config.IamStaff) && (_EditMode == Common.Enums.EditMode.Add))
            {
                cboClient.Visible = true;
            }

            if ((_EditMode == Common.Enums.EditMode.Edit) || (_EditMode == Common.Enums.EditMode.Read))
            {
                gbxHeader.Text += ": " + _OrderId.ToString();
                txtClientName.Visible = true;
            }
            else
            {
                if (_EditMode == Common.Enums.EditMode.Add)
                {
                    chkPickUp.Checked = true;
                }
            }
            txtTotalPages.Validator = new TextBoxValidation("String(value).match(/^[-]{0,1}[0-9]{1,9}$/)", "Bad value", "0-9");

            txtTotalPages.TextAlign = HorizontalAlignment.Right;
            txtTotalPages.ReadOnly = true;
            txtTotalCIP3.TextAlign = HorizontalAlignment.Right;
            txtTotalCIP3.ReadOnly = true;
            txtTotalBlueprints.TextAlign = HorizontalAlignment.Right;
            txtTotalBlueprints.ReadOnly = true;

            // 2017.05.11 paulus: 加闊畫面
            int enlarge = 200;
            this.Size = new Size(this.Width + enlarge, this.Height);
            colVpsFileName.Width += enlarge;
        }

        private void SetDropdowns()
        {
            //            T_Priority.LoadCombo(ref cboPriority, "Name", false);
            xFilm5.Settings.LoadComboBox.Priority(ref cboPriority);

            Client_User.LoadCombo(ref cboWorkshop, "FullName", false, false, "", "SecurityLevel = 6");

            if (_EditMode == Common.Enums.EditMode.Add)
            {
                //Client.LoadCombo(ref cboClient, "Name", false, false, "", "Status >= 1");

                //祇顯示有 VPS 的客
                String clientListWithVps = xFilm5.Controls.Utility.PrintQueue_VPS.AvailablePlateClientDelimitedList();
                Client.LoadCombo(ref cboClient, "Name", false, true, "", String.Format("ID IN ({0})", (clientListWithVps == String.Empty ? "0" : clientListWithVps)));
                _ClientId = Convert.ToInt32(cboClient.SelectedValue.ToString());

                Settings.ComboBoxDefault.Priority(ref cboPriority);
            }
        }

        private void ControlsDisabledByDefault()
        {
            cboDeliveryAddress.Enabled = false;
            cmdNewAddress.Enabled = false;
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

                string sql = String.Format("ClientID = {0}", _ClientId.ToString());
                Client_AddressBook.LoadCombo(ref cboDeliveryAddress, "Name", false, false, String.Empty, sql);

                cmdClientInfo.Visible = false;

                Client_User oBranch = Client_User.Load(client.Branch);
                if (oBranch != null)
                {
                    int index = cboWorkshop.FindString(oBranch.FullName, 0);
                    if (index >= 0 && index < cboWorkshop.Items.Count)
                    {
                        cboWorkshop.SelectedIndex = index;
                    }
                }
            }
        }

        private void SetVpsListView()
        {
            lvwVpsList.CheckBoxes = false;
            lvwVpsList.GridLines = true;
            colVpsPrintQueueId.Visible = false;

            lvwVpsList.Click += lvwVpsList_Click;
            //lvwVpsList.ItemCheck += LvwVpsList_ItemCheck;
            //lvwVpsList.SelectedIndexChanged += LvwVpsList_SelectedIndexChanged;

            // 2016.12.15 pulus: ListView disabled 太暗睇唔清，改為 click 冇反應
            //if (_EditMode != Common.Enums.EditMode.Add) lvwVpsList.Enabled = false;
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

            OrderHeader oOrder = OrderHeader.Load(_OrderId);
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
                            System.Web.HttpResponse response = objContext.Response;    // prefer to use Gizmox instead of: this.Context.HttpContext.Response;

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
                        objContext.Response.Write(String.Format("<html><body><h>Order ID: {0}, attachment not found!</h></body></html>", _OrderId.ToString()));
                    }
                }
                else
                {
                    objContext.Response.Write(String.Format("<html><body><h>Order ID: {0}, no attachment!</h></body></html>", _OrderId.ToString()));
                }
            }

            return objGH;
        }
        #endregion

        #region ShowOrder
        private void ShowOrder()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            int index = 0;
            OrderHeader oOrder = OrderHeader.Load(_OrderId);
            Client oClient = Client.Load(oOrder.ClientID);
            Order_Details oDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", oOrder.ID.ToString()));
            T_Priority oPriority = T_Priority.Load(oOrder.Priority);
            Client_User oPrepressOp = Client_User.Load(oOrder.PrePressOp);
            Client_User oWorkshop = Client_User.Load(oOrder.ProofingOp);

            _ClientId = oOrder.ClientID;
            cboWorkshop.BackColor = Color.LightYellow;

            #region gbxHeader
            lblClient.Text = String.Format(oDict.GetWord("client") + " ( {0} ):", oClient.ID.ToString());
            txtClientName.Text = oClient.Name;
            index = cboPriority.FindString(oDict.GetWord(oPriority.Name), 0);
            if (index >= 0 && index < cboPriority.Items.Count)
            {
                cboPriority.SelectedIndex = index;
            }
            if (oWorkshop != null)
            {
                index = cboWorkshop.FindString(oWorkshop.FullName, 0);
                if (index >= 0 && index < cboWorkshop.Items.Count)
                {
                    cboWorkshop.SelectedIndex = index;
                }
            }
            #endregion

            #region gbxDetails
            //
            #region prepare sql statement
            String sql = String.Format(@"
SELECT TOP 1000 [ClientID]
      ,[OrderHeaderId]
      ,[CupsJobID]
      ,[CupsJobTitle]
      ,[PlateSize]
      ,[BlueprintOrdered]   -- 05
      ,[PrintQueueID]
      ,[Status]
      ,CONVERT(VARCHAR(19), [PrintedOn], 120)
      ,[Retired]
      ,[VpsPrintQueueID]    -- 10
      ,[VpsFileName]
      ,CONVERT(VARCHAR(19), [VpsCreatedOn], 120)
      ,[VpsPlateOrdered]
      ,[VpsRetired]
      --,[VpsAge]             -- 15
      ,[CheckedPlate]         -- 15
      ,[CheckedCip3]
      ,[CheckedBlueprint]

FROM [dbo].[vwPrintQueueVpsList_Ordered]
WHERE [OrderHeaderId] = {0}
ORDER BY [VpsFileName]", _OrderId.ToString());
            #endregion
            
            #region fill the ordered list
            int iCount = 1, totalPages = 0, totalCip3 = 0;
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                String vpsFileName = reader.GetString(11);          // Vps File Name
                ListViewItem objItem = this.lvwVpsList.Items.Add(reader.GetInt32(10).ToString());           // VpsPrintQueueID
                objItem.SubItems.Add(iCount.ToString());            // Line Number
                objItem.SubItems.Add(vpsFileName.Substring(0, (vpsFileName.Length - 4)));                   // Vps File Name, without .VPS
                objItem.SubItems.Add(reader.GetBoolean(15) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // plate checkbox
                objItem.SubItems.Add(reader.GetBoolean(16) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // cip3 checkbox
                objItem.SubItems.Add(reader.GetBoolean(17) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // blueprint checkbox
                objItem.SubItems.Add(reader.GetString(4));          // Plate Sizze
                objItem.SubItems.Add(reader.GetString(12));         // Created On
                objItem.SubItems.Add(reader.GetString(8));          // Printed On

                iCount++;
                totalPages = reader.GetBoolean(15) == true ? totalPages + 1 : totalPages;
                totalCip3 = reader.GetBoolean(14) == true ? totalCip3 + 1 : totalCip3;
            }
            reader.Close();
            #endregion
            //
            #endregion

            #region gbxFooter
            txtTotalPages.Text = totalPages.ToString("#,##0");  // oDetails.Pages.ToString("#,##0");
            txtTotalCIP3.Text = totalCip3.ToString("#,##0");
            txtTotalBlueprints.Text = CountBlueprints().ToString("#,##0");

            txtRemarks.Text = oOrder.Remarks;
            switch (oDetails.DeliveryMethod)
            {
                case 1:
                    chkPickUp.Checked = true;
                    break;
                case 2:
                    chkDeliverTo.Checked = true;
                    Client_AddressBook oDeliveryAddr = Client_AddressBook.Load(oDetails.DeliveryAddr);
                    if (oDeliveryAddr != null)
                    {
                        cboDeliveryAddress.Text = oDeliveryAddr.Name;
                    }
                    break;
            }
            #endregion
        }

        private int CountBlueprints()
        {
            int result = 0;

            String curFileName = String.Empty;
            foreach (ListViewItem item in lvwVpsList.Items)
            {
                String thisFileName = item.SubItems[colVpsFileName.Index].Text;
                thisFileName = thisFileName.Substring(0, thisFileName.LastIndexOf("("));
                Boolean checkedBp = item.SubItems[colBlueprint.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;
                if (checkedBp) result = thisFileName != curFileName ? result + 1 : result;
                curFileName = thisFileName;
            }

            return result;
        }
        #endregion

        #region SaveOrder(), VerifyOrder(), LoadVpsList()
        private bool SaveOrder()
        {
            bool result = false;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    if (VerifyOrder())
                    {
                        result = SaveNew();

                        #region save 書版資料 => comment
                        if ((_OrderId != 0) && (_Comment != String.Empty))
                        {
                            xFilm5.DAL.OrderComment comment = new OrderComment();
                            comment.OrderID = _OrderId;
                            comment.Comment = _Comment;
                            comment.Save();
                        }
                        #endregion
                    }
                    break;
                case (int)Common.Enums.EditMode.Edit:
                    result = SaveEdit();
                    break;
            }
            return result;
        }

        private bool SaveNew()
        {
            bool result = false;

            if (SaveNewHeader())
            {
                if (SaveNewDetails() && SaveNewInternal())
                {
                    result = true;

                    //2016.06.17 paulus: 直接跳級至 Queuing => Ready
                    Common.Order.WriteLog(_OrderId, Common.Enums.Workflow.Ready);

                    //2016.07.17 paulus: Plate5 根據 order，搬運 Tiff 同 Blueprint 檔案
                    SavePrintQueue();
                    //xFilm5.Controls.Utility.JobOrder.Plate5_Shuffle(_OrderId);
                }
                else
                {
                    // delete the Header
                    OrderHeader oHeader = OrderHeader.Load(_OrderId);
                    oHeader.Delete();
                }
            }

            return result;
        }

        private bool SaveNewHeader()
        {
            bool result = false;

            try
            {
                OrderHeader oHeader = new OrderHeader();

                oHeader.ClientID = _ClientId;
                oHeader.UserID = Common.Config.CurrentUserId;
                oHeader.ServiceType = (int)Common.Enums.OrderType.Plate5;
                oHeader.PrePressOp = 0;
                oHeader.Priority = (int)cboPriority.SelectedValue;
                oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;

                oHeader.Attachment = false;

                oHeader.Remarks = txtRemarks.Text;
                //2016.06.17 paulus: 直1跳級至 Queuing => Ready
                oHeader.Status = (int)Common.Enums.Workflow.Ready;
                oHeader.DateReceived = DateTime.Now;
                oHeader.Save();

                _OrderId = oHeader.ID;
                result = true;
            }
            catch { }
            return result;
        }

        private bool SaveNewDetails()
        {
            bool result = false;

            Order_Details oDetails = new Order_Details();

            oDetails.OrderID = _OrderId;

            #region body
            //oDetails.Positive = chkCip3.Checked;

            //oDetails.Proofing = chkProofingWith.Checked;        // Plate5 改為: 要唔要藍紙
            #endregion

            #region footer: total pages, delivery address
            //try
            //{
            //    oDetails.Pages = Convert.ToInt16(txtTotalPages.Text.Trim());
            //}
            //catch
            //{
            //    oDetails.Pages = 0;
            //}
            short totalPages = 0;
            oDetails.Pages = short.TryParse(txtTotalPages.Text.Trim(), out totalPages) ? totalPages : totalPages;
            if (chkPickUp.Checked)
            {
                oDetails.DeliveryMethod = (int)Common.Enums.DeliveryMethod.PickUp;
            }
            if (chkDeliverTo.Checked)
            {
                oDetails.DeliveryMethod = (int)Common.Enums.DeliveryMethod.DeliverTo;
                oDetails.DeliveryAddr = (int)cboDeliveryAddress.SelectedValue;
            }
            #endregion

            try
            {
                oDetails.Save();
                result = true;
            }
            catch { }

            return result;
        }

        private bool SaveNewInternal()
        {
            bool result = true;

            Order_Internal oInternal = new Order_Internal();
            oInternal.OrderID = _OrderId;

            // 冇咗 PrepressOp，不過都要 update dbo.Internal，吉嘅都要，避免 related tables 麻煩
            //if (chkPrepressOp.Checked)
            //{
            //    oInternal.OutputBy = (int)cboPrepressOp.SelectedValue;
            //}
            try
            {
                oInternal.Save();
                result = true;
            }
            catch { }

            return result;
        }

        private bool SaveEdit()
        {
            bool result = false;

            try
            {
                OrderHeader oHeader = OrderHeader.Load(_OrderId);
                if (oHeader != null)
                {
                    oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;
                    oHeader.Save();
                }
                result = true;
            }
            catch { }

            return result;
        }

        private bool SavePrintQueue()
        {
            bool result = false;
            String lastBpFileName = "";

            foreach (ListViewItem item in lvwVpsList.Items)
            {
                int vpsPrintQueueId = Convert.ToInt32(item.SubItems[0].Text);
                String vpsFileName = item.SubItems[colVpsFileName.Index].Text;
                bool vpsChecked = item.SubItems[colPlate.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;
                bool cip3Checked = item.SubItems[colCIP3.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;
                bool bpChecked = item.SubItems[colBlueprint.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;

                String bpFileName = vpsFileName.Substring(0, vpsFileName.LastIndexOf('('));

                if (vpsChecked || cip3Checked || bpChecked)
                {
                    #region Save dbo.OrderPkPrintQueueVps
                    DAL.OrderPkPrintQueueVps pkVps = new OrderPkPrintQueueVps();
                    pkVps.OrderHeaderId = _OrderId;
                    pkVps.PrintQueueVpsId = vpsPrintQueueId;

                    pkVps.CheckedPlate = vpsChecked;
                    pkVps.CheckedCip3 = cip3Checked;
                    pkVps.CheckedBlueprint = bpChecked;
                    pkVps.CreatedOn = DateTime.Now;
                    pkVps.CreatedBy = DAL.Common.Config.CurrentUserId;
                    pkVps.ModifiedOn = DateTime.Now;
                    pkVps.ModifiedBy = DAL.Common.Config.CurrentUserId;
                    pkVps.Retired = false;
                    pkVps.Save();
                    #endregion

                    xFilm5.Controls.Utility.PrintQueue_LifeCycle.WriteLogWithVpsId(vpsPrintQueueId, DAL.Common.Enums.PrintQSubitemType.Order);
                }

                #region 抄 tiff / cip3 / blueprint
                if (vpsChecked)
                {
                    SetPlateOrderedToTrue(vpsPrintQueueId);

                    // 抄 tiff
                    Helper.BotHelper.PostPlate(vpsPrintQueueId);
                }

                if (cip3Checked)
                {
                    // 抄 cip3
                    //Post_xFilm5Bot_Cip3(vpsPrintQueueId);
                }

                if (bpChecked)
                {
                    SetBlueprintOrderedToTrue(vpsPrintQueueId);

                    // debug 用
                    //String source = Path.Combine(@"\\192.168.12.230\DirectPrint\EfiProof", "*" + bpFileName +"*");
                    //FileInfo[] files = Helper.FileHelper.FileUtils.GetFilesMatchWildCard(source, "MonAgent", "nx-9602");

                    // 2017.04.11 paulus: 改為 wildcard，祇 call 一次
                    if (bpFileName != lastBpFileName)
                    {
                        // 抄 blueprint
                        Helper.BotHelper.PostBlueprint(vpsPrintQueueId);

                        lastBpFileName = bpFileName;
                    }
                }
                #endregion
            }

            return result;
        }

        private void SetPlateOrderedToTrue(int pqVpsId)
        {
            DAL.PrintQueue_VPS pqVps = DAL.PrintQueue_VPS.Load(pqVpsId);
            if (pqVps != null)
            {
                pqVps.PlateOrdered = true;
                pqVps.ModifiedOn = DateTime.Now;
                pqVps.ModifiedBy = DAL.Common.Config.CurrentUserId;
                pqVps.Save();
            }
        }

        private void SetBlueprintOrderedToTrue(int pqVpsId)
        {
            DAL.PrintQueue_VPS pqVps = DAL.PrintQueue_VPS.Load(pqVpsId);
            if (pqVps != null)
            {
                DAL.PrintQueue pq = DAL.PrintQueue.Load(pqVps.PrintQueueID);
                if (pq != null)
                {
                    pq.BlueprintOrdered = true;
                    pq.ModifiedOn = DateTime.Now;
                    pq.ModifiedBy = DAL.Common.Config.CurrentUserId;
                    pq.Save();
                }
            }
        }

        private void Post_xFilm5Bot_Cip3(int pqVpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("cip3/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                PrintQueueVpsId = pqVpsId.ToString(),
                AnotherParam = 19.99
            });
            client.Execute(request);
        }

        private bool VerifyOrder()
        {
            bool result = true;
            string msg = String.Empty;

            #region Delivery
            if (!chkPickUp.Checked && !chkDeliverTo.Checked)
            {
                result = false;
                msg = msg + Environment.NewLine + "Must pick one: Pick Up / Deliver To";
            }
            #endregion

            if (!result)
            {
                MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

        private void LoadVpsList_Available()
        {
            this.lvwVpsList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);

            while (reader.Read())
            {
                String vpsFileName = reader.GetString(11);          // Vps File Name

                ListViewItem objItem = this.lvwVpsList.Items.Add(reader.GetInt32(10).ToString());               // get VpsPrintQueueID
                objItem.SubItems.Add(iCount.ToString());            // Line Number
                if ((vpsFileName.Length - 4) > 0)
                    objItem.SubItems.Add(vpsFileName.Substring(0, (vpsFileName.Length - 4)));                   // show Vps File Name, without .VPS
                else
                    objItem.SubItems.Add(vpsFileName);
                objItem.SubItems.Add(new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());
                objItem.SubItems.Add(new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());
                objItem.SubItems.Add(new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());
                objItem.SubItems.Add(reader.GetString(4));          // Plate Sizze
                objItem.SubItems.Add(reader.GetString(12));         // Created On
                objItem.SubItems.Add(reader.GetString(8));          // Printed On

                iCount++;
            }
            reader.Close();
        }

        private string BuildSqlQueryString()
        {
            String sql = String.Format(@"
SELECT TOP 1000 [ClientID]
      ,[OrderID]
      ,[CupsJobID]
      ,[CupsJobTitle]
      ,[PlateSize]
      ,[BlueprintOrdered]   -- 05
      ,[PrintQueueID]
      ,[Status]
      ,CONVERT(VARCHAR(19), [PrintedOn], 120)
      ,[Retired]
      ,[VpsPrintQueueID]    -- 10
      ,[VpsFileName]
      ,CONVERT(VARCHAR(19), [VpsCreatedOn], 120)
      ,[VpsPlateOrdered]
      ,[VpsRetired]
      ,[VpsAge]             -- 15
FROM [dbo].[vwPrintQueueVpsList_AvailablePlate]
WHERE [ClientID] = {0}
ORDER BY [VpsFileName] DESC", _ClientId.ToString());

            return sql;
        }

        private void SetDefaultWorkshop()
        {
            String branch = xFilm5.Controls.Utility.Client.GetDefaultBranchName(_ClientId);
            if (branch != String.Empty)
            {
                int index = cboWorkshop.FindString(branch, 0);
                if (index >= 0 && index < cboWorkshop.Items.Count)
                {
                    cboWorkshop.SelectedIndex = index;
                }
            }
        }
        #endregion

        #region Toggle CheckBox Click Events
        private void chkProofingWith_Click(object sender, EventArgs e)
        {

        }

        private void chkPickUp_Click(object sender, EventArgs e)
        {
            if (chkPickUp.Checked)
            {
                chkDeliverTo.Checked = false;
                cboDeliveryAddress.Enabled = false;
                cmdNewAddress.Enabled = false;
            }
        }

        private void chkDeliverTo_Click(object sender, EventArgs e)
        {
            cboDeliveryAddress.Enabled = chkDeliverTo.Checked;
            cmdNewAddress.Enabled = chkDeliverTo.Checked;
            if (chkDeliverTo.Checked)
            {
                chkPickUp.Checked = false;
                if (_ClientId != 0)
                {
                    cboDeliveryAddress.DataSource = null;
                    //string where = String.Format("ClientID = {0} AND PrimaryAddr = 1", _ClientId.ToString());
                    string where = String.Format("ClientID = {0}", _ClientId.ToString());
                    Client_AddressBook.LoadCombo(ref cboDeliveryAddress, "Name", false, false, "", where);
                }
            }
        }
        #endregion

        private void DoVpsProofed()
        {
            if (_EditMode == Common.Enums.EditMode.Add)
            {
                //if ((chkVpsProofed.Checked) && (txtTotalPages.Text != String.Empty))
                if (txtTotalPages.Text != String.Empty)
                {
                    #region VPS Proofed, 顯示 Save/ Save & Close buttons
                    foreach (ToolBarButton button in ansToolbar.Buttons)
                    {
                        switch (button.Tag.ToString().ToLower())
                        {
                            case "save":
                            case "save & close":
                                button.Visible = true;
                                break;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region VPS Not Proofed, 隱藏 Save/ Save & Close buttons
                    foreach (ToolBarButton button in ansToolbar.Buttons)
                    {
                        switch (button.Tag.ToString().ToLower())
                        {
                            case "save":
                            case "save & close":
                                button.Visible = false;
                                break;
                        }
                    }
                    #endregion
                }
            }
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Order?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Order And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "delete":
                        MessageBox.Show("Cancel Order?", "Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdCancel_Click));
                        break;
                    case "retouch":
                        MessageBox.Show("Move Order to Retouch?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdRetouch_Click));
                        break;
                    case "printing":
                        MessageBox.Show("Move Order to Printing?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdPrinting_Click));
                        break;
                    case "checked":
                        MessageBox.Show("Are you sure this Order is checked OK?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdChecked_Click));
                        break;
                    case "in":
                        MessageBox.Show("Move Order to Proofing (In)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdProofingIncoming_Click));
                        break;
                    case "ready":
                        MessageBox.Show("Move Order to Ready?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdReady_Click));
                        break;
                    case "dispatch":
                        MessageBox.Show("Move Order to Dispatch?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDispatch_Click));
                        break;
                    case "completed":
                        MessageBox.Show("Move Order to Completed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdCompleted_Click));
                        break;
                    case "fill":
                        #region popup billing form
                        List<int> fillOrderIds = new List<int>(new int[] { _OrderId });

                        Billing5 fill5 = new Billing5();
                        fill5.OrderIdList = fillOrderIds;
                        fill5.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        fill5.ShowDialog();
                        #endregion
                        break;
                    case "billing":
                        #region popup billing form
                        List<int> billingOrderIds = new List<int>(new int[] { _OrderId });

                        Billing5 billing5 = new Billing5();
                        billing5.OrderIdList = billingOrderIds;
                        billing5.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        billing5.ShowDialog();
                        #endregion
                        break;
                    case "invoice":
                        #region popup billing form (read only)
                        Billing invoice = new Billing();
                        invoice.OrderId = _OrderId;
                        invoice.EditMode = Common.Enums.EditMode.Read;
                        invoice.ShowDialog();
                        #endregion
                        break;
                    case "download":
                        Link.Open(new GatewayReference(this, "open"));
                        break;
                    case "comment":
                        Comment oComment = new Comment();
                        oComment.OrderId = _OrderId;
                        oComment.EditMode = Common.Enums.EditMode.Edit;
                        oComment.ShowDialog();
                        break;
                }
            }
        }

        private void cmdClientInfo_Click(object sender, EventArgs e)
        {
            Sales.Client.ClientRecord clientRec = new xFilm5.Sales.Client.ClientRecord();
            clientRec.ClientId = _ClientId;
            clientRec.EditMode = Common.Enums.EditMode.Edit;
            clientRec.ShowDialog();
        }

        private void cmdAttachment_Click(object sender, EventArgs e)
        {
            ofdAttachment.MaxFileSize = Common.Config.MaxFileSize;
            ofdAttachment.Multiselect = false;
            ofdAttachment.Title = "Job Order > Upload File > Attachment";
            ofdAttachment.ShowDialog();
        }

        private void ofdAttachment_FileOk(object sender, CancelEventArgs e)
        {
            string FileName = string.Empty;
            string FullName = string.Empty;
            string inbox = Common.Client.InBox(_ClientId);

            OpenFileDialog oFileDialog = sender as OpenFileDialog;

            switch (oFileDialog.DialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        HttpPostedFileHandle file = oFileDialog.Files[0] as HttpPostedFileHandle;
                        FileName = Path.GetFileName(file.PostedFileName);
                        FullName = Path.Combine(inbox, FileName);
                        file.SaveAs(FullName);
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        private void cboClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _ClientId = (int)cboClient.SelectedValue;
                chkDeliverTo.Checked = false;
                LoadVpsList_Available();
                SetDefaultWorkshop();
            }
            catch
            {
                _ClientId = 0;
            }
        }

        #region ans Button Clicks: Save, SaveClose, Cancel, Retouch, Printing, Proofing, Ready, Dispatch, Completed
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveOrder())
                    {
                        Helper.BotHelper.PostSendFcmOnOrder(_OrderId);      // 發出 FCM notification

                        MessageBox.Show(String.Format("Order ID {0} is saved!", _OrderId.ToString()), "Save Result");
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is ReadOnly...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdSaveClose_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveOrder())
                    {
                        Helper.BotHelper.PostSendFcmOnOrder(_OrderId);      // 發出 FCM notification

                        MessageBox.Show(String.Format("Order ID {0} is saved!", _OrderId.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("Error found...Job aborted!\nPlease review your changes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToCancelled(_OrderId))
                    {
                        // 2016.07.21 paulus: 把 order 點選的 dbo.PrintQueueVPS reset，於是可以重新再落 order
                        xFilm5.Controls.Utility.PrintQueue.ResetOrder(_OrderId);

                        MessageBox.Show(String.Format("Order ID {0} is cancelled.", _OrderId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not cancel this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdRetouch_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToRetouch(_OrderId))
                    {
                        //MessageBox.Show(String.Format("Order ID {0} is moved to Retouch.", _OrderId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdPrinting_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToPrinting(_OrderId))
                    {
                        //MessageBox.Show(String.Format("Order ID {0} is moved to Printing.", _OrderId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdChecked_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    Order_Details oDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", _OrderId.ToString()));
                    if (oDetails != null)
                    {
                        if (oDetails.Proofing)
                        {
                            if (Common.Order.MoveToProofingOutgoing(_OrderId))
                            {
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (Common.Order.MoveToReady(_OrderId))
                            {
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdProofingIncoming_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToProofingIncoming(_OrderId))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdReady_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToReady(_OrderId))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdDispatch_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToDispatch(_OrderId))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdCompleted_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Common.Order.MoveToCompleted(_OrderId))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This order is protected...You can not move this order!\nPlease review the order/ invoice status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Record is Read Only...Job aborted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdCloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Comment_Closed(object sender, EventArgs e)
        {
            PlateComment comment = (PlateComment)sender;
            if (comment.IsCompleted)
            {
                _Comment = comment.Value;
            }
        }

        private void LvwVpsList_ItemCheck(object objSender, ItemCheckEventArgs objArgs)
        {
            int counts = 0;
            foreach (ListViewItem item in lvwVpsList.Items)
            {
                if (item.Checked) ++counts;
            }
            txtTotalPages.Text = counts.ToString("###");

            DoVpsProofed();
        }

        private void LvwVpsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwVpsList.MultiSelect && lvwVpsList.CheckBoxes)
            {
                foreach (ListViewItem item in lvwVpsList.SelectedItems)
                {
                    item.Checked = !(item.Checked);
                }
            }
        }

        private void lvwVpsList_Click(object sender, EventArgs e)
        {
            if (_EditMode == Common.Enums.EditMode.Add)
            {
                MouseEventArgs mea = (MouseEventArgs)e;
                int colIndex = xFilm5.Controls.ListViewHelper.GetHitColumn(lvwVpsList, mea);
                if (colIndex >= 0)
                {
                    int row = lvwVpsList.SelectedIndex;
                    ListViewItem item = lvwVpsList.Items[row];
                    ListViewItem.ListViewSubItem subitem = item.SubItems[colIndex];
                    switch (colIndex)
                    {
                        case 3:     // colPlate
                            if (subitem.Text.IndexOf("checkbox-no") >= 0)
                                subitem.Text = "icons.16x16.checkbox-ok-12x12.jpg.wgx";
                            else
                                subitem.Text = "icons.16x16.checkbox-no-12x12.jpg.wgx";
                            break;
                        case 4:     // colCIP3
                            ToggleWholeSet(row, colIndex);
                            break;
                        case 5:     // colBlueprint
                            ToggleWholeSet(row, colIndex);
                            break;
                    }
                    ToggleToolbar();
                }
            }
        }

        private void ToggleWholeSet(int row, int column)
        {
            ListViewItem curItem = lvwVpsList.Items[row];
            ListViewItem.ListViewSubItem colFilename = curItem.SubItems[2];        // colVpsFileName
            String filename = colFilename.Text.Substring(0, colFilename.Text.IndexOf("("));

            foreach (ListViewItem item in lvwVpsList.Items)
            {
                if (item.SubItems[2].Text.IndexOf(filename) >= 0)
                {
                    ListViewItem.ListViewSubItem subitem = item.SubItems[column];
                    if (subitem.Text.IndexOf("checkbox-no") >= 0)
                        subitem.Text = "icons.16x16.checkbox-ok-12x12.jpg.wgx";
                    else
                        subitem.Text = "icons.16x16.checkbox-no-12x12.jpg.wgx";
                }
            }
        }

        private void ToggleToolbar()
        {
            bool show = false;
            int plates = 0, cip3s = 0, blueprints = 0;
            String curfilename = String.Empty, lastfilename = String.Empty;

            foreach (ListViewItem item in lvwVpsList.Items)
            {
                ListViewItem.ListViewSubItem colFilename = item.SubItems[2];        // colVpsFileName
                int pos = colFilename.Text.IndexOf("(");
                if (pos >= 0)
                {
                    curfilename = colFilename.Text.Substring(0, pos);

                    #region 數 Plate
                    ListViewItem.ListViewSubItem vps = item.SubItems[3];
                    if (vps.Text.IndexOf("checkbox-ok") >= 0)
                    {
                        show = true;
                        plates++;
                    }
                    #endregion

                    #region 數 CIP3
                    ListViewItem.ListViewSubItem cip3 = item.SubItems[4];
                    if (cip3.Text.IndexOf("checkbox-ok") >= 0)
                    {
                        show = true;
                        if (curfilename != lastfilename) cip3s++;
                    }
                    #endregion

                    #region 數 Blueprint
                    ListViewItem.ListViewSubItem bp = item.SubItems[5];
                    if (bp.Text.IndexOf("checkbox-ok") >= 0)
                    {
                        show = true;
                        if (curfilename != lastfilename) blueprints++;
                    }
                    #endregion
                }
                lastfilename = curfilename;
            }
            if (show)
            {
                #region VPS Proofed, 顯示 Save/ Save & Close buttons
                foreach (ToolBarButton button in ansToolbar.Buttons)
                {
                    switch (button.Tag.ToString().ToLower())
                    {
                        case "save":
                        case "save & close":
                            button.Visible = true;
                            break;
                    }
                }
                #endregion
            }
            else
            {
                #region VPS Not Proofed, 隱藏 Save/ Save & Close buttons
                foreach (ToolBarButton button in ansToolbar.Buttons)
                {
                    switch (button.Tag.ToString().ToLower())
                    {
                        case "save":
                        case "save & close":
                            button.Visible = false;
                            break;
                    }
                }
                #endregion
            }
            txtTotalPages.Text = plates.ToString("#,###");
            txtTotalCIP3.Text = cip3s.ToString("#,###");
            txtTotalBlueprints.Text = blueprints.ToString("#,###");
        }

        private void chkVpsProofed_CheckedChanged(object sender, EventArgs e)
        {
            DoVpsProofed();
        }
    }
}