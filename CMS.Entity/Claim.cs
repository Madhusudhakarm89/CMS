namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Claim")]
    public partial class Claim
    {
        public Claim()
        {
            ClaimServiceTimeLogs = new HashSet<ClaimServiceTimeLog>();
            ClaimDocumentMapping = new HashSet<ClaimDocumentMapping>();
            ClaimUserGroupMapping = new HashSet<ClaimUserGroupMapping>();
        }

        [Key]
        public int ClaimId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        [StringLength(200)]
        public string FileNo { get; set; }

        [StringLength(200)]
        public string ClaimNo { get; set; }

        [ForeignKey("Claimant")]
        public int ClaimantId { get; set; }

        [StringLength(200)]
        public string PolicyNo { get; set; }

        public DateTime? PolicyEffectiveDate { get; set; }
        public DateTime? PolicyExpirationDate { get; set; }

        [StringLength(200)]
        public string LoanNo { get; set; }

        [StringLength(200)]
        public string Mortgagee { get; set; }

        public DateTime ReceivedDate { get; set; }
        public DateTime LossDate { get; set; }

        [StringLength(200)]
        public string LossType { get; set; }

        [StringLength(200)]
        public string AdjustmentType { get; set; }

        public bool LossTotalUrgent { get; set; }
        public bool MouldClaim { get; set; }

        [ForeignKey("Manager")]
        public string ManagerId { get; set; }

        [ForeignKey("Adjuster")]
        public string AdjusterId { get; set; }

        [StringLength(200)]
        public string Branch { get; set; }

        [StringLength(2000)]
        public string LossDescription { get; set; }

        [StringLength(2000)]
        public string Instruction { get; set; }

        public string VinNo { get; set; }
        public string Deductible { get; set; }
        public string WindDeductible { get; set; }
        public string Premium { get; set; }
        public string Building { get; set; }
        public string Subrogation { get; set; }
        public string Salvage { get; set; }
        public string BusinessPersonalProperty { get; set; }
        public string Contents { get; set; }
        public string Ale { get; set; }
        public string DetachedPrivateStructures { get; set; }
        public string CondominiumImprovements { get; set; }
        public string IndustrialHygenist { get; set; }
        public decimal? AdjustingFee { get; set; }

        public DateTime FirstContactDate { get; set; }
        public DateTime? InspectionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool AcknowledgementMail { get; set; }
        public bool LossNotice { get; set; }

        [ForeignKey("ClaimStatus")]
        public int ClaimStatusId { get; set; }

        public bool SubscribeToAlert { get; set; }

        public bool VisibleToClient { get; set; }

        public string CreatedBy { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public bool IsActive { get; set; }

        public virtual Account Company { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Contact Claimant { get; set; }

        public virtual AspNetUser Manager { get; set; }
        public virtual AspNetUser Adjuster { get; set; }

        public virtual Status ClaimStatus { get; set; }

        public virtual ICollection<ClaimServiceTimeLog> ClaimServiceTimeLogs { get; set; }

        public virtual ICollection<ClaimDocumentMapping> ClaimDocumentMapping { get; set; }

        public virtual ICollection<ClaimUserGroupMapping> ClaimUserGroupMapping { get; set; }
    }
}
