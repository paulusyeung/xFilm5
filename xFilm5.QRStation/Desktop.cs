using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            Blueprint,
            Plate
        }
        #endregion

        public Desktop()
        {
            InitializeComponent();

            txtQrCodeData.KeyUp += TxtQrCodeData_KeyUp;
        }

        private void Desktop_Load(object sender, EventArgs e)
        {
            this.Text += " " + Properties.Settings.Default.BlueprintMachines;
        }

        private void TxtQrCodeData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                e.Handled = true;

                int barcodeLength = txtQrCodeData.TextLength;

                txtQrCodeData.Select(0, barcodeLength);

                String[] qrCodeData = txtQrCodeData.Text.Trim().Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                // 2016.11.15 paulus: QR Code 有 3 lines,
                //   line1 = Machine Type (藍紙 or 鋅板);
                //   line2 = 時間;
                //   line3 = Customer Number . Cups Job Id - File Name . p#(CMYK)
                if (qrCodeData.Length == 3)
                {
                    SourceType sourceType = IdentifySourceType(qrCodeData[0]);

                    String[] line3 = qrCodeData[2].Split('.');
                    int clientId = Convert.ToInt32(line3[0]);
                    String cupsJobId = line3[1].Split('-')[0];
                    String sql = String.Format("ClientID = {0} AND CupsJobID = N'{1}'", clientId.ToString(), cupsJobId);
                    //DAL.PrintQueue pq = DAL.PrintQueue.LoadWhere(sql);

                    //GetClientInfo(clientId);
                    //if (pq != null)
                    //    GetPrintQLifeCycle(pq.ID);
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

        private void GetClientInfo(int clientId)
        {
            //DAL.Client client = DAL.Client.Load(clientId);
            //if (client != null)
            //{

            //}
        }

        private void GetPrintQLifeCycle(int printQueueId)
        {
            String sql = String.Format("PrintQueueId = {0}", printQueueId.ToString());
            String[] orderBy = { "CreatedOn" };
            //DAL.PrintQueue_LifeCycleCollection allCycle = DAL.PrintQueue_LifeCycle.LoadCollection(sql, orderBy, true);
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
            var client = new RestClient("192.168.0.1");
            var request = new RestRequest("api/item/", Method.GET);
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
