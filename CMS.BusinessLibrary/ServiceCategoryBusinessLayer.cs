
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
    public interface IServiceCategoryBusinessLayer
    {
        Task<IEnumerable<ServiceCategoryViewModel>> GetAllServiceCategories();
        Task<ServiceCategoryViewModel> Find(int serviceItemId);
        Task<IEnumerable<ServiceCategoryViewModel>> Find(List<Expression<Func<ServiceCategory, bool>>> filterPredicates);
        Task<ServiceCategoryViewModel> Create(ServiceCategoryViewModel item);
        Task<ServiceCategoryViewModel> Update(ServiceCategoryViewModel item);

        Task<bool> Delete(int serviceCategoryId);
    }

    #endregion

    #region Class
    public sealed partial class ServiceCategoryBusinessLayer : IServiceCategoryBusinessLayer
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;


        public ServiceCategoryBusinessLayer()
        {
            this._serviceCategoryRepository = new ServiceCategoryRepository();
        }

        private IServiceCategoryRepository ServiceCategoryRepository
        {
            get { return this._serviceCategoryRepository; }
        }

        public async Task<IEnumerable<ServiceCategoryViewModel>> GetAllServiceCategories()
        {
            return EntityToViewModelMapper.Map(this.ServiceCategoryRepository.AllRecords);
        }

        public async Task<ServiceCategoryViewModel> Find(int ServiceItemId)
        {
            var serviceItemInfo = this.ServiceCategoryRepository.Find(ServiceItemId);

            return EntityToViewModelMapper.Map(serviceItemInfo);
        }

        public async Task<IEnumerable<ServiceCategoryViewModel>> Find(List<Expression<Func<ServiceCategory, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.ServiceCategoryRepository.Find(filterPredicates));
        }

        public async Task<ServiceCategoryViewModel> Create(ServiceCategoryViewModel serviceCategory)
        {
            ServiceCategory obj = ViewModelToEntityMapper.Map(serviceCategory);


            var serviceCategoryInfo = this.ServiceCategoryRepository.Add(obj);
            return EntityToViewModelMapper.Map(serviceCategoryInfo);
        }

        public async Task<ServiceCategoryViewModel> Update(ServiceCategoryViewModel serviceCategory)
        {
            var serviceCategoryInfo = this.ServiceCategoryRepository.Find(serviceCategory.ServiceCategoryId);
            if (serviceCategoryInfo != null)
            {

                serviceCategoryInfo = ViewModelToEntityMapper.Map(serviceCategory, serviceCategoryInfo);
                serviceCategoryInfo = this.ServiceCategoryRepository.Update(serviceCategoryInfo);

                return EntityToViewModelMapper.Map(serviceCategoryInfo);
            }

            return serviceCategory;
        }


        public async Task<bool> Delete(int ServiceCategoryId)
        {
            var serviceCategoryDetails = this.ServiceCategoryRepository.Find(ServiceCategoryId);

            if (serviceCategoryDetails != null)
            {
                serviceCategoryDetails.IsActive = false;
                var deletedCompany = this.ServiceCategoryRepository.Delete(serviceCategoryDetails);


                return true;
            }

            return false;
        }
    }
    #endregion
}
