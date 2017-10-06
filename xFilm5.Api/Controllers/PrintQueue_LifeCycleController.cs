using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class PrintQueue_LifeCycleController : ApiController
    {
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
    }
}
