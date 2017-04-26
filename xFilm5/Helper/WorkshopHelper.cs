using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class WorkshopHelper
    {
        /// <summary>
        /// 攞 dbo.Client_User.FullName 頭兩個 characters
        /// KT = 觀塘; TW = 荃灣; KF = 葵芳
        /// </summary>
        /// <param name="workshipId"></param>
        /// <returns></returns>
        public String GetWrokshopCode(int workshipId)
        {
            String result = "";

            DAL.Client_User wk = DAL.Client_User.Load(workshipId);
            if (wk != null)
            {
                result = wk.FullName.Substring(0, 2);
            }

            return result;
        }

        /// <summary>
        /// config.web parameter: Workshop_Address_XX
        /// where XX = Workshop Code
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public String GetAddress(int workshopId)
        {
            String result = String.Empty;

            DAL.Client_User user = DAL.Client_User.Load(workshopId);
            if (user != null)
            {
                String key = String.Format("Workshop_Address_{0}", GetWrokshopCode(workshopId));
                result = ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key].ToString() : "";
            }

            return result;
        }

        /// <summary>
        /// config.web parameter: Workshop_Xprinter_XX
        /// where XX = Workshop Code
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public String GetXprinter(int workshopId)
        {
            String result = String.Empty;

            DAL.Client_User user = DAL.Client_User.Load(workshopId);
            if (user != null)
            {
                String key = String.Format("Workshop_Xprinter_{0}", GetWrokshopCode(workshopId));
                result = ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key].ToString() : "";
            }

            return result;
        }
    }
}
