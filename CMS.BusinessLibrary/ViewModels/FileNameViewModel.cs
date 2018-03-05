
namespace CMS.BusinessLibrary.ViewModels
{
    public sealed class FileNameViewModel : BaseViewModel
    {
        public int FileNameId { get; set; }
        public string LocationName { get; set; }
        public string FileNumberPrefix { get; set; }
        public int SortOrder { get; set; }
    }
}
