namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_ServiceCategory")]
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {

        }

        [Key]
        public int ServiceCategoryId { get; set; }

        [StringLength(200)]
        public string ServiceCategoryName { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
