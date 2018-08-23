using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(OrderPkPrintQueneVpsController));

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

        [HttpPost]
        [Route("api/OrderPq/IsReady")]
        public IHttpActionResult PostOrderPqIsReady([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[api, PostOrderPqIsReady] jsonData == null");
                return NotFound();
            }
            else
            {
                try
                {
                    var item = jsonData.ToObject<OrderPkPrintQueueVps>();

                    using (var ctx = new xFilmEntities())
                    {
                        var orderPqVps = ctx.OrderPkPrintQueueVps.FirstOrDefault(v => v.OrderPkPrintQueueVpsId == item.OrderPkPrintQueueVpsId);
                        if (orderPqVps != null)
                        {
                            orderPqVps.IsReady = true;
                            ctx.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            log.Error("[api, OrderPkPrintQueueVps not found] \r\n" + item.ToString());
                            return NotFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("[api, PostOrderPqIsReady exception error] \r\n" + ex);
                    return NotFound();
                }
            }
        }

    }
}
