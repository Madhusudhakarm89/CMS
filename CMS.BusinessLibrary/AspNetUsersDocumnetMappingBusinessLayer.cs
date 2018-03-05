
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using CMS.Utilities.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IAspNetUsersDocumnetMappingBusinessLayer
    {
        Task<IEnumerable<AspNetUsersDocumentViewModel>> GetAllAspNetUsersDocuments(string userId, UploadFileType fileType);
        Task<AspNetUsersDocumentViewModel> Find(int documentId);
        Task<IEnumerable<AspNetUsersDocumentViewModel>> Find(List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates);
        Task<AspNetUsersDocumentViewModel> Create(AspNetUsersDocumentViewModel viewModel);
        Task<AspNetUsersDocumentViewModel> Update(AspNetUsersDocumentViewModel viewModel);

        Task<bool> Delete(int documentId);
        Task<AspNetUsersDocumentViewModel> GetAllAspNetUsersImage(string userId, UploadFileType fileType);
    }

    #endregion

    #region Class
    public sealed partial class AspNetUsersDocumnetMappingBusinessLayer : IAspNetUsersDocumnetMappingBusinessLayer
    {
        private readonly IAspNetUserDocumentMappingRepository _aspNetUserDocumentRepository;

        public AspNetUsersDocumnetMappingBusinessLayer()
        {
            this._aspNetUserDocumentRepository = new AspNetUserDocumentMappingRepository();
        }

        private IAspNetUserDocumentMappingRepository AspNetUsersDocumentRepository
        {
            get { return this._aspNetUserDocumentRepository; }
        }

        public async Task<IEnumerable<AspNetUsersDocumentViewModel>> GetAllAspNetUsersDocuments(string userId, UploadFileType fileType)
        {
            List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates = new List<Expression<Func<AspNetUsersDocumentMapping, bool>>>();
            filterPredicates.Add(p => p.UserId == userId);
            filterPredicates.Add(p => p.FileType == fileType.ToString());

            return EntityToViewModelMapper.Map(this.AspNetUsersDocumentRepository.Find(filterPredicates));
        }

        public async Task<AspNetUsersDocumentViewModel> GetAllAspNetUsersImage(string userId, UploadFileType fileType)
        {
            List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates = new List<Expression<Func<AspNetUsersDocumentMapping, bool>>>();
            filterPredicates.Add(p => p.UserId == userId);
            filterPredicates.Add(p => p.FileType == fileType.ToString());

            return EntityToViewModelMapper.MapImage(this.AspNetUsersDocumentRepository.Find(filterPredicates));
        }

        public async Task<AspNetUsersDocumentViewModel> Find(int documentId)
        {
            return EntityToViewModelMapper.Map(this.AspNetUsersDocumentRepository.Find(documentId));
        }

        public async Task<IEnumerable<AspNetUsersDocumentViewModel>> Find(List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.AspNetUsersDocumentRepository.Find(filterPredicates));
        }

        public async Task<AspNetUsersDocumentViewModel> Create(AspNetUsersDocumentViewModel documentViewModel)
        {
            AspNetUsersDocumentMapping aspNetUserDocument = ViewModelToEntityMapper.Map(documentViewModel);

            if (aspNetUserDocument != null)
            {
                aspNetUserDocument = this.AspNetUsersDocumentRepository.Add(aspNetUserDocument);
                if (aspNetUserDocument.DocumentId > 0)
                {
                    documentViewModel.DocumentId = aspNetUserDocument.DocumentId;
                }
                else
                {
                    documentViewModel.HasError = true;
                }
            }

            return documentViewModel;
        }

        public async Task<AspNetUsersDocumentViewModel> Update(AspNetUsersDocumentViewModel documentViewModel)
        {
            var aspNetUserDocument = this.AspNetUsersDocumentRepository.Find(documentViewModel.DocumentId);
            if (aspNetUserDocument != null)
            {
                var lastModifiedDate = aspNetUserDocument.LastModifiedOn;
                aspNetUserDocument = this.AspNetUsersDocumentRepository.Update(ViewModelToEntityMapper.Map(documentViewModel, aspNetUserDocument));

                if (lastModifiedDate < aspNetUserDocument.LastModifiedOn)
                {
                    return documentViewModel;
                }
                else
                {
                    documentViewModel.HasError = true;
                }
            }

            return documentViewModel;
        }


        public async Task<bool> Delete(int documentId)
        {
            var claimDocument = this.AspNetUsersDocumentRepository.Find(documentId);

            if (claimDocument != null)
            {
                claimDocument.IsActive = false;
                var deletedClaimDocument = this.AspNetUsersDocumentRepository.Delete(claimDocument);

                if (!deletedClaimDocument.IsActive)
                    return true;
            }

            return false;
        }

    }

    #endregion
}
