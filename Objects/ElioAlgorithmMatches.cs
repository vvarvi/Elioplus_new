using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_algorithm_matches")]
    public class ElioAlgorithmMatches : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("match_id")]
        public int MatchId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}