using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

using Gizmox.WebGUI;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using xFilm5.Bot.Models;

namespace xFilm5.Bot
{
    public class Utility
    {
        public class Owner
        {
            public static int GetOwnerId()
            {
                int result = 0;

                using (var ctx = new xFilm5Entities())
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

                using (var ctx = new xFilm5Entities())
                {
                    var client = ctx.Client.Where(x => x.Status == 2).SingleOrDefault();
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

                using (var ctx = new xFilm5Entities())
                {
                    var item = ctx.Client_AddressBook.Where(x => (x.ID == GetOwnerId()) && (x.PrimaryAddr == true)).SingleOrDefault();
                    if (item != null)
                    {
                        result = item.Address;
                    }
                }

                return result;
            }

            public static String GetWorkshopAddress(int workshopId)
            {
                String result = String.Empty;

                using (var ctx = new xFilm5Entities())
                {
                    var item = ctx.Client_User.Where(x => x.ID == workshopId).SingleOrDefault();
                    if (item != null)
                    {
                        result = ConfigurationManager.AppSettings[item.FullName] != null ? ConfigurationManager.AppSettings[item.FullName].ToString() : item.FullName;
                    }
                }

                return result;
            }
        }

        public class Workshop
        {
            /// <summary>
            /// 攞 dbo.Client_User.FullName 頭兩個 characters
            /// KT = 觀塘; TW = 荃灣; KF = 葵芳
            /// </summary>
            /// <param name="workshipId"></param>
            /// <returns></returns>
            public static String GetWrokshopCode(int workshipId)
            {
                String result = "";

                using (var ctx = new xFilm5Entities())
                {
                    var wk = ctx.Client_User.Where(x => x.ID == workshipId).SingleOrDefault();
                    if (wk != null)
                    {
                        result = wk.FullName.Substring(0, 2);
                    }
                }

                return result;
            }

            /// <summary>
            /// config.web parameter: Workshop_Address_XX
            /// where XX = Workshop Code
            /// </summary>
            /// <param name="workshopId"></param>
            /// <returns></returns>
            public static String GetAddress(int workshopId)
            {
                String result = String.Empty;

                String key = String.Format("Workshop_Address_{0}", GetWrokshopCode(workshopId));
                result = ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key].ToString() : "";

                return result;
            }

            /// <summary>
            /// config.web parameter: Workshop_Xprinter_XX
            /// where XX = Workshop Code
            /// </summary>
            /// <param name="workshopId"></param>
            /// <returns></returns>
            public static String GetXprinter(int workshopId)
            {
                String result = String.Empty;

                String key = String.Format("Workshop_Xprinter_{0}", GetWrokshopCode(workshopId));
                result = ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key].ToString() : "";

                return result;
            }
        }
    }
}
