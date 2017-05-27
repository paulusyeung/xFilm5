using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitMiracle.LibTiff.Classic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace xFilm3.QRCoder
{
    class LibTiffHelper
    {
        public static Bitmap BWTiffToBitmap(string tiffFileName)
        {
            using (Tiff tif = Tiff.Open(tiffFileName, "r"))
            {
                if (tif == null)
                    return null;

                FieldValue[] imageHeight = tif.GetField(TiffTag.IMAGELENGTH);
                int height = imageHeight[0].ToInt();

                FieldValue[] imageWidth = tif.GetField(TiffTag.IMAGEWIDTH);
                int width = imageWidth[0].ToInt();

                FieldValue[] bitsPerSample = tif.GetField(TiffTag.BITSPERSAMPLE);
                short bpp = bitsPerSample[0].ToShort();
                if (bpp != 1)
                    return null;

                FieldValue[] samplesPerPixel = tif.GetField(TiffTag.SAMPLESPERPIXEL);
                short spp = samplesPerPixel[0].ToShort();
                if (spp != 1)
                    return null;

                FieldValue[] photoMetric = tif.GetField(TiffTag.PHOTOMETRIC);
                Photometric photo = (Photometric)photoMetric[0].ToInt();
                if (photo != Photometric.MINISBLACK && photo != Photometric.MINISWHITE)
                    return null;

                int stride = tif.ScanlineSize();
                Bitmap result = new Bitmap(width, height, PixelFormat.Format1bppIndexed);

                // update bitmap palette according to Photometric value
                bool minIsWhite = (photo == Photometric.MINISWHITE);
                ColorPalette palette = result.Palette;
                palette.Entries[0] = (minIsWhite ? Color.White : Color.Black);
                palette.Entries[1] = (minIsWhite ? Color.Black : Color.White);
                result.Palette = palette;

                for (int i = 0; i < height; i++)
                {
                    Rectangle imgRect = new Rectangle(0, i, width, 1);
                    BitmapData imgData = result.LockBits(imgRect, ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

                    byte[] buffer = new byte[stride];
                    tif.ReadScanline(buffer, i);

                    Marshal.Copy(buffer, 0, imgData.Scan0, buffer.Length);
                    result.UnlockBits(imgData);
                }

                return result;
            }
        }

        public static void ConvertColorTiffTo32bitBitmap(string tiffFileName)
        {
            using (Tiff tif = Tiff.Open(tiffFileName, "r"))
            {
                // Find the width and height of the image
                FieldValue[] value = tif.GetField(TiffTag.IMAGEWIDTH);
                int width = value[0].ToInt();

                value = tif.GetField(TiffTag.IMAGELENGTH);
                int height = value[0].ToInt();

                // Read the image into the memory buffer
                int[] raster = new int[height * width];
                if (!tif.ReadRGBAImage(width, height, raster))
                {
                    System.Windows.Forms.MessageBox.Show("Could not read image");
                    return;
                }

                using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppRgb))
                {
                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                    BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                    byte[] bits = new byte[bmpdata.Stride * bmpdata.Height];

                    for (int y = 0; y < bmp.Height; y++)
                    {
                        int rasterOffset = y * bmp.Width;
                        int bitsOffset = (bmp.Height - y - 1) * bmpdata.Stride;

                        for (int x = 0; x < bmp.Width; x++)
                        {
                            int rgba = raster[rasterOffset++];
                            bits[bitsOffset++] = (byte)((rgba >> 16) & 0xff);
                            bits[bitsOffset++] = (byte)((rgba >> 8) & 0xff);
                            bits[bitsOffset++] = (byte)(rgba & 0xff);
                            bits[bitsOffset++] = (byte)((rgba >> 24) & 0xff);
                        }
                    }

                    Marshal.Copy(bits, 0, bmpdata.Scan0, bits.Length);
                    bmp.UnlockBits(bmpdata);

                    bmp.Save(tiffFileName + ".bmp");
                    System.Diagnostics.Process.Start(tiffFileName + ".bmp");
                }
            }
        }
    }
}
