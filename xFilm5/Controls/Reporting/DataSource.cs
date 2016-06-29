using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

using xFilm5.DAL;

namespace xFilm5.Controls.Reporting
{
    public class DataSource
    {
        public static DataTable Invoice(int invoiceId)
        {
            string sql = @"
SELECT  TOP (100) PERCENT
		[InvoiceId]
        ,[InvoiceNumber]
        ,[InvoiceDate]
        ,[InvoiceAmount]
        ,[OrderID]
        ,ISNULL([PaymentType], 0)
        ,ISNULL([PaidBy], '')
        ,[Remarks]
        ,[Status]
        ,[ClientName]
        ,[OrderedBy]
        ,[ReceivedOn]
        ,[CompletedOn]
        ,[INDetailsId]
        ,[ItemCode]
        ,[ItemDescription]
        ,[ItemQty]
        ,[ItemUoM]
        ,[ItemUnitAmt]
        ,[ItemDiscount]
        ,[ItemAmount]
FROM    [dbo].[vwInvoiceDetails]
WHERE	[InvoiceId] = " + invoiceId.ToString() + @"
ORDER BY [INDetailsId];
";
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }

        public static DataTable Statement(int clientId)
        {
            string sql = @"
DECLARE @CurMonth int
SET @CurMonth = YEAR(GETDATE()) * 12 + MONTH(GETDATE())
SELECT  TOP (100) PERCENT
	    [ClientID]
	   ,CASE (@CurMonth - (YEAR([InvoiceDate]) * 12 + MONTH([InvoiceDate])))
			WHEN 0 THEN 0
			WHEN 1 THEN 1
			WHEN 2 THEN 2
			ELSE 3
	    END AS Aging
       ,[InvoiceID]
       ,[InvoiceNumber]
       ,[InvoiceDate]
       ,[OsAmount]
       ,[InvoiceAmount]
       ,[Remarks]
       ,[OrderID]
FROM    [dbo].[vwInvoiceList_OS]
WHERE	[ClientId] = " + clientId.ToString() + @"
ORDER BY [InvoiceDate];
";
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }

        public static DataTable PaymentNotice(List<int> invoices)
        {
            StringBuilder criteria = new StringBuilder("(");
            foreach (int invoice in invoices)
            {
                criteria.Append(invoice.ToString() + ",");
            }
            criteria.Remove((criteria.Length - 1), 1);          // 把最後的，拿掉
            criteria.Append(")");

            string sql = @"
DECLARE @CurMonth int
SET @CurMonth = YEAR(GETDATE()) * 12 + MONTH(GETDATE())
SELECT  TOP (100) PERCENT
	    [ClientID]
	   ,CASE (@CurMonth - (YEAR([InvoiceDate]) * 12 + MONTH([InvoiceDate])))
			WHEN 0 THEN 0
			WHEN 1 THEN 1
			WHEN 2 THEN 2
			ELSE 3
	    END AS Aging
       ,[InvoiceID]
       ,[InvoiceNumber]
       ,[InvoiceDate]
       ,[OsAmount]
       ,[InvoiceAmount]
       ,[Remarks]
       ,[OrderID]
FROM    [dbo].[vwInvoiceList_OS]
WHERE	[InvoiceNumber] IN " + criteria.ToString() + @"
ORDER BY [InvoiceDate];
";
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }
    }
}
