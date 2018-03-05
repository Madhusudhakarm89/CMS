namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_TaxCategory")]
    public partial class TaxCategory
    {
        public TaxCategory()
        {
            ProviceTaxRatesMapping = new HashSet<ProviceTaxRatesMapping>();
        }

        [Key]
        public int TaxCategoryId { get; set; }

        [StringLength(50)]
        public string TaxName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<ProviceTaxRatesMapping> ProviceTaxRatesMapping { get; set; }
    }
}
