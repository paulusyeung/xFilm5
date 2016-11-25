using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;

namespace xFilm5.QRStation
{
    public partial class Desktop : Form
    {
        #region private properties
        private enum SourceType
        {
            None,
            Blueprint = (int)DAL4Win.Common.Enums.PrintQSubitemType.Blueprint,
            Plate = (int)DAL4Win.Common.Enums.PrintQSubitemType.Plate
        }
        #endregion

        public Desktop()
        {
            InitializeComponent();

            txtQrCodeData.KeyUp += TxtQrCodeData_KeyUp;
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
        }

        private void InitializeCounter()
        {
            String sql = "";

            #region Good Counts
            sql = String.Format("CONVERT(varchar(10), CreatedOn, 20) = '{0}' AND (PrintQSubitemType = {1})", 
                DateTime.Now.ToString("yyyy-MM-dd"),
                DAL4Win.Common.Enums.PrintQSubitemType.Plate.ToString("D"));
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
        }

        private void TxtQrCodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                // QR Code data 有三行 text，即係入面有 CR/LF，唯有認住最後一隻 character ) 先做嘢
                if (txtQrCodeData.Text.IndexOf(')') >= 0 )
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
                        #region QR Code 有料，做嘢
                        SourceType sourceType = IdentifySourceType(qrCodeData[0]);

                        String line3 = qrCodeData[2];
                        String[] line3parts = line3.Split('.');

                        int clientId = Convert.ToInt32(line3parts[0]);
                        String cupsJobId = line3parts[1].Split('-')[0];
                        String filename = line3.Replace(clientId.ToString() + ".", "");

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
                        #endregion
                    }
                }
            }
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
            DAL4Win.Client client = DAL4Win.Client.Load(clientId);
            if (client != null)
            {
                txtClientInfo.Text = client.Name;
            }
        }

        private void ShowPrintQLifeCycle(int printQueueId, String pageName)
        {
            String sql = String.Format("PrintQueueId = {0}", printQueueId.ToString());
            String[] orderBy = { "CreatedOn" };
            DAL4Win.PrintQueue_LifeCycleCollection allCycle = DAL4Win.PrintQueue_LifeCycle.LoadCollection(sql, orderBy, true);
            if (allCycle.Count > 0)
            {
                int i = 0;
                foreach (DAL4Win.PrintQueue_LifeCycle cycle in allCycle)
                {
                    bool samepage = true;
                    if (cycle.PrintQueueVpsId != 0)
                    {
                        DAL4Win.PrintQueue_VPS vps = DAL4Win.PrintQueue_VPS.Load(cycle.PrintQueueVpsId);
                        if (vps.VpsFileName.IndexOf(pageName) >= 0)
                            samepage = true;
                        else
                            samepage = false;
                    }

                    if (samepage)
                    {
                        ListViewItem item = lvwLifeCycle.Items.Add(allCycle[i].LifeCycleId.ToString());

                        item.SubItems.Add((i + 1).ToString());
                        item.SubItems.Add(cycle.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"));

                        #region Subitem.Add(SubitemType
                        String type = String.Empty;
                        switch (cycle.PrintQSubitemType)
                        {
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Ps:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Ps.ToString("G");
                                break;
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Vps:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Vps.ToString("G");
                                break;
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Cip3:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Cip3.ToString("G");
                                break;
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Tiff:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Tiff.ToString("G");
                                break;
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Blueprint:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Blueprint.ToString("G");
                                break;
                            case (int)DAL4Win.Common.Enums.PrintQSubitemType.Plate:
                                type = DAL4Win.Common.Enums.PrintQSubitemType.Plate.ToString("G");
                                break;
                        }
                        item.SubItems.Add(type);
                        #endregion

                        #region 拆色資料
                        String color = String.Empty;
                        DAL4Win.PrintQueue_VPS pqVps = DAL4Win.PrintQueue_VPS.Load(cycle.PrintQueueVpsId);
                        color = (pqVps != null) ? pqVps.VpsFileName.Substring(pqVps.VpsFileName.IndexOf('(') + 1, pqVps.VpsFileName.IndexOf(')') - pqVps.VpsFileName.IndexOf('(') - 1) : "";
                        item.SubItems.Add(color);
                        #endregion
                    }

                    i++;
                }
            }
        }

        private void ShowPreview(SourceType type, String filename)
        {
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
                        picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                        picPreview.Image = Image.FromFile(imgFile);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private bool LogLifeCycle(SourceType type, int pQueueId, int pQueueVpsId)
        {
            bool result = false;

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
