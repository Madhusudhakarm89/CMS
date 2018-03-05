
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface ITypeOfLossBusinessLayer
    {
        Task<IEnumerable<TypeOfLossViewModel>> GetAllTypeOfLoss();
        Task<TypeOfLossViewModel> Find(int lossTypeId);
        Task<IEnumerable<TypeOfLossViewModel>> Find(List<Expression<Func<TypeOfLoss, bool>>> filterPredicate);
        Task<TypeOfLossViewModel> Create(TypeOfLossViewModel item);
        Task<TypeOfLossViewModel> Update(TypeOfLossViewModel item);

        Task<bool> Delete(int lossTypeId);
    }

    #endregion

    #region Class
    public sealed partial class TypeOfLossBusinessLayer : ITypeOfLossBusinessLayer
    {
        private readonly ITypeOfLossRepository _typeOfLossRepository;


        public TypeOfLossBusinessLayer()
        {
            this._typeOfLossRepository = new TypeOfLossRepository();
        }

        private ITypeOfLossRepository TypeOfLossRepository
        {
            get { return this._typeOfLossRepository; }
        }

        public async Task<IEnumerable<TypeOfLossViewModel>> GetAllTypeOfLoss()
        {
            return EntityToViewModelMapper.Map(this.TypeOfLossRepository.AllRecords);
        }

        public async Task<TypeOfLossViewModel> Find(int lossTypeId)
        {
            return EntityToViewModelMapper.Map(this.TypeOfLossRepository.Find(lossTypeId));
        }

        public async Task<IEnumerable<TypeOfLossViewModel>> Find(List<Expression<Func<TypeOfLoss, bool>>> filterPredicate)
        {
            return EntityToViewModelMapper.Map(this.TypeOfLossRepository.Find(filterPredicate));
        }

        public async Task<TypeOfLossViewModel> Create(TypeOfLossViewModel model)
        {
            var typeOfLoss = this.TypeOfLossRepository.Add(ViewModelToEntityMapper.Map(model));
            if (typeOfLoss.Id > 0)
            {
                model.LossTypeId = typeOfLoss.Id;
            }
            else
            {
                model.HasError = true;
            }

            return model;
        }

        public async Task<TypeOfLossViewModel> Update(TypeOfLossViewModel viewModel)
        {
            var typeOfLossInfo = this.TypeOfLossRepository.Find(viewModel.LossTypeId);
            if (typeOfLossInfo != null)
            {
                var lastModifiedDate = typeOfLossInfo.LastModifiedDate;
                typeOfLossInfo = this.TypeOfLossRepository.Update(ViewModelToEntityMapper.Map(viewModel, typeOfLossInfo));

                if (lastModifiedDate < typeOfLossInfo.LastModifiedDate)
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


        public async Task<bool> Delete(int lossTypeId)
        {
            var lossTypeDetails = this.TypeOfLossRepository.Find(lossTypeId);

            if (lossTypeDetails != null)
            {
                lossTypeDetails.IsActive = false;
                var deletedLossType = this.TypeOfLossRepository.Delete(lossTypeDetails);

                if (!deletedLossType.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}