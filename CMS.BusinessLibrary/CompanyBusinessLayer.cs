
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
    public interface ICompanyBusinessLayer
    {
        Task<IEnumerable<CompanyViewModel>> GetAllCompanies(string userId);
        Task<CompanyViewModel> Find(int companyId);
        Task<IEnumerable<CompanyViewModel>> Find(List<Expression<Func<Account, bool>>> filterPredicates);
        Task<CompanyViewModel> Create(CompanyViewModel item);
        Task<CompanyViewModel> Update(CompanyViewModel item);

        Task<bool> Delete(int companyId);
    }

    #endregion

    #region Class
    public sealed partial class CompanyBusinessLayer : ICompanyBusinessLayer
    {
        private readonly IAccountRepository _companyRepository;


        public CompanyBusinessLayer()
        {
            this._companyRepository = new AccountRepository();
        }

        private IAccountRepository CompanyRepository
        {
            get { return this._companyRepository; }
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAllCompanies(string userId)
        {
            return this.CompanyRepository.AllRecords.Select(e =>
                new CompanyViewModel
                {
                    CompanyId = e.AccountId,
                    CompanyName = e.CompanyName,
                    Phone = e.Phone,
                    Type = e.Type,
                    AlternatePhone = e.AlternatePhone,
                    ContactEmailId = e.EmailId,
                    KeyContact = e.KeyContact ?? 0


                }).OrderBy(e => e.CompanyName);
        }

        public async Task<CompanyViewModel> Find(int companyId)
        {
            var companyInfo = this.CompanyRepository.Find(companyId);

            return new CompanyViewModel
            {
                CompanyId = companyInfo.AccountId,
                CompanyName = companyInfo.CompanyName,
                Type = companyInfo.Type,
                ContactEmailId = companyInfo.EmailId,
                Phone = companyInfo.Phone,
                AlternatePhone = companyInfo.AlternatePhone,
                Fax = companyInfo.Fax,
                Extension = companyInfo.Extension,
                DefaultAdjuster = companyInfo.DefaultAdjuster,
                KeyContact = companyInfo.KeyContact ?? 0,
                Unit = companyInfo.Unit,
                Street = companyInfo.Street,
                City = companyInfo.City,
                ProvinceId = companyInfo.ProvinceId,
                ProvinceName = companyInfo.Province.ProvinceName,
                CountryId = companyInfo.CountryId,
                CountryName = companyInfo.Country.CountryName,
                Postal = companyInfo.Postal,
                Status = companyInfo.Status
            };
        }

        public async Task<IEnumerable<CompanyViewModel>> Find(List<Expression<Func<Account, bool>>> filterPredicates)
        {
            return this.CompanyRepository.Find(filterPredicates).Select(e =>
                new CompanyViewModel
                {
                    CompanyId = e.AccountId,
                    CompanyName = e.CompanyName,
                    Type = e.Type,
                    ContactEmailId = e.EmailId,
                    Phone = e.Phone,
                    AlternatePhone = e.AlternatePhone,
                    Fax = e.Fax,
                    Extension = e.Extension,
                    DefaultAdjuster = e.DefaultAdjuster,
                    KeyContact = e.KeyContact ?? 0,
                    Unit = e.Unit,
                    Street = e.Street,
                    City = e.City,
                    ProvinceId = e.ProvinceId,
                    ProvinceName = e.Province.ProvinceName,
                    CountryId = e.CountryId,
                    CountryName = e.Country.CountryName,
                    Postal = e.Postal,
                    Status = e.Status
                }).OrderBy(e => e.CompanyName);
        }



        public async Task<CompanyViewModel> Create(CompanyViewModel account)
        {
            Account obj = new Account
            {
                CompanyName = account.CompanyName,
                DefaultAdjuster = account.DefaultAdjuster,
                City = account.City,
                CountryId = account.CountryId,
                KeyContact = account.KeyContact,
                ProvinceId = account.ProvinceId,
                Phone = account.Phone,
                Street = account.Street,
                EmailId = account.ContactEmailId,
                Extension = account.Extension,
                Postal = account.Postal,
                Type = account.Type,
                Unit = account.Unit,
                Fax = account.Fax,
                AlternatePhone = account.AlternatePhone,
                Status = account.Status,
                IsActive = true
            };


            var companyInfo = this.CompanyRepository.Add(obj);
            return new CompanyViewModel
            {
                CompanyId = companyInfo.AccountId,
                CompanyName = companyInfo.CompanyName,
                Type = companyInfo.Type,
                ContactEmailId = companyInfo.EmailId,
                Phone = companyInfo.Phone,
                AlternatePhone = companyInfo.AlternatePhone,
                Fax = companyInfo.Fax,
                Extension = companyInfo.Extension,
                DefaultAdjuster = companyInfo.DefaultAdjuster,
                KeyContact = companyInfo.KeyContact ?? 0,
                Unit = companyInfo.Unit,
                Street = companyInfo.Street,
                City = companyInfo.City,
                ProvinceId = companyInfo.ProvinceId,
                //ProvinceName = companyInfo.Province.ProvinceName,
                CountryId = companyInfo.CountryId,
                //CountryName = companyInfo.Country.CountryName,
                Postal = companyInfo.Postal,
                Status = companyInfo.Status
            };


        }

        public async Task<CompanyViewModel> Update(CompanyViewModel account)
        {
            var companyInfo = this.CompanyRepository.Find(account.CompanyId);
            if (companyInfo != null)
            {

                companyInfo.CompanyName = account.CompanyName;
                companyInfo.DefaultAdjuster = account.DefaultAdjuster;
                companyInfo.City = account.City;
                companyInfo.CountryId = account.CountryId;
                companyInfo.KeyContact = account.KeyContact;
                companyInfo.ProvinceId = account.ProvinceId;
                companyInfo.Phone = account.Phone;
                companyInfo.Street = account.Street;
                companyInfo.EmailId = account.ContactEmailId;
                companyInfo.Extension = account.Extension;
                companyInfo.Postal = account.Postal;
                companyInfo.Type = account.Type;
                companyInfo.Unit = account.Unit;
                companyInfo.Fax = account.Fax;
                companyInfo.AlternatePhone = account.AlternatePhone;
                companyInfo.Status = account.Status;

                companyInfo = this.CompanyRepository.Update(companyInfo);

                return new CompanyViewModel
                {
                    CompanyId = companyInfo.AccountId,
                    CompanyName = companyInfo.CompanyName,
                    Type = companyInfo.Type,
                    ContactEmailId = companyInfo.EmailId,
                    Phone = companyInfo.Phone,
                    AlternatePhone = companyInfo.AlternatePhone,
                    Fax = companyInfo.Fax,
                    Extension = companyInfo.Extension,
                    DefaultAdjuster = companyInfo.DefaultAdjuster,
                    KeyContact = companyInfo.KeyContact ?? 0,
                    Unit = companyInfo.Unit,
                    Street = companyInfo.Street,
                    City = companyInfo.City,
                    ProvinceId = companyInfo.ProvinceId,
                    //ProvinceName = companyInfo.Province.ProvinceName,
                    CountryId = companyInfo.CountryId,
                    //CountryName = companyInfo.Country.CountryName,
                    Postal = companyInfo.Postal,
                    Status = companyInfo.Status
                };
            }

            return account;
        }


        public async Task<bool> Delete(int companyId)
        {
            var companyDetails = this.CompanyRepository.Find(companyId);

            if (companyDetails != null)
            {
                companyDetails.IsActive = false;
                var deletedCompany = this.CompanyRepository.Delete(companyDetails);

                if (!deletedCompany.IsActive)
                    return true;
            }

            return false;
        }
    }

    #endregion
}
