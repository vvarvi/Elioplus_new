using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace WdS.ElioPlus.Lib.DB
{
    public class DataLoader<T>
    {
        public DBSession Session { get; set; }

        public DataLoader(DBSession session)
        {
            this.Session = session;
        }

        public T LoadSingle(string sql, params SqlParameter[] parameters)
        {
            List<T> items = this.Load(sql, parameters);
            if (items.Count == 1)
            {
                return items[0];
            }
            else if (items.Count > 1)
            {
                throw new Exception("More than one records found");
            }
            else return default(T);
        }

        public List<T> Load(string sql, params SqlParameter[] parameters)
        {
            DateTime startTime = DateTime.Now;

            try
            {
                Type t = typeof(T);
                List<T> list = new List<T>();
                SqlCommand cmd = new SqlCommand(sql, this.Session.Connection);
                cmd.CommandTimeout = this.Session.CommandTimeout;

                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }

                cmd.Transaction = this.Session.Transaction;

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, PropInfo> properties = DatabaseHelper.GetTypePropertiesCached(t);

                ConstructorInfo ci = t.GetConstructor(new Type[] { });
                // We should keep the field names retrieved so we do not have to try all the properties
                // and get exception errors
                List<string> FieldNames = new List<string>();

                try
                {
                    while (reader.Read())
                    {
                        // Fill the field names
                        if (FieldNames.Count == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                FieldNames.Add(reader.GetName(i));
                            }
                        }
                        T obj = (T)ci.Invoke(new object[] { });

                        list.Add(obj);

                        DatabaseHelper.FillObject(reader, obj, FieldNames, properties);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("DataLoader:Load Error Load  1");
                    Logger.Error("DataLoader:Load Error Load  sql:{0} Error:{1} ", sql, ex.Message);
                    throw ex;
                }

                reader.Close();
                cmd.Dispose();

                DBLogger.AppendToDbLog(startTime, Session.SessionGuid.ToString(), sql, 0, list.Count);

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error("DataLoader:Load Error Load  2");
                Logger.Error("DataLoader:Load Error Retrieving  sql:{0} Error:{1} ", sql, ex.Message);
                throw ex;
            }
        }

        public int Update(T obj)
        {
            DateTime startTime = DateTime.Now;

            int rows = 0;

            DataObject dobject = obj as DataObject;
            if (dobject != null)
                dobject.BeforeUpdate();


            Type ObjectType = obj.GetType();
            Dictionary<string, PropInfo> properties = DatabaseHelper.GetTypePropertiesCached(ObjectType);

            string TableName = DatabaseHelper.GetTableNameForClass(ObjectType);
            string sql = "UPDATE " + TableName + " SET ";
            int cnt = 0;
            List<string> fieldNames = new List<string>();
            foreach (string key in properties.Keys)
            {
                fieldNames.Add(key);
            }

            PropInfo PK = null;
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string key = fieldNames[i];
                PropInfo info = properties[key];
                if (info.FieldInfo.Persist)
                {
                    if (!info.FieldInfo.IsPrimaryKey)
                    {
                        if (cnt > 0) sql += ", ";
                        sql += " " + key + " = @" + key;
                        cnt++;
                    }
                    else
                    {
                        PK = info;
                    }
                }
            }
            sql += " WHERE " + PK.FieldInfo.FieldName + " = @" + PK.FieldInfo.FieldName;

            SqlCommand cmd = new SqlCommand(sql, this.Session.Connection);
            cmd.Transaction = this.Session.Transaction;

            // Add parameters
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string key = fieldNames[i];
                PropInfo info = properties[key];
                if (info.FieldInfo.Persist)
                {
                    if (!info.FieldInfo.IsPrimaryKey)
                    {
                        SqlParameter p = DatabaseHelper.CreateParameterForProperty(info, info.PropertyInfo.GetValue(obj, new object[] { }));
                        cmd.Parameters.Add(p);
                    }
                }
            }

            SqlParameter p2 = DatabaseHelper.CreateParameterForProperty(PK, PK.PropertyInfo.GetValue(obj, new object[] { }));
            cmd.Parameters.Add(p2);

            rows = cmd.ExecuteNonQuery();
            if (rows != 1)
                throw new Exception("Concurrency Update Exception");

            DBLogger.AppendToDbLog(startTime, Session.SessionGuid.ToString(), sql, 0, rows);

            return rows;
        }

        public int Insert(T obj, string hint)
        {
            return this.Insert(obj, true, hint);
        }

        public int Insert(T obj)
        {
            return this.Insert(obj, true);
        }

        public int Insert(T obj, bool updateIdentity)
        {
            return this.Insert(obj, true, "");
        }

        // we may ommit update identity for speed
        public int Insert(T obj, bool updateIdentity, string hint)
        {
            DateTime startTime = DateTime.Now;

            int rows = 0;

            DataObject dobject = obj as DataObject;
            if (dobject != null)
                dobject.BeforeInsert();

            Type ObjectType = obj.GetType();
            Dictionary<string, PropInfo> properties = DatabaseHelper.GetTypePropertiesCached(ObjectType);

            string TableName = DatabaseHelper.GetTableNameForClass(ObjectType);
            string sql = "INSERT INTO " + TableName + " " + hint + " (";
            int cnt = 0;
            List<string> fieldNames = new List<string>();
            foreach (string key in properties.Keys)
            {
                fieldNames.Add(key);
            }

            PropInfo IDENTITY = null;
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string key = fieldNames[i];
                PropInfo info = properties[key];
                if (info.FieldInfo.Persist)
                {
                    if (!info.FieldInfo.IsIdentity)
                    {
                        if (cnt > 0) sql += ", ";
                        sql += " " + key + " ";
                        cnt++;
                    }
                    else
                    {
                        IDENTITY = info;
                    }
                }
            }
            sql += ") VALUES (";

            cnt = 0;
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string key = fieldNames[i];
                PropInfo info = properties[key];
                if (info.FieldInfo.Persist)
                {
                    if (!info.FieldInfo.IsIdentity)
                    {
                        if (cnt > 0) sql += ", ";
                        sql += "@" + key;
                        cnt++;
                    }
                }
            }
            sql += ")";
            SqlCommand cmd = new SqlCommand(sql, this.Session.Connection);
            cmd.Transaction = this.Session.Transaction;

            // Add parameters
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string key = fieldNames[i];
                PropInfo info = properties[key];
                if (info.FieldInfo.Persist)
                {
                    if (!info.FieldInfo.IsIdentity)
                    {
                        #region To Delete

                        //SqlParameter p = new SqlParameter();
                        //p.ParameterName = info.FieldInfo.FieldName;

                        //Type T = info.PropertyInfo.PropertyType;
                        //if (info.PropertyInfo.PropertyType.IsGenericType && info.PropertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        //{
                        //    NullableConverter converter = new NullableConverter(info.PropertyInfo.PropertyType);
                        //    T = converter.UnderlyingType;
                        //}

                        //if (T == typeof(string))
                        //{
                        //    p.SqlDbType = SqlDbType.VarChar;
                        //}
                        //else if (T == typeof(DateTime?))
                        //{
                        //    p.SqlDbType = SqlDbType.Date;
                        //}
                        //else if (T == typeof(DateTime))
                        //{
                        //    p.SqlDbType = SqlDbType.Date;
                        //}
                        //else if (T == typeof(Int32))
                        //{
                        //    p.SqlDbType = SqlDbType.Int;
                        //}
                        //else if (T == typeof(Int64))
                        //{
                        //    p.SqlDbType = SqlDbType.BigInt;
                        //}
                        //else if (T == typeof(decimal))
                        //{
                        //    p.SqlDbType = SqlDbType.Decimal;
                        //}
                        //else if (T == typeof(bool))
                        //{
                        //    p.SqlDbType = SqlDbType.Bit;
                        //}
                        //else
                        //{
                        //    throw new Exception("SqlDbType not set for parameter : " + info.FieldInfo.FieldName);
                        //}
                        //if (Convert.ToInt32(info.PropertyInfo.GetValue(obj, new object[] { })) == null)
                        //    p.Value = System.DBNull.Value;
                        //else
                        //    p.Value = Convert.ToInt32(info.PropertyInfo.GetValue(obj, new object[] { }));

                        #endregion

                        SqlParameter p = DatabaseHelper.CreateParameterForProperty(info, info.PropertyInfo.GetValue(obj, new object[] { }));
                        
                        cmd.Parameters.Add(p);
                    }
                }
            }

            rows = cmd.ExecuteNonQuery();
            if (updateIdentity && IDENTITY != null)
            {
                int Identity = GetIdentity(this.Session);
                object IdObject = Identity;
                if (IDENTITY.PropertyInfo.PropertyType == typeof(decimal))
                    IdObject = Convert.ToDecimal(Identity);
                if (Identity != -1)
                {
                    IDENTITY.PropertyInfo.SetValue(obj, IdObject, new object[] { });
                }
            }

            DBLogger.AppendToDbLog(startTime, Session.SessionGuid.ToString(), sql, 0, rows);

            return rows;
        }

        public int Delete(T obj)
        {
            DateTime startTime = DateTime.Now;

            int rows = 0;

            Type ObjectType = obj.GetType();
            Dictionary<string, PropInfo> properties = DatabaseHelper.GetTypePropertiesCached(ObjectType);

            string TableName = DatabaseHelper.GetTableNameForClass(ObjectType);
            string sql = "DELETE FROM " + TableName + " WHERE ";

            PropInfo PK = null;
            foreach (PropInfo info in properties.Values)
            {

                if (info.FieldInfo.IsPrimaryKey)
                {
                    PK = info;
                    continue;
                }
            }
            sql += " " + PK.FieldInfo.FieldName + " = @" + PK.FieldInfo.FieldName;

            SqlCommand cmd = new SqlCommand(sql, this.Session.Connection);
            cmd.Transaction = this.Session.Transaction;

            SqlParameter p = DatabaseHelper.CreateParameterForProperty(PK, PK.PropertyInfo.GetValue(obj, new object[] { }));
            cmd.Parameters.Add(p);

            rows = cmd.ExecuteNonQuery();

            DBLogger.AppendToDbLog(startTime, Session.SessionGuid.ToString(), sql, 0, rows);

            return rows;
        }

        public int Delete(object PKValue)
        {
            DateTime startTime = DateTime.Now;

            int rows = 0;

            Type ObjectType = typeof(T);
            Dictionary<string, PropInfo> properties = DatabaseHelper.GetTypePropertiesCached(ObjectType);

            string TableName = DatabaseHelper.GetTableNameForClass(ObjectType);
            string sql = "DELETE FROM " + TableName + " WHERE ";

            PropInfo PK = null;
            foreach (PropInfo info in properties.Values)
            {

                if (info.FieldInfo.IsPrimaryKey)
                {
                    PK = info;
                    continue;
                }
            }
            sql += " " + PK.FieldInfo.FieldName + " = @" + PK.FieldInfo.FieldName;

            SqlCommand cmd = new SqlCommand(sql, this.Session.Connection);
            cmd.Transaction = this.Session.Transaction;

            SqlParameter p = DatabaseHelper.CreateParameterForProperty(PK, PKValue);
            cmd.Parameters.Add(p);

            rows = cmd.ExecuteNonQuery();

            DBLogger.AppendToDbLog(startTime, Session.SessionGuid.ToString(), sql, 0, rows);

            return rows;
        }

        public int DeleteByQuery(string sql, params SqlParameter[] parameters)
        {
            int rows = 0;

            List<T> list = this.Load(sql, parameters);
            for (int i = 0; i < list.Count; i++)
            {
                rows += this.Delete(list[i]);
            }
            return rows;
        }

        public int GetIdentity(DBSession session)
        {
            SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY", session.Connection);
            cmd.Transaction = session.Transaction;
            SqlDataReader reader = cmd.ExecuteReader();
            int Identity = -1;
            if (reader.Read())
            {
                Identity = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            cmd.Dispose();

            return Identity;
        }
    }
}