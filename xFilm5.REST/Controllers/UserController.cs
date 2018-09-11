using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;
using xFilm5.REST.Models;

namespace xFilm5.REST.Controllers
{
    public class UserController : ApiController
    {
        private xFilmEntities db = new xFilmEntities();

        /// <summary>
        /// GET: api/User
        /// usersid = 喺 Security Token 之內
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User")]
        [JwtAuthentication]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid)? userSid : Guid.Empty;

            var user = db.User.Where(x => x.UserSid == userSid).SingleOrDefault();
            if (user != null)
            {
                var userEx = new UserEx();
                userEx.UserId = user.UserId;
                userEx.UserType = user.UserType;
                userEx.UserSid = user.UserSid;
                userEx.LoginName = user.LoginName;
                userEx.LoginPassword = user.LoginPassword;
                userEx.Alias = user.Alias;
                userEx.Status = user.Status;
                userEx.CreatedOn = user.CreatedOn;
                userEx.CreatedBy = user.CreatedBy;
                userEx.ModifiedOn = user.ModifiedOn;
                userEx.ModifiedBy = user.ModifiedBy;
                userEx.Retired = user.Retired;
                userEx.RetiredOn = user.RetiredOn;
                userEx.RetiredBy = user.RetiredBy;
                userEx.UserRole = UserHelper.GetSecurityLevel(user.UserId);
                userEx.UserRoleName = ((CommonHelper.Enums.UserRole)userEx.UserRole).ToString();    // 2018.03.1 paulus: added
                userEx.UserAuth = null;
                userEx.UserNotification = null;
                userEx.UserPreference = null;

                return Json(userEx);
            }

