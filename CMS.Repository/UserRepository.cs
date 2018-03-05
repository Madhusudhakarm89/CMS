
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
    public interface IUserRepository : IBaseRepository<AspNetUser>
    {
        new IQueryable<AspNetUser> AllRecords { get; }
        IQueryable<AspNetUser> Find(List<Expression<Func<AspNetUser, bool>>> filterPredicates);

        AspNetUser Find(string id);
    }
    #endregion

    #region Class
    public sealed partial class UserRepository : BaseRepository<AspNetUser>, IUserRepository
    {
        public UserRepository() : base(new CMSDBContext()) { }

        public new IQueryable<AspNetUser> AllRecords
        {
            get
            {
                return base.AllRecords; 
            }
        }

        public IQueryable<AspNetUser> Find(List<Expression<Func<AspNetUser, bool>>> filterPredicates)
        {
            var allUsers = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<AspNetUser, bool>> predicate in filterPredicates)
                {
                    allUsers = allUsers.Where(predicate);
                }
            }

            return allUsers;
        }

        public AspNetUser Find(string id)
        {
            return this.AllRecords.Where(o => o.Id == id).FirstOrDefault();
        }
    }
    #endregion
}
