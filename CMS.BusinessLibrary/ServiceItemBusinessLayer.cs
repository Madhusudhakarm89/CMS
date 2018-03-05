
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
    public interface IServiceItemBusinessLayer
    {
        Task<IEnumerable<ServiceItemViewModel>> GetAllServiceItems();
        Task<ServiceItemViewModel> Find(int serviceItemId);
        Task<IEnumerable<ServiceItemViewModel>> Find(List<Expression<Func<ServiceItem, bool>>> filterPredicates);
        Task<ServiceItemViewModel> Create(ServiceItemViewModel item);
        Task<ServiceItemViewModel> Update(ServiceItemViewModel item);

        Task<bool> Delete(int serviceItemId);
    }

    #endregion

    #region Class
    public sealed partial class ServiceItemBusinessLayer : IServiceItemBusinessLayer
    {
        private readonly IServiceItemRepository _serviceItemRepository;


        public ServiceItemBusinessLayer()
        {
            this._serviceItemRepository = new ServiceItemRepository();
        }

        private IServiceItemRepository ServiceItemRepository
        {
            get { return this._serviceItemRepository; }
        }

        public async Task<IEnumerable<ServiceItemViewModel>> GetAllServiceItems()
        {
            return EntityToViewModelMapper.Map(this.ServiceItemRepository.AllRecords);
        }

        public async Task<ServiceItemViewModel> Find(int ServiceItemId)
        {
            var serviceItemInfo = this.ServiceItemRepository.Find(ServiceItemId);

            return EntityToViewModelMapper.Map(serviceItemInfo);
        }

        public async Task<IEnumerable<ServiceItemViewModel>> Find(List<Expression<Func<ServiceItem, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.ServiceItemRepository.Find(filterPredicates)).OrderBy(e => e.ServiceItemName);
        }

        public async Task<ServiceItemViewModel> Create(ServiceItemViewModel viewModel)
        {
            if (viewModel != null)
            {
                var serviceItem = this.ServiceItemRepository.Add(ViewModelToEntityMapper.Map(viewModel));

                if (serviceItem.ServiceItemId > 0)
                {
                    viewModel.ServiceItemId = serviceItem.ServiceItemId;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<ServiceItemViewModel> Update(ServiceItemViewModel viewModel)
        {
            if (viewModel != null)
            {
                var serviceItem = this.ServiceItemRepository.Find(viewModel.ServiceItemId);

                if (serviceItem != null)
                {
                    var lastModifiedDate = serviceItem.LastModifiedDate;
                    serviceItem = this.ServiceItemRepository.Update(ViewModelToEntityMapper.Map(viewModel, serviceItem));

                    if (lastModifiedDate < serviceItem.LastModifiedDate)
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


        public async Task<bool> Delete(int ServiceItemId)
        {
            var serviceItem = this.ServiceItemRepository.Find(ServiceItemId);

            if (serviceItem != null)
            {
                serviceItem.IsActive = false;
                var deletedServiceItem = this.ServiceItemRepository.Delete(serviceItem);

                if (!deletedServiceItem.IsActive)
                    return true;
            }

            return false;
        }
    }

    #endregion
}
