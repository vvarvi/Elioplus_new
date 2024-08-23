using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using System.Data.SqlClient;

namespace WdS.ElioPlus.Lib.DB
{
    public class DatabaseHelper
    {
        static private Dictionary<Type, string> _ClassAttributes;

        static public string GetTableNameForClass(Type t)
        {
            if (DatabaseHelper._ClassAttributes == null)
                DatabaseHelper._ClassAttributes = new Dictionary<Type, string>();

            lock (DatabaseHelper._ClassAttributes)
            {
                if (DatabaseHelper._ClassAttributes.ContainsKey(t))
                    return DatabaseHelper._ClassAttributes[t];
                else
                {
                    object[] attributes = t.GetCustomAttributes(typeof(ClassInfoAttribute), false);
                    if (attributes.Length == 1)
                    {
                        ClassInfoAttribute ci = attributes[0] as ClassInfoAttribute;
                        DatabaseHelper._ClassAttributes.Add(t, ci.tableName);
                        return ci.tableName;
                    }
                }
                return "";
            }
        }

        static object lockObject = new object();
        static volatile private Dictionary<Type, Dictionary<string, PropInfo>> PropertyCache;

        static public Dictionary<string, PropInfo> GetTypePropertiesCached(Type t)
        {
            try
            {
                if (DatabaseHelper.PropertyCache == null)
                    DatabaseHelper.PropertyCache = new Dictionary<Type, Dictionary<string, PropInfo>>();
                lock (lockObject)
                {
                    if (DatabaseHelper.PropertyCache.ContainsKey(t))
                    {
                        try
                        {
                            return DatabaseHelper.PropertyCache[t];
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("DatabaseHelper:GetTypePropertiesCached Error Loading already cached type 1 ");
                            throw ex;
                        }
                    }
                    else
                    {
                        try
                        {
                            System.Reflection.PropertyInfo[] props = t.GetProperties();
                            Dictionary<string, PropInfo> properties = new Dictionary<string, PropInfo>(); ;
                            for (int i = 0; i < props.Length; i++)
                            {
                                System.Reflection.PropertyInfo property = props[i];
                                try
                                {
                                    object[] attributes = property.GetCustomAttributes(typeof(Objects.FieldInfoAttribute), false);
                                    if (attributes.Length == 1)
                                    {
                                        Objects.FieldInfoAttribute attribute = attributes[0] as Objects.FieldInfoAttribute;
                                        PropInfo pi = new PropInfo();
                                        pi.FieldInfo = attribute;
                                        pi.PropertyInfo = property;
                                        properties.Add(attribute.FieldName.ToLower(), pi);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error("DatabaseHelper:GetTypePropertiesCached Error Loading property 2 ");
                                    throw ex;
                                }
                            }
                            DatabaseHelper.PropertyCache.Add(t, properties);
                            return properties;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("DatabaseHelper:GetTypePropertiesCached Error Loading not already cached type 3 ");
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("DatabaseHelper:GetTypePropertiesCached Error exception 4 ");
                throw ex;
            }
        }
        
        public DataTable GetDataTable(string sql, SqlConnection cn, SqlTransaction trans)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable table = new DataTable();
            da.Fill(table);
            da.Dispose();
            return table;
        }

        static public DataTable GetDataTable(string sql, DBSession session)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, session.Connection);
            da.SelectCommand.Transaction = session.Transaction;
            DataTable table = new DataTable();
            da.Fill(table);
            da.Dispose();
            return table;
        }

        static public DataTable GetDataTable(string sql, SqlParameter[] parameters, DBSession session)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, session.Connection);
            da.SelectCommand.Transaction = session.Transaction;
            da.SelectCommand.Parameters.AddRange(parameters);
            DataTable table = new DataTable();
            da.Fill(table);
            da.Dispose();
            return table;
        }

        /// <summary>
        /// Accepts an object, its property info array and a datareader and it fills the properties reading the data reader
        /// We pass the field names of the data reader so the speed is better since we get no exceptions trying to 
        /// retrieve every property
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        static public void FillObject(SqlDataReader reader, object obj, List<string> FieldNames, Dictionary<string, PropInfo> properties)
        {
            for (int i = 0; i < FieldNames.Count; i++)
            {
                if (properties.ContainsKey(FieldNames[i].ToLower()))
                {
                    PropertyInfo prop = properties[FieldNames[i].ToLower()].PropertyInfo;
                    try
                    {
                        object value = reader[FieldNames[i]];
                        if (value == System.DBNull.Value)
                        {
                            if (prop.PropertyType == typeof(String))
                            {
                                prop.SetValue(obj, "", new object[] { });
                            }
                            else if (prop.PropertyType == typeof(DateTime))
                            {
                                prop.SetValue(obj, null, new object[] { });
                            }

                        }
                        else
                        {
                            value = DatabaseHelper.ChangeType(value, prop.PropertyType);
                            prop.SetValue(obj, value, new object[] { });
                        }
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Field " + FieldNames[i] + " error setting value : " + ex.Message);
                        throw new Exception("Field " + FieldNames[i] + " error setting value : " + ex.Message);
                    }
                }
            }
        }

