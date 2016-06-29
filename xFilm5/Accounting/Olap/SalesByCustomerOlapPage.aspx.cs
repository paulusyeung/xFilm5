using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Gizmox.WebGUI;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraExport;
using DevExpress.XtraPivotGrid;

using xFilm5.DAL;

namespace xFilm5.Accounting.Olap
{
    public partial class SalesByCustomerOlapPage : Gizmox.WebGUI.Forms.Hosts.AspPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 砌個 Toolbar - Export To Excel
            this.ansOlap.Items.Clear();
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem();
            item.Image.Url = "~//Resources//Icons//16x16//16_Excel.gif";
            item.Text = PageContext.Text;       //攞 RtfStats.cs 提供嘅名稱（有中英亙捩）

            this.ansOlap.Items.Add(item);
            this.ansOlap.ItemClick += new DevExpress.Web.MenuItemEventHandler(ansReceivablesOlap_ItemClick);
            #endregion

            pvgExporter.ASPxPivotGridID = pvgOlap.ID;

            //pvgReceivablesOlap.CustomCellStyle += new PivotCustomCellStyleEventHandler(pvgReceivablesOlap_CustomCellStyle);

            BindData();
        }

        void pvgReceivablesOlap_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
        {
            if (e.ColumnValueType == PivotGridValueType.GrandTotal || e.ColumnValueType == PivotGridValueType.Total)
            {
                e.CellStyle.Font.Bold = true;
            }

            if (e.ColumnValueType != PivotGridValueType.Value || e.RowValueType != PivotGridValueType.Value)
                return;

        }

        void ansReceivablesOlap_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            ExportToExcel();
        }

        private void BindData()
        {
            String sql = @"
SELECT [Workshop]
	, [ClientName]
	, [Year]
	, [Month]
	, [Day]
	, CONVERT(NVARCHAR(10), [InvoiceDate], 120) AS InvoiceDate
	, [InvoiceNumber]
	, [InvoiceAmount]
	, [OrderNumber]
FROM [vwOlapSalesByCustomer]
WHERE ([Year] >= 2010)
ORDER BY [Workshop], [ClientName], [InvoiceNumber]
";

            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);
            pvgOlap.DataSource = ds.Tables[0];
            pvgOlap.DataBind();

            if (pvgOlap.Fields.Count != 0) return;      // 如果個 PivotGrid 已經有料，咁就唔使再 set

            pvgOlap.RetrieveFields();

            #region 砌最初 OLAP 個樣出嚟
            pvgOlap.OptionsPager.RowsPerPage = 15;

            //pvgReceivablesOlap.Fields["OrderedBy"].Area = PivotArea.RowArea;
            pvgOlap.Fields["Workshop"].Area = PivotArea.RowArea;
            pvgOlap.Fields["ClientName"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["PurchaseOrder"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["ProductCode"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["Qty"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["Qty"].ValueStyle.HorizontalAlign = HorizontalAlign.Right;
            //pvgReceivablesOlap.Fields["Unit"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["Unit"].ValueStyle.HorizontalAlign = HorizontalAlign.Center;
            //pvgReceivablesOlap.Fields["Price"].Area = PivotArea.RowArea;
            //pvgReceivablesOlap.Fields["Price"].ValueStyle.HorizontalAlign = HorizontalAlign.Right;

            //pvgReceivablesOlap.Fields["OrderedOn"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Year"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Year"].AreaIndex = 0;
            pvgOlap.Fields["Month"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Month"].AreaIndex = 1;
            pvgOlap.Fields["Day"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Day"].AreaIndex = 2;

            pvgOlap.Fields["InvoiceAmount"].Area = PivotArea.DataArea;

            pvgOlap.Fields["InvoiceDate"].Area = PivotArea.FilterArea;
            pvgOlap.Fields["InvoiceDate"].ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            pvgOlap.Fields["InvoiceDate"].ValueFormat.FormatString = "yyyy-MM-dd";

            //pvgReceivablesOlap.Fields["InvoiceDate"].GroupInterval = PivotGroupInterval.DateYear;

            pvgOlap.CollapseAllColumns();
            pvgOlap.Fields[0].CollapseAll();
            #endregion
        }

        public void ExportToExcel()
        {
            String filename = String.Format("ReceivablesOlap_{0}", DateTime.Now.ToString("yyyyMMddhhmm"));

            DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
            options.ShowGridLines = true;
            options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;

            pvgExporter.ExportXlsxToResponse(filename, options);
        }
    }
}