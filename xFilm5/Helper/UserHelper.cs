using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

                                // force entity framework to insert identity columns
                                // 參考: http://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns
                                // 留意要改埋 EF6.User.UserId.StoreGeneratedPattern
                                /**
                                using (var scope = ctx.Database.BeginTransaction())
                                {
                                    ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON ");
                                    ctx.User.Add(u);
                                    ctx.SaveChanges();
                                    ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF ");
                                    scope.Commit();
                                }
                                */
                                #endregion

                                #region 再 save dbo.UserPreferences
                                var up = new EF6.UserPreference();
                                var defaults = UserExHelper.Preferences.GetDefaults();

                                up.UserId = clientUserId;
                                up.ObjectId = UserExHelper.USEREX_OBJECTID;
                                up.ObjectType = (int)CommonHelper.Enums.ObjectType.UserExInfo;
                                up.MetadataXml = JsonConvert.SerializeObject(defaults, Formatting.Indented);
                                #endregion

                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON ");
                                ctx.User.Add(u);
                                ctx.UserPreference.Add(up);
                                ctx.SaveChanges();
                                ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF ");
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
    }
}
