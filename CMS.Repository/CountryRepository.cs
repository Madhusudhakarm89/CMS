
namespace CMS.Repository
{
    #region Namespace
    using CMS.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface ICountryRepository : IBaseRepository<Country>
    {
        new IQueryable<Country> AllRecords { get; }
        IQueryable<Country> Find(List<Expression<Func<Country, bool>>> filterPredicates);

        Country Find(int countryId);
    }
    #endregion

    #region Class
    public sealed partial class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Country> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<Country> Find(List<Expression<Func<Country, bool>>> filterPredicates)
        {
            var allCountries = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Country, bool>> predicate in filterPredicates)
                {
                    allCountries = allCountries.Where(predicate);
                }
            }

            return allCountries;
        }

        public Country Find(int countryId)
        {
            return this.AllRecords.Where(o => o.CountryId == countryId).FirstOrDefault();
        }
    }
    #endregion
}
