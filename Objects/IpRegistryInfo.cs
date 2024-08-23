using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class IpRegistryInfo : DataObject
    {      
        [FieldInfo("ip")]
        public string Ip { get; set; }

        [FieldInfo("type")]
        public string Type { get; set; }

        [FieldInfo("hostname")]
        public string Hostname { get; set; }

        [FieldInfo("company")]
        public Company Company{ get; set; }
    }

    public class Company
    {
        [FieldInfo("domain")]
        public string Domain { get; set; }

        [FieldInfo("name")]
        public string Name { get; set; }

        [FieldInfo("type")]
        public string Type { get; set; }
    }
}