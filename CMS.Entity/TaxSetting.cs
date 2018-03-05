namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_TaxSetting")]
    public partial class TaxSetting
    {
        [Key]
        public int Id { get; set; }
        public decimal TaxRate { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [ForeignKey("State")]
        public int ProvinceId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Country Country { get; set; }
        public virtual Province State { get; set; }
    }
}
