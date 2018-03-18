using xFilm5.EF6;
using xFilm5.REST.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace xFilm5.REST.Controllers
{
    public class FCMHistoryController : ApiController
    {
        private DateTime _DateZero = new DateTime(2017, 1, 1);
        private int _PageSize = 50;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FCMHistory json object list</returns>
        [HttpGet]
        [Route("api/FCMHistory")]
        [JwtAuthentication]
        public IHttpActionResult GetFCMHistory()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    //var items = ctx.FCMHistory.Where(x => x.Topic.ToLower() == "everyone" || x.Topic.ToLower() == "staffonly" || x.RecipientList.Contains(userSid.ToString())).ToList();
                    var items = from f in ctx.FCMHistory
                            where (f.DeliveredOn >= DbFunctions.AddDays(DateTime.Now, -30)) &&
                            (
                            f.Topic.Equals("everyone", StringComparison.OrdinalIgnoreCase) ||
                            f.Topic.Equals("staffonly", StringComparison.OrdinalIgnoreCase) ||
                            f.UserIdList.Contains(userSid.ToString())
                            )
                            orderby f.DeliveredOn descending
                            select f;
                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns>FCMHistory json object list</returns>
        [HttpGet]
        [Route("api/FCMHistory/page/{page:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetFCMHistoryByPage(int page)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    var max = page * _PageSize;
                    //var items = ctx.FCMHistory.Where(x => x.Topic.ToLower() == "everyone" || x.Topic.ToLower() == "staffonly" || x.RecipientList.Contains(userSid.ToString())).ToList();
                    var items = (from f in ctx.FCMHistory
                                where (f.DeliveredOn >= DbFunctions.AddDays(DateTime.Now, -30)) &&
                                (
                                f.Topic.Equals("everyone", StringComparison.OrdinalIgnoreCase) ||
                                f.Topic.Equals("staffonly", StringComparison.OrdinalIgnoreCase) ||
                                f.UserIdList.Contains(userSid.ToString())
                                )
                                orderby f.DeliveredOn descending
                                select f).Take (max);
                    if (items.Count() > 0)
                    {
                        return Json(items.ToList());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FCMHistory json object list</returns>
        [HttpGet]
        [Route("api/FCMHistory/{id:int}")]
        [JwtAuthentication]
        public IHttpActionResult GetFCMHistory(int id)
        {
            using (var ctx = new xFilmEntities())
            {
                var item = ctx.FCMHistory.Where(x => x.FCMHistoryId == id).SingleOrDefault();
                if (item != null)
                {
                    return Json(item);
                }
            }

            return null;
        }
    }
}
