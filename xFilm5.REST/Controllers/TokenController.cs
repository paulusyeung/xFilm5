using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Controllers
{
    public class TokenController : ApiController
    {
        /**
        /// <summary>
        /// 將 username + password 放在 header 內
        /// </summary>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token")]
        public string Get()
        {
            string user = "", pwd = "";
            var req = Request;
            var hdr = req.Headers;
            if ((hdr.Contains("username")) && (hdr.Contains("password")))
            {
                user = hdr.GetValues("username").First();
                pwd = hdr.GetValues("password").First();

                var oUser = CheckUser(user, pwd);
                if (oUser != null)
                {
                    return JwtManager.GenerateToken(oUser.UserSid.ToString());
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 將 username + password 放在 querystring 內
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token/{username}/{password}")]
        public string Get(string username, string password)
        {
            var oUser = CheckUser(username, password);
            if (oUser != null)
            {
                return JwtManager.GenerateToken(oUser.UserSid.ToString());
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        */

        /// <summary>
        /// 將 username + password 放在 header 內
        /// </summary>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token/Staff")]
        public string GetStaff()
        {
            string user = "", pwd = "";
            var req = Request;
            var hdr = req.Headers;
            if ((hdr.Contains("username")) && (hdr.Contains("password")))
            {
                user = hdr.GetValues("username").First();
                pwd = hdr.GetValues("password").First();

                var oUser = CheckUser(user, pwd);
                if (oUser != null)
                {
                    if (oUser.UserType == (int)CommonHelper.Enums.UserType.Staff)
                    {
                        return JwtManager.GenerateToken(oUser.UserSid.ToString());
                    }
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 將 username + password 放在 querystring 內
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token/Staff/{username}/{password}")]
        public string GetStaff(string username, string password)
        {
            var oUser = CheckUser(username, password);
            if (oUser != null)
            {
                if (oUser.UserType == (int)CommonHelper.Enums.UserType.Staff)
                {
                    return JwtManager.GenerateToken(oUser.UserSid.ToString());
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 將 username + password 放在 header 內
        /// </summary>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token/Client")]
        public string GetClient()
        {
            string user = "", pwd = "";
            var req = Request;
            var hdr = req.Headers;
            if ((hdr.Contains("username")) && (hdr.Contains("password")))
            {
                user = hdr.GetValues("username").First();
                pwd = hdr.GetValues("password").First();

                var oUser = CheckUser(user, pwd);
                if (oUser != null)
                {
                    if (oUser.UserType == (int)CommonHelper.Enums.UserType.Customer)
                    {
                        return JwtManager.GenerateToken(oUser.UserSid.ToString());
                    }
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 將 username + password 放在 querystring 內
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>token</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Token/Client/{username}/{password}")]
        public string GetClient(string username, string password)
        {
            var oUser = CheckUser(username, password);
            if (oUser != null)
            {
                if (oUser.UserType == (int)CommonHelper.Enums.UserType.Customer)
                {
                    return JwtManager.GenerateToken(oUser.UserSid.ToString());
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        private EF6.User CheckUser(string username, string password)
        {
            EF6.User result = null;

            using (var ctx = new EF6.xFilmEntities())
            {
                var user = ctx.User.FirstOrDefault(x => (x.LoginName == username) && (x.LoginPassword == password) && (x.Status >= 1));
                if (user != null)
                {
                    result = user;
                }
            }

            return result;
        }
    }
}
