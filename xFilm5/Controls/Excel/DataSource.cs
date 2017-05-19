using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

using xFilm5.DAL;

namespace xFilm5.Controls.Excel
{
    public class DataSource
    {
        public static DataTable Receipts(int ClientId, int year, int month)
        {
            string sql = String.Format(@"
SELECT CASE WHEN Ln = 1 THEN CAST([idx] AS VARCHAR(10)) ELSE '' END AS idx
      ,CASE WHEN Ln = 1 THEN CAST([ClientId] AS VARCHAR(10)) ELSE '' END AS ClientId
      ,CASE WHEN Ln = 1 THEN CAST([ClientName] AS VARCHAR(10)) ELSE '' END AS ClientName
--      ,[ClientAddress]
--      ,[ClientTel]
--      ,[ClientFax]
--      ,[ReceiptHeaderId]
      ,CASE WHEN Ln = 1 THEN CAST([ReceiptNumber] AS VARCHAR(10)) ELSE '' END AS ReceiptNumber
      ,CASE WHEN Ln = 1 THEN CONVERT(VARCHAR(10), [ReceiptDate], 120) ELSE '' END AS ReceiptDate
      ,CASE WHEN Ln = 1 THEN [ReceiptAmount] ELSE 0 END AS ReceiptAmount
      ,CASE WHEN Ln = 1 THEN ISNULL(CAST([OrderedClientUserId] AS VARCHAR(10)), '') ELSE '' END AS OrderedByUserId
      ,CASE WHEN Ln = 1 THEN ISNULL([OrderedClientUserName], '') ELSE '' END AS OrderedByUserName
      ,CASE WHEN Ln = 1 THEN (CASE WHEN [Paid] = 1 THEN 'Yes' ELSE 'No' END) ELSE '' END AS Paid
      ,CASE WHEN Ln = 1 THEN [PaymentType] ELSE '' END AS PaymentType
--      ,[INMasterId]
      ,CASE WHEN Ln = 1 THEN (CASE WHEN YEAR([PaidOn]) = 1900 THEN '' ELSE CONVERT(VARCHAR(10), [PaidOn], 120) END) ELSE '' END AS PaidOn
      ,CASE WHEN Ln = 1 THEN [PaidAmount] ELSE 0 END AS PaidAmount
      ,CASE WHEN Ln = 1 THEN [PaidRef] ELSE '' END AS PaidRef
      ,CASE WHEN Ln = 1 THEN [Remarks] ELSE '' END AS Remarks
      ,CASE WHEN Ln = 1 THEN CAST([Status] AS VARCHAR(10)) ELSE '' END AS Status
      ,CASE WHEN Ln = 1 THEN CONVERT(VARCHAR(19), [CreatedOn], 120) ELSE '' END AS CreatedOn
      ,CASE WHEN Ln = 1 THEN [CreatedBy] ELSE '' END AS CreatedBy
      ,CASE WHEN Ln = 1 THEN CONVERT(VARCHAR(19), [ModifiedOn], 120) ELSE '' END AS ModifiedOn
      ,CASE WHEN Ln = 1 THEN [ModifiedBy] ELSE '' END AS ModifiedBy
	  ,ROW_NUMBER() OVER (PARTITION BY [ReceiptHeaderId] ORDER By [ReceiptHeaderId], [ItemDescription]) AS Ln
--      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
--      ,[OrderPkPrintQueueVpsId]
--      ,[OrderHeaderId]
FROM
(
SELECT DENSE_RANK() OVER(Order By [ReceiptHeaderId]) AS idx
      ,[ClientId]
      ,[ClientName]
      ,[ClientAddress]
      ,[ClientTel]
      ,[ClientFax]
      ,[ReceiptHeaderId]
      ,[ReceiptNumber]
      ,[ReceiptDate]
      ,[ReceiptAmount]
      ,[PaymentType]
      ,[INMasterId]
      ,[OrderedClientUserId]
      ,[OrderedClientUserName]
      ,[Paid]
      ,[PaidOn]
      ,[PaidAmount]
      ,[PaidRef]
      ,[Remarks]
      ,[Status]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
	  ,ROW_NUMBER() OVER (PARTITION BY [ReceiptHeaderId] ORDER By [ReceiptHeaderId], [ItemDescription]) AS Ln
      ,[ReceiptDetailId]
      ,[ItemCode]
      ,[ItemDescription]
      ,[ItemQty]
      ,[ItemUoM]
      ,[ItemUnitAmt]
      ,[ItemDiscount]
      ,[ItemAmount]
      ,[OrderPkPrintQueueVpsId]
      ,[OrderHeaderId]
FROM [dbo].[vwReceiptDetailsList_Ex]
WHERE ([ClientId] = {0}) AND (YEAR([ReceiptDate]) = {1}) AND (MONTH([ReceiptDate]) = {2})
) AS s
ORDER By [ReceiptHeaderId], [ItemDescription]
", ClientId.ToString(), year.ToString(), month.ToString());
            DataSet dataset = new DataSet();
            using (dataset = SqlHelper.Default.ExecuteDataSet(CommandType.Text, sql))
            {
                return dataset.Tables[0];
            }
        }
    }
}
