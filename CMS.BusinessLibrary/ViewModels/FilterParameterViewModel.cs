
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    public partial class FilterParameterViewModel
    {
        public int[] ClaimId { get; set; }
        public int[] CompanyId { get; set; }
        public int[] ContactId { get; set; }

        public string CompanyName { get; set; }
        public string ContactName { get; set; }

        public string SearchKeyword { get; set; }
    }
}
