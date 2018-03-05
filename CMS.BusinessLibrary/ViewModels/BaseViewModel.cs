
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespaces
    using System;
    #endregion

    public class BaseViewModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public bool HasError { get; set; }
        public bool HasErrorOnSave { get; set; }
        public string ErrorMessage { get; set; }
    }
}
