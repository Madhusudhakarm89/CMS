
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
    public interface ITaxSettingsRepository : IBaseRepository<TaxSetting>
    {
        new IQueryable<TaxSetting> AllRecords { get; }
        IQueryable<TaxSetting> Find(List<Expression<Func<TaxSetting, bool>>> filterPredicates);

        TaxSetting Find(int Id);
    }
    #endregion

    #region Class
    public sealed partial class TaxSettingsRepository : BaseRepository<TaxSetting>, ITaxSettingsRepository
    {
        public TaxSettingsRepository() : base(new CMSDBContext()) { }

        public new IQueryable<TaxSetting> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<TaxSetting> Find(List<Expression<Func<TaxSetting, bool>>> filterPredicates)
        {
            var allTaxSettings = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<TaxSetting, bool>> predicate in filterPredicates)
                {
                    allTaxSettings = allTaxSettings.Where(predicate);
                }
            }

            return allTaxSettings;
        }

        public TaxSetting Find(int Id)
        {
            return this.AllRecords.Where(o => o.Id == Id).FirstOrDefault();
        }

       

    }
    #endregion
}
