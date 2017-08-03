using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xFilm5.Bot.Helper;
using xFilm5.EF6;

namespace xFilm5.Bot.Controllers
{
    public class FCMController : ApiController
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(BotController));

        [HttpPost]
        [Route("FCM/SendMessage")]
        public IHttpActionResult PostSendMessage()
        {
            var json = Request.Content.ReadAsStringAsync().Result;
            dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);

            if (expando == null)
            {
                log.Error("[bot, FCM, PostSendMessage] jsonData == null");
                //return NotFound();
            }
            else
            {
                string deviceId = (string)expando.DeviceId;
                string msgTitle = (string)expando.MessageTitle;
                string msgBody = (string)expando.MessageBody;

                if (!(String.IsNullOrEmpty(deviceId)))
                {
                    using (var ctx = new xFilmEntities())
                    {
                        try
                        {
                            var result = FCMHelper.SendPushNotification(Config.FCM_ServerKey, deviceId, Config.FCM_SenderId, msgTitle, msgBody);

                            if (result)
                            {
                                log.Info(String.Format("[bot, FCM, PostSendMessage] \r\nDevice Id = {0}", deviceId));
                                return Ok();
                            }
                            else
                            {
                                log.Error(String.Format("[bot, FCM, PostSendMessage] \r\nFirebase return false\r\nDevice Id = {0}", deviceId));
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("[bot, FCM, PostSendMessage] \r\nDevice Id = {0}\r\nExceptional Error = {1}", deviceId, ex.ToString()));
                            //return NotFound();
                        }
                    }
                }
                else
                {
                    log.Error("[bot, FCM, PostSendMessage] \r\nInvaoid Device Id = " + deviceId.ToString());
                    //return NotFound();
                }
                //return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("FCM/SendMessage/OnOrder/{orderId}")]
        public IHttpActionResult PostSendOnOrder(int orderId)
        {
            var result = FCMHelper.SendOnOrder(orderId);

            if (result)
            {
                log.Info(String.Format("[bot, FCM, PostSendMessage] \r\nFirebase return success\r\nOrder Id = {0}", orderId.ToString()));
                return Ok();
            }
            else
            {
                log.Error(String.Format("[bot, FCM, PostSendMessage] \r\nFirebase return failure\r\nOrder Id = {0}", orderId.ToString()));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("FCM/BroadcastMessage/{id}")]
        public IHttpActionResult PostBroadcastMessage([FromBody] JObject jsonData)
        {
            if (jsonData == null)
            {
                //log.Error("[bot, xprinter] jsonData == null");
                return NotFound();
            }
            else
            {
                int receiptId = jsonData["ReceiptId"].Value<int>();
                int languageId = jsonData["LanguageId"].Value<int>();
                string printerName = jsonData["PrinterName"].Value<string>();
                bool smallFont = jsonData["SmallFont"].Value<bool>();

                using (var ctx = new xFilmEntities())
                {
                    var hasRows = ctx.vwReceiptDetailsList_Ex.Where(x => x.ReceiptHeaderId == receiptId).Any();
                    if (hasRows)
                    {
                        try
                        {
                            //var xp80 = new PrinterHelper();
                            //xp80.Print(receiptId, languageId, printerName, smallFont);

                            //log.Info(String.Format("[bot, xprinter, receipt printed] \r\nReceipt Number = {0}\r\nLanguage Id = {1}\r\nPrinter Name = {2}", receiptId.ToString(), languageId.ToString(), printerName));
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            //log.Error(String.Format("[bot, xprinter, print error] \r\nExceptional Error = {0}", ex.ToString()));
                            return NotFound();
                        }
                    }
                    else
                    {
                        //log.Error("[bot, xprinter, Receipt not found] \r\n" + receiptId.ToString());
                        return NotFound();
                    }
                }
            }
        }
    }
}
