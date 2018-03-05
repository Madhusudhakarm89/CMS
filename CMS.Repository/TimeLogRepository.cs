
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
    public interface ITimeLogRepository : IBaseRepository<TimeLog>
    {
        new IQueryable<TimeLog> AllRecords { get; }
        IQueryable<TimeLog> Find(List<Expression<Func<TimeLog, bool>>> filterPredicates);

        TimeLog Find(int timelogId);
    }
    #endregion

    #region Class
    public sealed partial class TimeLogRepository : BaseRepository<TimeLog>, ITimeLogRepository
    {
        public TimeLogRepository() : base(new CMSDBContext()) { }

        public new IQueryable<TimeLog> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<TimeLog> Find(List<Expression<Func<TimeLog, bool>>> filterPredicates)
        {
            var allTimeLogs = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<TimeLog, bool>> predicate in filterPredicates)
                {
                    allTimeLogs = allTimeLogs.Where(predicate);
                }
            }

            return allTimeLogs;
        }

        public TimeLog Find(int timelogId)
        {
            return this.AllRecords.Where(o => o.TimeLogId == timelogId).FirstOrDefault();
        }
    }
    #endregion
}
