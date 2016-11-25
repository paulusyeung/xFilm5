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
using System.Windows.Forms;
using System.Globalization;
using System.Xml;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace xFilm5.DAL4Win
{
    /// <summary>
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.PrintQueue_LifeCycle.
    /// Date Created:   2016-11-25 05:57:27
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class PrintQueue_LifeCycle
    {
        private int key = 0;
        private int lifeCycleId = 0;
        private int printQueueId = 0;
        private int printQueueVpsId = 0;
        private int printQSubitemType = 0;
        private int status = 0;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private int createdBy = 0;

        /// <summary>
        /// Initialize an new empty PrintQueue_LifeCycle object.
        /// </summary>
        public PrintQueue_LifeCycle()
        {
        }

        /// <summary>
        /// Initialize a new PrintQueue_LifeCycle object with the given parameters.
        /// </summary>
        public PrintQueue_LifeCycle(int lifeCycleId, int printQueueId, int printQueueVpsId, int printQSubitemType, int status, DateTime createdOn, int createdBy)
        {
            this.lifeCycleId = lifeCycleId;
            this.printQueueId = printQueueId;
            this.printQueueVpsId = printQueueVpsId;
            this.printQSubitemType = printQSubitemType;
            this.status = status;
            this.createdOn = createdOn;
            this.createdBy = createdBy;
        }

        /// <summary>
        /// Loads a PrintQueue_LifeCycle object from the database using the given LifeCycleId
        /// </summary>
        /// <param name="lifeCycleId">The primary key value</param>
        /// <returns>A PrintQueue_LifeCycle object</returns>
        public static PrintQueue_LifeCycle Load(int lifeCycleId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LifeCycleId", lifeCycleId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPrintQueue_LifeCycle_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    PrintQueue_LifeCycle result = new PrintQueue_LifeCycle();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a PrintQueue_LifeCycle object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A PrintQueue_LifeCycle object</returns>
        public static PrintQueue_LifeCycle LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPrintQueue_LifeCycle_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    PrintQueue_LifeCycle result = new PrintQueue_LifeCycle();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of PrintQueue_LifeCycle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PrintQueue_LifeCycle objects in the database.</returns>
        public static PrintQueue_LifeCycleCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spPrintQueue_LifeCycle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PrintQueue_LifeCycle objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PrintQueue_LifeCycle objects in the database ordered by the columns specified.</returns>
        public static PrintQueue_LifeCycleCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPrintQueue_LifeCycle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PrintQueue_LifeCycle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PrintQueue_LifeCycle objects in the database.</returns>
        public static PrintQueue_LifeCycleCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spPrintQueue_LifeCycle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PrintQueue_LifeCycle objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PrintQueue_LifeCycle objects in the database ordered by the columns specified.</returns>
        public static PrintQueue_LifeCycleCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPrintQueue_LifeCycle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PrintQueue_LifeCycle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PrintQueue_LifeCycle objects in the database.</returns>
        public static PrintQueue_LifeCycleCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            PrintQueue_LifeCycleCollection result = new PrintQueue_LifeCycleCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    PrintQueue_LifeCycle tmp = new PrintQueue_LifeCycle();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a PrintQueue_LifeCycle object from the database.
        /// </summary>
        /// <param name="lifeCycleId">The primary key value</param>
        public static void Delete(int lifeCycleId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LifeCycleId", lifeCycleId) };
            SqlHelper.Default.ExecuteNonQuery("spPrintQueue_LifeCycle_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) lifeCycleId = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) printQueueId = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) printQueueVpsId = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) printQSubitemType = reader.GetInt32(3);
                if (!reader.IsDBNull(4)) status = reader.GetInt32(4);
                if (!reader.IsDBNull(5)) createdOn = reader.GetDateTime(5);
                if (!reader.IsDBNull(6)) createdBy = reader.GetInt32(6);
            }
        }

        public void Delete()
        {
            Delete(this.LifeCycleId);
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
                if (key != LifeCycleId)
                    this.Delete();
                Update();
            }
        }

        public int LifeCycleId
        {
            get { return lifeCycleId; }
            set { lifeCycleId = value; }
        }

        public int PrintQueueId
        {
            get { return printQueueId; }
            set { printQueueId = value; }
        }

        public int PrintQueueVpsId
        {
            get { return printQueueVpsId; }
            set { printQueueVpsId = value; }
        }

        public int PrintQSubitemType
        {
            get { return printQSubitemType; }
            set { printQSubitemType = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spPrintQueue_LifeCycle_InsRec", "@LifeCycleId", out returnedValue, parameterValues);

            lifeCycleId = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spPrintQueue_LifeCycle_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[7];
            prams[0] = GetSqlParameter("@LifeCycleId", ParameterDirection.Output, SqlDbType.Int, 4, this.LifeCycleId);
            prams[1] = GetSqlParameter("@PrintQueueId", ParameterDirection.Input, SqlDbType.Int, 4, this.PrintQueueId);
            prams[2] = GetSqlParameter("@PrintQueueVpsId", ParameterDirection.Input, SqlDbType.Int, 4, this.PrintQueueVpsId);
            prams[3] = GetSqlParameter("@PrintQSubitemType", ParameterDirection.Input, SqlDbType.Int, 4, this.PrintQSubitemType);
            prams[4] = GetSqlParameter("@Status", ParameterDirection.Input, SqlDbType.Int, 4, this.Status);
            prams[5] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[6] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.Int, 4, this.CreatedBy);
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
                GetSqlParameterWithoutDirection("@LifeCycleId", SqlDbType.Int, 4, this.LifeCycleId),
                GetSqlParameterWithoutDirection("@PrintQueueId", SqlDbType.Int, 4, this.PrintQueueId),
                GetSqlParameterWithoutDirection("@PrintQueueVpsId", SqlDbType.Int, 4, this.PrintQueueVpsId),
                GetSqlParameterWithoutDirection("@PrintQSubitemType", SqlDbType.Int, 4, this.PrintQSubitemType),
                GetSqlParameterWithoutDirection("@Status", SqlDbType.Int, 4, this.Status),
                GetSqlParameterWithoutDirection("@CreatedOn", SqlDbType.DateTime, 8, this.CreatedOn),
                GetSqlParameterWithoutDirection("@CreatedBy", SqlDbType.Int, 4, this.CreatedBy)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("lifeCycleId: " + lifeCycleId.ToString()).Append("\r\n");
            builder.Append("printQueueId: " + printQueueId.ToString()).Append("\r\n");
            builder.Append("printQueueVpsId: " + printQueueVpsId.ToString()).Append("\r\n");
            builder.Append("printQSubitemType: " + printQSubitemType.ToString()).Append("\r\n");
            builder.Append("status: " + status.ToString()).Append("\r\n");
            builder.Append("createdOn: " + createdOn.ToString()).Append("\r\n");
            builder.Append("createdBy: " + createdBy.ToString()).Append("\r\n");
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

            PrintQueue_LifeCycleCollection source;

            if (OrderBy == null || OrderBy.Length == 0)
            {
                OrderBy = TextField;
            }

            if (WhereClause.Length > 0)
            {
                source = PrintQueue_LifeCycle.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = PrintQueue_LifeCycle.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (PrintQueue_LifeCycle item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.PrintQueueId != 0)
                    {
                        filter = IgnorThis(item, ParentFilter);
                    }
                }
                if (!(filter))
                {
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.LifeCycleId));
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


        private static bool IgnorThis(PrintQueue_LifeCycle target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.PrintQueueId == 0)
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
                PrintQueue_LifeCycle parentTemplate = PrintQueue_LifeCycle.Load(target.PrintQueueId);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(PrintQueue_LifeCycle target, string[] textField, string textFormatString)
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
            var culture = CultureInfo.CurrentCulture;
            switch (culture.Name)
            {
                case "zh-Hant":
                case "zh-HK":
                case "zh-MO":
                case "zh-TW":
                    source[source.Length - 1] += "_Chs";
                    break;
                case "zh-CN":
                    source[source.Length - 1] += "_Cht";
                    break;
            }
            return source;
        }
    }


    /// <summary>
    /// Represents a collection of <see cref="PrintQueue_LifeCycle">PrintQueue_LifeCycle</see> objects.
    /// </summary>
    public class PrintQueue_LifeCycleCollection : BindingList<PrintQueue_LifeCycle>
    {
    }
}
