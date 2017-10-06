using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class ClientController : ApiController
    {
        [HttpGet]
        [Route("api/Client/{id:int}")]
        public IHttpActionResult GetClient(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var client = ctx.Client.Where(x => x.ID == id).SingleOrDefault();
                return Json(client);
            }
        }
    }
}
