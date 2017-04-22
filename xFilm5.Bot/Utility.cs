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
    }
}
