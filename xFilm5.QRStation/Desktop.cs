using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;
using xFilm5.QRStation.Helper;

namespace xFilm5.QRStation
{
    public partial class Desktop : Form
    {
        #region private properties
        private string _LastQRCode = "";

        private enum SourceType
        {
            None,
            Blueprint = (int)Common.Enums.PrintQSubitemType.Blueprint,
            Plate = (int)Common.Enums.PrintQSubitemType.Plate
        }
        #endregion

        public Desktop()
        {
            InitializeComponent();

            txtQrCodeData.KeyUp += TxtQrCodeData_KeyUp;
            txtQrCodeData.Enter += TxtQrCodeData_Enter;
#if (!DEBUG)
            this.WindowState = FormWindowState.Maximized;
#endif
        }

        private void Desktop_Load(object sender, EventArgs e)
        {
            //this.Text += " " + Properties.Settings.Default.BlueprintMachines;
            //Get_xFilm5Api();

            InitializeCounter();
            //SendWhatsAppMsg();

            ClearScreen();
        }

        private void ClearScreen()
        {
            txtClientInfo.Text = "";
            txtQrCodeData.Text = "";
            lblOrderNumber.Text = "";
        }

        private void InitializeCounter()
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                try
                {
                    var plates = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQSubitemType == (int)Common.Enums.PrintQSubitemType.Plate && DbFunctions.TruncateTime(x.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                    var blueprints = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQSubitemType == (int)Common.Enums.PrintQSubitemType.Blueprint && DbFunctions.TruncateTime(x.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                    lblGoodCount.Text = plates.ToString("##0");
                    lblBadCount.Text = blueprints.ToString("##0");
                }
                catch { }
            }

            /**
            String sql = "";

            #region Good Counts
            sql = String.Format("CONVERT(varchar(10), CreatedOn, 20) = '{0}' AND (PrintQSubitemType = {1})", 
                DateTime.Now.ToString("yyyy-MM-dd"),
                Common.Enums.PrintQSubitemType.Plate.ToString("D"));
            DAL4Win.PrintQueue_LifeCycleCollection allPlate = DAL4Win.PrintQueue_LifeCycle.LoadCollection(sql);
            lblGoodCount.Text = allPlate != null ? allPlate.Count.ToString() : "0";
            #endregion

            #region Bad Counts
            sql = String.Format("CONVERT(varchar(10), CreatedOn, 20) = '{0}' AND (PrintQSubitemType = {1})",
                DateTime.Now.ToString("yyyy-MM-dd"),
                DAL4Win.Common.Enums.PrintQSubitemType.Blueprint.ToString("D"));
            DAL4Win.PrintQueue_LifeCycleCollection allBp = DAL4Win.PrintQueue_LifeCycle.LoadCollection(sql);
            lblBadCount.Text = allBp != null ? allBp.Count.ToString() : "0";
            #endregion
            */
        }

