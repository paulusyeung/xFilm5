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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.ClientPricing.
    /// Date Created:   2016-12-23 05:17:50
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class ClientPricing
    {
        private int key = 0;
        private int pricingId = 0;
        private int clientId = 0;
        private int itemId = 0;
        private string alias = String.Empty;
        private string tag = String.Empty;
        private decimal unitPrice;
        private decimal discount;
        private int status = 0;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private int createdBy = 0;
        private DateTime modifiedOn = DateTime.Parse("1900-1-1");
        private int modifiedBy = 0;
        private bool retired;
        private DateTime retiredOn = DateTime.Parse("1900-1-1");
        private int retiredBy = 0;

        /// <summary>
        /// Initialize an new empty ClientPricing object.
        /// </summary>
        public ClientPricing()
        {
        }

        /// <summary>
        /// Initialize a new ClientPricing object with the given parameters.
        /// </summary>
        public ClientPricing(int pricingId, int clientId, int itemId, string alias, string tag, decimal unitPrice, decimal discount, int status, DateTime createdOn, int createdBy, DateTime modifiedOn, int modifiedBy, bool retired, DateTime retiredOn, int retiredBy)
        {
            this.pricingId = pricingId;
            this.clientId = clientId;
            this.itemId = itemId;
            this.alias = alias;
            this.tag = tag;
            this.unitPrice = unitPrice;
            this.discount = discount;
            this.status = status;
            this.createdOn = createdOn;
            this.createdBy = createdBy;
            this.modifiedOn = modifiedOn;
            this.modifiedBy = modifiedBy;
            this.retired = retired;
            this.retiredOn = retiredOn;
            this.retiredBy = retiredBy;
        }

        /// <summary>
        /// Loads a ClientPricing object from the database using the given PricingId
        /// </summary>
        /// <param name="pricingId">The primary key value</param>
        /// <returns>A ClientPricing object</returns>
        public static ClientPricing Load(int pricingId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PricingId", pricingId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spClientPricing_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    ClientPricing result = new ClientPricing();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a ClientPricing object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A ClientPricing object</returns>
        public static ClientPricing LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spClientPricing_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    ClientPricing result = new ClientPricing();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of ClientPricing objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ClientPricing objects in the database.</returns>
        public static ClientPricingCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spClientPricing_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ClientPricing objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ClientPricing objects in the database ordered by the columns specified.</returns>
        public static ClientPricingCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spClientPricing_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ClientPricing objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ClientPricing objects in the database.</returns>
        public static ClientPricingCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spClientPricing_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ClientPricing objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ClientPricing objects in the database ordered by the columns specified.</returns>
        public static ClientPricingCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spClientPricing_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ClientPricing objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ClientPricing objects in the database.</returns>
        public static ClientPricingCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            ClientPricingCollection result = new ClientPricingCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    ClientPricing tmp = new ClientPricing();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a ClientPricing object from the database.
        /// </summary>
        /// <param name="pricingId">The primary key value</param>
        public static void Delete(int pricingId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PricingId", pricingId) };
            SqlHelper.Default.ExecuteNonQuery("spClientPricing_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) pricingId = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) clientId = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) itemId = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) alias = reader.GetString(3);
                if (!reader.IsDBNull(4)) tag = reader.GetString(4);
                if (!reader.IsDBNull(5)) unitPrice = reader.GetDecimal(5);
                if (!reader.IsDBNull(6)) discount = reader.GetDecimal(6);
                if (!reader.IsDBNull(7)) status = reader.GetInt32(7);
                if (!reader.IsDBNull(8)) createdOn = reader.GetDateTime(8);
                if (!reader.IsDBNull(9)) createdBy = reader.GetInt32(9);
                if (!reader.IsDBNull(10)) modifiedOn = reader.GetDateTime(10);
                if (!reader.IsDBNull(11)) modifiedBy = reader.GetInt32(11);
                if (!reader.IsDBNull(12)) retired = reader.GetBoolean(12);
                if (!reader.IsDBNull(13)) retiredOn = reader.GetDateTime(13);
                if (!reader.IsDBNull(14)) retiredBy = reader.GetInt32(14);
            }
        }

        public void Delete()
        {
            Delete(this.PricingId);
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
                if (key != PricingId)
                    this.Delete();
                Update();
            }
        }

        public int PricingId
        {
            get { return pricingId; }
            set { pricingId = value; }
        }

        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
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

            SqlHelper.Default.ExecuteNonQuery("spClientPricing_InsRec", "@PricingId", out returnedValue, parameterValues);

            pricingId = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spClientPricing_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[15];
            prams[0] = GetSqlParameter("@PricingId", ParameterDirection.Output, SqlDbType.Int, 4, this.PricingId);
            prams[1] = GetSqlParameter("@ClientId", ParameterDirection.Input, SqlDbType.Int, 4, this.ClientId);
            prams[2] = GetSqlParameter("@ItemId", ParameterDirection.Input, SqlDbType.Int, 4, this.ItemId);
            prams[3] = GetSqlParameter("@Alias", ParameterDirection.Input, SqlDbType.NVarChar, 255, this.Alias);
            prams[4] = GetSqlParameter("@Tag", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.Tag);
            prams[5] = GetSqlParameter("@UnitPrice", ParameterDirection.Input, SqlDbType.Money, 8, this.UnitPrice);
            prams[6] = GetSqlParameter("@Discount", ParameterDirection.Input, SqlDbType.Decimal, 5, this.Discount);
            prams[7] = GetSqlParameter("@Status", ParameterDirection.Input, SqlDbType.Int, 4, this.Status);
            prams[8] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[9] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.Int, 4, this.CreatedBy);
            prams[10] = GetSqlParameter("@ModifiedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.ModifiedOn);
            prams[11] = GetSqlParameter("@ModifiedBy", ParameterDirection.Input, SqlDbType.Int, 4, this.ModifiedBy);
            prams[12] = GetSqlParameter("@Retired", ParameterDirection.Input, SqlDbType.Bit, 1, this.Retired);
            prams[13] = GetSqlParameter("@RetiredOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.RetiredOn);
            prams[14] = GetSqlParameter("@RetiredBy", ParameterDirection.Input, SqlDbType.Int, 4, this.RetiredBy);
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
                GetSqlParameterWithoutDirection("@PricingId", SqlDbType.Int, 4, this.PricingId),
                GetSqlParameterWithoutDirection("@ClientId", SqlDbType.Int, 4, this.ClientId),
                GetSqlParameterWithoutDirection("@ItemId", SqlDbType.Int, 4, this.ItemId),
                GetSqlParameterWithoutDirection("@Alias", SqlDbType.NVarChar, 255, this.Alias),
                GetSqlParameterWithoutDirection("@Tag", SqlDbType.NVarChar, 64, this.Tag),
                GetSqlParameterWithoutDirection("@UnitPrice", SqlDbType.Money, 8, this.UnitPrice),
                GetSqlParameterWithoutDirection("@Discount", SqlDbType.Decimal, 5, this.Discount),
                GetSqlParameterWithoutDirection("@Status", SqlDbType.Int, 4, this.Status),
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
            builder.Append("pricingId: " + pricingId.ToString()).Append("\r\n");
            builder.Append("clientId: " + clientId.ToString()).Append("\r\n");
            builder.Append("itemId: " + itemId.ToString()).Append("\r\n");
            builder.Append("alias: " + alias.ToString()).Append("\r\n");
            builder.Append("tag: " + tag.ToString()).Append("\r\n");
            builder.Append("unitPrice: " + unitPrice.ToString()).Append("\r\n");
            builder.Append("discount: " + discount.ToString()).Append("\r\n");
            builder.Append("status: " + status.ToString()).Append("\r\n");
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

            ClientPricingCollection source;

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
                source = ClientPricing.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = ClientPricing.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (ClientPricing item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.ClientId != 0)
                    {
                        filter = IgnorThis(item, ParentFilter);
                    }
                }
                if (!(filter))
                {
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.PricingId));
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


        private static bool IgnorThis(ClientPricing target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.ClientId == 0)
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
                ClientPricing parentTemplate = ClientPricing.Load(target.ClientId);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(ClientPricing target, string[] textField, string textFormatString)
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
    /// Represents a collection of <see cref="ClientPricing">ClientPricing</see> objects.
    /// </summary>
    public class ClientPricingCollection : BindingList<ClientPricing>
    {
    }
}
