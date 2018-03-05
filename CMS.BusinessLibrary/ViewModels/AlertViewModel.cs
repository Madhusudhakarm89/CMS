using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary.ViewModels
{
    public class AlertViewModel
    {
        public int AlertId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AlertTo { get; set; }

        public string AlertBy { get; set; }

        public string CreatedOn { get; set; }

        public bool? IsRead { get; set; }

        public string LastModifiedOn { get; set; }

        public string Status { get; set; }

    }
}
