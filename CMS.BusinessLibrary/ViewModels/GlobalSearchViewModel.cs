namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class GlobalSearchViewModel
    {
        public List<CompanyViewModel> CompanyViewModel { get; set; }
        public List<ContactViewModel> ContactViewModel { get; set; }
        public List<ClaimViewModel> ClaimViewModel { get; set; }
    }
}
