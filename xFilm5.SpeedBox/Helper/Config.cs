using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace xFilm5.SpeedBox
{
    public class Config
    {
        public static Guid SuperUserId { get; set; }
        public static Guid CurrentUserId { get; set; }
        public static String CurrentWordDict { get; set; }
        public static int CurrentLanguageId { get; set; }

        public static String CurrentTheme
        {
            get
            {
                String theme = "";
                if (HttpContext.Current.Request.Cookies["xPort5_SpeedBox_CurrentTheme"] != null)
                {
                    theme = HttpContext.Current.Request.Cookies["xPort5_SpeedBox_CurrentTheme"].Value;
                }
                return theme == String.Empty ? "Vista" : theme;
            }
            set
            {
                System.Web.HttpCookie oCookie = new System.Web.HttpCookie("xPort5_SpeedBox_CurrentTheme");

                if (value != null)
                {
                    // create the cookie
                    DateTime now = DateTime.Now;

                    oCookie.Value = value.ToString() == String.Empty ? "Vista" : value.ToString();
                    oCookie.Expires = now.AddYears(1);

                    System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
                }
                else
                {
                    // destory the cookie
                    DateTime now = DateTime.Now;

                    oCookie.Value = value.ToString();
                    oCookie.Expires = now.AddDays(-1);

                    System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
                }
            }
        }

        /// <summary>
        /// Film or Plate
        /// </summary>
        public static String CurrentPage
        {
            get
            {
                String page = "";
                if (HttpContext.Current.Request.Cookies["xPort5_SpeedBox_CurrentPage"] != null)
                {
                    page = HttpContext.Current.Request.Cookies["xPort5_SpeedBox_CurrentPage"].Value;
                }
                return page == String.Empty ? "Plate" : page;
            }
            set
            {
                System.Web.HttpCookie oCookie = new System.Web.HttpCookie("xPort5_SpeedBox_CurrentPage");

                if (value != null)
                {
                    // create the cookie
                    DateTime now = DateTime.Now;

                    oCookie.Value = value.ToString() == String.Empty ? "Plate" : value.ToString();
                    oCookie.Expires = now.AddYears(1);

                    System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
                }
                else
                {
                    // destory the cookie
                    DateTime now = DateTime.Now;

                    oCookie.Value = value.ToString();
                    oCookie.Expires = now.AddDays(-1);

                    System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
                }
            }
        }

        public static void LoadOnce_AtAppBegins()
        {
            LoadCurrentWordDict();
            LoadCurrentLanguageId();
        }

        public static void LoadCurrentWordDict()
        {
            //CurrentWordDict = Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), "WordDict.xml");
            CurrentWordDict = Path.Combine(HttpContext.Current.Server.MapPath("~"), "WordDict.xml");
        }

        public static void LoadCurrentLanguageId()
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
            CurrentLanguageId = result;
        }

        public static string InBox
        {
            get
            {
                string result = @"C:\Shared\xJos\InBox";

                if (ConfigurationManager.AppSettings["InBox"] != null)
                {
                    result = (string)ConfigurationManager.AppSettings["InBox"];
                    if (!(Directory.Exists(result)))
                    {
                        Directory.CreateDirectory(result);
                    }
                }

                return result;
            }
        }

        public static string OutBox
        {
            get
            {
                string result = @"C:\Shared\xJos\OutBox";

                if (ConfigurationManager.AppSettings["OutBox"] != null)
                {
                    result = (string)ConfigurationManager.AppSettings["OutBox"];
                    if (!(Directory.Exists(result)))
                    {
                        Directory.CreateDirectory(result);
                    }
                }

                return result;
            }
        }

        public static string DropBox
        {
            get
            {
                string result = @"C:\Shared\xJos\DropBox";

                if (ConfigurationManager.AppSettings["DropBox"] != null)
                {
                    result = (string)ConfigurationManager.AppSettings["DropBox"];
                    if (!(Directory.Exists(result)))
                    {
                        Directory.CreateDirectory(result);
                    }
                }

                return result;
            }
        }

        public static int MaxFileSize
        {
            get
            {
                int result = 1024 * 50;

                if (ConfigurationManager.AppSettings["MaxFileSize"] != null)
                {
                    result = Convert.ToInt32((string)ConfigurationManager.AppSettings["MaxFileSize"]);
                }

                return result;
            }
        }

        public static string SparkPost_ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SparkPost_ApiKey"] != null ? ConfigurationManager.AppSettings["SparkPost_ApiKey"] : "";
            }
        }

        public static string FCM_ServerKey
        {
            get
            {
                return ConfigurationManager.AppSettings["FCM_ServerKey"] != null ? ConfigurationManager.AppSettings["FCM_ServerKey"] : "";
            }
        }

        public static string FCM_SenderId
        {
            get
            {
                return ConfigurationManager.AppSettings["FCM_SenderId"] != null ? ConfigurationManager.AppSettings["FCM_SenderId"] : "";
            }
        }
    }
}
