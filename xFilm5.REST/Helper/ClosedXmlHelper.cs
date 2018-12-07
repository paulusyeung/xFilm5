using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using xFilm5.EF6;

namespace xFilm5.REST.Helper
{
    public class ClosedXmlHelper
    {
        public static MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        public static XLWorkbook GetOrderListAsExcel(List<vwOrderList> items)
        {
            String wksheetName = "Order List";
            var wb = new XLWorkbook(XLEventTracking.Disabled);      // Turn off Tracking to save memory and increase performance
            wb.Worksheets.Add(wksheetName);
            var ws = wb.Worksheets.Worksheet(wksheetName);

            #region construct the worksheet
            #region loop items
            int row = 2;
            foreach (var item in items)
            {
                ws.Cell("A" + row).Value = item.OrderID;
                ws.Cell("B" + row).Value = item.ClientID;
                ws.Cell("C" + row).Value = item.ClientName;
                ws.Cell("D" + row).Value = item.Priority;
                ws.Cell("E" + row).Value = item.Remarks;
                ws.Cell("F" + row).Value = item.DateReceived;
                ws.Cell("G" + row).Value = item.DateCompleted;
                ws.Cell("H" + row).Value = item.OrderType;
                ws.Cell("I" + row).Value = item.OrderedBy;
                ws.Cell("J" + row).Value = item.Status;

                ++row;
            }
            #endregion

            #region column style
            ws.Column("A").Width = 10;
            ws.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("B").Width = 10;
            ws.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("C").Width = 30;
            ws.Column("C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("D").Width = 10;
            ws.Column("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("E").Width = 30;
            ws.Column("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("F").Width = 20;
            ws.Column("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Worksheet(wksheetName).Range(2, 6, row + 1, 6).Style.DateFormat.SetFormat("yyyy-MM-dd HH:mm");
            ws.Column("G").Width = 20;
            ws.Column("G").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Worksheet(wksheetName).Range(2, 7, row + 1, 7).Style.DateFormat.SetFormat("yyyy-MM-dd HH:mm");
            ws.Column("H").Width = 10;
            ws.Column("H").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("I").Width = 10;
            ws.Column("I").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("J").Width = 10;
            ws.Column("J").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            #endregion

            #region header title
            var header = ws.Range("A1:J1");
            header.Style.Font.Bold = true;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            header.Style.Fill.BackgroundColor = XLColor.Gray;
            header.Style.Font.FontColor = XLColor.White;

            ws.Cell("A" + 1).Value = "Order ID";
            ws.Cell("B" + 1).Value = "Client ID";
            ws.Cell("C" + 1).Value = "Client Name";
            ws.Cell("D" + 1).Value = "Priority";
            ws.Cell("E" + 1).Value = "Remarks";
            ws.Cell("F" + 1).Value = "Date Received";
            ws.Cell("G" + 1).Value = "Date Completed";
            ws.Cell("H" + 1).Value = "Order Type";
            ws.Cell("I" + 1).Value = "Ordered By";
            ws.Cell("J" + 1).Value = "Status";

            wb.Worksheet(wksheetName).Range(1, 1, row + 1, 10).Style.Font.FontName = "Arial";
            #endregion

            /** Not in use
            int rowcount = items.Count + 1;        // 計多一行，Header row
            wb.Worksheet(wksheetName).Range(2, 6, rowcount, 6).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");         // Receipt Amount
            wb.Worksheet(wksheetName).Range(2, 12, rowcount, 12).Style.NumberFormat.SetFormat("$#,##0;-$#,##0;;@");             // Paid Amount
            wb.Worksheet(wksheetName).Range(2, 25, rowcount, 25).Style.NumberFormat.SetFormat("$#,##0.000;-$#,##0.000;;@");     // Unit Amount
            wb.Worksheet(wksheetName).Range(2, 26, rowcount, 26).Style.NumberFormat.SetFormat("#,##0.00;-#,##0.00;;@");         // Discount
            wb.Worksheet(wksheetName).Range(2, 27, rowcount, 27).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");         // Amount
                                                                                                                                  //wb.Worksheet(wksheetName).Range(2, 12, rowcount, 12).Style.DateFormat.SetFormat("dd/MM/yyyy");                      // required on
                                                                                                                                  //wb.Worksheet(wksheetName).Range(2, 19, rowcount, 19).Style.DateFormat.SetFormat("dd/MM/yyyy");                      // completed on

            wb.Worksheet(wksheetName).Columns(9, 19).Hide();    // hide columns I to S

            // subtotal amount
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).FormulaA1 = String.Format("=SUBTOTAL(9,F2:F{0})", rowcount.ToString());
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Double;
            */
            #endregion

            return wb;
        }

        public static XLWorkbook GetReceiptListAsExcel(List<vwReceiptDetailsList_Ex> items)
        {
            String wksheetName = "Receipt List";
            var wb = new XLWorkbook(XLEventTracking.Disabled);      // Turn off Tracking to save memory and increase performance
            wb.Worksheets.Add(wksheetName);
            var ws = wb.Worksheets.Worksheet(wksheetName);

            #region construct the worksheet
            #region loop items
            int row = 2;
            string curReceiptNumber = "";
            foreach (var item in items)
            {
                var newReceipt = curReceiptNumber != item.ReceiptNumber;
                ws.Cell("A" + row).Value = newReceipt ? item.ReceiptNumber : "";
                ws.Cell("B" + row).Value = newReceipt ? item.ClientId.ToString() : "";
                ws.Cell("C" + row).Value = newReceipt ? item.ClientName : "";
                ws.Cell("D" + row).Value = newReceipt ? item.ReceiptDate.Value.ToString("yyyy-MM-dd hh:ss") : "";
                ws.Cell("E" + row).Value = newReceipt ? item.ReceiptAmount : 0;
                ws.Cell("F" + row).Value = newReceipt ? (item.Paid ? "Yes" : "No") : "";
                ws.Cell("G" + row).Value = item.OrderHeaderId;
                ws.Cell("H" + row).Value = item.OrderedOn;
                ws.Cell("I" + row).Value = item.OrderedClientUserName;
                ws.Cell("J" + row).Value = item.ItemCode;
                ws.Cell("K" + row).Value = item.ItemDescription.Substring(10);
                ws.Cell("L" + row).Value = item.ItemQty;
                ws.Cell("M" + row).Value = item.ItemAmount;

                ++row;
                curReceiptNumber = item.ReceiptNumber;
            }
            #endregion

            #region column style
            ws.Column("A").Width = 10;
            ws.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("B").Width = 10;
            ws.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //ws.Column("B").Style.NumberFormat.SetFormat("####;-####;;@");
            ws.Column("C").Width = 30;
            ws.Column("C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("D").Width = 20;
            ws.Column("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Worksheet(wksheetName).Range(2, 4, row + 1, 4).Style.DateFormat.SetFormat("yyyy-MM-dd HH:mm");
            ws.Column("E").Width = 15;
            ws.Column("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Column("E").Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");
            ws.Column("F").Width = 10;
            ws.Column("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            
            ws.Column("G").Width = 10;
            ws.Column("G").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("H").Width = 20;
            ws.Column("H").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Worksheet(wksheetName).Range(2, 8, row + 1, 8).Style.DateFormat.SetFormat("yyyy-MM-dd HH:mm");
            ws.Column("I").Width = 20;
            ws.Column("I").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("J").Width = 15;
            ws.Column("J").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("K").Width = 30;
            ws.Column("K").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column("L").Width = 10;
            ws.Column("L").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column("L").Style.NumberFormat.SetFormat("#,##0;-#,##0;;@");
            ws.Column("M").Width = 10;
            ws.Column("M").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Column("M").Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");
            #endregion

            #region header title
            var header = ws.Range("A1:M1");
            header.Style.Font.Bold = true;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            header.Style.Fill.BackgroundColor = XLColor.Gray;
            header.Style.Font.FontColor = XLColor.White;

            ws.Cell("A" + 1).Value = "Receipt #";
            ws.Cell("B" + 1).Value = "Client ID";
            ws.Cell("C" + 1).Value = "Client Name";
            ws.Cell("D" + 1).Value = "Receipt Date";
            ws.Cell("E" + 1).Value = "Receipt Amt.";
            ws.Cell("F" + 1).Value = "Paid";
            ws.Cell("G" + 1).Value = "Order #";
            ws.Cell("H" + 1).Value = "Ordered On";
            ws.Cell("I" + 1).Value = "Ordered By";
            ws.Cell("J" + 1).Value = "Size";
            ws.Cell("K" + 1).Value = "Description";
            ws.Cell("L" + 1).Value = "Qty";
            ws.Cell("M" + 1).Value = "Amount";

            wb.Worksheet(wksheetName).Range(1, 1, row + 1, 13).Style.Font.FontName = "Arial";
            #endregion

            /** Not in use
            int rowcount = items.Count + 1;        // 計多一行，Header row
            wb.Worksheet(wksheetName).Range(2, 6, rowcount, 6).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");         // Receipt Amount
            wb.Worksheet(wksheetName).Range(2, 12, rowcount, 12).Style.NumberFormat.SetFormat("$#,##0;-$#,##0;;@");             // Paid Amount
            wb.Worksheet(wksheetName).Range(2, 25, rowcount, 25).Style.NumberFormat.SetFormat("$#,##0.000;-$#,##0.000;;@");     // Unit Amount
            wb.Worksheet(wksheetName).Range(2, 26, rowcount, 26).Style.NumberFormat.SetFormat("#,##0.00;-#,##0.00;;@");         // Discount
            wb.Worksheet(wksheetName).Range(2, 27, rowcount, 27).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");         // Amount
                                                                                                                                  //wb.Worksheet(wksheetName).Range(2, 12, rowcount, 12).Style.DateFormat.SetFormat("dd/MM/yyyy");                      // required on
                                                                                                                                  //wb.Worksheet(wksheetName).Range(2, 19, rowcount, 19).Style.DateFormat.SetFormat("dd/MM/yyyy");                      // completed on

            wb.Worksheet(wksheetName).Columns(9, 19).Hide();    // hide columns I to S

            // subtotal amount
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).FormulaA1 = String.Format("=SUBTOTAL(9,F2:F{0})", rowcount.ToString());
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.NumberFormat.SetFormat("$#,##0.00;-$#,##0.00;;@");
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            wb.Worksheet(wksheetName).Cell(rowcount + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Double;
            */
            #endregion

            return wb;
        }
    }
}