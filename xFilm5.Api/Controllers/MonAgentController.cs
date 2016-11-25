﻿using log4net;
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
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(MonAgentController));

        [Route("api/cups")]
        public IHttpActionResult PostCups([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[cups] jsonData == null");
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
                    try
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

                        UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Ps);

                        log.Info("[cups] " + jsonData.ToString());
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        log.Fatal("[cups] " + jsonData.ToString(), e);
                        return NotFound();
                    }
                }
                else
                {
                    log.Error("[cups] Client == null " + jsonData.ToString());
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
                log.Error("[vps] jsonData == null");
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS
                try
                {
                    // 每個 call 祇得一隻 record
                    int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                    String jobId = jsonData["jobId"].Value<String>();
                    String vpsFileName = jsonData["vpsFileName"].Value<String>();

                    String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                    if (pQueue != null)
                    {
                        // 2011.11.08 paulus: 先 check 下有冇相同 PrintQueue_VPS，如果冇先至 InsRec，否則 UpdRec
                        sql = String.Format("PrintQueueID = '{0}' AND VpsFileName = N'{1}'", pQueue.ID.ToString(), vpsFileName);
                        DAL.PrintQueue_VPS pQueueVps = DAL.PrintQueue_VPS.LoadWhere(sql);
                        if (pQueueVps == null)
                            pQueueVps = new DAL.PrintQueue_VPS();

                        #region dbo.PrintQuee_VPS_InsRec or UpRec
                        //DAL.PrintQueue_VPS pQueueVps = new DAL.PrintQueue_VPS();
                        pQueueVps.PrintQueueID = pQueue.ID;
                        pQueueVps.VpsFileName = vpsFileName;
                        pQueueVps.CreatedOn = DateTime.Now;
                        pQueueVps.ModifiedOn = DateTime.Now;
                        pQueueVps.Retired = false;
                        pQueueVps.Save();
                        #endregion

                        //UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Vps);
                        UpdateListCycle_Vps(pQueue.ID, pQueueVps.ID, (int)DAL.Common.Enums.PrintQSubitemType.Vps);

                        log.Info("[vps] " + jsonData.ToString());
                        return Ok();
                    }
                    else
                    {
                        log.Error("[vps] PrintQueue == null " + jsonData.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    log.Fatal("[vps] " + jsonData.ToString(), e);
                    return NotFound();
                }
                #endregion
            }
        }

        [Route("api/cip3")]
        public IHttpActionResult PostCip3([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[cip3] jsonData == null ");
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS
                try
                {
                    // 每個 call 祇得一隻 record
                    int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                    String jobId = jsonData["jobId"].Value<String>();
                    String cip3FileName = jsonData["cip3FileName"].Value<String>();

                    String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                    if (pQueue != null)
                    {
                        UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Cip3);

                        log.Info("[cip3] " + jsonData.ToString());
                        return Ok();
                    }
                    else
                    {
                        log.Error("[cip3] PrintQueue == null " + jsonData.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    log.Fatal("[cip3] " + jsonData.ToString(), e);
                    return NotFound();
                }
                #endregion
            }
        }

        [Route("api/tiff")]
        public IHttpActionResult PostTiff([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[tiff] jsonData == null");
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS
                try
                {
                    // 每個 call 祇得一隻 record
                    int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                    String jobId = jsonData["jobId"].Value<String>();
                    String tiffFileName = jsonData["tiffFileName"].Value<String>();

                    String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                    if (pQueue != null)
                    {
                        UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Tiff);

                        log.Info("[tiff] " + jsonData.ToString());
                        return Ok();
                    }
                    else
                    {
                        log.Error("[tiff] PrintQueue == null " + jsonData.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    log.Fatal("[tiff] " + jsonData.ToString(), e);
                    return NotFound();
                }
                #endregion
            }
        }

        [Route("api/bp")]
        public IHttpActionResult PostBlueprint([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[blueprint] jsonData == null");
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS
                try
                {
                    // 每個 call 祇得一隻 record
                    int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                    String jobId = jsonData["jobId"].Value<String>();
                    String bpFileName = jsonData["bpFileName"].Value<String>();

                    String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                    if (pQueue != null)
                    {
                        UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Blueprint);

                        log.Info("[blueprint] " + jsonData.ToString());
                        return Ok();
                    }
                    else
                    {
                        log.Error("[blueprint] PrintQueue == null " + jsonData.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    log.Fatal("[blueprint] " + jsonData.ToString(), e);
                    return NotFound();
                }
                #endregion
            }
        }

        [Route("api/plate")]
        public IHttpActionResult PostPlate([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                log.Error("[Plate] jsonData == null");
                return NotFound();
            }
            else
            {
                #region Extract POST json data and update dbo.PrintQueue_VPS
                try
                {
                    // 每個 call 祇得一隻 record
                    int clientId = Convert.ToInt32(jsonData["clientId"].Value<int>());
                    String jobId = jsonData["jobId"].Value<String>();
                    String plateFileName = jsonData["plateFileName"].Value<String>();

                    String sql = String.Format("ClientID = {0} AND CupsJobId = N'{1}'", clientId.ToString(), jobId);
                    DAL.PrintQueue pQueue = DAL.PrintQueue.LoadWhere(sql);
                    if (pQueue != null)
                    {
                        sql = String.Format("PrintQueueID = '{0}' AND VpsFileName = N'{1}'", pQueue.ID.ToString(), plateFileName);
                        DAL.PrintQueue_VPS pQueueVps = DAL.PrintQueue_VPS.LoadWhere(sql);

                        if (pQueueVps == null)
                        {
                            UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Plate);
                        }
                        else
                        {
                            UpdateListCycle_Vps(pQueue.ID, pQueueVps.ID, (int)DAL.Common.Enums.PrintQSubitemType.Plate);
                        }

                        log.Info("[plate] " + jsonData.ToString());
                        return Ok();
                    }
                    else
                    {
                        log.Error("[plate] PrintQueue == null " + jsonData.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    log.Fatal("[plate] " + jsonData.ToString(), e);
                    return NotFound();
                }
                #endregion
            }
        }

        /// <summary>
        /// log Life Cycle, 每個 SubitemType 一個 record
        /// </summary>
        /// <param name="pQueueId"></param>
        /// <param name="type"></param>
        private void UpdateListCycle(int pQueueId, int type)
        {
            String sql = String.Format("PrintQueueId = {0} AND PrintQSubitemType = {1}", pQueueId.ToString(), type);
            DAL.PrintQueue_LifeCycle lifeCycle = DAL.PrintQueue_LifeCycle.LoadWhere(sql);
            if (lifeCycle == null)
            {
                lifeCycle = new DAL.PrintQueue_LifeCycle();
                lifeCycle.PrintQueueId = pQueueId;
                lifeCycle.PrintQSubitemType = type;
            lifeCycle.Status = (int)DAL.Common.Enums.Status.Active;
            lifeCycle.CreatedOn = DateTime.Now;
            lifeCycle.CreatedBy = 0;
            lifeCycle.Save();

                //log.Info(String.Format("[InsRec PrintQueue_LifeCycle] PrintQueueId = {0}, PrintQSubitemType = {1}", pQueueId.ToString(), type.ToString()));
            }
        }

        private void UpdateListCycle_Vps(int pQueueId, int pQueueVpsId, int type)
        {
            String sql = String.Format("PrintQueueId = {0} AND PrintQueueVpsId = {1} AND PrintQSubitemType = {2}", pQueueId.ToString(), pQueueVpsId.ToString(), type);
            DAL.PrintQueue_LifeCycle lifeCycle = DAL.PrintQueue_LifeCycle.LoadWhere(sql);
            if (lifeCycle == null)
            {
                lifeCycle = new DAL.PrintQueue_LifeCycle();
                lifeCycle.PrintQueueId = pQueueId;
                lifeCycle.PrintQueueVpsId = pQueueVpsId;
                lifeCycle.PrintQSubitemType = type;
                lifeCycle.Status = (int)DAL.Common.Enums.Status.Active;
                lifeCycle.CreatedOn = DateTime.Now;
                lifeCycle.CreatedBy = 0;
                lifeCycle.Save();

                //log.Info(String.Format("[InsRec PrintQueue_LifeCycle] PrintQueueId = {0}, PrintQSubitemType = {1}", pQueueId.ToString(), type.ToString()));
            }
        }
    }
}
