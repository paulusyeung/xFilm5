using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace xFilm3.QRCoder
{
    public static class GlobalVar
    {
        private static Bitmap _QRCode;
        private static int _TiffResolution = 2400;
        private static String _DestFolder;

        public static Bitmap QRCode
        {
            get { return _QRCode; }
            set { _QRCode = value; }
        }

        public static String FileName = String.Empty;
        public static String SourceTiff = String.Empty;
        public static String DestFolder
        {
            get { return _DestFolder; }
            set {
                _DestFolder = value;
                if (!(Directory.Exists(_DestFolder)))
                {
                    Directory.CreateDirectory(_DestFolder);
                }
            }
        }
        public static String QRCodeText = String.Empty;

        public static int TiffResolution
        {
            get { return _TiffResolution; }
            set { _TiffResolution = value; }
        }
    }
}
