using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace xFilm5.REST.Helper
{
    public class OwnerHelper
    {
        public static int GetOwnerId()
        {
            int result = 0;

            //string sql = String.Format("Status = {0}", 2);
            //DAL.Client client = DAL.Client.LoadWhere(sql);
            using (var ctx = new EF6.xFilmEntities())
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

            //DAL.Client client = DAL.Client.Load(GetOwnerId());
            using (var ctx = new EF6.xFilmEntities())
            {
                var id = GetOwnerId();
                var client = ctx.Client.Where(x => x.ID == id).SingleOrDefault();
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

            //string sql = String.Format("ClientID = {0} AND PrimaryAddr = 1", GetOwnerId().ToString());
            using (var ctx = new EF6.xFilmEntities())
            {
                var id = GetOwnerId();
                var address = ctx.Client_AddressBook.Where(x => x.ClientID == id && x.PrimaryAddr == true).SingleOrDefault();
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

            using (var ctx = new EF6.xFilmEntities())
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