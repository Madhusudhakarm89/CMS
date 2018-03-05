
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
    public interface IClaimRepository : IBaseRepository<Claim>
    {
        new IQueryable<Claim> AllRecords { get; }
        IQueryable<Claim> Find(List<Expression<Func<Claim, bool>>> filterPredicates);

        Claim Find(int claimId);
    }
    #endregion

    #region Class
    public class ClaimRepository : BaseRepository<Claim>, IClaimRepository
    {
        public ClaimRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Claim> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<Claim> Find(List<Expression<Func<Claim, bool>>> filterPredicates)
        {
            var allClaims = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Claim, bool>> predicate in filterPredicates)
                {
                    allClaims = allClaims.Where(predicate);
                }
            }

            return allClaims;
        }

        public Claim Find(int claimId)
        {
            return this.AllRecords.Where(o => o.ClaimId == claimId).FirstOrDefault();
        }
        
    }    
    #endregion
}
