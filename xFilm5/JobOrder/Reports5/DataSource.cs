using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using xFilm5.DAL;

namespace xFilm5.JobOrder.Reports5
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

        public static DataTable Inv5(int invoiceId)
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
FROM    [dbo].[vwInv5DetailsList]
WHERE	[InvoiceId] = " + invoiceId.ToString() + @"
ORDER BY [INDetailsId];
";
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }

        public static DataTable DN(int receiptId)
        {
            string sql = @"
SELECT  TOP (100) PERCENT
		 [ReceiptHeaderId]              --[InvoiceId]
        ,[ReceiptNumber]                --[InvoiceNumber]
        ,[ReceiptDate]                  --[InvoiceDate]
        ,[ReceiptAmount]                --[InvoiceAmount]
        ,[OrderHeaderId]                --[OrderID]
        ,ISNULL([PaymentType], 0)
        ,ISNULL([ClientUserName], '')   --ISNULL([PaidBy], '')
        ,[Remarks]
        ,[Status]
        ,[ClientName]
        --,[OrderedBy]
        --,[ReceivedOn]
        --,[CompletedOn]
        ,[OrderPkPrintQueueVpsId]       --[INDetailsId]
        ,[ItemCode]
        ,[ItemDescription]
        ,[ItemQty]
        ,[ItemUoM]
        ,[ItemUnitAmt]
        ,[ItemDiscount]
        ,[ItemAmount]
FROM    [dbo].[vwReceiptDetailsList]
WHERE	[ReceiptHeaderId] = " + receiptId.ToString() + @"
ORDER BY [OrderPkPrintQueueVpsId];
";
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }
    }
}