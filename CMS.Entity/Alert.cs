namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Alert")]
    public partial class Alert
    {
        [Key]
        public int AlertId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(128)]
        [ForeignKey("AlertToUser")]
        public string AlertTo { get; set; }

        [StringLength(128)]
        [ForeignKey("AlertByUser")]
        public string AlertBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsActive { get; set; }
        public virtual AspNetUser AlertToUser { get; set; }
        public virtual AspNetUser AlertByUser { get; set; }
    }
}
