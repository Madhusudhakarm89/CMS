
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class ServiceItemViewModel : BaseViewModel
    {
        public int ServiceItemId { get; set; }
        public string ServiceItemName { get; set; }
        public string ServiceItemDescription { get; set; }
        public bool IsHourBased { get; set; }
        public decimal DefaultQuantity { get; set; }
        public decimal DefaultFee { get; set; }
        public decimal MinimumFee { get; set; }
        public int ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
    }
}
