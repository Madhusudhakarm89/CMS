namespace ClaimManagementSystem.Models
{
    #region Namspace
    //using CMS.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq;
    #endregion

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            IsApproved = false;
            IsLockedOut = false;
            IsActive = true;
            CreateDate = DateTime.Now;
            LastLoginDate = DateTime.Now;
            LastPasswordChangedDate = DateTime.Now;
            LastLockoutDate = DateTime.Parse("1/1/2016");
            LastModifiedDate = DateTime.Now;
        }

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
        public string PhoneNumber { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public virtual CMS.Entity.Country Country { get; set; }

        public virtual CMS.Entity.Province Province { get; set; }

        public List<string> Errors { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("FirstName", this.FirstName ?? string.Empty));
            userIdentity.AddClaim(new Claim("LastName", this.LastName ?? string.Empty));
            userIdentity.AddClaim(new Claim("Salutation", this.Salutation ?? string.Empty));
            userIdentity.AddClaim(new Claim("UserProfile", this.UserProfile ?? string.Empty));
            userIdentity.AddClaim(new Claim("RoleId", this.Roles.Select(e => e.RoleId).FirstOrDefault() ?? string.Empty));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DBConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}