﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using Gizmox.WebGUI.Forms;
using System.Xml;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace xFilm5.DAL
{
    /// <summary>
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.Acct_INDetails.
    /// Date Created:   2016-12-13 03:43:58
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class Acct_INDetails
    {
        private int key = 0;
        private int iD = 0;
        private int iNMasterID = 0;
        private int orderPkPrintQueueVpsId = 0;
        private string billingCode = String.Empty;
        private string description = String.Empty;
        private short qty;
        private decimal unitAmount;
        private decimal discount;
        private decimal amount;

        /// <summary>
        /// Initialize an new empty Acct_INDetails object.
        /// </summary>
        public Acct_INDetails()
        {
        }

        /// <summary>
        /// Initialize a new Acct_INDetails object with the given parameters.
        /// </summary>
        public Acct_INDetails(int iD, int iNMasterID, int orderPkPrintQueueVpsId, string billingCode, string description, short qty, decimal unitAmount, decimal discount, decimal amount)
        {
            this.iD = iD;
            this.iNMasterID = iNMasterID;
            this.orderPkPrintQueueVpsId = orderPkPrintQueueVpsId;
            this.billingCode = billingCode;
            this.description = description;
            this.qty = qty;
            this.unitAmount = unitAmount;
            this.discount = discount;
            this.amount = amount;
        }

        /// <summary>
        /// Loads a Acct_INDetails object from the database using the given ID
        /// </summary>
        /// <param name="iD">The primary key value</param>
        /// <returns>A Acct_INDetails object</returns>
        public static Acct_INDetails Load(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spAcct_INDetails_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    Acct_INDetails result = new Acct_INDetails();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a Acct_INDetails object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A Acct_INDetails object</returns>
        public static Acct_INDetails LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spAcct_INDetails_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    Acct_INDetails result = new Acct_INDetails();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of Acct_INDetails objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Acct_INDetails objects in the database.</returns>
        public static Acct_INDetailsCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spAcct_INDetails_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Acct_INDetails objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Acct_INDetails objects in the database ordered by the columns specified.</returns>
        public static Acct_INDetailsCollection LoadCollection(string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length - 1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spAcct_INDetails_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Acct_INDetails objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Acct_INDetails objects in the database.</returns>
        public static Acct_INDetailsCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spAcct_INDetails_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Acct_INDetails objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Acct_INDetails objects in the database ordered by the columns specified.</returns>
        public static Acct_INDetailsCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length - 1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause), new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spAcct_INDetails_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Acct_INDetails objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Acct_INDetails objects in the database.</returns>
        public static Acct_INDetailsCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            Acct_INDetailsCollection result = new Acct_INDetailsCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    Acct_INDetails tmp = new Acct_INDetails();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a Acct_INDetails object from the database.
        /// </summary>
        /// <param name="iD">The primary key value</param>
        public static void Delete(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            SqlHelper.Default.ExecuteNonQuery("spAcct_INDetails_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) iD = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) iNMasterID = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) orderPkPrintQueueVpsId = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) billingCode = reader.GetString(3);
                if (!reader.IsDBNull(4)) description = reader.GetString(4);
                if (!reader.IsDBNull(5)) qty = reader.GetInt16(5);
                if (!reader.IsDBNull(6)) unitAmount = reader.GetDecimal(6);
                if (!reader.IsDBNull(7)) discount = reader.GetDecimal(7);
                if (!reader.IsDBNull(8)) amount = reader.GetDecimal(8);
            }
        }

        public void Delete()
        {
            Delete(this.ID);
        }

        public void Save()
        {
            //  We use the key field which will have its default value unless it is set by Load(). When we save we can know if
            //  we need to do an insert (key == null) an update (key == primaryKey) or a 
            //  delete+update (key != null && key != primaryKey)

            if (key == 0)
                Insert();
            else
            {
                if (key != ID)
                    this.Delete();
                Update();
            }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public int INMasterID
        {
            get { return iNMasterID; }
            set { iNMasterID = value; }
        }

        public int OrderPkPrintQueueVpsId
        {
            get { return orderPkPrintQueueVpsId; }
            set { orderPkPrintQueueVpsId = value; }
        }

        public string BillingCode
        {
            get { return billingCode; }
            set { billingCode = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public short Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public decimal UnitAmount
        {
            get { return unitAmount; }
            set { unitAmount = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spAcct_INDetails_InsRec", "@ID", out returnedValue, parameterValues);

            iD = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spAcct_INDetails_UpdRec", parameterValues);
        }

        /// <summary>
        /// Gets the SQL parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetSqlParameter(string name, ParameterDirection direction, SqlDbType dbType, int size, object value)
        {
            SqlParameter p = new SqlParameter(name, dbType, size);
            p.Value = value;
            p.Direction = direction;
            return p;
        }

        private SqlParameter[] GetInsertParameterValues()
        {
            SqlParameter[] prams = new SqlParameter[9];
            prams[0] = GetSqlParameter("@ID", ParameterDirection.Output, SqlDbType.Int, 4, this.ID);
            prams[1] = GetSqlParameter("@INMasterID", ParameterDirection.Input, SqlDbType.Int, 4, this.INMasterID);
            prams[2] = GetSqlParameter("@OrderPkPrintQueueVpsId", ParameterDirection.Input, SqlDbType.Int, 4, this.OrderPkPrintQueueVpsId);
            prams[3] = GetSqlParameter("@BillingCode", ParameterDirection.Input, SqlDbType.NVarChar, 16, this.BillingCode);
            prams[4] = GetSqlParameter("@Description", ParameterDirection.Input, SqlDbType.NVarChar, 255, this.Description);
            prams[5] = GetSqlParameter("@Qty", ParameterDirection.Input, SqlDbType.SmallInt, 2, this.Qty);
            prams[6] = GetSqlParameter("@UnitAmount", ParameterDirection.Input, SqlDbType.Money, 8, this.UnitAmount);
            prams[7] = GetSqlParameter("@Discount", ParameterDirection.Input, SqlDbType.Decimal, 5, this.Discount);
            prams[8] = GetSqlParameter("@Amount", ParameterDirection.Input, SqlDbType.Money, 8, this.Amount);
            return prams;
        }

        /// <summary>
        /// Gets the SQL parameter without direction.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetSqlParameterWithoutDirection(string name, SqlDbType dbType, int size, object value)
        {
            SqlParameter p = new SqlParameter(name, dbType, size);
            p.Value = value;
            return p;
        }

        private SqlParameter[] GetUpdateParameterValues()
        {
            return new SqlParameter[]
            {
                GetSqlParameterWithoutDirection("@ID", SqlDbType.Int, 4, this.ID),
                GetSqlParameterWithoutDirection("@INMasterID", SqlDbType.Int, 4, this.INMasterID),
                GetSqlParameterWithoutDirection("@OrderPkPrintQueueVpsId", SqlDbType.Int, 4, this.OrderPkPrintQueueVpsId),
                GetSqlParameterWithoutDirection("@BillingCode", SqlDbType.NVarChar, 16, this.BillingCode),
                GetSqlParameterWithoutDirection("@Description", SqlDbType.NVarChar, 255, this.Description),
                GetSqlParameterWithoutDirection("@Qty", SqlDbType.SmallInt, 2, this.Qty),
                GetSqlParameterWithoutDirection("@UnitAmount", SqlDbType.Money, 8, this.UnitAmount),
                GetSqlParameterWithoutDirection("@Discount", SqlDbType.Decimal, 5, this.Discount),
                GetSqlParameterWithoutDirection("@Amount", SqlDbType.Money, 8, this.Amount)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("iD: " + iD.ToString()).Append("\r\n");
            builder.Append("iNMasterID: " + iNMasterID.ToString()).Append("\r\n");
            builder.Append("orderPkPrintQueueVpsId: " + orderPkPrintQueueVpsId.ToString()).Append("\r\n");
            builder.Append("billingCode: " + billingCode.ToString()).Append("\r\n");
            builder.Append("description: " + description.ToString()).Append("\r\n");
            builder.Append("qty: " + qty.ToString()).Append("\r\n");
            builder.Append("unitAmount: " + unitAmount.ToString()).Append("\r\n");
            builder.Append("discount: " + discount.ToString()).Append("\r\n");
            builder.Append("amount: " + amount.ToString()).Append("\r\n");
            builder.Append("\r\n");
            return builder.ToString();
        }

        #region Load ComboBox
        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale)
        {
            LoadCombo(ref ddList, TextField, SwitchLocale, false, string.Empty, string.Empty, new string[] { TextField });
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
        public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, string[] OrderBy)
        {
            LoadCombo(ref ddList, TextField, SwitchLocale, false, string.Empty, string.Empty, OrderBy);
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause)
        {
            LoadCombo(ref ddList, TextField, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, new String[] { TextField });
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause, string[] OrderBy)
        {
            LoadCombo(ref ddList, TextField, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, OrderBy);
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="ParentFilter">e.g. "ForeignFieldName = 'value'"</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string ParentFilter, string WhereClause, string[] OrderBy)
        {
            string[] textField = { TextField };
            LoadCombo(ref ddList, textField, "{0}", SwitchLocale, BlankLine, BlankLineText, ParentFilter, WhereClause, OrderBy);
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. new string[]{"FieldName1", "FieldName2", ...}</param>
        /// <param name="TextFormatString">e.g. "{0} - {1}"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string[] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause, string[] OrderBy)
        {
            LoadCombo(ref ddList, TextField, TextFormatString, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, OrderBy);
        }

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. new string[]{"FieldName1", "FieldName2", ...}</param>
        /// <param name="TextFormatString">e.g. "{0} - {1}"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="ParentFilter">e.g. "ForeignFieldName = 'value'"</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
        public static void LoadCombo(ref ComboBox ddList, string[] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string ParentFilter, string WhereClause, string[] OrderBy)
        {
            if (SwitchLocale)
            {
                TextField = GetSwitchLocale(TextField);
            }
            ddList.Items.Clear();

            Acct_INDetailsCollection source;

            if (OrderBy == null || OrderBy.Length == 0)
            {
                OrderBy = TextField;
            }

            if (WhereClause.Length > 0)
            {
                source = Acct_INDetails.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = Acct_INDetails.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (Acct_INDetails item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.INMasterID != 0)
                    {
                        filter = IgnorThis(item, ParentFilter);
                    }
                }
                if (!(filter))
                {
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.ID));
                }
            }

            ddList.DataSource = sourceList;
            ddList.DisplayMember = "Code";
            ddList.ValueMember = "Id";

            if (ddList.Items.Count > 0)
            {
                ddList.SelectedIndex = 0;
            }
        }

        #endregion


        private static bool IgnorThis(Acct_INDetails target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.INMasterID == 0)
            {
                PropertyInfo pi = target.GetType().GetProperty(parsed[0]);
                string filterField = (string)pi.GetValue(target, null);
                if (filterField.ToLower() == parsed[1].ToLower())
                {
                    result = false;
                }
            }
            else
            {
                Acct_INDetails parentTemplate = Acct_INDetails.Load(target.INMasterID);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(Acct_INDetails target, string[] textField, string textFormatString)
        {
            for (int i = 0; i < textField.Length; i++)
            {
                PropertyInfo pi = target.GetType().GetProperty(textField[i]);
                textFormatString = textFormatString.Replace("{" + i.ToString() + "}", pi != null ? pi.GetValue(target, null).ToString() : string.Empty);
            }
            return textFormatString;
        }

        private static string[] GetSwitchLocale(string[] source)
        {
            switch (Common.Config.CurrentLanguageId)
            {
                case 2:
                    source[source.Length - 1] += "_Chs";
                    break;
                case 3:
                    source[source.Length - 1] += "_Cht";
                    break;
            }
            return source;
        }
    }


    /// <summary>
    /// Represents a collection of <see cref="Acct_INDetails">Acct_INDetails</see> objects.
    /// </summary>
    public class Acct_INDetailsCollection : BindingList<Acct_INDetails>
    {
    }
}
