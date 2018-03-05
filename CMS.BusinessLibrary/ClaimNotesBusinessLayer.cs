
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IClaimNotesBusinessLayer
    {
        Task<IEnumerable<ClaimNotesViewModel>> GetAllClaimNotes(string userId, int claimId);
        Task<ClaimNotesViewModel> Find(int claimNoteId);
        Task<IEnumerable<ClaimNotesViewModel>> Find(List<Expression<Func<ClaimNote, bool>>> filterPredicates);
        Task<ClaimNotesViewModel> Create(ClaimNotesViewModel viewModel);
        Task<ClaimNotesViewModel> Update(ClaimNotesViewModel viewModel);
        Task<bool> Delete(int claimNoteId);
        Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssigneToMe(string userId);
        Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssignedByMe(string userId);
        Task<IEnumerable<ClaimNotesViewModel>> GetOverdueTasks(string userId);
    }
    #endregion

    #region Class
    public sealed partial class ClaimNotesBusinessLayer : IClaimNotesBusinessLayer
    {
        private readonly IClaimNotesRepository _claimNotesRepository;

        public ClaimNotesBusinessLayer()
        {
            this._claimNotesRepository = new ClaimNotesRepository();
        }

        private IClaimNotesRepository ClaimNoteRepository
        {
            get { return this._claimNotesRepository; }
        }

        public async Task<IEnumerable<ClaimNotesViewModel>> GetAllClaimNotes(string userId, int claimId)
        {
            List<Expression<Func<ClaimNote, bool>>> filterPredicates = new List<Expression<Func<ClaimNote, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);

            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(filterPredicates));
        }

        public async Task<ClaimNotesViewModel> Find(int claimNoteId)
        {
            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(claimNoteId));
        }

        public async Task<IEnumerable<ClaimNotesViewModel>> Find(List<Expression<Func<ClaimNote, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(filterPredicates).ToList());

        }

        public async Task<ClaimNotesViewModel> Create(ClaimNotesViewModel viewModel)
        {
            var claimNote = this.ClaimNoteRepository.Add(ViewModelToEntityMapper.Map(viewModel));
            if (claimNote.NoteId > 0)
            {
                viewModel.NoteId = claimNote.NoteId;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<ClaimNotesViewModel> Update(ClaimNotesViewModel viewModel)
        {
            var claimNote = this.ClaimNoteRepository.Find(viewModel.NoteId);
            if (claimNote != null && claimNote.IsActive)
            {
                var lastModifiedDate = claimNote.LastModifiedOn;
                claimNote = this.ClaimNoteRepository.Update(ViewModelToEntityMapper.Map(viewModel, claimNote));

                if (lastModifiedDate < claimNote.LastModifiedOn)
                {
                    return viewModel;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<bool> Delete(int claimNoteId)
        {
            var claimNote = this.ClaimNoteRepository.Find(claimNoteId);

            if (claimNote != null)
            {
                claimNote.IsActive = false;
                var deletedClaimNote = this.ClaimNoteRepository.Delete(claimNote);

                if (!deletedClaimNote.IsActive)
                    return true;
            }

            return false;
        }
        public async Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssignedByMe(string userId)
        {
            List<Expression<Func<ClaimNote, bool>>> filterPredicates = new List<Expression<Func<ClaimNote, bool>>>();
            filterPredicates.Add(p => p.CreatedBy == userId);
            filterPredicates.Add(p => p.IsTask == true);

            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(filterPredicates).ToList());
        }


        public async Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssigneToMe(string userId)
        {
            List<Expression<Func<ClaimNote, bool>>> filterPredicates = new List<Expression<Func<ClaimNote, bool>>>();
            filterPredicates.Add(p => p.AssignedTo == userId);
            filterPredicates.Add(p => p.IsTask == true);

            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(filterPredicates).ToList());
        }

        public async Task<IEnumerable<ClaimNotesViewModel>> GetOverdueTasks(string userId)
        {
            List<Expression<Func<ClaimNote, bool>>> filterPredicates = new List<Expression<Func<ClaimNote, bool>>>();
            filterPredicates.Add(p => p.AssignedTo == userId);
            filterPredicates.Add(p => p.IsActive == true);
            filterPredicates.Add(p => p.TaskDueDate < DateTime.Today);
            filterPredicates.Add(p => p.IsTask == true);

            return EntityToViewModelMapper.Map(this.ClaimNoteRepository.Find(filterPredicates).ToList());
        }
    }
    #endregion
}
