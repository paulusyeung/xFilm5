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
        private DateTime _DateZero = new DateTime(2017, 6, 1);

        [HttpGet]
        [Route("api/Order/ByMonth/{id:int}/{date:DateTime}")]
        [JwtAuthentication]
        public IHttpActionResult GetOrderByMonth(int id, DateTime date)
        {
            var month = date.ToString("yyyy-MM");
            if (id == 0)
            {
                #region All clients
                using (var ctx = new xFilmEntities())
                {
                    var list = ctx.vwOrderList.Where(x => x.DateReceived.Substring(0, 7) == month && x.OrderTypeID >= 6).OrderBy(x => x.OrderID).ToList();
                    return Json(list);
                }
                #endregion
            }
            else
            {
                #region A single client
                using (var ctx = new xFilmEntities())
                {
                    var list = ctx.vwOrderList.Where(x => x.DateReceived.Substring(0, 7) == month && x.ClientID == id && x.OrderTypeID >= 6).OrderBy(x => x.OrderID).ToList();
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
