namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_ClaimDetail")]
    public partial class ClaimDetail
    {
        [Key]
        public int ClaimDetailId { get; set; }

        [ForeignKey("Claim")]
        public int ClaimId { get; set; }

        [StringLength(200)]
        public string PolicyNo { get; set; }

        [StringLength(200)]
        public string ClaimantEmailId { get; set; }

        [StringLength(200)]
        public string LossType { get; set; }

        public decimal ClaimAmount { get; set; }

        [StringLength(200)]
        public string LossAddressStreet { get; set; }

        [StringLength(200)]
        public string LossAddressCity { get; set; }

        public int LossAddressProvinceId { get; set; }

        public int LossAddressCountryId { get; set; }

        [StringLength(30)]
        public string LossAddressPostal { get; set; }

        public DateTime LossDateFrom { get; set; }

        public DateTime LossDateTo { get; set; }

        public DateTime InformedDateFrom { get; set; }

        public DateTime InformedDateTo { get; set; }

        public DateTime FirstContactDate { get; set; }

        public DateTime FirstInspectionDate { get; set; }

        [StringLength(200)]
        public string RCV { get; set; }

        [StringLength(200)]
        public string ACV { get; set; }

        [ForeignKey("AdjusterDetail")]
        [StringLength(128)]
        public string Adjuster { get; set; }

        [StringLength(200)]
        public string ClaimRep { get; set; }

        public string ClaimNote { get; set; }

        public virtual AspNetUser AdjusterDetail { get; set; }

        //public virtual Country Country { get; set; }

        //public virtual Province Province { get; set; }

        public virtual Claim Claim { get; set; }
    }
}
