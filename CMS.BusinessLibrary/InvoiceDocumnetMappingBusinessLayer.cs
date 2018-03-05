
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
    public interface IInvoiceDocumnetMappingBusinessLayer
    {
        Task<IEnumerable<InvoiceDocumentViewModel>> GetAllGeneratedInvoices(string userId, int claimId, string applicationUrl, string claimNo);
        InvoiceDocumentViewModel Find(int documentId);
        IEnumerable<InvoiceDocumentViewModel> Find(List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates, string applicationUrl,string claimNo);
        InvoiceDocumentViewModel Create(InvoiceDocumentViewModel viewModel);
        InvoiceDocumentViewModel Update(InvoiceDocumentViewModel viewModel);

        bool Delete(int documentId);
        InvoiceDocumentViewModel GetAllAspNetUsersImage(string userId, UploadFileType fileType);
    }

    #endregion

    #region Class
    public sealed partial class InvoiceDocumnetMappingBusinessLayer : IInvoiceDocumnetMappingBusinessLayer
    {
        private readonly IInvoiceDocumentMappingRepository _invoiceDocumentRepository;

        public InvoiceDocumnetMappingBusinessLayer()
        {
            this._invoiceDocumentRepository = new InvoiceDocumentMappingRepository();
        }

        private IInvoiceDocumentMappingRepository InvoiceDocumentRepository
        {
            get { return this._invoiceDocumentRepository; }
        }

        public async Task<IEnumerable<InvoiceDocumentViewModel>> GetAllGeneratedInvoices(string userId, int claimId, string applicationUrl, string claimNo)
        {
            List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates = new List<Expression<Func<InvoiceDocumentMapping, bool>>>();
            filterPredicates.Add(p => p.UserId == userId);
            filterPredicates.Add(p => p.ClaimId == claimId);

            return EntityToViewModelMapper.Map(this.InvoiceDocumentRepository.Find(filterPredicates), applicationUrl,claimNo);
        }

        public InvoiceDocumentViewModel GetAllAspNetUsersImage(string userId, UploadFileType fileType)
        {
            List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates = new List<Expression<Func<InvoiceDocumentMapping, bool>>>();
            filterPredicates.Add(p => p.UserId == userId);
            filterPredicates.Add(p => p.FileType == fileType.ToString());

            return EntityToViewModelMapper.MapImage(this.InvoiceDocumentRepository.Find(filterPredicates));
        }

        public InvoiceDocumentViewModel Find(int documentId)
        {
            return EntityToViewModelMapper.Map(this.InvoiceDocumentRepository.Find(documentId));
        }

        public IEnumerable<InvoiceDocumentViewModel> Find(List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates, string applicationUrl,string claimNo)
        {
            return EntityToViewModelMapper.Map(this.InvoiceDocumentRepository.Find(filterPredicates), applicationUrl,claimNo);
        }

        public InvoiceDocumentViewModel Create(InvoiceDocumentViewModel documentViewModel)
        {
            InvoiceDocumentMapping invoiceDocument = ViewModelToEntityMapper.Map(documentViewModel);

            if (invoiceDocument != null)
            {
                invoiceDocument = this.InvoiceDocumentRepository.Add(invoiceDocument);
                if (invoiceDocument.DocumentId > 0)
                {
                    documentViewModel.DocumentId = invoiceDocument.DocumentId;
                }
                else
                {
                    documentViewModel.HasError = true;
                }
            }

            return documentViewModel;
        }

        public InvoiceDocumentViewModel Update(InvoiceDocumentViewModel documentViewModel)
        {
            var invoiceDocument = this.InvoiceDocumentRepository.Find(documentViewModel.DocumentId);
            if (invoiceDocument != null)
            {
                var lastModifiedDate = invoiceDocument.LastModifiedOn;
                invoiceDocument = this.InvoiceDocumentRepository.Update(ViewModelToEntityMapper.Map(documentViewModel, invoiceDocument));

                if (lastModifiedDate < invoiceDocument.LastModifiedOn)
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


        public bool Delete(int documentId)
        {
            var claimDocument = this.InvoiceDocumentRepository.Find(documentId);

            if (claimDocument != null)
            {
                claimDocument.IsActive = false;
                var deletedClaimDocument = this.InvoiceDocumentRepository.Delete(claimDocument);

                if (!deletedClaimDocument.IsActive)
                    return true;
            }

            return false;
        }

    }

    #endregion
}
