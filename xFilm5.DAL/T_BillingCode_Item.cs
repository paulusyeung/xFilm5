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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.T_BillingCode_Item.
    /// Date Created:   2016-06-25 12:50:13
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class T_BillingCode_Item
    {
        private int key = 0;
        private int iD = 0;
        private int deptID = 0;
        private string itemCode = String.Empty;
        private string groupCode = String.Empty;
        private string name = String.Empty;
        private string uoM = String.Empty;
        private decimal unitPrice;
        private bool retired;

        /// <summary>
        /// Initialize an new empty T_BillingCode_Item object.
        /// </summary>
        public T_BillingCode_Item()
        {
        }

        /// <summary>
        /// Initialize a new T_BillingCode_Item object with the given parameters.
        /// </summary>
        public T_BillingCode_Item(int iD, int deptID, string itemCode, string groupCode, string name, string uoM, decimal unitPrice, bool retired)
        {
            this.iD = iD;
            this.deptID = deptID;
            this.itemCode = itemCode;
            this.groupCode = groupCode;
            this.name = name;
            this.uoM = uoM;
            this.unitPrice = unitPrice;
            this.retired = retired;
        }

        /// <summary>
        /// Loads a T_BillingCode_Item object from the database using the given ID
        /// </summary>
        /// <param name="iD">The primary key value</param>
        /// <returns>A T_BillingCode_Item object</returns>
        public static T_BillingCode_Item Load(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spT_BillingCode_Item_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    T_BillingCode_Item result = new T_BillingCode_Item();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a T_BillingCode_Item object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A T_BillingCode_Item object</returns>
        public static T_BillingCode_Item LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spT_BillingCode_Item_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    T_BillingCode_Item result = new T_BillingCode_Item();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of T_BillingCode_Item objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the T_BillingCode_Item objects in the database.</returns>
        public static T_BillingCode_ItemCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spT_BillingCode_Item_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of T_BillingCode_Item objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the T_BillingCode_Item objects in the database ordered by the columns specified.</returns>
        public static T_BillingCode_ItemCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spT_BillingCode_Item_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of T_BillingCode_Item objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the T_BillingCode_Item objects in the database.</returns>
        public static T_BillingCode_ItemCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spT_BillingCode_Item_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of T_BillingCode_Item objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the T_BillingCode_Item objects in the database ordered by the columns specified.</returns>
        public static T_BillingCode_ItemCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spT_BillingCode_Item_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of T_BillingCode_Item objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the T_BillingCode_Item objects in the database.</returns>
        public static T_BillingCode_ItemCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            T_BillingCode_ItemCollection result = new T_BillingCode_ItemCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    T_BillingCode_Item tmp = new T_BillingCode_Item();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a T_BillingCode_Item object from the database.
        /// </summary>
        /// <param name="iD">The primary key value</param>
        public static void Delete(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            SqlHelper.Default.ExecuteNonQuery("spT_BillingCode_Item_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) iD = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) deptID = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) itemCode = reader.GetString(2);
                if (!reader.IsDBNull(3)) groupCode = reader.GetString(3);
                if (!reader.IsDBNull(4)) name = reader.GetString(4);
                if (!reader.IsDBNull(5)) uoM = reader.GetString(5);
                if (!reader.IsDBNull(6)) unitPrice = reader.GetDecimal(6);
                if (!reader.IsDBNull(7)) retired = reader.GetBoolean(7);
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

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public string GroupCode
        {
            get { return groupCode; }
            set { groupCode = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string UoM
        {
            get { return uoM; }
            set { uoM = value; }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public bool Retired
        {
            get { return retired; }
            set { retired = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spT_BillingCode_Item_InsRec", "@ID", out returnedValue, parameterValues);

            iD = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spT_BillingCode_Item_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[8];
            prams[0] = GetSqlParameter("@ID", ParameterDirection.Output, SqlDbType.Int, 4, this.ID);
            prams[1] = GetSqlParameter("@DeptID", ParameterDirection.Input, SqlDbType.Int, 4, this.DeptID);
            prams[2] = GetSqlParameter("@ItemCode", ParameterDirection.Input, SqlDbType.NVarChar, 16, this.ItemCode);
            prams[3] = GetSqlParameter("@GroupCode", ParameterDirection.Input, SqlDbType.NVarChar, 16, this.GroupCode);
            prams[4] = GetSqlParameter("@Name", ParameterDirection.Input, SqlDbType.NVarChar, 128, this.Name);
            prams[5] = GetSqlParameter("@UoM", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.UoM);
            prams[6] = GetSqlParameter("@UnitPrice", ParameterDirection.Input, SqlDbType.Money, 8, this.UnitPrice);
            prams[7] = GetSqlParameter("@Retired", ParameterDirection.Input, SqlDbType.Bit, 1, this.Retired);
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
                GetSqlParameterWithoutDirection("@DeptID", SqlDbType.Int, 4, this.DeptID),
                GetSqlParameterWithoutDirection("@ItemCode", SqlDbType.NVarChar, 16, this.ItemCode),
                GetSqlParameterWithoutDirection("@GroupCode", SqlDbType.NVarChar, 16, this.GroupCode),
                GetSqlParameterWithoutDirection("@Name", SqlDbType.NVarChar, 128, this.Name),
                GetSqlParameterWithoutDirection("@UoM", SqlDbType.NVarChar, 32, this.UoM),
                GetSqlParameterWithoutDirection("@UnitPrice", SqlDbType.Money, 8, this.UnitPrice),
                GetSqlParameterWithoutDirection("@Retired", SqlDbType.Bit, 1, this.Retired)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("iD: " + iD.ToString()).Append("\r\n");
            builder.Append("deptID: " + deptID.ToString()).Append("\r\n");
            builder.Append("itemCode: " + itemCode.ToString()).Append("\r\n");
            builder.Append("groupCode: " + groupCode.ToString()).Append("\r\n");
            builder.Append("name: " + name.ToString()).Append("\r\n");
            builder.Append("uoM: " + uoM.ToString()).Append("\r\n");
            builder.Append("unitPrice: " + unitPrice.ToString()).Append("\r\n");
            builder.Append("retired: " + retired.ToString()).Append("\r\n");
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

            T_BillingCode_ItemCollection source;

            if (OrderBy == null || OrderBy.Length == 0)
            {
                OrderBy = TextField;
            }
            // Filter the retired records
            if (WhereClause.Length > 0)
            {
                WhereClause += " AND Retired = 0";
            }
            else
            {
                WhereClause = "Retired = 0";
            }

            if (WhereClause.Length > 0)
            {
                source = T_BillingCode_Item.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = T_BillingCode_Item.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (T_BillingCode_Item item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.DeptID != 0)
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


        private static bool IgnorThis(T_BillingCode_Item target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.DeptID == 0)
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
                T_BillingCode_Item parentTemplate = T_BillingCode_Item.Load(target.DeptID);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(T_BillingCode_Item target, string[] textField, string textFormatString)
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
    /// Represents a collection of <see cref="T_BillingCode_Item">T_BillingCode_Item</see> objects.
    /// </summary>
    public class T_BillingCode_ItemCollection : BindingList<T_BillingCode_Item>
    {
    }
}
