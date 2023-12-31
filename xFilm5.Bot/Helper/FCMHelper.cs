﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using xFilm5.EF6;

namespace xFilm5.Bot.Helper
{
    public class FCMHelper
    {
        /// <summary>
        /// 冇用 plugin，睇下有冇問題先
        /// 一個以上 Device Id 可以： To = "deviceid1,deviceid2,deviceid3..."
        /// 所有 Android subscribers: To = "topics/android"
        /// 所有 iOS subscribers: To = "topics/ios"
        /// 所有 devices: To = "topics/all"
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="deviceId"></param>
        /// <param name="senderId"></param>
        /// <param name="msgTitle"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendPushNotification(string applicationID, string deviceId, string senderId, string msgTitle, string msgBody)
        {
            // refer: https://stackoverflow.com/questions/37412963/send-push-to-android-by-c-sharp-using-fcm-firebase-cloud-messaging
            //        https://stackoverflow.com/questions/38873523/how-to-send-notification-from-c-sharp-console-application
            //        https://stackoverflow.com/questions/38257160/firebase-cloud-messaging-and-c-sharp-server-side-code

            bool result = false;
            try
            {
                var list = deviceId.Split(',');
                foreach (string id in list)
                {
                    #region 砌個 message 出嚟
                    //string applicationID = "AIz..........Fep0";
                    //string senderId = "30............8";
                    //string deviceId = "ch_G60NPga4:APA9............T_LH8up40Ghi-J";

                    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";

                    var data = new
                    {
                        to = id,
                        notification = new
                        {
                            body = msgBody,
                            title = msgTitle,
                            sound = "Enabled"

                        },
                        /**
                        // refer: https://stackoverflow.com/questions/37711082/how-to-handle-notification-when-app-in-background-in-firebase/42279260#42279260
                        //data = new
                        //{
                        //    body = msgBody,
                        //    title = msgTitle,
                        //    sound = "Enabled"

                        //},
                        */
                        priority = "high"
                    };
                    //var serializer = new JavaScriptSerializer();
                    //var json = serializer.Serialize(data);
                    var json = JsonConvert.SerializeObject(data);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    tRequest.ContentLength = byteArray.Length;
                    #endregion

                    #region 將個 message 發出去
                    using (Stream dataStream = tRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = tRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    string str = sResponseFromServer;

                                    FCMResponse response = JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);

                                    // HACK: 好難講，有可能係暫時收唔倒，Firebase default 係 4 分鐘內都收唔倒先叫 failure，仲有，如果係 broadcast 入面有啲收到有啲收唔倒又點計？
                                    //       都係全部算 true 除咗 exceptional error
                                    result = true;
                                    /**
                                    if (response.success == 1)
                                    {
                                        //new NotificationBLL().InsertNotificationLog(dayNumber, notification, true);
                                        result = true;
                                    }
                                    else if (response.failure == 1)
                                    {
                                        //new NotificationBLL().InsertNotificationLog(dayNumber, notification, false);
                                        //sbLogger.AppendLine(string.Format("Error sent from FCM server, after sending request : {0} , for following device info: {1}", sResponseFromServer, jsonNotificationFormat));
                                    }
                                    */
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Web App background 唔識 fire，原因可能係要唔用 notification 字眼
        /// 參考：https://github.com/firebase/quickstart-js/issues/71#issuecomment-258872970
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="deviceId"></param>
        /// <param name="senderId"></param>
        /// <param name="msgTitle"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendPusData(string applicationID, string deviceId, string senderId, string msgTitle, string msgBody)
        {
            // refer: https://stackoverflow.com/questions/37412963/send-push-to-android-by-c-sharp-using-fcm-firebase-cloud-messaging
            //        https://stackoverflow.com/questions/38873523/how-to-send-notification-from-c-sharp-console-application
            //        https://stackoverflow.com/questions/38257160/firebase-cloud-messaging-and-c-sharp-server-side-code

            bool result = false;
            try
            {
                var list = deviceId.Split(',');
                foreach (string id in list)
                {
                    #region 砌個 message 出嚟
                    //string applicationID = "AIz..........Fep0";
                    //string senderId = "30............8";
                    //string deviceId = "ch_G60NPga4:APA9............T_LH8up40Ghi-J";

                    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";

                    var data = new
                    {
                        to = id,
                        //notification = new
                        //{
                        //    body = msgBody,
                        //    title = msgTitle,
                        //    sound = "Enabled"

                        //},
                        
                        // refer: https://stackoverflow.com/questions/37711082/how-to-handle-notification-when-app-in-background-in-firebase/42279260#42279260
                        data = new
                        {
                            body = msgBody,
                            title = msgTitle,
                            sound = "Enabled"

                        },
                        
                        priority = "high"
                    };
                    //var serializer = new JavaScriptSerializer();
                    //var json = serializer.Serialize(data);
                    var json = JsonConvert.SerializeObject(data);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    tRequest.ContentLength = byteArray.Length;
                    #endregion

                    #region 將個 message 發出去
                    using (Stream dataStream = tRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = tRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    string str = sResponseFromServer;

                                    FCMResponse response = JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);

                                    // HACK: 好難講，有可能係暫時收唔倒，Firebase default 係 4 分鐘內都收唔倒先叫 failure，仲有，如果係 broadcast 入面有啲收到有啲收唔倒又點計？
                                    //       都係全部算 true 除咗 exceptional error
                                    result = true;
                                    /**
                                    if (response.success == 1)
                                    {
                                        //new NotificationBLL().InsertNotificationLog(dayNumber, notification, true);
                                        result = true;
                                    }
                                    else if (response.failure == 1)
                                    {
                                        //new NotificationBLL().InsertNotificationLog(dayNumber, notification, false);
                                        //sbLogger.AppendLine(string.Format("Error sent from FCM server, after sending request : {0} , for following device info: {1}", sResponseFromServer, jsonNotificationFormat));
                                    }
                                    */
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return result;
        }

        public static bool SendOnVps(int clientId, string vpsFileName)
        {
            bool result = false;

            List<string> recipient = new List<string>();
            List<string> deviceList = new List<string>();       // 2018.03.14 paulus: log 邊個 device 會收到 FCM
            List<string> useridList = new List<string>();       // 2018.03.14 paulus: log 邊個 user 會收到 FCM

            int notifyType = (int)EnumHelper.User.NotifyType.OnVps;

            using (var ctx = new xFilmEntities())
            {
                var client = ctx.Client.Where(x => x.ID == clientId && x.Status >= 1).SingleOrDefault();
                if (client != null)
                {
                    #region 將同一個客有開嘅 user fcm token 加入 recipient list, 每隻登記咗嘅 device 都要發 FCM
                    var vpsList = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyType) && (x.ClientId == clientId)).ToList();

                    if (vpsList.Count > 0)
                    {
                        for (int i = 0; i < vpsList.Count; i++)
                        {
                            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(vpsList[i].AuthXml);
                            if (expando != null)
                            {
                                recipient.Add(expando.FCM.Token);

                                if (!(deviceList.Any(x => x.Contains(vpsList[i].DeviceId))))
                                    deviceList.Add(vpsList[i].DeviceId);
                                var userSid = UserHelper.GetUserSid(vpsList[i].UserId);
                                if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                    useridList.Add(userSid.ToString());
                            }
                        }
                    }
                    #endregion

                    if (recipient.Count > 0)
                    {
                        var deviceIds = string.Join(",", recipient.Where(x => x != String.Empty).ToArray());
                        var msgTitle = "x5 輕鬆 RIP";
                        var msgBody = String.Format("有 VPS：{0}", vpsFileName);

                        result = SendPusData(Config.FCM_ServerKey, deviceIds, Config.FCM_SenderId, msgTitle, msgBody);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 根據 VPS record 發 FCM 俾 Client & Staff
        /// </summary>
        /// <param name="pQueueVpsId"></param>
        /// <returns></returns>
        public static bool SendOnReady(int pQueueVpsId)
        {
            bool result = false;

            List<string> recipient = new List<string>();
            List<string> deviceList = new List<string>();       // 2018.03.14 paulus: log 邊個 device 會收到 FCM
            List<string> useridList = new List<string>();       // 2018.03.14 paulus: log 邊個 user 會收到 FCM

            int notifyKF = (int)EnumHelper.User.NotifyType.OnReady_KF;
            int notifyKT = (int)EnumHelper.User.NotifyType.OnReady_KT;
            int notifyTW = (int)EnumHelper.User.NotifyType.OnReady_TW;
            int notifyClient = (int)EnumHelper.User.NotifyType.OnReady;

            using (var ctx = new xFilmEntities())
            {
                var vps = ctx.PrintQueue_VPS.Where(x => x.ID == pQueueVpsId).SingleOrDefault();
                if (vps != null)
                {
                    var pkVps = ctx.vwOrderPkPrintQueueVpsList.Where(x => x.PrintQueueVpsId == pQueueVpsId).FirstOrDefault();
                    var order = ctx.vwOrderList.Where(x => x.OrderID == pkVps.OrderHeaderId).SingleOrDefault();
                    //var client = ctx.vwClientList.Where(x => x.ID == vps.PrintQueue.ClientID).SingleOrDefault();
                    //var users = ctx.Client_User.Where(x => x.ClientID == vps.PrintQueue.ClientID || x.SecurityLevel > 1);     // client + staff

                    #region 將有開嘅 user fcm token 加入 recipient list
                    switch (order.Workshop.Substring(0, 2).ToLower())
                    {   // staff 可以分開 branch
                        case "kf":
                            var kf = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyKF || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (kf.Count > 0)
                            {
                                for (int i = 0; i < kf.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(kf[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(kf[i].DeviceId))))
                                            deviceList.Add(kf[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(kf[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "kt":
                            var kt = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyKT || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (kt.Count > 0)
                            {
                                for (int i = 0; i < kt.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(kt[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(kt[i].DeviceId))))
                                            deviceList.Add(kt[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(kt[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "tw":
                            var tw = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyTW || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (tw.Count > 0)
                            {
                                for (int i = 0; i < tw.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(tw[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(tw[i].DeviceId))))
                                            deviceList.Add(tw[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(tw[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                    }
                    #endregion

                    if (recipient.Count > 0)
                    {
                        var deviceIds = string.Join(",", recipient.Where(x => x != String.Empty).ToArray());
                        var msgTitle = "x5 有貨";
                        var msgBody = String.Format("{0}: {1}", pkVps.OrderHeaderId.ToString(), vps.VpsFileName);

                        result = SendPushNotification(Config.FCM_ServerKey, deviceIds, Config.FCM_SenderId, msgTitle, msgBody);

                        #region 有 userid，log it
                        if (useridList.Count > 0)
                        {
                            var devIds = string.Join(",", deviceList.ToArray());
                            var userIds = string.Join(",", useridList.ToArray());
                            var hst = new FCMHistory();
                            hst.MessageTitle = msgTitle;
                            hst.MessageBody = msgBody;
                            hst.DeliveredOn = DateTime.Now;
                            hst.Topic = "Device";
                            hst.RecipientList = deviceIds;
                            hst.UserIdList = userIds;

                            var okay = FCMHistoryHelper.WriteHistory(hst);
                        }
                        #endregion
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 根據 order id 發 FCM 俾 Client & Staff
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static bool SendOnOrder(int orderId)
        {
            bool result = false;

            List<string> recipient = new List<string>();
            List<string> deviceList = new List<string>();       // 2018.03.14 paulus: log 邊個 device 會收到 FCM
            List<string> useridList = new List<string>();       // 2018.03.14 paulus: log 邊個 user 會收到 FCM

            int notifyKF = (int)EnumHelper.User.NotifyType.OnOrder_KF;
            int notifyKT = (int)EnumHelper.User.NotifyType.OnOrder_KT;
            int notifyTW = (int)EnumHelper.User.NotifyType.OnOrder_TW;
            int notifyClient = (int)EnumHelper.User.NotifyType.OnOrder;

            using (var ctx = new xFilmEntities())
            {
                var order = ctx.vwOrderList.Where(x => x.OrderID == orderId).SingleOrDefault();
                if (order != null)
                {
                    #region 將有開嘅 user fcm token 加入 recipient list
                    switch (order.Workshop.Substring(0, 2).ToLower())
                    {   // staff 可以分開 branch
                        case "kf":
                            var kf = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyKF || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (kf.Count > 0)
                            {
                                for (int i = 0; i < kf.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(kf[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(kf[i].DeviceId))))
                                            deviceList.Add(kf[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(kf[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "kt":
                            var kt = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyKT || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (kt.Count > 0)
                            {
                                for (int i = 0; i < kt.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(kt[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(kt[i].DeviceId))))
                                            deviceList.Add(kt[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(kt[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "tw":
                            var tw = ctx.vwUserNotificationList.Where(x => (x.NotifyType == notifyTW || x.NotifyType == notifyClient) && (x.SecurityLevel > 1 || x.ClientId == order.ClientID)).ToList();
                            #region 每隻登記咗嘅 device 都要發 FCM
                            if (tw.Count > 0)
                            {
                                for (int i = 0; i < tw.Count; i++)
                                {
                                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(tw[i].AuthXml);
                                    if (expando != null)
                                    {
                                        recipient.Add(expando.FCM.Token);

                                        if (!(deviceList.Any(x => x.Contains(tw[i].DeviceId))))
                                            deviceList.Add(tw[i].DeviceId);
                                        var userSid = UserHelper.GetUserSid(tw[i].UserId);
                                        if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                            useridList.Add(userSid.ToString());
                                    }
                                }
                            }
                            #endregion
                            break;
                    }
                    #endregion

                    if (recipient.Count > 0)
                    {
                        var deviceIds = string.Join(",", recipient.Where(x => x != String.Empty).ToArray());
                        var msgTitle = "x5 有柯打";
                        var msgBody = String.Format("單號：{0}，客名：{1}", orderId.ToString(), order.ClientName);

                        result = SendPushNotification(Config.FCM_ServerKey, deviceIds, Config.FCM_SenderId, msgTitle, msgBody);

                        #region 有 userid，log it
                        if (useridList.Count > 0)
                        {
                            var devIds = string.Join(",", deviceList.ToArray());
                            var userIds = string.Join(",", useridList.ToArray());
                            var hst = new FCMHistory();
                            hst.MessageTitle = msgTitle;
                            hst.MessageBody = msgBody;
                            hst.DeliveredOn = DateTime.Now;
                            hst.Topic = "Device";
                            hst.RecipientList = deviceIds;
                            hst.UserIdList = userIds;

                            var okay = FCMHistoryHelper.WriteHistory(hst);
                        }
                        #endregion
                    }
                }
            }

            return result;
        }

        public static bool SendOnEasyRipUploaded(int userId, string filename)
        {
            bool result = false;

            List<string> recipient = new List<string>();
            List<string> deviceList = new List<string>();       // 2018.03.14 paulus: log 邊個 device 會收到 FCM
            List<string> useridList = new List<string>();       // 2018.03.14 paulus: log 邊個 user 會收到 FCM

            int notifyType = (int)EnumHelper.User.NotifyType.OnVps;

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.User.Where(x => x.UserId == userId && x.Status >= 1).SingleOrDefault();
                if (user != null)
                {
                    #region 將有開嘅 user fcm token 加入 recipient list, 每隻登記咗嘅 device 都要發 FCM
                    var vpsList = ctx.vwUserNotificationList.Where( x => (x.NotifyType == notifyType) && (x.UserId == userId) ).ToList();

                    if (vpsList.Count > 0)
                    {
                        for (int i = 0; i < vpsList.Count; i++)
                        {
                            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(vpsList[i].AuthXml);
                            if (expando != null)
                            {
                                recipient.Add(expando.FCM.Token);

                                if (!(deviceList.Any(x => x.Contains(vpsList[i].DeviceId))))
                                    deviceList.Add(vpsList[i].DeviceId);
                                var userSid = UserHelper.GetUserSid(vpsList[i].UserId);
                                if (!(useridList.Any(x => x.Contains(userSid.ToString()))))
                                    useridList.Add(userSid.ToString());
                            }
                        }
                    }
                    #endregion

                    if (recipient.Count > 0)
                    {
                        var deviceIds = string.Join(",", recipient.Where(x => x != String.Empty).ToArray());
                        var msgTitle = "x5 輕鬆 RIP";
                        var msgBody = String.Format("收到檔案：{0}", filename);

                        result = SendPusData(Config.FCM_ServerKey, deviceIds, Config.FCM_SenderId, msgTitle, msgBody);
                    }
                }
            }

            return result;
        }

        public static bool SendEveryone(string topic, string msg)
        {
            bool result = false;

            var deviceIds = string.Format("/topics/{0}", topic);

            string msgTitle = topic.ToLower() == "everyone" ? "x5 大眾廣播" : "x5 內部廣播";

            var msgBody = msg;

            result = SendPushNotification(Config.FCM_ServerKey, deviceIds, Config.FCM_SenderId, msgTitle, msgBody);

            #region 2018.03.14 paulus: log it
            var hst = new FCMHistory();
            hst.MessageTitle = msgTitle;
            hst.MessageBody = msgBody;
            hst.DeliveredOn = DateTime.Now;
            hst.Topic = topic;
            hst.RecipientList = "";
            hst.UserIdList = "";

            var okay = FCMHistoryHelper.WriteHistory(hst);
            #endregion

            return result;
        }

        #region FCMResponse + FCMResult: objects used to deserialize Firebase response result
        /// <summary>
        /// refer: https://developers.google.com/cloud-messaging/http-server-ref#interpret-downstream
        /// eg. failure = 0 means all success no failure
        /// </summary>
        private class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        private class FCMResult
        {
            public string message_id { get; set; }
        }
        #endregion

        /** deprecated
        private static string GetFcmToken(UserNotification user)
        {
            string result = "";

            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(user.MetadataXml);
            if (expando != null) result = expando.FCM.Token;

            return result;
        }
        private static string GetFcmToken(int userId)
        {
            string result = "";

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.UserAuth.Where(x => x.UserId == userId).SingleOrDefault();
                if (user != null)
                {
                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(user.MetadataXml);
                    if (expando != null) result = expando.FCM.Token;
                }
            }
            return result;
        }
        private static string GetFcmToken(int userId, string deviceId)
        {
            string result = "";

            using (var ctx = new xFilmEntities())
            {
                var user = ctx.UserAuth.Where(x => x.UserId == userId && x.DeviceId == deviceId).SingleOrDefault();
                if (user != null)
                {
                    dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(user.MetadataXml);
                    if (expando != null) result = string.Format("{0}", expando.FCM.Token);
                }
            }
            return result;
        }
        */
    }
}
