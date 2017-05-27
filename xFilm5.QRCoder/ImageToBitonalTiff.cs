using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using BitMiracle.LibTiff.Classic;

namespace xFilm5.QRCoder
{
    static class ImageToBitonalTiff
    {
        public static void Write(Bitmap imgSource, String destTiffFilePathName, int tiffDpi)
        {
            // convert using WriteEncodedStrip （試過，這個格式唔得）
            //byte[] tiffBytes = GetTiffImageBytes(imgSource, false, tiffDpi, destTiffFilePathName);
            //File.WriteAllBytes(destTiffFilePathName + ".QRCodeStrip.tif", tiffBytes);

            // make another conversion using WriteScanline
            byte[] tiffBytes = GetTiffImageBytes(imgSource, true, tiffDpi, destTiffFilePathName);
            File.WriteAllBytes(Path.Combine(GlobalVar.DestFolder, GlobalVar.FileName), tiffBytes);
        }

        public static byte[] GetTiffImageBytes(Bitmap img, bool byScanlines, int tiffDpi, String destTiffFilePathName)
        {
            try
            {
                byte[] raster = GetImageRasterBytes(img);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Tiff tif = Tiff.ClientOpen("InMemory", "w", ms, new TiffStream()))
                    {
                        if (tif == null)
                            return null;
                        Tiff org = Tiff.Open(destTiffFilePathName, "r");                        // retrieve the origional tiff for tags
                        tif.SetField(TiffTag.IMAGEWIDTH, img.Width);
                        tif.SetField(TiffTag.IMAGELENGTH, img.Height);
                        tif.SetField(TiffTag.COMPRESSION, Compression.LZW);                     //.CCITTFAX4);
                        tif.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISWHITE);              //.MINISBLACK);

                        if (byScanlines)
                            tif.SetField(TiffTag.ROWSPERSTRIP, 512);                            //img.Height);
                        else
                            tif.SetField(TiffTag.ROWSPERSTRIP, img.Height);                     //img.Height);

                        tif.SetField(TiffTag.XRESOLUTION, tiffDpi);                             //img.HorizontalResolution);
                        tif.SetField(TiffTag.YRESOLUTION, tiffDpi);                             //img.VerticalResolution);

                        //tif.SetField(TiffTag.SUBFILETYPE, 0);
                        tif.SetField(TiffTag.BITSPERSAMPLE, 1);
                        //tif.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);
                        tif.SetField(TiffTag.ORIENTATION, Orientation.TOPLEFT);

                        tif.SetField(TiffTag.SAMPLESPERPIXEL, 1);
                        //tif.SetField(TiffTag.T6OPTIONS, 0);
                        tif.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.INCH);

                        //tif.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);

                        tif.SetField(TiffTag.IMAGEDESCRIPTION, org.GetField(TiffTag.IMAGEDESCRIPTION)[0].ToString());
                        tif.SetField(TiffTag.MODEL, "Prinergy");
                        tif.SetField(TiffTag.MAKE, "directoutput.com.hk");
                        tif.SetField(TiffTag.SOFTWARE, "xFilm3");
                        org.Dispose();

                        int tiffStride = tif.ScanlineSize();
                        int stride = raster.Length / img.Height;

                        if (byScanlines)
                        {
                            #region conversion using WriteScanline
                            // raster stride MAY be bigger than TIFF stride (due to padding in raster bits)
                            for (int i = 0, offset = 0; i < img.Height; i++)
                            {
                                bool res = tif.WriteScanline(raster, offset, i, 0);
                                if (!res)
                                    return null;

                                offset += stride;
                            }
                            #endregion
                        }
                        else
                        {
                            #region convert using WriteEncodedStrip
                            if (tiffStride < stride)
                            {
                                // raster stride is bigger than TIFF stride
                                // this is due to padding in raster bits
                                // we need to create correct TIFF strip and write it into TIFF

                                byte[] stripBits = new byte[tiffStride * img.Height];
                                for (int i = 0, rasterPos = 0, stripPos = 0; i < img.Height; i++)
                                {
                                    System.Buffer.BlockCopy(raster, rasterPos, stripBits, stripPos, tiffStride);
                                    rasterPos += stride;
                                    stripPos += tiffStride;
                                }

                                // Write the information to the file
                                int n = tif.WriteEncodedStrip(0, stripBits, stripBits.Length);
                                if (n <= 0)
                                    return null;
                            }
                            else
                            {
                                // Write the information to the file
                                int n = tif.WriteEncodedStrip(0, raster, raster.Length);
                                if (n <= 0)
                                    return null;
                            }
                            #endregion
                        }
                    }

