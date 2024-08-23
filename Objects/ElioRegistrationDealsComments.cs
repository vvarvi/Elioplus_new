using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_registration_deals_comments")]
    public class ElioRegistrationDealsComments : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("deal_id")]
        public int DealId { get; set; }

        [FieldInfo("comment_id")]
        public int CommentId { get; set; }
    }
}