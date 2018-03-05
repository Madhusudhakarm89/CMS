using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public interface IClaimStatusBusinessLayer
    {
        Task<IEnumerable<ClaimStatusViewModel>> GetAllClaimStatus(string type);
        Task<ClaimStatusViewModel> Find(int claimStatusId);
        Task<IEnumerable<ClaimStatusViewModel>> Find(List<Expression<Func<Status, bool>>> filterPredicates);
        Task<ClaimStatusViewModel> Create(ClaimStatusViewModel viewModel);
        Task<ClaimStatusViewModel> Update(ClaimStatusViewModel viewModel);
        Task<bool> Delete(int claimStatusId);
    }
    #endregion

    #region Class
    public sealed partial class ClaimStatusBusinessLayer : IClaimStatusBusinessLayer
    {
        private readonly IClaimStatusRepository _claimStatusRepository;

        public ClaimStatusBusinessLayer()
        {
            this._claimStatusRepository = new ClaimStatusRepository();
        }

        private IClaimStatusRepository ClaimStatusRepository
        {
            get { return this._claimStatusRepository; }
        }

        public async Task<IEnumerable<ClaimStatusViewModel>> GetAllClaimStatus(string type)
        {
            List<Expression<Func<Status, bool>>> filterPredicate = new List<Expression<Func<Status, bool>>>();

            if (!string.IsNullOrWhiteSpace(type))
            {
                filterPredicate.Add(p => p.StatusFor.ToLower().Equals(type));
            }
            else
            {
                filterPredicate.Add(p => p.StatusFor.ToLower().Equals("claim"));
            }

            return await this.Find(filterPredicate);
        }

        public async Task<ClaimStatusViewModel> Find(int claimStatusId)
        {
            return EntityToViewModelMapper.Map(this.ClaimStatusRepository.Find(claimStatusId));
        }

        public async Task<IEnumerable<ClaimStatusViewModel>> Find(List<Expression<Func<Status, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.ClaimStatusRepository.Find(filterPredicates).ToList());
        }

        public async Task<ClaimStatusViewModel> Create(ClaimStatusViewModel viewModel)
        {
            var claimStatus = this.ClaimStatusRepository.Add(ViewModelToEntityMapper.Map(viewModel));
            if (claimStatus.Id > 0)
            {
                viewModel.ClaimStatusId = claimStatus.Id;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<ClaimStatusViewModel> Update(ClaimStatusViewModel viewModel)
        {
            var ClaimStatus = this.ClaimStatusRepository.Find(viewModel.ClaimStatusId);
            if (ClaimStatus != null && ClaimStatus.IsActive)
            {
                var lastModifiedDate = ClaimStatus.LastModifiedDate;
                ClaimStatus = this.ClaimStatusRepository.Update(ViewModelToEntityMapper.Map(viewModel, ClaimStatus));

                if (lastModifiedDate < ClaimStatus.LastModifiedDate)
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

        public async Task<bool> Delete(int claimStatusId)
        {
            var claimStatus = this.ClaimStatusRepository.Find(claimStatusId);

            if (claimStatus != null)
            {
                claimStatus.IsActive = false;
                var deletedClaimStatus = this.ClaimStatusRepository.Delete(claimStatus);

                if (!deletedClaimStatus.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
