using Hangfire;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using xFilm5.Bot.Helper;
using xFilm5.EF6;

namespace xFilm5.Bot.Controllers
{
    [RoutePrefix("CloudDisk")]
    public class CloudDiskController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 1, 1);
        private int _PageSize = 50;

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

        #region Get Cups, Cip3, Vps, Blueprint, Plate, Film, thumbnail
        [HttpGet]
        [Route("cups/{clientId:int}/{page:int}/")]
        public  IHttpActionResult GetCups(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/cups";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path):                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);    // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("cups/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetCups(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/cups";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("cip3/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetCip3(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/cip3";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);    // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("cip3/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetCip3(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/cip3";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("vps/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetVps(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/vps";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);    // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("vps/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetVps(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/vps";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("blueprint/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetBlueprint(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/blueprint";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);    // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("blueprint/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetBlueprint(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/blueprint";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("plate/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetPlate(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/plate";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);     // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("plate/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetPlate(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/plate";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("film/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetFilm(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/film";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);     // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("film/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetFilm(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/film";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("thumbnail/{clientId:int}/{page:int}/")]
        public IHttpActionResult GetThumbnail(int clientId, int page)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/thumbnail";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = page == 0 ?
                        CloudDiskHelper.FileLst(clientId, 0, path) :                   // all
                        CloudDiskHelper.FileLst(clientId, page * _PageSize, path);     // pages
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("thumbnail/keyword/{clientId:int}/{keyword}/")]
        public IHttpActionResult GetThumbnail(int clientId, String keyword)
        {
            using (var ctx = new xFilmEntities())
            {
                var path = "/thumbnail";
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status > 0).SingleOrDefault();
                if (client != null)
                {
                    var list = (CloudDiskHelper.FileLst(clientId, 0, path)).Where(x => x.Name.Contains(keyword)).ToList();
                    if (list != null)
                        return Json(list);
                }
            }
            return null;
        }
        #endregion

        [HttpGet]
        [Route("users/subadmin/{workshop}/")]
        public IHttpActionResult GetSubAdminUsers(string workshop)
        {
            var items = CloudDiskHelper.GetSubAdminUsers();
            if (items.Count > 0)
            {
                using (var ctx = new xFilmEntities())
                {
                    if (workshop.ToLower() == "all")
                    {
                        var all = ctx.vwClientList.Where(c => items.Contains(c.ID.ToString())).OrderBy(x => x.Name).ToList();
                        return Json(all);
                    }
                    else
                    {
                        var list = ctx.vwClientList.Where(c => items.Contains(c.ID.ToString()) && c.BranchName == workshop).OrderBy(x => x.Name).ToList();
                        return Json(list);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// if width or height = 0, return full size image
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="filename"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("thumbnail/{clientId:int}/{filename}/{width:int}/{height:int}")]
        public HttpResponseMessage GetThumbnail(int clientId, string filename, int width = 100, int height = 100)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (var ctx = new xFilmEntities())
            {
                //ctx.Configuration.LazyLoadingEnabled = false;
                var client = ctx.vwClientList.Where(x => x.ID == clientId).SingleOrDefault();
                if (client != null)
                {
                    var img = CloudDiskHelper.Thumbnail(clientId, filename);
                    if (img != null)
                    {
                        var suffix = "png";
                        var image = Image.FromStream(img);

                        //var thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                        var thumb = width == 0 || height == 0 ? image : ImageHelper.FixedSize(image, width, height);
                        var buffer = ImageHelper.ImageToByteArray(thumb, suffix);

                        var contentLength = buffer.Length;

                        //200
                        //successful
                        var statuscode = HttpStatusCode.OK;
                        response = Request.CreateResponse(statuscode);
                        response.Content = new StreamContent(new MemoryStream(buffer));
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                        response.Content.Headers.ContentLength = contentLength;

                        ContentDispositionHeaderValue contentDisposition = null;
                        if (ContentDispositionHeaderValue.TryParse("inline; filename=" + filename, out contentDisposition))
                        {
                            response.Content.Headers.ContentDisposition = contentDisposition;
                        }
                    }
                    else
                    {
                        var message = String.Format("Unable to find resource. Resource \"{0}\" may not exist.", filename);
                        HttpError err = new HttpError(message);
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            return response;
        }


        [HttpPost]
        [Route("Action/Email/{clientId:int}")]
        public IHttpActionResult PostActionEmail(int clientId)
        {
            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionEmail>(json);

            if (data == null)
            {
                log.Error("[bot, CloudDisk, PostSenActionEmail] jsonData == null");
                //return NotFound();
            }
            else
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        if (!(String.IsNullOrEmpty(data.Recipient)))
                        {
                            var result = CloudDiskHelper.ActionEmail(data, clientId);

                            if (result)
                            {
                                log.Info(String.Format("[bot, CloudDisk, PostSenActionEmail] \r\njsondata = {0}", json));
                                return Ok();
                            }
                            else
                            {
                                log.Error(String.Format("[bot, CloudDisk, PostSenActionEmail] \r\nEmailer return false\r\njsondat = {0}", json));
                            }
                        }
                        else
                        {
                            log.Error("[bot, CloudDisk, PostSenActionEmail] \r\nInvaoid Recipient IsNullOrEmpty");
                            //return NotFound();
                        }
                    }
                    else
                    {
                        log.Error("[bot, CloudDisk, PostSenActionEmail] \r\nInvaoid Client Id: " + clientId.ToString());
                        //return NotFound();
                    }
                }
            }
            return NotFound();
        }
    }
}
