
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
    public interface IAccountRepository : IBaseRepository<Account>
    {
        new IQueryable<Account> AllRecords { get; }
        IQueryable<Account> Find(List<Expression<Func<Account, bool>>> filterPredicates);

        Account Find(int accountId);
    }
    #endregion

    #region Class
    public sealed partial class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Account> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<Account> Find(List<Expression<Func<Account, bool>>> filterPredicates)
        {
            var allClaims = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Account, bool>> predicate in filterPredicates)
                {
                    allClaims = allClaims.Where(predicate);
                }
            }

            return allClaims;
        }

        public Account Find(int accountId)
        {
            return this.AllRecords.Where(o => o.AccountId == accountId).FirstOrDefault();
        }

       

    }
    #endregion
}
