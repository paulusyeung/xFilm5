using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.Controls.Email;

namespace xFilm5.Helper
{
    public static class EmailHelper
    {
        /// <summary>
        /// 2017.05.14 paulus: 新增功能，可以 email 張單
        /// </summary>
        public static void EmailReceipt(int receiptId, int clientId)
        {
            if (Helper.ClientHelper.IsReceiptEmail(clientId))
            {
                var p = Helper.ClientHelper.GetEmailRecipient(clientId);
                if (p != "")
                {
                    EmailEx.SendDNEmail(receiptId, p);
                }
            }
        }
    }
}
