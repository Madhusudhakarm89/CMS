namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_ClaimServiceTimeLog")]
    public partial class ClaimServiceTimeLog
    {
        [Key]
        public int TimeLogId { get; set; }

        public int? ClaimId { get; set; }

        public int? ServiceItemId { get; set; }

        public DateTime? ServiceStartedDate { get; set; }

        public DateTime? ServiceCompletionDate { get; set; }

        public decimal? TotalTimeSpent { get; set; }

        public virtual ServiceItem ServiceItem { get; set; }

        public virtual Claim Claim { get; set; }
    }
}
