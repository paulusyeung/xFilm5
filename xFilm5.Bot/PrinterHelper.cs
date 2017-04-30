using Gizmox.WebGUI.Common.Resources;
using PrinterUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using xFilm5.Bot.Models;

namespace xFilm5.Bot
{
    public class PrinterHelper
    {
        // 2017.04.26 paulus: 如果 local config.web 有 Xprinter 名，就用 local，否則就用 caller 嘅 printerName
        String _Xprinter = "";

        public void Print(int receiptId, int languageId, string printerName)
        {
            using (var ctx = new xFilm5Entities())
            {
                var hasRows = ctx.vwReceiptDetailsList.Where(x => x.ReceiptHeaderId == receiptId).Any();
                if (hasRows)
                {
                    var bytesValue = GetPrintContent(receiptId, languageId);

                    #region

                    #region 先用 local add 咗隻 cups printer，用以下嘅 loop 嚟搵出佢個 printer name
                    //LocalPrintServer server = new LocalPrintServer();
                    //var pq = server.DefaultPrintQueue;
                    //var pqs = server.GetPrintQueues();
                    //foreach (var p in pqs)
                    //{
                    //    String pname = p.Name;
                    //}
                    #endregion

                    //string printerName = @"\\http://192.168.2.92:631\KT-XP80C";     // 我懷疑哩個 syntax 係因為安裝咗 samba

                    #region HACK: 通常喺哩度就可以 send 去俾隻 printer，不過曾經試過要 login (RaspberryPi)，估計係同隻 CUPS 嘅設定有關
                    var t = Encoding.Default.GetString(bytesValue);
                    //t += Encoding.Default.GetString(bytesValue);      // 印兩張相同嘅單

                    RawPrinterHelper.SendStringToPrinter(_Xprinter == "" ? printerName : _Xprinter, t);
                    #endregion

                    #region HACK: 可以睇見所有 CUPS2 嘅 print queues，不過就唔一定睇到隻 OrangePi/ RaspberryPi
                    //try
                    //{
                    //    PrintServer pServer = new PrintServer(@"\\CUPS2", PrintSystemDesiredAccess.EnumerateServer);
                    //    var pQueues = pServer.GetPrintQueues();
                    //    PrintServer ppServer = new PrintServer(@"\\kt-printserver\", PrintSystemDesiredAccess.EnumerateServer);
                    //    var ppQueues = ppServer.GetPrintQueues();
                    //}
                    //catch { }
                    #endregion

                    #region HACK: 如果要 login credential 先可以打印
                    /*
                    string hostname = "kt.pserver.xfilm5";   // 修改 C:\Windows\System32\drivers\etc\hosts

                    var uri = new Uri(@"\\192.168.2.92");
                    string userName = "root";
                    string userPassword = "nx-9602";
                    System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);

                    using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                    {
                        try
                        {
                            #region 有網友教用 net command 去 login 先，不過我試咗，Win32 Errors 係冇效嘅
                            //var p = new Process();
                            //p.StartInfo.FileName = "net";
                            //p.StartInfo.Arguments = "use \\\\kt-printserver nx-9602 /user:domain\\root";
                            //p.StartInfo.CreateNoWindow = true;
                            //p.Start();
                            //p.WaitForExit();
                            #endregion

                            //xFilm5.Controls.RawPrinterHelper.SendStringToPrinter(@"\\192.168.2.92\kt-xp80c", Encoding.Default.GetString(bytesValue));                     // 隻 RaspberryPi 用哩個 syntax
                            //PrinterUtility.PrintExtensions.Print(bytesValue, "\\\\192.168.2.222\\tx2"); // PrintUtilityTest.Properties.Settings.Default.PrinterPath);
                        }
                        catch { }
                    }
                    */
                    #endregion

                    #region 用 LinQ 搵 printer name
                    //var printers = new System.Printing.PrintServer(@"\\WIN-10pv").GetPrintQueues()
                    //    .Where(t =>
                    //    {
                    //        try { return t.IsShared && !t.IsNotAvailable; }
                    //        catch { return false; }
                    //    })
                    //    .Select(t => t.FullName)
                    //    .ToArray();
                    #endregion

                    #endregion

                    #region HACK: 取消（暫時得哩個辦法 ok，use RawPrinterHelper.cs 經 local printer send 去隻 CUPS xPrinter，有反應！）
                    //IntPtr pUnmanagedBytes = new IntPtr(0);
                    //int nLength = bytesValue.Length;
                    //pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                    //xFilm5.Controls.RawPrinterHelper.SendBytesToPrinter(@"\\http://192.168.2.222:631\tx2", pUnmanagedBytes, nLength);
                    #endregion

                    // HACK: 用 TcpClient 當係 ip printer，唔得，可能要買 networ xprinter
                    //SendToTcpPrinter(BytesValue);

                    #region 保留做參考
                    /*

                    var uri = new Uri(@"\\192.168.2.222");
                    string userName = "dell";
                    string userPassword = "1234";
                    System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(userName, userPassword);
                    using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                    {
                        try
                        {
                            PrintServer ppServer = new PrintServer(@"\\192.168.2.222");
                            var ppQueues = ppServer.GetPrintQueues();

                            xFilm5.Controls.RawPrinterHelper.SendFileToPrinter(@"\\http://192.168.2.222:631\tx2", @"C:\Shared\ThermalPrinter-0406-120118.txt");
                            PrinterUtility.PrintExtensions.Print(bytesValue, "\\\\192.168.2.222\\tx2"); // PrintUtilityTest.Properties.Settings.Default.PrinterPath);
                        }
                        catch { }
                    }

                    // HACK: 個 printer name 點都唔得，試過：tx2, \\localhost\tx2, \\WIN-10pv\tx2, \\http://192.168.2.188:631\tx2
                    //try
                    //    {
                    //        PrinterUtility.PrintExtensions.Print(bytesValue, "\\\\192.168.2.222\\tx2"); // PrintUtilityTest.Properties.Settings.Default.PrinterPath);
                    //    }
                    //    catch { }
                    // HACK: write to a local file for debugging
                    String finalResult = Encoding.Unicode.GetString(bytesValue);
                    File.WriteAllBytes(String.Format(@"C:\Shared\ThermalPrinter-{0}.txt", DateTime.Now.ToString("MMdd-HHmmss")), bytesValue);
                    xFilm5.Controls.RawPrinterHelper.SendStringToPrinter("printer name", Encoding.Default.GetString(bytesValue));

                    */
                    #endregion

                    // backup as file (for debugging)
                    //File.WriteAllBytes(String.Format(@"C:\Shared\ThermalPrinter-{0}.txt", DateTime.Now.ToString("MMdd-HHmmss")), bytesValue);
                }
            }
        }

