using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using xFilm5.DAL;

using DevExpress.XtraCharts;

namespace xFilm5.Accounting.Chart
{
    public partial class RevenueByBranch : System.Web.UI.Page
    {
        private List<string> _Branch = new List<string>();
        private List<Int32> _Year = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetBranch();
            SetYear();

            if (!(Page.IsPostBack))
            {
                SetAttribute();
            }
        }

        #region SetAttribute(), SetBranch(), SetYear(), FillFilterWithBranch(), FillFilterWithYear()
        private void SetAttribute()
        {
            FillFilterWithBranch();

            wccRevenue.Series.Clear();
        }

        private void SetBranch()
        {
            string sql = @"
SELECT DISTINCT [Workshop]
FROM [dbo].[vwRevenueByBranch]
WHERE [Year] >= 2010
ORDER BY [Workshop]
";
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);
            while (reader.Read())
            {
                _Branch.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void SetYear()
        {
            string sql = @"
SELECT DISTINCT [Year]
FROM [dbo].[vwRevenueByBranch]
WHERE [Year] >= 2010
ORDER BY [Year]
";
            SqlDataReader reader = SqlHelper.Default.ExecuteReader(CommandType.Text, sql);
            while (reader.Read())
            {
                _Year.Add(reader.GetInt32(0));
            }
            reader.Close();
        }

        private void FillFilterWithBranch()
        {
            cboFilter.Items.Clear();

            foreach (string branch in _Branch)
            {
                cboFilter.Items.Add(branch);
            }
            if (cboFilter.Items.Count > 0)
            {
                cboFilter.SelectedIndex = 0;
            }
        }

        private void FillFilterWithYear()
        {
            cboFilter.Items.Clear();

            foreach (int year in _Year)
            {
                cboFilter.Items.Add(year.ToString());
            }
            if (cboFilter.Items.Count > 0)
            {
                cboFilter.SelectedIndex = 0;
            }
        }
        #endregion

        private void ChartByBranch()
        {
            wccRevenue.Series.Clear();

            foreach (int year in _Year)
            {
                Series series = new Series(year.ToString(), ViewType.Line);
                series.ArgumentDataMember = "Month";
                series.DataFilters.Add("Workshop", "System.String", DataFilterCondition.Equal, cboFilter.SelectedItem.Text);
                series.DataFilters.Add("Year", "System.Int32", DataFilterCondition.Equal, year);
                series.ValueDataMembers.AddRange(new string [] {"Revenue"});
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                series.PointOptions.ValueNumericOptions.Precision = 0;

                //series.Label.BackColor = System.Drawing.Color.Transparent;
                //series.Label.Border.Visible = false;
                PointSeriesLabel label = ((PointSeriesLabel)series.Label);

                wccRevenue.Series.Add(series);
            }
            ChartTitle title = new ChartTitle();
            title.Text = String.Format("{0}", cboFilter.SelectedItem.Text);
            wccRevenue.Titles.Clear();
            wccRevenue.Titles.Add(title);
            ((XYDiagram)wccRevenue.Diagram).AxisY.Title.Text = "Revenue ($)";
        }

        private void ChartByYear()
        {
            wccRevenue.Series.Clear();

            foreach (string branch in _Branch)
            {
                Series series = new Series(branch, ViewType.Line);
                series.ArgumentDataMember = "Month";
                series.DataFilters.Add("Workshop", "System.String", DataFilterCondition.Equal, branch);
                series.DataFilters.Add("Year", "System.Int32", DataFilterCondition.Equal, Convert.ToInt32(cboFilter.SelectedItem.Text));
                series.ValueDataMembers.AddRange(new string[] { "Revenue" });
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                series.PointOptions.ValueNumericOptions.Precision = 0;

                wccRevenue.Series.Add(series);
            }
            ChartTitle title = new ChartTitle();
            title.Text = String.Format("Year {0}", cboFilter.SelectedItem.Text);
            wccRevenue.Titles.Clear();
            wccRevenue.Titles.Add(title);
            ((XYDiagram)wccRevenue.Diagram).AxisY.Title.Text = "Revenue ($)";
        }

        protected void radGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radGroupBy.SelectedItem.Text)
            {
                case "Branch":
                    FillFilterWithBranch();
                    break;
                case "Year":
                    FillFilterWithYear();
                    break;
            }
        }

        protected void cmdRefresh_ButtonClick(object source, DevExpress.Web.ButtonEditClickEventArgs e)
        {
            switch (radGroupBy.SelectedItem.Text)
            {
                case "Branch":
                    ChartByBranch();
                    break;
                case "Year":
                    ChartByYear();
                    break;
            }
            SqlDs.SelectParameters.Clear();
            SqlDs.ConnectionString = DAL.Common.Config.ConnectionString;
            SqlDs.SelectCommand = "SELECT [Workshop], [Year], [Month], [Revenue] FROM [vwRevenueByBranch] WHERE ([Year] >= @Year) ORDER BY [Year], [Month]";
            SqlDs.SelectParameters.Add(new Parameter("Year", DbType.Int32, "2010"));
//            SqlDs.DataBind();
            wccRevenue.DataBind();
        }
    }
}
