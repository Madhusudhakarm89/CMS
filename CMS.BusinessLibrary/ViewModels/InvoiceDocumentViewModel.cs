using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary.ViewModels
{
    public class InvoiceDocumentViewModel:BaseViewModel
    {
        public int DocumentId { get; set; }
        public string UserId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string FileDisplayName { get; set; }

        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public int ClaimId { get; set; }
    }
}
