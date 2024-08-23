using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_community_posts")]
    public class ElioCommunityPosts : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("creator_user_id")]
        public int CreatorUserId { get; set; }

        [FieldInfo("topic")]
        public string Topic { get; set; }

        [FieldInfo("post")]
        public string Post { get; set; }

        [FieldInfo("topic_url")]
        public string TopicUrl { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("total_votes")]
        public int TotalVotes { get; set; }

        [FieldInfo("total_comments")]
        public int TotalComments { get; set; }

        [FieldInfo("must_read")]
        public int MustRead { get; set; }
    }
}