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
using System.Configuration;
using RestSharp;

namespace xFilm5.JobOrder.Forms
{
    public partial class Film5: Form, IGatewayComponent
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _ClientId = 0;
        private string _Comment = String.Empty;

        public Film5()
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

            chkProofingWith.Text = oDict.GetWord("blueprints");

            lblTotalPages.Text = oDict.GetWordWithColon("total_pages");
            lblRemarks.Text = oDict.GetWordWithColon("remarks");
            chkPickUp.Text = oDict.GetWord("pick_up");
            chkDeliverTo.Text = oDict.GetWordWithColon("deliver_to");
            lblPages.Text = oDict.GetWord("pages");

            colVpsFileName.Text = "VPS " + oDict.GetWord("file_name");
            colPlateSize.Text = oDict.GetWord("film_size");
            colCreatedOn.Text = oDict.GetWord("created_on");
            colPrintedOn.Text = oDict.GetWord("printed_on");
            chkVpsProofed.Text = oDict.GetWord("vps_proofed");
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
                        this.ansToolbar.Buttons.Add(cmdDelete);
                        this.ansToolbar.Buttons.Add(sep);
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
            toolTip1.SetToolTip(cmdClientInfo, "Show client record");
            toolTip1.SetToolTip(cmdNewAddress, "Add new address");
            toolTip1.SetToolTip(txtTotalPages, "Numeric only");

            if ((_EditMode == Common.Enums.EditMode.Edit) || (_EditMode == Common.Enums.EditMode.Read))
            {
                gbxHeader.Text += ": " + _OrderId.ToString();
                txtClientName.Visible = true;

                // 2016-07-19 paulus: Plate5 唔可以改，費事再 shuffle 啲 Tiff 同 Blueprint
                chkCip3.Enabled = false;
                chkProofingWith.Enabled = false;
                chkVpsProofed.Enabled = false;
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

            chkVpsProofed.ForeColor = Color.Red;
            lvwVpsList.Dock = DockStyle.Fill;

            //colPlateSize.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Left;

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
                String clientListWithVps = xFilm5.Controls.Utility.PrintQueue_VPS.AvailableFilmClientDelimitedList();
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
            lvwVpsList.CheckBoxes = true;
            colVpsPrintQueueId.Visible = false;

            if (_EditMode == Common.Enums.EditMode.Add)
            {
                lvwVpsList.ItemCheck += LvwVpsList_ItemCheck;
                lvwVpsList.SelectedIndexChanged += LvwVpsList_SelectedIndexChanged;
            }

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

        #region ShowOrder(), SaveOrder(), VerifyOrder(), LoadVpsList()
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
                //ListViewItem objItem = this.lvwVpsList.Items.Add(reader.GetInt32(10).ToString());           // VpsPrintQueueID
                //objItem.SubItems.Add(iCount.ToString());            // Line Number
                //objItem.SubItems.Add(vpsFileName.Substring(0, (vpsFileName.Length - 4)));                   // Vps File Name, without .VPS
                //objItem.SubItems.Add(reader.GetBoolean(15) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // plate checkbox
                //objItem.SubItems.Add(reader.GetBoolean(16) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // cip3 checkbox
                //objItem.SubItems.Add(reader.GetBoolean(17) == true ? new IconResourceHandle("16x16.checkbox-ok-12x12.jpg").ToString() : new IconResourceHandle("16x16.checkbox-no-12x12.jpg").ToString());      // blueprint checkbox
                //objItem.SubItems.Add(reader.GetString(4));          // Plate Sizze
                //objItem.SubItems.Add(reader.GetString(12));         // Created On
                //objItem.SubItems.Add(reader.GetString(8));          // Printed On

                ListViewItem objItem = this.lvwVpsList.Items.Add(reader.GetInt32(10).ToString());     // VpsPrintQueueID
                objItem.SubItems.Add(iCount.ToString());            // Line Number
                objItem.SubItems.Add(reader.GetString(11));         // Vps File Name
                objItem.SubItems.Add(reader.GetString(4));          // Plate Sizze
                objItem.SubItems.Add(reader.GetString(12));         // Created On
                objItem.SubItems.Add(reader.GetString(8));          // Printed On
                objItem.Checked = true;

                iCount++;
                totalPages = reader.GetBoolean(15) == true ? totalPages + 1 : totalPages;
                totalCip3 = reader.GetBoolean(14) == true ? totalCip3 + 1 : totalCip3;
            }
            reader.Close();
            #endregion
            //
            #endregion

            #region gbxFooter
            txtTotalPages.Text = oDetails.Pages.ToString("#,##0");
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

