
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
    public interface IServiceItemRepository : IBaseRepository<ServiceItem>
    {
        new IQueryable<ServiceItem> AllRecords { get; }
        IQueryable<ServiceItem> Find(List<Expression<Func<ServiceItem, bool>>> filterPredicates);

        ServiceItem Find(int ServiceItemId);
    }
    #endregion

    #region Class
    public sealed partial class ServiceItemRepository : BaseRepository<ServiceItem>, IServiceItemRepository
    {
        public ServiceItemRepository() : base(new CMSDBContext()) { }

        public new IQueryable<ServiceItem> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<ServiceItem> Find(List<Expression<Func<ServiceItem, bool>>> filterPredicates)
        {
            var allServiceItems = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<ServiceItem, bool>> predicate in filterPredicates)
                {
                    allServiceItems = allServiceItems.Where(predicate);
                }
            }

            return allServiceItems;
        }

        public ServiceItem Find(int serviceItemId)
        {
            return this.AllRecords.Where(o => o.ServiceItemId == serviceItemId).FirstOrDefault();
        }
    }
    #endregion
}
