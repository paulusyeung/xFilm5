using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using xFilm5.EF6;

namespace xFilm5.REST
{
    /// <summary>
    /// 抄 xFilm5.Helper.CommonHelper.cs
    /// </summary>
    public class CommonHelper
    {
        public class Enums
        {
            public enum Status
            {
                Inactive = -1,
                Draft = 0,
                Active,
                Power
            }

            /// <summary>
            /// 用於 dbo.UserAuth 同埋 dbo.UserNotification
            /// </summary>
            public enum DeviceType
            {
                Chrome,
                Firefox,
                Safari,
                Email,
                SMS,
                WhatsApp
            }

            /// <summary>
            /// 用於 dbo.UserPreference
            /// </summary>
            public enum ObjectType
            {
                UserExInfo,
                PageLayout,
                ListviewLayout
            }

            public enum UserRole
            {
                Undefined,
                Customer,
                Operator,
                Sales,
                Account,
                Admin,
                Workshop,
                Cashier
            }

            public enum UserType
            {
                Staff,
                Customer,
                Supplier
            }

            public enum PaymentType
            {
                OnAccount,
                Cash
            }

            public enum OrderType
            {
                Blueprint,
                Film,
                Plate
            }

            public enum Workflow
            {
                Cancelled = 1,
                Queuing,
                Retouch,
                Printing,
                ProofingOutgoing,
                ProofingIncoming,
                Ready,
                Dispatch,
                Completed
            }

            public enum PrintQSubitemType
            {
                Ps,             // 收到 ps
                Vps,            // 有 vps
                Tiff,           // 有 tiff
                Cip3,           // 有 cip3
                Blueprint,      // 有 藍紙
                Plate,          // 有 鋅
                Order,          // 落咗荷打
                Receipt,        // 收咗貸
                Invoice,        // 開咗單
                Film            // 2017 追加
            }
        }

        public class Config
        {
            public static int CurrentUserId
            {
                get
                {
                    int cookieUserId = 0;
                    if (HttpContext.Current.Request.Cookies["xFilm"] != null)
                    {
                        try
                        {
                            string userId = HttpContext.Current.Request.Cookies["xFilm"].Value;
                            cookieUserId = Convert.ToInt32(userId);

                            //cookieUserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["xFilm3"].Value);
                        }
                        catch
                        {
                            cookieUserId = 0;
                        }
                    }
                    return cookieUserId;
                }
                set
                {
                    System.Web.HttpCookie oCookie = new System.Web.HttpCookie("xFilm");
                    DateTime now = DateTime.Now;

                    if (value != 0)
                    {
                        // create cookie
                        oCookie.Value = value.ToString();
                        oCookie.Expires = now.AddYears(1);

                        System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);

                        //VWGContext.Current.Cookies["xFlim3"] = value.ToString();
                    }
                    else
                    {
                        // destroy cookie
                        oCookie.Value = value.ToString();
                        oCookie.Expires = now.AddDays(-1);

                        System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);

                        //VWGContext.Current.Cookies["xFilm3"] = null;
                    }
                }
            }

            public static int CurrentLanguageId
            {
                get
                {
                    int result = 1;
                    string sLang = (string)System.Web.HttpContext.Current.Session["UserLanguage"];
                    if (sLang == null) sLang = System.Web.HttpContext.Current.Request.UserLanguages[0];

                    switch (sLang.ToLower())
                    {
                        case "chs":
                        case "zh-chs":
                        case "zh-cn":
                            result = 2;
                            break;
                        case "cht":
                        case "zh-cht":
                        case "zh-hk":
                        case "zh-tw":
                            result = 3;
                            break;
                        case "en":
                        case "en-us":
                        default:
                            result = 1;
                            break;
                    }
                    return result;
                }
            }

            public static string CurrentWordDict
            {
                get
                {
                    string result = "WordDict.xml";
                    return Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), result);
                }
            }

            public static IFormatProvider DefaultCultureInfo
            {
                get
                {
                    CultureInfo defaultCultureInfo = new CultureInfo("en-US");
                    return defaultCultureInfo;
                }
            }

            public static void SetCultureInfo(string selectedLanguage)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            }

            public static bool IamStaff
            {
                get
                {
                    bool result = false;

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var cUser = ctx.Client_User.Where(x => x.ID == CurrentUserId).SingleOrDefault();
                        if (cUser != null)
                        {
                            switch (cUser.SecurityLevel)
                            {
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    result = true;
                                    break;
                            }
                        }
                    }

                    return result;
                }
            }

            public static bool IamClient
            {
                get
                {
                    bool result = false;

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var cUser = ctx.Client_User.Where(x => x.ID == CurrentUserId).SingleOrDefault();
                        if (cUser != null)
                        {
                            switch (cUser.SecurityLevel)
                            {
                                case 1:
                                    result = true;
                                    break;
                            }
                        }
                    }

                    return result;
                }
            }

            /// <summary>
            /// 己已經冇用，改由 xFilm5.Bot 根據 Workshop 自己決定
            /// </summary>
            public static string Xprinter_KT
            {
                get
                {
                    return ConfigurationManager.AppSettings["Xprinter_KT"] != null ? ConfigurationManager.AppSettings["Xprinter_KT"] : "";
                }
            }

            public static string SparkPost_ApiKey
            {
                get
                {
                    return ConfigurationManager.AppSettings["SparkPost_ApiKey"] != null ? ConfigurationManager.AppSettings["SparkPost_ApiKey"] : "";
                }
            }
        }

        public class xFilm5System
        {
            public static int GetNextInvoiceNumber()
            {
                int result = 0;

                using (var ctx = new xFilmEntities())
                {
                    var sys = ctx.X_Counter.FirstOrDefault();
                    if (sys != null)
                    {
                        result = sys.InvoiceNo.Value;
                        sys.InvoiceNo++;
                        ctx.SaveChanges();
                    }
                }

                return result;
            }
        }

        public class Owner
        {
            public static int GetOwnerId()
            {
                int result = 0;

                using (var ctx = new xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.Status == 2).SingleOrDefault();
                    if (client != null)
                    {
                        result = client.ID;
                    }
                }

                return result;
            }

            public static string GetOwnerName()
            {
                string result = String.Empty;

                using (var ctx = new xFilmEntities())
                {
                    int ownerId = GetOwnerId();

                    var client = ctx.Client.Where(x => x.ID == ownerId).SingleOrDefault();
                    if (client != null)
                    {
                        result = client.Name;
                    }
                }

                return result;
            }

            public static string GetOwnerAddress()
            {
                string result = String.Empty;

                using (var ctx = new xFilmEntities())
                {
                    int ownerId = GetOwnerId();

                    var address = ctx.Client_AddressBook.Where(x => x.ClientID == ownerId && x.PrimaryAddr == true).SingleOrDefault();
                    if (address != null)
                    {
                        result = address.Address;
                    }
                }

                return result;
            }

            public static String GetWorkshopAddress(int workshopId)
            {
                String result = String.Empty;

                using (var ctx = new xFilmEntities())
                {
                    var user = ctx.Client_User.Where(x => x.ID == workshopId).SingleOrDefault();
                    if (user != null)
                    {
                        result = ConfigurationManager.AppSettings[user.FullName] != null ? ConfigurationManager.AppSettings[user.FullName].ToString() : user.FullName;
                    }
                }

                return result;
            }
        }
    }
}
