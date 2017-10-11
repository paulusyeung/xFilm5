using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.QRStation.Helper
{
    public class ApiHelper
    {
        //String apiServer = "http://192.168.12.143/xFilm5.Api";  // HACK: 唔想搞隻 App.Config :)     ConfigurationManager.AppSettings["ApiServer"];
        static string apiServer = "http://192.168.12.143/xFilm5.Api";

        public static int GetCounter_Plate()
        {
            int result = 0;
            try
            {
                var client = new RestClient(apiServer);
                var request = new RestRequest("api/PrintQueue_LifeCycle/Counter/Plate/", Method.GET);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                // 個 Content 多咗啲 black slashes "\"，唔可以直接用
                var str = JToken.Parse(queryResult.Content).ToString();

                dynamic plate = JsonConvert.DeserializeObject<ExpandoObject>(str);
                result = (int)plate.Count;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public static int GetCounter_Blueprint()
        {
            int result = 0;
            try
            {
                var client = new RestClient(apiServer);
                var request = new RestRequest("api/PrintQueue_LifeCycle/Counter/Blueprint/", Method.GET);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                // 個 Content 多咗啲 black slashes "\"，唔可以直接用
                var str = JToken.Parse(queryResult.Content).ToString();

                dynamic plate = JsonConvert.DeserializeObject<ExpandoObject>(str);
                result = (int)plate.Count;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public static Client GetClient(int clientId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/Client/{0}/", clientId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<Client>(queryResult.Content);
        }

        public static PrintQueue GetPrintQueue(int clientId, string cupsJobId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/PrintQueue/{0}/{1}/", clientId.ToString(), cupsJobId), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<PrintQueue>(queryResult.Content);
        }

        public static PrintQueue_VPS GetPrintQueueVps(int printQueueVpsId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/PrintQueueVps/{0}/", printQueueVpsId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<PrintQueue_VPS>(queryResult.Content);
        }

        public static PrintQueue_VPS GetPrintQueueVps(int clientId, string vpsFileName)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/PrintQueueVps/{0}/{1}/", clientId.ToString(), vpsFileName), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<PrintQueue_VPS>(queryResult.Content);
        }

        public static OrderPkPrintQueueVps GetOrderPkPrintQueueVps_Plate(int vpsId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/OrderPq/Plate/{0}/", vpsId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<OrderPkPrintQueueVps>(queryResult.Content);
        }

        public static int GetOrderPkPrintQueueVps_Plate_Count(int orderHeaderId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/OrderPq/Plate/Count/{0}/", orderHeaderId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(queryResult.Content);
            return (int)result.Count;
        }

        public static int GetOrderPkPrintQueueVps_Plate_IsReadyCount(int orderHeaderId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/OrderPq/Plate/IsReady/Count/{0}/", orderHeaderId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(queryResult.Content);
            return (int)result.IsReadyCount;
        }

        public static OrderPkPrintQueueVps GetOrderPkPrintQueueVps_Blueprint(int vpsId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/PrintQueueVps/Plate/{0}/", vpsId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            return JsonConvert.DeserializeObject<OrderPkPrintQueueVps>(queryResult.Content);
        }

        public static List<PrintQueue_LifeCycle> GetPrintQueue_LifeCycle(int printQueueId)
        {
            var client = new RestClient(apiServer);
            var request = new RestRequest(string.Format("api/PrintQueue_LifeCycle/{0}/", printQueueId.ToString()), Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);

            return JsonConvert.DeserializeObject<List<PrintQueue_LifeCycle>>(queryResult.Content);
        }

        /// <summary>
        /// 去攞個 Tiff (鋅) 或者 bmp (藍紙)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static async Task<Stream> GetProductPicture(int type, string filename)
        {
            var url = apiServer + string.Format("/api/Tiff/Preview/{0}/{1}/", (type - 3).ToString(), filename);

            var httpClient = new HttpClient();
            try
            {
                var stream = await httpClient.GetStreamAsync(url);
                return stream;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool PostOrderPqVpsIsReady(OrderPkPrintQueueVps orderPqVps)
        {
            //var url = apiServer + string.Format("/api/OrderPq/IsReady/{0}/{1}/", orderPqVpsId.ToString());

            //String botServer = "http://192.168.12.143/xFilm5.Bot";  // HACK: 唔想搞隻 App.Config :)     ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif

            var json = JsonConvert.SerializeObject(orderPqVps);
            var client = new RestClient(apiServer);
            var request = new RestRequest("api/OrderPq/IsReady", Method.POST);

            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            //request.RequestFormat = DataFormat.Json;
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            //request.AddBody(new
            //{
            //    OrderId = vpsId.ToString(),
            //    AnotherParam = 19.99
            //});
            //request.AddObject(orderPqVps);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostLifeCycle(PrintQueue_LifeCycle cycle)
        {
            //var url = apiServer + string.Format("/api/OrderPq/IsReady/{0}/{1}/", orderPqVpsId.ToString());

            //String botServer = "http://192.168.12.143/xFilm5.Bot";  // HACK: 唔想搞隻 App.Config :)     ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif

            var json = JsonConvert.SerializeObject(cycle);
            var client = new RestClient(apiServer);
            var request = new RestRequest("api/PrintQueue_LifeCycle", Method.POST);

            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            //request.RequestFormat = DataFormat.Json;
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            //request.AddBody(new
            //{
            //    OrderId = vpsId.ToString(),
            //    AnotherParam = 19.99
            //});
            //request.AddObject(orderPqVps);
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }
    }
}
