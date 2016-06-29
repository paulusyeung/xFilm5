using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using FileHelpers;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using xFilm5.DAL;

namespace xFilm5.Controls.Export
{
    public class CsvLoader
    {
        public static void Client_Email(ref Gizmox.WebGUI.Forms.ListView lvw, string fileName)
        {
            if ((lvw.Items.Count > 0) && (fileName != String.Empty))
            {
                List<CsvData.ClientEmailCsv> records = new List<CsvData.ClientEmailCsv>();

                #region Header
                CsvData.ClientEmailCsv header = new CsvData.ClientEmailCsv();
                header.Branch       = lvw.Columns[10].Text;
                header.ClientId     = lvw.Columns[4].Text;
                header.ClientName   = lvw.Columns[0].Text;
                header.Contact      = lvw.Columns[8].Text;
                header.Email        = lvw.Columns[11].Text;

                records.Add(header);
                #endregion

                foreach (ListViewItem row in lvw.Items)
                {
                    CsvData.ClientEmailCsv item = new CsvData.ClientEmailCsv();
                    item.Branch     = row.SubItems[10].Text;
                    item.ClientId   = row.SubItems[4].Text;
                    item.ClientName = row.SubItems[0].Text;
                    item.Contact    = row.SubItems[8].Text;
                    item.Email      = row.SubItems[11].Text;

                    records.Add(item);
                }

                FileHelperEngine engine = new FileHelperEngine(typeof(CsvData.ClientEmailCsv));

                engine.Encoding = System.Text.Encoding.Unicode;

                engine.WriteFile(Path.Combine(DAL.Common.Config.OutBox, fileName), records);

            }
        }

        public static void Client_Details(ref Gizmox.WebGUI.Forms.ListView lvw, string fileName)
        {
            if ((lvw.Items.Count > 0) && (fileName != String.Empty))
            {
                List<CsvData.ClientDetailsCsv> records = new List<CsvData.ClientDetailsCsv>();

                #region Header
                CsvData.ClientDetailsCsv header = new CsvData.ClientDetailsCsv();
                header.ClientName   = lvw.Columns[0].Text;
                header.ClientId     = lvw.Columns[4].Text;
                header.Address      = lvw.Columns[5].Text;
                header.Tel          = lvw.Columns[6].Text;
                header.Fax          = lvw.Columns[7].Text;
                header.Contact      = lvw.Columns[8].Text;
                header.Mobile       = lvw.Columns[9].Text;
                header.Branch       = lvw.Columns[10].Text;
                header.Email        = lvw.Columns[11].Text;

                records.Add(header);
                #endregion

                foreach (ListViewItem row in lvw.Items)
                {
                    CsvData.ClientDetailsCsv item = new CsvData.ClientDetailsCsv();
                    item.ClientName = row.SubItems[0].Text;
                    item.ClientId   = row.SubItems[4].Text;
                    item.Address    = row.SubItems[5].Text.Replace(Environment.NewLine, ", ");
                    item.Tel        = row.SubItems[6].Text;
                    item.Fax        = row.SubItems[7].Text;
                    item.Mobile     = row.SubItems[9].Text;
                    item.Contact    = row.SubItems[8].Text;
                    item.Branch     = row.SubItems[10].Text;
                    item.Email      = row.SubItems[11].Text;

                    records.Add(item);
                }

                FileHelperEngine engine = new FileHelperEngine(typeof(CsvData.ClientDetailsCsv));

                engine.Encoding = System.Text.Encoding.Unicode;

                engine.WriteFile(Path.Combine(DAL.Common.Config.OutBox, fileName), records);

            }
        }
    }

}
