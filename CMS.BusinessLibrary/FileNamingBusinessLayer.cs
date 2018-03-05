
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IFileNamingBusinessLayer
    {
        Task<IEnumerable<FileNameViewModel>> GetAllFileNamingCode();
        Task<FileNameViewModel> Find(int fileNameId);
        Task<IEnumerable<FileNameViewModel>> Find(FileNameViewModel viewModel);
        Task<FileNameViewModel> Create(FileNameViewModel viewModel);
        Task<FileNameViewModel> Update(FileNameViewModel viewModel);

        Task<bool> Delete(int fileNameId);
    }

    #endregion

    #region Class
    public sealed partial class FileNamingBusinessLayer : IFileNamingBusinessLayer
    {
        private readonly IFileNamingRepository _fileNameRepository;


        public FileNamingBusinessLayer()
        {
            this._fileNameRepository = new FileNamingRepository();
        }

        private IFileNamingRepository FileNameRepository
        {
            get { return this._fileNameRepository; }
        }

        public async Task<IEnumerable<FileNameViewModel>> GetAllFileNamingCode()
        {
            return EntityToViewModelMapper.Map(this.FileNameRepository.AllRecords);
        }

        public async Task<FileNameViewModel> Find(int fileNameId)
        {
            return EntityToViewModelMapper.Map(this.FileNameRepository.Find(fileNameId));
        }

        public async Task<IEnumerable<FileNameViewModel>> Find(FileNameViewModel viewModel)
        {
            List<Expression<Func<FileNameCode, bool>>> filterPredicate = new List<Expression<Func<FileNameCode, bool>>>();
            if (viewModel != null)
            {
                filterPredicate.Add(p => (!string.IsNullOrWhiteSpace(p.LocationName) && p.LocationName.Contains(viewModel.LocationName))
                                            || (!string.IsNullOrWhiteSpace(p.FileNumberPrefix) && p.FileNumberPrefix.Contains(viewModel.FileNumberPrefix)));
            }
            return EntityToViewModelMapper.Map(this.FileNameRepository.Find(filterPredicate));
        }

        public async Task<FileNameViewModel> Create(FileNameViewModel viewModel)
        {
            var fileNamingDetails = this.FileNameRepository.Add(ViewModelToEntityMapper.Map(viewModel));
            if (fileNamingDetails.Id > 0)
            {
                viewModel.FileNameId = fileNamingDetails.Id;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<FileNameViewModel> Update(FileNameViewModel viewModel)
        {
            var fileNameInfo = this.FileNameRepository.Find(viewModel.FileNameId);
            if (fileNameInfo != null)
            {
                var lastModifiedDate = fileNameInfo.LastModifiedOn;
                fileNameInfo = this.FileNameRepository.Update(ViewModelToEntityMapper.Map(viewModel, fileNameInfo));

                if (lastModifiedDate < fileNameInfo.LastModifiedOn)
                {
                    return viewModel;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<bool> Delete(int fileNameId)
        {
            var fileNameDetails = this.FileNameRepository.Find(fileNameId);

            if (fileNameDetails != null)
            {
                fileNameDetails.IsActive = false;
                var deletedFileName = this.FileNameRepository.Delete(fileNameDetails);

                if (!deletedFileName.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
