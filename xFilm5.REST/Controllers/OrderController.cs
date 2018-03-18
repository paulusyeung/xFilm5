using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;

namespace xFilm5.REST.Controllers
{
    public class OrderController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 5, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>vwPrintQueueVpsList_Ordered json object list</returns>
        [HttpGet]
        [Route("api/Order/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                if (ctx.vwPrintQueueVpsList_Ordered.Where(x => x.OrderHeaderId == id).Any())
                {
                    var list = ctx.vwPrintQueueVpsList_Ordered.Where(x => x.OrderHeaderId == id).OrderBy(x => x.VpsFileName).ToList();
                    return Json(list);
                }
            }

            return null;
        }

        [HttpGet]
        [Route("api/Order/ByMonth/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrderByMonth(int id, DateTime date)
        {
            var month = date.ToString("yyyy-MM");
            if (id == 0)
            {
                #region All distinct clients within the same month
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
select [OrderID]
      ,[ClientID]
      ,[ClientName]
      ,[ClientStatus]
      ,[PriorityID]
      ,[Priority]
      ,[Attachment]
      ,[Remarks]
      ,[DateReceived]
      ,[DateCompleted]
      ,[OrderTypeID]
      ,[OrderType]
      ,[OrderedBy]
      ,[PrePressOp]
      ,[RetouchBy]
      ,[Workshop]
      ,[StatusID]
      ,[Status]
      ,[DeliveryMethod]
      ,[Comment]
	  from (
SELECT TOP 100 PERCENT [OrderID]
      ,[ClientID]
      ,[ClientName]
      ,[ClientStatus]
      ,[PriorityID]
      ,[Priority]
      ,[Attachment]
      ,[Remarks]
      ,[DateReceived]
      ,[DateCompleted]
      ,[OrderTypeID]
      ,[OrderType]
      ,[OrderedBy]
      ,[PrePressOp]
      ,[RetouchBy]
      ,[Workshop]
      ,[StatusID]
      ,[Status]
      ,[DeliveryMethod]
      ,[Comment]
	  ,row_number() OVER (partition BY [ClientName] order by [ClientName], [OrderID]) as Ln
FROM [xFilm3_NuStar].[dbo].[vwOrderList]
where (OrderTypeID >= 6) AND (substring([DateReceived], 1, 7) = '{0}')
) as oops
where Ln = 1
order by [ClientName]", month);

                    var list = ctx.Database.SqlQuery<vwOrderList>(qry).ToList();
                    return Json(list);
                }
                #endregion
            }
            else
            {
                #region All orders of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    var list = ctx.vwOrderList.Where(x => x.DateReceived.StartsWith(month) && x.ClientID == id && x.OrderTypeID >= 6).OrderBy(x => x.OrderID).ToList();
                    return Json(list);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Order/ByKeyword/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrderByKeyword(String keyword)
        {
            if ((keyword != "") && (keyword.Length >= 3))
            {
                #region All
                using (var ctx = new xFilmEntities())
                {
                    var list = ctx.vwOrderList.Where(x => x.OrderTypeID >= 6 
                        && (x.OrderID.ToString().Contains(keyword) || x.ClientID.ToString().Contains(keyword) || x.ClientName.Contains(keyword) || x.DateReceived.Contains(keyword) || x.Remarks.Contains(keyword)))
                        .OrderBy(x => x.OrderID).ToList();
                    return Json(list);
                }
                #endregion
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/Order/pdf/{id:int}")]
        [JwtAuthentication]
        public HttpResponseMessage GetOrderPdf(int id)
        {   // ref: https://stackoverflow.com/questions/36042614/how-to-return-a-pdf-from-a-web-api-application
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (var ctx = new EF6.xFilmEntities())
            {
                var hasrow = ctx.OrderHeader.Where(x => x.ID == id).Any();
                if (hasrow)
                {
                    var docName = String.Format("Order_{0}.pdf", id.ToString());
                    var list = ctx.vwOrderPkPrintQueueVpsList.Where(x => x.OrderHeaderId == id).OrderBy(x => x.VpsFileName).ToList();

                    Reports.Order rptOrder = new Reports.Order();
                    rptOrder.DataSource = list;
                    //rptOrder.ClientId = clientId;
                    rptOrder.CreateDocument();

                    MemoryStream memStream = new System.IO.MemoryStream();
                    //rptOrder.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                    rptOrder.ExportToPdf(memStream);

                    byte[] buffer = new byte[0];
                    buffer = memStream.GetBuffer();
                    var contentLength = buffer.Length;

                    //200
                    //successful
                    var statuscode = HttpStatusCode.OK;
                    response = Request.CreateResponse(statuscode);
                    response.Content = new StreamContent(new MemoryStream(buffer));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentLength = contentLength;

                    ContentDispositionHeaderValue contentDisposition = null;
                    if (ContentDispositionHeaderValue.TryParse("inline; filename=" + docName, out contentDisposition))
                    {
                        response.Content.Headers.ContentDisposition = contentDisposition;
                    }
                }
                else
                {
                    var message = String.Format("Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                    HttpError err = new HttpError(message);
                    response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                }
            }

            /**
            var documents = reader.GetDocument(id);
            if (documents != null && documents.Length == 1)
            {
                var document = documents[0];
                id = document.docid;
                byte[] buffer = new byte[0];

                //generate pdf document
                MemoryStream memoryStream = new MemoryStream();
                MyPDFGenerator.New().PrintToStream(document, memoryStream);

                //get buffer
                buffer = memoryStream.GetBuffer();

                //content length for use in header
                var contentLength = buffer.Length;

                //200
                //successful
                var statuscode = HttpStatusCode.OK;
                response = Request.CreateResponse(statuscode);
                response.Content = new StreamContent(new MemoryStream(buffer));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = contentLength;

                ContentDispositionHeaderValue contentDisposition = null;
                if (ContentDispositionHeaderValue.TryParse("inline; filename=" + document.Name + ".pdf", out contentDisposition))
                {
                    response.Content.Headers.ContentDisposition = contentDisposition;
                }
            }
            else
            {
                var message = String.Format("Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                HttpError err = new HttpError(message);
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
            }
            */

            return response;
        }

        [HttpGet]
        [Route("api/Order/Available/Plate/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailablePlate(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
                else
                {
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("api/Order/Available/Film/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailableFilm(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    if (ctx.vwPrintQueueVpsList_AvailableFilm.Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailableFilm.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
                else
                {
                    if (ctx.vwPrintQueueVpsList_AvailableFilm.Where(x => x.ClientID == id).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailableFilm.Where(x => x.ClientID == id).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("api/Order/Available/Blueprint/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailableBlueprint(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.BlueprintOrdered == false).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.BlueprintOrdered == false).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
                else
                {
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id && x.BlueprintOrdered == false).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id && x.BlueprintOrdered == false).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                }
            }

            return null;
        }

        [HttpPost]
        [Route("api/Order/Placing/Plate/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostPlacingOrder_Plate(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            var json = Request.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<List<vwPrintQueueVpsList_AvailablePlate>>(json);

            int clientId;
            clientId = Int32.TryParse(id, out clientId) ? clientId : 0;
            clientId = (clientId == 0) ? items[0].ClientID : clientId;

            using (var ctx = new xFilmEntities())
            {
                var sid = Guid.Parse(userSid);
                var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                if (user != null)
                {
                    /**
                    var receiptId = ReceiptHelper.SaveReceipt(items, user.UserId, CommonHelper.Enums.PaymentType.OnAccount);
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
                    */
                }
            }
            return NotFound();
        }
    }
}
