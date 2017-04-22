using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
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

        public static void PostXprinter(int receiptId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
//#if (DEBUG)
//            botServer = "http://localhost:35543/";
//#endif
            var client = new RestClient(botServer);
            var request = new RestRequest("xprinter/", Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            try
            {
                //serialize an object to JSON and set it as content for a request
                //request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                //request.AddJsonBody(obj);
                //PrintServer pServer = new PrintServer(@"\\192.168.2.223", PrintSystemDesiredAccess.EnumerateServer);
                //var pQueues = pServer.GetPrintQueues();
            }
            catch { }

            //LocalPrintServer server = new LocalPrintServer();
            //var pq = server.DefaultPrintQueue;
            //var pqs = server.GetPrintQueues();

            String printerName = Controls.Utility.Config.Xprinter_KT;   // @"\\192.168.2.223\KT-XP80C";
            //String printerName = @"\\http://192.168.2.223:631\KT-XP80C";

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = DAL.Common.Config.CurrentLanguageId.ToString(),
                PrinterName = printerName,
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
        }
    }
}
