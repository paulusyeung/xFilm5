﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace xFilm5.Api.Helper
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

        /** Not used in here
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

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = DAL.Common.Config.CurrentLanguageId.ToString(),
                PrinterName = printerName,
                SmallFont = smallFont.ToString(),
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
        }
        */

        /** Not used in here
        public static bool PostEmailReceipt(int receiptId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest("email/receipt/", Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            #region 準備要用到嘅 data: Recipients
            String recipients = "";
            using (var ctx = new EF6.xFilmEntities())
            {
                var receipt = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == receiptId).SingleOrDefault();
                recipients = Helper.ClientHelper.GetEmailRecipient(receipt.ClientId);
            }
            #endregion

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                ReceiptId = receiptId.ToString(),
                LanguageId = DAL.Common.Config.CurrentLanguageId.ToString(),
                Recipient = recipients,
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }
        */

        public static bool PostEmailReceipt(int receiptId, string recipients)
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
                LanguageId = DAL.Common.Config.CurrentLanguageId.ToString(),
                Recipient = recipients,
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostSendFcmOnVps(int clientId, string vpsFileName)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest("FCM/SendMessage/OnVps/", Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                ClientId = clientId.ToString(),
                VpsFileName = vpsFileName,
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

        public static bool PostSendFcmOnReady(int vpsId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
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
                PrintQueueVpsId = vpsId.ToString(),
                AnotherParam = 19.99
            });
            var response = client.Execute(request);
            return ((response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostBroadcastFcm(String topic, String msg)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(string.Format("FCM/BroadcastMessage/{0}/{1}/", topic, msg), Method.POST);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("ReceiptId", receiptId.ToString());
            //request.AddParameter("LanguageId", DAL.Common.Config.CurrentLanguageId.ToString());
            //request.AddParameter("PrinterName", printerName);
            request.AddBody(new
            {
                Topic = topic,
                AnotherParam = 19.99
            });
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

        public static bool PostCloudDisk_MigrateFile(int clientId, int userId)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/MigrateFile/{0}/{1}/", clientId.ToString(), userId.ToString()), Method.POST);

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

        #region Special Edition for xFilm5.Api

        /// <summary>
        /// Fire and forget, let Hangfire take over
        /// </summary>
        /// <param name="cupsJobTitle"></param>
        /// <returns></returns>
        public static bool PostCloudDisk_ApiCupsUploadFile(String cupsJobTitle)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/ApiCupsUploadFile/{0}/", cupsJobTitle), Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                CupsJobTitle = cupsJobTitle,
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
            return ((result.StatusCode == System.Net.HttpStatusCode.Accepted || result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        /// <summary>
        /// Fire and forget, let Hangfire take over
        /// </summary>
        /// <param name="vpsFileName"></param>
        /// <returns></returns>
        public static bool PostCloudDisk_ApiVpsUploadFile(String vpsFileName)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/ApiVpsUploadFile/{0}/", vpsFileName), Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                VpsFileName = vpsFileName,
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
            return ((result.StatusCode == System.Net.HttpStatusCode.Accepted || result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        /// <summary>
        /// Fire and forget, let Hangfire take over
        /// </summary>
        /// <param name="cip3FileName"></param>
        /// <returns></returns>
        public static bool PostCloudDisk_ApiCip3UploadFile(String cip3FileName)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/ApiCip3UploadFile/{0}/", cip3FileName), Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                Cip3FileName = cip3FileName,
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
            return ((result.StatusCode == System.Net.HttpStatusCode.Accepted || result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        public static bool PostCloudDisk_ApiLowResTiffUploadFile(String tiffFileName)
        {
            String botServer = ConfigurationManager.AppSettings["BotServer"];
            //#if (DEBUG)
            //            botServer = "http://localhost:35543/";
            //#endif
            var client = new RestClient(botServer);
            var request = new RestRequest(String.Format("CloudDisk/ApiLowResTiffUploadFile/{0}/", tiffFileName), Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                VpsFileName = tiffFileName,
                AnotherParam = 19.99
            });
            var result = client.Execute(request);
            return ((result.StatusCode == System.Net.HttpStatusCode.Accepted || result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false);
        }

        #endregion
    }
}