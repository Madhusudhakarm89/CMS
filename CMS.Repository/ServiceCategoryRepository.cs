
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
    public interface IServiceCategoryRepository : IBaseRepository<ServiceCategory>
    {
        new IQueryable<ServiceCategory> AllRecords { get; }
        IQueryable<ServiceCategory> Find(List<Expression<Func<ServiceCategory, bool>>> filterPredicates);

        ServiceCategory Find(int ServiceItemId);
    }
    #endregion

    #region Class
    public sealed partial class ServiceCategoryRepository : BaseRepository<ServiceCategory>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository() : base(new CMSDBContext()) { }

        public new IQueryable<ServiceCategory> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<ServiceCategory> Find(List<Expression<Func<ServiceCategory, bool>>> filterPredicates)
        {
            var allServiceCategories = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<ServiceCategory, bool>> predicate in filterPredicates)
                {
                    allServiceCategories = allServiceCategories.Where(predicate);
                }
            }

            return allServiceCategories;
        }

        public ServiceCategory Find(int serviceCategoryId)
        {
            return this.AllRecords.Where(o => o.ServiceCategoryId == serviceCategoryId).FirstOrDefault();
        }
    }
    #endregion
}
