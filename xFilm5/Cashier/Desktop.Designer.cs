namespace xFilm5.Cashier
{
    partial class Desktop
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
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle1 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle2 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle3 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            Gizmox.WebGUI.Common.Resources.IconResourceHandle iconResourceHandle4 = new Gizmox.WebGUI.Common.Resources.IconResourceHandle();
            this.navPane = new Gizmox.WebGUI.Forms.Panel();
            this.cmdPower = new Gizmox.WebGUI.Forms.Button();
            this.cmdGas = new Gizmox.WebGUI.Forms.Button();
            this.cmdHome = new Gizmox.WebGUI.Forms.Button();
            this.wspPane = new Gizmox.WebGUI.Forms.Panel();
            this.button1 = new Gizmox.WebGUI.Forms.Button();
            this.SuspendLayout();
            // 
            // navPane
            // 
            this.navPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.navPane.Controls.Add(this.cmdPower);
            this.navPane.Controls.Add(this.cmdGas);
            this.navPane.Controls.Add(this.cmdHome);
            this.navPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Right;
            this.navPane.DockPadding.Bottom = 6;
            this.navPane.DockPadding.Right = 6;
            this.navPane.DockPadding.Top = 6;
            this.navPane.Location = new System.Drawing.Point(712, 0);
            this.navPane.Name = "navPane";
            this.navPane.Size = new System.Drawing.Size(142, 589);
            this.navPane.TabIndex = 0;
            // 
            // cmdPower
            // 
            this.cmdPower.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Bottom | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            iconResourceHandle1.File = "128x128.Power.png";
            this.cmdPower.Image = iconResourceHandle1;
            this.cmdPower.Location = new System.Drawing.Point(0, 446);
            this.cmdPower.Name = "cmdPower";
            this.cmdPower.Size = new System.Drawing.Size(136, 136);
            this.cmdPower.TabIndex = 0;
            this.cmdPower.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdPower.Click += new System.EventHandler(this.cmdPower_Click);
            // 
            // cmdGas
            // 
            this.cmdGas.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            iconResourceHandle2.File = "128x128.Gas.png";
            this.cmdGas.Image = iconResourceHandle2;
            this.cmdGas.Location = new System.Drawing.Point(0, 142);
            this.cmdGas.Name = "cmdGas";
            this.cmdGas.Size = new System.Drawing.Size(136, 136);
            this.cmdGas.TabIndex = 0;
            this.cmdGas.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdGas.Click += new System.EventHandler(this.cmdGas_Click);
            // 
            // cmdHome
            // 
            this.cmdHome.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            iconResourceHandle3.File = "128x128.Home.png";
            this.cmdHome.Image = iconResourceHandle3;
            this.cmdHome.Location = new System.Drawing.Point(0, 6);
            this.cmdHome.Name = "cmdHome";
            this.cmdHome.Size = new System.Drawing.Size(136, 136);
            this.cmdHome.TabIndex = 0;
            this.cmdHome.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            this.cmdHome.Click += new System.EventHandler(this.cmdHome_Click);
            // 
            // wspPane
            // 
            this.wspPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.wspPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.wspPane.DockPadding.All = 6;
            this.wspPane.Location = new System.Drawing.Point(0, 0);
            this.wspPane.Name = "wspPane";
            this.wspPane.Size = new System.Drawing.Size(712, 589);
            this.wspPane.TabIndex = 1;
            // 
            // button1
            // 
            iconResourceHandle4.File = "128x128.Gas.png";
            this.button1.Image = iconResourceHandle4;
            this.button1.Location = new System.Drawing.Point(0, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 136);
            this.button1.TabIndex = 0;
            this.button1.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            // 
            // Desktop
            // 
            this.Controls.Add(this.wspPane);
            this.Controls.Add(this.navPane);
            this.Size = new System.Drawing.Size(854, 589);
            this.Text = "Desktop";
            this.Load += new System.EventHandler(this.Desktop_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel navPane;
        private Gizmox.WebGUI.Forms.Panel wspPane;
        private Gizmox.WebGUI.Forms.Button cmdHome;
        private Gizmox.WebGUI.Forms.Button cmdGas;
        private Gizmox.WebGUI.Forms.Button button1;
        private Gizmox.WebGUI.Forms.Button cmdPower;


    }
}