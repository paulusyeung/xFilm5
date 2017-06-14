using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;
using xFilm5.REST.Models;

namespace xFilm5.REST.Helper
{
    public static class InvoiceHelper
    {
        public static int SaveCashInvoice(List<vwOrderPkPrintQueueVpsListEx> list, int userId, int receiptHeaderId)
        {
            int result = 0;

            if ((list.Count > 0) && (userId != 0))
            {
                using (var ctx = new xFilmEntities())
                {
                    using (var scope = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            var clientId = list[0].ClientID;

                            var receiptHdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptHeaderId).SingleOrDefault();
                            if (receiptHdr != null)
                            {
                                #region InsRec: Acct_INMaster
                                var inv5Hdr = new EF6.Acct_INMaster();

                                inv5Hdr.InvoiceNumber = CommonHelper.xFilm5System.GetNextInvoiceNumber();
                                inv5Hdr.InvoiceDate = receiptHdr.ReceiptDate;
                                inv5Hdr.InvoiceAmount = receiptHdr.ReceiptAmount;

                                inv5Hdr.ClientID = receiptHdr.ClientId;
                                inv5Hdr.PaymentType = (int)CommonHelper.Enums.PaymentType.Cash;
                                inv5Hdr.Paid = true;
                                inv5Hdr.PaidOn = DateTime.Now;
                                inv5Hdr.PaidAmount = receiptHdr.ReceiptAmount.Value;
                                inv5Hdr.CreatedOn = DateTime.Now;
                                inv5Hdr.CreatedBy = userId;
                                inv5Hdr.LastModifiedOn = DateTime.Now;
                                inv5Hdr.LastModifiedBy = userId;
                                inv5Hdr.Status = (int)CommonHelper.Enums.Status.Active;

                                ctx.Acct_INMaster.Add(inv5Hdr);
                                ctx.SaveChanges();

                                receiptHdr.INMasterId = inv5Hdr.ID;
                                receiptHdr.Paid = true;
                                receiptHdr.PaidOn = DateTime.Now;
                                receiptHdr.PaidAmount = receiptHdr.ReceiptAmount.Value;
                                ctx.SaveChanges();
                                #endregion

                                foreach (var item in list)
                                {
                                    var pkPq = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == item.OrderHeaderId && x.PrintQueueVpsId == item.PrintQueueVpsId).SingleOrDefault();
                                    if (pkPq != null)
                                    {
                                        #region 每隻 OrderPkPrintQueueVps InsRec: 一隻 dbo.Acct_INDetails
                                        var receiptDtl = ctx.ReceiptDetail.Where(x => x.OrderPkPrintQueueVpsId == pkPq.OrderPkPrintQueueVpsId).SingleOrDefault();

                                        var inv5Dtl = new EF6.Acct_INDetails();

                                        inv5Dtl.INMasterID = inv5Hdr.ID;
                                        inv5Dtl.OrderPkPrintQueueVpsId = receiptDtl.OrderPkPrintQueueVpsId;
                                        inv5Dtl.BillingCode = receiptDtl.BillingCode;
                                        inv5Dtl.Description = receiptDtl.Description;
                                        inv5Dtl.Qty = receiptDtl.Qty;
                                        inv5Dtl.UnitAmount = receiptDtl.UnitAmount;
                                        inv5Dtl.Discount = receiptDtl.Discount;
                                        inv5Dtl.Amount = receiptDtl.Amount;

                                        ctx.Acct_INDetails.Add(inv5Dtl);
                                        ctx.SaveChanges();
                                        #endregion

                                        #region UpdRec: dbo.OrderPkPrintQueueVps.IsBilled
                                        pkPq.IsBilled = true;
                                        pkPq.ModifiedOn = DateTime.Now;
                                        pkPq.ModifiedBy = userId;
                                        ctx.SaveChanges();
                                        #endregion

                                        #region InsRec: dbo.PrintQueue_LifeCycle
                                        var cycle = new EF6.PrintQueue_LifeCycle();

                                        cycle.PrintQueueId = pkPq.PrintQueue_VPS.PrintQueueID;
                                        cycle.PrintQueueVpsId = pkPq.PrintQueue_VPS.ID;
                                        cycle.PrintQSubitemType = (int)CommonHelper.Enums.PrintQSubitemType.Invoice;
                                        cycle.Status = (int)CommonHelper.Enums.Status.Active;
                                        cycle.CreatedOn = DateTime.Now;
                                        cycle.CreatedBy = userId;

                                        ctx.PrintQueue_LifeCycle.Add(cycle);
                                        ctx.SaveChanges();
                                        #endregion
                                    }
                                }

                                scope.Commit();
                                result = inv5Hdr.ID;
                            }
                        }
                        catch (Exception)
                        {
                            scope.Rollback();
                        }
                    }
                }
            }

            return result;
        }

        public static void PrintToXprinter(int invoiceId)
        {
            using (var ctx = new xFilmEntities())
            {
                var receipt = ctx.ReceiptHeader.Where(x => x.INMasterId == invoiceId).SingleOrDefault();

                if (receipt != null)
                    Helper.BotHelper.PostXprinter(receipt.ReceiptHeaderId);
            }
        }
    }
}
