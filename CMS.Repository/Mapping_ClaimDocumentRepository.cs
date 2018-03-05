
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
    public interface IClaimDocumentMappingRepository : IBaseRepository<ClaimDocumentMapping>
    {
        new IQueryable<ClaimDocumentMapping> AllRecords { get; }
        IQueryable<ClaimDocumentMapping> Find(List<Expression<Func<ClaimDocumentMapping, bool>>> filterPredicates);

        ClaimDocumentMapping Find(int documentId);
    }
    #endregion

    #region Class
    public sealed partial class ClaimDocumentMappingRepository : BaseRepository<ClaimDocumentMapping>, IClaimDocumentMappingRepository
    {
        public ClaimDocumentMappingRepository() : base(new CMSDBContext()) { }

        public new IQueryable<ClaimDocumentMapping> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true); 
            }
        }

        public IQueryable<ClaimDocumentMapping> Find(List<Expression<Func<ClaimDocumentMapping, bool>>> filterPredicates)
        {
            var allClaimsDocuments = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<ClaimDocumentMapping, bool>> predicate in filterPredicates)
                {
                    allClaimsDocuments = allClaimsDocuments.Where(predicate);
                }
            }

            return allClaimsDocuments;
        }

        public ClaimDocumentMapping Find(int documentId)
        {
            return this.AllRecords.Where(o => o.DocumentId == documentId).FirstOrDefault();
        }
    }
    #endregion
}
