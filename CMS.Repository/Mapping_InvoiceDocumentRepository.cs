
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
    public interface IInvoiceDocumentMappingRepository : IBaseRepository<InvoiceDocumentMapping>
    {
        new IQueryable<InvoiceDocumentMapping> AllRecords { get; }
        IQueryable<InvoiceDocumentMapping> Find(List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates);
        InvoiceDocumentMapping Find(int documentId);
    }
    #endregion

    #region Class
    public sealed partial class InvoiceDocumentMappingRepository : BaseRepository<InvoiceDocumentMapping>, IInvoiceDocumentMappingRepository
    {
        public InvoiceDocumentMappingRepository() : base(new CMSDBContext()) { }


        public new IQueryable<InvoiceDocumentMapping> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<InvoiceDocumentMapping> Find(List<Expression<Func<InvoiceDocumentMapping, bool>>> filterPredicates)
        {
            var allInvoiceDocuments = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<InvoiceDocumentMapping, bool>> predicate in filterPredicates)
                {
                    allInvoiceDocuments = allInvoiceDocuments.Where(predicate);
                }
            }

            return allInvoiceDocuments;
        }

        public InvoiceDocumentMapping Find(int documentId)
        {
            return this.AllRecords.Where(o => o.DocumentId == documentId).FirstOrDefault();
        }
    }
    #endregion
}
