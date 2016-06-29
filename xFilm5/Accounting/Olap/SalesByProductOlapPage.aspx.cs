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
    public partial class SalesByProductOlapPage : Gizmox.WebGUI.Forms.Hosts.AspPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 砌個 Toolbar - Export To Excel
            this.ansOlap.Items.Clear();
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem();
            item.Image.Url = "~//Resources//Icons//16x16//16_Excel.gif";
            item.Text = PageContext.Text;       //攞 RtfStats.cs 提供嘅名稱（有中英亙捩）

            this.ansOlap.Items.Add(item);
            this.ansOlap.ItemClick += new DevExpress.Web.MenuItemEventHandler(ansOlap_ItemClick);
            #endregion

            pvgExporter.ASPxPivotGridID = pvgOlap.ID;

            //pvgOlap.CustomCellStyle += new PivotCustomCellStyleEventHandler(pvgOlap_CustomCellStyle);

            BindData();
        }

        void pvgOlap_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
        {
            if (e.ColumnValueType == PivotGridValueType.GrandTotal || e.ColumnValueType == PivotGridValueType.Total)
            {
                e.CellStyle.Font.Bold = true;
            }

            if (e.ColumnValueType != PivotGridValueType.Value || e.RowValueType != PivotGridValueType.Value)
                return;

        }

        void ansOlap_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            ExportToExcel();
        }

        private void BindData()
        {
            String sql = @"

SELECT [Workshop]
	, [Product]
	, [Year]
	, [Month]
	, [Day]
	, CONVERT(NVARCHAR(10), [InvoiceDate], 120) AS InvoiceDate
	, [InvoiceNumber]
	, [OrderNumber]
	, [Qty]
	, [Amount]
FROM [vwOlapSalesByProduct]
WHERE ([Year] >= 2010)
ORDER BY [Workshop], [Product], [InvoiceDate]
";

            DataSet ds = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql);
            pvgOlap.DataSource = ds.Tables[0];
            pvgOlap.DataBind();

            if (pvgOlap.Fields.Count != 0) return;      // 如果個 PivotGrid 已經有料，咁就唔使再 set

            pvgOlap.RetrieveFields();

            #region 砌最初 OLAP 個樣出嚟
            pvgOlap.OptionsPager.RowsPerPage = 15;

            //pvgOlap.Fields["OrderedBy"].Area = PivotArea.RowArea;
            pvgOlap.Fields["Workshop"].Area = PivotArea.RowArea;
            pvgOlap.Fields["Product"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["PurchaseOrder"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["ProductCode"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["Qty"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["Qty"].ValueStyle.HorizontalAlign = HorizontalAlign.Right;
            //pvgOlap.Fields["Unit"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["Unit"].ValueStyle.HorizontalAlign = HorizontalAlign.Center;
            //pvgOlap.Fields["Price"].Area = PivotArea.RowArea;
            //pvgOlap.Fields["Price"].ValueStyle.HorizontalAlign = HorizontalAlign.Right;

            //pvgOlap.Fields["OrderedOn"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Year"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Year"].AreaIndex = 0;
            pvgOlap.Fields["Month"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Month"].AreaIndex = 1;
            pvgOlap.Fields["Day"].Area = PivotArea.ColumnArea;
            pvgOlap.Fields["Day"].AreaIndex = 2;

            pvgOlap.Fields["Qty"].Area = PivotArea.DataArea;
            pvgOlap.Fields["Amount"].Area = PivotArea.DataArea;

            pvgOlap.Fields["InvoiceDate"].Area = PivotArea.FilterArea;
            pvgOlap.Fields["InvoiceDate"].ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            pvgOlap.Fields["InvoiceDate"].ValueFormat.FormatString = "yyyy-MM-dd";

            //pvgOlap.Fields["InvoiceDate"].GroupInterval = PivotGroupInterval.DateYear;

            pvgOlap.CollapseAllColumns();           //哩個唔 work
            pvgOlap.Fields[0].CollapseAll();        //改用哩個
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