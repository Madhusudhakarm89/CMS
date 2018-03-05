
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    #endregion

    public sealed class ClaimNotesViewModel : BaseViewModel
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsTask { get; set; }
        public string CreatedDate { get; set; }
        public string TaskEndDate { get; set; }
        public int ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public string AssignedTo { get; set; }
        public UserViewModel AssignedToUser { get; set; }
        public string  CreatedBy { get; set; }

        public UserViewModel CreatedByUser { get; set; }

        public string CompanyName { get; set; }
        public string ContactName { get; set; }

        public string DueDate { get; set; }
        public int CompanyId { get; set; }
        public int ContactId { get; set; }
        public int  IsDuteDatetask { get; set; }
    }
}
