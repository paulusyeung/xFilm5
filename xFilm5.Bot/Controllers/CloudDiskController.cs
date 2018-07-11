using Hangfire;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.Bot.Helper;
using xFilm5.EF6;

namespace xFilm5.Bot.Controllers
{
    [RoutePrefix("CloudDisk")]
    public class CloudDiskController : ApiController
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(BotController));

        [HttpPost]
        [Route("MigrateFile/{clientId:int}/{userId:int}/")]
        public IHttpActionResult PostMigrateFile(int clientId, int userId)
        {
            using (var ctx = new xFilmEntities())
            {
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    BackgroundJob.Enqueue(() => CloudDiskHelper.MigrateFiles(clientId, userId));

                    log.Info(String.Format("[bot, CloudDisk, MigrateFile] \r\nHangfire accepted the Job\r\nClient Id = {0}", clientId.ToString()));

                    return StatusCode(HttpStatusCode.Accepted);     // 202 or use: return new StatusCodeResult(202);
                }
            }

            log.Info(String.Format("[bot, CloudDisk, MigrateFile] \r\nError found before submitting to Hangfire\r\nClient Id = {0}", clientId.ToString()));

            return BadRequest();
        }

        [HttpPost]
        [Route("CreateClient/{clientId:int}/{userId:int}/")]
        public IHttpActionResult PostCreateClient(int clientId, int userId)
        {
            using (var ctx = new xFilmEntities())
            {
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    BackgroundJob.Enqueue(() => CloudDiskHelper.CreateClient(clientId, userId));

                    log.Info(String.Format("[bot, CloudDisk, CreateClient] \r\nHangfire accepted the Job\r\nClient Id = {0}", clientId.ToString()));

                    return StatusCode(HttpStatusCode.Accepted);     // 202 or use: return new StatusCodeResult(202);
                }
            }

            log.Info(String.Format("[bot, CloudDisk, CreateClient] \r\nError found before submitting to Hangfire\r\nClient Id = {0}", clientId.ToString()));

            return BadRequest();
        }
    }
}
