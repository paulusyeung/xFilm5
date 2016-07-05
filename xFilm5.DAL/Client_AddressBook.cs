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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.Client_AddressBook.
    /// Date Created:   2016-06-25 12:43:10
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class Client_AddressBook
    {
        private int key = 0;
        private int iD = 0;
        private int clientID = 0;
        private bool primaryAddr;
        private string name = String.Empty;
        private string address = String.Empty;
        private string tel = String.Empty;
        private string fax = String.Empty;
        private string contactPerson = String.Empty;
        private string mobile = String.Empty;
        private string pager = String.Empty;
        private string sMS = String.Empty;
        private int sMS_Lang = 0;
        private DateTime createdOn = DateTime.Parse("1900-1-1");

        /// <summary>
        /// Initialize an new empty Client_AddressBook object.
        /// </summary>
        public Client_AddressBook()
        {
        }

        /// <summary>
        /// Initialize a new Client_AddressBook object with the given parameters.
        /// </summary>
        public Client_AddressBook(int iD, int clientID, bool primaryAddr, string name, string address, string tel, string fax, string contactPerson, string mobile, string pager, string sMS, int sMS_Lang, DateTime createdOn)
        {
            this.iD = iD;
            this.clientID = clientID;
            this.primaryAddr = primaryAddr;
            this.name = name;
            this.address = address;
            this.tel = tel;
            this.fax = fax;
            this.contactPerson = contactPerson;
            this.mobile = mobile;
            this.pager = pager;
            this.sMS = sMS;
            this.sMS_Lang = sMS_Lang;
            this.createdOn = createdOn;
        }

        /// <summary>
        /// Loads a Client_AddressBook object from the database using the given ID
        /// </summary>
        /// <param name="iD">The primary key value</param>
        /// <returns>A Client_AddressBook object</returns>
        public static Client_AddressBook Load(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spClient_AddressBook_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    Client_AddressBook result = new Client_AddressBook();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a Client_AddressBook object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A Client_AddressBook object</returns>
        public static Client_AddressBook LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spClient_AddressBook_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    Client_AddressBook result = new Client_AddressBook();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a collection of Client_AddressBook objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Client_AddressBook objects in the database.</returns>
        public static Client_AddressBookCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] { };
            return LoadCollection("spClient_AddressBook_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Client_AddressBook objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Client_AddressBook objects in the database ordered by the columns specified.</returns>
        public static Client_AddressBookCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spClient_AddressBook_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Client_AddressBook objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Client_AddressBook objects in the database.</returns>
        public static Client_AddressBookCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spClient_AddressBook_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Client_AddressBook objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the Client_AddressBook objects in the database ordered by the columns specified.</returns>
        public static Client_AddressBookCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spClient_AddressBook_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of Client_AddressBook objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the Client_AddressBook objects in the database.</returns>
        public static Client_AddressBookCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            Client_AddressBookCollection result = new Client_AddressBookCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    Client_AddressBook tmp = new Client_AddressBook();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a Client_AddressBook object from the database.
        /// </summary>
        /// <param name="iD">The primary key value</param>
        public static void Delete(int iD)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ID", iD) };
            SqlHelper.Default.ExecuteNonQuery("spClient_AddressBook_DelRec", parameterValues);
        }


        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) iD = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) clientID = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) primaryAddr = reader.GetBoolean(2);
                if (!reader.IsDBNull(3)) name = reader.GetString(3);
                if (!reader.IsDBNull(4)) address = reader.GetString(4);
                if (!reader.IsDBNull(5)) tel = reader.GetString(5);
                if (!reader.IsDBNull(6)) fax = reader.GetString(6);
                if (!reader.IsDBNull(7)) contactPerson = reader.GetString(7);
                if (!reader.IsDBNull(8)) mobile = reader.GetString(8);
                if (!reader.IsDBNull(9)) pager = reader.GetString(9);
                if (!reader.IsDBNull(10)) sMS = reader.GetString(10);
                if (!reader.IsDBNull(11)) sMS_Lang = reader.GetInt32(11);
                if (!reader.IsDBNull(12)) createdOn = reader.GetDateTime(12);
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

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public bool PrimaryAddr
        {
            get { return primaryAddr; }
            set { primaryAddr = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        public string Pager
        {
            get { return pager; }
            set { pager = value; }
        }

        public string SMS
        {
            get { return sMS; }
            set { sMS = value; }
        }

        public int SMS_Lang
        {
            get { return sMS_Lang; }
            set { sMS_Lang = value; }
        }

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;

            SqlHelper.Default.ExecuteNonQuery("spClient_AddressBook_InsRec", "@ID", out returnedValue, parameterValues);

            iD = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();

            SqlHelper.Default.ExecuteNonQuery("spClient_AddressBook_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[13];
            prams[0] = GetSqlParameter("@ID", ParameterDirection.Output, SqlDbType.Int, 4, this.ID);
            prams[1] = GetSqlParameter("@ClientID", ParameterDirection.Input, SqlDbType.Int, 4, this.ClientID);
            prams[2] = GetSqlParameter("@PrimaryAddr", ParameterDirection.Input, SqlDbType.Bit, 1, this.PrimaryAddr);
            prams[3] = GetSqlParameter("@Name", ParameterDirection.Input, SqlDbType.NVarChar, 128, this.Name);
            prams[4] = GetSqlParameter("@Address", ParameterDirection.Input, SqlDbType.NVarChar, 255, this.Address);
            prams[5] = GetSqlParameter("@Tel", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.Tel);
            prams[6] = GetSqlParameter("@Fax", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.Fax);
            prams[7] = GetSqlParameter("@ContactPerson", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.ContactPerson);
            prams[8] = GetSqlParameter("@Mobile", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.Mobile);
            prams[9] = GetSqlParameter("@Pager", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.Pager);
            prams[10] = GetSqlParameter("@SMS", ParameterDirection.Input, SqlDbType.NVarChar, 32, this.SMS);
            prams[11] = GetSqlParameter("@SMS_Lang", ParameterDirection.Input, SqlDbType.Int, 4, this.SMS_Lang);
            prams[12] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
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
                GetSqlParameterWithoutDirection("@ClientID", SqlDbType.Int, 4, this.ClientID),
                GetSqlParameterWithoutDirection("@PrimaryAddr", SqlDbType.Bit, 1, this.PrimaryAddr),
                GetSqlParameterWithoutDirection("@Name", SqlDbType.NVarChar, 128, this.Name),
                GetSqlParameterWithoutDirection("@Address", SqlDbType.NVarChar, 255, this.Address),
                GetSqlParameterWithoutDirection("@Tel", SqlDbType.NVarChar, 32, this.Tel),
                GetSqlParameterWithoutDirection("@Fax", SqlDbType.NVarChar, 32, this.Fax),
                GetSqlParameterWithoutDirection("@ContactPerson", SqlDbType.NVarChar, 64, this.ContactPerson),
                GetSqlParameterWithoutDirection("@Mobile", SqlDbType.NVarChar, 32, this.Mobile),
                GetSqlParameterWithoutDirection("@Pager", SqlDbType.NVarChar, 32, this.Pager),
                GetSqlParameterWithoutDirection("@SMS", SqlDbType.NVarChar, 32, this.SMS),
                GetSqlParameterWithoutDirection("@SMS_Lang", SqlDbType.Int, 4, this.SMS_Lang),
                GetSqlParameterWithoutDirection("@CreatedOn", SqlDbType.DateTime, 8, this.CreatedOn)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("iD: " + iD.ToString()).Append("\r\n");
            builder.Append("clientID: " + clientID.ToString()).Append("\r\n");
            builder.Append("primaryAddr: " + primaryAddr.ToString()).Append("\r\n");
            builder.Append("name: " + name.ToString()).Append("\r\n");
            builder.Append("address: " + address.ToString()).Append("\r\n");
            builder.Append("tel: " + tel.ToString()).Append("\r\n");
            builder.Append("fax: " + fax.ToString()).Append("\r\n");
            builder.Append("contactPerson: " + contactPerson.ToString()).Append("\r\n");
            builder.Append("mobile: " + mobile.ToString()).Append("\r\n");
            builder.Append("pager: " + pager.ToString()).Append("\r\n");
            builder.Append("sMS: " + sMS.ToString()).Append("\r\n");
            builder.Append("sMS_Lang: " + sMS_Lang.ToString()).Append("\r\n");
            builder.Append("createdOn: " + createdOn.ToString()).Append("\r\n");
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

            Client_AddressBookCollection source;

            if (OrderBy == null || OrderBy.Length == 0)
            {
                OrderBy = TextField;
            }

            if (WhereClause.Length > 0)
            {
                source = Client_AddressBook.LoadCollection(WhereClause, OrderBy, true);
            }
            else
            {
                source = Client_AddressBook.LoadCollection(OrderBy, true);
            }

            Common.ComboList sourceList = new Common.ComboList();

            if (BlankLine)
            {
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
            }

            foreach (Client_AddressBook item in source)
            {
                bool filter = false;
                if (ParentFilter.Trim() != String.Empty)
                {
                    filter = true;
                    if (item.ClientID != 0)
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


        private static bool IgnorThis(Client_AddressBook target, string parentFilter)
        {
            bool result = true;
            parentFilter = parentFilter.Replace(" ", "");       // remove spaces
            parentFilter = parentFilter.Replace("'", "");       // remove '
            string[] parsed = parentFilter.Split('=');          // parse

            if (target.ClientID == 0)
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
                Client_AddressBook parentTemplate = Client_AddressBook.Load(target.ClientID);
                result = IgnorThis(parentTemplate, parentFilter);
            }
            return result;
        }

        private static string GetFormatedText(Client_AddressBook target, string[] textField, string textFormatString)
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
    /// Represents a collection of <see cref="Client_AddressBook">Client_AddressBook</see> objects.
    /// </summary>
    public class Client_AddressBookCollection : BindingList<Client_AddressBook>
    {
    }
}