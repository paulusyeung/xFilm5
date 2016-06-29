namespace xFilm5.JobOrder
{
    partial class LogFile
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
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.gbxLogFile = new Gizmox.WebGUI.Forms.GroupBox();
            this.lvwLogFile = new Gizmox.WebGUI.Forms.ListView();
            this.colUpdateOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colStatus = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colUserName = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Controls.Add(this.gbxLogFile);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.DockPadding.All = 6;
            this.wspPane.Location = new System.Drawing.Point(0, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(354, 323);
            this.wspPane.TabIndex = 0;
            // 
            // gbxLogFile
            // 
            this.gbxLogFile.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.gbxLogFile.Controls.Add(this.lvwLogFile);
            this.gbxLogFile.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.gbxLogFile.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbxLogFile.Location = new System.Drawing.Point(6, 6);
            this.gbxLogFile.Name = "gbxLogFile";
            this.gbxLogFile.Size = new System.Drawing.Size(342, 311);
            this.gbxLogFile.TabIndex = 0;
            this.gbxLogFile.Text = "Order ID:";
            // 
            // lvwLogFile
            // 
            this.lvwLogFile.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwLogFile.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colUpdateOn,
            this.colStatus,
            this.colUserName});
            this.lvwLogFile.DataMember = null;
            this.lvwLogFile.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwLogFile.ItemsPerPage = 20;
            this.lvwLogFile.Location = new System.Drawing.Point(3, 16);
            this.lvwLogFile.Name = "lvwLogFile";
            this.lvwLogFile.Size = new System.Drawing.Size(336, 292);
            this.lvwLogFile.TabIndex = 0;
            // 
            // colUpdateOn
            // 
            this.colUpdateOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colUpdateOn.Image = null;
            this.colUpdateOn.Text = "Updated On";
            this.colUpdateOn.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colStatus.Image = null;
            this.colStatus.Text = "Status";
            this.colStatus.Width = 80;
            // 
            // colUserName
            // 
            this.colUserName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colUserName.Image = null;
            this.colUserName.Text = "Updated By";
            this.colUserName.Width = 150;
            // 
            // LogFile
            // 
            this.Controls.Add(this.wspPane);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(354, 323);
            this.Text = "Job Order > Log File";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.GroupBox gbxLogFile;
        private Gizmox.WebGUI.Forms.ListView lvwLogFile;
        private Gizmox.WebGUI.Forms.ColumnHeader colUpdateOn;
        private Gizmox.WebGUI.Forms.ColumnHeader colStatus;
        private Gizmox.WebGUI.Forms.ColumnHeader colUserName;


    }
}