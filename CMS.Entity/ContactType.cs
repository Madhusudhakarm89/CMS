
namespace CMS.Entity
{
    #region Namespace
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion

    [Table("tm_ContactType")]
    public partial class ContactType
    {
        [Key]
        public int ContactTypeId { get; set; }

        public string ContactTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
