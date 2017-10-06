using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class OrderPkPrintQueneVpsController : ApiController
    {
        [HttpGet]
        [Route("api/OrderPq/Plate/{id:int}")]
        public IHttpActionResult GetOrderPq_Plate(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var orderPq = ctx.OrderPkPrintQueueVps.Where(x => x.PrintQueueVpsId == id && x.CheckedPlate == true).FirstOrDefault();
                return Json(orderPq);
            }
        }

        /// <summary>
        /// 數下同一張 Order 有幾多 Plate
        /// </summary>
        /// <param name="orderHeaderid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/OrderPq/Plate/Count/{orderHeaderid:int}")]
        public IHttpActionResult GetOrderPq_Plate_Count(int orderHeaderid)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var count = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == orderHeaderid && x.CheckedPlate == true).Count();
                dynamic result = new ExpandoObject();
                result.Count = count;

                return Json(result);
            }
        }

        /// <summary>
        /// 數下同一張 Order 有幾多 Plate 已經 IsReady
        /// </summary>
        /// <param name="orderHeaderid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/OrderPq/Plate/IsReady/Count/{orderHeaderid:int}")]
        public IHttpActionResult GetOrderPq_Plate_IsReadyCount(int orderHeaderid)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var count = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == orderHeaderid && x.CheckedPlate == true && x.IsReady == true).Count();
                dynamic result = new ExpandoObject();
                result.IsReadyCount = count;

                return Json(result);
            }
        }

        [HttpGet]
        [Route("api/OrderPq/Blueprint/{id:int}")]
        public IHttpActionResult GetOrderPq_Blueprint(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var orderPq = ctx.OrderPkPrintQueueVps.Where(x => x.PrintQueueVpsId == id && x.CheckedBlueprint == true).FirstOrDefault();
                return Json(orderPq);
            }
        }
    }
}
