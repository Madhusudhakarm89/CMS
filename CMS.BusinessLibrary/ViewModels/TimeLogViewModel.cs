
namespace CMS.BusinessLibrary.ViewModels
{
    public sealed class TimeLogViewModel : BaseViewModel
    {
        public int TimeLogId { get; set; }
        public int ClaimId { get; set; }
        public string FileNo { get; set; }
        public string ClaimNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int ServiceItemId { get; set; }
        public string ServiceItemName { get; set; }
        public decimal ServiceRate { get; set; }
        public string HoursSpent { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Comment { get; set; }
        public string TaskDate { get; set; }
        public string LoggedOn { get; set; }
        public string AdjusterId { get; set; }
        public string AdjusterName { get; set; }
        public decimal TotalAmount { get; set; }
        public int InvoiceTimelogId { get; set; }
        public UserViewModel Adjuster { get; set; }

        public bool IsBilled { get; set; }  
       
    }
}
