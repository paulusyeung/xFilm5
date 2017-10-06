using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class PrintQueueVpsController : ApiController
    {
        [HttpGet]
        [Route("api/PrintQueueVps/{pqVpsId:int}/")]
        public IHttpActionResult GetPrintQueueVps(int pqVpsId)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var pQueue = ctx.PrintQueue_VPS.Where(x => x.ID == pqVpsId).SingleOrDefault();
                return Json(pQueue);
            }
        }

        [HttpGet]
        [Route("api/PrintQueueVps/{pqId:int}/{vpsFileName}")]
        public IHttpActionResult GetPrintQueueVps(int pqId, string vpsFileName)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var pQueue = ctx.PrintQueue_VPS.Where(x => x.PrintQueueID == pqId && x.VpsFileName.Contains(vpsFileName)).FirstOrDefault();
                return Json(pQueue);
            }
        }
    }
}
