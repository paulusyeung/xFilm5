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
    public class ClientController : ApiController
    {
        /// <summary>
        /// GET: api/Client/User
        /// usersid = 喺 Security Token 之內
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Client/User")]
        [JwtAuthentication]
        public IHttpActionResult GetClientUser()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var clientUser = ctx.vwClientUserList.Where(x => x.UserId == user.UserId).SingleOrDefault();
                    if (clientUser != null)
                    {
                        return Json(clientUser);
                    }
                }
            }

            return NotFound();
        }
    }
}
