using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_community_posts_comments")]
    public class ElioCommunityPostsComments : DataObject
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

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("reply_to_comment_id")]
        public int ReplyToCommentId { get; set; }

        [FieldInfo("depth")]
        public int Depth { get; set; }
    }
}