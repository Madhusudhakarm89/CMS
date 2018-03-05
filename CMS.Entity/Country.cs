namespace CMS.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_Country")]
    public partial class Country
    {
        public Country()
        {
            Provinces = new HashSet<Province>();
        }

        [Key]
        public int CountryId { get; set; }

        [StringLength(200)]
        public string CountryName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
