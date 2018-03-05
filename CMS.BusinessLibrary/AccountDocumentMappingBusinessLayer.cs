
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IAccountDocumentMappingBusinessLayer
    {
        Task<IEnumerable<CompanyDocumentViewModel>> GetAllCompanyDocuments(int accountId);
        Task<CompanyDocumentViewModel> CompanyUpload(CompanyDocumentViewModel obj);
    }

    #endregion

    #region Class
    public sealed partial class AccountDocumnetManageBusinessLayer : IAccountDocumentMappingBusinessLayer
    {
        private readonly IAccountDocumentMappingRepository _accountDocumentMappingRepository;


        public AccountDocumnetManageBusinessLayer()
        {
            this._accountDocumentMappingRepository = new AccountDocumentMappingRepository();
        }

        private IAccountDocumentMappingRepository AccountDocumentMappingRepository
        {
            get { return this._accountDocumentMappingRepository; }
        }

        public async Task<IEnumerable<CompanyDocumentViewModel>> GetAllCompanyDocuments(int accountId)
        {
            try
            {
                return this.AccountDocumentMappingRepository.AllRecords.Select(e =>
                    new CompanyDocumentViewModel
                    {
                        AccountId = e.AccountId,
                        FileName = e.FileName,
                        DocumentId = e.DocumentId
                    }).Where(e => e.AccountId == accountId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<CompanyDocumentViewModel> CompanyUpload(CompanyDocumentViewModel accountDocumentMapping)
        {

            AccountDocumentMapping obj = new AccountDocumentMapping
            {
                DocumentId = accountDocumentMapping.DocumentId,
                AccountId = accountDocumentMapping.AccountId,
                FileName = accountDocumentMapping.FileName

            };

            var companyInfo = this.AccountDocumentMappingRepository.Add(obj);
            return new CompanyDocumentViewModel
            {
                DocumentId = companyInfo.DocumentId,
                AccountId = companyInfo.AccountId,
                FileName = companyInfo.FileName


            };

        }


    }

    #endregion
}
