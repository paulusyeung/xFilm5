using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace xFilm5.Controls.Email
{
    public class EmailEx
    {
        /// <summary>
        /// 可以用 [,] / [ ] / [;] 嚟分隔 email 收件人
        /// </summary>
        /// <param name="ReceiptHeaderId"></param>
        /// <param name="Recipient"></param>
        /// <returns></returns>
        public static bool SendDNEmail(int ReceiptHeaderId, String Recipient)
        {
            bool result = false;

            var emails = Recipient.Split(new Char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            result = SendDNEmail(ReceiptHeaderId, emails);

            return result;
        }

        public static bool SendDNEmail(int ReceiptHeaderId, String[] Recipients)
        {
            bool result = false;
            String sql = "";
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(DAL.Common.Config.CurrentWordDict, DAL.Common.Config.CurrentLanguageId);

            #region 準備啲有用嘅 records: receiptHeader, clientUser
            DAL.ReceiptHeader receiptHeader = DAL.ReceiptHeader.Load(ReceiptHeaderId);
            DAL.Client client = null;
            DAL.Client_AddressBook clientAddr = null;
            DAL.Client_User clientUser = null;
            if (receiptHeader != null)
            {
                client = DAL.Client.Load(receiptHeader.ClientId);
                sql = String.Format("ClientID = {0} AND PrimaryAddr = 1", receiptHeader.ClientId.ToString());
                clientAddr = DAL.Client_AddressBook.LoadWhere(sql);

                if (receiptHeader.ClientUserId != 0)
                {
                    clientUser = DAL.Client_User.Load(receiptHeader.ClientUserId);
                }
                else
                {
                    sql = String.Format("ClientID = {0} AND PrimaryUser = 1", receiptHeader.ClientId.ToString());
                    clientUser = DAL.Client_User.LoadWhere(sql);
                }
            }
            String company_addr = xFilm5.Controls.Utility.Owner.GetWorkshopAddress(client.Branch).Replace(@"\n", Environment.NewLine);
            #endregion

            var transmission = new SparkPost.Transmission();
            transmission.Content.TemplateId = "delivery-note-en";
            //transmission.Content.From.Email = "support@directoutput.com.hk";
            //transmission.Content.ReplyTo = "no-reply<support@directoutput.com.hk>";

            #region Substitute header data
            transmission.SubstitutionData["subject"] = String.Format(oDict.GetWordWithColon("delivery_note") + " {0}", receiptHeader.ReceiptNumber);
            transmission.SubstitutionData["company_address"] = company_addr;
            transmission.SubstitutionData["dn_number"] = receiptHeader.ReceiptNumber;
            transmission.SubstitutionData["dn_date"] = receiptHeader.ReceiptDate.ToString("yyyy-MM-dd");

            transmission.SubstitutionData["client_address"] = clientAddr != null ? clientAddr.Address : "";
            transmission.SubstitutionData["client_name"] = client != null ? client.Name : "";
            transmission.SubstitutionData["client_user_name"] = clientUser != null ? clientUser.FullName : "";
            transmission.SubstitutionData["client_email"] = clientUser != null ? clientUser.Email : "";

            transmission.SubstitutionData["total_amount"] = receiptHeader.ReceiptAmount.ToString("$#,###.00");
            #endregion

            #region Subsitute items data
            var items = new List<DNItem>();
            sql = String.Format("ReceiptHeaderId = {0}", ReceiptHeaderId.ToString());
            String[] orderby = { "Description" };
            DAL.ReceiptDetailCollection receiptDetails = DAL.ReceiptDetail.LoadCollection(sql, orderby, true);
            if (receiptDetails.Count > 0)
            {
                for (int i = 0; i < receiptDetails.Count; i++)
                {
                    items.Add(new DNItem { item_name = receiptDetails[i].BillingCode + " " + receiptDetails[i].Description, item_price = receiptDetails[i].Amount.ToString("$#,###.00") });
                }
            }

            // you can pass more complicated data, so long as it
            // can be parsed easily to JSON
            transmission.SubstitutionData["items"] = items;
            #endregion


            #region set recipients data
            foreach (String r in Recipients)
            {
                if (IsValidEmail(r.Trim()))
                {
                    var recipient = new SparkPost.Recipient
                    {
                        Address = new SparkPost.Address { Email = r.Trim() }
                    };
                    transmission.Recipients.Add(recipient);
                }
            }
            #endregion

            var spclient = new SparkPost.Client(xFilm5.Controls.Utility.Config.SparkPost_ApiKey);

            //2018.08.03 paulus: send 唔倒，除非加埋 TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            var response = spclient.Transmissions.Send(transmission).Result;
            // or client.Transmissions.Send(transmission).Wait();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = true;
            else
                result = false;

            return result;
        }

        public static bool IsValidEmail(String email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
    public class DNItem
    {
        public string item_name { get; set; }
        public string item_price { get; set; }
    }
}
