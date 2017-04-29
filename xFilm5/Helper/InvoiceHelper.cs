using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class InvoiceHelper
    {
        public static bool IsBilling5(int invoiceId)
        {
            bool result = false;

            DAL.Acct_INMaster item = DAL.Acct_INMaster.Load(invoiceId);
            if (item != null)
            {
                if (item.OrderID == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool IsCash(int invoiceId)
        {
            bool result = false;

            DAL.Acct_INMaster item = DAL.Acct_INMaster.Load(invoiceId);
            if (item != null)
            {
                if (item.PaymentType != 0)  // 0 = 月結
                {
                    result = true;
                }
            }

            return result;
        }

        public static void PrintToXprinter(int invoiceId)
        {
            String sql = String.Format("INMasterId = {0}", invoiceId.ToString());
            DAL.ReceiptHeader receipt = DAL.ReceiptHeader.LoadWhere(sql);
            if (receipt != null)
                Helper.BotHelper.PostXprinter(receipt.ReceiptHeaderId);
        }
    }
}
