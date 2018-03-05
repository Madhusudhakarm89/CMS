namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_UserContactDetails")]
    public partial class UserContactDetails
    {
        [Key]
        public int UserContactId { get; set; }

        [ForeignKey("User")]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string Province { get; set; }

        [StringLength(200)]
        public string Country { get; set; }

        [StringLength(10)]
        public string Postal { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string Cell { get; set; }

        [StringLength(30)]
        public string Fax { get; set; }

        [StringLength(30)]
        public string TollFree { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
