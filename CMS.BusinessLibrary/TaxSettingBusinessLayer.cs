
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
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface ITaxSettingBusinessLayer
    {
        Task<IEnumerable<TaxSettingViewModel>> GetAllTaxSettings();
        Task<TaxSettingViewModel> Find(int Id);
        Task<IEnumerable<TaxSettingViewModel>> Find(List<Expression<Func<TaxSetting, bool>>> filterPredicates);
        Task<TaxSettingViewModel> Create(TaxSettingViewModel viewModel);
        Task<TaxSettingViewModel> Update(TaxSettingViewModel viewModel);

        Task<bool> Delete(int Id);
    }
    #endregion

    #region Class
    public sealed partial class TaxSettingBusinessLayer : ITaxSettingBusinessLayer
    {
        private readonly ITaxSettingsRepository _taxSettingRepository;


        public TaxSettingBusinessLayer()
        {
            this._taxSettingRepository = new TaxSettingsRepository();
        }

        private ITaxSettingsRepository TaxSettingRepository
        {
            get { return this._taxSettingRepository; }
        }

        public async Task<IEnumerable<TaxSettingViewModel>> GetAllTaxSettings()
        {
            return EntityToViewModelMapper.Map(this.TaxSettingRepository.AllRecords);
        }

        public async Task<TaxSettingViewModel> Find(int Id)
        {
            var taxSettingInfo = this.TaxSettingRepository.Find(Id);
            return EntityToViewModelMapper.Map(taxSettingInfo);
        }

        public async Task<IEnumerable<TaxSettingViewModel>> Find(List<Expression<Func<TaxSetting, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.TaxSettingRepository.Find(filterPredicates)).OrderBy(e => e.TaxRate);
        }

        public async Task<TaxSettingViewModel> Create(TaxSettingViewModel viewModel)
        {
            if (viewModel != null)
            {
                var taxSetting = this.TaxSettingRepository.Add(ViewModelToEntityMapper.Map(viewModel));

                if (taxSetting.Id > 0)
                {
                    viewModel.Id = taxSetting.Id;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<TaxSettingViewModel> Update(TaxSettingViewModel viewModel)
        {
            if (viewModel != null)
            {
                var taxSetting = this.TaxSettingRepository.Find(viewModel.Id);
                if (taxSetting != null)
                {
                    var lastModifiedDate = taxSetting.LastModifiedDate;
                    taxSetting = this.TaxSettingRepository.Update(ViewModelToEntityMapper.Map(viewModel, taxSetting));

                    if (lastModifiedDate < taxSetting.LastModifiedDate)
                    {
                        return viewModel;
                    }
                    else
                    {
                        viewModel.HasError = true;
                    }
                }
            }

            return viewModel;
        }


        public async Task<bool> Delete(int Id)
        {
            var taxSetting = this.TaxSettingRepository.Find(Id);

            if (taxSetting != null)
            {
                taxSetting.IsActive = false;
                var deletedTaxSetting = this.TaxSettingRepository.Delete(taxSetting);

                if (!deletedTaxSetting.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
