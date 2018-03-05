namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Mapping_ProvinceTaxRates")]
    public partial class ProviceTaxRatesMapping
    {
        [Key]
        public int TaxRateId { get; set; }

        public int? ProviceId { get; set; }

        public int? TaxCategoryId { get; set; }

        public decimal? Rate { get; set; }

        public virtual Province Province { get; set; }

        public virtual TaxCategory TaxCategory { get; set; }
    }
}