            return NotFound();
        }

        /// <summary>
        /// GET: api/User
        /// usersid = 以 userkey 藏喺 QueryString 之內
        /// </summary>
        /// <param name="userkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/{userkey}")]
        [JwtAuthentication]
        public IHttpActionResult Get(string userkey)
        {
            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(userkey, out userSid) ? userSid : Guid.Empty;

            var user = db.User.Where(x => x.UserSid == userSid).SingleOrDefault();
            if (user != null)
            {
                var userEx = new UserEx();
                userEx.UserId = user.UserId;
                userEx.UserType = user.UserType;
                userEx.UserSid = user.UserSid;
                userEx.LoginName = user.LoginName;
                userEx.LoginPassword = user.LoginPassword;
                userEx.Alias = user.Alias;
                userEx.Status = user.Status;
                userEx.CreatedOn = user.CreatedOn;
                userEx.CreatedBy = user.CreatedBy;
                userEx.ModifiedOn = user.ModifiedOn;
                userEx.ModifiedBy = user.ModifiedBy;
                userEx.Retired = user.Retired;
                userEx.RetiredOn = user.RetiredOn;
                userEx.RetiredBy = user.RetiredBy;
                userEx.UserRole = UserHelper.GetSecurityLevel(user.UserId);

                return Json(userEx);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/User/Notification/{deviceid}")]
        [JwtAuthentication]
        public IHttpActionResult GetNotification(string deviceId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var firstitem = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId).FirstOrDefault();
                    if (firstitem != null)
                    {
                        // 直接用 MetadataXMl 會多咗啲 escape code，搞到個 MetadataXml 變成唔係 valid json，要先轉換一下
                        // refer: https://stackoverflow.com/questions/36241098/ihttpactionresult-with-json-string
                        var unserializedContent = JsonConvert.DeserializeObject(firstitem.MetadataXml);
                        return Json(unserializedContent);
                    }
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/User/Notification/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostNotification(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;

            #region 用 ExpandoObject 拆解個 json data
            //refer: http://www.tomdupont.net/2014/02/deserialize-to-expandoobject-with.html
            //var converter = new ExpandoObjectConverter();     用唔用真係冇分別嘅
            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);

            //string fcmToken = expando.FCM.Token;
            string deviceId = expando.DeviceInfo.Id;
            //string deviceModel = expando.DeviceInfo.Model;
            int platform = (int)expando.DeviceInfo.Platform;
            //string version = expando.DeviceInfo.Version;
            //DateTime registeredOn = ((DateTime)expando.RegisteredOn).ToLocalTime();

            dynamic options = expando.Options;
            #endregion

            if (expando != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                    if (user != null)
                    {
                        using (var scope = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                #region loop throught 所有 options
                                foreach (KeyValuePair<string, object> item in options)
                                {
                                    var notifyType = EnumHelper.User.NotifyType.None;
                                    notifyType = Enum.TryParse(item.Key, out notifyType) ? notifyType : EnumHelper.User.NotifyType.None;
                                    var ntype = (int)notifyType;

                                    if (notifyType != EnumHelper.User.NotifyType.None)
                                    {
                                        if ((bool)item.Value)
                                        {
                                            #region 選擇哩個短訊，創建 dbo.UserNotification record
                                            var updRec = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId && x.NotifyType == ntype).SingleOrDefault();
                                            if (updRec != null)
                                            {
                                                //updRec.UserId = user.UserId;
                                                //updRec.DeviceId = deviceId;
                                                //updRec.NotifyType = ntype;
                                                updRec.Platform = platform;
                                                updRec.MetadataXml = json;
                                                ctx.SaveChanges();
                                            }
                                            else
                                            {
                                                updRec = new UserNotification();
                                                updRec.UserId = user.UserId;
                                                updRec.DeviceId = deviceId;
                                                updRec.NotifyType = ntype;
                                                updRec.Platform = platform;
                                                updRec.MetadataXml = json;
                                                ctx.UserNotification.Add(updRec);
                                                ctx.SaveChanges();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 唔要哩個短訊，刪除 dbo.UserNotification record
                                            var delRec = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId && x.NotifyType == ntype).SingleOrDefault();
                                            if (delRec != null)
                                            {
                                                ctx.UserNotification.Remove(delRec);
                                                ctx.SaveChanges();
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                scope.Commit();
                                return Ok();
                            }
                            catch
                            {
                                scope.Rollback();
                            }
                        }
                    }
                }
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/User/Preference/{clientid}")]
        [JwtAuthentication]
        public IHttpActionResult GetPreference(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var firstitem = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId).FirstOrDefault();
                    if (firstitem != null)
                    {
                        // 直接用 MetadataXMl 會多咗啲 escape code，搞到個 MetadataXml 變成唔係 valid json，要先轉換一下
                        // refer: https://stackoverflow.com/questions/36241098/ihttpactionresult-with-json-string
                        var unserializedContent = JsonConvert.DeserializeObject(firstitem.MetadataXml);
                        return Json(unserializedContent);
                    }
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/User/Preference/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostPreference(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;

            #region 用 ExpandoObject 拆解個 json data
            //refer: http://www.tomdupont.net/2014/02/deserialize-to-expandoobject-with.html
            //var converter = new ExpandoObjectConverter();     用唔用真係冇分別嘅
            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);

            //string fcmToken = expando.FCM.Token;
            string deviceId = expando.DeviceInfo.Id;
            //string deviceModel = expando.DeviceInfo.Model;
            int platform = (int)expando.DeviceInfo.Platform;
            //string version = expando.DeviceInfo.Version;
            //DateTime registeredOn = ((DateTime)expando.RegisteredOn).ToLocalTime();

            dynamic options = expando.Options;
            #endregion

            if (expando != null)
            {
                using (var ctx = new xFilmEntities())
                {
                    var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                    if (user != null)
                    {
                        using (var scope = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                #region loop throught 所有 options
                                foreach (KeyValuePair<string, object> item in options)
                                {
                                    var notifyType = EnumHelper.User.NotifyType.None;
                                    notifyType = Enum.TryParse(item.Key, out notifyType) ? notifyType : EnumHelper.User.NotifyType.None;
                                    var ntype = (int)notifyType;

                                    if (notifyType != EnumHelper.User.NotifyType.None)
                                    {
                                        if ((bool)item.Value)
                                        {
                                            #region 選擇哩個短訊，創建 dbo.UserNotification record
                                            var updRec = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId && x.NotifyType == ntype).SingleOrDefault();
                                            if (updRec != null)
                                            {
                                                //updRec.UserId = user.UserId;
                                                //updRec.DeviceId = deviceId;
                                                //updRec.NotifyType = ntype;
                                                updRec.Platform = platform;
                                                updRec.MetadataXml = json;
                                                ctx.SaveChanges();
                                            }
                                            else
                                            {
                                                updRec = new UserNotification();
                                                updRec.UserId = user.UserId;
                                                updRec.DeviceId = deviceId;
                                                updRec.NotifyType = ntype;
                                                updRec.Platform = platform;
                                                updRec.MetadataXml = json;
                                                ctx.UserNotification.Add(updRec);
                                                ctx.SaveChanges();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 唔要哩個短訊，刪除 dbo.UserNotification record
                                            var delRec = ctx.UserNotification.Where(x => x.UserId == user.UserId && x.DeviceId == deviceId && x.NotifyType == ntype).SingleOrDefault();
                                            if (delRec != null)
                                            {
                                                ctx.UserNotification.Remove(delRec);
                                                ctx.SaveChanges();
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                scope.Commit();
                                return Ok();
                            }
                            catch
                            {
                                scope.Rollback();
                            }
                        }
                    }
                }
            }

            return NotFound();
        }
    }
}
