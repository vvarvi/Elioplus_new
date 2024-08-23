using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_user_program_review_votes")]
    public class ElioUserProgramReviewVotes : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("Elio_user_program_review_visitor_id")]
        public int ElioUserProgramReviewVisitorId { get; set; }

        [FieldInfo("Elio_user_program_review_id")]
        public int ElioUserProgramReviewId { get; set; }

        [FieldInfo("votes")]
        public int Votes { get; set; }
    }
}