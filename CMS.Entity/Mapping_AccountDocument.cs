namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_AccountDocument")]
    public partial class AccountDocumentMapping
    {
        [Key]
        public string DocumentId { get; set; }

        public int AccountId { get; set; }

        [StringLength(300)]
        public string FileName { get; set; }

        [StringLength(300)]
        public string FileLocation { get; set; }

        public virtual Account Account { get; set; }
    }
}