                    return ms.GetBuffer();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte[] GetImageRasterBytes(Bitmap img)
        {
            // Specify full image
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);

            Bitmap bmp = img;
            byte[] bits = null;

            try
            {
                // Lock the managed memory
                if (img.PixelFormat != PixelFormat.Format1bppIndexed)
                    bmp = convertToBitonal(img);

                BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);

                // Declare an array to hold the bytes of the bitmap.
                bits = new byte[bmpdata.Stride * bmpdata.Height];

                // Copy the sample values into the array.
                Marshal.Copy(bmpdata.Scan0, bits, 0, bits.Length);

                // Release managed memory
                bmp.UnlockBits(bmpdata);
            }
            finally
            {
                if (bmp != img)
                    bmp.Dispose();
            }

            return bits;
        }

        private static Bitmap convertToBitonal(Bitmap original)
        {
            int sourceStride;
            byte[] sourceBuffer = extractBytes(original, out sourceStride);

            // Create destination bitmap
            Bitmap destination = new Bitmap(original.Width, original.Height,
                PixelFormat.Format1bppIndexed);

            destination.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(
                new Rectangle(0, 0, destination.Width, destination.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create buffer for destination bitmap bits
            int imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = destination.Height;
            int width = destination.Width;
            int threshold = 500;

            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceStride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;

                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] +
                        sourceBuffer[sourceIndex + 3];

                    if (pixelTotal > threshold)
                        destinationValue += (byte)pixelValue;

                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }

                    sourceIndex += 4;
                }

                if (pixelValue != 128)
                    destinationBuffer[destinationIndex] = destinationValue;
            }

            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);
            destination.UnlockBits(destinationData);
            return destination;
        }

        private static byte[] extractBytes(Bitmap original, out int stride)
        {
            Bitmap source = null;

            try
            {
                // If original bitmap is not already in 32 BPP, ARGB format, then convert
                if (original.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                    source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                    using (Graphics g = Graphics.FromImage(source))
                    {
                        g.DrawImageUnscaled(original, 0, 0);
                    }
                }
                else
                {
                    source = original;
                }

                // Lock source bitmap in memory
                BitmapData sourceData = source.LockBits(
                    new Rectangle(0, 0, source.Width, source.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                // Copy image data to binary array
                int imageSize = sourceData.Stride * sourceData.Height;
                byte[] sourceBuffer = new byte[imageSize];
                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

                // Unlock source bitmap
                source.UnlockBits(sourceData);

                stride = sourceData.Stride;
                return sourceBuffer;
            }
            finally
            {
                if (source != original)
                    source.Dispose();
            }

        }

        private static void SetTiffTags(Tiff tiffInt, String tiffExt)
        {
            using (Tiff img = Tiff.Open(tiffExt, "r"))
            {
                short numberOfDirectories = img.NumberOfDirectories();
                for (short d = 0; d < numberOfDirectories; ++d)
                {
                    //if (d != 0)
                    //    writer.WriteLine("---------------------------------");

                    img.SetDirectory((short)d);

                    //writer.WriteLine("Image {0}, page {1} has following tags set:\n", fileName, d);
                    for (ushort t = ushort.MinValue; t < ushort.MaxValue; ++t)
                    {
                        TiffTag tag = (TiffTag)t;
                        FieldValue[] value = img.GetField(tag);
                        if (value != null)
                        {
                            //for (int j = 0; j < value.Length; j++)
                            //{
                            //    writer.WriteLine("{0}", tag.ToString());
                            //    writer.WriteLine("{0} : {1}", value[j].Value.GetType().ToString(), value[j].ToString());
                            //}

                            //writer.WriteLine();
                            tiffInt.SetField(tag, value);
                        }
                    }
                }
                img.Dispose();
            }
        }
    }
}
