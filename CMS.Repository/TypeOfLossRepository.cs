
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
    public interface ITypeOfLossRepository : IBaseRepository<TypeOfLoss>
    {
        new IQueryable<TypeOfLoss> AllRecords { get; }
        IQueryable<TypeOfLoss> Find(List<Expression<Func<TypeOfLoss, bool>>> filterPredicate);

        TypeOfLoss Find(int lossTypeId);
    }
    #endregion

    #region Class
    public sealed partial class TypeOfLossRepository : BaseRepository<TypeOfLoss>, ITypeOfLossRepository
    {
        public TypeOfLossRepository() : base(new CMSDBContext()) { }

        public new IQueryable<TypeOfLoss> AllRecords
        {
            get
            {
                return base.AllRecords.Where(e => e.IsActive == true);
            }
        }

        public IQueryable<TypeOfLoss> Find(List<Expression<Func<TypeOfLoss, bool>>> filterPredicate)
        {
            var allTypeOfLoss = this.AllRecords;

            if (filterPredicate != null && filterPredicate.Count > 0)
            {
                foreach (Expression<Func<TypeOfLoss, bool>> predicate in filterPredicate)
                {
                    allTypeOfLoss = allTypeOfLoss.Where(predicate);
                }
            }

            return allTypeOfLoss;
        }

        public TypeOfLoss Find(int lossTypeId)
        {
            return this.AllRecords.Where(o => o.Id == lossTypeId).FirstOrDefault();
        }
    }
    #endregion
}
