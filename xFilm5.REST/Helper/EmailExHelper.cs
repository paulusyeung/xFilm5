using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
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
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(CommonHelper.Config.CurrentWordDict, CommonHelper.Config.CurrentLanguageId);

            using (var ctx = new xFilmEntities())
            {

                #region 準備啲有用嘅 records: receiptHeader, clientUser
                var receiptHeader = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == ReceiptHeaderId).SingleOrDefault();
                Client client = null;
                Client_AddressBook clientAddr = null;
                Client_User clientUser = null;
                //DAL.ReceiptHeader receiptHeader = DAL.ReceiptHeader.Load(ReceiptHeaderId);
                //DAL.Client client = null;
                //DAL.Client_AddressBook clientAddr = null;
                //DAL.Client_User clientUser = null;
                if (receiptHeader != null)
                {
                    client = ctx.Client.Where(x => x.ID == receiptHeader.ClientId).SingleOrDefault();
                    clientAddr = ctx.Client_AddressBook.Where(x => x.ClientID == receiptHeader.ClientId && x.PrimaryAddr == true).SingleOrDefault();

                    if (receiptHeader.ClientUserId != 0)
                    {
                        clientUser = ctx.Client_User.Where(x => x.ClientID == receiptHeader.ClientUserId).SingleOrDefault();
                    }
                    else
                    {
                        sql = String.Format("ClientID = {0} AND PrimaryUser = 1", receiptHeader.ClientId.ToString());
                        clientUser = ctx.Client_User.Where(x => x.ClientID == receiptHeader.ClientId && x.PrimaryUser == true).SingleOrDefault();
                    }
                }
                String company_addr = CommonHelper.Owner.GetWorkshopAddress(client.Branch.Value).Replace(@"\n", Environment.NewLine);
                #endregion

                var transmission = new SparkPost.Transmission();
                transmission.Content.TemplateId = "delivery-note-en";
                //transmission.Content.From.Email = "support@directoutput.com.hk";
                //transmission.Content.ReplyTo = "no-reply<support@directoutput.com.hk>";

                #region Substitute header data
                transmission.SubstitutionData["subject"] = String.Format(oDict.GetWordWithColon("delivery_note") + " {0}", receiptHeader.ReceiptNumber);
                transmission.SubstitutionData["company_address"] = company_addr;
                transmission.SubstitutionData["dn_number"] = receiptHeader.ReceiptNumber;
                transmission.SubstitutionData["dn_date"] = receiptHeader.ReceiptDate.Value.ToString("yyyy-MM-dd");

                transmission.SubstitutionData["client_address"] = clientAddr != null ? clientAddr.Address : "";
                transmission.SubstitutionData["client_name"] = client != null ? client.Name : "";
                transmission.SubstitutionData["client_user_name"] = clientUser != null ? clientUser.FullName : "";
                transmission.SubstitutionData["client_email"] = clientUser != null ? clientUser.Email : "";

                transmission.SubstitutionData["total_amount"] = receiptHeader.ReceiptAmount.Value.ToString("$#,###.00");
                #endregion

                #region Subsitute items data
                var items = new List<DNItem>();
                //sql = String.Format("ReceiptHeaderId = {0}", ReceiptHeaderId.ToString());
                //String[] orderby = { "Description" };
                //DAL.ReceiptDetailCollection receiptDetails = DAL.ReceiptDetail.LoadCollection(sql, orderby, true);
                var receiptDetails = ctx.ReceiptDetail.Where(x => x.ReceiptHeaderId == ReceiptHeaderId).OrderBy(x => x.Description).ToList();
                if (receiptDetails.Count > 0)
                {
                    for (int i = 0; i < receiptDetails.Count; i++)
                    {
                        items.Add(new DNItem { item_name = receiptDetails[i].BillingCode + " " + receiptDetails[i].Description, item_price = receiptDetails[i].Amount.Value.ToString("$#,###.00") });
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

                var spclient = new SparkPost.Client(CommonHelper.Config.SparkPost_ApiKey);
                var response = spclient.Transmissions.Send(transmission).Result;
                // or client.Transmissions.Send(transmission).Wait();

                result = (response.StatusCode == System.Net.HttpStatusCode.OK) ? true : false;
            }
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
