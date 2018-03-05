
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    #endregion

    public sealed class ClaimStatusViewModel : BaseViewModel
    {
        public int ClaimStatusId { get; set; }
        public string Status { get; set; }
        public int SortOrder { get; set; }
    }
}
