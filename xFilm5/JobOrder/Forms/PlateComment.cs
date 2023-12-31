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

namespace xFilm5.JobOrder.Forms
{
    public partial class PlateComment : Form
    {
        private Common.Enums.EditMode _EditMode = Common.Enums.EditMode.Read;
        private int _OrderId = 0;
        private int _CommentId = 0;

        public PlateComment()
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

        public string Value
        {
            get
            {
                return this.txtComment.Text;
            }
        }

        public bool IsCompleted = false;
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            SetAnsToolbar();
            SetAttributes();
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
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", System.Web.HttpUtility.UrlDecode(oDict.GetWord("save_close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", oDict.GetWord("delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            if (_EditMode != Common.Enums.EditMode.Read)
            {
//                this.ansToolbar.Buttons.Add(cmdSave);
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
            txtComment.Text = @"
書版
　　紙度尺寸
　　色楷

膠裝/騎馬釘
　　頁數
　　封面/內文
　　顏色數目
　　成品尺寸
　　隻碼
　　內缩

大版行列
　　列(水平)/行(垂直)
　　反台方向
　　　　牙口反/左右反

書版組合
　　四邊出血

";
        }
        #endregion

        private void SaveComment()
        {
            IsCompleted = true;

            this.Close();
        }

        private void ansToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save & close":
                        SaveComment();
                        break;
                }
            }
        }
    }
}