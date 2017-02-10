using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

using Gizmox.WebGUI.Common.Interfaces;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

namespace xFilm5.Controls
{
    public class DataGridViewExtension
    {
        public static DataGridViewColumnHeaderCell LoadIcon(String IconFile)
        {
            DataGridViewColumnHeaderCell cell = new DataGridViewColumnHeaderCell();
            LoadIcon(ref cell, IconFile);
            return cell;
        }

        public static void LoadIcon(ref DataGridViewColumnHeaderCell cell, String IconFile)
        {
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Panel.Visible = true;
            cell.Panel.BackColor = Color.Transparent;
            cell.Panel.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.None;

            Button button = new Button();
            button.Enabled = false;
            button.BackColor = Color.Transparent;
            button.Dock = DockStyle.Fill;
            button.FlatStyle = FlatStyle.Flat;
            button.BorderColor = Color.Transparent;
            button.BorderWidth = 0;
            button.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.None;
            button.VisualEffect = new Gizmox.WebGUI.Forms.VisualEffects.EmptyVisualEffect();
            button.Image = new Gizmox.WebGUI.Common.Resources.IconResourceHandle(IconFile);
            button.Size = new Size(button.Image.Width, button.Height + 2);

            cell.Panel.Controls.Add(button);
        }
    }


    #region EllipsisColumn
    /// <summary>
    /// 喺 Companion Kit 搬過嚟用
    /// Ellipsis cell consist of a docked to right button and a docked fill TextBox
    /// The cell's panel is used to display the controls
    /// </summary>
    public class DataGridViewEllipsisCell : DataGridViewTextBoxCell
    {
        Gizmox.WebGUI.Forms.Button objButton = null;
        Gizmox.WebGUI.Forms.TextBox objTextbox = null;

        public event EventHandler Ellipsis;

        public DataGridViewEllipsisCell() : base()
        {
            // Get reference to cell's panel
            DataGridViewCellPanel objPanel = this.Panel;

            // Create the button
            objButton = new Gizmox.WebGUI.Forms.Button();
            objButton.Width = 30;
            objButton.Text = "...";
            objButton.Dock = DockStyle.Right;
            objButton.Click += this.buttonClick;

            // Create the textbox
            objTextbox = new Gizmox.WebGUI.Forms.TextBox();
            objTextbox.Dock = DockStyle.Fill;

            // Wire the TextChanged event to update the underlying cell's value
            objTextbox.TextChanged += this.textChanged;

            // Add the controls
            objPanel.Controls.Add(objTextbox);
            objPanel.Controls.Add(objButton);

            // Activate the panel
            objPanel.Visible = true;
            this.Colspan = 1;
            this.Rowspan = 1;
        }

        /// <summary>
        /// When TextBox's text is update, update to underlying cell's value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textChanged(object sender, EventArgs e)
        {
            this.SetValue(this.RowIndex, objTextbox.Text);
        }

        /// <summary>
        /// When value is set, also set TextBox.Text
        /// </summary>
        /// <param name="intRowIndex"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        protected override bool SetValue(int intRowIndex, object objValue)
        {
            if (objTextbox.Text != (string)objValue)
                objTextbox.Text = (string)objValue;
            return base.SetValue(intRowIndex, objValue);
        }

        /// <summary>
        /// Underlying cell's value always determines the value. Update TextBox.Text if required.
        /// </summary>
        /// <param name="intRowIndex"></param>
        /// <returns></returns>
        protected override object GetValue(int intRowIndex)
        {
            string strValue = (string)base.GetValue(intRowIndex);
            if (objTextbox.Text != strValue)
                objTextbox.Text = strValue;
            return strValue;
        }

        /// <summary>
        /// On Button.Click, raise the Ellipsis event for the cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick(object sender, EventArgs e)
        {
            // sender will be the Cell
            if (this.Ellipsis != null)
                this.Ellipsis(this, EventArgs.Empty);
        }

        /// <summary>
        /// Make sure Panel's controls follow cell's ReadOnly setting when cell is rendered
        /// </summary>
        /// <param name="objWriter"></param>
        protected override void RenderReadOnlyAttribute(IAttributeWriter objWriter)
        {
            if (objTextbox.ReadOnly != base.ReadOnly)
                objTextbox.ReadOnly = base.ReadOnly;
            if (objButton.Enabled != !base.ReadOnly)
                objButton.Enabled = !base.ReadOnly;
            base.RenderReadOnlyAttribute(objWriter);
        }

    }

