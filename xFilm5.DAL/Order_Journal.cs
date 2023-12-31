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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.Order_Journal.
    /// Date Created:   2016-06-25 12:46:18
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class Order_Journal
    {
        private int key = 0;
        private int iD = 0;
        private int orderID = 0;
        private int status = 0;
        private int userID = 0;
        private DateTime dateUpdated = DateTime.Parse("1900-1-1");

        /// <summary>
        /// Initialize an new empty Order_Journal object.
        /// </summary>
        public Order_Journal()
        {
        }

        /// <summary>
        /// Initialize a new Order_Journal object with the given parameters.
        /// </summary>
        public Order_Journal(int iD, int orderID, int status, int userID, DateTime dateUpdated)
        {
            this.iD = iD;
            this.orderID = orderID;
            this.status = status;
            this.userID = userID;
            this.dateUpdated = dateUpdated;
        }

        /// <summary>
        /// Loads a Order_Journal object from the database using the given ID
        /// </summary>
        /// <param name="iD">The primary key value</param>
        /// <returns>A Order_Journal object</returns>
        public static Order_Journal Load(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spOrder_Journal_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    Order_Journal result = new Order_Journal();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a Order_Journal object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A Order_Journal object</returns>
        public static Order_Journal LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spOrder_Journal_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    Order_Journal result = new Order_Journal();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of Order_Journal objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Order_Journal objects in the database.</returns>
        public static Order_JournalCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spOrder_Journal_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Order_Journal objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Order_Journal objects in the database ordered by the columns specified.</returns>
        public static Order_JournalCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spOrder_Journal_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Order_Journal objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Order_Journal objects in the database.</returns>
        public static Order_JournalCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spOrder_Journal_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Order_Journal objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Order_Journal objects in the database ordered by the columns specified.</returns>
        public static Order_JournalCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spOrder_Journal_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Order_Journal objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Order_Journal objects in the database.</returns>
        public static Order_JournalCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            Order_JournalCollection result = new Order_JournalCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    Order_Journal tmp = new Order_Journal();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a Order_Journal object from the database.
        /// </summary>
        /// <param name="iD">The primary key value</param>
        public static void Delete(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            SqlHelper.Default.ExecuteNonQuery("spOrder_Journal_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) iD = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) orderID = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) status = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) userID = reader.GetInt32(3);
                if (!reader.IsDBNull(4)) dateUpdated = reader.GetDateTime(4);
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

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime DateUpdated
        {
            get { return dateUpdated; }
            set { dateUpdated = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spOrder_Journal_InsRec", "@ID", out returnedValue, parameterValues);

            iD = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spOrder_Journal_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[5];
            prams[0] = GetSqlParameter("@ID", ParameterDirection.Output, SqlDbType.Int, 4, this.ID);
            prams[1] = GetSqlParameter("@OrderID", ParameterDirection.Input, SqlDbType.Int, 4, this.OrderID);
            prams[2] = GetSqlParameter("@Status", ParameterDirection.Input, SqlDbType.Int, 4, this.Status);
            prams[3] = GetSqlParameter("@UserID", ParameterDirection.Input, SqlDbType.Int, 4, this.UserID);
            prams[4] = GetSqlParameter("@DateUpdated", ParameterDirection.Input, SqlDbType.DateTime, 8, this.DateUpdated);
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
                GetSqlParameterWithoutDirection("@OrderID", SqlDbType.Int, 4, this.OrderID),
                GetSqlParameterWithoutDirection("@Status", SqlDbType.Int, 4, this.Status),
                GetSqlParameterWithoutDirection("@UserID", SqlDbType.Int, 4, this.UserID),
                GetSqlParameterWithoutDirection("@DateUpdated", SqlDbType.DateTime, 8, this.DateUpdated)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("iD: " + iD.ToString()).Append("\r\n");
            builder.Append("orderID: " + orderID.ToString()).Append("\r\n");
            builder.Append("status: " + status.ToString()).Append("\r\n");
            builder.Append("userID: " + userID.ToString()).Append("\r\n");
            builder.Append("dateUpdated: " + dateUpdated.ToString()).Append("\r\n");
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

            Order_JournalCollection source;

            if (OrderBy == null || OrderBy.Length == 0)
            {
                OrderBy = TextField;
            }

            if (WhereClause.Length > 0)
            {
                source = Order_Journal.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = Order_Journal.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (Order_Journal item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.OrderID != 0)
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


        private static bool IgnorThis(Order_Journal target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.OrderID == 0)
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
                Order_Journal parentTemplate = Order_Journal.Load(target.OrderID);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(Order_Journal target, string[] textField, string textFormatString)
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
    /// Represents a collection of <see cref="Order_Journal">Order_Journal</see> objects.
    /// </summary>
    public class Order_JournalCollection : BindingList<Order_Journal>
    {
    }
}
