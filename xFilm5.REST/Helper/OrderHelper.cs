using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;
using xFilm5.REST.Models;

namespace xFilm5.REST.Helper
{
    public class OrderHelper
    {
        public static int PlaceOrder_Plate(OrderFormData_Plate data, int userId)
        {
            int result = 0;

            using (var ctx = new xFilmEntities())
            {
                using (var scope = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var workshop = ctx.vwWorkshopList.Where(x => x.WorkshopName == data.Workshop).SingleOrDefault();

                        #region dbo.OrderHeader
                        var oHeader = new OrderHeader();

                        oHeader.ClientID = data.ClientId;
                        oHeader.UserID = userId;
                        oHeader.ServiceType = (int)EnumHelper.Order.OrderType.Plate5;
                        oHeader.PrePressOp = 0;
                        oHeader.Priority = data.Priority;
                        oHeader.ProofingOp = workshop != null ? workshop.WorkshopId : 0;

                        oHeader.Attachment = false;
                        oHeader.AttachmentURL = String.Empty;

                        oHeader.Remarks = (data.Remarks == null) ? String.Empty : data.Remarks;
                        oHeader.Status = (int)EnumHelper.Order.Workflow.Ready;      //2016.06.17 paulus: 直接跳級至 Queuing => Ready
                        oHeader.DateReceived = DateTime.Now;
                        oHeader.DateCompleted = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidOn = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidRef = String.Empty;
                        oHeader.Amount = 0;

                        ctx.OrderHeader.Add(oHeader);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Details
                        var oDetails = new Order_Details();

                        oDetails.OrderID = oHeader.ID;

                        oDetails.Pages = (short)data.Items.Count;
                        oDetails.DeliveryMethod = data.Pickup ? (int)EnumHelper.Order.DeliveryMethod.PickUp : (int)EnumHelper.Order.DeliveryMethod.DeliverTo;
                        oDetails.DeliveryAddr = (data.Deliver) ? data.DeliverTo : 0;

                        ctx.Order_Details.Add(oDetails);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Internal
                        // 冇咗 PrepressOp，不過都要 update dbo.Internal，吉嘅都要，避免 related tables 麻煩
                        var oInternal = new Order_Internal();

                        oInternal.OrderID = oHeader.ID;
                        oInternal.OutputBy = 0;
                        oInternal.DateUpdated = DateTime.Parse("1900-01-01 00:00:00");
                        oInternal.UpdateCounter = 0;

                        ctx.Order_Internal.Add(oInternal);
                        ctx.SaveChanges();
                        #endregion

                        /** data.Remarks != comment, 所以要 skip
                        #region dbo.OrderComment: save 書版資料 => comment
                        if ((oHeader.ID != 0) && (!String.IsNullOrEmpty(data.Remarks)))
                        {
                            var oComment = new OrderComment();
                            oComment.OrderID = oHeader.ID;
                            oComment.Comment = data.Remarks;

                            ctx.OrderComment.Add(oComment);
                            ctx.SaveChanges();
                        }
                        #endregion
                        */

                        #region dbo.Order_Journal
                        var oLog = new Order_Journal();
                        oLog.OrderID = oHeader.ID; ;
                        oLog.UserID = userId;
                        oLog.Status = (int)EnumHelper.Order.Workflow.Ready;
                        oLog.DateUpdated = DateTime.Now;

                        ctx.Order_Journal.Add(oLog);
                        ctx.SaveChanges();
                        #endregion

                        #region post order items
                        foreach (vwPrintQueueVpsList_AvailablePlate item in data.Items)
                        {
                            #region dbo.OrderPkPrintQueueVps
                            var pkVpsExist = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == oHeader.ID && x.PrintQueueVpsId == item.VpsPrintQueueID).Any();
                            OrderPkPrintQueueVps pkVps = null;
                            if (pkVpsExist)
                            {
                                pkVps = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == oHeader.ID && x.PrintQueueVpsId == item.VpsPrintQueueID).Single();
                            }
                            else
                            {
                                pkVps = new OrderPkPrintQueueVps();
                                pkVps.OrderHeaderId = oHeader.ID;
                                pkVps.PrintQueueVpsId = item.VpsPrintQueueID;
                                pkVps.CheckedCip3 = false;           // 手機 apps 冇得選
                                pkVps.CheckedBlueprint = false;

                                pkVps.CreatedOn = DateTime.Now;
                                pkVps.CreatedBy = userId;
                                pkVps.Retired = false;
                                pkVps.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");
                                pkVps.RetiredBy = 0;
                            }
                            //var pkVps = new OrderPkPrintQueueVps();

                            //pkVps.OrderHeaderId = oHeader.ID;
                            //pkVps.PrintQueueVpsId = item.VpsPrintQueueID;

                            pkVps.CheckedPlate = true;
                            //pkVps.CheckedCip3 = false;           // 手機 apps 冇得選
                            //pkVps.CheckedBlueprint = false;

                            //pkVps.CreatedOn = DateTime.Now;
                            //pkVps.CreatedBy = userId;
                            pkVps.ModifiedOn = DateTime.Now;
                            pkVps.ModifiedBy = userId;
                            //pkVps.Retired = false;
                            //pkVps.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");
                            //pkVps.RetiredBy = 0;

                            if (!pkVpsExist)
                                ctx.OrderPkPrintQueueVps.Add(pkVps);
                            ctx.SaveChanges();
                            #endregion

                            #region dbo.PrintQueue_LifeCycle
                            //因為用咗 BeginTransaction，要用同一個 context 先可以 RollBack, 所以要喺 local 搞個 Log
                            //PrintQueueHelper.WriteLogWithVpsId(item.VpsPrintQueueID, EnumHelper.Order.PrintQSubitemType.Order, userId);
                            var vps = ctx.PrintQueue_VPS.Where(x => x.ID == item.VpsPrintQueueID).SingleOrDefault();
                            if (vps != null)
                            {
                                var log = new PrintQueue_LifeCycle();
                                log.PrintQueueId = vps.PrintQueueID;
                                log.PrintQueueVpsId = vps.ID;
                                log.PrintQSubitemType = (int)EnumHelper.Order.PrintQSubitemType.Order;
                                log.Status = (int)EnumHelper.Common.Status.Active;
                                log.CreatedOn = DateTime.Now;
                                log.CreatedBy = userId;

                                ctx.PrintQueue_LifeCycle.Add(log);
                                ctx.SaveChanges();
                            }
                            #endregion

                            #region Set Plate Ordered To True
                            var pqVps = ctx.PrintQueue_VPS.Where(x => x.ID == item.VpsPrintQueueID).SingleOrDefault();
                            if (pqVps != null)
                            {
                                pqVps.PlateOrdered = true;
                                pqVps.ModifiedOn = DateTime.Now;
                                pqVps.ModifiedBy = userId;

                                ctx.SaveChanges();
                            }
                            #endregion
                        }
                        #endregion

                        scope.Commit();
                        result = oHeader.ID;

                        #region 叫 Bot send Fcm Notifications
                        BotHelper.PostSendFcmOnOrder(oHeader.ID);
                        #endregion

                        #region 叫 Bot 抄 tiff / cip3
                        foreach (vwPrintQueueVpsList_AvailablePlate item in data.Items)
                        {
                            BotHelper.PostPlate(item.VpsPrintQueueID);
                            //BotHelper.PostCip3(item.VpsPrintQueueID);     // 好似冇理 cip3，冇搞就無謂 call
                        }
                        #endregion
                    }
                    catch
                    {
                        scope.Rollback();
                    }
                }
            }

            return result;
        }

        public static int PlaceOrder_Blueprint(OrderFormData_Blueprint data, int userId)
        {
            int result = 0;

            using (var ctx = new xFilmEntities())
            {
                using (var scope = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var workshop = ctx.vwWorkshopList.Where(x => x.WorkshopName == data.Workshop).SingleOrDefault();

                        #region dbo.OrderHeader
                        var oHeader = new OrderHeader();

                        oHeader.ClientID = data.ClientId;
                        oHeader.UserID = userId;
                        oHeader.ServiceType = (int)EnumHelper.Order.OrderType.Plate5;
                        oHeader.PrePressOp = 0;
                        oHeader.Priority = data.Priority;
                        oHeader.ProofingOp = workshop != null ? workshop.WorkshopId : 0;

                        oHeader.Attachment = false;
                        oHeader.AttachmentURL = String.Empty;

                        oHeader.Remarks = (data.Remarks == null) ? String.Empty : data.Remarks;
                        oHeader.Status = (int)EnumHelper.Order.Workflow.Ready;      //2016.06.17 paulus: 直接跳級至 Queuing => Ready
                        oHeader.DateReceived = DateTime.Now;
                        oHeader.DateCompleted = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidOn = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidRef = String.Empty;
                        oHeader.Amount = 0;

                        ctx.OrderHeader.Add(oHeader);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Details
                        var oDetails = new Order_Details();

                        oDetails.OrderID = oHeader.ID;

                        oDetails.Pages = (short)data.Items.Count;
                        oDetails.DeliveryMethod = data.Pickup ? (int)EnumHelper.Order.DeliveryMethod.PickUp : (int)EnumHelper.Order.DeliveryMethod.DeliverTo;
                        oDetails.DeliveryAddr = (data.Deliver) ? data.DeliverTo : 0;

                        ctx.Order_Details.Add(oDetails);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Internal
                        // 冇咗 PrepressOp，不過都要 update dbo.Internal，吉嘅都要，避免 related tables 麻煩
                        var oInternal = new Order_Internal();

                        oInternal.OrderID = oHeader.ID;
                        oInternal.OutputBy = 0;
                        oInternal.DateUpdated = DateTime.Parse("1900-01-01 00:00:00");
                        oInternal.UpdateCounter = 0;

                        ctx.Order_Internal.Add(oInternal);
                        ctx.SaveChanges();
                        #endregion

                        /** data.Remarks != comment, 所以要 skip
                        #region dbo.OrderComment: save 書版資料 => comment
                        if ((oHeader.ID != 0) && (!String.IsNullOrEmpty(data.Remarks)))
                        {
                            var oComment = new OrderComment();
                            oComment.OrderID = oHeader.ID;
                            oComment.Comment = data.Remarks;

                            ctx.OrderComment.Add(oComment);
                            ctx.SaveChanges();
                        }
                        #endregion
                        */

                        #region dbo.Order_Journal
                        var oLog = new Order_Journal();
                        oLog.OrderID = oHeader.ID; ;
                        oLog.UserID = userId;
                        oLog.Status = (int)EnumHelper.Order.Workflow.Ready;
                        oLog.DateUpdated = DateTime.Now;

                        ctx.Order_Journal.Add(oLog);
                        ctx.SaveChanges();
                        #endregion

                        #region post order items
                        foreach (vwPrintQueueVpsList_AvailablePlate item in data.Items)
                        {
                            // 因為 blueprint 落 order 時冇理 CMYK 專色, 要用檔案名搵哂出嚟
                            var subitems = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.VpsFileName.StartsWith(item.VpsFileName)).ToList();
                            foreach (vwPrintQueueVpsList_AvailablePlate subitem in subitems)
                            {
                                #region dbo.OrderPkPrintQueueVps
                                var pkVpsExist = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == oHeader.ID && x.PrintQueueVpsId == subitem.VpsPrintQueueID).Any();
                                OrderPkPrintQueueVps pkVps = null;
                                if (pkVpsExist)
                                {
                                    pkVps = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == oHeader.ID && x.PrintQueueVpsId == subitem.VpsPrintQueueID).Single();
                                }
                                else
                                {
                                    pkVps = new OrderPkPrintQueueVps();

                                    pkVps.OrderHeaderId = oHeader.ID;
                                    pkVps.PrintQueueVpsId = subitem.VpsPrintQueueID;

                                    pkVps.CheckedPlate = false;
                                    pkVps.CheckedCip3 = false;           // 手機 apps 冇選過, 暫時當有選

                                    pkVps.CreatedOn = DateTime.Now;
                                    pkVps.CreatedBy = userId;

                                    pkVps.Retired = false;
                                    pkVps.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");
                                    pkVps.RetiredBy = 0;
                                }
                                //pkVps.OrderHeaderId = oHeader.ID;
                                //pkVps.PrintQueueVpsId = subitem.VpsPrintQueueID;

                                //pkVps.CheckedPlate = false;
                                //pkVps.CheckedCip3 = false;           // 手機 apps 冇選過, 暫時當有選
                                pkVps.CheckedBlueprint = true;

                                //pkVps.CreatedOn = DateTime.Now;
                                //pkVps.CreatedBy = userId;
                                pkVps.ModifiedOn = DateTime.Now;
                                pkVps.ModifiedBy = userId;
                                //pkVps.Retired = false;
                                //pkVps.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");
                                //pkVps.RetiredBy = 0;

                                if (!pkVpsExist)
                                    ctx.OrderPkPrintQueueVps.Add(pkVps);
                                ctx.SaveChanges();
                                #endregion

                                #region dbo.PrintQueue_LifeCycle
                                //因為用咗 BeginTransaction，要用同一個 context 先可以 RollBack, 所以要喺 local 搞個 Log
                                //PrintQueueHelper.WriteLogWithVpsId(item.VpsPrintQueueID, EnumHelper.Order.PrintQSubitemType.Order, userId);
                                var vps = ctx.PrintQueue_VPS.Where(x => x.ID == subitem.VpsPrintQueueID).SingleOrDefault();
                                if (vps != null)
                                {
                                    var log = new PrintQueue_LifeCycle();
                                    log.PrintQueueId = vps.PrintQueueID;
                                    log.PrintQueueVpsId = vps.ID;
                                    log.PrintQSubitemType = (int)EnumHelper.Order.PrintQSubitemType.Order;
                                    log.Status = (int)EnumHelper.Common.Status.Active;
                                    log.CreatedOn = DateTime.Now;
                                    log.CreatedBy = userId;

                                    ctx.PrintQueue_LifeCycle.Add(log);
                                    ctx.SaveChanges();
                                }
                                #endregion

                                #region Set Blueprint Ordered To True
                                var pqVps = ctx.PrintQueue_VPS.Where(x => x.ID == subitem.VpsPrintQueueID).SingleOrDefault();
                                if (pqVps != null)
                                {
                                    var pq = ctx.PrintQueue.Where(x => x.ID == pqVps.PrintQueueID).SingleOrDefault();
                                    if (pq != null)
                                    {
                                        pq.BlueprintOrdered = true;
                                        pq.ModifiedOn = DateTime.Now;
                                        pq.ModifiedBy = userId;

                                        ctx.SaveChanges();
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        scope.Commit();
                        result = oHeader.ID;

                        #region 叫 Bot send Fcm Notifications
                        BotHelper.PostSendFcmOnOrder(oHeader.ID);
                        #endregion

                        #region 叫 Bot 抄 blueprint
                        foreach (vwPrintQueueVpsList_AvailablePlate item in data.Items)
                        {
                            BotHelper.PostBlueprint(item.VpsPrintQueueID);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        scope.Rollback();
                    }
                }
            }

            return result;
        }

        public static int PlaceOrder_Film(OrderFormData_Film data, int userId)
        {
            int result = 0;

            using (var ctx = new xFilmEntities())
            {
                using (var scope = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var workshop = ctx.vwWorkshopList.Where(x => x.WorkshopName == data.Workshop).SingleOrDefault();

                        #region dbo.OrderHeader
                        var oHeader = new OrderHeader();

                        oHeader.ClientID = data.ClientId;
                        oHeader.UserID = userId;
                        oHeader.ServiceType = (int)EnumHelper.Order.OrderType.Film5;
                        oHeader.PrePressOp = 0;
                        oHeader.Priority = data.Priority;
                        oHeader.ProofingOp = workshop != null ? workshop.WorkshopId : 0;

                        oHeader.Attachment = false;
                        oHeader.AttachmentURL = String.Empty;

                        oHeader.Remarks = (data.Remarks == null) ? String.Empty : data.Remarks;
                        oHeader.Status = (int)EnumHelper.Order.Workflow.Ready;      //2016.06.17 paulus: 直接跳級至 Queuing => Ready
                        oHeader.DateReceived = DateTime.Now;
                        oHeader.DateCompleted = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidOn = DateTime.Parse("1900-01-01 00:00:00");
                        oHeader.PaidRef = String.Empty;
                        oHeader.Amount = 0;

                        ctx.OrderHeader.Add(oHeader);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Details
                        var oDetails = new Order_Details();

                        oDetails.OrderID = oHeader.ID;

                        oDetails.Pages = (short)data.Items.Count;
                        oDetails.DeliveryMethod = data.Pickup ? (int)EnumHelper.Order.DeliveryMethod.PickUp : (int)EnumHelper.Order.DeliveryMethod.DeliverTo;
                        oDetails.DeliveryAddr = (data.Deliver) ? data.DeliverTo : 0;

                        ctx.Order_Details.Add(oDetails);
                        ctx.SaveChanges();
                        #endregion

                        #region dbo.Order_Internal
                        // 冇咗 PrepressOp，不過都要 update dbo.Internal，吉嘅都要，避免 related tables 麻煩
                        var oInternal = new Order_Internal();

                        oInternal.OrderID = oHeader.ID;
                        oInternal.OutputBy = 0;
                        oInternal.DateUpdated = DateTime.Parse("1900-01-01 00:00:00");
                        oInternal.UpdateCounter = 0;

                        ctx.Order_Internal.Add(oInternal);
                        ctx.SaveChanges();
                        #endregion

                        /** data.Remarks != comment, 所以要 skip
                        #region dbo.OrderComment: save 書版資料 => comment
                        if ((oHeader.ID != 0) && (!String.IsNullOrEmpty(data.Remarks)))
                        {
                            var oComment = new OrderComment();
                            oComment.OrderID = oHeader.ID;
                            oComment.Comment = data.Remarks;

                            ctx.OrderComment.Add(oComment);
                            ctx.SaveChanges();
                        }
                        #endregion
                        */

                        #region dbo.Order_Journal
                        var oLog = new Order_Journal();
                        oLog.OrderID = oHeader.ID; ;
                        oLog.UserID = userId;
                        oLog.Status = (int)EnumHelper.Order.Workflow.Ready;
                        oLog.DateUpdated = DateTime.Now;

                        ctx.Order_Journal.Add(oLog);
                        ctx.SaveChanges();
                        #endregion

                        #region post order items
                        foreach (vwPrintQueueVpsList_AvailableFilm item in data.Items)
                        {
                            #region dbo.OrderPkPrintQueueVps
                            var pkVps = new OrderPkPrintQueueVps();

                            pkVps.OrderHeaderId = oHeader.ID;
                            pkVps.PrintQueueVpsId = item.VpsPrintQueueID;

                            pkVps.CheckedPlate = true;          // HACK: Plate 同 Film 共用

                            pkVps.CreatedOn = DateTime.Now;
                            pkVps.CreatedBy = userId;
                            pkVps.ModifiedOn = DateTime.Now;
                            pkVps.ModifiedBy = userId;
                            pkVps.Retired = false;
                            pkVps.RetiredOn = DateTime.Parse("1900-01-01 00:00:00");
                            pkVps.RetiredBy = 0;

                            ctx.OrderPkPrintQueueVps.Add(pkVps);
                            ctx.SaveChanges();
                            #endregion

                            #region dbo.PrintQueue_LifeCycle
                            //因為用咗 BeginTransaction，要用同一個 context 先可以 RollBack, 所以要喺 local 搞個 Log
                            //PrintQueueHelper.WriteLogWithVpsId(item.VpsPrintQueueID, EnumHelper.Order.PrintQSubitemType.Order, userId);
                            var vps = ctx.PrintQueue_VPS.Where(x => x.ID == item.VpsPrintQueueID).SingleOrDefault();
                            if (vps != null)
                            {
                                var log = new PrintQueue_LifeCycle();
                                log.PrintQueueId = vps.PrintQueueID;
                                log.PrintQueueVpsId = vps.ID;
                                log.PrintQSubitemType = (int)EnumHelper.Order.PrintQSubitemType.Film;
                                log.Status = (int)EnumHelper.Common.Status.Active;
                                log.CreatedOn = DateTime.Now;
                                log.CreatedBy = userId;

                                ctx.PrintQueue_LifeCycle.Add(log);
                                ctx.SaveChanges();
                            }
                            #endregion

                            #region Set Film Ordered To True
                            var pqVps = ctx.PrintQueue_VPS.Where(x => x.ID == item.VpsPrintQueueID).SingleOrDefault();
                            if (pqVps != null)
                            {
                                pqVps.PlateOrdered = true;              // HACK: Plate 同 Film 共用
                                pqVps.ModifiedOn = DateTime.Now;
                                pqVps.ModifiedBy = userId;

                                ctx.SaveChanges();
                            }
                            #endregion
                    }
                        #endregion

                        scope.Commit();
                        result = oHeader.ID;

                        #region 叫 Bot send Fcm Notifications: 菲林可以 skip, 不過, 照 call 試下
                        BotHelper.PostSendFcmOnOrder(oHeader.ID);
                        #endregion

                        #region 叫 Bot 抄 tiff
                        foreach (vwPrintQueueVpsList_AvailableFilm item in data.Items)
                        {
                            BotHelper.PostFilm(item.VpsPrintQueueID);
                        }
                        #endregion
                    }
                    catch
                    {
                        scope.Rollback();
                    }
                }
            }

            return result;
        }
    }
}
