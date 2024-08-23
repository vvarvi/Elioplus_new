using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    public class DataObject
    {
        public virtual void BeforeInsert()
        { }
        public virtual void BeforeUpdate()
        { }

        public object GetValue(string fieldname)
        {
            try
            {
                return this.GetType().InvokeMember(fieldname, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, this, null);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void SetValue(string fieldname, object value)
        {
            this.GetType().InvokeMember(fieldname, System.Reflection.BindingFlags.SetProperty, null, this, new object[] { value });
        }
    }
}
