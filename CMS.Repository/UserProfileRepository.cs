using CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    #region Interface
    public interface IUserProfileRepository: IBaseRepository<AspNetRole>
    {
        new IQueryable<AspNetRole> AllRecords { get; }

        AspNetRole Find(string id);
    }
    #endregion

    #region Class
    public sealed partial class UserProfileRepository : BaseRepository<AspNetRole>, IUserProfileRepository
    {
        public UserProfileRepository() : base(new CMSDBContext()) { }

        public new IQueryable<AspNetRole> AllRecords
        {
            get
            {
                return base.AllRecords;
            }
        }
        public AspNetRole Find(string id)
        {
            return this.AllRecords.Where(o => o.Id == id).FirstOrDefault();
        }
    }
    #endregion
}
