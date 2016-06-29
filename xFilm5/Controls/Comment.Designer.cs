namespace xFilm5.JobOrder
{
    partial class Comment
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
            this.ansToolbar = new Gizmox.WebGUI.Forms.ToolBar();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.txtComment = new Gizmox.WebGUI.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ansToolbar
            // 
            this.ansToolbar.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.ansToolbar.Appearance = Gizmox.WebGUI.Forms.ToolBarAppearance.Normal;
            this.ansToolbar.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.ansToolbar.DragHandle = true;
            this.ansToolbar.DropDownArrows = false;
            this.ansToolbar.ImageList = null;
            this.ansToolbar.Location = new System.Drawing.Point(0, 0);
            this.ansToolbar.MenuHandle = true;
            this.ansToolbar.Name = "ansToolbar";
            this.ansToolbar.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.No;
            this.ansToolbar.ShowToolTips = true;
            this.ansToolbar.TabIndex = 0;
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Controls.Add(this.txtComment);
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.DockPadding.All = 6;
            this.wspPane.Location = new System.Drawing.Point(0, 28);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(480, 332);
            this.wspPane.TabIndex = 1;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.txtComment.BackColor = System.Drawing.Color.LightYellow;
            this.txtComment.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(6, 6);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(468, 320);
            this.txtComment.TabIndex = 0;
            // 
            // Comment
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.ansToolbar);
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(480, 360);
            this.Text = "Job Order > Comment";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.ToolBar ansToolbar;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.TextBox txtComment;


    }
}