
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
    public interface ITimeLogUnitRepository : IBaseRepository<TimeLogUnit>
    {
        new IQueryable<TimeLogUnit> AllRecords { get; }
        IQueryable<TimeLogUnit> Find(List<Expression<Func<TimeLogUnit, bool>>> filterPredicate);

        TimeLogUnit Find(int unitId);
    }
    #endregion

    #region Class
    public sealed partial class TimeLogUnitRepository : BaseRepository<TimeLogUnit>, ITimeLogUnitRepository
    {
        public TimeLogUnitRepository() : base(new CMSDBContext()) { }

        public new IQueryable<TimeLogUnit> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<TimeLogUnit> Find(List<Expression<Func<TimeLogUnit, bool>>> filterPredicate)
        {
            var allTimeLogUnits = this.AllRecords;

            if (filterPredicate != null && filterPredicate.Count > 0)
            {
                foreach (Expression<Func<TimeLogUnit, bool>> predicate in filterPredicate)
                {
                    allTimeLogUnits = allTimeLogUnits.Where(predicate);
                }
            }

            return allTimeLogUnits;
        }

        public TimeLogUnit Find(int unitId)
        {
            return this.AllRecords.Where(o => o.Id == unitId).FirstOrDefault();
        }
    }
    #endregion
}
