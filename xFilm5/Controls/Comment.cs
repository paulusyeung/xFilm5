#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;

#endregion

namespace xFilm5.JobOrder
{
    public partial class Comment : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _CommentId = 0;

        public Comment()
        {
            InitializeComponent();
        }

        #region public properties
        public int OrderId
        {
            set
            {
                _OrderId = value;
            }
        }
        public Common.Enums.EditMode EditMode
        {
            set
            {
                _EditMode = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
            ShowComment();
        }

        #region Configure Controls on Form Load
        private void SetCaptions()
        {
        }

        private void SetAnsToolbar()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.ansToolbar.MenuHandle = false;
            this.ansToolbar.DragHandle = false;
            this.ansToolbar.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", oDict.GetWord("save"));
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("Icons.16x16.16_L_save.gif");

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", System.Web.HttpUtility.UrlDecode(oDict.GetWord("save_close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("Icons.16x16.16_saveClose.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("Icons.16x16.16_L_remove.gif");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
                this.ansToolbar.Buttons.Add(cmdSave);
                this.ansToolbar.Buttons.Add(cmdSaveClose);
                if (_EditMode != Common.Enums.EditMode.Add)
                {
                    this.ansToolbar.Buttons.Add(cmdDelete);
                }
            }

            this.ansToolbar.ButtonClick += new ToolBarButtonClickEventHandler(ansToolbar_ButtonClick);
        }

        private void SetAttributes()
        {
        }
        #endregion

        private void ShowComment()
        {
            OrderComment comment = OrderComment.LoadWhere(String.Format("OrderID = {0}", _OrderId.ToString()));
            if (comment != null)
            {
                txtComment.Text = comment.Comment;
                _EditMode = Common.Enums.EditMode.Edit;
                _CommentId = comment.ID;
                this.Text = String.Format("Order ID: {0}", _OrderId.ToString());
            }
            else
            {
                _EditMode = Common.Enums.EditMode.Add;
            }
        }

        private bool SaveComment()
        {
            bool result = false;
            OrderComment comment = null;

            try
            {
                switch ((int)_EditMode)
                {
                    case (int)Common.Enums.EditMode.Add:
                        comment = new OrderComment();
                        break;
                    case (int)Common.Enums.EditMode.Edit:
                        comment = OrderComment.Load(_CommentId);
                        break;
                }
                comment.Comment = txtComment.Text.Trim();
                comment.OrderID = _OrderId;
                comment.Save();

                _CommentId = comment.ID;
                result = true;
            }
            catch { }
            return result;
        }

        private bool DeleteComment()
        {
            bool result = false;

            OrderComment comment = OrderComment.Load(_CommentId);
            if (comment != null)
            {
                try
                {
                    comment.Delete();
                    result = true;
                }
                catch { }
            }

            return result;
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Comment?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSave_Click));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Comment And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdSaveClose_Click));
                        break;
                    case "delete":
                        MessageBox.Show("Delete Comment?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(cmdDelete_Click));
                        break;
                }
            }
        }

        #region ans Button Clicks: Save, SaveClose, Cancel, Retouch, Printing, Proofing, Ready, Dispatch, Completed
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (SaveComment())
                    {
                        MessageBox.Show(String.Format("Comment for Order ID {0} is saved!", _OrderId.ToString()), "Save Result");
                        if (_EditMode == Common.Enums.EditMode.Add)
                        {
                            _EditMode = Common.Enums.EditMode.Edit;
                        }
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
                    if (SaveComment())
                    {
                        MessageBox.Show(String.Format("Comment for Order ID {0} is saved!", _OrderId.ToString()), "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
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

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (DeleteComment())
                    {
                        MessageBox.Show(String.Format("Comment for Order ID {0} is deleted.", _OrderId.ToString()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, new EventHandler(cmdCloseForm));
                    }
                    else
                    {
                        MessageBox.Show("This record is protected...You can not cancel this record!\nPlease review the client status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}