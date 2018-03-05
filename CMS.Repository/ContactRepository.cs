
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
    public interface IContactRepository : IBaseRepository<Contact>
    {
        new IQueryable<Contact> AllRecords { get; }
        IQueryable<Contact> Find(List<Expression<Func<Contact, bool>>> filterPredicates);

        Contact Find(int contactId);
    }
    #endregion

    #region Class
    public sealed partial class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Contact> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<Contact> Find(List<Expression<Func<Contact, bool>>> filterPredicates)
        {
            var allContacts = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Contact, bool>> predicate in filterPredicates)
                {
                    allContacts = allContacts.Where(predicate);
                }
            }

            return allContacts;
        }

        public Contact Find(int contactId)
        {
            return this.AllRecords.Where(o => o.ContactId == contactId).FirstOrDefault();
        }
    }
    #endregion
}
