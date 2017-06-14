using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xFilm5.EF6;
using xFilm5.REST.Models;

namespace xFilm5.REST.Helper
{
    public static class ReceiptHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns>ReceiptHeaderId</returns>
        public static int SaveReceipt(List<vwOrderPkPrintQueueVpsListEx> list, int userId, CommonHelper.Enums.PaymentType type)
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

                            #region 全部 OrderPkPrintQueueVps InsRec: 一隻 dbo.ReceiptHeader
                            var hdr = new ReceiptHeader();

                            hdr.ClientId = clientId;

                            hdr.PaymentType = (int)type;
                            hdr.ReceiptNumber = String.Empty;                           // 依家未有，SaveChanges 之後再補番
                            hdr.ReceiptDate = DateTime.Now;
                            hdr.ReceiptAmount = 0;                                      // HACK: 依家未有，要遲啲補番

                            hdr.Paid = (type == CommonHelper.Enums.PaymentType.Cash) ? true : false;
                            hdr.PaidOn = (type == CommonHelper.Enums.PaymentType.Cash) ? DateTime.Now : new DateTime(1900, 1, 1);
                            hdr.PaidRef = "";
                            hdr.PaidAmount = 0;                                         // HACK: 依家未有，要遲啲補番

                            hdr.Status = (int)CommonHelper.Enums.Status.Active;
                            hdr.CreatedOn = DateTime.Now;
                            hdr.CreatedBy = userId;
                            hdr.ModifiedOn = DateTime.Now;
                            hdr.ModifiedBy = userId;

                            ctx.ReceiptHeader.Add(hdr);
                            ctx.SaveChanges();
                            #endregion

