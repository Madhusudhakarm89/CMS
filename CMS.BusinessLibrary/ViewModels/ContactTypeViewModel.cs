using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary.ViewModels
{
    public class ContactTypeViewModel : BaseViewModel
    {
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