        private bool SaveOrder()
        {
            bool result = false;

            switch ((int)_EditMode)
            {
                case (int)Common.Enums.EditMode.Add:
                    if (VerifyOrder())
                    {
                        result = SaveNew();

                        if ((_OrderId != 0) && (_Comment != String.Empty))
                        {
                            // save 書版資料 => comment
                            xFilm5.DAL.OrderComment comment = new OrderComment();
                            comment.OrderID = _OrderId;
                            comment.Comment = _Comment;
                            comment.Save();
                        }
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
                    xFilm5.Controls.Utility.JobOrder.Plate5_Shuffle(_OrderId);
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
                oHeader.ServiceType = (int)Common.Enums.OrderType.Film5;
                oHeader.PrePressOp = 0;
                oHeader.Priority = (int)cboPriority.SelectedValue;
                oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;

                oHeader.Attachment = true;

                oHeader.Remarks = txtRemarks.Text;
                //2016.06.17 paulus: 直接跳級至 Queuing => Ready
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
            short pages = 0;

            Order_Details oDetails = new Order_Details();

            oDetails.OrderID = _OrderId;

            #region body
            //oDetails.Positive = chkCip3.Checked;

            //oDetails.Proofing = chkProofingWith.Checked;        // Plate5 改為: 要唔要藍紙
            #endregion

            #region footer
            oDetails.Pages = short.TryParse(txtTotalPages.Text.Trim(), out pages) ? pages : (short)0;

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

            //try
            //{
            oDetails.Save();
            result = true;
            //}
            //catch { }

            return result;
        }

        private bool SaveNewInternal()
        {
            bool result = false;

            Order_Internal oInternal = new Order_Internal();
            oInternal.OrderID = _OrderId;

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

            foreach (ListViewItem item in lvwVpsList.Items)
            {
                if (item.Checked)
                {
                    int vpsPrintQueueId = Convert.ToInt32(item.SubItems[0].Text);
                    DAL.PrintQueue_VPS vps = DAL.PrintQueue_VPS.Load(vpsPrintQueueId);
                    if (vps != null)
                    {
                        DAL.PrintQueue pq = DAL.PrintQueue.Load(vps.PrintQueueID);
                        if (pq != null)
                        {
                            #region Set filme ordered to true
                            vps.PlateOrdered = true;                    // HACK: Plate 同 Film 共用
                            vps.ModifiedOn = DateTime.Now;
                            vps.ModifiedBy = DAL.Common.Config.CurrentUserId;
                            vps.Save();
                            #endregion

                            #region 用 dbo.OrderPkPrintQueueVps 取代
                            //pq.OrderID = _OrderId;
                            //pq.ModifiedOn = DateTime.Now;
                            //pq.ModifiedBy = DAL.Common.Config.CurrentUserId;
                            //pq.Save();
                            #endregion

                            #region Save dbo.OrderPkPrintQueueVps
                            DAL.OrderPkPrintQueueVps pkVps = new OrderPkPrintQueueVps();
                            pkVps.OrderHeaderId = _OrderId;
                            pkVps.PrintQueueVpsId = vpsPrintQueueId;

                            pkVps.CheckedPlate = true;                  // HACK: Plate 同 Film 共用
                            pkVps.CreatedOn = DateTime.Now;
                            pkVps.CreatedBy = DAL.Common.Config.CurrentUserId;
                            pkVps.ModifiedOn = DateTime.Now;
                            pkVps.ModifiedBy = DAL.Common.Config.CurrentUserId;
                            pkVps.Retired = false;
                            pkVps.Save();
                            #endregion

                            xFilm5.Controls.Utility.PrintQueue_LifeCycle.WriteLogWithVpsId(vpsPrintQueueId, DAL.Common.Enums.PrintQSubitemType.Film);

                            #region 抄 tiff
                            Helper.BotHelper.PostFilm(vpsPrintQueueId);
                            #endregion

                            result = true;
                        }
                    }
                }
            }

            return result;
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
                ListViewItem objItem = this.lvwVpsList.Items.Add(reader.GetInt32(10).ToString());     // VpsPrintQueueID
                objItem.SubItems.Add(iCount.ToString());            // Line Number
                objItem.SubItems.Add(reader.GetString(11));         // Vps File Name
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
FROM [dbo].[vwPrintQueueVpsList_AvailableFilm]
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
                        //Billing fill = new Billing();
                        //fill.OrderId = _OrderId;
                        //fill.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        //fill.ShowDialog();
                        List<int> fillOrderIds = new List<int>(new int[] { _OrderId });

                        Billing5 fill5 = new Billing5();
                        fill5.OrderIdList = fillOrderIds;
                        fill5.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        fill5.ShowDialog();
                        #endregion
                        break;
                    case "billing":
                        #region popup billing form
                        //Billing billing = new Billing();
                        //billing.OrderId = _OrderId;
                        //billing.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        //billing.ShowDialog();
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

        private void chkVpsProofed_CheckedChanged(object sender, EventArgs e)
        {
            DoVpsProofed();
        }
    }
}