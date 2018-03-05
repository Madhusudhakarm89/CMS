
namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_InvoiceTimelog")]
    public partial class Mapping_InvoiceTimelog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        [ForeignKey("Timelog")]
        public int TimelogId { get; set; }

        public decimal ServiceTotal { get; set; }

        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual TimeLog Timelog { get; set; }
    }
}
