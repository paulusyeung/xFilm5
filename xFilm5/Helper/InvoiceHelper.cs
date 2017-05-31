using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class InvoiceHelper
    {
        public static bool IsBilling5(int invoiceId)
        {
            bool result = false;

            DAL.Acct_INMaster item = DAL.Acct_INMaster.Load(invoiceId);
            if (item != null)
            {
                if (item.OrderID == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool IsCash(int invoiceId)
        {
            bool result = false;

            DAL.Acct_INMaster item = DAL.Acct_INMaster.Load(invoiceId);
            if (item != null)
            {
                if (item.PaymentType != 0)  // 0 = 月結
                {
                    result = true;
                }
            }

            return result;
        }

        public static void PrintToXprinter(int invoiceId)
        {
            String sql = String.Format("INMasterId = {0}", invoiceId.ToString());
            DAL.ReceiptHeader receipt = DAL.ReceiptHeader.LoadWhere(sql);
            if (receipt != null)
                Helper.BotHelper.PostXprinter(receipt.ReceiptHeaderId);
        }

        public static bool GenMonthlyInvoice(List<EF6.vwReceiptDetailsList_Ex> items, int clientId, int invoiceNumber, DateTime invoiceDate, Decimal invoiceAmount)
        {
            bool result = false;
            int invMasterId = 0;

            if (items.Count > 0)
            {
                using (var ctx = new EF6.xFilmEntities())
                {
                    // 用 Transactions 可以快啲同埋有 error 可以 roolback
                    using (var scope = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            #region InsRec: dbo.Acct_INMaster
                            var inv5Hdr = new EF6.Acct_INMaster();

                            inv5Hdr.InvoiceNumber = invoiceNumber;
                            inv5Hdr.InvoiceDate = invoiceDate;
                            inv5Hdr.InvoiceAmount = invoiceAmount;
                            inv5Hdr.ClientID = clientId;

                            inv5Hdr.Paid = false;
                            inv5Hdr.Remarks = "";

                            inv5Hdr.CreatedOn = DateTime.Now;
                            inv5Hdr.CreatedBy = CommonHelper.Config.CurrentUserId;
                            inv5Hdr.LastModifiedOn = DateTime.Now;
                            inv5Hdr.LastModifiedBy = CommonHelper.Config.CurrentUserId;
                            inv5Hdr.Status = (int)CommonHelper.Enums.Status.Active;

                            ctx.Acct_INMaster.Add(inv5Hdr);
                            ctx.SaveChanges();

                            invMasterId = inv5Hdr.ID;
                            #endregion

                            for (int i = 0; i < items.Count; i++)
                            {
                                var item = items[i];
                                var hdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == item.ReceiptHeaderId).SingleOrDefault();
                                if (hdr != null)
                                {
                                    #region UpdRec: dbo.ReceiptHeader.INMasterId
                                    hdr.INMasterId = invMasterId;
                                    //ctx.SaveChanges();
                                    #endregion

                                    var dtl = ctx.ReceiptDetail.Where(x => x.ReceiptHeaderId == hdr.ReceiptHeaderId).ToList();
                                    if (dtl.Count > 0)
                                    {
                                        for (int j = 0; j < dtl.Count; j++)
                                        {
                                            var dtlRow = dtl[j];
                                            var pk = ctx.OrderPkPrintQueueVps.Where(x => x.OrderPkPrintQueueVpsId == dtlRow.OrderPkPrintQueueVpsId).SingleOrDefault();
                                            if (pk != null)
                                            {
                                                #region UpdRec: dbo.OrderPkPrintQueueVpsId.IsBilled
                                                pk.IsBilled = true;
                                                pk.ModifiedOn = DateTime.Now;
                                                pk.ModifiedBy = CommonHelper.Config.CurrentUserId;
                                                #endregion

                                                #region InsRec: dbo.Acct_INDetails
                                                var invDetail = new EF6.Acct_INDetails();

                                                invDetail.INMasterID = invMasterId;
                                                invDetail.OrderPkPrintQueueVpsId = pk.OrderPkPrintQueueVpsId;
                                                invDetail.BillingCode = dtlRow.BillingCode;
                                                invDetail.Description = dtlRow.Description;
                                                invDetail.Qty = dtlRow.Qty;
                                                invDetail.UnitAmount = dtlRow.UnitAmount;
                                                invDetail.Discount = dtlRow.Discount;
                                                invDetail.Amount = dtlRow.Amount;

                                                ctx.Acct_INDetails.Add(invDetail);
                                                #endregion

                                                //ctx.SaveChanges();
                                            }
                                        }
                                    }
                                    // 一次過 save 哩隻 ReceiptHeader 有關嘅 changes
                                    ctx.SaveChanges();
                                }
                            }
                            scope.Commit();
                            result = true;
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

        public static bool SetInvoiceToPaid(int invoiceId, DateTime paidOn, String paidRef)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                // 用 Transactions 可以快啲同埋有 error 可以 roolback
                using (var scope = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var invHdr = ctx.Acct_INMaster.Where(x => x.ID == invoiceId).SingleOrDefault();
                        if (invHdr != null)
                        {
                            #region UpdRec: dbo.Acct_INMaster
                            invHdr.Paid = true;
                            invHdr.PaidAmount = invHdr.InvoiceAmount.Value;
                            invHdr.PaidOn = paidOn;
                            invHdr.PaidRef = paidRef;
                            invHdr.LastModifiedOn = DateTime.Now;
                            invHdr.LastModifiedBy = CommonHelper.Config.CurrentUserId;
                            #endregion

                            #region UpdRec: related dbo.ReceiptHeader
                            var invDtls = ctx.Acct_INDetails.Where(x => x.INMasterID == invoiceId).ToList();
                            if (invDtls.Count > 0)
                            {
                                for (int i = 0; i < invDtls.Count; i++)
                                {
                                    var invDtl = invDtls[i];
                                    var rDetails = ctx.ReceiptDetail.Where(x => x.OrderPkPrintQueueVpsId == invDtl.OrderPkPrintQueueVpsId).ToList();
                                    if (rDetails.Count > 0)
                                    {
                                        for (int j = 0; j < rDetails.Count; j++)
                                        {
                                            var rDetail = rDetails[j];
                                            var rHeader = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == rDetail.ReceiptHeaderId).SingleOrDefault();
                                            if (rHeader != null)
                                            {
                                                rHeader.Paid = true;
                                                rHeader.PaidAmount = invHdr.InvoiceAmount.Value;
                                                rHeader.PaidOn = paidOn;
                                                rHeader.PaidRef = paidRef;
                                                rHeader.ModifiedOn = DateTime.Now;
                                                rHeader.ModifiedBy = CommonHelper.Config.CurrentUserId;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            ctx.SaveChanges();

                            scope.Commit();
                            result = true;
                        }
                    }
                    catch (Exception)
                    {
                        scope.Rollback();
                    }
                }
            }

            return result;
        }
    }
}
