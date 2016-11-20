﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace xFilm5.DAL4Win
{
    /// <summary>
    /// This is the Common utility for Data Access Layer.
    /// Date Created:   2016-11-17 10:14:57
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       Common for BusinessObjects_v5.0.cst
    /// </summary>
    public class Common
    {
        #region Enum
        public class Enums
        {
            public enum Status
            {
                Suspened = -1,
                Draft = 0,
                Active,
                Power
            }

            public enum EditMode
            {
                Add,
                Edit,
                Read
            }

            public enum UserRole
            {
                Guest,
                Operator,
                Supervisor,
                Manager,
                Admin
            }

            public enum Language
            {
                English = 1,
                SimplifiedChinese,
                TranditionalChinese
            }

            public enum PersonalAddress
            {
                HomeEn,
                HomeZh,
                WorkEn,
                WorkZh
            }

            public enum CorporateAddress
            {
                OfficeEn,
                OfficeZh,
                Factory
            }

            #region 2016.11.05 paulus: 工業 4.0
            public enum PrintQSubitemType
            {
                Ps,
                Vps,
                Tiff,
                Cip3,
                Blueprint,
                Plate
            }

            public enum DeviceType
            {
                Chrome,
                Firefox,
                Safari,
                Email,
                SMS,
                WhatsApp
            }
            #endregion
        }
        #endregion

        #region Config
        public class Config
        {
            private static string ConnectionString4Excel03
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["OleConn4Excel03"].ConnectionString;
                }
            }

            private static string ConnectionString4Excel07
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["OleConn4Excel07"].ConnectionString;
                }
            }

            public static OleDbConnection GetOleDbConnection(string dataSource)
            {
                string connString = string.Empty;

                if (dataSource.Length > 0)
                {
                    string ext = dataSource.Remove(0, dataSource.LastIndexOf('.') + 1);
                    switch (ext.ToLower().Trim())
                    {
                        case "xls":
                            connString = string.Format(ConnectionString4Excel03, dataSource);
                            break;
                        case "xlsx":
                            connString = string.Format(ConnectionString4Excel07, dataSource);
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(connString))
                {
                    OleDbConnection oConn = new OleDbConnection(connString);

                    if (oConn.State == ConnectionState.Open)
                    {
                        oConn.Close();
                    }

                    return oConn;
                }
                else
                {
                    return null;
                }
            }

            public static string ConnectionString
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["SysDb"].ConnectionString;
                }
            }

            //public static Guid CurrentUserId
            //{
            //    get
            //    {
            //        Guid cookieStaffId = Guid.Empty;
            //        if (HttpContext.Current.Request.Cookies["xFilm5.DAL4Win"] != null)
            //        {
            //            if (Common.Utility.IsGUID(HttpContext.Current.Request.Cookies["xFilm5.DAL4Win"].Value))
            //            {
            //                cookieStaffId = new Guid(HttpContext.Current.Request.Cookies["xFilm5.DAL4Win"].Value);
            //            }
            //        }
            //        return cookieStaffId;
            //    }
            //    set
            //    {
            //        System.Web.HttpCookie oCookie = new System.Web.HttpCookie("xFilm5.DAL4Win");

            //        if (value != Guid.Empty)
            //        {
            //            // create the cookie
            //            DateTime now = DateTime.Now;

            //            oCookie.Value = value.ToString();
            //            oCookie.Expires = now.AddYears(1);

            //            System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
            //        }
            //        else
            //        {
            //            // destory the cookie
            //            DateTime now = DateTime.Now;

            //            oCookie.Value = value.ToString();
            //            oCookie.Expires = now.AddDays(-1);

            //            System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
            //        }
            //    }
            //}

            //public static int CurrentLanguageId
            //{
            //    get
            //    {
            //        int result = 1;
            //        string sLang = (string)System.Web.HttpContext.Current.Session["UserLanguage"];
            //        if (sLang == null) sLang = System.Web.HttpContext.Current.Request.UserLanguages[0];

            //        switch (sLang.ToLower())
            //        {
            //            case "chs":
            //            case "zh-chs":
            //            case "zh-cn":
            //                result = 2;
            //                break;
            //            case "cht":
            //            case "zh-cht":
            //            case "zh-hk":
            //            case "zh-tw":
            //                result = 3;
            //                break;
            //            case "en":
            //            case "en-us":
            //            default:
            //                result = 1;
            //                break;
            //        }
            //        return result;
            //    }
            //}

            //public static string CurrentWordDict
            //{
            //    get
            //    {
            //        string result = "WordDict.xml";
            //        return Path.Combine(VWGContext.Current.Config.GetDirectory("UserData"), result);
            //    }
            //}

            /// <summary>
            /// Maximum records allowed in SQL Query
            /// Default = 500 records
            /// </summary>
            public static int SqlQueryLimit
            {
                get
                {
                    string sqlQueryLimit = "500";
                    if (ConfigurationManager.AppSettings["SqlQueryLimit"] != null)
                    {
                        sqlQueryLimit = ConfigurationManager.AppSettings["SqlQueryLimit"];
                    }
                    return Convert.ToInt32(sqlQueryLimit);
                }
            }

            /// <summary>
            /// Maximum Command Timed out of SQL Query
            /// Default = 600 seconds
            /// </summary>
            public static int CommandTimedOut
            {
                get
                {
                    string commandTimedOut = "600";
                    if (ConfigurationManager.AppSettings["CommandTimedOut"] != null)
                    {
                        commandTimedOut = ConfigurationManager.AppSettings["CommandTimedOut"];
                    }
                    return Convert.ToInt32(commandTimedOut);
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

            public static string InBox
            {
                get
                {
                    string result = @"C:\xFilm5.DAL4Win\InBox";

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
                    string result = @"C:\xFilm5.DAL4Win\OutBox";

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
                    string result = @"C:\xFilm5.DAL4Win\DropBox";

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

            public static string GsWorkFolder
            {
                get
                {
                    string result = @"C:\xFilm5.DAL4Win\WorkFolder";

                    if (ConfigurationManager.AppSettings["Gswin32_WorkFolder"] != null)
                    {
                        result = (string)ConfigurationManager.AppSettings["Gswin32_WorkFolder"];
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

            public static string AttachedImageExtension
            {
                get
                {
                    string result = "bmp;gif;jpg;jpeg;png";

                    if (ConfigurationManager.AppSettings["AttachedImageExtension"] != null)
                    {
                        result = (string)ConfigurationManager.AppSettings["AttachedImageExtension"];
                    }

                    return result;
                }
            }

            public static string AttachedFileExtension
            {
                get
                {
                    string result = "txt;pdf;doc;docx;xls;xlsx";

                    if (ConfigurationManager.AppSettings["AttachedFileExtension"] != null)
                    {
                        result = (string)ConfigurationManager.AppSettings["AttachedFileExtension"];
                    }

                    return result;
                }
            }
        }
        #endregion

        #region Utility
        public class Utility
        {
            public static bool IsGUID(string expression)
            {
                if (expression != null)
                {
                    Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                    return guidRegEx.IsMatch(expression);
                }
                return false;
            }

            // Matches any unsigned or signed floating point number/numeric string.
            public static bool IsNumeric(string expression)
            {
                if (expression != null)
                {
                    Regex numericRegEx = new Regex(@"^-?\d+(\.\d+)?$");
                    return numericRegEx.IsMatch(expression);
                }
                return false;
            }

            public static bool IsImage(string extension)
            {
                if (extension != null)
                {
                    return Common.Config.AttachedImageExtension.Contains(extension);
                }

                return false;
            }

            /// <summary>
            /// ExtractNumbers("12EFR77")
            /// </summary>
            /// <param name="expr"></param>
            /// <returns></returns>
            public static string ExtractNumbers(string expr)
            {
                return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]"));
            }
        }
        #endregion

        #region DateTime Helper
        public class DateTimeHelper
        {
            /// <summary>
            /// Convert the datetime value to string with time or without.
            /// If the value is equaled to 1900-01-01, it would return a emty value.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="withTime"></param>
            /// <returns></returns>
            public static string DateTimeToString(DateTime value, bool withTime)
            {
                string formatString = GetDateFormat();
                if (withTime)
                {
                    formatString = GetDateTimeFormat();
                }

                if (!value.Equals(new DateTime(1900, 1, 1)))
                {
                    return value.ToString(formatString);
                }
                else
                {
                    return string.Empty;
                }
            }
            public static string DateTimeToString(string value, bool withTime)
            {
                string result = String.Empty;
                string formatString = GetDateFormat();
                if (withTime)
                {
                    formatString = GetDateTimeFormat();
                }
                try
                {
                    DateTime source = DateTime.Parse(value);
                    if (!source.Equals(new DateTime(1900, 1, 1)))
                    {
                        result = source.ToString(formatString);
                    }
                }
                catch { }
                return result;
            }

            public static string GetDateFormat()
            {
                string result = String.Empty;

                //switch (VWGContext.Current.CurrentUICulture.ToString())
                //{
                //    case "zh-CHS":
                //        result = "yyyy-MM-dd";
                //        break;
                //    case "zh-CHT":
                //        result = "dd/MM/yyyy";
                //        break;
                //    case "en-US":
                //    default:
                //        result = "dd/MM/yyyy";
                //        break;
                //}
                result = "yyyy-MM-dd";

                return result;
            }

            public static string GetDateTimeFormat()
            {
                string result = String.Empty;

                //switch (VWGContext.Current.CurrentUICulture.ToString())
                //{
                //    case "zh-CHS":
                //        result = "yyyy-MM-dd HH:mm";
                //        break;
                //    case "zh-CHT":
                //        result = "dd/MM/yyyy HH:mm";
                //        break;
                //    case "en-US":
                //    default:
                //        result = "dd/MM/yyyy HH:mm";
                //        break;
                //}
                result = "yyyy-MM-dd HH:mm";

                return result;
            }
        }
        #endregion

        #region Combo Box Item

        public class ComboItem
        {
            private string _code = string.Empty;
            private int _id = 0;

            public ComboItem(string code, int id)
            {
                _code = code;
                _id = id;
            }

            public string Code
            {
                get
                {
                    return _code;
                }
                set
                {
                    _code = value;
                }
            }

            public int Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;
                }
            }
        }

        public class ComboList : BindingList<ComboItem>
        {
        }

        #endregion
    }
}
