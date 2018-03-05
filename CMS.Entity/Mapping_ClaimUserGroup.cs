namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_ClaimUserGroup")]
    public partial class ClaimUserGroupMapping
    {
        [Key]
        public int DocumentId { get; set; }

        public int? ClaimId { get; set; }

        public int? UserGroupId { get; set; }

        public virtual Claim Claim { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
