using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gizmox.WebGUI.Forms;

namespace xFilm5.Controls.Excel
{
    public class ExcelEx
    {
        public static MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
    }
}
