
namespace CMS.BusinessLibrary.ViewModels
{
    public sealed class TypeOfLossViewModel : BaseViewModel
    {
        public int LossTypeId { get; set; }
        public string LossTypeName { get; set; }
        public int SortOrder { get; set; }
    }
}