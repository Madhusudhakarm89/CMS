namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; }
        public string UserTypeName { get; set; }
        public int UserTypeId { get; set; }
        public string ProfileType { get; set; }
        public int ProfileTypeId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public int? CountryId { get; set; }
        public string ProvinceName { get; set; }
        public int? ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string CreatedDate { get; set; }
        public bool ReceiveAlerts { get; set; }
        public int StatusId { get; set; }
        public string LastModifiedDate { get; set; }
        public string FullName 
        {
            get {
                return String.Format("{0}{1}", !string.IsNullOrWhiteSpace(FirstName) ? FirstName.Trim() + " " : string.Empty
                                                    , !string.IsNullOrWhiteSpace(LastName) ? LastName.Trim() : string.Empty);
            }
        }
    }
}
