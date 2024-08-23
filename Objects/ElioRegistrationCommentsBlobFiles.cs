using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_registration_comments_blob_files
    /// </summary>
    [ClassInfo("Elio_registration_comments_blob_files")]
    public partial class ElioRegistrationCommentsBlobFiles : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : comment_files_id
        /// </summary>
        [FieldInfo("comment_files_id")]
        public int CommentFilesId { get; set; }    
        /// <summary>
        /// Database Field : blob_file
        /// </summary>
        [FieldInfo("blob_file")]
        public Byte[] BlobFile { get; set; }
        /// <summary>
        /// Database Field : file_type
        /// </summary>
        [FieldInfo("file_type")]
        public string FileType { get; set; }
        /// <summary>
        /// Database Field : file_name
        /// </summary>
        [FieldInfo("file_name")]
        public string FileName { get; set; }
        /// <summary>
        /// Database Field : file_size
        /// </summary>
        [FieldInfo("file_size")]
        public int FileSize { get; set; }
        /// <summary>
        /// Database Field : file_path
        /// </summary>
        [FieldInfo("file_path")]
        public string FilePath { get; set; }
        /// <summary>
        /// Database Field : date_created
        /// </summary>
        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