        public static object ChangeType(object value, Type conversionType)
        {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            } // end if

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType &&
              conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                // It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
                // InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
                // determine what the underlying type is
                // If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
                // have a type--so just return null
                // Note: We only do this check if we're converting to a nullable type, since doing it outside
                // would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
                // value is null and conversionType is a value type.
                if (value == null)
                {
                    return null;
                } // end if

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            } // end if

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return Convert.ChangeType(value, conversionType);
        }
        //vvarvi
        static public SqlParameter CreateDecimalParameter(string name, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.Int;
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = Convert.ToDecimal(value);
            return p;
        }
        static public SqlParameter CreateStringParameter(string name, string value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.VarChar;
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = value;
            return p;
        }        
        static public SqlParameter CreateBoolParameter(string name, bool value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.Bit;
            p.Value = value;
            return p;
        }
        static public SqlParameter CreateIntParameter(string name, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.Int;
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = Convert.ToInt32(value);
            return p;
        }
        static public SqlParameter CreateLongParameter(string name, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.BigInt;
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = Convert.ToInt64(value);
            return p;
        }
        static public SqlParameter CreateDateTimeParameter(string name, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = SqlDbType.DateTime;
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = Convert.ToDateTime(value);
            return p;
        }

        static public SqlParameter CreateParameterForPropInfo(PropertyInfo info, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = info.Name;

            Type T = info.PropertyType;
            if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter converter = new NullableConverter(info.PropertyType);
                T = converter.UnderlyingType;
            }

            if (T == typeof(string))
            {
                p.SqlDbType = SqlDbType.VarChar;
            }
            else if (T == typeof(DateTime?))
            {
                p.SqlDbType = SqlDbType.Date;
            }
            else if (T == typeof(DateTime))
            {
                p.SqlDbType = SqlDbType.Date;
            }
            else if (T == typeof(Int32))
            {
                p.SqlDbType = SqlDbType.Int;
            }
            else if (T == typeof(Int64))
            {
                p.SqlDbType = SqlDbType.BigInt;
            }
            else if (T == typeof(decimal))
            {
                p.SqlDbType = SqlDbType.Decimal;
            }
            else if (T == typeof(bool))
            {
                p.SqlDbType = SqlDbType.Bit;
            }
            else
            {
                throw new Exception("SqlDbType not set for parameter : " + info.Name);
            }
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = value;
            return p;
        }

        static public SqlParameter CreateParameterForProperty(PropInfo info, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = info.FieldInfo.FieldName;

            Type T = info.PropertyInfo.PropertyType;
            if (info.PropertyInfo.PropertyType.IsGenericType && info.PropertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter converter = new NullableConverter(info.PropertyInfo.PropertyType);
                T = converter.UnderlyingType;
            }

            if (T == typeof(string))
            {
                p.SqlDbType = SqlDbType.VarChar;
            }
            else if (T == typeof(DateTime?))
            {
                p.SqlDbType = SqlDbType.DateTime;
            }
            else if (T == typeof(DateTime))
            {
                p.SqlDbType = SqlDbType.DateTime;
            }
            else if (T == typeof(Int32))
            {
                p.SqlDbType = SqlDbType.Int;
            }
            else if (T == typeof(Int64))
            {
                p.SqlDbType = SqlDbType.BigInt;
            }
            else if (T == typeof(decimal))
            {
                p.SqlDbType = SqlDbType.Decimal;
            }
            else if (T == typeof(bool))
            {
                p.SqlDbType = SqlDbType.Bit;
            }
            else if (T == typeof(Byte[]))
            {
                p.SqlDbType = SqlDbType.VarBinary;
            }
            else
            {
                throw new Exception("SqlDbType not set for parameter : " + info.FieldInfo.FieldName);
            }
            if (value == null)
                p.Value = System.DBNull.Value;
            else
                p.Value = value;
            return p;
        }
    }
}