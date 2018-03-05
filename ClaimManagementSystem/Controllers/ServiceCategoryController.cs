
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Threading.Tasks;
    using NLog;

    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;

    #endregion

    public class ServiceCategoryController : BaseApiController
    {
        private readonly IServiceCategoryBusinessLayer BusinessLayer;

        public ServiceCategoryController()
        {
            this.BusinessLayer = new ServiceCategoryBusinessLayer();
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceCategoryViewModel>> GetAllServiceCategories()
        {
            try
            {
                IEnumerable<ServiceCategoryViewModel> serviceCategories = await this.BusinessLayer.GetAllServiceCategories();
                return serviceCategories;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}
