
namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_TimeLog")]
    public partial class TimeLog
    {
        [Key]
        public int TimeLogId { get; set; }

        [ForeignKey("TimeLogClaim")]
        public int ClaimId { get; set; }


        [ForeignKey("TimeLogServiceItem")]
        public int ServiceItemId { get; set; }

        public string HoursSpent { get; set; }
        public decimal Quantity { get; set; }
        public string Comment { get; set; }

        public DateTime TaskDate { get; set; }
        public DateTime LoggedOn { get; set; }

        [ForeignKey("Adjuster")]
        public string AdjusterId { get; set; }

        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public bool IsActive { get; set; }

        public virtual Claim TimeLogClaim { get; set; }
        public virtual ServiceItem TimeLogServiceItem { get; set; }
        public virtual AspNetUser Adjuster { get; set; }
    }
}
