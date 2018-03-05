using CMS.BusinessLibrary;
using CMS.BusinessLibrary.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using NLog;
using Microsoft.Owin.Security;

namespace ClaimManagementSystem.Controllers
{
    public class SystemAlertsController : BaseApiController
    {
        private readonly ISystemAlertsBusinessLayer BusinessLayer;

        public SystemAlertsController()
        {
            this.BusinessLayer = new SystemAlertsBusinessLayer();
        }


        [HttpGet]
        public async Task<IEnumerable<AlertViewModel>> GetAllSystemAlerts()
        {
            try
            {
                IEnumerable<AlertViewModel> systemAlerts = await this.BusinessLayer.GetAllSystemAlerts();
                return systemAlerts;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IEnumerable<AlertViewModel>> GetSystemAlerts()
        {
            try
            {
                IEnumerable<AlertViewModel> systemAlerts = await this.BusinessLayer.GetAllSystemAlerts();
                return systemAlerts.Where(e=>e.IsRead==true).OrderByDescending(e => e.CreatedOn).Take(3);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<AlertViewModel> Create(AlertViewModel viewModel)
        {
            try
            {
                
                viewModel.Title = viewModel.Title;
                viewModel.Description = viewModel.Description;
                if (viewModel.IsRead != null)
                {
                    viewModel.IsRead = viewModel.IsRead;
                }
                else
                {
                    viewModel.IsRead = false;
                }
                viewModel.AlertBy = User.Identity.GetUserId();
                viewModel.AlertTo = User.Identity.GetUserId();
                AlertViewModel systemAlert = await this.BusinessLayer.Create(viewModel);
                return systemAlert;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<AlertViewModel> Update(AlertViewModel updateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    updateModel.AlertBy = User.Identity.GetUserId();
                    updateModel.AlertTo = User.Identity.GetUserId();
                    return await this.BusinessLayer.Update(updateModel);
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
            return null;
        }

        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await this.BusinessLayer.Delete(id);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<AlertViewModel> Find(int id)
        {
            try
            {
                AlertViewModel systemAlert = await this.BusinessLayer.Find(id);
                return systemAlert;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}
