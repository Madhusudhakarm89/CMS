namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_ContactUserGroup")]
    public partial class ContactUserGroupMapping
    {
        [Key]
        public int DocumentId { get; set; }

        public int? ContactId { get; set; }

        public int? UserGroupId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
