using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Bot.Helper
{
    public class UserHelper
    {
        /// <summary>
        /// refer to Common.Enums.UserRole
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetSecurityLevel(int userId)
        {
            int result = 0;

            using (var ctx = new EF6.xFilmEntities())
            {
                var cUser = ctx.Client_User.Where(x => x.ID == userId).SingleOrDefault();
                if (cUser != null)
                {
                    result = (int)cUser.SecurityLevel;
                }
            }

            return result;
        }

        public static Guid GetUserSid(int userId)
        {
            Guid result = Guid.Empty;

            using (var ctx = new EF6.xFilmEntities())
            {
                var cUser = ctx.User.Where(x => x.UserId == userId).SingleOrDefault();
                if (cUser != null)
                {
                    result = cUser.UserSid;
                }
            }

            return result;
        }

        public static String GetUserEmail(int userId)
        {
            var result = "";

            using (var ctx = new EF6.xFilmEntities())
            {
                var user = ctx.Client_User.Where(x => x.ID == userId).SingleOrDefault();
                if (user != null)
                {
                    result = new EmailAddressAttribute().IsValid(user.Email) ? user.Email : "";
                }
            }

            return result;
        }

        /// <summary>
        /// Return the user's device ids delimitted by ','
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static String GetUserMobileDeviceTokens(int userId)
        {
            var result = "";

            using (var ctx = new EF6.xFilmEntities())
            {
                var items = ctx.vwUserNotificationList
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.DeviceId).ThenBy(x => x.NotifyType)
                    //.Select(x => x.DeviceId)
                    //.Distinct()
                    .ToList();
                if (items.Count > 0)
                {
                    List<string> recipient = new List<string>();
                    //List<string> deviceList = new List<string>();       // 2018.03.14 paulus: log 邊個 device 會收到 FCM
                    //List<string> useridList = new List<string>();       // 2018.03.14 paulus: log 邊個 user 會收到 FCM

                    for (int i = 0; i < items.Count; i++)
                    {
                        dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(items[i].AuthXml);
                        if (expando != null)
                        {
                            if (!(recipient.Any(x => x.Contains(expando.FCM.Token))))
                                recipient.Add(expando.FCM.Token);
                        }
                    }
                    result = String.Join(",", recipient.Where(x => x != String.Empty).ToArray());
                }
            }
            return result;
        }
    }
}
