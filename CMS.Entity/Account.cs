namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Account")]
    public partial class Account
    {
        public Account()
        {
            AccountDocumentMapping = new HashSet<AccountDocumentMapping>();
            AccountUserGroupMapping = new HashSet<AccountUserGroupMapping>();
        }

        [Key]
        public int AccountId { get; set; }

    

        [StringLength(200)]
        public string CompanyName { get; set; }



        [StringLength(200)]
        public string Type { get; set; }

       

        [StringLength(200)]
        public string EmailId { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public string DefaultAdjuster { get; set; }

        public int? KeyContact { get; set; }

        public string Status { get; set; }
        public bool IsActive { get; set; }

        [StringLength(30)]
        public string Postal { get; set; }
          [StringLength(30)]
        public string AlternatePhone { get; set; }
         [StringLength(30)]
          public string Extension { get; set; }
         [StringLength(30)]
         public string Unit { get; set; }
         [StringLength(30)]
         public string Fax { get; set; }

        public virtual Country Country { get; set; }

        public virtual Province Province { get; set; }


        public virtual ICollection<AccountDocumentMapping> AccountDocumentMapping { get; set; }

        public virtual ICollection<AccountUserGroupMapping> AccountUserGroupMapping { get; set; }
    }
}
