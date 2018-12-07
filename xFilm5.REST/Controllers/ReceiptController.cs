using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Controllers
{
    public class ReceiptController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 5, 1);

        [HttpGet]
        [Route("api/Receipt/ByDay/{id:int}/{date:DateTime}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetReceiptByDay(int id, DateTime date, string workshop = null)
        {
            var month = date.ToString("yyyy-MM-dd");
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
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (convert(nvarchar(10), ReceiptDate , 120) = '{0}')
  ) as oops
  where Ln = 1
  order by [ReceiptNumber]", month);

                    if (addFilter)
                    {
                        var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).Where(x => x.WorkshopName == workshop).ToList();
                        return Json(list);
                    }
                    else
                    {
                        var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
                        return Json(list);
                    }
                }
                #endregion
            }
            else
            {
                #region All receipts of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (ClientId = {0}) and (convert(nvarchar(10), ReceiptDate , 120) = '{1}')
  ) as oops
  where Ln = 1
  order by [ReceiptNumber]", id.ToString(), month);
                    var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
                    return Json(list);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Receipt/ByMonth/{id:int}/{date:DateTime}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetReceiptByMonth(int id, DateTime date, string workshop = null)
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
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ClientName] order by [ClientName], [ReceiptNumber]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (convert(nvarchar(7), ReceiptDate , 120) = '{0}')
  ) as oops
  where Ln = 1
  order by [ClientName]", month);

                    if (addFilter)
                    {
                        var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).Where(x => x.WorkshopName == workshop).ToList();
                        return Json(list);
                    }
                    else
                    {
                        var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
                        return Json(list);
                    }
                }
                #endregion
            }
            else
            {
                #region All receipts of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (ClientId = {0}) and (convert(nvarchar(7), ReceiptDate , 120) = '{1}')
  ) as oops
  where Ln = 1
  order by [ReceiptNumber]", id.ToString(), month);
                    var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
                    return Json(list);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Receipt/ByMonth/excel/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public HttpResponseMessage GetReceiptByMonthAsExcel(int id, DateTime date)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    var month = date.ToString("yyyy-MM");

                    #region construct query string: qry
                    string qry = String.Format(@"
select [Ln], [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (ClientId = {0}) and (convert(nvarchar(7), ReceiptDate , 120) = '{1}')
  ) as oops
  --where Ln = 1
  order by [ReceiptNumber]", id.ToString(), month);
                    #endregion

                    var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();

                    if (list.Count > 0)
                    {
                        var docName = String.Format("Receipt_{0}.xlsx", id.ToString());

                        var wb = ClosedXmlHelper.GetReceiptListAsExcel(list);
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
                        var message = String.Format("GetReceiptByMonthAsExcel: Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                        HttpError err = new HttpError(message);
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = String.Format("GetReceiptByMonthAsExcel: Exceptional error.\r\n{0}", ex.ToString());
                HttpError err = new HttpError(message);
                response = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, err);
            }

            return response;
        }

        [HttpGet]
        [Route("api/Receipt/ByKeyword/{id:int}/{keyword}/{workshop?}")]
        [JwtAuthentication]
        public IHttpActionResult GetReceiptByKeyword(int id, String keyword, string workshop = null)
        {
            if ((keyword != "") && (keyword.Length >= 3))
            {
                #region All
                using (var ctx = new xFilmEntities())
                {
                    if (id == 0)
                    {
                        #region Staff, all client

                        #region 睇吓使唔使 filter by workshop
                        var addFilter = false;
                        if (!(String.IsNullOrEmpty(workshop)))
                        {
                            //如果 workshop 係 exist 嘅，淨係 return 同一個 workshop 嘅 order
                            addFilter = ctx.vwWorkshopList.Where(x => x.WorkshopName == workshop).Any();
                        }
                        #endregion

                        var qry = String.Format(@"
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (convert(nvarchar, [ReceiptNumber]) like '%{0}%') or (convert(nvarchar(7), ReceiptDate , 120) like '%{0}%' or [ClientName] like '%{0}%' or (convert(nvarchar, ClientId) like '%{0}%') or (convert(nvarchar(10), ReceiptDate, 120) like '%{0}%') or convert(nvarchar, [OrderHeaderId]) like '%{0}%')
  ) as oops
  where Ln = 1
  order by [ReceiptNumber]", keyword);

                        if (addFilter)
                        {
                            var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).Where(x => x.WorkshopName == workshop).ToList();
                            return Json(list);
                        }
                        else
                        {
                            var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
                            return Json(list);
                        }
                        #endregion
                    }
                    else
                    {
                        #region client, one client
                        var qry = String.Format(@"
select [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (ClientId = {0}) and ((convert(nvarchar, [ReceiptNumber]) like '%{1}%') or (convert(nvarchar(7), ReceiptDate , 120) like '%{1}%' or [ClientName] like '%{1}%' or  (convert(nvarchar(10), ReceiptDate, 120) like '%{1}%') or convert(nvarchar, [OrderHeaderId]) like '%{1}%'))
  ) as oops
  where Ln = 1
  order by [ReceiptNumber]", id.ToString(), keyword);
                        var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();
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
        [Route("api/Receipt/ByKeyword/excel/{id:int}/{keyword}")]
        [JwtAuthentication]
        public HttpResponseMessage GetReceiptByKeywordAsExcel(int id, String keyword)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    #region construct query string: qry
                    string qry = String.Format(@"
select [Ln], [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
from (
SELECT TOP 100 PERCENT [ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[ClientUserId]
      ,[ClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
      ,[OrderedOn]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[PrintQueueVpsId]
      ,[CheckedPlate]
      ,[CheckedCip3]
      ,[CheckedBlueprint]
      ,[IsReady]
      ,[IsReceived]
      ,[IsBilled]
      ,[WorkshopId]
      ,[WorkshopName]
	  ,row_number() OVER (partition BY [ReceiptNumber] order by [ReceiptNumber], [ItemDescription]) as Ln
  FROM [dbo].[vwReceiptDetailsList_Ex]
  where (ClientId = {0}) and ((convert(nvarchar, [ReceiptNumber]) like '%{1}%') or (convert(nvarchar(7), ReceiptDate , 120) like '%{1}%' or (convert(nvarchar(10), ReceiptDate, 120) like '%{1}%') or convert(nvarchar, [OrderHeaderId]) like '%{1}%'))
  ) as oops
  --where Ln = 1
  order by [ReceiptNumber]", id.ToString(), keyword);
                    #endregion

                    var list = ctx.Database.SqlQuery<vwReceiptDetailsList_Ex>(qry).ToList();

                    if (list.Count > 0)
                    {
                        var docName = String.Format("Receipt_{0}.xlsx", id.ToString());

                        var wb = ClosedXmlHelper.GetReceiptListAsExcel(list);
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
                        var message = String.Format("GetReceiptByKeywordAsExcel: Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                        HttpError err = new HttpError(message);
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = String.Format("GetReceiptByKeywordAsExcel: Exceptional error.\r\n{0}", ex.ToString());
                HttpError err = new HttpError(message);
                response = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, err);
            }

            return response;
        }

        [HttpGet]
        [Route("api/Receipt/pdf/{id:int}")]
        [JwtAuthentication]
        public HttpResponseMessage GetReceiptPdf(int id)
        {   // ref: https://stackoverflow.com/questions/36042614/how-to-return-a-pdf-from-a-web-api-application
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (var ctx = new EF6.xFilmEntities())
            {
                var hasrow = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == id).Any();
                if (hasrow)
                {
                    var docName = String.Format("Receipt_{0}.pdf", id.ToString());
                    //var list = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == id).OrderBy(x => x.OrderPkPrintQueueVpsId).ToList();
                    var list = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == id).OrderBy(x => x.ItemDescription).ToList();

                    Reports.Receipt rptReceipt = new Reports.Receipt();
                    rptReceipt.ReceiptId = id;
                    rptReceipt.DataSource = list;
                    rptReceipt.CreateDocument();

                    MemoryStream memStream = new System.IO.MemoryStream();
                    //rptOrder.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                    rptReceipt.ExportToPdf(memStream);

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

            return response;
        }
    }
}
