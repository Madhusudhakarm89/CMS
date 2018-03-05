
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
    public interface ICountryBusinessLayer
    {
        Task<IEnumerable<CountryViewModel>> GetAllCountries();
        Task<CountryViewModel> Find(int CountryId);
        Task<IEnumerable<CountryViewModel>> Find(List<Expression<Func<Country, bool>>> filterPredicates);
        Task<CountryViewModel> Create(CountryViewModel item);
        Task<bool> Delete(int CountryId);
    }
        
    #endregion

    #region Class
    public sealed partial class CountryBusinessLayer : ICountryBusinessLayer
    {
        private readonly ICountryRepository _CountryRepository;
       

        public CountryBusinessLayer()
        {
            this._CountryRepository = new CountryRepository();
        }

        private ICountryRepository CountryRepository
        {
            get { return this._CountryRepository; }
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllCountries()
        {
            return this.CountryRepository.AllRecords.Select(e =>
                new CountryViewModel
                {
                    CountryId = e.CountryId,
                    CountryName = e.CountryName
                    
                }).OrderBy(e => e.CountryName);
        }

        public async Task<CountryViewModel> Find(int CountryId)
        {
            var CountryInfo = this.CountryRepository.Find(CountryId);

            return new CountryViewModel
            {
                CountryId = CountryInfo.CountryId,
                CountryName = CountryInfo.CountryName
            };
        }

        public async Task<IEnumerable<CountryViewModel>> Find(List<Expression<Func<Country, bool>>> filterPredicates)
        {
            return this.CountryRepository.Find(filterPredicates).Select(e =>
                new CountryViewModel
                {
                    CountryId = e.CountryId,
                    CountryName = e.CountryName
                }).OrderBy(e => e.CountryName);
        }

        public async Task<CountryViewModel> Create(CountryViewModel country)
        {
            Country obj = new Country
            {
                CountryName = country.CountryName,
                CountryId = country.CountryId
            };


            var CountryInfo = this.CountryRepository.Add(obj);
            return new CountryViewModel
            {
                CountryName = CountryInfo.CountryName,
                CountryId = CountryInfo.CountryId
            };
            
            
        }


        public async Task<bool> Delete(int CountryId)
        {
            var CountryDetails = this.CountryRepository.Find(CountryId);

            if (CountryDetails != null)
            {
                
                var deletedCountry = this.CountryRepository.Delete(CountryDetails);

                
            }

            return false;
        }
    }

    #endregion
}
