using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;
using xFilm5.REST.Models;

namespace xFilm5.REST.Controllers
{
    public class CashierController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 6, 1);

        [HttpGet]
        [Route("api/Cashier/Ready/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetReady(int id)
        {
            if (id == 0)
            {
                #region All clients
                using (var ctx = new xFilmEntities())
                {   // 菲林冇 QR Code scan，所以未攞就當 Ready / WiP
                    var qryBp = String.Format(@"
Select * from (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 1 AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    var qryFilm = String.Format(@"
Select * from (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    var qryPlate = String.Format(@"
Select * from (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 1 AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).ThenBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
            else
            {
                #region A single client
                using (var ctx = new xFilmEntities())
                {
                    var qryBp = String.Format(@"
Select * from (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 1 AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    var qryFilm = String.Format(@"
Select * from (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    var qryPlate = String.Format(@"
Select * from (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 1 AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).ThenBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Cashier/WiP/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetWiP(int id)
        {
            if (id == 0)
            {
                #region All clients
                using (var ctx = new xFilmEntities())
                {
                    //var qryBp = String.Format("SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));
                    var qryBp = String.Format(@"
Select * from (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    //var qryFilm = String.Format("SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));
                    var qryFilm = String.Format(@"
Select * from (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    //var qryPlate = String.Format("SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));
                    var qryPlate = String.Format(@"
Select * from (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"));

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).OrderBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
            else
            {
                #region A single client
                using (var ctx = new xFilmEntities())
                {
                    //var qryBp = String.Format("SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());
                    var qryBp = String.Format(@"
Select * from (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    //var qryFilm = String.Format("SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());
                    var qryFilm = String.Format(@"
Select * from (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    //var qryPlate = String.Format("SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());
                    var qryPlate = String.Format(@"
Select * from (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE [CreatedOn] >= '{0}' AND [IsReady] = 0 AND [IsReceived] = 0 AND [ClientID] = {1}
) AS pk INNER JOIN
dbo.OrderHeader AS o ON pk.OrderHeaderId = o.ID
WHERE (o.Status <> 1)", _DateZero.ToString("yyyy-MM-dd hh:mm:sss"), id.ToString());

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).OrderBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Cashier/Completed/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public IHttpActionResult GetCompleted(int id, DateTime date)
        {
            if (id == 0)
            {
                #region All clients
                using (var ctx = new xFilmEntities())
                {
                    //var qryBp = String.Format("SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1", date.ToString("yyyy-MM-dd"));
                    var qryBp = String.Format(@"
SELECT * FROM (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}')", date.ToString("yyyy-MM-dd"));

                    //var qryFilm = String.Format("SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1", date.ToString("yyyy-MM-dd"));
                    var qryFilm = String.Format(@"
SELECT * FROM (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}')", date.ToString("yyyy-MM-dd"));

                    //var qryPlate = String.Format("SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1", date.ToString("yyyy-MM-dd"));
                    var qryPlate = String.Format(@"
SELECT * FROM (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}')", date.ToString("yyyy-MM-dd"));

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).ThenBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
            else
            {
                #region A single client
                using (var ctx = new xFilmEntities())
                {
                    //var qryBp = String.Format("SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}", date.ToString("yyyy-MM-dd"), id.ToString());
                    var qryBp = String.Format(@"
SELECT * FROM (
SELECT 1 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Blueprint
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}' AND [ClientID] = {1})", date.ToString("yyyy-MM-dd"), id.ToString());

                    //var qryFilm = String.Format("SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}", date.ToString("yyyy-MM-dd"), id.ToString());
                    var qryFilm = String.Format(@"
SELECT * FROM (
SELECT 2 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Film
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}' AND [ClientID] = {1})", date.ToString("yyyy-MM-dd"), id.ToString());

                    //var qryPlate = String.Format("SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}", date.ToString("yyyy-MM-dd"), id.ToString());
                    var qryPlate = String.Format(@"
SELECT * FROM (
SELECT 3 AS [OrderType], * FROM dbo.vwOrderPkPrintQueueVpsList_Plate
WHERE CONVERT(NVARCHAR(10), [ModifiedOn], 120) = '{0}' AND [IsReceived] = 1 AND [ClientID] = {1}
) AS oops
WHERE [PrintQueueVpsId] IN (
SELECT DISTINCT [PrintQueueVpsId]
FROM dbo.vwReceiptDetailsList_Ex
WHERE CONVERT(NVARCHAR(10), [ReceiptDate], 120) = '{0}' AND [ClientID] = {1})", date.ToString("yyyy-MM-dd"), id.ToString());

                    var bp = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryBp).ToList();
                    var film = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryFilm).ToList();
                    var plate = ctx.Database.SqlQuery<vwOrderPkPrintQueueVpsListEx>(qryPlate).ToList();
                    plate.AddRange(bp);
                    plate.AddRange(film);
                    var all = plate.OrderBy(x => x.OrderHeaderId).ThenBy(x => x.VpsFileName);
                    return Json(all);
                }
                #endregion
            }
        }

        [HttpPost]
        [Route("api/Cashier/Monthly/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostMonthly(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var json = Request.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<vwOrderPkPrintQueueVpsListEx>>(json);

            int clientId;
            clientId = Int32.TryParse(id, out clientId) ? clientId : 0;
            clientId = (clientId == 0) ? list[0].ClientID : clientId;

            using (var ctx = new xFilmEntities())
            {
                var sid = Guid.Parse(userSid);
                var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                if (user != null)
                {
                    var receiptId = ReceiptHelper.SaveReceipt(list, user.UserId, CommonHelper.Enums.PaymentType.OnAccount);
                    if (receiptId != 0)
                    {
                        // 由 xFilm5.Bot 負責打印小票據
                        if (Helper.ClientHelper.IsReceiptSlip(clientId))
                        {
                            Helper.BotHelper.PostXprinter(receiptId, clientId);
                        }

                        // 由 xFilm5.Bot 負責 email 張單
                        var recipient = ClientHelper.GetEmailRecipient(clientId);
                        BotHelper.PostEmailReceipt(receiptId, recipient, clientId);

                        return Ok();
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/Cashier/Cash/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostCash(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var json = Request.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<vwOrderPkPrintQueueVpsListEx>>(json);

            int clientId;
            clientId = Int32.TryParse(id, out clientId) ? clientId : 0;
            clientId = (clientId == 0) ? list[0].ClientID : clientId;

            using (var ctx = new xFilmEntities())
            {
                var sid = Guid.Parse(userSid);
                var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                if (user != null)
                {
                    var receiptId = ReceiptHelper.SaveReceipt(list, user.UserId, CommonHelper.Enums.PaymentType.Cash);
                    if (receiptId != 0)
                    {
                        var invId = InvoiceHelper.SaveCashInvoice(list, user.UserId, receiptId);
                        if (invId != 0)
                        {
                            // 由 xFilm5.Bot 負責打印小票據
                            if (Helper.ClientHelper.IsReceiptSlip(clientId))
                            {
                                Helper.InvoiceHelper.PrintToXprinter(invId);
                            }

                            // 由 xFilm5.Bot 負責 email 張單
                            var recipient = ClientHelper.GetEmailRecipient(clientId);
                            BotHelper.PostEmailReceipt(receiptId, recipient, clientId);

                            return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/Reprint/Receipt/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostReprintReceipt(string id)
        {
            /**
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var json = Request.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<vwOrderPkPrintQueueVpsListEx>>(json);
            */
            int receiptId;
            receiptId = Int32.TryParse(id, out receiptId) ? receiptId : 0;

            using (var ctx = new xFilmEntities())
            {
                var hdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptId).SingleOrDefault();
                if (hdr != null)
                {
                    // 由 xFilm5.Bot 負責打印小票據
                    if (Helper.ClientHelper.IsReceiptSlip(hdr.ClientId))
                    {
                        Helper.BotHelper.PostXprinter(receiptId, hdr.ClientId);
                    }
                    /**
                    // 由 xFilm5.Bot 負責 email 張單
                    var recipient = ClientHelper.GetEmailRecipient(hdr.ClientId);
                    BotHelper.PostEmailReceipt(receiptId, recipient, hdr.ClientId);
                    */
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/Reprint/Invoice/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostReprintInvoice(string id)
        {
            /**
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var json = Request.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<vwOrderPkPrintQueueVpsListEx>>(json);
            */
            int invoiceId;
            invoiceId = Int32.TryParse(id, out invoiceId) ? invoiceId : 0;

            using (var ctx = new xFilmEntities())
            {
                var inv = ctx.Acct_INMaster.Where(x => x.ID == invoiceId).SingleOrDefault();
                if (inv != null)
                {
                    var receipt = ctx.ReceiptHeader.Where(x => x.INMasterId == invoiceId).SingleOrDefault();
                    // 由 xFilm5.Bot 負責打印小票據
                    if (Helper.ClientHelper.IsReceiptSlip(inv.ClientID))
                    {
                        Helper.InvoiceHelper.PrintToXprinter(invoiceId);
                    }
                    /**
                    // 由 xFilm5.Bot 負責 email 張單
                    var recipient = ClientHelper.GetEmailRecipient(inv.ClientID);
                    BotHelper.PostEmailReceipt(receipt.ReceiptHeaderId, recipient, inv.ClientID);
                    */
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
