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

namespace xFilm5.JobOrder.Forms
{
    public partial class Plate : Form, IGatewayComponent
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _ClientId = 0;
        private string _Comment = String.Empty;

        public Plate()
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
            ControlsDisabledByDefault();

            if (xFilm5.Controls.Utility.User.IsClient())
            {
                SetClientForm();
            }

            if ((_EditMode == Common.Enums.EditMode.Edit) || (_EditMode == Common.Enums.EditMode.Read))
            {
                ShowOrder();
            }
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            gbxHeader.Text = oDict.GetWord("order_id");
            lblClient.Text = oDict.GetWord("client");
            chkPrepressOp.Text = oDict.GetWordWithColon("prepress_op");
            lblPriority.Text = oDict.GetWordWithColon("priority");
            lblWorkshop.Text = oDict.GetWordWithColon("workshop");

            chkOthers.Text = oDict.GetWordWithColon("others");
            chkStandardSize.Text = oDict.GetWordWithColon("standard_size");
            chkOtherSize.Text = oDict.GetWordWithColon("other_size");
            chkGripperDistance.Text = oDict.GetWordWithColon("gripper_distance");
            chkGripperEdge.Text = oDict.GetWordWithColon("gripper_edge");
            chkImposition.Text = oDict.GetWord("imposition");
            chkBook.Text = oDict.GetWord("book");
            chkProofingWith.Text = oDict.GetWordWithColon("digital_proof");

            lblTotalPages.Text = oDict.GetWordWithColon("total_pages");
            lblTotalFilms.Text = oDict.GetWordWithColon("total_films");
            lblRemarks.Text = oDict.GetWordWithColon("remarks");
            chkAttachment.Text = oDict.GetWordWithColon("attachment");
            chkPickUp.Text = oDict.GetWord("pick_up");
            chkDeliverTo.Text = oDict.GetWordWithColon("deliver_to");
            lblPages.Text = oDict.GetWord("pages");
            lblPCS.Text = oDict.GetWord("pcs");
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
                        this.ansToolbar.Buttons.Add(cmdSaveClose);
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
                    this.ansToolbar.Buttons.Add(cmdSaveClose);
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
                                this.ansToolbar.Buttons.Add(cmdDispatch);
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
            toolTip1.SetToolTip(cmdAttachment, "Select upload file");
            toolTip1.SetToolTip(cmdNewAddress, "Add new address");
            toolTip1.SetToolTip(txtAttachment, "Click the Browse button to upload your filepaulus");
            toolTip1.SetToolTip(txtTotalPages, "Numeric only");
            toolTip1.SetToolTip(txtTotalFilms, "Numeric only");

