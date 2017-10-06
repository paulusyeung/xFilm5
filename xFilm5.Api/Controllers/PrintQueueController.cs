using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class PrintQueueController : ApiController
    {
        [HttpGet]
        [Route("api/PrintQueue/{clientId:int}/{cupsJobId}")]
        public IHttpActionResult GetPrintQueue(int clientId, string cupsJobId)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var pQueue = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == cupsJobId).SingleOrDefault();
                return Json(pQueue);
            }
        }
    }
}
