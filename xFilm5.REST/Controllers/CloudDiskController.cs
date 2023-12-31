﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Controllers
{
    [RoutePrefix("api/CloudDisk")]
    public class CloudDiskController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 1, 1);
        private int _PageSize = 50;

        #region Get Cups, Cip3, Vps, Blueprint, Plate, Film, Thumbnail
        [HttpGet]
        [Route("cups/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetCups(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetCups(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("cups/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetCups(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetCups(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("cip3/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetCip3(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetCip3(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("cip3/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetCip3(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetCip3(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("vps/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetVps(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetVps(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("vps/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetVps(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetVps(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("Blueprint/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetBlueprint(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetBlueprint(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("Blueprint/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetBlueprint(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetBlueprint(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("plate/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetPlate(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetPlate(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("plate/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetPlate(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetPlate(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("film/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetFilm(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetFilm(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("film/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetFilm(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetFilm(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("thumbnail/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetThumbnail(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetThumbnail(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("thumbnail/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetThumbnail(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetThumbnail(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("tools/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetTools(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetTools(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("tools/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetTools(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetTools(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("speedbox/{clientId:int}/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetSpeedBox(int clientId, int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetSpeedBox(clientId, page);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("speedbox/keyword/{clientId:int}/{keyword}")]
        [JwtAuthentication]
        public IHttpActionResult GetSpeedBox(int clientId, String keyword)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetSpeedBox(clientId, keyword);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }
        #endregion

        [HttpGet]
        [Route("users/subadmin/{workshop}")]
        [JwtAuthentication]
        public IHttpActionResult GetSubAdminUsers(String workshop)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var items = BotHelper.GetSubAdminUsers(workshop);

                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        [HttpGet]
        [Route("thumbnail/{clientid:int}/{filename}/{width:int}/{height:int}")]
        [JwtAuthentication]
        public HttpResponseMessage GetThumbnail(int clientId, string filename, int width = 100, int height = 100)
        {
            using (var ctx = new xFilmEntities())
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

                var raw = BotHelper.GetThumbnail(clientId, filename, width, height);
                if (raw != null)
                {
                    var contentLength = raw.Length;

                    //200
                    //successful
                    var statuscode = HttpStatusCode.OK;
                    response = Request.CreateResponse(statuscode);
                    response.Content = new StreamContent(new MemoryStream(raw));
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
                return response;
            }
        }

        [HttpPost]
        [Route("Action/Email/{clientId:int}")]
        [JwtAuthentication]
        public IHttpActionResult PostActionEmail(int clientId)
        {
            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionEmail>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        if (!(String.IsNullOrEmpty(data.Recipient)))
                        {
                            var result = BotHelper.PostCloudDiskActionEmail(data, clientId);
                            if (result) return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Action/Reprint/{clientId:int}")]
        [JwtAuthentication]
        public IHttpActionResult PostActionReprint(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionReprintEx>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                        if (user != null)
                        {
                            var result = OrderHelper.CloudDiskRePrint(data, user.UserId);
                            if (result != 0) return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Action/Output/Blueprint/{clientId:int}")]
        [JwtAuthentication]
        public IHttpActionResult PostActionOutputBlueprint(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionOutputEx>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                        if (user != null)
                        {
                            List<Models.CloudDisk.ResourceInfoEx> newItems = new List<Models.CloudDisk.ResourceInfoEx>();
                            foreach (var item in data.Items)
                            {
                                #region replace the selected item with VPS files
                                var original_pqVpsList = ctx.vwPrintQueueVpsList_Ordered
                                    .Where(x => x.VpsFileName.Contains(item.Name) && x.CheckedBlueprint == true)
                                    .OrderBy(x => x.OrderPkPrintQueueVpsId)
                                    .ToList();
                                if (original_pqVpsList.Count > 0)
                                {
                                    foreach (var original_pqVpItem in original_pqVpsList)
                                    {
                                        var newItem = new Models.CloudDisk.ResourceInfoEx();
                                        newItem.Name = original_pqVpItem.VpsFileName;
                                        newItem.Path = item.Path;
                                        newItem.ETag = item.ETag;
                                        newItem.ContentType = item.ContentType;
                                        newItem.LastModified = item.LastModified;
                                        newItem.Created = item.Created;
                                        newItem.QuotaUsed = item.QuotaUsed;
                                        newItem.QuotaAvailable = item.QuotaAvailable;
                                        newItem.Size = item.Size;
                                        newItems.Add(newItem);
                                    }
                                }
                                #endregion
                            }
                            data.Items = newItems;

                            //var result = BotHelper.PostCloudDiskActionOutput_Blueprint(data, clientId, user.UserId);
                            var result = OrderHelper.CloudDiskReOutput_Blueprint(data, user.UserId);
                            if (result != 0) return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Action/Output/Plate/{clientId:int}")]
        [JwtAuthentication]
        public IHttpActionResult PostActionOutputPlate(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;
            Models.CloudDisk.ActionOutputEx data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionOutputEx>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                        if (user != null)
                        {
                            //var result = BotHelper.PostCloudDiskActionOutput_Plate(data, clientId, user.UserId);
                            var result = OrderHelper.CloudDiskReOutput_Plate(data, user.UserId);
                            if (result != 0) return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Action/Output/Film/{clientId:int}")]
        [JwtAuthentication]
        public IHttpActionResult PostActionOutputFilm(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Models.CloudDisk.ActionOutputEx>(json);

            if (data != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                        if (user != null)
                        {
                            //var result = BotHelper.PostCloudDiskActionOutput_Film(data, clientId, user.UserId);
                            var result = OrderHelper.CloudDiskReOutput_Film(data, user.UserId);
                            if (result != 0) return Ok();
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("speedbox/upload/{clientId:int}")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> PostSpeedBoxUpload(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                if (client != null)
                {
                    try
                    {
                        HttpRequestMessage request = this.Request;
                        if (!request.Content.IsMimeMultipartContent())
                        {
                            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                        }
                        var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());     // upload file 暫存 temporaray folder，唔使理 permission

                        var task = await request.Content.ReadAsMultipartAsync(provider).
                            ContinueWith<HttpResponseMessage>(o =>
                            {
                                /** 唔使 loop，暫時得一個 file
                                foreach (MultipartFileData file in provider.FileData)
                                {
                                    var filename = file.Headers.ContentDisposition.FileName;
                                    var localFilePath = "Server file path: " + file.LocalFileName;
                                }
                                */

                                if (provider.Contents.Count > 0)
                                {
                                    var tmpFileName = provider.FileData.First().LocalFileName;
                                    var orgFileName = provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                                    #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                                    String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                                    String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                                    String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];
                                    String speedBox_HotFolder = ConfigurationManager.AppSettings["SpeedBox_HotFolder"];
                                    String speedBox_TempFolder = ConfigurationManager.AppSettings["SpeedBox_TempFolder"];

                                    String tempPath = serverUri + speedBox_TempFolder;
                                    String tempFileName = String.Format("{0}.{1}", clientId.ToString(), orgFileName);
                                    String filePath = Path.Combine(tempPath, tempFileName);

                                    //var uri = new Uri(serverUri);
                                    //System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                                    #endregion

                                    #region 抄去 shared folder on server 然後叫 Bot Server 做嘢
                                    using (new Impersonation(serverUri, userName, userPassword))
                                    {
                                        if (!(Directory.Exists(tempPath)))
                                            Directory.CreateDirectory(tempPath);
                                        if (File.Exists(filePath))
                                            File.Delete(filePath);
                                        File.Move(tmpFileName, filePath);

                                        BotHelper.PostSpeedBox(clientId, filePath, orgFileName);    // 通知 Bot Server 跟進
                                    }
                                    #endregion

                                    return Request.CreateResponse(HttpStatusCode.OK, string.Format("File {0} uploaded...{1}", orgFileName, tmpFileName));
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...No file found");
                                }
                            }
                        );
                        return task;
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...Client not found");
            }
        }
    }
}
