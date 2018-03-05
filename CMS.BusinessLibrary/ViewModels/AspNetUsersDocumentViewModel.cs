using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary.ViewModels
{
    public class AspNetUsersDocumentViewModel : BaseViewModel
    {
        public int DocumentId { get; set; }
        public string UserId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string FileDisplayName { get; set; }
    }
}
