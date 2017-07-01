using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

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

            #region 2017.05.14 paulus: 加個 SmallFont option
            bool smallFont = false;
            using (var ctx = new EF6.xFilmEntities())
            {
                var rHdr = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptId).SingleOrDefault();
                if (rHdr != null)
                {
                    smallFont = Helper.ClientHelper.IsReceiptSmallFont(rHdr.ClientId);
                }
            }
            #endregion

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = CommonHelper.Config.CurrentLanguageId.ToString(),
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
                LanguageId = ClientHelper.GetDefaultLanguageId(clientId).ToString(),
                Recipient = recipients,
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }
    }
}
