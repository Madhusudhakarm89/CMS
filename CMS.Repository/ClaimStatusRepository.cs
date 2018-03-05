
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
    public interface IClaimStatusRepository : IBaseRepository<Status>
    {
        new IQueryable<Status> AllRecords { get; }
        IQueryable<Status> Find(List<Expression<Func<Status, bool>>> filterPredicates);

        Status Find(int claimStatusId);
    }
    #endregion

    #region Class
    public class ClaimStatusRepository : BaseRepository<Status>, IClaimStatusRepository
    {
        public ClaimStatusRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Status> AllRecords
        {
            get
            {
                return base.AllRecords.Where(o => o.IsActive == true); 
            }
        }

        public IQueryable<Status> Find(List<Expression<Func<Status, bool>>> filterPredicates)
        {
            var allClaimStatus = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Status, bool>> predicate in filterPredicates)
                {
                    allClaimStatus = allClaimStatus.Where(predicate);
                }
            }

            return allClaimStatus;
        }

        public Status Find(int claimStatusId)
        {
            return this.AllRecords.Where(o => o.Id == claimStatusId).FirstOrDefault();
        }
    }
    #endregion
}
