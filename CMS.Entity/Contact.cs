namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Contact")]
    public partial class Contact
    {
        public Contact()
        {
            //Claims = new HashSet<Claim>();
            ContactDocumentMapping = new HashSet<ContactDocumentMapping>();
            ContactUserGroupMapping = new HashSet<ContactUserGroupMapping>();
        }

        [Key]
        public int ContactId { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [ForeignKey("ContactType")]
        public int ContactTypeId { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        [StringLength(200)]
        public string EmailId { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string Cell { get; set; }

        public bool IsKeyContact { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [StringLength(50)]
        public string Postal { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public bool IsActive { get; set; }

        public virtual Account Company { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Country Country { get; set; }

        public virtual Province Province { get; set; }

        public virtual AspNetUser Owner { get; set; }

        //public virtual ICollection<Claim> Claims { get; set; }

        public virtual ICollection<ContactDocumentMapping> ContactDocumentMapping { get; set; }

        public virtual ICollection<ContactUserGroupMapping> ContactUserGroupMapping { get; set; }
    }
}
