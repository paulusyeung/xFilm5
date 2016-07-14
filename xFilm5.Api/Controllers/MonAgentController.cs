using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.Api.Models;
using xFilm5.DAL;

namespace xFilm5.Api.Controllers
{
    public class MonAgentController : ApiController
    {
        [Route("api/cups")]
        public IHttpActionResult PostCups([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue

                // 每個 call 祇得一隻 record
                int clientId = jsonData["clientId"].Value<int>();
                String jobId = jsonData["jobId"].Value<String>();
                String jobTitle = jsonData["jobTitle"].Value<String>();
                String plateSize = jsonData["plateSize"].Value<String>();

                DAL.Client client = DAL.Client.Load(clientId);
                if (client != null)
                {
                    #region dbo.PrintQuee_InsRec
                    DAL.PrintQueue pQueue = new DAL.PrintQueue();
                    pQueue.ClientID = clientId;
                    pQueue.CupsJobID = jobId;
                    pQueue.CupsJobTitle = jobTitle;
                    pQueue.PlateSize = plateSize;
                    pQueue.Status = (int)DAL.Common.Enums.Status.Active;
                    pQueue.CreatedOn = DateTime.Now;
                    pQueue.ModifiedOn = DateTime.Now;
                    pQueue.Retired = false;
                    pQueue.Save();
                    #endregion

                    return Ok();
                }
                else
                {
                    return NotFound();
                }

                #endregion
            }
        }

        [Route("api/vps")]
        public IHttpActionResult PostVps([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS

                // 每個 call 祇得一隻 record
                int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                String jobId = jsonData["jobId"].Value<String>();
                String vpsFileName = jsonData["vpsFileName"].Value<String>();

                String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                if (pQueue != null)
                {
                    #region dbo.PrintQuee_VPS_InsRec
                    DAL.PrintQueue_VPS pQueueVps = new DAL.PrintQueue_VPS();
                    pQueueVps.PrintQueueID = pQueue.ID;
                    pQueueVps.VpsFileName = vpsFileName;
                    pQueueVps.CreatedOn = DateTime.Now;
                    pQueueVps.ModifiedOn = DateTime.Now;
                    pQueueVps.Retired = false;
                    pQueueVps.Save();
                    #endregion

                    return Ok();
                }
                else
                {
                    return NotFound();
                }

                #endregion
            }
        }
    }
}
