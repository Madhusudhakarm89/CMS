namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_mapping_AspNetUsersDocument")]
    public partial class AspNetUsersDocumentMapping
    {
        [Key]
        public int DocumentId { get; set; }

        [ForeignKey("AspNetUser")]
        public string UserId { get; set; }

        [StringLength(30)]
        public string FileType { get; set; }

        [StringLength(300)]
        public string FileName { get; set; }

        [StringLength(300)]
        public string FileLocation { get; set; }

        [StringLength(250)]
        public string FileDisplayName { get; set; }

        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
