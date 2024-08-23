using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_Exceptions
    /// </summary>
    [ClassInfo("Elio_Exceptions")]
    public partial class ElioExceptions : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_in_session
        /// </summary>
        [FieldInfo("user_in_session")]
        public int UserInSession { get; set; }
        /// <summary>
        /// Database Field : current_session_id
        /// </summary>
        [FieldInfo("current_session_id")]
        public string CurrentSessionId { get; set; }        
        /// <summary>
        /// Database Field : page_url
        /// </summary>
        [FieldInfo("page_url")]
        public string PageUrl { get; set; }
        /// <summary>
        /// Database Field : trace_function
        /// </summary>
        [FieldInfo("trace_function")]
        public string TraceFunction { get; set; }
        /// <summary>
        /// Database Field : exception_message
        /// </summary>
        [FieldInfo("exception_message")]
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// Database Field : stack_trace
        /// </summary>
        [FieldInfo("stack_trace")]
        public string StackTrace { get; set; }
        /// <summary>
        /// Database Field : remote_ip
        /// </summary>
        [FieldInfo("remote_ip")]
        public string RemoteIp { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
