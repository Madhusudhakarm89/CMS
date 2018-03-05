namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUsers")]
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            //UserClaims = new HashSet<AspNetUserClaim>();
            UserLogins = new HashSet<AspNetUserLogin>();
            //UserContactDetails = new HashSet<UserContactDetails>();
            //Alerts = new HashSet<Alert>();
            //ClaimDetails = new HashSet<ClaimDetail>();
            //Reminders = new HashSet<Reminder>();
            //UserGroups = new HashSet<UserGroup>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime LastLockoutDate { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }


        public string Street { get; set; }
        public string City { get; set; }

        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        public string PostalCode { get; set; }
        public string Department { get; set; }
        public string UserType { get; set; }
        public string UserProfile { get; set; }

        public string CompanyName { get; set; }
        public string Status { get; set; }
        public bool IsReceiveAlerts { get; set; }
        public string CellNumber { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public virtual CMS.Entity.Country Country { get; set; }

        public virtual CMS.Entity.Province Province { get; set; }


        //public virtual ICollection<AspNetUserClaim> UserClaims { get; set; }

        public virtual ICollection<AspNetUserLogin> UserLogins { get; set; }

        //public virtual UserContactDetails UserContactDetail { get; set; }

        //public virtual ICollection<Alert> Alerts { get; set; }

        //public virtual ICollection<ClaimDetail> ClaimDetails { get; set; }

        //public virtual ICollection<Reminder> Reminders { get; set; }

        //public virtual ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
