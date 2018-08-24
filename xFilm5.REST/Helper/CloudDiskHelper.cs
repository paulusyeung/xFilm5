using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public static class CloudDiskHelper
    {
        /// <summary>
        /// Extract from dbo.PrintQueue and +1 for CupsJobId = Zyy999999,example Z18123456
        /// </summary>
        /// <returns></returns>
        public static string GetNextReOutputCupsJobId()
        {
            var result = "";
            var seed = String.Format("Z{0}", DateTime.Today.ToString("yy"));

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    var curJobId = ctx.PrintQueue.Where(x => x.CupsJobID.StartsWith(seed)).OrderByDescending(x => x.CupsJobID).FirstOrDefault();
                    if (curJobId != null)
                    {
                        result = GetNextReOutputCupsJobId(curJobId.CupsJobID);
                    }
                    else
                    {
                        result = seed + "1".PadLeft(5, '0');
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }

            return result;
        }

        /// <summary>
        /// Increement the source CupsJobId by 1
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetNextReOutputCupsJobId(string source)
        {
            string result = "";

            var seed = source.Substring(0, 3);
            var currentId = int.Parse(source.Replace(seed, ""));
            result = seed + (currentId + 1).ToString().PadLeft(5, '0');

            return result;
        }
    }
}