using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class ElioCommunityPostsCommentsIJPosts : DataObject
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
        public DateTime CommentSysDate { get; set; }

        //[FieldInfo("topic")]
        //public string Topic { get; set; }

        //[FieldInfo("post")]
        //public string Post { get; set; }

        //[FieldInfo("topic_url")]
        //public string TopicUrl { get; set; }

        //[FieldInfo("sysdate")]
        //public DateTime PostSysDate { get; set; }

    }
}