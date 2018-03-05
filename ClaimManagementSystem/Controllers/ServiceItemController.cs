
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Description;
    using Microsoft.AspNet.Identity;
    using NLog;

    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    using System.Net.Http;
    using System.Web;
    using System.IO;
    #endregion

    public class ServiceItemController : BaseApiController
    {
        private readonly IServiceItemBusinessLayer BusinessLayer;

        public ServiceItemController()
        {
            this.BusinessLayer = new ServiceItemBusinessLayer();
        }


        [HttpGet]
        public async Task<IEnumerable<ServiceItemViewModel>> GetAllServiceItems()
        {
            try
            {
                IEnumerable<ServiceItemViewModel> serviceItems = await this.BusinessLayer.GetAllServiceItems();
                return serviceItems;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ServiceItemViewModel> Find(int id)
        {
            try
            {
                ServiceItemViewModel serviceItems = await this.BusinessLayer.Find(id);
                return serviceItems;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ServiceItemViewModel> Save(ServiceItemViewModel serviceItemData)
        {
            try
            {
                serviceItemData.CreatedBy = User.Identity.GetUserId();
                serviceItemData.CreatedOn = DateTime.Now;
                serviceItemData.LastModifiedBy = User.Identity.GetUserId();
                serviceItemData.LastModifiedOn = DateTime.Now;
                serviceItemData.IsActive = true;

                return await this.BusinessLayer.Create(serviceItemData);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ServiceItemViewModel> Update(ServiceItemViewModel serviceItemData)
        {
            try
            {
                serviceItemData.LastModifiedBy = User.Identity.GetUserId();
                serviceItemData.LastModifiedOn = DateTime.Now;
                serviceItemData.IsActive = true;

                return await this.BusinessLayer.Update(serviceItemData);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
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


       
    }
}
