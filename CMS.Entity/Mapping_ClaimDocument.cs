namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_ClaimDocument")]
    public partial class ClaimDocumentMapping
    {
        [Key]
        public int DocumentId { get; set; }

        [ForeignKey("Claim")]
        public int ClaimId { get; set; }

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

        public virtual Claim Claim { get; set; }
    }
}
