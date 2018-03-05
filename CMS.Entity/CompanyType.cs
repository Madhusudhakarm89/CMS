
namespace CMS.Entity
{
    #region Namespace
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion

    [Table("tm_CompanyType")]
    public partial class CompanyType
    {
        [Key]
        public int CompanyTypeId { get; set; }

        public string CompanyTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
