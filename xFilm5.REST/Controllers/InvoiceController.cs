﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;

namespace xFilm5.REST.Controllers
{
    public class InvoiceController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 5, 1);

        [HttpGet]
        [Route("api/Invoice/ByMonth/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public IHttpActionResult GetInvoiceByMonth(int id, DateTime date)
        {
            var month = date.ToString("yyyy-MM");
            if (id == 0)
            {
                #region All distinct clients within the same month
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
select [ClientId]
      ,[ClientName]
      ,[ClientStatus]
      ,[InvoiceID]
      ,[InvoiceNumber]
      ,[InvoiceDate]
      ,[OsAmount]
      ,[InvoiceAmount]
      ,[Status]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[OrderID]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
from (
SELECT [ClientId]
      ,[ClientName]
      ,[ClientStatus]
      ,[InvoiceID]
      ,[InvoiceNumber]
      ,[InvoiceDate]
      ,[OsAmount]
      ,[InvoiceAmount]
      ,[Status]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[OrderID]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
	  ,row_number() OVER (partition BY ClientName ORDER BY ClientName, InvoiceNumber) AS Ln
FROM [xFilm3_NuStar].[dbo].[vwInvoiceList_All]
where ([ClientStatus] = 1) and ([InvoiceDate] >= '{0}') and (convert(nvarchar(7), InvoiceDate, 120) = '{1}')
) as opps
where Ln = 1
order by [ClientName]", _DateZero.ToString("yyyy-MM-dd"), month);

                    var list = ctx.Database.SqlQuery<vwInvoiceList_All>(qry).ToList();
                    return Json(list);
                }
                #endregion
            }
            else
            {
                #region All orders of a single client within the same month
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
SELECT TOP (1000) [ClientId]
      ,[ClientName]
      ,[ClientStatus]
      ,[InvoiceID]
      ,[InvoiceNumber]
      ,[InvoiceDate]
      ,[OsAmount]
      ,[InvoiceAmount]
      ,[Status]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[OrderID]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
FROM [xFilm3_NuStar].[dbo].[vwInvoiceList_All]
where ([ClientStatus] = 1) and ([InvoiceDate] >= '{0}') and (ClientId = {1}) and (convert(nvarchar(7), InvoiceDate, 120) = '{2}')
order by [InvoiceNumber]", _DateZero.ToString("yyyy-MM-dd"), id.ToString(), month);

                    var list = ctx.Database.SqlQuery<vwInvoiceList_All>(qry).ToList();
                    return Json(list);
                }
                #endregion
            }
        }

        [HttpGet]
        [Route("api/Invoice/ByKeyword/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetInvoiceByKeyword(String keyword)
        {
            if ((keyword != "") && (keyword.Length >= 3))
            {
                #region All
                using (var ctx = new xFilmEntities())
                {
                    string qry = String.Format(@"
SELECT TOP (1000) [ClientId]
      ,[ClientName]
      ,[ClientStatus]
      ,[InvoiceID]
      ,[InvoiceNumber]
      ,[InvoiceDate]
      ,[OsAmount]
      ,[InvoiceAmount]
      ,[Status]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[OrderID]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
FROM [xFilm3_NuStar].[dbo].[vwInvoiceList_All]
where ([ClientStatus] = 1) and [InvoiceDate] >= '{0}' and ((convert(nvarchar, ClientId) like '%{1}%') or (ClientName like '%{1}%') or (InvoiceNumber like '%{1}%' or (convert(nvarchar(10), InvoiceDate, 120) like '%{1}%')))
order by [InvoiceNumber]", _DateZero.ToString("yyyy-MM-dd"), keyword);

                    var list = ctx.Database.SqlQuery<vwInvoiceList_All>(qry).ToList();

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
        [Route("api/Invoice/pdf/{id:int}")]
        [JwtAuthentication]
        public HttpResponseMessage GetInvoicePdf(int id)
        {   // ref: https://stackoverflow.com/questions/36042614/how-to-return-a-pdf-from-a-web-api-application
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (var ctx = new EF6.xFilmEntities())
            {
                var hasrow = ctx.Acct_INMaster.Where(x => x.ID == id).Any();
                if (hasrow)
                {
                    var docName = String.Format("Invoice_{0}.pdf", id.ToString());
                    var list = ctx.vwInv5DetailsList.Where(x => x.InvoiceHeaderId == id).OrderBy(x => x.ItemDescription).ToList();

                    Reports.Invoice rptInvoice = new Reports.Invoice();
                    rptInvoice.DataSource = list;
                    //rptOrder.ClientId = clientId;
                    rptInvoice.CreateDocument();

                    MemoryStream memStream = new System.IO.MemoryStream();
                    //rptOrder.ExportOptions.Pdf.NeverEmbeddedFonts = "MingLiU;Microsoft YaHei";
                    rptInvoice.ExportToPdf(memStream);

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
    }
}