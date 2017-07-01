using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
