namespace xFilm5.Customer.JobOrder
{
    partial class DropBox
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
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.lvwFileExplorer = new Gizmox.WebGUI.Forms.ListView();
            this.colFileName = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colFileSize = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colFileType = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.colModifiedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            this.ansFileExplorer = new Gizmox.WebGUI.Forms.ToolBar();
            this.toolTip1 = new Gizmox.WebGUI.Forms.ToolTip(this.components);
            this.fileUpload = new Gizmox.WebGUI.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Controls.Add(this.lvwFileExplorer);
            this.wspPane.Controls.Add(this.ansFileExplorer);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.Location = new System.Drawing.Point(0, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(640, 306);
            this.wspPane.TabIndex = 2;
            // 
            // lvwFileExplorer
            // 
            this.lvwFileExplorer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.lvwFileExplorer.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            this.colFileName,
            this.colFileSize,
            this.colFileType,
            this.colModifiedOn});
            this.lvwFileExplorer.DataMember = null;
            this.lvwFileExplorer.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.lvwFileExplorer.ItemsPerPage = 20;
            this.lvwFileExplorer.Location = new System.Drawing.Point(0, 28);
            this.lvwFileExplorer.MultiSelect = false;
            this.lvwFileExplorer.Name = "lvwFileExplorer";
            this.lvwFileExplorer.Size = new System.Drawing.Size(640, 278);
            this.lvwFileExplorer.TabIndex = 1;
            this.lvwFileExplorer.SelectedIndexChanged += new System.EventHandler(this.lvwFileExplorer_SelectedIndexChanged);
            this.lvwFileExplorer.DoubleClick += new System.EventHandler(this.lvwFileExplorer_DoubleClick);
            // 
            // colFileName
            // 
            this.colFileName.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colFileName.Image = null;
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 300;
            // 
            // colFileSize
            // 
            this.colFileSize.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colFileSize.Image = null;
            this.colFileSize.Text = "File Size";
            this.colFileSize.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            this.colFileSize.Width = 80;
            // 
            // colFileType
            // 
            this.colFileType.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colFileType.Image = null;
            this.colFileType.Text = "File Type";
            this.colFileType.Width = 120;
            // 
            // colModifiedOn
            // 
            this.colModifiedOn.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            this.colModifiedOn.Image = null;
            this.colModifiedOn.Text = "Modified On";
            this.colModifiedOn.Width = 100;
            // 
            // ansFileExplorer
            // 
            this.ansFileExplorer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansFileExplorer.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansFileExplorer.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansFileExplorer.DragHandle = true;
            this.ansFileExplorer.DropDownArrows = false;
            this.ansFileExplorer.ImageList = null;
            this.ansFileExplorer.Location = new System.Drawing.Point(0, 0);
            this.ansFileExplorer.MenuHandle = true;
            this.ansFileExplorer.Name = "ansFileExplorer";
            this.ansFileExplorer.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansFileExplorer.ShowToolTips = true;
            this.ansFileExplorer.TabIndex = 0;
            // 
            // fileUpload
            // 
            this.fileUpload.FileOk += new System.ComponentModel.CancelEventHandler(this.fileUpload_FileOk);
            // 
            // DropBox
            // 
            this.Controls.Add(this.wspPane);
            this.Size = new System.Drawing.Size(640, 306);
            this.Text = "DropBoxExplorer";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.ListView lvwFileExplorer;
        private Gizmox.WebGUI.Forms.ToolBar ansFileExplorer;
        private Gizmox.WebGUI.Forms.ColumnHeader colFileName;
        private Gizmox.WebGUI.Forms.ColumnHeader colFileSize;
        private Gizmox.WebGUI.Forms.ColumnHeader colFileType;
        private Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn;
        private Gizmox.WebGUI.Forms.ToolTip toolTip1;
        private Gizmox.WebGUI.Forms.OpenFileDialog fileUpload;


    }
}