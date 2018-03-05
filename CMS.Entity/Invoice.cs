
namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Invoice")]
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceTimelogs = new HashSet<Mapping_InvoiceTimelog>();
        }

        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceClaim")]
        public int ClaimId { get; set; }

        [StringLength(200)]
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InvoiceTotal { get; set; }

        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual Claim InvoiceClaim { get; set; }
        public virtual ICollection<Mapping_InvoiceTimelog> InvoiceTimelogs { get; set; }
    }
}
