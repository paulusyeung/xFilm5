using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public class OrderHelper
    {
        public static bool PlacingOrder_Plate(List<vwPrintQueueVpsList_AvailablePlate> items)
        {
            bool result = false;

            result = SaveNew();

            #region save 書版資料 => comment
            if ((_OrderId != 0) && (_Comment != String.Empty))
            {
                xFilm5.DAL.OrderComment comment = new OrderComment();
                comment.OrderID = _OrderId;
                comment.Comment = _Comment;
                comment.Save();
            }
            #endregion

            return result;
        }

        private static bool SaveNew()
        {
            bool result = false;

            if (SaveNewHeader())
            {
                if (SaveNewDetails() && SaveNewInternal())
                {
                    result = true;

                    //2016.06.17 paulus: 直接跳級至 Queuing => Ready
                    Common.Order.WriteLog(_OrderId, Common.Enums.Workflow.Ready);

                    //2016.07.17 paulus: Plate5 根據 order，搬運 Tiff 同 Blueprint 檔案
                    SavePrintQueue();
                    //xFilm5.Controls.Utility.JobOrder.Plate5_Shuffle(_OrderId);
                }
                else
                {
                    // delete the Header
                    OrderHeader oHeader = OrderHeader.Load(_OrderId);
                    oHeader.Delete();
                }
            }

            return result;
        }

        private static bool SaveNewHeader()
        {
            bool result = false;

            try
            {
                OrderHeader oHeader = new OrderHeader();

                oHeader.ClientID = _ClientId;
                oHeader.UserID = Common.Config.CurrentUserId;
                oHeader.ServiceType = (int)Common.Enums.OrderType.Plate5;
                oHeader.PrePressOp = 0;
                oHeader.Priority = (int)cboPriority.SelectedValue;
                oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;

                oHeader.Attachment = false;

                oHeader.Remarks = txtRemarks.Text;
                //2016.06.17 paulus: 直1跳級至 Queuing => Ready
                oHeader.Status = (int)Common.Enums.Workflow.Ready;
                oHeader.DateReceived = DateTime.Now;
                oHeader.Save();

                _OrderId = oHeader.ID;
                result = true;
            }
            catch { }
            return result;
        }

        private static bool SaveNewDetails()
        {
            bool result = false;

            Order_Details oDetails = new Order_Details();

            oDetails.OrderID = _OrderId;

            #region body
            //oDetails.Positive = chkCip3.Checked;

            //oDetails.Proofing = chkProofingWith.Checked;        // Plate5 改為: 要唔要藍紙
            #endregion

            #region footer: total pages, delivery address
            //try
            //{
            //    oDetails.Pages = Convert.ToInt16(txtTotalPages.Text.Trim());
            //}
            //catch
            //{
            //    oDetails.Pages = 0;
            //}
            short totalPages = 0;
            oDetails.Pages = short.TryParse(txtTotalPages.Text.Trim(), out totalPages) ? totalPages : totalPages;
            if (chkPickUp.Checked)
            {
                oDetails.DeliveryMethod = (int)Common.Enums.DeliveryMethod.PickUp;
            }
            if (chkDeliverTo.Checked)
            {
                oDetails.DeliveryMethod = (int)Common.Enums.DeliveryMethod.DeliverTo;
                oDetails.DeliveryAddr = (int)cboDeliveryAddress.SelectedValue;
            }
            #endregion

            try
            {
                oDetails.Save();
                result = true;
            }
            catch { }

            return result;
        }

        private static bool SaveNewInternal()
        {
            bool result = true;

            Order_Internal oInternal = new Order_Internal();
            oInternal.OrderID = _OrderId;

            // 冇咗 PrepressOp，不過都要 update dbo.Internal，吉嘅都要，避免 related tables 麻煩
            //if (chkPrepressOp.Checked)
            //{
            //    oInternal.OutputBy = (int)cboPrepressOp.SelectedValue;
            //}
            try
            {
                oInternal.Save();
                result = true;
            }
            catch { }

            return result;
        }

        private static bool SaveEdit()
        {
            bool result = false;

            try
            {
                OrderHeader oHeader = OrderHeader.Load(_OrderId);
                if (oHeader != null)
                {
                    oHeader.ProofingOp = (int)cboWorkshop.SelectedValue;
                    oHeader.Save();
                }
                result = true;
            }
            catch { }

            return result;
        }

        private static bool SavePrintQueue()
        {
            bool result = false;
            String lastBpFileName = "";

            foreach (ListViewItem item in lvwVpsList.Items)
            {
                int vpsPrintQueueId = Convert.ToInt32(item.SubItems[0].Text);
                String vpsFileName = item.SubItems[colVpsFileName.Index].Text;
                bool vpsChecked = item.SubItems[colPlate.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;
                bool cip3Checked = item.SubItems[colCIP3.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;
                bool bpChecked = item.SubItems[colBlueprint.Index].Text.IndexOf("checkbox-ok") >= 0 ? true : false;

                String bpFileName = vpsFileName.Substring(0, vpsFileName.LastIndexOf('('));

                if (vpsChecked || cip3Checked || bpChecked)
                {
                    #region Save dbo.OrderPkPrintQueueVps
                    DAL.OrderPkPrintQueueVps pkVps = new OrderPkPrintQueueVps();
                    pkVps.OrderHeaderId = _OrderId;
                    pkVps.PrintQueueVpsId = vpsPrintQueueId;

                    pkVps.CheckedPlate = vpsChecked;
                    pkVps.CheckedCip3 = cip3Checked;
                    pkVps.CheckedBlueprint = bpChecked;
                    pkVps.CreatedOn = DateTime.Now;
                    pkVps.CreatedBy = DAL.Common.Config.CurrentUserId;
                    pkVps.ModifiedOn = DateTime.Now;
                    pkVps.ModifiedBy = DAL.Common.Config.CurrentUserId;
                    pkVps.Retired = false;
                    pkVps.Save();
                    #endregion

                    xFilm5.Controls.Utility.PrintQueue_LifeCycle.WriteLogWithVpsId(vpsPrintQueueId, DAL.Common.Enums.PrintQSubitemType.Order);
                }

                #region 抄 tiff / cip3 / blueprint
                if (vpsChecked)
                {
                    SetPlateOrderedToTrue(vpsPrintQueueId);

                    // 抄 tiff
                    Post_xFilm5Bot_Plate(vpsPrintQueueId);
                }

                if (cip3Checked)
                {
                    // 抄 cip3
                    //Post_xFilm5Bot_Cip3(vpsPrintQueueId);
                }

                if (bpChecked)
                {
                    SetBlueprintOrderedToTrue(vpsPrintQueueId);

                    // debug 用
                    //String source = Path.Combine(@"\\192.168.12.230\DirectPrint\EfiProof", "*" + bpFileName +"*");
                    //FileInfo[] files = Helper.FileHelper.FileUtils.GetFilesMatchWildCard(source, "MonAgent", "nx-9602");

                    // 2017.04.11 paulus: 改為 wildcard，祇 call 一次
                    if (bpFileName != lastBpFileName)
                    {
                        // 抄 blueprint
                        Post_xFilm5Bot_Blueprint(vpsPrintQueueId);

                        lastBpFileName = bpFileName;
                    }
                }
                #endregion
            }

            return result;
        }

    }
}
