
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class ContactViewModel : BaseViewModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public bool IsKeyContact { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string ContactName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
