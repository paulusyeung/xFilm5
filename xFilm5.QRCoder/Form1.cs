using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using BitMiracle.LibTiff.Classic;
using ZXing;
using ZXing.Common;

namespace xFilm5.QRCoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdTiffFile_Click(object sender, EventArgs e)
        {
            using (var openDlg = new OpenFileDialog())
            {
                openDlg.FileName = txtTiffFile.Text;
                openDlg.Multiselect = false;
                openDlg.Filter = "TIFF files|*.tif;.tiff";
                if (openDlg.ShowDialog(this) == DialogResult.OK)
                {
                    GlobalVar.SourceTiff = openDlg.FileName;
                    GlobalVar.FileName = openDlg.SafeFileName;
                    GlobalVar.DestFolder = Path.GetDirectoryName(openDlg.FileName) + @"\QRCoder";
                    Utility.ExtractQRCodeText();

                    txtTiffFile.Text = GlobalVar.FileName;
                    txtQRCodeText.Text = GlobalVar.QRCodeText;

                    //Utility.ShowTiffTags();
                }
            }
        }

        private void cmdEncode_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.GenQRCode(Convert.ToInt16(cboQRCodeSize.Text));

                picQRCode.Image = GlobalVar.QRCode;
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdGenTiff_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ImageToBitonalTiff.Write(GlobalVar.QRCode, GlobalVar.SourceTiff, Convert.ToInt16(cboTiffResolution.Text));
            Cursor.Current = Cursors.Default;
            MessageBox.Show(String.Format("Generate QR Code Tiff:\nSize {0} x {0}, Resolution {1}\ndone!", cboQRCodeSize.Text, cboTiffResolution.Text));
        }

        private void cmdMerge_Click(object sender, EventArgs e)
        {
            using (Tiff image = Tiff.Open(GlobalVar.SourceTiff, "r"))
            {
                Cursor.Current = Cursors.WaitCursor;
                Utility.Merge();
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
