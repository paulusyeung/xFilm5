﻿using RestSharp;
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
        }

        private void Desktop_Load(object sender, EventArgs e)
        {
            //this.Text += " " + Properties.Settings.Default.BlueprintMachines;
            //Get_xFilm5Api();
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

                    this.Text += "%" + txtQrCodeData.Text;

                    String[] qrCodeData = txtQrCodeData.Text.Trim().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    // 2016.11.15 paulus: QR Code 有 3 行 text,
                    //   line1 = Machine Type (藍紙 or 鋅板);
                    //   line2 = 時間;
                    //   line3 = Customer Number . Cups Job Id - File Name . p#(CMYK)
                    if (qrCodeData.Length == 3)
                    {
                        #region QR Code 有料，做嘢
                        SourceType sourceType = IdentifySourceType(qrCodeData[0]);

                        String[] line3 = qrCodeData[2].Split('.');
                        int clientId = Convert.ToInt32(line3[0]);
                        String cupsJobId = line3[1].Split('-')[0];
                        String sql = String.Format("ClientID = {0} AND CupsJobID = N'{1}'", clientId.ToString(), cupsJobId);
                        DAL4Win.PrintQueue pq = DAL4Win.PrintQueue.LoadWhere(sql);

                        ShowClientInfo(clientId);                       // 目前淨係顯示 Client 名，日後可以顯示其他資料
                        if (pq != null)                                 // 如果已經有 PrintQueue
                        {
                            LogLifeCycle(sourceType, pq.ID);            //   update Log File
                            ShowPrintQLifeCycle(pq.ID);                 //   顯示同一隻 PrintQueue 嘅 log file
                        }
                        ShowPreview(sourceType, qrCodeData[2]);         // 顯示 thumbnail

                        #endregion
                    }
                }
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

        private void ShowPrintQLifeCycle(int printQueueId)
        {
            String sql = String.Format("PrintQueueId = {0}", printQueueId.ToString());
            String[] orderBy = { "CreatedOn" };
            DAL4Win.PrintQueue_LifeCycleCollection allCycle = DAL4Win.PrintQueue_LifeCycle.LoadCollection(sql, orderBy, true);
            if (allCycle.Count > 0)
            {
                lvwLifeCycle.Items.Clear();
                int i = 0;
                foreach (DAL4Win.PrintQueue_LifeCycle cycle in allCycle)
                {
                    ListViewItem item = lvwLifeCycle.Items.Add(allCycle[i].LifeCycleId.ToString());

                    item.SubItems.Add((i + 1).ToString());
                    item.SubItems.Add(cycle.CreatedOn.ToString("yyyy-MM-dd hh:mm:ss"));

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
                    imgFile = Path.Combine(uri.LocalPath, filename.Substring(0, filename.IndexOf('(') - 1) + "(CMYK).tif");
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
                    picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                    picPreview.Image = Image.FromFile(imgFile);
                }
                else
                {
                    picPreview.ImageLocation = "";
                }
            }
        }

        private bool LogLifeCycle(SourceType type, int pQueueId)
        {
            bool result = false;

            String sql = String.Format("PrintQueueId = {0} AND PrintQSubitemType = {1}", pQueueId.ToString(), type.ToString());
            DAL4Win.PrintQueue_LifeCycle cycle = DAL4Win.PrintQueue_LifeCycle.LoadWhere(sql);
            if (cycle == null)
            {
                cycle = new DAL4Win.PrintQueue_LifeCycle();
                cycle.PrintQueueId = pQueueId;
                cycle.PrintQSubitemType = (int)type;
                cycle.Status = (int)DAL4Win.Common.Enums.Status.Active;
                cycle.CreatedOn = DateTime.Now;
                cycle.CreatedBy = 0;
                cycle.Save();

                result = true;
            }

            return result;
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
