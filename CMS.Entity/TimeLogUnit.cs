namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_TimeLogUnit")]
    public partial class TimeLogUnit
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("CreatedByUser")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("LastModifiedByUser")]
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public virtual AspNetUser CreatedByUser { get; set; }
        public virtual AspNetUser LastModifiedByUser { get; set; }
    }
}
