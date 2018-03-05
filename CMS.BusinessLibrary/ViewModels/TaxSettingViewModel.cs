
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class TaxSettingViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public decimal TaxRate { get; set; }

        public int CountryId { get; set; }

        public int ProvinceId { get; set; }

        public string CountryName { get; set; }

        public string StateName { get; set; }
    }
}
