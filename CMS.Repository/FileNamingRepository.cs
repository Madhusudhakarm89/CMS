
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
    public interface IFileNamingRepository : IBaseRepository<FileNameCode>
    {
        new IQueryable<FileNameCode> AllRecords { get; }
        IQueryable<FileNameCode> Find(List<Expression<Func<FileNameCode, bool>>> filterPredicates);

        FileNameCode Find(int fileNameId);
    }
    #endregion

    #region Class
    public sealed partial class FileNamingRepository : BaseRepository<FileNameCode>, IFileNamingRepository
    {
        public FileNamingRepository() : base(new CMSDBContext()) { }

        public new IQueryable<FileNameCode> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<FileNameCode> Find(List<Expression<Func<FileNameCode, bool>>> filterPredicates)
        {
            var allInvoice = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<FileNameCode, bool>> predicate in filterPredicates)
                {
                    allInvoice = allInvoice.Where(predicate);
                }
            }

            return allInvoice;
        }

        public FileNameCode Find(int fileNameId)
        {
            return this.AllRecords.Where(o => o.Id == fileNameId).FirstOrDefault();
        }
    }
    #endregion
}
