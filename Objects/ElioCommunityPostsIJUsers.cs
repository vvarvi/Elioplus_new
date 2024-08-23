using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class ElioCommunityPostsIJUsers : DataObject
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

        [FieldInfo("username")]
        public string Username { get; set; }

        [FieldInfo("last_name")]
        public string LastName { get; set; }

        [FieldInfo("first_name")]
        public string FirstName { get; set; }

        [FieldInfo("personal_image")]
        public string PersonalImage { get; set; }

        [FieldInfo("position")]
        public string Position { get; set; }

        [FieldInfo("community_summary_text")]
        public string CommunitySummaryText { get; set; }
    }
}