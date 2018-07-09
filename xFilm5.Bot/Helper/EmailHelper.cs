﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.Bot.Helper
{
    public static class EmailHelper
    {
        /// <summary>
        /// 可以用 [,] / [ ] / [;] 嚟分隔 email 收件人
        /// </summary>
        /// <param name="ReceiptHeaderId"></param>
        /// <param name="Recipient"></param>
        /// <returns></returns>
        public static bool EmailReceipt(int ReceiptHeaderId, String Recipient)
        {
            bool result = false;

            var emails = Recipient.Split(new Char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            result = EmailReceipt(ReceiptHeaderId, emails);

            return result;
        }

        public static bool EmailReceipt(int ReceiptHeaderId, String[] Recipients)
        {
            bool result = false;

            //if (Config.CurrentWordDict == null) Config.LoadCurrentWordDict();
            //nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Config.CurrentWordDict, Config.CurrentLanguageId);

            using (var ctx = new xFilmEntities())
            {
                #region 準備啲有用嘅 records: receiptHeader, clientUser

                var receiptHeader = ctx.ReceiptHeader.Where(x => x.ReceiptHeaderId == ReceiptHeaderId).SingleOrDefault();
                EF6.Client client = null;
                EF6.Client_AddressBook clientAddr = null;
                EF6.Client_User clientUser = null;
                if (receiptHeader != null)
                {
                    client = ctx.Client.Where(x => x.ID == receiptHeader.ClientId).SingleOrDefault();
                    clientAddr = ctx.Client_AddressBook.Where(x => x.ClientID == receiptHeader.ClientId && x.PrimaryAddr == true).SingleOrDefault();

                    if (receiptHeader.ClientUserId != 0)
                    {
                        clientUser = ctx.Client_User.Where(x => x.ID == receiptHeader.ClientUserId).SingleOrDefault();
                    }
                    else
                    {
                        clientUser = ctx.Client_User.Where(x => x.ClientID == receiptHeader.ClientId && x.PrimaryUser == true).SingleOrDefault();
                    }
                }
                String company_addr = Utility.Owner.GetWorkshopAddress(client.Branch.Value).Replace(@"\n", Environment.NewLine);
                #endregion

                var transmission = new SparkPost.Transmission();
                transmission.Content.TemplateId = "delivery-note-en";
                //transmission.Content.From.Email = "support@directoutput.com.hk";
                //transmission.Content.ReplyTo = "no-reply<support@directoutput.com.hk>";

                #region Substitute header data
                var subject = Config.CurrentLanguageId == 3 ? "收貨單 {0}" : Config.CurrentLanguageId == 2 ? "收货单 {0}" : "Delivery Note {0}";
                transmission.SubstitutionData["subject"] = String.Format(subject, receiptHeader.ReceiptNumber);
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

                var spclient = new SparkPost.Client(Config.SparkPost_ApiKey);

                spclient.CustomSettings.SendingMode = SparkPost.SendingModes.Sync;

                var response = spclient.Transmissions.Send(transmission);

                result = (response.Result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false;

                //result = true;
            }
            return result;
        }

        /// <summary>
        /// Sending email via SparkPost
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool EmailMessage(String email, String subject, String message)
        {
            bool result = false;

            try
            {
                var transmission = new SparkPost.Transmission();

                transmission.Content.From.Email = "support@directoutput.com.hk";
                transmission.Content.ReplyTo = "no-reply<support@directoutput.com.hk>";
                transmission.Content.Subject = subject;
                transmission.Content.Text = message;

                var recipient = new SparkPost.Recipient
                {
                    Address = new SparkPost.Address { Email = email }
                };
                transmission.Recipients.Add(recipient);

                var spclient = new SparkPost.Client(Config.SparkPost_ApiKey);

                spclient.CustomSettings.SendingMode = SparkPost.SendingModes.Sync;

                var response = spclient.Transmissions.Send(transmission);

                result = (response.Result.StatusCode == System.Net.HttpStatusCode.OK) ? true : false;
            }
            catch (Exception ex)
            {
                //
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