        // PrinterUtility video: https://www.youtube.com/watch?v=0mC_yvT3abw
        // paper:  576 dots/line
        // font A: 12x24 48 characters/line
        // font B:  9x17 64 characters/line
        // 中文字: 24x24 24 characters/line
        // CodePage: https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx
        // GB18030 = Codepage 54936, GB2312 = 936, Big5 = 950
        private Byte[] GetPrintContent(int receiptId, int languageId = 1)
        {
            String dictFile = Path.Combine(HttpContext.Current.Server.MapPath("~"), "WordDict.xml");
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(dictFile, languageId);

            using (var ctx = new xFilm5Entities())
            {
                var items = ctx.vwReceiptDetailsList.Where(x => x.ReceiptHeaderId == receiptId).OrderBy(x => x.OrderPkPrintQueueVpsId).ToList<vwReceiptDetailsList>();

                var header = items[0];
                var orderHdr = ctx.OrderHeader.Where(x => x.ID == header.OrderHeaderId).SingleOrDefault();
                var orderDtls = ctx.Order_Details.Where(x => x.OrderID == header.OrderHeaderId).FirstOrDefault();
                var clientAddr = ctx.Client_AddressBook.Where(x => x.ClientID == orderHdr.ClientID && x.PrimaryAddr == true).SingleOrDefault();
                var deliverTo = ctx.Client_AddressBook.Where(x => x.ClientID == orderDtls.DeliveryAddr).SingleOrDefault();

                _Xprinter = Utility.Workshop.GetXprinter(orderHdr.ProofingOp.Value);

                //}

                // DataTable dt = DataSource.DN(receiptId);
                //DataRow hdr = dt.Rows[0];
                String line = String.Empty;
                const int codePage = 54936;

                PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();

                #region Company Logo image
                //var BytesValue = GetLogo(@"C:\Shared\CompanyLogo.png");
                //var BytesValue = GetLogo("CompanyLogoBW.jpg");
                var BytesValue = new byte[0];   // Encoding.Unicode.GetBytes(string.Empty);
                #endregion

                #region Company Address
                ///DAL.OrderHeader order = DAL.OrderHeader.Load(hdr.Field<int>("OrderHeaderId"));
                String[] address = Utility.Workshop.GetAddress(orderHdr.ProofingOp.Value).Split(new String[] { @"\n" }, StringSplitOptions.None);

                //BytesValue = PrintExtensions.AddBytes(BytesValue, InitPrinter());

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
                BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.Default.GetBytes(address[0] + "\n"));

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
                for (int i = 1; i < address.Length; i++)
                {
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.Default.GetBytes(address[i] + "\n"));
                }
                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

