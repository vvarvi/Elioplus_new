using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    public class ElioCommunityPostsCommentsIJUsers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("community_post_id")]
        public int CommunityPostId { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("comment")]
        public string Comment { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

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

        [FieldInfo("reply_to_comment_id")]
        public int ReplyToCommentId { get; set; }

        [FieldInfo("depth")]
        public int Depth { get; set; }
    }
}