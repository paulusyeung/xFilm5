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

        [HttpPost]
        [Route("ApiCupsUploadFile/{cupsJobTitle}/")]
        public IHttpActionResult PostApiCupsUploadFile(string cupsJobTitle)
        {
            using (var ctx = new xFilmEntities())
            {
                BackgroundJob.Enqueue(() => CloudDiskHelper.ApiCupsUploadFile(cupsJobTitle));

                log.Info(String.Format("[bot, CloudDisk, ApiCupsUploadFile] \r\nHangfire accepted the Job\r\nCupJobTitle = {0}", cupsJobTitle));

                return StatusCode(HttpStatusCode.Accepted);     // 202 or use: return new StatusCodeResult(202);
            }

            log.Info(String.Format("[bot, CloudDisk, ApiCupsUploadFile] \r\nError found before submitting to Hangfire\r\nCupJobTitle = {0}", cupsJobTitle));

            return BadRequest();
        }

        [HttpPost]
        [Route("ApiVpsUploadFile/{vpsFileName}/")]
        public IHttpActionResult PostApiVpsUploadFile(string vpsFileName)
        {
            using (var ctx = new xFilmEntities())
            {
                BackgroundJob.Enqueue(() => CloudDiskHelper.ApiVpsUploadFile(vpsFileName));

                log.Info(String.Format("[bot, CloudDisk, ApiVpsUploadFile] \r\nHangfire accepted the Job\r\nVpsFileName = {0}", vpsFileName));

                return StatusCode(HttpStatusCode.Accepted);     // 202 or use: return new StatusCodeResult(202);
            }

            log.Info(String.Format("[bot, CloudDisk, ApiVpsUploadFile] \r\nError found before submitting to Hangfire\r\nVpsFileName = {0}", vpsFileName));

            return BadRequest();
        }

        [HttpPost]
        [Route("ApiCip3UploadFile/{cip3FileName}/")]
        public IHttpActionResult PostApiCip3UploadFile(string cip3FileName)
        {
            using (var ctx = new xFilmEntities())
            {
                BackgroundJob.Enqueue(() => CloudDiskHelper.ApiCip3UploadFile(cip3FileName));

                log.Info(String.Format("[bot, CloudDisk, ApiCip3UploadFile] \r\nHangfire accepted the Job\r\nCip3FileName = {0}", cip3FileName));

                return StatusCode(HttpStatusCode.Accepted);     // 202 or use: return new StatusCodeResult(202);
            }

            log.Info(String.Format("[bot, CloudDisk, ApiCip3UploadFile] \r\nError found before submitting to Hangfire\r\nCip3FileName = {0}", cip3FileName));

            return BadRequest();
        }
    }
}