        private void TxtQrCodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                // QR Code data 有三行 text，即係入面有 CR/LF，唯有認住最後一隻 character ) 先做嘢
                if (txtQrCodeData.Text.IndexOf(')') >= 0)
                {
                    e.Handled = true;

                    // select 哂所有 text，下一個 scanned data 可以 overwrite 之前嘅
                    txtQrCodeData.Select(0, txtQrCodeData.TextLength);

                    String[] qrCodeData = txtQrCodeData.Text.Trim().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    // 2016.11.15 paulus: QR Code 有 3 行 text,
                    //   line1 = Machine Type (藍紙 or 鋅板);
                    //   line2 = 時間;
                    //   line3 = Customer Number . Cups Job Id - File Name . p#(CMYK)
                    if (qrCodeData.Length == 3)
                    {
                        // 2017.08.09 paulus: 如果同一個 QR Code 就 skip，唔好重做
                        if (_LastQRCode != qrCodeData[2])
                        {
                            #region QR Code 有料，做嘢
                            SourceType sourceType = IdentifySourceType(qrCodeData[0]);

                            String line3 = qrCodeData[2];
                            String[] line3parts = line3.Split('.');

                            int clientId = Convert.ToInt32(line3parts[0]);
                            String cupsJobId = line3parts[1].Split('-')[0];
                            String filename = line3.Replace(clientId.ToString() + ".", "");

                            #region 清空原有畫面上的資料
                            txtClientInfo.Text = String.Empty;
                            if (lvwLifeCycle.Items.Count > 0) lvwLifeCycle.Items.Clear();
                            if (picPreview.Image != null) picPreview.Image.Dispose();
                            lblOrderNumber.Text = "";
                            #endregion

                            #region 開始顯示新嘅資料
                            ShowClientInfo(clientId);                                                                   // 目前淨係顯示 Client 名，日後可以顯示其他資料

                            using (var ctx = new EF6.xFilmEntities())
                            {
                                //var pQueue = ctx.PrintQueue.Where(x => x.ClientID == clientId && x.CupsJobID == cupsJobId).SingleOrDefault();
                                var pQueue = ApiHelper.GetPrintQueue(clientId, cupsJobId);
                                if (pQueue != null)
                                {
                                    String vpsFileName = (sourceType == SourceType.Plate) ? filename + ".vps" : filename.Substring(0, filename.IndexOf('('));
                                    //var pQueueVps = ctx.PrintQueue_VPS.Where(x => x.PrintQueueID == pQueue.ID && x.VpsFileName.Contains(vpsFileName)).FirstOrDefault();
                                    var pQueueVps = ApiHelper.GetPrintQueueVps(pQueue.ID, vpsFileName);
                                    if (pQueueVps != null)
                                    {
                                        try
                                        {
                                            EF6.OrderPkPrintQueueVps orderPq = null;
                                            switch (sourceType)
                                            {
                                                case SourceType.Plate:
                                                    //orderPq = ctx.OrderPkPrintQueueVps.Where(x => x.PrintQueueVpsId == pQueueVps.ID && x.CheckedPlate == true).FirstOrDefault();
                                                    orderPq = ApiHelper.GetOrderPkPrintQueueVps_Plate(pQueueVps.ID);
                                                    break;
                                                case SourceType.Blueprint:
                                                    //orderPq = ctx.OrderPkPrintQueueVps.Where(x => x.PrintQueueVpsId == pQueueVps.ID && x.CheckedBlueprint == true).FirstOrDefault();
                                                    orderPq = ApiHelper.GetOrderPkPrintQueueVps_Blueprint(pQueueVps.ID);
                                                    break;
                                            }

                                            int pQueueVpsId = (pQueueVps == null) ? 0 : pQueueVps.ID;
                                            LogLifeCycle(sourceType, pQueue.ID, pQueueVpsId);                                   //   update Log File

                                            if (orderPq != null)
                                            {
                                                orderPq.IsReady = true;
                                                ctx.SaveChanges();

                                                //var xQty = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == orderPq.OrderHeaderId.Value && x.CheckedPlate == true).Count();
                                                var xQty = ApiHelper.GetOrderPkPrintQueueVps_Plate_Count(orderPq.OrderHeaderId.Value);
                                                //var yQty = ctx.OrderPkPrintQueueVps.Where(x => x.OrderHeaderId == orderPq.OrderHeaderId.Value && x.CheckedPlate == true && x.IsReady == true).Count();
                                                var yQty = ApiHelper.GetOrderPkPrintQueueVps_Plate_IsReadyCount(orderPq.OrderHeaderId.Value);

                                                lblOrderNumber.Text = String.Format("{0}/{1}", orderPq.OrderHeaderId.Value.ToString("###"), (xQty - yQty).ToString());              // 顯示 Order ID as Order Number
                                            }

                                            ShowPrintQLifeCycle(pQueue.ID, filename.Substring(0, filename.IndexOf('(')));       //   顯示同一隻 PrintQueue 嘅 log file

                                            Helper.BotHelper.PostSendFcmOnOrder(pQueueVpsId);       // 2017.08.04 paulus: 叫 xFIlm5.Bot server 發短訊
                                        }
                                        catch (Exception ex) { }
                                    }
                                }
                            }
                            ShowPreview(sourceType, qrCodeData[2]);                                                     // 顯示 thumbnail
                            #endregion

                            InitializeCounter();

                            /**
                            String sql = String.Format("ClientID = {0} AND CupsJobID = N'{1}'", clientId.ToString(), cupsJobId);
                            DAL4Win.PrintQueue pq = DAL4Win.PrintQueue.LoadWhere(sql);

                            // 清空原有的資料
                            txtClientInfo.Text = String.Empty;
                            if (lvwLifeCycle.Items.Count > 0) lvwLifeCycle.Items.Clear();
                            if (picPreview.Image != null) picPreview.Image.Dispose();

                            ShowClientInfo(clientId);                           // 目前淨係顯示 Client 名，日後可以顯示其他資料
                            if (pq != null)                                     // 如果已經有 PrintQueue
                            {
                                sql = String.Format("PrintQueueID = '{0}' AND VpsFileName = N'{1}.vps'", pq.ID.ToString(), filename);
                                DAL4Win.PrintQueue_VPS pQueueVps = DAL4Win.PrintQueue_VPS.LoadWhere(sql);
                                int pQueueVpsId = (pQueueVps == null) ? 0 : pQueueVps.ID;

                                LogLifeCycle(sourceType, pq.ID, pQueueVpsId);   //   update Log File
                                ShowPrintQLifeCycle(pq.ID, filename.Substring(0, filename.IndexOf('(')));                     //   顯示同一隻 PrintQueue 嘅 log file
                            }
                            ShowPreview(sourceType, qrCodeData[2]);             // 顯示 thumbnail

                            InitializeCounter();
                            //IncrementCounter(sourceType);
                            */
                            #endregion

                            _LastQRCode = qrCodeData[2];
                        }
                    }
                }
            }
        }

        private void TxtQrCodeData_Enter(object sender, EventArgs e)
        {
            txtQrCodeData.Select(0, txtQrCodeData.TextLength);
        }

        private void IncrementCounter(SourceType type)
        {
            switch (type)
            {
                case SourceType.Plate:
                    lblGoodCount.Text = (Convert.ToInt16(lblGoodCount.Text) + 1).ToString();
                    break;
                case SourceType.Blueprint:
                    lblBadCount.Text = (Convert.ToInt16(lblGoodCount.Text) + 1).ToString();
                    break;
            }
        }

        private SourceType IdentifySourceType(String target)
        {
            SourceType result = SourceType.None;

            //var bp = ConfigurationManager.AppSettings["BlueprintMachines"];
            String bMachines = Properties.Settings.Default.BlueprintMachines;
            String pMachines = Properties.Settings.Default.CtpMachines;

            if (bMachines.ToUpper().Contains(target.ToUpper()))
                result = SourceType.Blueprint;
            else if (pMachines.ToUpper().Contains(target.ToUpper()))
            {
                result = SourceType.Plate;
            }

            return result;
        }

        private void ShowClientInfo(int clientId)
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                //var client = ctx.Client.Where(x => x.ID == clientId).SingleOrDefault();
                var client = ApiHelper.GetClient(clientId);
                if (client != null)
                {
                    txtClientInfo.Text = client.Name;
                }
            }
            /*
            DAL4Win.Client client = DAL4Win.Client.Load(clientId);
            if (client != null)
            {
                txtClientInfo.Text = client.Name;
            }
            */
        }

        private void ShowPrintQLifeCycle(int printQueueId, String pageName)
        {
            using (var ctx = new EF6.xFilmEntities())
            {
                //var allCycle = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueId == printQueueId).OrderBy(x => x.CreatedOn).ToList();
                var allCycle = ApiHelper.GetPrintQueue_LifeCycle(printQueueId);

                for (int i = 0; i < allCycle.Count; i++)
                {
                    var cycle = allCycle[i];
                    #region 祇顯示同一個 page 嘅 VPS + Plate
                    bool samepage = true;
                    if (cycle.PrintQueueVpsId != null)
                    {
                        /** 改用 xFilm5.Api
                        //DAL4Win.PrintQueue_VPS vps = DAL4Win.PrintQueue_VPS.Load(cycle.PrintQueueVpsId);
                        //var vps = ctx.PrintQueue_VPS.Where(x => x.ID == cycle.PrintQueueVpsId).SingleOrDefault();

                        //if (vps.VpsFileName.IndexOf(pageName) >= 0)

                        if (cycle.PrintQueue_VPS.VpsFileName.IndexOf(pageName) >= 0)
                            samepage = true;
                        else
                            samepage = false;
                        */

                        var pqVps = ApiHelper.GetPrintQueueVps(cycle.PrintQueueVpsId.Value);
                        if (pqVps.VpsFileName.IndexOf(pageName) >= 0)
                            samepage = true;
                        else
                            samepage = false;
                    }
                    #endregion
                    if (samepage)
                    {
                        ListViewItem item = lvwLifeCycle.Items.Add(allCycle[i].LifeCycleId.ToString());

                        item.SubItems.Add((i + 1).ToString());
                        item.SubItems.Add(cycle.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"));

                        #region Subitem.Add(SubitemType
                        String type = String.Empty;
                        switch (cycle.PrintQSubitemType)
                        {
                            case (int)Common.Enums.PrintQSubitemType.Ps:
                                type = Common.Enums.PrintQSubitemType.Ps.ToString("G");
                                break;
                            case (int)Common.Enums.PrintQSubitemType.Vps:
                                type = Common.Enums.PrintQSubitemType.Vps.ToString("G");
                                break;
                            case (int)Common.Enums.PrintQSubitemType.Cip3:
                                type = Common.Enums.PrintQSubitemType.Cip3.ToString("G");
                                break;
                            case (int)Common.Enums.PrintQSubitemType.Tiff:
                                type = Common.Enums.PrintQSubitemType.Tiff.ToString("G");
                                break;
                            case (int)Common.Enums.PrintQSubitemType.Blueprint:
                                type = Common.Enums.PrintQSubitemType.Blueprint.ToString("G");
                                break;
                            case (int)Common.Enums.PrintQSubitemType.Plate:
                                type = Common.Enums.PrintQSubitemType.Plate.ToString("G");
                                break;
                        }
                        item.SubItems.Add(type);
                        #endregion

                        #region 拆色資料
                        String color = String.Empty;

                        //String vpsFileName = (cycle.PrintQueue_VPS == null) ? "" : cycle.PrintQueue_VPS.VpsFileName;
                        // 改為:
                        var pqVps = new EF6.PrintQueue_VPS();
                        if (cycle.PrintQueueVpsId != null)
                            pqVps = ApiHelper.GetPrintQueueVps(cycle.PrintQueueVpsId.Value);
                        String vpsFileName = (pqVps.VpsFileName == null) ? "" : pqVps.VpsFileName;

                        color = (vpsFileName != "") ? vpsFileName.Substring(vpsFileName.IndexOf('(') + 1, vpsFileName.IndexOf(')') - vpsFileName.IndexOf('(') - 1) : "";
                        item.SubItems.Add(color);
                        #endregion
                    }
                }
            }
        }

        private async void ShowPreview(SourceType type, String filename)
        {
            /** 取消，改用 xFilm5.Api
            var uri = new Uri(Properties.Settings.Default.BitmapUri);
            String imgFile = String.Empty;

            #region imgFile 分單色同彩色，單色用 bmp，彩色用 tif
            switch (type)
            {
                case SourceType.Blueprint:
                    #region tiff = 202020.A1245-745x605-TEST.p1(CMYK).tif
                    imgFile = Path.Combine(uri.LocalPath, filename.Substring(0, filename.IndexOf('(')) + "(CMYK).tif");
                    #endregion
                    break;
                case SourceType.Plate:
                    #region bitmap = 202020.A1245-745x605-TEST.p1(C).bmp
                    imgFile = Path.Combine(uri.LocalPath, filename + ".bmp");
                    #endregion
                    break;
            }
            #endregion

            // 用 NetworkConnection.cs 做 impersonation，shared folder 係 synologies
            System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(
                Properties.Settings.Default.BitmapUri_UserName, 
                Properties.Settings.Default.BitmapUri_UserPassword);

            using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
            {
                // 順利攞到 access permission
                if (File.Exists(imgFile))
                {
                    try
                    {
                        picPreview.SizeMode = PictureBoxSizeMode.Zoom;
                        picPreview.Image = Image.FromFile(imgFile);
                    }
                    catch
                    {

                    }
                }
            }
            */

            var stream = await ApiHelper.GetProductPicture((int)type, filename);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.Image = Image.FromStream(stream);

        }

        private bool LogLifeCycle(SourceType type, int pQueueId, int pQueueVpsId)
        {
            bool result = false;

            /**
            String sql = String.Format("PrintQueueId = {0} AND PrintQueueVpsId = {1} AND PrintQSubitemType = {2}", pQueueId.ToString(), pQueueVpsId.ToString(), type.ToString("D"));
            DAL4Win.PrintQueue_LifeCycle cycle = DAL4Win.PrintQueue_LifeCycle.LoadWhere(sql);
            if (cycle == null)
            {
                cycle = new DAL4Win.PrintQueue_LifeCycle();
                cycle.PrintQueueId = pQueueId;
                cycle.PrintQueueVpsId = pQueueVpsId;
                cycle.PrintQSubitemType = (int)type;
                cycle.Status = (int)DAL4Win.Common.Enums.Status.Active;
                cycle.CreatedOn = DateTime.Now;
                cycle.CreatedBy = 0;
                cycle.Save();

                result = true;
            }
            */

            using (var ctx = new EF6.xFilmEntities())
            {
                var cycle = ctx.PrintQueue_LifeCycle.Where(x => x.PrintQueueId == pQueueId && x.PrintQueueVpsId == pQueueVpsId && x.PrintQSubitemType == (int)type).SingleOrDefault();
                if (cycle == null)
                {
                    try
                    {
                        cycle = new EF6.PrintQueue_LifeCycle();
                        cycle.PrintQueueId = pQueueId;
                        cycle.PrintQueueVpsId = pQueueVpsId;
                        cycle.PrintQSubitemType = (int)type;
                        cycle.Status = (int)Common.Enums.Status.Active;
                        cycle.CreatedOn = DateTime.Now;
                        cycle.CreatedBy = 0;

                        ctx.PrintQueue_LifeCycle.Add(cycle);
                        ctx.SaveChanges();

                        result = true;
                    }
                    catch { }
                }
            }

            return result;
        }

        private void SendWhatsAppMsg()
        {
            String from = "85260620034", pwd = "AgoOF4qWeQ4pkdH7okR6grxJ0mE=", name = "Support@NuStar";

            WhatsApp wa = new WhatsApp(from, pwd, name, true, false);
            wa.OnConnectSuccess += () =>
            {
                Console.WriteLine("Connected...");
                wa.OnLoginSuccess += (phno, data) =>
                {
                    Console.WriteLine("\r\nConnection success!");
                    wa.SendMessage("85298384761", "from c#");
                    Console.WriteLine("\r\nMessage sent!");
                };

                wa.OnLoginFailed += (data) =>
                {
                    Console.WriteLine(String.Format("\r\nLogin failed {0}", data));
                };
                wa.Login();
            };
            wa.OnConnectFailed += (ex) =>
            {
                Console.WriteLine(String.Format("\r\nConnection failed {0}"), ex.StackTrace);
            };
            wa.Connect();
        }

        #region RestSharp samples
        private void Basic_xFilm5Api()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("http://twitter.com");
            client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest();
            request.Resource = "statuses/friends_timeline.xml";

            IRestResponse response = client.Execute(request);
        }

        private void Get_xFilm5Api()
        {
            var client = new RestClient(Properties.Settings.Default.ApiUri);
            var request = new RestRequest("api/xFilm5/", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            Console.WriteLine(queryResult.Content);
            //var queryResult = client.Execute<List<Items>>(request).Data;
        }

        private void Post_xFilm5Api()
        {
            var client = new RestClient("http://192.168.0.1");
            var request = new RestRequest("api/item/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            //request.AddBody(new Item
            //{
            //    ItemName = someName,
            //    Price = 19.99
            //});
            client.Execute(request);
        }

        private void Delete_xFIlm5Api()
        {
            var client = new RestClient("http://192.168.0.1");
            var request = new RestRequest("api/item/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            //request.AddBody(new Item
            //{
            //    ItemName = someName,
            //    Price = 19.99
            //});
            client.Execute(request);
        }
        #endregion
    }
}
