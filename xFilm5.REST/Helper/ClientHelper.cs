using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.REST.Helper
{
    public class ClientHelper
    {
        public static bool IsReceiptSlip(int clientId)
        {
            bool result = true;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Receipt != null ? p.Receipt.Slip : false;
                    }
                }
            }

            return result;
        }

        public static bool IsReceiptEmail(int clientId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Receipt != null ? p.Receipt.Email : false;
                    }
                }
            }

            return result;
        }

        public static bool IsReceiptGrouping(int clientId)
        {
            bool result = true;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Receipt != null ? p.Receipt.Grouping : false;
                    }
                }
            }

            return result;
        }

        public static bool IsReceiptSmallFont(int clientId)
        {
            bool result = true;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Receipt != null ? p.Receipt.SmallFont : false;
                    }
                }
            }

            return result;
        }

        public static bool IsNotifyByEmail(int clientId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Notification != null ? p.Notification.Email : false;
                    }
                }
            }

            return result;
        }

        public static bool IsNotifyByMobileApp(int clientId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Notification != null ? p.Notification.MobileApp : false;
                    }
                }
            }

            return result;
        }

        public static bool IsNotifynOnOrder(int clientId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Notification != null ? p.Notification.OnOrder : false;
                    }
                }
            }

            return result;
        }

        public static bool IsNotifyOnReady(int clientId)
        {
            bool result = false;

            using (var ctx = new EF6.xFilmEntities())
            {
                var pUser = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (pUser != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == pUser.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Notification != null ? p.Notification.OnReady : false;
                    }
                }
            }

            return result;
        }

        public static String GetEmailRecipient(int clientId)
        {
            String result = "";

            using (var ctx = new EF6.xFilmEntities())
            {
                var cu = ctx.Client_User.Where(x => x.ClientID == clientId && x.PrimaryUser == true).SingleOrDefault();
                if (cu != null)
                {
                    var up = ctx.UserPreference.Where(x => x.UserId == cu.ID).SingleOrDefault();
                    if (up != null)
                    {
                        var p = new Helper.UserExHelper.Preferences();
                        p.Read(up.UserId);
                        result = p.Receipt != null ? p.EmailRecipient : "";
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 1 = En, 2 = Chs, 3 =Cht
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static int GetDefaultLanguageId(int clientId)
        {
            int result = 1;

            using (var ctx = new EF6.xFilmEntities())
            {
                var cAddr = ctx.Client_AddressBook.Where(x => x.ClientID == clientId && x.PrimaryAddr == true).SingleOrDefault();
                if (cAddr != null)
                {
                    result = cAddr.SMS_Lang.HasValue ? (cAddr.SMS_Lang.Value == 0 ? 1 : cAddr.SMS_Lang.Value) : 1;
                }
            }

            return result;
        }
    }
}
