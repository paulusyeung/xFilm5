using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BitMiracle.LibTiff.Classic;

namespace xFilm3.QRCoder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if ((args.Length == 0) || (args.Length > 2))
            {   // 手動選檔案以及其他 parameters，用嚟做 debug
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {   // Command-line 提供檔案路徑，用 default paramters 自動 convert，然後關閉: xFilm3.QRCode.exe arg0 arg1
                String sourceTiff = args[0];
                String destFolder = args[1];
                if (File.Exists(sourceTiff) && Directory.Exists(destFolder))
                {
                    GlobalVar.SourceTiff = sourceTiff;
                    GlobalVar.FileName = Path.GetFileName(sourceTiff);
                    GlobalVar.DestFolder = destFolder;
                    Utility.ExtractQRCodeText();

                    Utility.GenQRCode();

                    using (Tiff image = Tiff.Open(GlobalVar.SourceTiff, "r"))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Utility.Merge();
                        Cursor.Current = Cursors.Default;
                    }
                }
                Application.Exit();
            }
        }
    }
}
