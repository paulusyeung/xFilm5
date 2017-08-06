using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.QRStation.Helper
{
    public class BotHelper
    {
        public static bool PostSendFcmOnOrder(int vpsId)
        {
            String botServer = "http://192.168.12.143/xFilm5.Bot";  // HACK: 唔想搞隻 App.Config :)     ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("FCM/SendMessage/OnReady/{0}/", vpsId.ToString()), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                OrderId = vpsId.ToString(),
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }
    }
}
