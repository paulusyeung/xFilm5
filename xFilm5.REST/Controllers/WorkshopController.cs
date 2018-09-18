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
    public class WorkshopController : ApiController
    {
        [HttpGet]
        [Route("api/Workshop")]
        //[JwtAuthentication]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //Guid userSid = Guid.Empty;
            //userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //var items = ctx.Client_User.Where(x => x.SecurityLevel == 6).OrderBy(x => x.FullName).ToList();
                var wkshop = ctx.vwWorkshopList.OrderBy(x => x.WorkshopName).ToList();
                return Json(wkshop);
            }

            return NotFound();
        }
    }
}
