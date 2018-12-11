using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

namespace xFilm5.REST.Controllers
{
    public class DashboardController : ApiController
    {
        [HttpPost]
        [Route("api/speedbox/upload/plate/{clientId:int}")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> PostSpeedBoxPlate(int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                if (client != null)
                {
                    var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                    if (user != null)
                    {
                        try
                        {
                            HttpRequestMessage request = this.Request;
                            if (!request.Content.IsMimeMultipartContent())
                            {
                                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                            }
                            var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());     // upload file 暫存 temporaray folder，唔使理 permission

                            var task = await request.Content.ReadAsMultipartAsync(provider).
                                ContinueWith<HttpResponseMessage>(o =>
                                {
                                    /** 唔使 loop，暫時得一個 file
                                    foreach (MultipartFileData file in provider.FileData)
                                    {
                                        var filename = file.Headers.ContentDisposition.FileName;
                                        var localFilePath = "Server file path: " + file.LocalFileName;
                                    }
                                    */

                                    if (provider.Contents.Count > 0)
                                    {
                                        var tmpFileName = provider.FileData.First().LocalFileName;
                                        var orgFileName = provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                                        String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                                        String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                                        String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];

                                        String cacheFileName = String.Format("{0}_{1}", Guid.NewGuid(), orgFileName);   //  加個 Guid 喺前而面
                                        String cachePath = String.Format("{0}{1}", serverUri, ConfigurationManager.AppSettings["SpeedBox_TempFolder"]);
                                        String cacheFilePath = Path.Combine(cachePath, cacheFileName);
                                        #endregion

                                        #region 抄去 server
                                        using (new Impersonation(serverUri, userName, userPassword))
                                        {
                                            File.Copy(tmpFileName, cacheFilePath, true);
                                            File.Delete(tmpFileName);                      // Copy is synchronous operation, delete 應該唔會有 error

                                            var filename = "Plate." + orgFileName;
                                            Helper.BotHelper.PostSpeedBox(clientId, cacheFileName, filename);      // 再交俾 BotServer 處理
                                        }
                                        #endregion

                                        return Request.CreateResponse(HttpStatusCode.OK, string.Format("File {0} uploaded...{1}", cacheFileName, tmpFileName));
                                    }
                                    else
                                    {
                                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...No file found");
                                    }
                                }
                            );
                            return task;
                        }
                        catch (Exception ex)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                        }
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...Product not found");
            }
        }

        [HttpPost]
        [Route("api/speedbox/upload/film/{positive}/{emulsionup}/{clientId:int}")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> PostSpeedBoxFilm(String positive, String emulsionUp, int clientId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Guid userSid = Guid.Empty;
            userSid = Guid.TryParse(identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault(), out userSid) ? userSid : Guid.Empty;

            using (var ctx = new xFilmEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                if (client != null)
                {
                    var user = ctx.User.Where(x => x.UserSid == userSid).SingleOrDefault();
                    if (user != null)
                    {
                        try
                        {
                            HttpRequestMessage request = this.Request;
                            if (!request.Content.IsMimeMultipartContent())
                            {
                                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                            }
                            var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());     // upload file 暫存 temporaray folder，唔使理 permission

                            var task = await request.Content.ReadAsMultipartAsync(provider).
                                ContinueWith<HttpResponseMessage>(o =>
                                {
                                    /** 唔使 loop，暫時得一個 file
                                    foreach (MultipartFileData file in provider.FileData)
                                    {
                                        var filename = file.Headers.ContentDisposition.FileName;
                                        var localFilePath = "Server file path: " + file.LocalFileName;
                                    }
                                    */

                                    if (provider.Contents.Count > 0)
                                    {
                                        var tmpFileName = provider.FileData.First().LocalFileName;
                                        var orgFileName = provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                                        #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                                        String serverUri = ConfigurationManager.AppSettings["SpeedBox_ServerUri"];
                                        String userName = ConfigurationManager.AppSettings["SpeedBox_UserName"];
                                        String userPassword = ConfigurationManager.AppSettings["SpeedBox_UserPassword"];

                                        String cacheFileName = String.Format("{0}_{1}", Guid.NewGuid(), orgFileName);   //  加個 Guid 喺前而面
                                        String cachePath = String.Format("{0}{1}", serverUri, ConfigurationManager.AppSettings["SpeedBox_TempFolder"]);
                                        String cacheFilePath = Path.Combine(cachePath, cacheFileName);
                                        #endregion

                                        #region 抄去 server
                                        using (new Impersonation(serverUri, userName, userPassword))
                                        {
                                            File.Copy(tmpFileName, cacheFilePath, true);
                                            File.Delete(tmpFileName);                      // Copy is synchronous operation, delete 應該唔會有 error

                                            var filename = String.Format("Film.{0}.{1}.{2}",
                                                positive.ToLower() == "p" ? "P" : "N",
                                                emulsionUp.ToLower() == "u" ? "U" : "D",
                                                orgFileName);

                                            Helper.BotHelper.PostSpeedBox(clientId, cacheFileName, filename);      // 再交俾 BotServer 處理
                                        }
                                        #endregion

                                        return Request.CreateResponse(HttpStatusCode.OK, string.Format("File {0} uploaded...{1}", cacheFileName, tmpFileName));
                                    }
                                    else
                                    {
                                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...No file found");
                                    }
                                }
                            );
                            return task;
                        }
                        catch (Exception ex)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                        }
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Upload failed...Product not found");
            }
        }
    }
}
