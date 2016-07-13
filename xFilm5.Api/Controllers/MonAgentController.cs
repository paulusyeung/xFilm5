using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace xFilm5.Api.Controllers
{
    public class MonAgentController : ApiController
    {
        [Route("api/cups")]
        public IHttpActionResult PostCups(int clientId, String pJobId, String pJobTitle)
        {
            if (clientId != 202020)
                return NotFound();
            else
                return Ok();
        }

        [Route("api/vps")]
        public IHttpActionResult PostVps(int clientId, String pJobId, String pJobTitle)
        {
            if (clientId != 202020)
                return NotFound();
            else
                return Ok();
        }
    }
}
