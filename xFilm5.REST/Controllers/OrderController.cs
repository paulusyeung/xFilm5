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
    public class OrderController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 5, 1);

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
    }
}
