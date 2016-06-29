namespace xFilm5.Customer.Address
{
    partial class AddressList
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle6 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.lksPane = new Gizmox.WebGUI.Forms.Panel();
            this.cmdLookup = new Gizmox.WebGUI.Forms.Button();
            this.txtLookup = new Gizmox.WebGUI.Forms.TextBox();
            this.lblLookup = new Gizmox.WebGUI.Forms.Label();
            this.lvwClientList = new Gizmox.WebGUI.Forms.ListView();
            this.colAddressName = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colAddress = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colTel = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colFax = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colAddressId = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.pnlShortcut = new Gizmox.WebGUI.Forms.Panel();
            this.cmdAll = new Gizmox.WebGUI.Forms.Button();
            this.cmdX = new Gizmox.WebGUI.Forms.Button();
            this.cmdY = new Gizmox.WebGUI.Forms.Button();
            this.cmdZ = new Gizmox.WebGUI.Forms.Button();
            this.cmdL = new Gizmox.WebGUI.Forms.Button();
            this.cmd9 = new Gizmox.WebGUI.Forms.Button();
            this.cmdM = new Gizmox.WebGUI.Forms.Button();
            this.cmdN = new Gizmox.WebGUI.Forms.Button();
            this.cmdQ = new Gizmox.WebGUI.Forms.Button();
            this.cmdP = new Gizmox.WebGUI.Forms.Button();
            this.cmdO = new Gizmox.WebGUI.Forms.Button();
            this.cmdU = new Gizmox.WebGUI.Forms.Button();
            this.cmdV = new Gizmox.WebGUI.Forms.Button();
            this.cmdW = new Gizmox.WebGUI.Forms.Button();
            this.cmdT = new Gizmox.WebGUI.Forms.Button();
            this.cmdS = new Gizmox.WebGUI.Forms.Button();
            this.cmdR = new Gizmox.WebGUI.Forms.Button();
            this.cmdF = new Gizmox.WebGUI.Forms.Button();
            this.cmdG = new Gizmox.WebGUI.Forms.Button();
            this.cmdH = new Gizmox.WebGUI.Forms.Button();
            this.cmdK = new Gizmox.WebGUI.Forms.Button();
            this.cmdJ = new Gizmox.WebGUI.Forms.Button();
            this.cmdI = new Gizmox.WebGUI.Forms.Button();
            this.cmdC = new Gizmox.WebGUI.Forms.Button();
            this.cmdD = new Gizmox.WebGUI.Forms.Button();
            this.cmdE = new Gizmox.WebGUI.Forms.Button();
            this.cmdB = new Gizmox.WebGUI.Forms.Button();
            this.cmdA = new Gizmox.WebGUI.Forms.Button();
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.colContact = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colMobile = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colCreatedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lksPane
            // 
            this.lksPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lksPane.Controls.Add(this.cmdLookup);
            this.lksPane.Controls.Add(this.txtLookup);
            this.lksPane.Controls.Add(this.lblLookup);
            this.lksPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.lksPane.Location = new System.Drawing.Point(0, 0);
            this.lksPane.Name = "lksPane";
            this.lksPane.Size = new System.Drawing.Size(732, 34);
            this.lksPane.TabIndex = 0;
            // 
            // cmdLookup
            // 
            iconResourceHandle6.File = "16x16.16_find.gif";
            this.cmdLookup.Image = iconResourceHandle6;
            this.cmdLookup.Location = new System.Drawing.Point(183, 6);
            this.cmdLookup.Name = "cmdLookup";
            this.cmdLookup.Size = new System.Drawing.Size(22, 22);
            this.cmdLookup.TabIndex = 4;
            this.cmdLookup.TabStop = false;
            this.cmdLookup.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdLookup.Click += new System.EventHandler(this.cmdLookup_Click);
            // 
            // txtLookup
            // 
            this.txtLookup.Location = new System.Drawing.Point(63, 7);
            this.txtLookup.Name = "txtLookup";
            this.txtLookup.Size = new System.Drawing.Size(120, 20);
            this.txtLookup.TabIndex = 2;
            this.txtLookup.EnterKeyDown += new Gizmox.WebGUI.Forms.KeyEventHandler(this.txtLookup_EnterKeyDown);
            // 
            // lblLookup
            // 
            this.lblLookup.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblLookup.Location = new System.Drawing.Point(2, 9);
            this.lblLookup.Name = "lblLookup";
            this.lblLookup.Size = new System.Drawing.Size(61, 18);
            this.lblLookup.TabIndex = 0;
            this.lblLookup.Text = "Look for:";
            // 
            // lvwClientList
            // 
            this.lvwClientList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwClientList.AutoColumnGeneration = false;
            this.lvwClientList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colAddressName,
            this.colAddressId,
            this.colLN,
            this.colAddress,
            this.colTel,
            this.colFax,
            this.colContact,
            this.colMobile,
            this.colCreatedOn});
            this.lvwClientList.DataMember = null;
            this.lvwClientList.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwClientList.GridLines = true;
            this.lvwClientList.ItemsPerPage = 500;
            this.lvwClientList.Location = new System.Drawing.Point(0, 34);
            this.lvwClientList.Name = "lvwClientList";
            this.lvwClientList.Size = new System.Drawing.Size(732, 472);
            this.lvwClientList.TabIndex = 3;
            this.lvwClientList.DoubleClick += new System.EventHandler(this.lvwClientList_DoubleClick);
            // 
            // colAddressName
            // 
            this.colAddressName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colAddressName.Image = null;
            this.colAddressName.Text = "Name";
            this.colAddressName.Width = 130;
            // 
            // colLN
            // 
            this.colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colLN.Image = null;
            this.colLN.Text = "#";
            this.colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            this.colLN.Width = 30;
            // 
            // colAddress
            // 
            this.colAddress.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colAddress.Image = null;
            this.colAddress.Text = "Address";
            this.colAddress.Width = 200;
            // 
            // colTel
            // 
            this.colTel.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colTel.Image = null;
            this.colTel.Text = "Tel";
            this.colTel.Width = 80;
            // 
            // colFax
            // 
            this.colFax.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colFax.Image = null;
            this.colFax.Text = "Fax";
            this.colFax.Width = 80;
            // 
            // colAddressId
            // 
            this.colAddressId.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colAddressId.Image = null;
            this.colAddressId.Text = "Item ID";
            this.colAddressId.Visible = false;
            this.colAddressId.Width = 100;
            // 
            // pnlShortcut
            // 
            this.pnlShortcut.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.pnlShortcut.Controls.Add(this.cmdAll);
            this.pnlShortcut.Controls.Add(this.cmdX);
            this.pnlShortcut.Controls.Add(this.cmdY);
            this.pnlShortcut.Controls.Add(this.cmdZ);
            this.pnlShortcut.Controls.Add(this.cmdL);
            this.pnlShortcut.Controls.Add(this.cmd9);
            this.pnlShortcut.Controls.Add(this.cmdM);
            this.pnlShortcut.Controls.Add(this.cmdN);
            this.pnlShortcut.Controls.Add(this.cmdQ);
            this.pnlShortcut.Controls.Add(this.cmdP);
            this.pnlShortcut.Controls.Add(this.cmdO);
            this.pnlShortcut.Controls.Add(this.cmdU);
            this.pnlShortcut.Controls.Add(this.cmdV);
            this.pnlShortcut.Controls.Add(this.cmdW);
            this.pnlShortcut.Controls.Add(this.cmdT);
            this.pnlShortcut.Controls.Add(this.cmdS);
            this.pnlShortcut.Controls.Add(this.cmdR);
            this.pnlShortcut.Controls.Add(this.cmdF);
            this.pnlShortcut.Controls.Add(this.cmdG);
            this.pnlShortcut.Controls.Add(this.cmdH);
            this.pnlShortcut.Controls.Add(this.cmdK);
            this.pnlShortcut.Controls.Add(this.cmdJ);
            this.pnlShortcut.Controls.Add(this.cmdI);
            this.pnlShortcut.Controls.Add(this.cmdC);
            this.pnlShortcut.Controls.Add(this.cmdD);
            this.pnlShortcut.Controls.Add(this.cmdE);
            this.pnlShortcut.Controls.Add(this.cmdB);
            this.pnlShortcut.Controls.Add(this.cmdA);
            this.pnlShortcut.Dock = Gizmox.WebGUI.Forms.DockStyle.Bottom;
            this.pnlShortcut.Location = new System.Drawing.Point(0, 482);
            this.pnlShortcut.Name = "pnlShortcut";
            this.pnlShortcut.Size = new System.Drawing.Size(732, 24);
            this.pnlShortcut.TabIndex = 4;
            // 
            // cmdAll
            // 
            this.cmdAll.Location = new System.Drawing.Point(540, 4);
            this.cmdAll.Name = "cmdAll";
            this.cmdAll.Size = new System.Drawing.Size(30, 20);
            this.cmdAll.TabIndex = 1;
            this.cmdAll.Tag = "All";
            this.cmdAll.Text = "All";
            this.cmdAll.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdX
            // 
            this.cmdX.Location = new System.Drawing.Point(480, 4);
            this.cmdX.Name = "cmdX";
            this.cmdX.Size = new System.Drawing.Size(20, 20);
            this.cmdX.TabIndex = 0;
            this.cmdX.Tag = "X";
            this.cmdX.Text = "X";
            this.cmdX.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdY
            // 
            this.cmdY.Location = new System.Drawing.Point(500, 4);
            this.cmdY.Name = "cmdY";
            this.cmdY.Size = new System.Drawing.Size(20, 20);
            this.cmdY.TabIndex = 1;
            this.cmdY.Tag = "Y";
            this.cmdY.Text = "Y";
            this.cmdY.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdZ
            // 
            this.cmdZ.Location = new System.Drawing.Point(520, 4);
            this.cmdZ.Name = "cmdZ";
            this.cmdZ.Size = new System.Drawing.Size(20, 20);
            this.cmdZ.TabIndex = 1;
            this.cmdZ.Tag = "Z";
            this.cmdZ.Text = "Z";
            this.cmdZ.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdL
            // 
            this.cmdL.Location = new System.Drawing.Point(240, 4);
            this.cmdL.Name = "cmdL";
            this.cmdL.Size = new System.Drawing.Size(20, 20);
            this.cmdL.TabIndex = 0;
            this.cmdL.Tag = "L";
            this.cmdL.Text = "L";
            this.cmdL.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmd9
            // 
            this.cmd9.Location = new System.Drawing.Point(0, 4);
            this.cmd9.Name = "cmd9";
            this.cmd9.Size = new System.Drawing.Size(20, 20);
            this.cmd9.TabIndex = 0;
            this.cmd9.Tag = "9";
            this.cmd9.Text = "#";
            this.cmd9.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdM
            // 
            this.cmdM.Location = new System.Drawing.Point(260, 4);
            this.cmdM.Name = "cmdM";
            this.cmdM.Size = new System.Drawing.Size(20, 20);
            this.cmdM.TabIndex = 1;
            this.cmdM.Tag = "M";
            this.cmdM.Text = "M";
            this.cmdM.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdN
            // 
            this.cmdN.Location = new System.Drawing.Point(280, 4);
            this.cmdN.Name = "cmdN";
            this.cmdN.Size = new System.Drawing.Size(20, 20);
            this.cmdN.TabIndex = 1;
            this.cmdN.Tag = "N";
            this.cmdN.Text = "N";
            this.cmdN.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdQ
            // 
            this.cmdQ.Location = new System.Drawing.Point(340, 4);
            this.cmdQ.Name = "cmdQ";
            this.cmdQ.Size = new System.Drawing.Size(20, 20);
            this.cmdQ.TabIndex = 1;
            this.cmdQ.Tag = "Q";
            this.cmdQ.Text = "Q";
            this.cmdQ.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdP
            // 
            this.cmdP.Location = new System.Drawing.Point(320, 4);
            this.cmdP.Name = "cmdP";
            this.cmdP.Size = new System.Drawing.Size(20, 20);
            this.cmdP.TabIndex = 1;
            this.cmdP.Tag = "P";
            this.cmdP.Text = "P";
            this.cmdP.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdO
            // 
            this.cmdO.Location = new System.Drawing.Point(300, 4);
            this.cmdO.Name = "cmdO";
            this.cmdO.Size = new System.Drawing.Size(20, 20);
            this.cmdO.TabIndex = 0;
            this.cmdO.Tag = "O";
            this.cmdO.Text = "O";
            this.cmdO.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdU
            // 
            this.cmdU.Location = new System.Drawing.Point(420, 4);
            this.cmdU.Name = "cmdU";
            this.cmdU.Size = new System.Drawing.Size(20, 20);
            this.cmdU.TabIndex = 0;
            this.cmdU.Tag = "U";
            this.cmdU.Text = "U";
            this.cmdU.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdV
            // 
            this.cmdV.Location = new System.Drawing.Point(440, 4);
            this.cmdV.Name = "cmdV";
            this.cmdV.Size = new System.Drawing.Size(20, 20);
            this.cmdV.TabIndex = 1;
            this.cmdV.Tag = "V";
            this.cmdV.Text = "V";
            this.cmdV.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdW
            // 
            this.cmdW.Location = new System.Drawing.Point(460, 4);
            this.cmdW.Name = "cmdW";
            this.cmdW.Size = new System.Drawing.Size(20, 20);
            this.cmdW.TabIndex = 1;
            this.cmdW.Tag = "W";
            this.cmdW.Text = "W";
            this.cmdW.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdT
            // 
            this.cmdT.Location = new System.Drawing.Point(400, 4);
            this.cmdT.Name = "cmdT";
            this.cmdT.Size = new System.Drawing.Size(20, 20);
            this.cmdT.TabIndex = 1;
            this.cmdT.Tag = "T";
            this.cmdT.Text = "T";
            this.cmdT.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdS
            // 
            this.cmdS.Location = new System.Drawing.Point(380, 4);
            this.cmdS.Name = "cmdS";
            this.cmdS.Size = new System.Drawing.Size(20, 20);
            this.cmdS.TabIndex = 1;
            this.cmdS.Tag = "S";
            this.cmdS.Text = "S";
            this.cmdS.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdR
            // 
            this.cmdR.Location = new System.Drawing.Point(360, 4);
            this.cmdR.Name = "cmdR";
            this.cmdR.Size = new System.Drawing.Size(20, 20);
            this.cmdR.TabIndex = 0;
            this.cmdR.Tag = "R";
            this.cmdR.Text = "R";
            this.cmdR.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdF
            // 
            this.cmdF.Location = new System.Drawing.Point(120, 4);
            this.cmdF.Name = "cmdF";
            this.cmdF.Size = new System.Drawing.Size(20, 20);
            this.cmdF.TabIndex = 0;
            this.cmdF.Tag = "F";
            this.cmdF.Text = "F";
            this.cmdF.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdG
            // 
            this.cmdG.Location = new System.Drawing.Point(140, 4);
            this.cmdG.Name = "cmdG";
            this.cmdG.Size = new System.Drawing.Size(20, 20);
            this.cmdG.TabIndex = 1;
            this.cmdG.Tag = "G";
            this.cmdG.Text = "G";
            this.cmdG.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdH
            // 
            this.cmdH.Location = new System.Drawing.Point(160, 4);
            this.cmdH.Name = "cmdH";
            this.cmdH.Size = new System.Drawing.Size(20, 20);
            this.cmdH.TabIndex = 1;
            this.cmdH.Tag = "H";
            this.cmdH.Text = "H";
            this.cmdH.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdK
            // 
            this.cmdK.Location = new System.Drawing.Point(220, 4);
            this.cmdK.Name = "cmdK";
            this.cmdK.Size = new System.Drawing.Size(20, 20);
            this.cmdK.TabIndex = 1;
            this.cmdK.Tag = "K";
            this.cmdK.Text = "K";
            this.cmdK.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdJ
            // 
            this.cmdJ.Location = new System.Drawing.Point(200, 4);
            this.cmdJ.Name = "cmdJ";
            this.cmdJ.Size = new System.Drawing.Size(20, 20);
            this.cmdJ.TabIndex = 1;
            this.cmdJ.Tag = "J";
            this.cmdJ.Text = "J";
            this.cmdJ.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdI
            // 
            this.cmdI.Location = new System.Drawing.Point(180, 4);
            this.cmdI.Name = "cmdI";
            this.cmdI.Size = new System.Drawing.Size(20, 20);
            this.cmdI.TabIndex = 0;
            this.cmdI.Tag = "I";
            this.cmdI.Text = "I";
            this.cmdI.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdC
            // 
            this.cmdC.Location = new System.Drawing.Point(60, 4);
            this.cmdC.Name = "cmdC";
            this.cmdC.Size = new System.Drawing.Size(20, 20);
            this.cmdC.TabIndex = 0;
            this.cmdC.Tag = "C";
            this.cmdC.Text = "C";
            this.cmdC.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdD
            // 
            this.cmdD.Location = new System.Drawing.Point(80, 4);
            this.cmdD.Name = "cmdD";
            this.cmdD.Size = new System.Drawing.Size(20, 20);
            this.cmdD.TabIndex = 1;
            this.cmdD.Tag = "D";
            this.cmdD.Text = "D";
            this.cmdD.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdE
            // 
            this.cmdE.Location = new System.Drawing.Point(100, 4);
            this.cmdE.Name = "cmdE";
            this.cmdE.Size = new System.Drawing.Size(20, 20);
            this.cmdE.TabIndex = 1;
            this.cmdE.Tag = "E";
            this.cmdE.Text = "E";
            this.cmdE.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdB
            // 
            this.cmdB.Location = new System.Drawing.Point(40, 4);
            this.cmdB.Name = "cmdB";
            this.cmdB.Size = new System.Drawing.Size(20, 20);
            this.cmdB.TabIndex = 1;
            this.cmdB.Tag = "B";
            this.cmdB.Text = "B";
            this.cmdB.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // cmdA
            // 
            this.cmdA.Location = new System.Drawing.Point(20, 4);
            this.cmdA.Name = "cmdA";
            this.cmdA.Size = new System.Drawing.Size(20, 20);
            this.cmdA.TabIndex = 1;
            this.cmdA.Tag = "A";
            this.cmdA.Text = "A";
            this.cmdA.Click += new System.EventHandler(this.ShortcutButton_Click);
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 34);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 5;
            // 
            // colContact
            // 
            this.colContact.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colContact.Image = null;
            this.colContact.Text = "Contact";
            this.colContact.Width = 100;
            // 
            // colMobile
            // 
            this.colMobile.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colMobile.Image = null;
            this.colMobile.Text = "Mobile";
            this.colMobile.Width = 100;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colCreatedOn.Image = null;
            this.colCreatedOn.Text = "Created On";
            this.colCreatedOn.Width = 100;
            // 
            // AddressList
            // 
            this.Controls.Add(this.ansToolbar);
            this.Controls.Add(this.pnlShortcut);
            this.Controls.Add(this.lvwClientList);
            this.Controls.Add(this.lksPane);
            this.Size = new System.Drawing.Size(732, 506);
            this.Text = "ClientList";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel lksPane;
        private Gizmox.WebGUI.Forms.ListView lvwClientList;
        private Gizmox.WebGUI.Forms.TextBox txtLookup;
        private Gizmox.WebGUI.Forms.Label lblLookup;
        private Gizmox.WebGUI.Forms.Button cmdLookup;
        private Gizmox.WebGUI.Forms.Panel pnlShortcut;
        private Gizmox.WebGUI.Forms.Button cmd9;
        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Button cmdA;
        private Gizmox.WebGUI.Forms.Button cmdB;
        private Gizmox.WebGUI.Forms.Button cmdF;
        private Gizmox.WebGUI.Forms.Button cmdG;
        private Gizmox.WebGUI.Forms.Button cmdH;
        private Gizmox.WebGUI.Forms.Button cmdK;
        private Gizmox.WebGUI.Forms.Button cmdJ;
        private Gizmox.WebGUI.Forms.Button cmdI;
        private Gizmox.WebGUI.Forms.Button cmdC;
        private Gizmox.WebGUI.Forms.Button cmdD;
        private Gizmox.WebGUI.Forms.Button cmdE;
        private Gizmox.WebGUI.Forms.Button cmdL;
        private Gizmox.WebGUI.Forms.Button cmdM;
        private Gizmox.WebGUI.Forms.Button cmdN;
        private Gizmox.WebGUI.Forms.Button cmdQ;
        private Gizmox.WebGUI.Forms.Button cmdP;
        private Gizmox.WebGUI.Forms.Button cmdO;
        private Gizmox.WebGUI.Forms.Button cmdU;
        private Gizmox.WebGUI.Forms.Button cmdV;
        private Gizmox.WebGUI.Forms.Button cmdW;
        private Gizmox.WebGUI.Forms.Button cmdT;
        private Gizmox.WebGUI.Forms.Button cmdS;
        private Gizmox.WebGUI.Forms.Button cmdR;
        private Gizmox.WebGUI.Forms.Button cmdX;
        private Gizmox.WebGUI.Forms.Button cmdY;
        private Gizmox.WebGUI.Forms.Button cmdZ;
        private Gizmox.WebGUI.Forms.ColumnHeader colAddressName;
        private Gizmox.WebGUI.Forms.ColumnHeader colAddress;
        private Gizmox.WebGUI.Forms.ColumnHeader colTel;
        private Gizmox.WebGUI.Forms.ColumnHeader colFax;
        private Gizmox.WebGUI.Forms.ColumnHeader colAddressId;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.Button cmdAll;
        private Gizmox.WebGUI.Forms.ColumnHeader colLN;
        private Gizmox.WebGUI.Forms.ColumnHeader colContact;
        private Gizmox.WebGUI.Forms.ColumnHeader colMobile;
        private Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn;


    }
}