using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{    
    public class ElioSubIndustriesGroupItemsByCountry : DataObject
    {        
        [FieldInfo("sub_industies_group_id", IsPrimaryKey = true)]
        public int SubIndustriesGroupId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("number")]
        public int Number { get; set; }
    }
}