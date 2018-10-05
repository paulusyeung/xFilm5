using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class UserHelper
    {
        public enum UserType
        {
            Staff,
            Customer,
            Supplier
        }

        /// <summary>
        /// 每個 dbo.Client_User 配一個 dbo.User + 一個 dbo.UserPreferences
        /// dbo.User.UserId 同 dbo.UserPreferences.UserId 會採用 dbo.Client_User.ID
        /// </summary>
        /// <param name="clientUserId"></param>
        /// <returns></returns>
        public static bool Migrate2UserEx(int clientUserId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                using (var scope = ctx.Database.BeginTransaction())
                {
                    try
                    {   // 如果 dbo.User 冇 record 先要 migrate
                        if (!(ctx.User.Where(x => x.UserId == clientUserId).Any()))
                        {
                            var cUser = ctx.Client_User.Where(x => x.ID == clientUserId).SingleOrDefault();
                            if (cUser != null)
                            {
                                #region 先 save dbo.User
                                /** EF6 won't send the UserId if using User.Add && SaveChanges, needs to do it using ExecuteSqlCommand
                                var u = new EF6.User();
                                u.UserId = clientUserId;
                                u.UserType = CommonHelper.Config.IamStaff ? (int)UserType.Staff : (int)UserType.Customer;
                                u.UserSid = Guid.NewGuid();
                                u.Alias = cUser.FullName;
                                u.LoginName = cUser.Email;
                                u.LoginPassword = cUser.Password;

                                u.Status = (int)CommonHelper.Enums.Status.Active;
                                u.CreatedOn = DateTime.Now;
                                u.CreatedBy = CommonHelper.Config.CurrentUserId;
                                u.ModifiedOn = DateTime.Now;
                                u.ModifiedBy = CommonHelper.Config.CurrentUserId;
                                u.Retired = false;
                                */
                                // force entity framework to insert identity columns
                                // 參考: http://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns
                                // 留意要改埋 EF6.User.UserId.StoreGeneratedPattern
                                var userType = CommonHelper.Config.IamStaff ? (int)UserType.Staff : (int)UserType.Customer;
                                var sql = String.Format(@"
INSERT [dbo].[User]([UserId], [UserType], [UserSid], [LoginName], [LoginPassword], [Alias], [Status], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Retired], [RetiredOn], [RetiredBy])
VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', {6}, '{7}', {8}, '{9}', {10}, {11}, '{12}', {13})"
, clientUserId.ToString()                                   // UserId
, userType.ToString()                                       // UserType
, Guid.NewGuid()                                            // UserSid
, cUser.Email                                               // LoginName
, cUser.Password                                            // LoginPassword
, cUser.FullName                                            // Alias
, ((int)CommonHelper.Enums.Status.Active).ToString()        // Status
, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")              // CreatedOn
, CommonHelper.Config.CurrentUserId.ToString()              // CreatedBy
, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")              // ModifiedOn
, CommonHelper.Config.CurrentUserId.ToString()              // ModifiedBy
, 0                                                         // Retired = false
, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")              // RetiredOn
, "0"                                                       // RetiredBy
);
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON ");
                                ctx.Database.ExecuteSqlCommand(sql);
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF ");
                                #endregion

                                #region 再 save dbo.UserPreferences
                                var up = new EF6.UserPreference();
                                var defaults = UserExHelper.Preferences.GetDefaults();

                                up.UserId = clientUserId;
                                up.ObjectId = UserExHelper.USEREX_OBJECTID;
                                up.ObjectType = (int)CommonHelper.Enums.ObjectType.UserExInfo;
                                up.MetadataXml = JsonConvert.SerializeObject(defaults, Formatting.Indented);
                                ctx.UserPreference.Add(up);
                                ctx.SaveChanges();
                                #endregion

                                /**
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON ");
                                ctx.Database.ExecuteSqlCommand(sql);
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF ");
                                ctx.User.Add(u);
                                ctx.UserPreference.Add(up);
                                ctx.SaveChanges();
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF ");
                                */

                                scope.Commit();

                                result = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        scope.Rollback();
                    }
                }
            }

            return result;
        }

        public static bool IsUserExist(int userId)
        {
            bool result = false;

            DAL.User user = DAL.User.Load(userId);
            if (user != null)
                result = true;

            return result;
        }

        public static bool IsUserMigrated(int clientUserId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                result = ctx.User.Where(x => x.UserId == clientUserId).Any();
            }

            return result;
        }

        public static bool IsPrimaryUser(int clientUserId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                try
                {
                    var u = ctx.Client_User.Where(x => x.ID == clientUserId).SingleOrDefault();
                    if (u != null)
                    {
                        result = u.PrimaryUser;
                    }
                }
                catch { }
            }

            return result;
        }

        public static int GetClientId(int userId)
        {
            int result = 0;

            using (var ctx = new EF6.xFilmEntities())
            {
                var u = ctx.Client_User.Where(x => x.ID == userId).SingleOrDefault();
                if (u != null)
                    {
                    result = u.ClientID;
                }
            }

            return result;
        }

        public static UserType GetUserType(int clientUserId)
        {
            UserType result = UserType.Staff;

            using (var ctx = new EF6.xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserId == clientUserId).SingleOrDefault();
                if (user != null)
                {   // migrate 咗就會有 dbo.User record
                    result = (UserType)user.UserType;
                }
                else
                {   // 未 migrate 就要用番 dbo.Client_User record
                    var cUser = ctx.Client_User.Where(x => x.ID == clientUserId).SingleOrDefault();
                    if (cUser != null)
                    {
                        switch (cUser.SecurityLevel)
                        {
                            case 1:
                                result = UserType.Customer;
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                result = UserType.Staff;
                                break;
                        }
                    }
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
