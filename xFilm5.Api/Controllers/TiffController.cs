using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using xFilm5.EF6;

namespace xFilm5.Api.Controllers
{
    public class TiffController : ApiController
    {
        [HttpGet]
        [Route("api/Tiff/Preview/{mode:int}/{filename}")]
        public HttpResponseMessage GetTiffPreview(int mode, String filename)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

            #region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
            String serverUri = ConfigurationManager.AppSettings["PPic_ServerUri"];
            String userName = ConfigurationManager.AppSettings["PPic_UserName"];
            String userPassword = ConfigurationManager.AppSettings["PPic_UserPassword"];
            String sourecPath = serverUri + ConfigurationManager.AppSettings["PPic_SourcePath"];
            //String filePath = Path.Combine(sourecPath, product.StockNumber + @"\" + filename);

            var uri = new Uri(serverUri);
            //System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
            #endregion

            //var uri = new Uri(sourecPath);
            String imgFile = String.Empty;

            #region imgFile 分單色同彩色，單色用 bmp，彩色用 tif
            switch (mode)
            {
                case 1:
                    #region tiff = 202020.A1245-745x605-TEST.p1(CMYK).tif
                    imgFile = Path.Combine(sourecPath, filename.Substring(0, filename.IndexOf('(')) + "(CMYK).tif");
                    #endregion
                    break;
                case 2:
                    #region bitmap = 202020.A1245-745x605-TEST.p1(C).bmp
                    imgFile = Path.Combine(sourecPath, filename + ".bmp");
                    #endregion
                    break;
            }
            #endregion

            // 用 NetworkConnection.cs 做 impersonation，shared folder 係 synologies
            System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);

            //using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
            using (new Impersonation(serverUri, userName, userPassword))
            {
                // 順利攞到 access permission
                if (File.Exists(imgFile))
                {
                    #region 有檔案,砌隻 http response 
                    var suffix = Path.GetExtension(imgFile);
                    var buffer = File.ReadAllBytes(imgFile);

                    var contentLength = buffer.Length;

                    //200
                    //successful
                    var statuscode = HttpStatusCode.OK;
                    response = Request.CreateResponse(statuscode);
                    response.Content = new StreamContent(new MemoryStream(buffer));
                    switch (suffix)
                    {
                        case "bmp":
                            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/bmp");
                            break;
                        case "tif":
                            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/tiff");
                            break;
                    }
                    response.Content.Headers.ContentLength = contentLength;

                    ContentDispositionHeaderValue contentDisposition = null;
                    if (ContentDispositionHeaderValue.TryParse("inline; filename=" + filename, out contentDisposition))
                    {
                        response.Content.Headers.ContentDisposition = contentDisposition;
                    }
                    #endregion
                }
                else
                {
                    var message = String.Format("Unable to find resource. Resource \"{0}\" may not exist.", filename);
                    HttpError err = new HttpError(message);
                    response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                }
            }
            return response;

            /**
            using (var ctx = new JB5Entities())
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);

                //ctx.Configuration.LazyLoadingEnabled = false;
                var product = ctx.Product.Where(x => x.ProductId == id).SingleOrDefault();
                if (product != null)
                {
                    var filename = ProductHelper.GetAttachmentFileName(id);
                    if (filename != "")
                    {
                        //#region 用 NetworkConnection: 來自於 NetworkConnection.cs 用嚟做 impersonation
                        //String serverUri = ConfigurationManager.AppSettings["PPic_ServerUri"];
                        //String userName = ConfigurationManager.AppSettings["PPic_UserName"];
                        //String userPassword = ConfigurationManager.AppSettings["PPic_UserPassword"];
                        //String sourecPath = serverUri + ConfigurationManager.AppSettings["PPic_SourcePath"];
                        //String filePath = Path.Combine(sourecPath, product.StockNumber + @"\" + filename);

                        //var uri = new Uri(serverUri);
                        //System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                        //#endregion

                        using (new Impersonation(serverUri, userName, userPassword))
                        {
                            // do whatever you want
                            if (File.Exists(filePath))
                            {
                                var suffix = Path.GetExtension(filePath);
                                var buffer = File.ReadAllBytes(filePath);

                                var contentLength = buffer.Length;

                                //200
                                //successful
                                var statuscode = HttpStatusCode.OK;
                                response = Request.CreateResponse(statuscode);
                                response.Content = new StreamContent(new MemoryStream(buffer));
                                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                                response.Content.Headers.ContentLength = contentLength;

                                ContentDispositionHeaderValue contentDisposition = null;
                                if (ContentDispositionHeaderValue.TryParse("inline; filename=" + filename, out contentDisposition))
                                {
                                    response.Content.Headers.ContentDisposition = contentDisposition;
                                }
                            }
                            else
                            {
                                var message = String.Format("Unable to find resource. Resource \"{0}\" may not exist.", id.ToString());
                                HttpError err = new HttpError(message);
                                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
                            }
                        }
                    }
                }

                return response;
                
            }
    */
        }
    }
}
