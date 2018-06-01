using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public class PrintQueueHelper
    {
        public static bool WriteLogWithPqId(int pqId, EnumHelper.Order.PrintQSubitemType type, int userId)
        {
            bool result = false;

            using (var ctx = new xFilmEntities())
            {
                var pq = ctx.PrintQueue.Where(x => x.ID == pqId).SingleOrDefault();
                if (pq != null)
                {
                    var log = new PrintQueue_LifeCycle();
                    log.PrintQueueId = pq.ID;
                    log.PrintQSubitemType = (int)type;
                    log.Status = (int)EnumHelper.Common.Status.Active;
                    log.CreatedOn = DateTime.Now;
                    log.CreatedBy = userId;

                    ctx.PrintQueue_LifeCycle.Add(log);
                    ctx.SaveChanges();

                    result = true;
                }
            }

            return result;
        }

        public static bool WriteLogWithVpsId(int vpsId, EnumHelper.Order.PrintQSubitemType type, int userId)
        {
            bool result = false;

            using (var ctx = new xFilmEntities())
            {
                var vps = ctx.PrintQueue_VPS.Where(x => x.ID == vpsId).SingleOrDefault();
                if (vps != null)
                {
                    var log = new PrintQueue_LifeCycle();
                    log.PrintQueueId = vps.PrintQueueID;
                    log.PrintQueueVpsId = vps.ID;
                    log.PrintQSubitemType = (int)type;
                    log.Status = (int)EnumHelper.Common.Status.Active;
                    log.CreatedOn = DateTime.Now;
                    log.CreatedBy = userId;

                    ctx.PrintQueue_LifeCycle.Add(log);
                    ctx.SaveChanges();

                    result = true;
                }
            }

            return result;
        }
    }
}
