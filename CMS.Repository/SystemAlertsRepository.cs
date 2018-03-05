using CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    #region Interface
    public interface ISystemAlertsRepository : IBaseRepository<Alert>
    {
        new IQueryable<Alert> AllRecords { get; }
        //IQueryable<Province> Find(List<Expression<Func<Province, bool>>> filterPredicates);

        Alert Find(int id);
    }
    #endregion
    public class SystemAlertsRepository:BaseRepository<Alert>, ISystemAlertsRepository
    {
        public SystemAlertsRepository() : base(new CMSDBContext()) { }

        public new IQueryable<Alert> AllRecords
        {
            get
            {
                return base.AllRecords.OrderByDescending(e=>e.CreatedOn); 
            }
        }

        public Alert Find(int id)
        {
            return this.AllRecords.Where(o => o.AlertId == id).FirstOrDefault();
        }
    }
}
