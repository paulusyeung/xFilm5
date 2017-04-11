using Gizmox.WebGUI.Common.Resources;
using PrinterUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using xFilm5.DAL;

namespace xFilm5.JobOrder.Reports5
{
    class DN_POS80
    {
        public void Print(int receiptId)
        {
            using (DataTable dt = DataSource.DN(receiptId))
            {
                if (dt.Rows.Count > 0)
                {
                    var bytesValue = GetPrintContent(dt);

                    #region locate the local/ server printers
                    //LocalPrintServer server = new LocalPrintServer();
                    //var pq = server.DefaultPrintQueue;
                    //var pqs = server.GetPrintQueues();
                    //foreach (var p in pqs)
                    //{
                    //    String pname = p.Name;
                    //}

                    //// HACK: 可以睇見所有 CUPS2 嘅 print queues
                    //PrintServer pServer = new PrintServer(@"\\CUPS2");
                    //var pQueues = pServer.GetPrintQueues();
                    //PrintServer ppServer = new PrintServer(@"\\192.168.2.222");
                    //var ppQueues = ppServer.GetPrintQueues();

                    //var printers = new System.Printing.PrintServer(@"\\WIN-10pv").GetPrintQueues()
                    //    .Where(t =>
                    //    {
                    //        try { return t.IsShared && !t.IsNotAvailable; }
                    //        catch { return false; }
                    //    })
                    //    .Select(t => t.FullName)
                    //    .ToArray();

                    #endregion

                    #region HACK: 暫時得哩個辦法 ok，use RawPrinterHelper.cs 經 local printer send 去隻 CUPS xPrinter，有反應！
                    //IntPtr pUnmanagedBytes = new IntPtr(0);
                    //int nLength = bytesValue.Length;
                    //pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                    //xFilm5.Controls.RawPrinterHelper.SendBytesToPrinter(@"\\http://192.168.2.222:631\tx2", pUnmanagedBytes, nLength);
                    #endregion

                    // HACK: 用 TcpClient 當係 ip printer，唔得，可能要買 networ xprinter
                    //SendToTcpPrinter(BytesValue);
                    var t = Encoding.Default.GetString(bytesValue);
                    //t += Encoding.Default.GetString(bytesValue);
                    xFilm5.Controls.RawPrinterHelper.SendStringToPrinter(@"\\http://192.168.2.222:631\tx2", t);

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

                    File.WriteAllBytes(String.Format(@"C:\Shared\ThermalPrinter-{0}.txt", DateTime.Now.ToString("MMdd-HHmmss")), bytesValue);
                }
            }
        }

        static void SendToTcpPrinter(Byte[] data)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                String server = "192.168.2.188";
                Int32 port = 631;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                //Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                //data = new Byte[256];

                // String to store the response ASCII representation.
                //String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            //Console.WriteLine("\n Press Enter to continue...");
            //Console.Read();
        }

