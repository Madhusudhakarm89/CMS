
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

    public class TaxSettingController : BaseApiController
    {
        private readonly ITaxSettingBusinessLayer BusinessLayer;

        public TaxSettingController()
        {
            this.BusinessLayer = new TaxSettingBusinessLayer();
        }


        [HttpGet]
        public async Task<IEnumerable<TaxSettingViewModel>> GetAllTaxSettings()
        {
            try
            {
                IEnumerable<TaxSettingViewModel> taxSettings = await this.BusinessLayer.GetAllTaxSettings();
                return taxSettings;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<TaxSettingViewModel> Find(int id)
        {
            try
            {
                TaxSettingViewModel taxSettings = await this.BusinessLayer.Find(id);
                return taxSettings;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<TaxSettingViewModel> Save(TaxSettingViewModel viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                return await this.BusinessLayer.Create(viewModel);

            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<TaxSettingViewModel> Update(TaxSettingViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                return await this.BusinessLayer.Update(viewModel);
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
