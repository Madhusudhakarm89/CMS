
namespace CMS.Entity
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_FileNamingCode")]
    public class FileNameCode
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string LocationName { get; set; }
        public string FileNumberPrefix { get; set; }

        [ReadOnly(true)]
        public int SortOrder { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
