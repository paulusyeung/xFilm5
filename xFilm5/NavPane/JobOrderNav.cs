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

namespace xFilm5.NavPane
{
    public partial class JobOrderNav : UserControl
    {
        public JobOrderNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("joborder", this.navJobOrder.Nodes);
        }

        private void navInvt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navJobOrder.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navJobOrder.SelectedNode.Tag);
                ShowAppToolStrip((string)navJobOrder.SelectedNode.Tag);
            }
        }

        #region Show private AppToolStrip
        private void ShowAppToolStrip(string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);
                if (controls.Length > 0)
                {
                    Panel atsPane = (Panel)controls[0];
                    //                atsPane.Controls.Clear();

                    switch (Tag.ToLower())
                    {
                        case "jo_queuing":
                        case "jo_retouch":
                        case "jo_printing":
                        case "jo_proofing":
                        case "jo_ready":
                        case "jo_dispacth":
                        case "jo_completed":
                        case "support_dropbox":
                            xFilm5.AtsPane.JobOrderAts oJoAts = new xFilm5.AtsPane.JobOrderAts();
                            oJoAts.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oJoAts);
                            break;
                        case "sales_prospect":
                            xFilm5.Sales.Prospect.ProspectAts prospectAts = new xFilm5.Sales.Prospect.ProspectAts();
                            prospectAts.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(prospectAts);
                            break;
                        case "sales_client":
                            xFilm5.Sales.Client.ClientAts clientAts = new xFilm5.Sales.Client.ClientAts();
                            clientAts.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(clientAts);
                            break;
                        case "sales_pricelist":
                            xFilm5.Sales.PriceList.PriceListAts  pricelistAts = new xFilm5.Sales.PriceList.PriceListAts();
                            pricelistAts.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(pricelistAts);
                            break;
                    }
                }
            }
        }
        #endregion

        #region Show private Workspace
        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
                    #region Staff
                    case "jo_queuing":
                        xFilm5.JobOrder.JoDefaultWsp oJoQueuing = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoQueuing.DockPadding.All = 6;
                        oJoQueuing.Dock = DockStyle.Fill;
                        oJoQueuing.WorkflowFrom = Common.Enums.Workflow.Queuing;
                        oJoQueuing.WorkflowTo = Common.Enums.Workflow.Queuing;
                        wspPane.Controls.Add(oJoQueuing);
                        break;
                    case "jo_retouch":
                        xFilm5.JobOrder.JoDefaultWsp oJoRetouch = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoRetouch.DockPadding.All = 6;
                        oJoRetouch.Dock = DockStyle.Fill;
                        oJoRetouch.WorkflowFrom = Common.Enums.Workflow.Retouch;
                        oJoRetouch.WorkflowTo = Common.Enums.Workflow.Retouch;
                        wspPane.Controls.Add(oJoRetouch);
                        break;
                    case "jo_printing":
                        xFilm5.JobOrder.JoDefaultWsp oJoPrinting = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoPrinting.DockPadding.All = 6;
                        oJoPrinting.Dock = DockStyle.Fill;
                        oJoPrinting.WorkflowFrom = Common.Enums.Workflow.Printing;
                        oJoPrinting.WorkflowTo = Common.Enums.Workflow.Printing;
                        wspPane.Controls.Add(oJoPrinting);
                        break;
                    case "jo_proofing":
                        xFilm5.JobOrder.JoDefaultWsp oJoProofing = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoProofing.DockPadding.All = 6;
                        oJoProofing.Dock = DockStyle.Fill;
                        oJoProofing.WorkflowFrom = Common.Enums.Workflow.ProofingOutgoing;
                        oJoProofing.WorkflowTo = Common.Enums.Workflow.ProofingIncoming;
                        wspPane.Controls.Add(oJoProofing);
                        break;
                    case "jo_ready":
                        xFilm5.JobOrder.JoDefaultWsp oJoReady = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoReady.DockPadding.All = 6;
                        oJoReady.Dock = DockStyle.Fill;
                        oJoReady.WorkflowFrom = Common.Enums.Workflow.Ready;
                        oJoReady.WorkflowTo = Common.Enums.Workflow.Ready;
                        wspPane.Controls.Add(oJoReady);
                        break;
                    case "jo_dispatch":
                        xFilm5.JobOrder.JoDefaultWsp oJoDispatch = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoDispatch.DockPadding.All = 6;
                        oJoDispatch.Dock = DockStyle.Fill;
                        oJoDispatch.WorkflowFrom = Common.Enums.Workflow.Dispatch;
                        oJoDispatch.WorkflowTo = Common.Enums.Workflow.Dispatch;
                        wspPane.Controls.Add(oJoDispatch);
                        break;
                    case "jo_completed":
                        xFilm5.JobOrder.JoDefaultWsp oJoCompleted = new xFilm5.JobOrder.JoDefaultWsp(controls[0]);
                        oJoCompleted.DockPadding.All = 6;
                        oJoCompleted.Dock = DockStyle.Fill;
                        oJoCompleted.WorkflowFrom = Common.Enums.Workflow.Completed;
                        oJoCompleted.WorkflowTo = Common.Enums.Workflow.Completed;
                        wspPane.Controls.Add(oJoCompleted);
                        break;
                    case "sales_client":
                        xFilm5.Sales.Client.ClientList oClientList = new xFilm5.Sales.Client.ClientList();
                        oClientList.DockPadding.All = 6;
                        oClientList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClientList);
                        break;
                    case "sales_prospect":
                        xFilm5.Sales.Prospect.ProspectList oProspectList = new xFilm5.Sales.Prospect.ProspectList();
                        oProspectList.DockPadding.All = 6;
                        oProspectList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oProspectList);
                        break;
                    case "sales_pricelist":
                        xFilm5.Sales.PriceList.PriceList oPriceList = new xFilm5.Sales.PriceList.PriceList();
                        oPriceList.DockPadding.All = 6;
                        oPriceList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oPriceList);
                        break;
                    #endregion
                    #region Customer
                    case "cu_order":
                        xFilm5.Customer.JobOrder.JoList cuQueuing = new xFilm5.Customer.JobOrder.JoList(controls[0]);
                        cuQueuing.DockPadding.All = 6;
                        cuQueuing.Dock = DockStyle.Fill;
                        cuQueuing.WorkflowFrom = Common.Enums.Workflow.Queuing;
                        cuQueuing.WorkflowTo = Common.Enums.Workflow.Ready;
                        wspPane.Controls.Add(cuQueuing);
                        break;
                    case "cu_dropbox":
                        xFilm5.Customer.JobOrder.DropBox cuDropBox = new xFilm5.Customer.JobOrder.DropBox();
                        cuDropBox.DockPadding.All = 6;
                        cuDropBox.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuDropBox);
                        break;
                    #endregion
                    case "support_dropbox":
                        xFilm5.Support.DropBoxExplorer oDropBox = new xFilm5.Support.DropBoxExplorer();
                        oDropBox.DockPadding.All = 6;
                        oDropBox.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oDropBox);
                        break;
                }
            }
        }
        #endregion
    }
}