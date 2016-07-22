using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                    // 先捜一搜，有可能係 CUPS reprint
                    String sql = String.Format("ClientID = {0} AND CupsJobID = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pq = DAL.PrintQueue.LoadWhere(sql);
                    if (pq == null)
                    {
                        #region New print queue, use dbo.PrintQueue_InsRec
                        pq = new DAL.PrintQueue();
                        pq.ClientID = clientId;
                        pq.CupsJobID = jobId;
                        #endregion
                    }
                    else
                    {
                        #region Reprint, reset PrintQueue.OrderID, clear dbo.PrintQueue_VPS items
                        pq.OrderID = 0;

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = String.Format("DELETE PrintQueue_VPS WHERE PrintQueueID = {0}", pq.ID.ToString());
                            cmd.CommandType = CommandType.Text;

                            SqlHelper.Default.ExecuteNonQuery(cmd);
                        }
                        #endregion
                    }

                    #region common, dbo.PrintQuee_InsRec or _UpdRec
                    pq.CupsJobTitle = jobTitle;
                    pq.PlateSize = plateSize;
                    pq.Status = (int)DAL.Common.Enums.Status.Active;
                    pq.CreatedOn = DateTime.Now;
                    pq.ModifiedOn = DateTime.Now;
                    pq.Retired = false;
                    pq.Save();
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
