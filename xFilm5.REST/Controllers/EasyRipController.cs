using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using log4net;

using xFilm5.EF6;
using xFilm5.REST.Filters;
using xFilm5.REST.Helper;

namespace xFilm5.REST.Controllers
{
    [RoutePrefix("api/EasyRip")]
    public class EasyRipController : ApiController
    {
        #region Instead of naming my invoking class, I started using the following:
        //private static log4net.ILog Log { get; set; }
        //ILog log = log4net.LogManager.GetLogger(typeof(BotController));

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // In this way, I can use the same line of code in every class that uses log4net without having to remember to change code when I copy and paste.
        // Alternatively, i could create a logging class, and have every other class inherit from my logging class.
        // Refer: https://stackoverflow.com/questions/7089286/correct-way-of-using-log4net-logger-naming
        #endregion

        [HttpPost]
        [Route("film")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> PostEasyRipFilm()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    try
                    {
                        var clientUser = ctx.vwClientUserList.Where(x => x.UserId == user.UserId).SingleOrDefault();
                        int clientId = clientUser == null ? 0 : clientUser.ClientId;

                        HttpRequestMessage request = this.Request;
                        if (!request.Content.IsMimeMultipartContent())
                        {
                            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                        }

                        var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());     // upload file 暫存 temporaray folder，唔使理 permission

                        await request.Content.ReadAsMultipartAsync(provider);

                        #region 解碼 FormData 

                        //var fdata = provider.FormData;
                        bool flag;

                        var positive = HttpContext.Current.Request.Form["positive"] != null ? bool.TryParse(HttpContext.Current.Request.Form["positive"], out flag) : false;
                        var negative = HttpContext.Current.Request.Form["negative"] != null ? bool.TryParse(HttpContext.Current.Request.Form["negative"], out flag) : false;
                        var emulsionUp = HttpContext.Current.Request.Form["emulsion-up"] != null ? bool.TryParse(HttpContext.Current.Request.Form["emulsion-up"], out flag) : false;
                        var emulsionDown = HttpContext.Current.Request.Form["emulsion-down"] != null ? bool.TryParse(HttpContext.Current.Request.Form["emulsion-down"], out flag) : false;
                        var colorSeparation = HttpContext.Current.Request.Form["color-separation"] != null ? bool.TryParse(HttpContext.Current.Request.Form["color-separation"], out flag) : false;

                        #endregion

                        if (provider.Contents.Count > 0)
                        {
                            #region 準備啲 params
                            String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                            String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                            String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];
                            String speedBox_HotFolder = ConfigurationManager.AppSettings["SpeedBox_HotFolder"];
                            String speedBox_TempFolder = ConfigurationManager.AppSettings["SpeedBox_TempFolder"];
                            #endregion

                            foreach (MultipartFileData file in provider.FileData)
                            {
                                #region 砌啲 file names 同埋 file paths
                                String srcFileNPath = file.LocalFileName;
                                String orgFileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                                String moddedFileName = String.Format("{0}.{1}.{2}.{3}",                                    // moddedFileName: 照 xFilm5.AtsPane.JobOrderAts 砌個檔案名
                                        colorSeparation ? "Film4C" : "Film",
                                        positive ? "P" : "N",
                                        emulsionUp ? "U" : "D",
                                        orgFileName);

                                String tempPath = serverUri + speedBox_TempFolder;
                                String tempFileName = String.Format("{0}_{1}", Guid.NewGuid().ToString(), orgFileName);     // tempFileName = [new guid]_[Original File Name]
                                String tempFilePath = Path.Combine(tempPath, tempFileName);                                 // tempFilePath = \\[SpeedBox_ServerUri]\[SpeedBox_TempFolder]\[tempFileName]
                                #endregion


                                #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation 抄檔案
                                using (new Impersonation(serverUri, userName, userPassword))
                                {
                                    if (!(Directory.Exists(tempPath)))                                  // 抄去 shared folder on server 然後叫 Bot Server 做嘢
                                        Directory.CreateDirectory(tempPath);
                                    if (File.Exists(tempFilePath))
                                        File.Delete(tempFilePath);
                                    File.Move(srcFileNPath, tempFilePath);

                                    //BotHelper.PostSpeedBox(clientId, tempFileName, moddedFileName);      // 通知 Bot Server 跟進

                                    log.Info(string.Format("[rest, easyrip, film] File [{0}] uploaded to [{1}]", orgFileName, tempFilePath));
                                }
                                #endregion
                            }

                            return Request.CreateResponse(HttpStatusCode.OK, "File uploaded.");
                        }
                        else
                        {
                            log.Error("[rest, easyrip, film] Upload failed...No file found");
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...No file found");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("[rest, easyrip, film, Exceptional Error]", ex);

                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }

