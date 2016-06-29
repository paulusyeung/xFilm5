#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;
#endregion

namespace xFilm5.JobOrder
{
    public partial class LogFile : Form
    {
        private int _OrderId;

        public LogFile()
        {
            InitializeComponent();
        }

        public int OrderId
        {
            set
            {
                _OrderId = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetCaptions();
            ShowLogFile();
        }

        private void SetCaptions()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            colUpdateOn.Text = oDict.GetWord("updated_on");
            colStatus.Text = oDict.GetWord("workflow");
            colUserName.Text = oDict.GetWord("updated_by");
        }

        private void ShowLogFile()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            string where = String.Format("OrderID = {0}", _OrderId.ToString());
            string[] orderby = { "DateUpdated" };

            gbxLogFile.Text = String.Format(oDict.GetWordWithColon("order_id") + " {0}", _OrderId.ToString());

            Order_JournalCollection logs = Order_Journal.LoadCollection(where, orderby, true);
            if (logs.Count > 0)
            {
                foreach (Order_Journal log in logs)
                {
                    T_Workflow status = T_Workflow.Load(log.Status);
                    Client_User user = Client_User.Load(log.UserID);

                    ListViewItem item = this.lvwLogFile.Items.Add(log.DateUpdated.ToString("yyyy-MM-dd HH:mm"));
                    switch (log.Status)
                    {
                        case (int)Common.Enums.Workflow.Queuing:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Queuing.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.Retouch:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Retouch.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.Printing:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Printing.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.ProofingOutgoing:
                        case (int)Common.Enums.Workflow.ProofingIncoming:
                            item.SubItems.Add(oDict.GetWord("proofing"));
                            break;
                        case (int)Common.Enums.Workflow.Ready:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Ready.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.Dispatch:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Dispatch.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.Completed:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Completed.ToString()));
                            break;
                        case (int)Common.Enums.Workflow.Cancelled:
                            item.SubItems.Add(oDict.GetWord(Common.Enums.Workflow.Cancelled.ToString()));
                            break;
                    }
                    item.SubItems.Add(user.FullName);
                }
            }
        }
    }
}