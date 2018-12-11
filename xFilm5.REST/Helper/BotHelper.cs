using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using xFilm5.REST.Models;

namespace xFilm5.REST.Helper
{
    public class BotHelper
    {
        public static void PostPlate(int pqVpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("plate/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                PrintQueueVpsId = pqVpsId.ToString(),
                AnotherParam = 19.99
            });
            client.Execute(request);
        }

        public static void PostBlueprint(int pqVpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("blueprint/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                PrintQueueVpsId = pqVpsId.ToString(),
                AnotherParam = 19.99
            });
            client.Execute(request);
        }

        public static void PostCip3(int pqVpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("cip3/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                PrintQueueVpsId = pqVpsId.ToString(),
                AnotherParam = 19.99
            });
            client.Execute(request);
        }

        public static void PostFilm(int pqVpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("film/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                PrintQueueVpsId = pqVpsId.ToString(),
                AnotherParam = 19.99
            });
            client.Execute(request);
        }

        public static void PostXprinter(int receiptId, int clientId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
//#if (DEBUG)
//            botServer = "http://localhost:35543/";
//#endif
            var client = new RestClient(botServer);
            var request = new RestRequest("xprinter/", Method.POST);

            String printerName = CommonHelper.Config.Xprinter_KT;

            #region 2017.05.14 paulus: 加個 SmallFont option & Language Id
            int languageId = 1; // default 英文
            bool smallFont = false;
            using (var ctx = new EF6.xFilmEntities())
            {
                var rHdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptId).SingleOrDefault();
                if (rHdr != null)
                {
                    smallFont = Helper.ClientHelper.IsReceiptSmallFont(rHdr.ClientId);

                    languageId = Helper.ClientHelper.GetDefaultLanguageId(rHdr.ClientId);
                }
            }
            #endregion

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = languageId.ToString(),
                PrinterName = printerName,
                SmallFont = smallFont.ToString(),
                AnotherParam = 19.99
            });
            /** 好似 AddBody 係用 JsonSerializer 而 AddParameter 就冇
            request.AddParameter("ReceiptId", receiptId.ToString());
            request.AddParameter("LanguageId", ClientHelper.GetDefaultLanguageId(clientId).ToString());
            request.AddParameter("PrinterName", printerName);
            request.AddParameter("SmallFont", smallFont.ToString());
            */
            var result = client.Execute(request);
        }

        public static bool PostEmailReceipt(int receiptId, string recipients, int clientId)
        {

            #region 2017.05.14 paulus: 加個 SmallFont option & Language Id
            int languageId = 1; // default 英文
            bool smallFont = false;
            using (var ctx = new EF6.xFilmEntities())
            {
                var rHdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptId).SingleOrDefault();
                if (rHdr != null)
                {
                    smallFont = Helper.ClientHelper.IsReceiptSmallFont(rHdr.ClientId);

                    languageId = Helper.ClientHelper.GetDefaultLanguageId(rHdr.ClientId);
                }
            }
            #endregion

            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest("email/receipt/", Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = languageId.ToString(),
                Recipient = recipients,
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostEmailSmtp(string recipient, string subject, string message)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("email/smtp/", Method.POST);

            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);

            //request.AddHeader("Content-type", "application/json");
            request.AddBody(new
            {
                Recipient = recipient,
                Subject = subject,
                Message = message,
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostSendFcmOnOrder(int orderId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("FCM/SendMessage/OnOrder/{0}/", orderId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                OrderId = orderId.ToString(),
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        #region Get Cups, Cip3, Vps, Blueprint, Plate, Film, Thumbnail
        public static List<CloudDisk.ResourceInfo> GetCups(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/cups/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetCups(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/cups/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetCip3(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/cip3/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetCip3(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/cip3/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetVps(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/vps/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetVps(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/vps/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetBlueprint(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/Blueprint/group/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetBlueprint(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/Blueprint/group/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetPlate(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/plate/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetPlate(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/plate/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetFilm(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/film/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetFilm(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/film/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetThumbnail(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/thumbnail/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetThumbnail(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/thumbnail/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetTools(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/tools/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetTools(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/tools/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetSpeedBox(int clientId, int page)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/speedbox/{0}/{1}/", clientId.ToString(), page.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Page = page.ToString(),
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }

        public static List<CloudDisk.ResourceInfo> GetSpeedBox(int clientId, String keyword)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/speedbox/keyword/{0}/{1}/", clientId.ToString(), keyword);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                Keyword = keyword,
                AnotherParam = 19.99
            });
            var cups = client.Execute<List<CloudDisk.ResourceInfo>>(request);
            return cups.StatusCode == System.Net.HttpStatusCode.OK ? cups.Data : null;
        }
        #endregion

        public static List<EF6.vwClientList> GetSubAdminUsers(String workshop)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/users/subadmin/{0}/", workshop);

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                WorkshopName = workshop,
                AnotherParam = 19.99
            });
            var list = client.Execute<List<EF6.vwClientList>>(request);
            return list.StatusCode == System.Net.HttpStatusCode.OK ? list.Data : null;
        }

        public static byte[] GetThumbnail(int clientId, String filename, int width, int height)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            String uri = String.Format("clouddisk/thumbnail/{0}/{1}/{2}/{3}/", clientId.ToString(), filename, width.ToString(), height.ToString());

            var client = new RestClient(botServer);
            var request = new RestRequest(uri, Method.GET);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId,
                FileName = filename,
                Width = width,
                Height = height,
                AnotherParam = 19.99
            });
            var image = client.DownloadData(request);

            //var response = client.Execute(request);
            //var raw = (response.StatusCode == System.Net.HttpStatusCode.OK) ? response.RawBytes : null;

            return image;
        }

        public static bool PostCloudDiskActionEmail(Models.CloudDisk.ActionEmail data, int clientId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("CloudDisk/Action/Email/{0}/", clientId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDiskActionReprint(Models.CloudDisk.ActionReprintEx data, int clientId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("CloudDisk/Action/Reprint/{0}/", clientId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDiskActionOutput_Blueprint(Models.CloudDisk.ActionOutputEx data, int userId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("CloudDisk/Action/Output/Blueprint/{0}/", userId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDiskActionOutput_Plate(Models.CloudDisk.ActionOutputEx data, int userId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("CloudDisk/Action/Output/Plate/{0}/", userId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDiskActionOutput_Film(Models.CloudDisk.ActionOutputEx data, int userId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("CloudDisk/Action/Output/Film/{0}/", userId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDisk_CreateClient(int clientId, int userId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/CreateClient/{0}/{1}/", clientId.ToString(), userId.ToString()), Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                UserId = userId.ToString(),
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
            return (result.StatusCode == System.Net.HttpStatusCode.Accepted ? true : false);
        }


        /// <summary>
        /// 2018.11.17 paulus: 將 SpeedBox 收到嘅檔案上載去 CloudDisk，同時抄１份去　hotfolder
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="filepath: guid_originalfilename"></param>
        /// <param name="filename: Plate.filenam or Film.P.U.originalfilenamee"></param>
        public static void PostSpeedBox(int clientId, String filepath, String filename)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            var client = new RestClient(botServer);
            var request = new RestRequest("speedbox/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            //serialize an object to JSON and set it as content for a request
            //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            //request.AddJsonBody(obj);

            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                FilePath = filepath,
                FileName = filename,
                AnotherParam = 19.99
            });
            client.Execute(request);
        }
    }
}
