using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.Bot.Helper
{
    class FCMHistoryHelper
    {
        public static bool WriteHistory(FCMHistory fcmHistory)
        {
            bool result = false;

            try
            {
                using (var ctx = new xFilmEntities())
                {
                    var hst = new FCMHistory();

                    //hst.FCMHistoryId = Guid.NewGuid();    Id is an Identity field
                    hst.MessageTitle = fcmHistory.MessageTitle;
                    hst.MessageBody = fcmHistory.MessageBody;
                    hst.DeliveredOn = fcmHistory.DeliveredOn;
                    hst.Topic = fcmHistory.Topic;
                    hst.RecipientList = fcmHistory.RecipientList.Substring(0, 1024);
                    hst.UserIdList = fcmHistory.UserIdList;

                    ctx.FCMHistory.Add(hst);
                    ctx.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
