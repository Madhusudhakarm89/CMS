
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
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        new IQueryable<Province> AllRecords { get; }
        IQueryable<Province> Find(List<Expression<Func<Province, bool>>> filterPredicates);

        Province Find(int provinceId);
    }
    #endregion

    #region Class
    public sealed partial class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Province> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<Province> Find(List<Expression<Func<Province, bool>>> filterPredicates)
        {
            var allProvinces = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Province, bool>> predicate in filterPredicates)
                {
                    allProvinces = allProvinces.Where(predicate);
                }
            }

            return allProvinces;
        }

        public Province Find(int provinceId)
        {
            return this.AllRecords.Where(o => o.ProvinceId == provinceId).FirstOrDefault();
        }
    }
    #endregion
}