                            decimal totalAmount = 0;
                            foreach (var item in list)
                            {
                                var pkPq = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == item.OrderHeaderId && x.PrintQueueVpsId == item.PrintQueueVpsId).SingleOrDefault();
                                if (pkPq != null)
                                {
                                    var orderType = (CommonHelper.Enums.OrderType)item.OrderType;

                                    #region 每隻 OrderPkPrintQueueVps InsRec: 一隻 dbo.ReceiptDetail
                                    var dtl = new EF6.ReceiptDetail();

                                    dtl.ReceiptHeaderId = hdr.ReceiptHeaderId;
                                    dtl.OrderPkPrintQueueVpsId = pkPq.OrderPkPrintQueueVpsId;

                                    GetDetails(ref dtl, item);

                                    ctx.ReceiptDetail.Add(dtl);
                                    ctx.SaveChanges();
                                    #endregion

                                    totalAmount += dtl.Amount.Value;

                                    #region 記得 UpdRec: dbo.OrderPkPrintQueueVps
                                    pkPq.IsReceived = true;
                                    pkPq.IsBilled = type == CommonHelper.Enums.PaymentType.Cash ? true : false;
                                    pkPq.ModifiedOn = DateTime.Now;
                                    pkPq.ModifiedBy = userId;

                                    ctx.SaveChanges();
                                    #endregion

                                    #region UpdRec: dbo.OrderHeader
                                    pkPq.OrderHeader.DateCompleted = DateTime.Now;
                                    pkPq.OrderHeader.Status = (int)CommonHelper.Enums.Workflow.Completed;
                                    ctx.SaveChanges();
                                    #endregion

                                    #region InsRec: Order_Journal
                                    var log = new EF6.Order_Journal();

                                    log.OrderID = pkPq.OrderHeaderId.Value;
                                    log.Status = (int)CommonHelper.Enums.Workflow.Completed;
                                    log.UserID = userId;
                                    log.DateUpdated = DateTime.Now;

                                    ctx.Order_Journal.Add(log);
                                    #endregion

                                    #region InsRec: dbo.PrintQueue_LifeCycle
                                    var cycle = new EF6.PrintQueue_LifeCycle();

                                    cycle.PrintQueueId = pkPq.PrintQueue_VPS.PrintQueueID;
                                    cycle.PrintQueueVpsId = pkPq.PrintQueue_VPS.ID;
                                    cycle.PrintQSubitemType = (int)CommonHelper.Enums.PrintQSubitemType.Receipt;
                                    cycle.Status = (int)CommonHelper.Enums.Status.Active;
                                    cycle.CreatedOn = DateTime.Now;
                                    cycle.CreatedBy = userId;

                                    ctx.PrintQueue_LifeCycle.Add(cycle);
                                    ctx.SaveChanges();
                                    #endregion
                                }
                            }

                            #region 補番個 ReceiptNumber 同埋 total amount 落 dbo.ReceiptHeader
                            hdr.ReceiptNumber = hdr.ReceiptHeaderId.ToString();
                            hdr.ReceiptAmount = totalAmount;
                            hdr.PaidAmount = (type == CommonHelper.Enums.PaymentType.Cash) ? totalAmount : 0;
                            ctx.SaveChanges();
                            #endregion

                            scope.Commit();
                            result = hdr.ReceiptHeaderId;
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

        private static void GetDetails(ref EF6.ReceiptDetail dtl, vwOrderPkPrintQueueVpsListEx item)
        {
            String description = "";
            Decimal price = 0, discount = 0, amount = 0;

            var orderType = (CommonHelper.Enums.OrderType)item.OrderType;
            var clientId = item.ClientID;
            using (var ctx = new xFilmEntities())
            {
                switch (orderType)
                {
                    case CommonHelper.Enums.OrderType.Blueprint:
                        #region 計價（藍紙）
                        var bpCode = String.Format("{0}-BP", item.VpsPlateSize);
                        var bp = ctx.T_BillingCode_Item.Where(x => x.ItemCode == bpCode).SingleOrDefault();
                        if (bp != null)
                        {
                            var bpVip = ctx.ClientPricing.Where(x => x.ClientId == clientId && x.ItemId == bp.ID).SingleOrDefault();

                            description = item.VpsFileName.Substring(0, item.VpsFileName.Length - 4);
                            description = String.Format("{0}: {1}", item.OrderHeaderId.ToString(), description);
                            price = bpVip != null ? bpVip.UnitPrice : bp.UnitPrice.Value;
                            discount = bpVip != null ? bpVip.Discount : 0;
                            amount = price * (100 - discount) / 100;

                            // price, discount, amount
                            dtl.BillingCode = bpCode;
                            dtl.Description = description;
                            dtl.Qty = 1;
                            dtl.UnitAmount = price;
                            dtl.Discount = discount;
                            dtl.Amount = amount;
                        }
                        #endregion
                        break;
                    case CommonHelper.Enums.OrderType.Film:
                        #region 計價（菲林）
                        var fmCode = String.Format("{0}", item.VpsPlateSize);
                        var fm = ctx.T_BillingCode_Item.Where(x => x.ItemCode == fmCode).SingleOrDefault();
                        if (fm != null)
                        {
                            var fmVip = ctx.ClientPricing.Where(x => x.ClientId == clientId && x.ItemId == fm.ID).SingleOrDefault();

                            description = item.VpsFileName.Substring(0, item.VpsFileName.Length - 4);
                            description = String.Format("{0}: {1}", item.OrderHeaderId.ToString(), description);

                            price = fmVip != null ? fmVip.UnitPrice : fm.UnitPrice.Value;
                            discount = fmVip != null ? fmVip.Discount : 0;
                            amount = price * (100 - discount) / 100;

                            var fmCodeMin = String.Format("{0}00", item.VpsFileName.Substring(0, 1));
                            var fmMin = ctx.T_BillingCode_Item.Where(x => x.ItemCode == fmCodeMin).SingleOrDefault();
                            var fmVipMin = ctx.ClientPricing.Where(x => x.ClientId == clientId && x.ItemId == fmMin.ID).SingleOrDefault();

                            Decimal unitPriceMm = (fmVipMin != null) ? fmVipMin.UnitPrice : fmMin.UnitPrice.Value;

                            // 如果 長 X 闊 X 菲林價 少於 minimum，收 minimum
                            var sides = item.VpsPlateSize.Substring(1).Split('x');
                            Decimal xLength = 0, yLength = 0;
                            xLength = Decimal.TryParse(sides[0], out xLength) ? xLength : 0;
                            yLength = Decimal.TryParse(sides[1], out yLength) ? yLength : 0;
                            bool minCharge = unitPriceMm > Math.Round(xLength * yLength * amount);

                            Decimal prx = minCharge ? unitPriceMm : price;
                            Decimal disc = discount;
                            //Decimal amount = minCharge ? itemMm.UnitPrice : (int)Math.Ceiling(xLength * yLength * itemF.UnitPrice);
                            Decimal amt = minCharge ? unitPriceMm : Math.Round(xLength * yLength * amount);

                            // prx, disc, amt
                            dtl.BillingCode = fmCode;
                            dtl.Description = description;
                            dtl.Qty = 1;
                            dtl.UnitAmount = prx;
                            dtl.Discount = disc;
                            dtl.Amount = amt;
                        }
                        #endregion
                        break;
                    case CommonHelper.Enums.OrderType.Plate:
                        #region 計價（鋅板）
                        var ptCode = String.Format("{0}", item.VpsPlateSize);
                        var pt = ctx.T_BillingCode_Item.Where(x => x.ItemCode == ptCode).SingleOrDefault();
                        if (pt != null)
                        {
                            var bpVip = ctx.ClientPricing.Where(x => x.ClientId == clientId && x.ItemId == pt.ID).SingleOrDefault();

                            description = item.VpsFileName.Substring(0, item.VpsFileName.Length - 4);
                            description = String.Format("{0}: {1}", item.OrderHeaderId.ToString(), description);
                            price = bpVip != null ? bpVip.UnitPrice : pt.UnitPrice.Value;
                            discount = bpVip != null ? bpVip.Discount : 0;
                            amount = price * (100 - discount) / 100;

                            // price, discount, amount
                            dtl.BillingCode = ptCode;
                            dtl.Description = description;
                            dtl.Qty = 1;
                            dtl.UnitAmount = price;
                            dtl.Discount = discount;
                            dtl.Amount = amount;
                        }
                        #endregion
                        break;
                }
            }
        }
    }
}