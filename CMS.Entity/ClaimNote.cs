
namespace CMS.Entity
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion

    [Table("tx_ClaimNote")]
    public partial class ClaimNote
    {
        [Key]
        public int NoteId { get; set; }

        [ForeignKey("ClaimDetails")]
        public int ClaimId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(8000)]
        public string Description { get; set; }

        public bool IsTask { get; set; }
        public DateTime? TaskDueDate { get; set; }

        [ForeignKey("AssignedToUser")]
        public string AssignedTo { get; set; }
        public string CreatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual AspNetUser AssignedToUser { get; set; }
        public virtual Claim ClaimDetails { get; set; }

        public virtual AspNetUser CreatedByUser { get; set; }
    }
}