            if ((_EditMode == Common.Enums.EditMode.Edit) || (_EditMode == Common.Enums.EditMode.Read))
            {
                gbxHeader.Text += ": " + _OrderId.ToString();
                txtClientName.Visible = true;
            }
            else
            {
                if (_EditMode == Common.Enums.EditMode.Add)
                {
                    chkStandardSize.Checked = true;
                    chkPickUp.Checked = true;
                }
            }
            txtTotalPages.Validator = new TextBoxValidation("String(value).match(/^[-]{0,1}[0-9]{1,9}$/)", "Bad value", "0-9");
            txtTotalFilms.Validator = new TextBoxValidation("String(value).match(/^[-]{0,1}[0-9]{1,9}$/)", "Bad value", "0-9");
        }

        private void SetDropdowns()
        {
            //            T_Priority.LoadCombo(ref cboPriority, "Name", false);
            xFilm5.Settings.LoadComboBox.Priority(ref cboPriority);

            Client_User.LoadCombo(ref cboPrepressOp, "FullName", false, false, "", "SecurityLevel = 2");
            Client_User.LoadCombo(ref cboWorkshop, "FullName", false, false, "", "SecurityLevel = 6");

            T_SoftwareVersion.LoadCombo(ref cboCorelDraw, "VersionNumber", false, false, "", "SoftwareID = 11");
            T_SoftwareVersion.LoadCombo(ref cboIllustrator, "VersionNumber", false, false, "", "SoftwareID = 5");
            T_SoftwareVersion.LoadCombo(ref cboFreeHand, "VersionNumber", false, false, "", "SoftwareID = 3");
            T_SoftwareVersion.LoadCombo(ref cboPageMaker, "VersionNumber", false, false, "", "SoftwareID = 1");
            T_SoftwareVersion.LoadCombo(ref cboPhotoShop, "VersionNumber", false, false, "", "SoftwareID = 7");
            T_SoftwareVersion.LoadCombo(ref cboQuarkXpress, "VersionNumber", false, false, "", "SoftwareID = 9");
            T_SoftwareVersion.LoadCombo(ref cboMsWord, "VersionNumber", false, false, "", "SoftwareID = 13");

            T_FriendlyService.LoadCombo(ref cboPaperSize, "Name", false);
            T_LSR.LoadCombo(ref cboLSR, "Name", false);

            xFilm5.Settings.LoadComboBox.GripperEdge(ref cboGripperEdge);
            xFilm5.Settings.LoadComboBox.DigitalProof(ref cboProofingWith);

            if (_EditMode == Common.Enums.EditMode.Add)
            {
                Client.LoadCombo(ref cboClient, "Name", false, false, "", "Status >= 1");
                _ClientId = Convert.ToInt32(cboClient.SelectedValue.ToString());

                Settings.ComboBoxDefault.Priority(ref cboPriority);
                Settings.ComboBoxDefault.CorelDraw(ref cboCorelDraw);
                Settings.ComboBoxDefault.Illustrator(ref cboIllustrator);
                Settings.ComboBoxDefault.FreeHand(ref cboFreeHand);
                Settings.ComboBoxDefault.PageMaker(ref cboPageMaker);
                Settings.ComboBoxDefault.PhotoShop(ref cboPhotoShop);
                Settings.ComboBoxDefault.QuarkXpress(ref cboQuarkXpress);
                Settings.ComboBoxDefault.MsWord(ref cboMsWord);
                Settings.ComboBoxDefault.LSR(ref cboLSR);
                Settings.ComboBoxDefault.StandardSize(ref cboPaperSize);
            }
        }

        private void ControlsDisabledByDefault()
        {
            cboPrepressOp.Enabled = false;
            cboCorelDraw.Enabled = false;
            cboIllustrator.Enabled = false;
            cboFreeHand.Enabled = false;
            cboPageMaker.Enabled = false;
            cboPhotoShop.Enabled = false;
            cboQuarkXpress.Enabled = false;
            cboMsWord.Enabled = false;
            txtOthers.Enabled = false;
            if (_EditMode == Common.Enums.EditMode.Add)
            {
                cboPaperSize.Enabled = true;
            }
            else
            {
                cboPaperSize.Enabled = false;
            }
            txtOtherSize.Enabled = false;
            txtGripperDistance.Enabled = false;
            cboGripperEdge.Enabled = false;
            cboProofingWith.Enabled = false;
            txtAttachment.Enabled = false;
            cmdAttachment.Enabled = false;
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

        #region ShowOrder(), SaveOrder(), VerifyOrder()
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
            switch (oDetails.Platform)
            {
                case 1:
                    radPC.Checked = true;
                    break;
                case 2:
                    radMac.Checked = true;
                    break;
            }
            if (oPrepressOp != null)
            {
                index = cboPrepressOp.FindString(oPrepressOp.FullName, 0);
                if (index >= 0 && index < cboPrepressOp.Items.Count)
                {
                    cboPrepressOp.SelectedIndex = index;
                    chkPrepressOp.Checked = true;
                }
            }
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
            #region - Software & Version
            string[] software = oDetails.Software.Split(' ');
            if (software.Length > 0)
            {
                switch (software[0])
                {
                    case "CorelDraw":
                        chkCorelDraw.Checked = true;
                        index = cboCorelDraw.FindString(software[1], 0);
                        if (index >= 0 && index < cboCorelDraw.Items.Count)
                        {
                            cboCorelDraw.SelectedIndex = index;
                        }
                        break;
                    case "Illustrator":
                    case "AI":
                        chkIllustrator.Checked = true;
                        index = cboIllustrator.FindString(software[1], 0);
                        if (index >= 0 && index < cboIllustrator.Items.Count)
                        {
                            cboIllustrator.SelectedIndex = index;
                        }
                        break;
                    case "FreeHand":
                        chkFreeHand.Checked = true;
                        index = cboFreeHand.FindString(software[1], 0);
                        if (index >= 0 && index < cboFreeHand.Items.Count)
                        {
                            cboFreeHand.SelectedIndex = index;
                        }
                        break;
                    case "PageMaker":
                        chkPageMaker.Checked = true;
                        index = cboPageMaker.FindString(software[1], 0);
                        if (index >= 0 && index < cboPageMaker.Items.Count)
                        {
                            cboPageMaker.SelectedIndex = index;
                        }
                        break;
                    case "PhotoShop":
                        chkPhotoShop.Checked = true;
                        index = cboPhotoShop.FindString(software[1], 0);
                        if (index >= 0 && index < cboPhotoShop.Items.Count)
                        {
                            cboPhotoShop.SelectedIndex = index;
                        }
                        break;
                    case "QuarkXpress":
                        chkQuarkXpress.Checked = true;
                        index = cboQuarkXpress.FindString(software[1], 0);
                        if (index >= 0 && index < cboQuarkXpress.Items.Count)
                        {
                            cboQuarkXpress.SelectedIndex = index;
                        }
                        break;
                    case "MsWord":
                    case "MS Word":
                        chkMsWord.Checked = true;
                        index = cboMsWord.FindString(software[1], 0);
                        if (index >= 0 && index < cboMsWord.Items.Count)
                        {
                            cboMsWord.SelectedIndex = index;
                        }
                        break;
                    case "Others":
                        chkOthers.Checked = true;
                        if (software.Length > 1)
                        {
                            txtOthers.Text = software[1];
                        }
                        break;
                }
            }
            #endregion

            #region - Film Instructions
            if (oDetails.StandardSize)
            {
                chkStandardSize.Checked = true;
                T_FriendlyService oPaperSize = T_FriendlyService.Load(oDetails.SizeID);
                if (oPaperSize != null)
                {
                    index = cboPaperSize.FindString(oPaperSize.Name, 0);
                    if (index >= 0 && index < cboPaperSize.Items.Count)
                    {
                        cboPaperSize.SelectedIndex = index;
                    }
                }
            }
            if (oDetails.OtherSize)
            {
                chkOtherSize.Checked = true;
                txtOtherSize.Text = oDetails.OtherSizeText;
            }
            chkCip3.Checked = oDetails.Positive;
            chkImposition.Checked = oDetails.Negative;
            chkBook.Checked = oDetails.EmulsionUp;
            chkGripperDistance.Checked = oDetails.SpotColor;
            if (oDetails.SpotColor)
            {
                txtGripperDistance.Text = oDetails.SpotColorText;
            }
            chkGripperEdge.Checked = oDetails.Barcode;
            if (oDetails.Barcode)
            {
                cboGripperEdge.SelectedIndex = oDetails.BarcodeQty;
            }

            T_LSR oLSR = T_LSR.Load(oDetails.LineScreensResolution);
            if (oLSR != null)
            {
                index = cboLSR.FindString(oLSR.Name, 0);
                if (index >= 0 && index < cboLSR.Items.Count)
                {
                    cboLSR.SelectedIndex = index;
                }
            }
            if (oDetails.Proofing)
            {
                chkProofingWith.Checked = true;

                cboProofingWith.SelectedIndex = oDetails.ProofingWith;
            }

            #endregion
            #endregion

            #region gbxFooter
            txtTotalPages.Text = oDetails.Pages.ToString("#,##0");
            txtTotalFilms.Text = oDetails.TotalFilms.ToString("#,##0");
            txtRemarks.Text = oOrder.Remarks;
            chkAttachment.Checked = oOrder.Attachment;
            txtAttachment.Text = oOrder.AttachmentURL;
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
                    Common.Order.WriteLog(_OrderId, Common.Enums.Workflow.Queuing);
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
                oHeader.ServiceType = (int)Common.Enums.OrderType.Plate;
                if (chkPrepressOp.Checked)
                {
                    oHeader.PrePressOp = (int)cboPrepressOp.SelectedValue;
                }
                else
                {
                    oHeader.PrePressOp = 0;
                }
                oHeader.Priority = (int)cboPriority.SelectedValue;
                oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;
                if (chkAttachment.Checked)
                {
                    oHeader.Attachment = true;
                    oHeader.AttachmentURL = txtAttachment.Text;
                }
                else
                {
                    oHeader.Attachment = false;
                    oHeader.AttachmentURL = String.Empty;
                }
                oHeader.Remarks = txtRemarks.Text;
                oHeader.Status = (int)Common.Enums.Workflow.Queuing;
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

            #region platform & software
            if (radPC.Checked)
            {
                oDetails.Platform = (int)Common.Enums.Platform.PC;
            }
            if (radMac.Checked)
            {
                oDetails.Platform = (int)Common.Enums.Platform.Mac;
            }
            if (chkCorelDraw.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.CorelDraw.ToString(), cboCorelDraw.Text);
            }
            if (chkIllustrator.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.Illustrator.ToString(), cboIllustrator.Text);
            }
            if (chkFreeHand.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.FreeHand.ToString(), cboFreeHand.Text);
            }
            if (chkPageMaker.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.PageMaker.ToString(), cboPageMaker.Text);
            }
            if (chkPhotoShop.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.PhotoShop.ToString(), cboPhotoShop.Text);
            }
            if (chkQuarkXpress.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.QuarkXpress.ToString(), cboQuarkXpress.Text);
            }
            if (chkMsWord.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.MsWord.ToString(), cboMsWord.Text);
            }
            if (chkOthers.Checked)
            {
                oDetails.Software = String.Format("{0} {1}", Common.Enums.Software.Others.ToString(), txtOthers.Text.Trim());
            }
            #endregion

            #region body
            if (chkStandardSize.Checked)
            {
                oDetails.StandardSize = true;
                oDetails.SizeID = (int)cboPaperSize.SelectedValue;
            }
            if (chkOtherSize.Checked)
            {
                oDetails.OtherSize = true;
                oDetails.OtherSizeText = txtOtherSize.Text;
            }
            oDetails.SpotColor = chkGripperDistance.Checked;
            oDetails.SpotColorText = txtGripperDistance.Text.Trim();
            oDetails.Barcode = chkGripperEdge.Checked;
            oDetails.BarcodeQty = Convert.ToInt16(cboGripperEdge.SelectedIndex);
            oDetails.Positive = chkCip3.Checked;
            oDetails.Negative = chkImposition.Checked;
            oDetails.EmulsionUp = chkBook.Checked;
            oDetails.LineScreensResolution = (int)cboLSR.SelectedValue;
            if (chkProofingWith.Checked)
            {
                oDetails.Proofing = true;
                oDetails.ProofingWith = cboProofingWith.SelectedIndex;
            }
            #endregion

            #region footer
            try
            {
                oDetails.Pages = Convert.ToInt16(txtTotalPages.Text.Trim());
            }
            catch
            {
                oDetails.Pages = 0;
            }
            try
            {
                oDetails.TotalFilms = Convert.ToInt16(txtTotalFilms.Text.Trim());
            }
            catch
            {
                oDetails.TotalFilms = 0;
            }
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
            bool result = false;

            Order_Internal oInternal = new Order_Internal();
            oInternal.OrderID = _OrderId;

            if (chkPrepressOp.Checked)
            {
                oInternal.OutputBy = (int)cboPrepressOp.SelectedValue;
            }
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

        private bool VerifyOrder()
        {
            bool result = true;
            string msg = String.Empty;

            #region Software
            if (!(chkCorelDraw.Checked || chkIllustrator.Checked || chkFreeHand.Checked || chkPageMaker.Checked || chkPhotoShop.Checked || chkQuarkXpress.Checked || chkMsWord.Checked || chkOthers.Checked))
            {
                result = false;
                msg = msg + Environment.NewLine + "Must pick one: Software";
            }
            else
            {
                if (chkOthers.Checked && txtOthers.Text.Trim() == String.Empty)
                {
                    result = false;
                    msg = msg + Environment.NewLine + "Others cannot be blank.";
                }
            }
            #endregion

            #region Size
            if (chkStandardSize.Checked || chkOtherSize.Checked)
            {
                if (chkOtherSize.Checked && txtOtherSize.Text.Trim() == String.Empty)
                {
                    result = false;
                    msg = msg + Environment.NewLine + "Other Size cannot be blank.";
                }
            }
            else
            {
                result = false;
                msg = msg + Environment.NewLine + "Must pick one: Standard Size / Other Size";
            }
            #endregion

            #region Gripper Distance
            if (chkGripperDistance.Checked && txtGripperDistance.Text.Trim() == String.Empty)
            {
                result = false;
                msg = msg + Environment.NewLine + "Gripper Distance cannot be blank.";
            }
            #endregion

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
        #endregion

        #region Toggle CheckBox Click Events
        private void chkPrepressOp_Click(object sender, EventArgs e)
        {
            cboPrepressOp.Enabled = chkPrepressOp.Checked;
        }

        private void Software_Click(object sender, EventArgs e)
        {
            CheckBox target = (CheckBox)sender;

            chkCorelDraw.Checked = false;
            chkIllustrator.Checked = false;
            chkFreeHand.Checked = false;
            chkPageMaker.Checked = false;
            chkPhotoShop.Checked = false;
            chkQuarkXpress.Checked = false;
            chkMsWord.Checked = false;
            chkOthers.Checked = false;

            switch (target.Name)
            {
                case "chkCorelDraw":
                    chkCorelDraw.Checked = !(target.Checked);
                    break;
                case "chkIllustrator":
                    chkIllustrator.Checked = !(target.Checked);
                    break;
                case "chkFreeHand":
                    chkFreeHand.Checked = !(target.Checked);
                    break;
                case "chkPageMaker":
                    chkPageMaker.Checked = !(target.Checked);
                    break;
                case "chkPhotoShop":
                    chkPhotoShop.Checked = !(target.Checked);
                    break;
                case "chkQuarkXpress":
                    chkQuarkXpress.Checked = !(target.Checked);
                    break;
                case "chkMsWord":
                    chkMsWord.Checked = !(target.Checked);
                    break;
                case "chkOthers":
                    chkOthers.Checked = !(target.Checked);
                    break;
            }

            cboCorelDraw.Enabled = chkCorelDraw.Checked;
            cboIllustrator.Enabled = chkIllustrator.Checked;
            cboFreeHand.Enabled = chkFreeHand.Checked;
            cboPageMaker.Enabled = chkPageMaker.Checked;
            cboPhotoShop.Enabled = chkPhotoShop.Checked;
            cboQuarkXpress.Enabled = chkQuarkXpress.Checked;
            cboMsWord.Enabled = chkMsWord.Checked;
            txtOthers.Enabled = chkOthers.Checked;
        }

        private void chkStandardSize_Click(object sender, EventArgs e)
        {
            cboPaperSize.Enabled = chkStandardSize.Checked;
            if (chkStandardSize.Checked)
            {
                chkOtherSize.Checked = false;
                txtOtherSize.Text = String.Empty;
                txtOtherSize.Enabled = false;
            }
        }

        private void chkOtherSize_Click(object sender, EventArgs e)
        {
            txtOtherSize.Enabled = chkOtherSize.Checked;
            if (chkOtherSize.Checked)
            {
                chkStandardSize.Checked = false;
                cboPaperSize.Enabled = false;
            }
        }

        private void chkProofingWith_Click(object sender, EventArgs e)
        {
            cboProofingWith.Enabled = chkProofingWith.Checked;
        }

        private void chkAttachment_Click(object sender, EventArgs e)
        {
            cmdAttachment.Enabled = chkAttachment.Checked;
            if (!(chkAttachment.Checked))
            {
                txtAttachment.Text = String.Empty;
            }
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

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Order?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
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
                        Billing fill = new Billing();
                        fill.OrderId = _OrderId;
                        fill.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        fill.ShowDialog();
                        #endregion
                        break;
                    case "billing":
                        #region popup billing form
                        Billing billing = new Billing();
                        billing.OrderId = _OrderId;
                        billing.EditMode = Common.Invoice.GetEditMode(_OrderId);
                        billing.ShowDialog();
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
                        txtAttachment.Text = FileName;
                    }
                    catch
                    {
                        txtAttachment.Text = String.Empty;
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

        private void chkGripperDistance_Click(object sender, EventArgs e)
        {
            txtGripperDistance.Enabled = chkGripperDistance.Checked;
        }

        private void chkGripperEdge_Click(object sender, EventArgs e)
        {
            cboGripperEdge.Enabled = chkGripperEdge.Checked;
        }

        private void chkBook_Click(object sender, EventArgs e)
        {
            if (chkBook.Checked)
            {
                PlateComment comment = new PlateComment();
                comment.Closed += new EventHandler(Comment_Closed);
                comment.EditMode = Common.Enums.EditMode.Add;
                comment.ShowDialog();
            }
        }

        private void Comment_Closed(object sender, EventArgs e)
        {
            PlateComment comment = (PlateComment)sender;
            if (comment.IsCompleted)
            {
                _Comment = comment.Value;
            }
        }
    }
}