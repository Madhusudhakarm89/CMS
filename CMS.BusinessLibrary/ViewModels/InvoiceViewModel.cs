
namespace CMS.BusinessLibrary.ViewModels
{
    #region NameSpaces
    using System.Collections.Generic;
    #endregion

    public sealed class InvoiceViewModel : BaseViewModel
    {
        public int InvoiceId { get; set; }
        public int ClaimId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public string FileNumber { get; set; }
        public string ClaimNumber { get; set; }
        public string PolicyNumber { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int ClaimantId { get; set; }
        public string ClaimantName { get; set; }
        public string AdjusterId { get; set; }
        public string AdjusterName { get; set; }
        public int LossTypeId { get; set; }
        public string LossType { get; set; }
        public string LossAddress { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string LostDate { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string ContactName { get; set; }
        public IList<TimeLogViewModel> TimeLogs { get; set; }  

       
    }
}
