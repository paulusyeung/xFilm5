using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace xFilm5.Helper
{
    /// <summary>
    /// 2017.05.11 paulus: 
    /// 事源，我想用 EF6 取代 DAL，哩個 helper 用嚟代替 DAL.Common 配合 EF6 用
    /// 唔使抄哂過嚟，用到先 copy
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
        }
    }
}
