﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;
using xFilm5.REST.Models;

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
        [Route("api/Order/ByMonth/{id:int}/{date:DateTime}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrderByMonth(int id, DateTime date, string workshop = null)
        {
            var month = date.ToString("yyyy-MM");
            if (id == 0)
            {
                #region All distinct clients within the same month
                using (var ctx = new xFilmEntities())
                {
                    #region 睇吓使唔使 filter by workshop
                    var addFilter = false;
                    if (!(String.IsNullOrEmpty(workshop)))
                    {
                        //如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order
                        addFilter = ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any();
                    }
                    #endregion

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
FROM [dbo].[vwOrderList]
where (OrderTypeID >= 6) AND (substring([DateReceived], 1, 7) = '{0}')
) as oops
where Ln = 1
order by [ClientName]", month);

                    if (addFilter)
                    {
                        var list = ctx.Database.SqlQuery<vwOrderList>(qry).Where(x => x.Workshop == workshop).ToList();
                        return Json(list);
                    }
                    else
                    {
                        var list = ctx.Database.SqlQuery<vwOrderList>(qry).ToList();
                        return Json(list);
                    }
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

        /// <summary>
        /// All orders of a single client within the same month
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="workshop"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Order/ByMonth/excel/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public HttpResponseMessage GetOrderByMonthAsExcel(int id, DateTime date)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    var month = date.ToString("yyyy-MM");

                    var list = ctx.vwOrderList.Where(x => x.DateReceived.StartsWith(month) && x.ClientID == id && x.OrderTypeID >= 6).OrderBy(x => x.OrderID).ToList();
                    if (list.Count > 0)
                    {
                        var docName = String.Format("Order_{0}.xlsx", id.ToString());

                        var wb = ClosedXmlHelper.GetOrderListAsExcel(list);
                        MemoryStream memStream = new System.IO.MemoryStream();
                        wb.SaveAs(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        //200
                        //successful
                        var statuscode = HttpStatusCode.OK;
                        response = Request.CreateResponse(statuscode);
                        response.Content = new StreamContent(memStream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        response.Content.Headers.ContentLength = memStream.Length;

                        ContentDispositionHeaderValue contentDisposition = null;
                        if (ContentDispositionHeaderValue.TryParse("attachment; filename=" + docName, out contentDisposition))
                        {
                            response.Content.Headers.ContentDisposition = contentDisposition;
                        }
                    }
                    else
                    {
                        var message = String.Format("GetOrderByMonthAsExcel: Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                        HttpError err = new HttpError(message);
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = String.Format("GetOrderByMonthAsExcel: Exceptional error.\r\n{0}", ex.ToString());
                HttpError err = new HttpError(message);
                response = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, err);
            }

            return response;
        }

        [HttpGet]
        [Route("api/Order/ByKeyword/{id:int}/{keyword}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrderByKeyword(int id, String keyword, string workshop = null)
        {
            if ((keyword != "") && (keyword.Length >= 3))
            {
                #region All
                using (var ctx = new xFilmEntities())
                {
                    if (id == 0)
                    {
                        #region Staff, all Client
                        #region 睇吓使唔使 filter by workshop
                        var addFilter = false;
                        if (!(String.IsNullOrEmpty(workshop)))
                        {
                            //如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order
                            addFilter = ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any();
                        }
                        #endregion

                        if (addFilter)
                        {
                            var list = ctx.vwOrderList.Where(x => x.OrderTypeID >= 6 && x.Workshop == workshop
                                && (x.OrderID.ToString().Contains(keyword) || x.ClientID.ToString().Contains(keyword) || x.ClientName.Contains(keyword) || x.DateReceived.Contains(keyword) || x.Remarks.Contains(keyword)))
                                .OrderBy(x => x.OrderID).ToList();
                            return Json(list);
                        }
                        else
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
                        #region Client, one client
                        var list = ctx.vwOrderList.Where(x => x.OrderTypeID >= 6
                            && x.ClientID == id
                            && (x.OrderID.ToString().Contains(keyword) || x.ClientName.Contains(keyword) || x.DateReceived.Contains(keyword) || x.Remarks.Contains(keyword)))
                            .OrderBy(x => x.OrderID).ToList();
                        return Json(list);
                        #endregion
                    }
                }
                #endregion
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/Order/ByKeyword/excel/{id:int}/{keyword}")]
        [JwtAuthentication]
        public HttpResponseMessage GetOrderByKeywordAsExcel(int id, String keyword)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    var list = ctx.vwOrderList.Where(x => x.OrderTypeID >= 6 && x.ClientID == id
                        && (x.OrderID.ToString().Contains(keyword) || x.DateReceived.Contains(keyword) || x.Remarks.Contains(keyword)))
                        .OrderBy(x => x.OrderID).ToList();
                    if (list.Count > 0)
                    {
                        var docName = String.Format("Order_{0}.xlsx", id.ToString());

                        var wb = ClosedXmlHelper.GetOrderListAsExcel(list);
                        MemoryStream memStream = new System.IO.MemoryStream();
                        wb.SaveAs(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        //200
                        //successful
                        var statuscode = HttpStatusCode.OK;
                        response = Request.CreateResponse(statuscode);
                        response.Content = new StreamContent(memStream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        response.Content.Headers.ContentLength = memStream.Length;

                        ContentDispositionHeaderValue contentDisposition = null;
                        if (ContentDispositionHeaderValue.TryParse("attachment; filename=" + docName, out contentDisposition))
                        {
                            response.Content.Headers.ContentDisposition = contentDisposition;
                        }
                    }
                    else
                    {
                        var message = String.Format("GetOrderByKeywordAsExcel: Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                        HttpError err = new HttpError(message);
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = String.Format("GetOrderByKeywordAsExcel: Exceptional error.\r\n{0}", ex.ToString());
                HttpError err = new HttpError(message);
                response = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, err);
            }

            return response;
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
        [Route("api/Order/Available/Plate/{id:int}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailablePlate(int id, string workshop = null)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    #region Staff
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Any())
                    {
                        if (String.IsNullOrEmpty(workshop))
                        {
                            var list = ctx.vwPrintQueueVpsList_AvailablePlate.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                            return Json(list);
                        }
                        else
                        {
                            #region 如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order；否則就 return 所有 workshops 嘅 orders
                            if (ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any())
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.DefaultWorkshopName == workshop).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            else
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailablePlate.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Client
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                    #endregion
                }
            }

            return null;
        }

        [HttpGet]
        [Route("api/Order/Available/Film/{id:int}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailableFilm(int id, string workshop = null)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    #region Staff
                    if (ctx.vwPrintQueueVpsList_AvailableFilm.Any())
                    {
                        if (String.IsNullOrEmpty(workshop))
                        {
                            var list = ctx.vwPrintQueueVpsList_AvailableFilm.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                            return Json(list);
                        }
                        else
                        {
                            #region 如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order；否則就 return 所有 workshops 嘅 orders
                            if (ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any())
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailableFilm.Where(x => x.DefaultWorkshopName == workshop).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            else
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailableFilm.OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Client
                    if (ctx.vwPrintQueueVpsList_AvailableFilm.Where(x => x.ClientID == id).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailableFilm.Where(x => x.ClientID == id).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                    #endregion
                }
            }

            return null;
        }

        [HttpGet]
        [Route("api/Order/Available/Blueprint/{id:int}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrder_AvailableBlueprint(int id, string workshop = null)
        {
            using (var ctx = new xFilmEntities())
            {
                if (id == 0)
                {
                    #region Staff
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.BlueprintOrdered == false).Any())
                    {
                        if (String.IsNullOrEmpty(workshop))
                        {
                            var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.BlueprintOrdered == false).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                            return Json(list);
                        }
                        {
                            #region 如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order；否則就 return 所有 workshops 嘅 orders
                            if (ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any())
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.DefaultWorkshopName == workshop && x.BlueprintOrdered == false).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            else
                            {
                                var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.BlueprintOrdered == false).OrderBy(x => x.ClientName).ThenBy(x => x.VpsFileName).ToList();
                                return Json(list);
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Client
                    if (ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id && x.BlueprintOrdered == false).Any())
                    {
                        var list = ctx.vwPrintQueueVpsList_AvailablePlate.Where(x => x.ClientID == id && x.BlueprintOrdered == false).OrderBy(x => x.VpsFileName).ToList();
                        return Json(list);
                    }
                    #endregion
                }
            }

            return null;
        }

        [HttpPost]
        [Route("api/Order/Place/Plate/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostPlaceOrder_Plate(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            try
            {
                var json = Request.Content.ReadAsStringAsync().Result;
                //dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);
                var data = JsonConvert.DeserializeObject<OrderFormData_Plate>(json);

                using (var ctx = new xFilmEntities())
                {
                    var sid = Guid.Parse(userSid);
                    var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                    if (user != null)
                    {
                        if (data.Items.Count > 0)
                        {
                            var orderId = OrderHelper.PlaceOrder_Plate(data, user.UserId);

                            if (orderId != 0)
                            {
                                var feedback = ctx.vwOrderList.Where(x => x.OrderID == orderId).SingleOrDefault();
                                return Ok(feedback);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/Order/Place/Blueprint/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostPlaceOrder_Blueprint(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            try
            {
                var json = Request.Content.ReadAsStringAsync().Result;
                //dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);
                var data = JsonConvert.DeserializeObject<OrderFormData_Blueprint>(json);

                using (var ctx = new xFilmEntities())
                {
                    var sid = Guid.Parse(userSid);
                    var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                    if (user != null)
                    {
                        if (data.Items.Count > 0)
                        {
                            var orderId = OrderHelper.PlaceOrder_Blueprint(data, user.UserId);

                            if (orderId != 0)
                            {
                                var feedback = ctx.vwOrderList.Where(x => x.OrderID == orderId).SingleOrDefault();
                                return Ok(feedback);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/Order/Place/Flim/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostPlaceOrder_Flim(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userSid = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

            try
            {
                var json = Request.Content.ReadAsStringAsync().Result;
                //dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);
                var data = JsonConvert.DeserializeObject<OrderFormData_Film>(json);

                using (var ctx = new xFilmEntities())
                {
                    var sid = Guid.Parse(userSid);
                    var user = ctx.User.Where(x => x.UserSid == sid).SingleOrDefault();
                    if (user != null)
                    {
                        if (data.Items.Count > 0)
                        {
                            var orderId = OrderHelper.PlaceOrder_Film(data, user.UserId);

                            if (orderId != 0)
                            {
                                var feedback = ctx.vwOrderList.Where(x => x.OrderID == orderId).SingleOrDefault();
                                return Ok(feedback);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return NotFound();
        }
    }
}
