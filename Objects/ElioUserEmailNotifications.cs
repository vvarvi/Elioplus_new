using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_user_email_notifications")]
    public class ElioUserEmailNotifications : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("html_email_body")]
        public string HtmlEmailBody { get; set; }

        [FieldInfo("subject")]
        public string Subject { get; set; }
    }
}