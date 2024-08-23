using System;
using System.Reflection;

namespace WdS.ElioPlus.Objects
{
    public class FieldInfoAttribute : Attribute
    {
        private string _FieldName;
        private bool _IsPrimaryKey;
        private bool _IsIdentity;
        private bool _Persist;

        public string FieldName { get { return _FieldName; } }
        public bool IsPrimaryKey 
        { 
            get { return _IsPrimaryKey; } 
            set { _IsPrimaryKey = value; } 
        }
        public bool IsIdentity
        {
            get { return _IsIdentity; }
            set { _IsIdentity = value; }
        }

        public bool Persist
        {
            get { return _Persist; }
            set { _Persist = value; }
        }

        public FieldInfoAttribute(string fieldName)
        {
            this._FieldName = fieldName;
            this.IsPrimaryKey = false;
            this.IsIdentity = false;
            this.Persist = true;
        }
    }

    public class ClassInfoAttribute : Attribute
    {
        private string _TableName;

        public string tableName { get { return _TableName; } }

        public ClassInfoAttribute(string table)
        {
            this._TableName = table;
        }
    }

    public class PropInfo
    {
        public PropertyInfo PropertyInfo;
        public FieldInfoAttribute FieldInfo;
    }
}
