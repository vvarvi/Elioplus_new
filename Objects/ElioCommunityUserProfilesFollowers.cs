using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_community_user_profiles_followers")]
    public class ElioCommunityUserProfilesFollowers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("follower_user_id")]
        public int FollowerUserId { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
    }
}