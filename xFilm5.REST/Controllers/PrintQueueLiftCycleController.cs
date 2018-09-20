using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;

namespace xFilm5.REST.Controllers
{
    public class PrintQueueLiftCycleController : ApiController
    {
        [HttpGet]
        [Route("api/PrintQueueVps/LifeCycle/{printQueueVpsId:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetPrintQueueVps_LifeCycle(int printQueueVpsId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var pqVps = ctx.vwPrintQueueVpsList.Where(x => x.VpsPrintQueueID == printQueueVpsId).First();
                    if (pqVps != null)
                    {
                        var items = ctx.vwPrintQueue_LifeCycleList
                            .Where(x => x.CupsJobID == pqVps.CupsJobID && (x.PrintQSubitemType <= 3 || x.PrintQueueVpsId == printQueueVpsId))
                            .OrderBy(x => x.CreatedOn)
                            .ToList();

                        return Json(items);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }

            return null;
        }
    }
}
