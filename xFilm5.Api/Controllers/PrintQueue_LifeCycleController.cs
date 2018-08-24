using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class PrintQueue_LifeCycleController : ApiController
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(PrintQueue_LifeCycleController));

        [HttpGet]
        [Route("api/PrintQueue_LifeCycle/Counter/Plate/{workshop}/")]
        public IHttpActionResult GetCounterPlate(String workshop)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var count = ctx.vwPrintQueue_LifeCycleListWithWorkshop.Where(x => x.PrintQSubitemType == (int)EnumHelper.Order.PrintQSubitemType.Plate && x.Workshop.StartsWith(workshop) && DbFunctions.TruncateTime(x.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                dynamic plates = new ExpandoObject();
                plates.Count = count;
                return Json(JsonConvert.SerializeObject(plates));
            }
        }

        [HttpGet]
        [Route("api/PrintQueue_LifeCycle/Counter/Blueprint/{workshop}/")]
        public IHttpActionResult GetCounterBlueprint(String workshop)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var count = ctx.vwPrintQueue_LifeCycleListWithWorkshop.Where(x => x.PrintQSubitemType == (int)EnumHelper.Order.PrintQSubitemType.Blueprint && x.Workshop.StartsWith(workshop) && DbFunctions.TruncateTime(x.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                dynamic bp = new ExpandoObject();
                bp.Count = count;
                return Json(JsonConvert.SerializeObject(bp));
            }
        }

        [HttpGet]
        [Route("api/PrintQueue_LifeCycle/{printQueueId:int}/")]
        public IHttpActionResult GetPrintQueueVps(int printQueueId)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var pQueue = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueId == printQueueId).OrderBy(x => x.CreatedOn).ToList();
                return Json(pQueue);
            }
        }

        [HttpPost]
        [Route("api/PrintQueue_LifeCycle/")]
        public IHttpActionResult PostPrintQueue_LifeCycle([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[api, PrintQueue_LifeCycle] jsonData == null");
                return NotFound();
            }
            else
            {
                try
                {
                    var item = jsonData.ToObject<PrintQueue_LifeCycle>();

                    using (var ctx = new xFilmEntities())
                    {
                        var cycle = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueId == item.PrintQueueId && x.PrintQueueVpsId == item.PrintQueueVpsId && x.PrintQSubitemType == item.PrintQSubitemType).SingleOrDefault();
                        if (cycle == null)
                        {
                            ctx.PrintQueue_LifeCycle.Add(item);
                            ctx.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            log.Error("[api, PostPrintQueue_LifeCycle duplicated] \r\n" + jsonData.ToString());
                            return NotFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("[api, PostPrintQueue_LifeCycle exception error] \r\n" + ex);
                    return NotFound();
                }
            }
        }
    }
}