                log.Error("[rest, easyrip, film] Upload failed...Invalid user");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...Invalid user");
            }
        }

        [HttpPost]
        [Route("plate")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> PostEasyRipPlate()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                if (user != null)
                {
                    try
                    {
                        var clientUser = ctx.vwClientUserList.Where(x => x.UserId == user.UserId).SingleOrDefault();
                        int clientId = clientUser == null ? 0 : clientUser.ClientId;

                        HttpRequestMessage request = this.Request;
                        if (!request.Content.IsMimeMultipartContent())
                        {
                            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                        }

                        var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());     // upload file 暫存 temporaray folder，唔使理 permission

                        await request.Content.ReadAsMultipartAsync(provider);

                        #region 解碼 FormData 

                        //var fdata = provider.FormData;
                        bool flag;

                        var greyscale = HttpContext.Current.Request.Form["greyscale"] != null ? bool.TryParse(HttpContext.Current.Request.Form["greyscale"], out flag) : false;
                        var blackOverprint = HttpContext.Current.Request.Form["black-overprint"] != null ? bool.TryParse(HttpContext.Current.Request.Form["black-overprint"], out flag) : false;
                        var spotToCMYK = HttpContext.Current.Request.Form["spot-to-cmyk"] != null ? bool.TryParse(HttpContext.Current.Request.Form["spot-to-cmyk"], out flag) : false;
                        var dotGain50 = HttpContext.Current.Request.Form["dot-gain-50"] != null ? bool.TryParse(HttpContext.Current.Request.Form["dot-gain-50"], out flag) : false;
                        var dotGain43 = HttpContext.Current.Request.Form["dot-gain-43"] != null ? bool.TryParse(HttpContext.Current.Request.Form["dot-gain-43"], out flag) : false;
                        var dotGain40 = HttpContext.Current.Request.Form["dot-gain-40"] != null ? bool.TryParse(HttpContext.Current.Request.Form["dot-gain-40"], out flag) : false;

                        #endregion

                        if (provider.Contents.Count > 0)
                        {
                            //var srcFileNPath = provider.FileData.First().LocalFileName;                                 // srcFilePath = [%TEMP%]\\BodyPart_[guid]
                            //var orgFileName = provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                            #region 準備啲 params
                            String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                            String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                            String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];
                            String speedBox_HotFolder = ConfigurationManager.AppSettings["SpeedBox_HotFolder"];
                            String speedBox_TempFolder = ConfigurationManager.AppSettings["SpeedBox_TempFolder"];

                            var Ax = "A0";
                            if (greyscale)
                                Ax = "A1";
                            else if (blackOverprint && !spotToCMYK)
                                Ax = "A2";
                            else if (blackOverprint && spotToCMYK)
                                Ax = "A3";
                            else if (!blackOverprint && spotToCMYK)
                                Ax = "A4";

                            var Bx = "B0";
                            if (dotGain50)
                                Bx = "B1";
                            else if (dotGain43)
                                Bx = "B2";
                            else if (dotGain40)
                                Bx = "B3";

                            //String moddedFileName = String.Format("Plate.{0}.{1}.{2}", Ax, Bx, orgFileName);             // moddedFileName: 照 xFilm5.AtsPane.JobOrderAts 砌個檔案名

                            //String tempPath = serverUri + speedBox_TempFolder;
                            //String tempFileName = String.Format("{0}_{1}", Guid.NewGuid().ToString(), orgFileName);     // tempFileName = [new guid]_[Original File Name]
                            //String tempFilePath = Path.Combine(tempPath, tempFileName);                                 // tempFilePath = \\[SpeedBox_ServerUri]\[SpeedBox_TempFolder]\[tempFileName]

                            //var uri = new Uri(serverUri);
                            //System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                            #endregion

                            foreach (MultipartFileData file in provider.FileData)
                            {
                                #region 砌啲 file names 同埋 file paths
                                String srcFileNPath = file.LocalFileName;
                                String orgFileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                                String moddedFileName = String.Format("Plate.{0}.{1}.{2}", Ax, Bx, orgFileName);            // moddedFileName: 照 xFilm5.AtsPane.JobOrderAts 砌個檔案名

                                String tempPath = serverUri + speedBox_TempFolder;
                                String tempFileName = String.Format("{0}_{1}", Guid.NewGuid().ToString(), orgFileName);     // tempFileName = [new guid]_[Original File Name]
                                String tempFilePath = Path.Combine(tempPath, tempFileName);                                 // tempFilePath = \\[SpeedBox_ServerUri]\[SpeedBox_TempFolder]\[tempFileName]
                                #endregion

                                #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation 抄檔案

                                using (new Impersonation(serverUri, userName, userPassword))
                                {
                                    if (!(Directory.Exists(tempPath)))                                  // 抄去 shared folder on server 然後叫 Bot Server 做嘢
                                        Directory.CreateDirectory(tempPath);
                                    if (File.Exists(tempFilePath))
                                        File.Delete(tempFilePath);
                                    File.Move(srcFileNPath, tempFilePath);

                                    //BotHelper.PostSpeedBox(clientId, tempFileName, moddedFileName);      // 通知 Bot Server 跟進

                                    log.Info(string.Format("[rest, easyrip, plate] File [{0}] uploaded to [{1}]", orgFileName, tempFilePath));
                                }
                                #endregion
                            }
                            
                            return Request.CreateResponse(HttpStatusCode.OK, "File uploaded.");
                        }
                        else
                        {
                            log.Error("[rest, easyrip, plate] Upload failed...No file found");
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...No file found");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("[rest, easyrip, plate, Exceptional Error]", ex);

                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }

                log.Error("[rest, easyrip, plate] Upload failed...Invalid user");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...Invalid user");
            }
        }
    }
}
