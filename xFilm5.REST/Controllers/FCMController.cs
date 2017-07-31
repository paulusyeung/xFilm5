using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;
using xFilm5.REST.Models;

namespace xFilm5.REST.Controllers
{
    public class FCMController : ApiController
    {
        [HttpPost]
        [Route("api/FCM/Register/{id}")]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PostRegister(string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            var json = Request.Content.ReadAsStringAsync().Result;

            try
            {
                #region 用 ExpandoObject 拆解個 json data
                //refer: http://www.tomdupont.net/2014/02/deserialize-to-expandoobject-with.html
                //var converter = new ExpandoObjectConverter();     用唔用真係冇分別嘅
                dynamic expando = JsonConvert.DeserializeObject<ExpandoObject>(json);
                if (expando != null)
                {
                    #region 其實可以直接用隻 ExpandoObject，轉為 local variables 方便 debug
                    string fcmToken = expando.FCM.Token;
                    string deviceId = expando.DeviceInfo.Id;
                    string deviceModel = expando.DeviceInfo.Model;
                    int platform = (int)expando.DeviceInfo.Platform;
                    string version = expando.DeviceInfo.Version;
                    DateTime registeredOn = ((DateTime)expando.RegisteredOn).ToLocalTime();
                    #endregion

                    using (var ctx = new xFilmEntities())
                    {
                        var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                        if (user != null)
                        {
                            // 用 DeviceId 做 unique key，一隻 DeviceId 一個 User
                            var auth = ctx.UserAuth.Where(x => x.DeviceId == deviceId).SingleOrDefault();
                            if (auth != null)
                            {
                                #region overwrite existing dbo.UserAuth record
                                auth.UserId = user.UserId;
                                auth.DeviceId = deviceId;
                                auth.AuthType = (int)EnumHelper.User.AuthType.Firebase;
                                auth.Platform = platform;
                                auth.MetadataXml = json;
                                ctx.SaveChanges();
                                #endregion
                            }
                            else
                            {
                                #region create a new dbo.UserAuth record
                                auth = new UserAuth();
                                auth.UserId = user.UserId;
                                auth.DeviceId = deviceId;
                                auth.AuthType = (int)EnumHelper.User.AuthType.Firebase;
                                auth.Platform = platform;
                                auth.MetadataXml = json;
                                ctx.UserAuth.Add(auth);
                                ctx.SaveChanges();
                                #endregion
                            }
                            return Ok();
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/FCM/SendMessage/{id}")]
        [JwtAuthentication]
        public IHttpActionResult PostSendMessage([FromBody] JObject jsonData)
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

        [HttpPost]
        [Route("api/FCM/BroadcastMessage/{id}")]
        [JwtAuthentication]
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
