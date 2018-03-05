
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class CompanyDocumentViewModel
    {
        public string DocumentId { get; set; }
        public int AccountId { get; set; }
        public string FileName { get; set; }
        //public string FileLocation { get; set; }
    }
}
