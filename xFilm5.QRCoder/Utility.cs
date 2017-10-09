using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using BitMiracle.LibTiff.Classic;
using ZXing;
using ZXing.Common;
using System.IO;
using ZXing.QrCode;

namespace xFilm5.QRCoder
{
    class Utility
    {
        /*  Workshop    QR Size             offset  marginY
         *  KT          600 = 10mm/950      1040    900
         *  TW          420 = 7mm/660       730     1560
         */
        public static void GenQRCode()
        {
            // 600 = 1040x1040
            GenQRCode(420);     //改 Size 要改埋 Offset.  Size: KT = 600, TW = 420  Offset: KT = 1040, TW = 1370
        }

        public static void GenQRCode(int QRCodeSize)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Height = (QRCodeSize * 2),
                    Width = (QRCodeSize * 2)
                }
            };

            GlobalVar.QRCode = writer.Write(GlobalVar.QRCodeText);
            Utility.ConvertQRCodeTo32bppPArgb();                      //Ensure that QRCode is 32 bit per pixel
        }

        /// <summary>
        /// 轉換為 32bppPArgb 格式，方便轉 1bpp TIFF
        /// </summary>
        /// <param name="img"></param>
        public static void ConvertQRCodeTo32bppPArgb()
        {
            if (GlobalVar.QRCode.PixelFormat != PixelFormat.Format32bppPArgb)
            {
                Bitmap tmp = new Bitmap(GlobalVar.QRCode.Width, GlobalVar.QRCode.Height, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(tmp);
                g.DrawImage(GlobalVar.QRCode, new Rectangle(0, 0, GlobalVar.QRCode.Width, GlobalVar.QRCode.Height), 0, 0, GlobalVar.QRCode.Width, GlobalVar.QRCode.Height, GraphicsUnit.Pixel);
                GlobalVar.QRCode.Dispose();
                g.Dispose();
                GlobalVar.QRCode = tmp;
            }
        }

        protected static void SetIndexedPixel(int x, int y, BitmapData bmd, bool pixel)
        {
            int index = y * bmd.Stride + (x >> 3);
            byte p = Marshal.ReadByte(bmd.Scan0, index);
            byte mask = (byte)(0x80 >> (x & 0x7));

            if (pixel)
                p |= mask;
            else
                p &= (byte)(mask ^ 0xff);

            Marshal.WriteByte(bmd.Scan0, index, p);
        }

        unsafe public static void Merge()
        {
            using (Bitmap bitmap = LibTiffHelper.BWTiffToBitmap(GlobalVar.SourceTiff))
            {
                if (bitmap != null)
                {
                    int offset = 730;  // 等於隻 QR Code (600 size) 嘅 pixel Width or Height
                    //int margin = 600;
                    int marginX = 1, marginY = 1560; // TW = 1560, KT = 900
                    //int x0 = (bitmap.Width - _QRCode.Width) - margin, y0 = (bitmap.Height - _QRCode.Height) - margin;     //右下角
                    //int x0 = marginX, y0 = (bitmap.Height - GlobalVar.QRCode.Height) - marginY;                             //左下角
                    int x0 = (Math.Abs(bitmap.Width/2) - 8400 - offset), y0 = (bitmap.Height - offset) - marginY;  //左中下
                    int w = GlobalVar.QRCode.Width, h = GlobalVar.QRCode.Height;

                    //注意：qrcLocked 是 32bppPArgb，而 tifLocked 是 1bpp
                    BitmapData tifLocked = bitmap.LockBits(new Rectangle(x0, y0, w, h), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    BitmapData qrcLocked = GlobalVar.QRCode.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, GlobalVar.QRCode.PixelFormat);

                    //把 rqcLocked 轉為 1bpp
                    int x, y;
                    for (y = 0; y < qrcLocked.Height; y++)
                    {
                        for (x = 0; x < qrcLocked.Width; x++)
                        {
                            //generate the address of the colour pixel
                            int index = y * qrcLocked.Stride + (x * 4);

                            //check its brightness
                            if (Color.FromArgb(Marshal.ReadByte(qrcLocked.Scan0, index + 2),
                                Marshal.ReadByte(qrcLocked.Scan0, index + 1),
                                Marshal.ReadByte(qrcLocked.Scan0, index)).GetBrightness() > 0.5f)
                            {
                                SetIndexedPixel(x, y, tifLocked, false);   //set it if its bright.
                            }
                            else
                            {
                                SetIndexedPixel(x, y, tifLocked, true);    //set it if its dark.
                            }
                        }
                    }
                    GlobalVar.QRCode.UnlockBits(qrcLocked);         //Release LockBits，
                    bitmap.UnlockBits(tifLocked);                   // 同時把改動寫回原來的 image
                }
                ImageToBitonalTiff.Write(bitmap, GlobalVar.SourceTiff, Convert.ToInt32(GlobalVar.TiffResolution));

                //bitmap.Save(_FilePath + ".bmp");
                bitmap.Dispose();
                GlobalVar.QRCode.Dispose();
            }
        }

        public static void ShowTiffTags()
        {
            using (Tiff img = Tiff.Open(GlobalVar.SourceTiff, "r"))
            {
                FieldValue[] value = img.GetField(TiffTag.IMAGEWIDTH);
                int width = value[0].ToInt();

                value = img.GetField(TiffTag.IMAGELENGTH);
                int height = value[0].ToInt();

                value = img.GetField(TiffTag.XRESOLUTION);
                float dpiX = value[0].ToFloat();

                value = img.GetField(TiffTag.YRESOLUTION);
                float dpiY = value[0].ToInt();

                value = img.GetField(TiffTag.ROWSPERSTRIP);
                int stripRows = value[0].ToInt();

                value = img.GetField(TiffTag.STRIPOFFSETS);
                int[] stripOffsets = value[0].ToIntArray();

                value = img.GetField(TiffTag.STRIPBYTECOUNTS);
                int[] stripByteCounts = value[0].ToIntArray();

                img.Dispose();

                MessageBox.Show(string.Format("Width = {0}, Height = {1},\nDPI = {2}x{3}\nRows/Strip = {4}",
                    width, height, dpiX, dpiY, stripRows), "TIFF properties");
            }
        }

        public static void ExtractQRCodeText()
        {
            String filename = Path.GetFileNameWithoutExtension(GlobalVar.SourceTiff);
            String result = String.Empty;
            String[] filenames = filename.Split('.');

            if (filenames.Length < 3)
            {
                result = filename;
            }
            else
            {
                result = filenames[0] + Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + filename.Substring(filenames[0].Length + 1);
            }

            GlobalVar.QRCodeText = result;
        }
    }
}
