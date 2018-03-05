using CMS.BusinessLibrary.EntityModelMapping;
using CMS.BusinessLibrary.ViewModels;
using CMS.Entity;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary
{
    #region Interface
    public interface ISystemAlertsBusinessLayer
    {
        Task<IEnumerable<AlertViewModel>> GetAllSystemAlerts();
        Task<AlertViewModel> Create(AlertViewModel viewModel);
        Task<bool> Delete(int id);
        Task<AlertViewModel> Update(AlertViewModel viewModel);
        Task<AlertViewModel> Find(int alertId);
    }

    #endregion
    public class SystemAlertsBusinessLayer : ISystemAlertsBusinessLayer
    {
         private readonly ISystemAlertsRepository _systemAlertsRepository;


         public SystemAlertsBusinessLayer()
        {
            this._systemAlertsRepository = new SystemAlertsRepository();
        }

         private ISystemAlertsRepository SystemAlertsRepository
        {
            get { return this._systemAlertsRepository; }
        }

        public async Task<IEnumerable<AlertViewModel>> GetAllSystemAlerts()
        {
            return EntityToViewModelMapper.Map(this._systemAlertsRepository.AllRecords.ToList());
        }

        public async Task<AlertViewModel> Create(AlertViewModel viewModel)
        {
            var systemAlerts = new Alert
            {
                Title=viewModel.Title,
                Description=viewModel.Description,
                AlertBy=viewModel.AlertBy,
                AlertTo=viewModel.AlertTo,
                IsRead=viewModel.IsRead,
                IsActive=viewModel.IsRead,
                CreatedOn=DateTime.Now
            };

            systemAlerts = this.SystemAlertsRepository.Add(systemAlerts);

            if (systemAlerts.AlertId > 0)
            {
                viewModel.AlertId = systemAlerts.AlertId;
            }
            else
            {
                viewModel.AlertId =0;
            }

            return viewModel;
        }

        public async Task<AlertViewModel> Update(AlertViewModel updateModel)
        {
            var systemAlerts = this._systemAlertsRepository.Find(updateModel.AlertId);
            if (systemAlerts != null)
            {
                systemAlerts.Title=updateModel.Title;
                systemAlerts.Description=updateModel.Description;
                systemAlerts.AlertTo=updateModel.AlertTo;
                systemAlerts.AlertBy=updateModel.AlertBy;
                systemAlerts.LastModifiedOn=DateTime.Now;
                systemAlerts.IsRead = updateModel.IsRead;
                systemAlerts.IsActive = updateModel.IsRead;
                systemAlerts = this._systemAlertsRepository.Update(systemAlerts);
                return new AlertViewModel
                {
                    AlertId = systemAlerts.AlertId,
                };
            }
            else
            {
                updateModel.AlertId = 0;
            }

            return updateModel;
        }

        public async Task<bool> Delete(int id)
        {
            var systemAlerts = this._systemAlertsRepository.Find(id);
            if (systemAlerts != null)
            {
                systemAlerts.AlertId = id;
                systemAlerts.IsRead = false;
                systemAlerts.IsActive = false;
                systemAlerts = this._systemAlertsRepository.Update(systemAlerts);

                if (systemAlerts != null)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public async Task<AlertViewModel> Find(int alertId)
        {
            return EntityToViewModelMapper.Map(this._systemAlertsRepository.Find(alertId));
        }
    }
}
