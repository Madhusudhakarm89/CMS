namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_AccountUserGroup")]
    public partial class AccountUserGroupMapping
    {
        [Key]
        public int DocumentId { get; set; }

        public int? AccountId { get; set; }

        public int? UserGroupId { get; set; }

        public virtual Account Account { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
