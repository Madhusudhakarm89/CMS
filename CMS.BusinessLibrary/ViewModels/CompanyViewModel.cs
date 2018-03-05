
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyTypeId { get; set; }
        public string Type { get; set; }
        public string ContactEmailId { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Postal { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string DefaultAdjuster { get; set; }
        public string DefaultAdjusterName { get; set; }
        public int KeyContact { get; set; }
        public string KeyContactName { get; set; }
        public string AlternatePhone { get; set; }
        public string Extension { get; set; }
        public string Fax { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
    }
}