        // PrinterUtility video: https://www.youtube.com/watch?v=0mC_yvT3abw
        // paper:  576 dots/line
        // font A: 12x24 48 characters/line
        // font B:  9x17 64 characters/line
        // 中文字: 24x24 24 characters/line
        // CodePage: https://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx
        // GB18030 = Codepage 54936, GB2312 = 936, Big5 = 950
        private Byte[] GetPrintContent(DataTable dt)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);
            // DataTable dt = DataSource.DN(receiptId);
            DataRow hdr = dt.Rows[0];
            String line = String.Empty;
            const int codePage = 54936;

            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();

            #region Company Logo image
            //var BytesValue = GetLogo(@"C:\Shared\CompanyLogo.png");
            //var BytesValue = GetLogo("CompanyLogoBW.jpg");
            var BytesValue = new byte[0];   // Encoding.Unicode.GetBytes(string.Empty);
            #endregion

            #region Company Address
            DAL.OrderHeader order = DAL.OrderHeader.Load(hdr.Field<int>("OrderHeaderId"));
            String[] address = xFilm5.Controls.Utility.Owner.GetWorkshopAddress(order.ProofingOp).Split(new String[] { @"\n" }, StringSplitOptions.None);

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

            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(oDict.GetWord("delivery_note") + "\n"));
            #endregion

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());


            #region Header
            #region Prepare Client Address
            Client_AddressBook client = Client_AddressBook.LoadWhere(String.Format("ClientID = {0} AND PrimaryAddr = 1", order.ClientID.ToString()));
            List<String> billToName = ConvertToMultipleLines(client.Name, 50);
            List<String> billToAddr = ConvertToMultipleLines(client.Address.Replace(System.Environment.NewLine, " ").Replace("\n", " "), 24);
            String billToTel = client.Tel;
            List<String> shipToName = new List<String>();
            List<String> shipToAddr = new List<String>();
            String shipToTel = String.Empty;

            Order_Details orderDetails = Order_Details.LoadWhere(String.Format("OrderID = {0}", order.ID.ToString()));
            if (orderDetails != null)
            {
                switch (orderDetails.DeliveryMethod)
                {
                    case (int)Common.Enums.DeliveryMethod.DeliverTo:
                        Client_AddressBook delivery = Client_AddressBook.Load(orderDetails.DeliveryAddr);
                        if (delivery != null)
                        {
                            shipToName = ConvertToMultipleLines(delivery.Name, 24);
                            shipToAddr = ConvertToMultipleLines(delivery.Address.Replace(System.Environment.NewLine, " ").Replace("\n", " "), 24);
                            shipToTel = delivery.Tel; 
                        }
                        break;
                    case (int)Common.Enums.DeliveryMethod.PickUp:
                        shipToAddr.Add(String.Format("*** {0} ***", oDict.GetWord("pick_up").ToUpper()));
                        break;
                }
            }

            #endregion

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());

            switch (Common.Config.CurrentLanguageId)
            {
                case 1:
                    #region 英文
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(line = String.Format("{0,-24}{1,-8}\n", oDict.GetWordWithColon("transaction#"), hdr.Field<String>("ReceiptNumber"))));
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(line = String.Format("{0,-24}{1:yyyy-MM-dd HH:mm:ss}\n", oDict.GetWordWithColon("date_time"), hdr.Field<DateTime>("ReceiptDate"))));

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

                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("\n"));

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
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-6}{1,-8}\n", oDict.GetWordWithColon("transaction#"), hdr.Field<String>("ReceiptNumber"))));
                    BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.GetEncoding(codePage).GetBytes(String.Format("{0,-6}{1:yyyy-MM-dd HH:mm:ss}\n", oDict.GetWordWithColon("date_time"), hdr.Field<DateTime>("ReceiptDate"))));

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
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 1", 12, 11, 144.00));
            foreach (DataRow row in dt.Rows)
            {
                List<String> itemDescription = ConvertToMultipleLines(row.Field<String>("ItemDescription"), 50);
                var qty = row.Field<short>("ItemQty");
                var amt = row.Field<Decimal>("ItemAmount");
                BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(line = String.Format("{0} {1,-50} {2,8:N2}\n", qty.ToString().PadLeft(2), itemDescription[0], amt)));

                // 如果超過一行，第二行開始唔使打印 Qty 同 Amount
                if (itemDescription.Count > 1)
                {
                    for (int i = 1; i < itemDescription.Count; i++)
                    {
                        BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(line = String.Format("   {0,-50}\n", itemDescription[i].PadLeft(3))));
                    }
                }
            }
            #endregion

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

            #region Footer
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth2());

            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(String.Format("{0,8:N2}\n", hdr.Field<Decimal>("ReceiptAmount"))));
            #endregion

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontB());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());

            #region Barcode / QR Code
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, BarcodeHeight(162*2));        // 冇用，PrinterUtility 自己控制咗
            BytesValue = PrintExtensions.AddBytes(BytesValue, Barcode(hdr.Field<String>("ReceiptNumber")));

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
