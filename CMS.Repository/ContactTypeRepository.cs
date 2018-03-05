using CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    #region Interface
    public interface IContactTypeRepository : IBaseRepository<ContactType>
    {
        new IQueryable<ContactType> AllRecords { get; }

        ContactType Find(int contactTypeId);
    }
    #endregion

    #region Class
    public sealed partial class ContactTypeRepository : BaseRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository() : base(new CMSDBContext()) { }

        public new IQueryable<ContactType> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }
        public ContactType Find(int contactTypeId)
        {
            return this.AllRecords.Where(o => o.ContactTypeId == contactTypeId).FirstOrDefault();
        }
    }
    #endregion
}
