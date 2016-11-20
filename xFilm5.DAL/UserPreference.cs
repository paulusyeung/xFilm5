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
using id = System.String;

namespace xFilm5.DAL
{
    /// <summary>
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.UserPreference.
    /// Date Created:   2016-11-13 09:17:02
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class UserPreference
    {
        private int key = 0;
        private int preferenceId = 0;
        private int userId = 0;
        private Guid objectId = Guid.Empty;
        private int objectType = 0;
        private Dictionary<id, MetadataAttributes> metadataXml = new Dictionary<id, MetadataAttributes>();

        /// <summary>
        /// Initialize an new empty UserPreference object.
        /// </summary>
        public UserPreference()
        {
        }
		
        /// <summary>
        /// Initialize a new UserPreference object with the given parameters.
        /// </summary>
        public UserPreference(int preferenceId, int userId, Guid objectId, int objectType, Dictionary<id, MetadataAttributes> metadataXml)
        {
                this.preferenceId = preferenceId;
                this.userId = userId;
                this.objectId = objectId;
                this.objectType = objectType;
                this.metadataXml = metadataXml;
        }	
		
        /// <summary>
        /// Loads a UserPreference object from the database using the given PreferenceId
        /// </summary>
        /// <param name="preferenceId">The primary key value</param>
        /// <returns>A UserPreference object</returns>
        public static UserPreference Load(int preferenceId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PreferenceId", preferenceId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spUserPreference_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    UserPreference result = new UserPreference();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a UserPreference object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A UserPreference object</returns>
        public static UserPreference LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spUserPreference_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    UserPreference result = new UserPreference();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of UserPreference objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the UserPreference objects in the database.</returns>
        public static UserPreferenceCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spUserPreference_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of UserPreference objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the UserPreference objects in the database ordered by the columns specified.</returns>
        public static UserPreferenceCollection LoadCollection(string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length-1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spUserPreference_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of UserPreference objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the UserPreference objects in the database.</returns>
        public static UserPreferenceCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spUserPreference_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of UserPreference objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the UserPreference objects in the database ordered by the columns specified.</returns>
        public static UserPreferenceCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length-1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause), new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spUserPreference_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of UserPreference objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the UserPreference objects in the database.</returns>
        public static UserPreferenceCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            UserPreferenceCollection result = new UserPreferenceCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    UserPreference tmp = new UserPreference();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a UserPreference object from the database.
        /// </summary>
        /// <param name="preferenceId">The primary key value</param>
        public static void Delete(int preferenceId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PreferenceId", preferenceId) };
            SqlHelper.Default.ExecuteNonQuery("spUserPreference_DelRec", parameterValues);
        }

        #region XML Manipulation

        #region Metadata Attributes
        
        public class MetadataAttribute
        {
            private string _key = string.Empty;
            private string _value = string.Empty;

            public MetadataAttribute(string key, string value)
            {
                _key = key;
                _value = value;
            }

            public string Key
            {
                get
                {
                    return _key;
                }
                set
                {
                    _key = value;
                }
            }

            public string Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }
        }

        public class MetadataAttributes : BindingList<MetadataAttribute>
        {
        }

        #endregion

        protected string RootNode = "Metadata";

        /// <summary>
        /// Process the SqlXml Data from Database to prepare MetadataXml.
        /// </summary>
        private void PrepareMetadataXml(SqlXml dataXml, out Dictionary<id, MetadataAttributes> metadataXml)
        {
            metadataXml = new Dictionary<id, MetadataAttributes>();

            if (!dataXml.IsNull)
            {
                XmlDocument metadata = new XmlDocument();
                metadata.LoadXml(dataXml.Value);

                XmlNodeList dataList = metadata.SelectNodes("//data");
                if (dataList.Count > 0)
                {
                    MetadataAttributes attributes = new MetadataAttributes();

                    foreach (XmlNode node in dataList)
                    {
                        string key = node.ChildNodes[0].InnerText;
                        string value = node.ChildNodes[1].InnerText;

                        attributes.Add(new MetadataAttribute(key, value));
                    }

                    if (attributes.Count > 0)
                    {
                        metadataXml.Add("data", attributes);
                    }
                }
                else
                {
                    MetadataAttributes attributes = new MetadataAttributes();
                    XmlNodeList targetNode = metadata.SelectNodes("//" + RootNode);
                    foreach (XmlNode node in targetNode)
                    {
                        if (node.HasChildNodes)
                        {
                            ProcessingNodes(node, ref metadataXml, attributes);
                        }
                        else
                        {
                            XmlAttributeCollection attrList = node.Attributes;
                            foreach (XmlAttribute attr in attrList)
                            {
                                attributes.Add(new MetadataAttribute(attr.Name, attr.Value));
                            }

                            if (attributes.Count > 0)
                            {
                                metadataXml.Add("data", attributes);
                            }
                        }
                    }
                }
            }
        }

        private void ProcessingNodes(XmlNode node, ref Dictionary<id, MetadataAttributes> metadataXml, MetadataAttributes attributes)
        {
            foreach (XmlNode child in node)
            {
                attributes = new MetadataAttributes();
                string metadataKey = string.Empty;
                XmlAttributeCollection attrList = child.Attributes;
                foreach (XmlAttribute attr in attrList)
                {
                    if (attr.Name == "id")
                    {
                        metadataKey = attr.Value;
                    }
                    else
                    {
                        attributes.Add(new MetadataAttribute(attr.Name, attr.Value));
                    }
                }

                if (metadataKey != string.Empty)
                {
                    metadataXml.Add(metadataKey, attributes);
                }
            }
        }

        /// <summary>
        /// Generate xml string.
        /// </summary>
        /// <returns>.</returns>
        public string GenerateXml(Dictionary<string, MetadataAttributes> metadataXml)
        {
            string sqlXml = string.Empty;
            XmlDocument metadata = new XmlDocument();
            XmlNode node = metadata.AppendChild(metadata.CreateElement(RootNode));

            foreach (KeyValuePair<id, MetadataAttributes> kvp in metadataXml)
            {
                XmlElement element = metadata.CreateElement("record");
                element.SetAttribute("id", kvp.Key);

                foreach (MetadataAttribute attr in kvp.Value)
                {
                    element.SetAttribute(attr.Key, attr.Value);
                }

                node.AppendChild(element);
            }

            sqlXml = metadata.OuterXml;
            return sqlXml;
        }
        

        public MetadataAttributes GetMetadataList(string id)
        {
            if (this.metadataXml.ContainsKey(id))
            {
                return this.metadataXml[id];
            }
            else
            {
                return new MetadataAttributes();
            }
        }

        /// <summary>
        /// GetMetadata by Key.
        /// </summary>
        /// <returns>The string value of the Key.</returns>
        public string GetMetadata(string key)
        {
            if (this.GetMetadataList("data").Count > 0)
            {
                MetadataAttributes attributes = this.GetMetadataList("data");
                string value = string.Empty;
                foreach (MetadataAttribute attr in attributes)
                {
                    if (attr.Key == key)
                    {
                        value = attr.Value;
                        break;
                    }
                }
                return value;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set Metadata with id = key
        /// </summary>
        public void SetMetadata(string key, MetadataAttributes data)
        {
            if (this.metadataXml == null)
            {
                this.metadataXml = new Dictionary<string, MetadataAttributes>();
            }

            if (this.metadataXml.ContainsKey(key))
            {
                this.metadataXml[key] = data;
            }
            else
            {
                this.metadataXml.Add(key, data);
            }
        }

        /// <summary>
        /// Set Metadata with id = key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetMetadata(string key, MetadataAttribute data)
        {
            MetadataAttributes attributes = this.GetMetadataList(key);

            if (!attributes.Contains(data))
            {
                attributes.Add(data);
            }

            this.SetMetadata(key, attributes);
        }

        /// <summary>
        /// Set Metadata with default id;
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetMetadata(string key, string data)
        {
            this.SetMetadata("data", new MetadataAttribute(key, data));
        }
        
        #endregion
		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetInt32(0);
                if (!reader.IsDBNull(0)) preferenceId = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) userId = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) objectId = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) objectType = reader.GetInt32(3);
                if (!reader.IsDBNull(4)) 
				{
					this.PrepareMetadataXml(reader.GetSqlXml(4), out this.metadataXml);
				}
					
            }
        }
		
        public void Delete()
        {
            Delete(this.PreferenceId);
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
                if (key != PreferenceId)
                    this.Delete();
                Update();
            }
        }

        public int PreferenceId
        {
            get { return preferenceId; }
            set { preferenceId = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public Guid ObjectId
        {
            get { return objectId; }
            set { objectId = value; }
        }

        public int ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        public Dictionary<id, MetadataAttributes> MetadataXml
        {
            get { return metadataXml; }
            set { metadataXml = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spUserPreference_InsRec", "@PreferenceId", out returnedValue, parameterValues);
            
            preferenceId = returnedValue != null ? (int)returnedValue : 0;
            key = returnedValue != null ? (int)returnedValue : 0;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spUserPreference_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@PreferenceId", ParameterDirection.Output, SqlDbType.Int, 4, this.PreferenceId);
            prams[1] = GetSqlParameter("@UserId", ParameterDirection.Input, SqlDbType.Int, 4, this.UserId);
            prams[2] = GetSqlParameter("@ObjectId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ObjectId);
            prams[3] = GetSqlParameter("@ObjectType", ParameterDirection.Input, SqlDbType.Int, 4, this.ObjectType);
            prams[4] = GetSqlParameter("@MetadataXml", ParameterDirection.Input, SqlDbType.Xml, -1, this.GenerateXml(this.MetadataXml));
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
                GetSqlParameterWithoutDirection("@PreferenceId", SqlDbType.Int, 4, this.PreferenceId),
                GetSqlParameterWithoutDirection("@UserId", SqlDbType.Int, 4, this.UserId),
                GetSqlParameterWithoutDirection("@ObjectId", SqlDbType.UniqueIdentifier, 16, this.ObjectId),
                GetSqlParameterWithoutDirection("@ObjectType", SqlDbType.Int, 4, this.ObjectType),
                GetSqlParameterWithoutDirection("@MetadataXml", SqlDbType.Xml, -1, this.GenerateXml(this.MetadataXml))
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("preferenceId: " + preferenceId.ToString()).Append("\r\n");
            builder.Append("userId: " + userId.ToString()).Append("\r\n");
            builder.Append("objectId: " + objectId.ToString()).Append("\r\n");
            builder.Append("objectType: " + objectType.ToString()).Append("\r\n");
            builder.Append("metadataXml: " + this.GenerateXml(this.MetadataXml)).Append("\r\n");
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
			LoadCombo(ref ddList, TextField, SwitchLocale, false, string.Empty, string.Empty, new string[]{ TextField });
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
			string [] textField = {TextField};
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
		public static void LoadCombo(ref ComboBox ddList, string [] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause, string[] OrderBy)
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
		public static void LoadCombo(ref ComboBox ddList, string [] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string ParentFilter, string WhereClause, string[] OrderBy)
		{
			if (SwitchLocale)
			{
				TextField = GetSwitchLocale(TextField);
			}
			ddList.Items.Clear();						
			
			UserPreferenceCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = UserPreference.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = UserPreference.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, 0));
			}
			
			foreach (UserPreference item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.UserId != 0)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.PreferenceId));
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
		
		
		private static bool IgnorThis(UserPreference target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.UserId == 0)
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
				UserPreference parentTemplate = UserPreference.Load(target.UserId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(UserPreference target, string [] textField, string textFormatString)
		{
			for (int i = 0; i < textField.Length; i++)
			{
				PropertyInfo pi = target.GetType().GetProperty(textField[i]);
				textFormatString = textFormatString.Replace("{" + i.ToString() +"}", pi != null ? pi.GetValue(target, null).ToString() : string.Empty);
			}
			return textFormatString;
		}
		
		private static string [] GetSwitchLocale(string [] source)
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
    /// Represents a collection of <see cref="UserPreference">UserPreference</see> objects.
    /// </summary>
    public class UserPreferenceCollection : BindingList< UserPreference>
    {
	}
}