
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class ClaimDocumentViewModel : BaseViewModel
    {
        public int DocumentId { get; set; }
        public int ClaimId {get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string FileDisplayName { get; set; }
    }
}
