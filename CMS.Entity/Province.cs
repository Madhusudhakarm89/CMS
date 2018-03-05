namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_Province")]
    public partial class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        [StringLength(200)]
        public string ProvinceName { get; set; }

        public int CountryId { get; set; }

        public bool IsActive { get; set; }

        public virtual Country Country { get; set; }
    }
}
