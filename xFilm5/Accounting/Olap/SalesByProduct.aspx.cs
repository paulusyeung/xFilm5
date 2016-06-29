using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Utils;

using xFilm5.DAL;

namespace xFilm5.Accounting.Olap
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SalesByProduct : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                BindData();
                olapPivotGrid.DataBind();
                CollapsePivotGrid();
            }
        }

        #region APSxPivotGrid
        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            olapPivotGrid.Fields.Clear();

            #region Row Area
            // Workshop
            PivotGridField workshop = new PivotGridField("Workshop", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            workshop.Caption = "Workshop";
            workshop.AreaIndex = 0;

            if (!olapPivotGrid.Fields.Contains(workshop))
            {
                olapPivotGrid.Fields.AddField(workshop);
            }

            // Client Name
            PivotGridField product = new PivotGridField("Product", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            product.Caption = "Product";
            product.AreaIndex = 1;

            if (!olapPivotGrid.Fields.Contains(product))
            {
                olapPivotGrid.Fields.AddField(product);
            }
            #endregion

            #region Data Area
            // Invoice Amount
            PivotGridField invoiceAmt = new PivotGridField("Amount", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            invoiceAmt.Caption = "Amount";
            invoiceAmt.AreaIndex = 0;
            invoiceAmt.CellFormat.FormatString = "{0:n2}";
            invoiceAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            if (!olapPivotGrid.Fields.Contains(invoiceAmt))
            {
                olapPivotGrid.Fields.AddField(invoiceAmt);
            }

            // Qty
            PivotGridField qty = new PivotGridField("Qty", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            qty.Caption = "Qty";
            qty.AreaIndex = 0;
            qty.CellFormat.FormatString = "{0:n0}";
            qty.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            if (!olapPivotGrid.Fields.Contains(qty))
            {
                olapPivotGrid.Fields.AddField(qty);
            }
            #endregion

            #region Filter Area

            // Invoice Number
            PivotGridField invoiceNumber = new PivotGridField("InvoiceNumber", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            invoiceNumber.Caption = "Invoice No.";
//            invoiceNumber.AreaIndex = 2;

            if (!olapPivotGrid.Fields.Contains(invoiceNumber))
            {
                olapPivotGrid.Fields.AddField(invoiceNumber);
            }

            // Invoice Date
            PivotGridField invoiceDate = new PivotGridField("InvoiceDate", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            invoiceDate.Caption = "Invoice Date";
//            invoiceDate.AreaIndex = 3;

            if (!olapPivotGrid.Fields.Contains(invoiceDate))
            {
                olapPivotGrid.Fields.AddField(invoiceDate);
            }

            // Order ID
            PivotGridField orderId = new PivotGridField("OrderNumber", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            orderId.Caption = "Order ID";
//            orderId.AreaIndex = 4;

            if (!olapPivotGrid.Fields.Contains(orderId))
            {
                olapPivotGrid.Fields.AddField(orderId);
            }

            PivotGridField year = new PivotGridField("Year", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            year.Caption = "Year";
            if (!olapPivotGrid.Fields.Contains(year))
            {
                olapPivotGrid.Fields.AddField(year);
            }

            PivotGridField month = new PivotGridField("Month", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            month.Caption = "Month";

            if (!olapPivotGrid.Fields.Contains(month))
            {
                olapPivotGrid.Fields.AddField(month);
            }

            PivotGridField day = new PivotGridField("Day", DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            day.Caption = "Day";

            if (!olapPivotGrid.Fields.Contains(day))
            {
                olapPivotGrid.Fields.AddField(day);
            }
            #endregion
        }

        private void Export(bool saveAs)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                //this.olapPivotGridExporter.OptionsPrint.PrintHeadersOnEveryPage = checkPrintHeadersOnEveryPage.Checked;
                //this.olapPivotGridExporter.OptionsPrint.PrintFilterHeaders = checkPrintFilterHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
                //this.olapPivotGridExporter.OptionsPrint.PrintColumnHeaders = checkPrintColumnHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
                //this.olapPivotGridExporter.OptionsPrint.PrintRowHeaders = checkPrintRowHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
                //this.olapPivotGridExporter.OptionsPrint.PrintDataHeaders = checkPrintDataHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;

                string contentType = "", fileName = "";
                //switch (listExportFormat.SelectedIndex)
                //{
                //    case 0:
                //        contentType = "application/pdf";
                //        fileName = "PivotGrid.pdf";
                //        this.olapPivotGridExporter.ExportToPdf(stream);
                //        break;
                //    case 1:
                //        contentType = "application/ms-excel";
                //        fileName = "PivotGrid.xls";
                //        this.olapPivotGridExporter.ExportToXls(stream);
                //        break;
                //    case 2:
                //        contentType = "text/enriched";
                //        fileName = "PivotGrid.rtf";
                //        this.olapPivotGridExporter.ExportToRtf(stream);
                //        break;
                //    case 3:
                //        contentType = "text/plain";
                //        fileName = "PivotGrid.txt";
                //        this.olapPivotGridExporter.ExportToText(stream);
                //        break;
                //}

                byte[] buffer = stream.GetBuffer();

                string disposition = saveAs ? "attachment" : "inline";
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", contentType);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", disposition + "; filename=" + fileName);
                Response.BinaryWrite(buffer);
                Response.End();
            }
        }

        private void CollapsePivotGrid()
        {
            foreach (DevExpress.Web.ASPxPivotGrid.PivotGridField f in olapPivotGrid.Fields)
            {
                if (f.Area.ToString() == "RowArea" && f.AreaIndex >= 0)
                {
                    f.CollapseAll();
                }
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnSaveAs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            Export(true);
        }

        /// <summary>
        /// Handles the Click event of the btOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btOpen_Click(object sender, EventArgs e)
        {
            Export(false);
        }

        #region Properties
        /// <summary>
        /// Gets from date.
        /// </summary>
        /// <value>From date.</value>
        private DateTime FromDate
        {
            get
            {
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
        }

        /// <summary>
        /// Gets to date.
        /// </summary>
        /// <value>To date.</value>
        private DateTime ToDate
        {
            get
            {
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            }
        }
        #endregion
    }
}
