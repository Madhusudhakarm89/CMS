
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
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        new IQueryable<Invoice> AllRecords { get; }
        IQueryable<Invoice> Find(List<Expression<Func<Invoice, bool>>> filterPredicates);

        Invoice Find(int invoiceId);
    }
    #endregion

    #region Class
    public sealed partial class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Invoice> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<Invoice> Find(List<Expression<Func<Invoice, bool>>> filterPredicates)
        {
            var allInvoice = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Invoice, bool>> predicate in filterPredicates)
                {
                    allInvoice = allInvoice.Where(predicate);
                }
            }

            return allInvoice;
        }

        public Invoice Find(int invoiceId)
        {
            return this.AllRecords.Where(o => o.InvoiceId == invoiceId).FirstOrDefault();
        }
    }
    #endregion
}
