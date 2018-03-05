
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
    public interface IMapping_InvoiceTimelogRepository : IBaseRepository<Mapping_InvoiceTimelog>
    {
        new IQueryable<Mapping_InvoiceTimelog> AllRecords { get; }
        IQueryable<Mapping_InvoiceTimelog> Find(List<Expression<Func<Mapping_InvoiceTimelog, bool>>> filterPredicates);

        Mapping_InvoiceTimelog Find(int mappingId);
    }
    #endregion

    #region Class
    public sealed partial class Mapping_InvoiceTimelogRepository : BaseRepository<Mapping_InvoiceTimelog>, IMapping_InvoiceTimelogRepository
    {
        public Mapping_InvoiceTimelogRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Mapping_InvoiceTimelog> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<Mapping_InvoiceTimelog> Find(List<Expression<Func<Mapping_InvoiceTimelog, bool>>> filterPredicates)
        {
            var allInvoice = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<Mapping_InvoiceTimelog, bool>> predicate in filterPredicates)
                {
                    allInvoice = allInvoice.Where(predicate);
                }
            }

            return allInvoice;
        }

        public Mapping_InvoiceTimelog Find(int mappingId)
        {
            return this.AllRecords.Where(o => o.Id == mappingId).FirstOrDefault();
        }
    }
    #endregion
}