    public class DataGridViewEllipsisColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewEllipsisColumn() : base()
        {
            // Set a custom template for a cell of DataGridView.
            this.CellTemplate = new DataGridViewEllipsisCell();

            this.HeaderText = "Ellipsis Column";
            this.Name = "ellipsisColumn";
        }

    }
    #endregion

    #region IconTextBoxColumn
    /// <summary>
    /// 2015-12-17 paulus: 搞唔掂啲 visual behaviour，暫時放棄
    /// 喺 Companion Kit 搬 EllipsisColumn 過嚟用，再改做 Icon(PictureBox) + TextBox
    /// consist of a docked to right PictureBox and a docked fill TextBox
    /// The cell's panel is used to display the controls
    /// </summary>
    public class DataGridViewIconTextBoxCell : DataGridViewTextBoxCell
    {
        Gizmox.WebGUI.Forms.PictureBox oPictureBox = null;
        Gizmox.WebGUI.Forms.TextBox oTextbox = null;

        public event EventHandler Ellipsis;
        public ResourceHandle Image
        {
            set { oPictureBox.Image = value; }
            get { return oPictureBox.Image; }
        }

        public DataGridViewIconTextBoxCell()
            : base()
        {
            // Get reference to cell's panel
            DataGridViewCellPanel objPanel = this.Panel;

            // Create the button
            oPictureBox = new Gizmox.WebGUI.Forms.PictureBox();
            oPictureBox.Size = new Size(16, 16);
            oPictureBox.Margin = new Padding(4, 2, 2, 2);
            oPictureBox.BackColor = Color.Transparent;
            oPictureBox.Image = this.Image;
            oPictureBox.Dock = DockStyle.Left;
            oPictureBox.Click += this.buttonClick;

            // Create the textbox
            oTextbox = new Gizmox.WebGUI.Forms.TextBox();
            oTextbox.Dock = DockStyle.Fill;
            oTextbox.BorderStyle = BorderStyle.None;
            oTextbox.BackColor = Color.Transparent;

            // Wire the TextChanged event to update the underlying cell's value
            oTextbox.TextChanged += this.textChanged;

            // Add the controls
            objPanel.Controls.Add(oTextbox);
            objPanel.Controls.Add(oPictureBox);

            // Activate the panel
            objPanel.Visible = true;
            this.Colspan = 1;
            this.Rowspan = 1;
        }

        /// <summary>
        /// When TextBox's text is update, update to underlying cell's value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textChanged(object sender, EventArgs e)
        {
            this.SetValue(this.RowIndex, oTextbox.Text);
        }

        /// <summary>
        /// When value is set, also set TextBox.Text
        /// </summary>
        /// <param name="intRowIndex"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        protected override bool SetValue(int intRowIndex, object objValue)
        {
            if (oTextbox.Text != (string)objValue)
                oTextbox.Text = (string)objValue;
            return base.SetValue(intRowIndex, objValue);
        }

        /// <summary>
        /// Underlying cell's value always determines the value. Update TextBox.Text if required.
        /// </summary>
        /// <param name="intRowIndex"></param>
        /// <returns></returns>
        protected override object GetValue(int intRowIndex)
        {
            string strValue = (string)base.GetValue(intRowIndex);
            if (oTextbox.Text != strValue)
                oTextbox.Text = strValue;
            return strValue;
        }

        /// <summary>
        /// On Button.Click, raise the Ellipsis event for the cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick(object sender, EventArgs e)
        {
            // sender will be the Cell
            if (this.Ellipsis != null)
                this.Ellipsis(this, EventArgs.Empty);
        }

        /// <summary>
        /// Make sure Panel's controls follow cell's ReadOnly setting when cell is rendered
        /// </summary>
        /// <param name="objWriter"></param>
        protected override void RenderReadOnlyAttribute(IAttributeWriter objWriter)
        {
            //唔想啲字 ReadOnly 時候 dim 暗咗
            //if (oTextbox.ReadOnly != base.ReadOnly)
            //    oTextbox.ReadOnly = base.ReadOnly;
            if (oPictureBox.Enabled != !base.ReadOnly)
                oPictureBox.Enabled = !base.ReadOnly;
            base.RenderReadOnlyAttribute(objWriter);
        }

    }

    public class DataGridViewIconTextBoxColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewIconTextBoxColumn()
            : base()
        {
            // Set a custom template for a cell of DataGridView.
            this.CellTemplate = new DataGridViewIconTextBoxCell();

            this.HeaderText = "IconTextBox Column";
            this.Name = "iconTextBoxColumn";
        }

    }
    #endregion
}