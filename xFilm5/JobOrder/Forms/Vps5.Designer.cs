namespace xFilm5.JobOrder.Forms
{
    partial class Vps5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vps5));
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.gbxDetails = new Gizmox.WebGUI.Forms.GroupBox();
            this.chkVpsProofed = new Gizmox.WebGUI.Forms.CheckBox();
            this.lvwVpsList = new Gizmox.WebGUI.Forms.ListView();
            this.colVpsPrintQueueId = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colLineNumber = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colVpsFileName = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colPlateSize = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colCreatedOn = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.colPrintedOn = ((Gizmox.WebGUI.Forms.ColumnHeader)(new Gizmox.WebGUI.Forms.ColumnHeader()));
            this.chkProofingWith = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkCip3 = new Gizmox.WebGUI.Forms.CheckBox();
            this.gbxFooter = new Gizmox.WebGUI.Forms.GroupBox();
            this.cboDeliveryAddress = new Gizmox.WebGUI.Forms.ComboBox();
            this.cmdNewAddress = new Gizmox.WebGUI.Forms.Button();
            this.chkDeliverTo = new Gizmox.WebGUI.Forms.CheckBox();
            this.chkPickUp = new Gizmox.WebGUI.Forms.CheckBox();
            this.txtRemarks = new Gizmox.WebGUI.Forms.TextBox();
            this.lblRemarks = new Gizmox.WebGUI.Forms.Label();
            this.lblPages = new Gizmox.WebGUI.Forms.Label();
            this.txtTotalPages = new Gizmox.WebGUI.Forms.TextBox();
            this.lblTotalPages = new Gizmox.WebGUI.Forms.Label();
            this.gbxHeader = new Gizmox.WebGUI.Forms.GroupBox();
            this.cmdClientInfo = new Gizmox.WebGUI.Forms.Button();
            this.txtClientName = new Gizmox.WebGUI.Forms.TextBox();
            this.cboClient = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblWorkshop = new Gizmox.WebGUI.Forms.Label();
            this.lblPriority = new Gizmox.WebGUI.Forms.Label();
            this.cboWorkshop = new Gizmox.WebGUI.Forms.ComboBox();
            this.cboPriority = new Gizmox.WebGUI.Forms.ComboBox();
            this.lblClient = new Gizmox.WebGUI.Forms.Label();
            this.ofdAttachment = new Gizmox.WebGUI.Forms.OpenFileDialog();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip();
            this.wspPane.SuspendLayout();
            this.gbxDetails.SuspendLayout();
            this.gbxFooter.SuspendLayout();
            this.gbxHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = true;
            this.ansToolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.Size = new System.Drawing.Size(100, 22);
            this.ansToolbar.TabIndex = 0;
            // 
            // wspPane
            // 
            this.wspPane.Controls.Add(this.gbxDetails);
            this.wspPane.Controls.Add(this.gbxFooter);
            this.wspPane.Controls.Add(this.gbxHeader);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.DockPadding.All = 6;
            this.wspPane.Location = new System.Drawing.Point(0, 28);
            this.wspPane.Name = "wspPane";
            this.wspPane.Padding = new Gizmox.WebGUI.Forms.Padding(6);
            this.wspPane.Size = new System.Drawing.Size(493, 506);
            this.wspPane.TabIndex = 1;
            // 
            // gbxDetails
            // 
            this.gbxDetails.Controls.Add(this.chkVpsProofed);
            this.gbxDetails.Controls.Add(this.lvwVpsList);
            this.gbxDetails.Controls.Add(this.chkProofingWith);
            this.gbxDetails.Controls.Add(this.chkCip3);
            this.gbxDetails.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.gbxDetails.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxDetails.Location = new System.Drawing.Point(6, 83);
            this.gbxDetails.Name = "gbxDetails";
            this.gbxDetails.Size = new System.Drawing.Size(481, 267);
            this.gbxDetails.TabIndex = 2;
            this.gbxDetails.TabStop = false;
            // 
            // chkVpsProofed
            // 
            this.chkVpsProofed.Location = new System.Drawing.Point(336, 258);
            this.chkVpsProofed.Name = "chkVpsProofed";
            this.chkVpsProofed.Size = new System.Drawing.Size(102, 21);
            this.chkVpsProofed.TabIndex = 28;
            this.chkVpsProofed.Text = "VPS Proofed";
            this.chkVpsProofed.CheckedChanged += new System.EventHandler(this.chkVpsProofed_CheckedChanged);
            // 
            // lvwVpsList
            // 
            this.lvwVpsList.CheckBoxes = true;
            this.lvwVpsList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colVpsPrintQueueId,
            this.colLineNumber,
            this.colVpsFileName,
            this.colPlateSize,
            this.colCreatedOn,
            this.colPrintedOn});
            this.lvwVpsList.DataMember = null;
            this.lvwVpsList.Location = new System.Drawing.Point(7, 17);
            this.lvwVpsList.Name = "lvwVpsList";
            this.lvwVpsList.Size = new System.Drawing.Size(456, 240);
            this.lvwVpsList.TabIndex = 29;
            // 
            // colVpsPrintQueueId
            // 
            this.colVpsPrintQueueId.Text = "";
            this.colVpsPrintQueueId.Width = 150;
            // 
            // colLineNumber
            // 
            this.colLineNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLineNumber.Text = "#";
            this.colLineNumber.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colLineNumber.Width = 20;
            // 
            // colVpsFileName
            // 
            this.colVpsFileName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colVpsFileName.Text = "VPS File Name";
            this.colVpsFileName.Width = 220;
            // 
            // colPlateSize
            // 
            this.colPlateSize.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPlateSize.Text = "Plate Size";
            this.colPlateSize.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colPlateSize.Width = 60;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colCreatedOn.Text = "Created On";
            this.colCreatedOn.Width = 110;
            // 
            // colPrintedOn
            // 
            this.colPrintedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colPrintedOn.Text = "Printed On";
            this.colPrintedOn.Width = 110;
            // 
            // chkProofingWith
            // 
            this.chkProofingWith.Location = new System.Drawing.Point(204, 258);
            this.chkProofingWith.Name = "chkProofingWith";
            this.chkProofingWith.Size = new System.Drawing.Size(102, 21);
            this.chkProofingWith.TabIndex = 28;
            this.chkProofingWith.Text = "Proofing With:";
            this.chkProofingWith.Click += new System.EventHandler(this.chkProofingWith_Click);
            // 
            // chkCip3
            // 
            this.chkCip3.Location = new System.Drawing.Point(109, 258);
            this.chkCip3.Name = "chkCip3";
            this.chkCip3.Size = new System.Drawing.Size(91, 21);
            this.chkCip3.TabIndex = 25;
            this.chkCip3.Text = "CIP3";
            // 
            // gbxFooter
            // 
            this.gbxFooter.Controls.Add(this.cboDeliveryAddress);
            this.gbxFooter.Controls.Add(this.cmdNewAddress);
            this.gbxFooter.Controls.Add(this.chkDeliverTo);
            this.gbxFooter.Controls.Add(this.chkPickUp);
            this.gbxFooter.Controls.Add(this.txtRemarks);
            this.gbxFooter.Controls.Add(this.lblRemarks);
            this.gbxFooter.Controls.Add(this.lblPages);
            this.gbxFooter.Controls.Add(this.txtTotalPages);
            this.gbxFooter.Controls.Add(this.lblTotalPages);
            this.gbxFooter.Dock = Gizmox.WebGUI.Forms.DockStyle.Bottom;
            this.gbxFooter.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxFooter.Location = new System.Drawing.Point(6, 364);
            this.gbxFooter.Name = "gbxFooter";
            this.gbxFooter.Size = new System.Drawing.Size(481, 136);
            this.gbxFooter.TabIndex = 3;
            this.gbxFooter.TabStop = false;
            // 
            // cboDeliveryAddress
            // 
            this.cboDeliveryAddress.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboDeliveryAddress.Location = new System.Drawing.Point(204, 105);
            this.cboDeliveryAddress.Name = "cboDeliveryAddress";
            this.cboDeliveryAddress.Size = new System.Drawing.Size(232, 21);
            this.cboDeliveryAddress.TabIndex = 13;
            // 
            // cmdNewAddress
            // 
            this.cmdNewAddress.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdNewAddress.Image"));
            this.cmdNewAddress.Location = new System.Drawing.Point(439, 104);
            this.cmdNewAddress.Name = "cmdNewAddress";
            this.cmdNewAddress.Size = new System.Drawing.Size(24, 24);
            this.cmdNewAddress.TabIndex = 14;
            this.cmdNewAddress.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            // 
            // chkDeliverTo
            // 
            this.chkDeliverTo.Location = new System.Drawing.Point(109, 105);
            this.chkDeliverTo.Name = "chkDeliverTo";
            this.chkDeliverTo.Size = new System.Drawing.Size(91, 24);
            this.chkDeliverTo.TabIndex = 12;
            this.chkDeliverTo.Text = "Deliver To:";
            this.chkDeliverTo.Click += new System.EventHandler(this.chkDeliverTo_Click);
            // 
            // chkPickUp
            // 
            this.chkPickUp.Location = new System.Drawing.Point(7, 105);
            this.chkPickUp.Name = "chkPickUp";
            this.chkPickUp.Size = new System.Drawing.Size(87, 24);
            this.chkPickUp.TabIndex = 11;
            this.chkPickUp.Text = "Pick Up";
            this.chkPickUp.Click += new System.EventHandler(this.chkPickUp_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(109, 39);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(327, 40);
            this.txtRemarks.TabIndex = 7;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(7, 39);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(87, 21);
            this.lblRemarks.TabIndex = 6;
            this.lblRemarks.Text = "Remarks:";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPages
            // 
            this.lblPages.Location = new System.Drawing.Point(215, 16);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(36, 21);
            this.lblPages.TabIndex = 2;
            this.lblPages.Text = "Pages";
            this.lblPages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalPages
            // 
            this.txtTotalPages.Location = new System.Drawing.Point(109, 16);
            this.txtTotalPages.Name = "txtTotalPages";
            this.txtTotalPages.Size = new System.Drawing.Size(100, 20);
            this.txtTotalPages.TabIndex = 1;
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.Location = new System.Drawing.Point(7, 16);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(100, 21);
            this.lblTotalPages.TabIndex = 0;
            this.lblTotalPages.Text = "Total Pages:";
            this.lblTotalPages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbxHeader
            // 
            this.gbxHeader.Controls.Add(this.cmdClientInfo);
            this.gbxHeader.Controls.Add(this.txtClientName);
            this.gbxHeader.Controls.Add(this.cboClient);
            this.gbxHeader.Controls.Add(this.lblWorkshop);
            this.gbxHeader.Controls.Add(this.lblPriority);
            this.gbxHeader.Controls.Add(this.cboWorkshop);
            this.gbxHeader.Controls.Add(this.cboPriority);
            this.gbxHeader.Controls.Add(this.lblClient);
            this.gbxHeader.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.gbxHeader.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxHeader.Location = new System.Drawing.Point(6, 6);
            this.gbxHeader.Name = "gbxHeader";
            this.gbxHeader.Size = new System.Drawing.Size(481, 77);
            this.gbxHeader.TabIndex = 1;
            this.gbxHeader.TabStop = false;
            this.gbxHeader.Text = "Order ID";
            // 
            // cmdClientInfo
            // 
            this.cmdClientInfo.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(resources.GetString("cmdClientInfo.Image"));
            this.cmdClientInfo.Location = new System.Drawing.Point(439, 19);
            this.cmdClientInfo.Name = "cmdClientInfo";
            this.cmdClientInfo.Size = new System.Drawing.Size(24, 24);
            this.cmdClientInfo.TabIndex = 2;
            this.cmdClientInfo.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdClientInfo.Click += new System.EventHandler(this.cmdClientInfo_Click);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(109, 20);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(327, 20);
            this.txtClientName.TabIndex = 1;
            this.txtClientName.Visible = false;
            // 
            // cboClient
            // 
            this.cboClient.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboClient.Location = new System.Drawing.Point(109, 20);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(327, 21);
            this.cboClient.TabIndex = 1;
            this.cboClient.Visible = false;
            this.cboClient.SelectedIndexChanged += new System.EventHandler(this.cboClient_SelectedIndexChanged);
            // 
            // lblWorkshop
            // 
            this.lblWorkshop.Location = new System.Drawing.Point(263, 44);
            this.lblWorkshop.Name = "lblWorkshop";
            this.lblWorkshop.Size = new System.Drawing.Size(64, 21);
            this.lblWorkshop.TabIndex = 9;
            this.lblWorkshop.Text = "Workshop:";
            this.lblWorkshop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPriority
            // 
            this.lblPriority.Location = new System.Drawing.Point(36, 44);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(64, 21);
            this.lblPriority.TabIndex = 8;
            this.lblPriority.Text = "Priority:";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboWorkshop
            // 
            this.cboWorkshop.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboWorkshop.Location = new System.Drawing.Point(336, 44);
            this.cboWorkshop.Name = "cboWorkshop";
            this.cboWorkshop.Size = new System.Drawing.Size(100, 21);
            this.cboWorkshop.TabIndex = 7;
            // 
            // cboPriority
            // 
            this.cboPriority.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cboPriority.Location = new System.Drawing.Point(109, 44);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(100, 21);
            this.cboPriority.TabIndex = 6;
            // 
            // lblClient
            // 
            this.lblClient.Location = new System.Drawing.Point(7, 20);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(87, 21);
            this.lblClient.TabIndex = 0;
            this.lblClient.Text = "Client:";
            this.lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ofdAttachment
            // 
            this.ofdAttachment.Theme = "";
            this.ofdAttachment.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdAttachment_FileOk);
            // 
            // Vps5
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.ansToolbar);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(493, 534);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterParent;
            this.Text = "Job Order > VPS";
            this.wspPane.ResumeLayout(false);
            this.gbxDetails.ResumeLayout(false);
            this.gbxFooter.ResumeLayout(false);
            this.gbxHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.GroupBox gbxHeader;
        private Gizmox.WebGUI.Forms.GroupBox gbxDetails;
        private Gizmox.WebGUI.Forms.GroupBox gbxFooter;
        private Gizmox.WebGUI.Forms.Label lblClient;
        private Gizmox.WebGUI.Forms.Label lblPriority;
        private Gizmox.WebGUI.Forms.ComboBox cboWorkshop;
        private Gizmox.WebGUI.Forms.ComboBox cboPriority;
        private Gizmox.WebGUI.Forms.Label lblWorkshop;
        private Gizmox.WebGUI.Forms.CheckBox chkProofingWith;
        private Gizmox.WebGUI.Forms.CheckBox chkCip3;
        private Gizmox.WebGUI.Forms.Label lblTotalPages;
        private Gizmox.WebGUI.Forms.Label lblRemarks;
        private Gizmox.WebGUI.Forms.Label lblPages;
        private Gizmox.WebGUI.Forms.TextBox txtTotalPages;
        private Gizmox.WebGUI.Forms.TextBox txtRemarks;
        private Gizmox.WebGUI.Forms.OpenFileDialog ofdAttachment;
        private Gizmox.WebGUI.Forms.ComboBox cboDeliveryAddress;
        private Gizmox.WebGUI.Forms.Button cmdNewAddress;
        private Gizmox.WebGUI.Forms.CheckBox chkDeliverTo;
        private Gizmox.WebGUI.Forms.CheckBox chkPickUp;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.ComboBox cboClient;
        private Gizmox.WebGUI.Forms.TextBox txtClientName;
        private Gizmox.WebGUI.Forms.Button cmdClientInfo;
        private Gizmox.WebGUI.Forms.ListView lvwVpsList;
        private Gizmox.WebGUI.Forms.ColumnHeader colVpsFileName;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colPrintedOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colVpsPrintQueueId;
        private Gizmox.WebGUI.Forms.ColumnHeader colLineNumber;
        private Gizmox.WebGUI.Forms.CheckBox chkVpsProofed;
        private Gizmox.WebGUI.Forms.ColumnHeader colPlateSize;
    }
}