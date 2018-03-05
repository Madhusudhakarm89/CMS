namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_UserGroup")]
    public partial class UserGroup
    {
        public UserGroup()
        {
            AccountUserGroupMapping = new HashSet<AccountUserGroupMapping>();
            ClaimUserGroupMapping = new HashSet<ClaimUserGroupMapping>();
            ContactUserGroupMapping = new HashSet<ContactUserGroupMapping>();
        }

        [Key]
        public int UserGroupId { get; set; }

        [StringLength(200)]
        public string UserGroupName { get; set; }

        public bool IsActive { get; set; }
        
        public virtual ICollection<AccountUserGroupMapping> AccountUserGroupMapping { get; set; }

        public virtual ICollection<ClaimUserGroupMapping> ClaimUserGroupMapping { get; set; }

        public virtual ICollection<ContactUserGroupMapping> ContactUserGroupMapping { get; set; }
    }
}
