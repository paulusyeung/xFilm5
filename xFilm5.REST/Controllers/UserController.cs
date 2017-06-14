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
    }
}
