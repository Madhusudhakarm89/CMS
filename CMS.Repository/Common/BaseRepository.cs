
namespace CMS.Repository
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AllRecords { get; }

        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);

        //to hard delete the record in table
        TEntity DeletePermanent(TEntity entity);
    }

    #endregion

    #region Class
    public partial class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            this._context = context;
        }

        public DbContext Context {
            get { return this._context; }
        }

        public IQueryable<TEntity> AllRecords
        {
            get
            {
                return this.Context.Set<TEntity>().AsQueryable<TEntity>();
            }
        }

       
        public TEntity Add(TEntity entity)
            
        {
            try
            {
                this.Context.Set<TEntity>().Add(entity);
                this.Context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TEntity Update(TEntity entity)
        {
            var existingRecord = this.Context.Entry(entity);

            if (existingRecord.State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
                existingRecord = this.Context.Entry(entity);
            }

            existingRecord.State = EntityState.Modified;
            this.Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            var existingRecord = this.Context.Entry(entity);

            if (existingRecord.State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
                existingRecord = this.Context.Entry(entity);
            }

            existingRecord.State = EntityState.Modified;
            this.Context.SaveChanges();
            return entity;
        }

        public TEntity DeletePermanent(TEntity entity)
        {
            var existingRecord = this.Context.Entry(entity);

            if (existingRecord != null)
            {
                this.Context.Set<TEntity>().Attach(entity);
                this.Context.Set<TEntity>().Remove(entity);
            }

            this.Context.SaveChanges();
            return entity;
        }
    }
    #endregion
}
