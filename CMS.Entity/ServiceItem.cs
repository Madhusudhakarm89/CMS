namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_ServiceItem")]
    public partial class ServiceItem
    {
        [Key]
        public int ServiceItemId { get; set; }

        [StringLength(500)]
        public string ServiceItemName { get; set; }
        [StringLength(2000)]
        public string ServiceItemDescription { get; set; }
        public bool IsHourBased { get; set; }
        public decimal DefaultQuantity { get; set; }
        public decimal DefaultFee { get; set; }
        public decimal MinimumFee { get; set; }
        [ForeignKey("Category")]
        public int ServiceCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ServiceCategory Category { get; set; }
    }
}
