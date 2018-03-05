
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
    public interface IClaimNotesRepository : IBaseRepository<ClaimNote>
    {
        new IQueryable<ClaimNote> AllRecords { get; }
        IQueryable<ClaimNote> Find(List<Expression<Func<ClaimNote, bool>>> filterPredicates);

        ClaimNote Find(int claimNoteId);
    }
    #endregion

    #region Class
    public class ClaimNotesRepository : BaseRepository<ClaimNote>, IClaimNotesRepository
    {
        public ClaimNotesRepository() : base(new CMSDBContext()) { }

        public new IQueryable<ClaimNote> AllRecords
        {
            get
            {
                return base.AllRecords.Where(o => o.IsActive == true); 
            }
        }

        public IQueryable<ClaimNote> Find(List<Expression<Func<ClaimNote, bool>>> filterPredicates)
        {
            var allNotes = this.AllRecords;

            if (filterPredicates != null && filterPredicates.Count > 0)
            {
                foreach (Expression<Func<ClaimNote, bool>> predicate in filterPredicates)
                {
                    allNotes = allNotes.Where(predicate);
                }
            }

            return allNotes;
        }

        public ClaimNote Find(int claimNoteId)
        {
            return this.AllRecords.Where(o => o.NoteId == claimNoteId).FirstOrDefault();
        }
    }    
    #endregion
}
