
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
    public interface IClaimDocumentMappingBusinessLayer
    {
        Task<IEnumerable<ClaimDocumentViewModel>> GetAllClaimDocuments(string userId, int claimId, UploadFileType fileType);
        Task<ClaimDocumentViewModel> Find(int documentId);
        Task<IEnumerable<ClaimDocumentViewModel>> Find(List<Expression<Func<ClaimDocumentMapping, bool>>> filterPredicates);
        Task<ClaimDocumentViewModel> Create(ClaimDocumentViewModel viewModel);
        Task<ClaimDocumentViewModel> Update(ClaimDocumentViewModel viewModel);

        Task<bool> Delete(int documentId);
    }

    #endregion

    #region Class
    public sealed partial class ClaimDocumnetMappingBusinessLayer : IClaimDocumentMappingBusinessLayer
    {
        private readonly IClaimDocumentMappingRepository _claimDocumentRepository;

        public ClaimDocumnetMappingBusinessLayer()
        {
            this._claimDocumentRepository = new ClaimDocumentMappingRepository();
        }

        private IClaimDocumentMappingRepository ClaimDocumentRepository
        {
            get { return this._claimDocumentRepository; }
        }

        public async Task<IEnumerable<ClaimDocumentViewModel>> GetAllClaimDocuments(string userId, int claimId, UploadFileType fileType)
        {
            List<Expression<Func<ClaimDocumentMapping, bool>>> filterPredicates = new List<Expression<Func<ClaimDocumentMapping, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);
            filterPredicates.Add(p => p.FileType == fileType.ToString());

            return EntityToViewModelMapper.Map(this.ClaimDocumentRepository.Find(filterPredicates));
        }

        public async Task<ClaimDocumentViewModel> Find(int documentId)
        {
            return EntityToViewModelMapper.Map(this.ClaimDocumentRepository.Find(documentId));
        }

        public async Task<IEnumerable<ClaimDocumentViewModel>> Find(List<Expression<Func<ClaimDocumentMapping, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.ClaimDocumentRepository.Find(filterPredicates));
        }

        public async Task<ClaimDocumentViewModel> Create(ClaimDocumentViewModel documentViewModel)
        {
            ClaimDocumentMapping claimDocument = ViewModelToEntityMapper.Map(documentViewModel);

            if (claimDocument != null)
            {
                claimDocument = this.ClaimDocumentRepository.Add(claimDocument);
                if (claimDocument.DocumentId > 0)
                {
                    documentViewModel.DocumentId = claimDocument.DocumentId;
                }
                else
                {
                    documentViewModel.HasError = true;
                }
            }

            return documentViewModel;
        }

        public async Task<ClaimDocumentViewModel> Update(ClaimDocumentViewModel documentViewModel)
        {
            var claimDocument = this.ClaimDocumentRepository.Find(documentViewModel.DocumentId);
            if (claimDocument != null)
            {
                var lastModifiedDate = claimDocument.LastModifiedOn;
                claimDocument = this.ClaimDocumentRepository.Update(ViewModelToEntityMapper.Map(documentViewModel, claimDocument));

                if (lastModifiedDate < claimDocument.LastModifiedOn)
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
            var claimDocument = this.ClaimDocumentRepository.Find(documentId);

            if (claimDocument != null)
            {
                claimDocument.IsActive = false;
                var deletedClaimDocument = this.ClaimDocumentRepository.Delete(claimDocument);

                if (!deletedClaimDocument.IsActive)
                    return true;
            }

            return false;
        }
    }

    #endregion
}
