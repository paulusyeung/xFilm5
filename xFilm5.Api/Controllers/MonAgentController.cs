using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.Api.Helper;
//using xFilm5.Api.Models;
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

                using (var ctx = new EF6.xFilmEntities())
                {
                    var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                    if (client != null)
                    {
                        try
                        {
                            // 先捜一搜，有可能係 CUPS reprint
                            var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                            if (pq == null)
                            {
                                #region New print queue, use dbo.PrintQueue_InsRec
                                pq = new EF6.PrintQueue();
                                pq.ClientID = clientId;
                                pq.CupsJobID = jobId;
                                pq.CupsJobTitle = jobTitle;
                                pq.PlateSize = plateSize;
                                pq.BlueprintOrdered = false;
                                pq.Status = (int)DAL.Common.Enums.Status.Active;
                                pq.CreatedOn = DateTime.Now;
                                pq.CreatedBy = 0;
                                pq.ModifiedOn = DateTime.Now;
                                pq.ModifiedBy = 0;
                                pq.Retired = false;

                                ctx.PrintQueue.Add(pq);
                                ctx.SaveChanges();
                                #endregion
                            }
                            else
                            {
                                #region Reprint, reset PrintQueue.OrderID, clear dbo.PrintQueue_VPS items
                                pq.OrderID = 0;

                                pq.CupsJobTitle = jobTitle;
                                pq.PlateSize = plateSize;
                                pq.BlueprintOrdered = false;
                                pq.Status = (int)DAL.Common.Enums.Status.Active;
                                pq.CreatedOn = DateTime.Now;
                                pq.CreatedBy = 0;
                                pq.ModifiedOn = DateTime.Now;
                                pq.ModifiedBy = 0;
                                pq.Retired = false;

                                using (var scope = ctx.Database.BeginTransaction())
                                {
                                    ctx.Database.ExecuteSqlCommand(String.Format("DELETE PrintQueue_VPS WHERE PrintQueueID = {0}", pq.ID.ToString()));
                                    //ctx.PrintQueue.Attach(pq);
                                    ctx.SaveChanges();

                                    scope.Commit();
                                }
                                #endregion
                            }

                            UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Ps);

                            #region 2018.07.13 paulus: 通知 xFilm5.Bot upload file 去 Cloud Disk
                            BotHelper.PostCloudDisk_ApiCupsUploadFile(jobTitle);
                            #endregion

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
                }
                /**
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
                */
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

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                        if (pq != null)
                        {
                            // 2011.11.08 paulus: 先 check 下有冇相同 PrintQueue_VPS，如果冇先至 InsRec，否則 UpdRec
                            var vps = ctx.PrintQueue_VPS.Where(x => x.PrintQueueID == pq.ID && x.VpsFileName == vpsFileName).SingleOrDefault();

                            #region dbo.PrintQuee_VPS_InsRec or UpRec
                            if (vps == null)
                            {
                                vps = new EF6.PrintQueue_VPS();

                                vps.PrintQueueID = pq.ID;
                                vps.VpsFileName = vpsFileName;
                                vps.PlateOrdered = false;
                                vps.CreatedOn = DateTime.Now;
                                vps.CreatedBy = 0;
                                vps.ModifiedOn = DateTime.Now;
                                vps.ModifiedBy = 0;
                                vps.Retired = false;
                                vps.RetiredOn = Helper.DateTimeHelper.GetZeroDate();

                                ctx.PrintQueue_VPS.Add(vps);
                                ctx.SaveChanges();
                            }
                            else
                            {
                                vps.PrintQueueID = pq.ID;
                                vps.VpsFileName = vpsFileName;
                                vps.PlateOrdered = false;
                                vps.CreatedOn = DateTime.Now;
                                vps.CreatedBy = 0;
                                vps.ModifiedOn = DateTime.Now;
                                vps.ModifiedBy = 0;
                                vps.Retired = false;

                                ctx.SaveChanges();
                            }
                            #endregion

                            //UpdateListCycle(pQueue.ID, (int)DAL.Common.Enums.PrintQSubitemType.Vps);
                            UpdateListCycle_Vps(pq.ID, vps.ID, (int)DAL.Common.Enums.PrintQSubitemType.Vps);

                            #region 2018.07.13 paulus: 通知 xFilm5.Bot upload file 去 Cloud Disk
                            BotHelper.PostCloudDisk_ApiVpsUploadFile(String.Format("{0}.{1}", clientId.ToString(), vpsFileName));
                            #endregion

                            log.Info("[vps] " + jsonData.ToString());
                            return Ok();
                        }
                        else
                        {
                            log.Error("[vps] PrintQueue == null " + jsonData.ToString());
                            return NotFound();
                        }
                    }
                    /**
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
                    */
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

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                        if (pq != null)
                        {
                            UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Cip3);

                            #region 2018.07.13 paulus: 通知 xFilm5.Bot upload file 去 Cloud Disk
                            BotHelper.PostCloudDisk_ApiCip3UploadFile(cip3FileName);
                            #endregion

                            log.Info("[cip3] " + jsonData.ToString());
                            return Ok();
                        }
                        else
                        {
                            log.Error("[cip3] PrintQueue == null " + jsonData.ToString());
                            return NotFound();
                        }
                    }
                    /**
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
                    */
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

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                        if (pq != null)
                        {
                            UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Tiff);

                            log.Info("[tiff] " + jsonData.ToString());
                            return Ok();
                        }
                        else
                        {
                            log.Error("[tiff] PrintQueue == null " + jsonData.ToString());
                            return NotFound();
                        }
                    }
                    /**
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
                    */
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
                    String bpFileNameWithoutSuffix = bpFileName.Substring(0, bpFileName.LastIndexOf('.'));

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                        if (pq != null)
                        {
                            #region 2018.11.06 paulus: update dbo.OrderPkPrintQueue.IsReady
                            var pqVps = ctx.PrintQueue_VPS.Where(x => x.PrintQueueID == pq.ID && x.VpsFileName.Contains(bpFileNameWithoutSuffix)).FirstOrDefault();
                            if (pqVps != null)
                            {
                                var pkPq = ctx.OrderPkPrintQueueVps.Where(x => x.PrintQueueVpsId == pqVps.ID && x.CheckedBlueprint == true).FirstOrDefault();
                                if (pkPq != null)
                                {
                                    pkPq.IsReady = true;
                                    ctx.SaveChanges();
                                }
                            }
                            #endregion

                            UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Blueprint);

                            log.Info("[blueprint] " + jsonData.ToString());

                            #region 2018.11.06 paulus: send Fcm notification
                            if (pqVps != null)
                            {
                                Helper.BotHelper.PostSendFcmOnOrder(pqVps.ID);       // 叫 xFIlm5.Bot server 發短訊
                            }
                            #endregion

                            return Ok();
                        }
                        else
                        {
                            log.Error("[blueprint] PrintQueue == null " + jsonData.ToString());
                            return NotFound();
                        }
                    }
                    /**
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
                    */
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

                    using (var ctx = new EF6.xFilmEntities())
                    {
                        var pq = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == jobId).SingleOrDefault();
                        if (pq != null)
                        {
                            var vps = ctx.PrintQueue_VPS.Where(x => x.PrintQueueID == pq.ID && x.VpsFileName == plateFileName).SingleOrDefault();
                            if (vps == null)
                            {
                                UpdateListCycle(pq.ID, (int)DAL.Common.Enums.PrintQSubitemType.Plate);
                            }
                            else
                            {
                                UpdateListCycle_Vps(pq.ID, vps.ID, (int)DAL.Common.Enums.PrintQSubitemType.Plate);
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
                    /**
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
                    */
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
            using (var ctx = new EF6.xFilmEntities())
            {
                var cycleExist = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueId == pQueueId && x.PrintQSubitemType == type).Any();
                if (!(cycleExist))
                {
                    var cycle = new EF6.PrintQueue_LifeCycle();
                    cycle.PrintQueueId = pQueueId;
                    cycle.PrintQSubitemType = type;
                    cycle.Status = (int)DAL.Common.Enums.Status.Active;
                    cycle.CreatedOn = DateTime.Now;
                    cycle.CreatedBy = 0;

                    ctx.PrintQueue_LifeCycle.Add(cycle);
                    ctx.SaveChanges();
                }
            }
            /**
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
            */
        }

        private void UpdateListCycle_Vps(int pQueueId, int pQueueVpsId, int type)
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                var cycleExist = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueVpsId == pQueueId && x.PrintQSubitemType == type).Any();
                if (!(cycleExist))
                {
                    var cycle = new EF6.PrintQueue_LifeCycle();
                    cycle.PrintQueueId = pQueueId;
                    cycle.PrintQueueVpsId = pQueueVpsId;
                    cycle.PrintQSubitemType = type;
                    cycle.Status = (int)DAL.Common.Enums.Status.Active;
                    cycle.CreatedOn = DateTime.Now;
                    cycle.CreatedBy = 0;

                    ctx.PrintQueue_LifeCycle.Add(cycle);
                    ctx.SaveChanges();
                }
            }
            /**
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
            */
        }
    }
}
