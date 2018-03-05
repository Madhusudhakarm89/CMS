
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
    public interface IAccountDocumentMappingRepository : IBaseRepository<AccountDocumentMapping>
    {
        new IQueryable<AccountDocumentMapping> AllRecords { get; }
    }
    #endregion

    #region Class
    public sealed partial class AccountDocumentMappingRepository : BaseRepository<AccountDocumentMapping>, IAccountDocumentMappingRepository
    {
        public AccountDocumentMappingRepository() : base(new CMSDBContext()) { }

        public new IQueryable<AccountDocumentMapping> AllRecords
        {
            get
            {
                return base.AllRecords;
            }
        }

    }
    #endregion
}
