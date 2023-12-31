using System;
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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.OrderPkPrintQueueVps.
    /// Date Created:   2016-12-16 01:37:19
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class OrderPkPrintQueueVps
    {
        private int key = 0;
        private int orderPkPrintQueueVpsId = 0;
        private int orderHeaderId = 0;
        private int printQueueVpsId = 0;
        private bool checkedPlate;
        private bool checkedCip3;
        private bool checkedBlueprint;
        private bool isReady;
        private bool isReceived;
        private bool isBilled;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private int createdBy = 0;
        private DateTime modifiedOn = DateTime.Parse("1900-1-1");
        private int modifiedBy = 0;
        private bool retired;
        private DateTime retiredOn = DateTime.Parse("1900-1-1");
        private int retiredBy = 0;

        /// <summary>
        /// Initialize an new empty OrderPkPrintQueueVps object.
        /// </summary>
        public OrderPkPrintQueueVps()
        {
        }

        /// <summary>
        /// Initialize a new OrderPkPrintQueueVps object with the given parameters.
        /// </summary>
        public OrderPkPrintQueueVps(int orderPkPrintQueueVpsId, int orderHeaderId, int printQueueVpsId, bool checkedPlate, bool checkedCip3, bool checkedBlueprint, bool isReady, bool isReceived, bool isBilled, DateTime createdOn, int createdBy, DateTime modifiedOn, int modifiedBy, bool retired, DateTime retiredOn, int retiredBy)
        {
            this.orderPkPrintQueueVpsId = orderPkPrintQueueVpsId;
            this.orderHeaderId = orderHeaderId;
            this.printQueueVpsId = printQueueVpsId;
            this.checkedPlate = checkedPlate;
            this.checkedCip3 = checkedCip3;
            this.checkedBlueprint = checkedBlueprint;
            this.isReady = isReady;
            this.isReceived = isReceived;
            this.isBilled = isBilled;
            this.createdOn = createdOn;
            this.createdBy = createdBy;
            this.modifiedOn = modifiedOn;
            this.modifiedBy = modifiedBy;
            this.retired = retired;
            this.retiredOn = retiredOn;
            this.retiredBy = retiredBy;
        }

        /// <summary>
        /// Loads a OrderPkPrintQueueVps object from the database using the given OrderPkPrintQueueVpsId
        /// </summary>
        /// <param name="orderPkPrintQueueVpsId">The primary key value</param>
        /// <returns>A OrderPkPrintQueueVps object</returns>
        public static OrderPkPrintQueueVps Load(int orderPkPrintQueueVpsId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@OrderPkPrintQueueVpsId", orderPkPrintQueueVpsId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spOrderPkPrintQueueVps_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    OrderPkPrintQueueVps result = new OrderPkPrintQueueVps();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a OrderPkPrintQueueVps object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A OrderPkPrintQueueVps object</returns>
        public static OrderPkPrintQueueVps LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spOrderPkPrintQueueVps_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    OrderPkPrintQueueVps result = new OrderPkPrintQueueVps();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of OrderPkPrintQueueVps objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the OrderPkPrintQueueVps objects in the database.</returns>
        public static OrderPkPrintQueueVpsCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spOrderPkPrintQueueVps_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of OrderPkPrintQueueVps objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the OrderPkPrintQueueVps objects in the database ordered by the columns specified.</returns>
        public static OrderPkPrintQueueVpsCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spOrderPkPrintQueueVps_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of OrderPkPrintQueueVps objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the OrderPkPrintQueueVps objects in the database.</returns>
        public static OrderPkPrintQueueVpsCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spOrderPkPrintQueueVps_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of OrderPkPrintQueueVps objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the OrderPkPrintQueueVps objects in the database ordered by the columns specified.</returns>
        public static OrderPkPrintQueueVpsCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spOrderPkPrintQueueVps_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of OrderPkPrintQueueVps objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the OrderPkPrintQueueVps objects in the database.</returns>
        public static OrderPkPrintQueueVpsCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            OrderPkPrintQueueVpsCollection result = new OrderPkPrintQueueVpsCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    OrderPkPrintQueueVps tmp = new OrderPkPrintQueueVps();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a OrderPkPrintQueueVps object from the database.
        /// </summary>
        /// <param name="orderPkPrintQueueVpsId">The primary key value</param>
        public static void Delete(int orderPkPrintQueueVpsId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@OrderPkPrintQueueVpsId", orderPkPrintQueueVpsId) };
            SqlHelper.Default.ExecuteNonQuery("spOrderPkPrintQueueVps_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) orderPkPrintQueueVpsId = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) orderHeaderId = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) printQueueVpsId = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) checkedPlate = reader.GetBoolean(3);
                if (!reader.IsDBNull(4)) checkedCip3 = reader.GetBoolean(4);
                if (!reader.IsDBNull(5)) checkedBlueprint = reader.GetBoolean(5);
                if (!reader.IsDBNull(6)) isReady = reader.GetBoolean(6);
                if (!reader.IsDBNull(7)) isReceived = reader.GetBoolean(7);
                if (!reader.IsDBNull(8)) isBilled = reader.GetBoolean(8);
                if (!reader.IsDBNull(9)) createdOn = reader.GetDateTime(9);
                if (!reader.IsDBNull(10)) createdBy = reader.GetInt32(10);
                if (!reader.IsDBNull(11)) modifiedOn = reader.GetDateTime(11);
                if (!reader.IsDBNull(12)) modifiedBy = reader.GetInt32(12);
                if (!reader.IsDBNull(13)) retired = reader.GetBoolean(13);
                if (!reader.IsDBNull(14)) retiredOn = reader.GetDateTime(14);
                if (!reader.IsDBNull(15)) retiredBy = reader.GetInt32(15);
            }
        }

        public void Delete()
        {
            Delete(this.OrderPkPrintQueueVpsId);
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
                if (key != OrderPkPrintQueueVpsId)
                    this.Delete();
                Update();
            }
        }

        public int OrderPkPrintQueueVpsId
        {
            get { return orderPkPrintQueueVpsId; }
            set { orderPkPrintQueueVpsId = value; }
        }

        public int OrderHeaderId
        {
            get { return orderHeaderId; }
            set { orderHeaderId = value; }
        }

        public int PrintQueueVpsId
        {
            get { return printQueueVpsId; }
            set { printQueueVpsId = value; }
        }

        public bool CheckedPlate
        {
            get { return checkedPlate; }
            set { checkedPlate = value; }
        }

        public bool CheckedCip3
        {
            get { return checkedCip3; }
            set { checkedCip3 = value; }
        }

        public bool CheckedBlueprint
        {
            get { return checkedBlueprint; }
            set { checkedBlueprint = value; }
        }

        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        }

        public bool IsReceived
        {
            get { return isReceived; }
            set { isReceived = value; }
        }

        public bool IsBilled
        {
            get { return isBilled; }
            set { isBilled = value; }
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

        public DateTime ModifiedOn
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }

        public int ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        public bool Retired
        {
            get { return retired; }
            set { retired = value; }
        }

        public DateTime RetiredOn
        {
            get { return retiredOn; }
            set { retiredOn = value; }
        }

        public int RetiredBy
        {
            get { return retiredBy; }
            set { retiredBy = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spOrderPkPrintQueueVps_InsRec", "@OrderPkPrintQueueVpsId", out returnedValue, parameterValues);

            orderPkPrintQueueVpsId = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spOrderPkPrintQueueVps_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[16];
            prams[0] = GetSqlParameter("@OrderPkPrintQueueVpsId", ParameterDirection.Output, SqlDbType.Int, 4, this.OrderPkPrintQueueVpsId);
            prams[1] = GetSqlParameter("@OrderHeaderId", ParameterDirection.Input, SqlDbType.Int, 4, this.OrderHeaderId);
            prams[2] = GetSqlParameter("@PrintQueueVpsId", ParameterDirection.Input, SqlDbType.Int, 4, this.PrintQueueVpsId);
            prams[3] = GetSqlParameter("@CheckedPlate", ParameterDirection.Input, SqlDbType.Bit, 1, this.CheckedPlate);
            prams[4] = GetSqlParameter("@CheckedCip3", ParameterDirection.Input, SqlDbType.Bit, 1, this.CheckedCip3);
            prams[5] = GetSqlParameter("@CheckedBlueprint", ParameterDirection.Input, SqlDbType.Bit, 1, this.CheckedBlueprint);
            prams[6] = GetSqlParameter("@IsReady", ParameterDirection.Input, SqlDbType.Bit, 1, this.IsReady);
            prams[7] = GetSqlParameter("@IsReceived", ParameterDirection.Input, SqlDbType.Bit, 1, this.IsReceived);
            prams[8] = GetSqlParameter("@IsBilled", ParameterDirection.Input, SqlDbType.Bit, 1, this.IsBilled);
            prams[9] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[10] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.Int, 4, this.CreatedBy);
            prams[11] = GetSqlParameter("@ModifiedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.ModifiedOn);
            prams[12] = GetSqlParameter("@ModifiedBy", ParameterDirection.Input, SqlDbType.Int, 4, this.ModifiedBy);
            prams[13] = GetSqlParameter("@Retired", ParameterDirection.Input, SqlDbType.Bit, 1, this.Retired);
            prams[14] = GetSqlParameter("@RetiredOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.RetiredOn);
            prams[15] = GetSqlParameter("@RetiredBy", ParameterDirection.Input, SqlDbType.Int, 4, this.RetiredBy);
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
                GetSqlParameterWithoutDirection("@OrderPkPrintQueueVpsId", SqlDbType.Int, 4, this.OrderPkPrintQueueVpsId),
                GetSqlParameterWithoutDirection("@OrderHeaderId", SqlDbType.Int, 4, this.OrderHeaderId),
                GetSqlParameterWithoutDirection("@PrintQueueVpsId", SqlDbType.Int, 4, this.PrintQueueVpsId),
                GetSqlParameterWithoutDirection("@CheckedPlate", SqlDbType.Bit, 1, this.CheckedPlate),
                GetSqlParameterWithoutDirection("@CheckedCip3", SqlDbType.Bit, 1, this.CheckedCip3),
                GetSqlParameterWithoutDirection("@CheckedBlueprint", SqlDbType.Bit, 1, this.CheckedBlueprint),
                GetSqlParameterWithoutDirection("@IsReady", SqlDbType.Bit, 1, this.IsReady),
                GetSqlParameterWithoutDirection("@IsReceived", SqlDbType.Bit, 1, this.IsReceived),
                GetSqlParameterWithoutDirection("@IsBilled", SqlDbType.Bit, 1, this.IsBilled),
                GetSqlParameterWithoutDirection("@CreatedOn", SqlDbType.DateTime, 8, this.CreatedOn),
                GetSqlParameterWithoutDirection("@CreatedBy", SqlDbType.Int, 4, this.CreatedBy),
                GetSqlParameterWithoutDirection("@ModifiedOn", SqlDbType.DateTime, 8, this.ModifiedOn),
                GetSqlParameterWithoutDirection("@ModifiedBy", SqlDbType.Int, 4, this.ModifiedBy),
                GetSqlParameterWithoutDirection("@Retired", SqlDbType.Bit, 1, this.Retired),
                GetSqlParameterWithoutDirection("@RetiredOn", SqlDbType.DateTime, 8, this.RetiredOn),
                GetSqlParameterWithoutDirection("@RetiredBy", SqlDbType.Int, 4, this.RetiredBy)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("orderPkPrintQueueVpsId: " + orderPkPrintQueueVpsId.ToString()).Append("\r\n");
            builder.Append("orderHeaderId: " + orderHeaderId.ToString()).Append("\r\n");
            builder.Append("printQueueVpsId: " + printQueueVpsId.ToString()).Append("\r\n");
            builder.Append("checkedPlate: " + checkedPlate.ToString()).Append("\r\n");
            builder.Append("checkedCip3: " + checkedCip3.ToString()).Append("\r\n");
            builder.Append("checkedBlueprint: " + checkedBlueprint.ToString()).Append("\r\n");
            builder.Append("isReady: " + isReady.ToString()).Append("\r\n");
            builder.Append("isReceived: " + isReceived.ToString()).Append("\r\n");
            builder.Append("isBilled: " + isBilled.ToString()).Append("\r\n");
            builder.Append("createdOn: " + createdOn.ToString()).Append("\r\n");
            builder.Append("createdBy: " + createdBy.ToString()).Append("\r\n");
            builder.Append("modifiedOn: " + modifiedOn.ToString()).Append("\r\n");
            builder.Append("modifiedBy: " + modifiedBy.ToString()).Append("\r\n");
            builder.Append("retired: " + retired.ToString()).Append("\r\n");
            builder.Append("retiredOn: " + retiredOn.ToString()).Append("\r\n");
            builder.Append("retiredBy: " + retiredBy.ToString()).Append("\r\n");
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

            OrderPkPrintQueueVpsCollection source;

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
                source = OrderPkPrintQueueVps.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = OrderPkPrintQueueVps.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (OrderPkPrintQueueVps item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.OrderHeaderId != 0)
                    {
                        filter = IgnorThis(item, ParentFilter);
                    }
                }
                if (!(filter))
                {
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.OrderPkPrintQueueVpsId));
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


        private static bool IgnorThis(OrderPkPrintQueueVps target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.OrderHeaderId == 0)
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
                OrderPkPrintQueueVps parentTemplate = OrderPkPrintQueueVps.Load(target.OrderHeaderId);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(OrderPkPrintQueueVps target, string[] textField, string textFormatString)
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
    /// Represents a collection of <see cref="OrderPkPrintQueueVps">OrderPkPrintQueueVps</see> objects.
    /// </summary>
    public class OrderPkPrintQueueVpsCollection : BindingList<OrderPkPrintQueueVps>
    {
    }
}
