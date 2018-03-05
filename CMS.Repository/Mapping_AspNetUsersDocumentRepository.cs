
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
    public interface IAspNetUserDocumentMappingRepository : IBaseRepository<AspNetUsersDocumentMapping>
    {
        new IQueryable<AspNetUsersDocumentMapping> AllRecords { get; }
        IQueryable<AspNetUsersDocumentMapping> Find(List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates);
        AspNetUsersDocumentMapping Find(int documentId);
    }
    #endregion

    #region Class
    public sealed partial class AspNetUserDocumentMappingRepository : BaseRepository<AspNetUsersDocumentMapping>, IAspNetUserDocumentMappingRepository
    {
        public AspNetUserDocumentMappingRepository() : base(new CMSDBContext()) { }


        public new IQueryable<AspNetUsersDocumentMapping> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<AspNetUsersDocumentMapping> Find(List<Expression<Func<AspNetUsersDocumentMapping, bool>>> filterPredicates)
        {
            var allClaimsDocuments = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<AspNetUsersDocumentMapping, bool>> predicate in filterPredicates)
                {
                    allClaimsDocuments = allClaimsDocuments.Where(predicate);
                }
            }

            return allClaimsDocuments;
        }

        public AspNetUsersDocumentMapping Find(int documentId)
        {
            return this.AllRecords.Where(o => o.DocumentId == documentId).FirstOrDefault();
        }
    }
    #endregion
}