                #region Title: DELIVERY NOTE
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());

                if (header.Paid)
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(oDict.GetWord("cash_note") + "\n"));
                else
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(oDict.GetWord("delivery_note") + "\n"));
                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

                #region Header
                #region Prepare Client Address
                //Client_AddressBook client = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", order.ClientID.ToString()));
                List<String> billToName = ConvertToMultipleLines(clientAddr.Name, 50);
                List<String> billToAddr = ConvertToMultipleLines(clientAddr.Address.Replace(System.Environment.NewLine, " ").Replace("\n", " "), 24);
                String billToTel = clientAddr.Tel;
                List<String> shipToName = new List<String>();
                List<String> shipToAddr = new List<String>();
                String shipToTel = String.Empty;

                //Order_Details orderDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", order.ID.ToString()));
                if (orderDtls != null)
                {
                    switch (orderDtls.DeliveryMethod)
                    {
                        case (int)Enums.DeliveryMethod.DeliverTo:
                            //Client_AddressBook delivery = Client_AddressBook.Load(orderDetails.DeliveryAddr);
                            if (deliverTo != null)
                            {
                                shipToName = ConvertToMultipleLines(deliverTo.Name, 24);
                                shipToAddr = ConvertToMultipleLines(deliverTo.Address.Replace(System.Environment.NewLine, " ").Replace("\n", " "), 24);
                                shipToTel = deliverTo.Tel;
                            }
                            break;
                        case (int)Enums.DeliveryMethod.PickUp:
                            shipToAddr.Add(String.Format("*** {0} ***", oDict.GetWord("pick_up").ToUpper()));
                            break;
                    }
                }

                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());

                switch (languageId)
                {
                    case 1:
                        #region 英文
                        if (header.Paid)
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(line = String.Format("{0,-24}{1,-8}/{2,-8}\n", oDict.GetWordWithColon("transaction#"), header.ReceiptNumber, header.INMasterId.ToString())));
                        else
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(line = String.Format("{0,-24}{1,-8}\n", oDict.GetWordWithColon("transaction#"), header.ReceiptNumber)));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(line = String.Format("{0,-24}{1:yyyy-MM-dd HH:mm:ss}\n", oDict.GetWordWithColon("date_time"), header.ReceiptDate)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(String.Format("{0}\n", oDict.GetWordWithColon("bill_to").ToUpper())));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());

                        for (int i = 0; i < billToName.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", billToName[i])));
                        }
                        for (int i = 0; i < billToAddr.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", billToAddr[i])));
                        }
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0} {1}\n", oDict.GetWordWithColon("tel"), billToTel)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(String.Format("{0}\n", oDict.GetWordWithColon("ship_to").ToUpper())));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());

                        for (int i = 0; i < shipToName.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", shipToName[i])));
                        }
                        for (int i = 0; i < shipToAddr.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", shipToAddr[i])));
                        }
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0} {1}\n", oDict.GetWordWithColon("tel"), shipToTel)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        #region items column header
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Qty                      Description                     Amount\n"));
                        #endregion

                        #endregion
                        break;
                    case 2:
                    case 3:
                        #region 中文
                        if (header.Paid)
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-6}{1,-8}/{2,-8}\n", oDict.GetWordWithColon("transaction#"), header.ReceiptNumber, header.INMasterId.ToString())));
                        else
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-6}{1,-8}\n", oDict.GetWordWithColon("transaction#"), header.ReceiptNumber)));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-6}{1:yyyy-MM-dd HH:mm:ss}\n", oDict.GetWordWithColon("date_time"), header.ReceiptDate)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", oDict.GetWordWithColon("bill_to"))));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());

                        for (int i = 0; i < billToName.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", billToName[i])));
                        }
                        for (int i = 0; i < billToAddr.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", billToAddr[i])));
                        }
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0} {1}\n", oDict.GetWordWithColon("tel"), billToTel)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", oDict.GetWordWithColon("ship_to"))));
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());

                        for (int i = 0; i < shipToName.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", shipToName[i])));
                        }
                        for (int i = 0; i < shipToAddr.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0}\n", shipToAddr[i])));
                        }
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0} {1}\n", oDict.GetWordWithColon("tel"), shipToTel)));

                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

                        #region items column header
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                        BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());

                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-19}{1,-19}{2,2}\n", oDict.GetWord("qty"), oDict.GetWord("item_description"), oDict.GetWord("amount"))));
                        #endregion

                        #endregion
                        break;
                }
                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

                #region Body
                int totalQty = 0;
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                //BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 1", 12, 11, 144.00));
                foreach (vwReceiptDetailsList item in items)
                {
                    List<String> itemDescription = ConvertToMultipleLines(item.ItemCode + " " + item.ItemDescription, 50);
                    var qty = item.ItemQty;
                    var amt = item.ItemAmount;
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(line = String.Format("{0} {1,-50} {2,8:N2}\n", qty.ToString().PadLeft(2), itemDescription[0], amt)));

                    // 如果超過一行，第二行開始唔使打印 Qty 同 Amount
                    if (itemDescription.Count > 1)
                    {
                        for (int i = 1; i < itemDescription.Count; i++)
                        {
                            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(line = String.Format("   {0,-50}\n", itemDescription[i].PadLeft(3))));
                        }
                    }
                    totalQty += (int)qty;
                }
                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

                #region Footer: left (total qty), right (total amount)
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());

                BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(String.Format("{0,2:N0}", totalQty)));

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());

                BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(String.Format("{0,8:N2}\n", header.ReceiptAmount)));
                #endregion

                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

                #region Barcode / QR Code
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
                BytesValue = PrintExtensions.AddBytes(BytesValue, BarcodeHeight(162 * 2));        // 冇用，PrinterUtility 自己控制咗
                BytesValue = PrintExtensions.AddBytes(BytesValue, Barcode(header.ReceiptNumber));

                //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128((hdr.Field<String>("ReceiptNumber")).ToString()));
                //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print((hdr.Field<String>("ReceiptNumber")).ToString(), PrinterUtility.Enums.QrCodeSize.Medio));

                #endregion

                #region Appedix / Ad
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
                BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
                BytesValue = PrintExtensions.AddBytes(BytesValue, DateTime.Now.ToString("yyyyMMddHHmmss") + "\n");
                #endregion

                //BytesValue = PrintExtensions.AddBytes(BytesValue, FeedLines(10));
                BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());
                BytesValue = PrintExtensions.AddBytes(BytesValue, FormFeed());

                return BytesValue;
            }
        }

        private List<String> ConvertToMultipleLines(String source, int chunkSize)
        {
            var result = source
                .Where((x, i) => i % chunkSize == 0)
                .Select(
                    (x, i) => new string(source
                        .Skip(i * chunkSize)
                        .Take(chunkSize)
                        .ToArray()))
                .ToArray();

            return result.ToList();
        }

        /// <summary>
        /// Bardcode height, n = 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private byte[] BarcodeHeight(int n = 162)
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)29);
            oby.Add((byte)104);
            oby.Add((byte)n);
            return oby.ToArray();
        }

        private byte[] Barcode(string n, int m = 4)
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)29);
            oby.Add((byte)107);
            oby.Add((byte)m);

            byte[] ascii = Encoding.ASCII.GetBytes(n);
            foreach (byte b in ascii)
            {
                oby.Add(b);
            }

            oby.Add((byte)0);
            return oby.ToArray();
        }

        private byte[] CutPage()
        {
            List<byte> oby = new List<byte>();
            //oby.Add(Convert.ToByte(Convert.ToChar(0x1D)));
            //oby.Add(Convert.ToByte('V'));
            oby.Add((byte)29);
            oby.Add((byte)86);
            oby.Add((byte)66);
            oby.Add((byte)0);
            return oby.ToArray();
        }

        /// <summary>
        /// Print buffer & Feed n lines
        /// </summary>
        /// <returns></returns>
        private byte[] FeedLines(int n)
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)27);
            oby.Add((byte)100);
            oby.Add((byte)n);
            return oby.ToArray();
        }

        /// <summary>
        /// Print buffer and return to standard mode
        /// </summary>
        /// <returns></returns>
        private byte[] FormFeed()
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)12);
            return oby.ToArray();
        }

        /// <summary>
        /// Initialize printer
        /// Clear print buffer and reset printer mode to Power On
        /// </summary>
        /// <returns></returns>
        private byte[] InitPrinter()
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)27);
            oby.Add((byte)64);
            return oby.ToArray();
        }

        /// <summary>
        /// Esc R 15：選擇字庫 15 (中國)
        /// </summary>
        /// <returns></returns>
        private byte[] SelectFont15()
        {
            List<byte> oby = new List<byte>();
            oby.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            oby.Add(Convert.ToByte('R'));
            oby.Add((byte)15);
            return oby.ToArray();
        }

        // http://alexandriacomputers.com/cash-drawer-codes/
        public byte[] OpenCashDrawer()
        {
            List<byte> oby = new List<byte>();
            oby.Add((byte)27);
            oby.Add((byte)112);
            oby.Add((byte)48);
            oby.Add((byte)55);
            oby.Add((byte)121);
            return oby.ToArray();
        }

        public byte[] GetLogo(string LogoPath)
        {
            List<byte> byteList = new List<byte>();
            //if (!File.Exists(LogoPath))
            //    return null;
            BitmapData data = GetBitmapData(LogoPath);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            // BinaryWriter bw = new BinaryWriter(stream);
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            //bw.Write((char));
            byteList.Add(Convert.ToByte('@'));
            //bw.Write('@');
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            // bw.Write((char)0x1B);
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            //bw.Write((byte)24);
            byteList.Add((byte)24);
            while (offset < data.Height)
            {
                byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
                byteList.Add(Convert.ToByte('*'));
                //bw.Write((char)0x1B);
                //bw.Write('*');         // bit-image mode
                byteList.Add((byte)33);
                //bw.Write((byte)33);    // 24-dot double-density
                byteList.Add(width[0]);
                byteList.Add(width[1]);
                //bw.Write(width[0]);  // width low byte
                //bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }
                        byteList.Add(slice);
                        //bw.Write(slice);
                    }
                }
                offset += 24;
                byteList.Add(Convert.ToByte(0x0A));
                //bw.Write((char));
            }
            // Restore the line spacing to the default of 30 dots.
            byteList.Add(Convert.ToByte(0x1B));
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            byteList.Add((byte)30);
            return byteList.ToArray();
            //bw.Flush();
            //byte[] bytes = stream.ToArray();
            //return logo + Encoding.Default.GetString(bytes);
        }

        public BitmapData GetBitmapData(string bmpFileName)
        {
            //改為 VWG 方式
            //Bitmap bp = (Bitmap)(new ImageResourceHandle("CompanyLogo.png")).ToImage();
            //using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            using (var bitmap = (Bitmap)(new ImageResourceHandle(bmpFileName)).ToImage())
            {
                var threshold = 127;
                var index = 0;
                double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}
