namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_ContactDocument")]
    public partial class ContactDocumentMapping
    {
        [Key]
        public int DocumentId { get; set; }

        public int? ContactId { get; set; }

        [StringLength(300)]
        public string FileName { get; set; }

        [StringLength(300)]
        public string FileLocation { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
