
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using NLog;

    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    #endregion

    public class CompanyController : BaseApiController
    {
        private readonly ICompanyBusinessLayer _businessLayer;

        public CompanyController()
        {
            this._businessLayer = new CompanyBusinessLayer();
        }
        private ICompanyBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyViewModel>> GetCompanies()
        {
            try
            {
                IEnumerable<CompanyViewModel> companies = await this.BusinessLayer.GetAllCompanies(User.Identity.GetUserId());
                return companies;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<CompanyViewModel> Find(int id)
        {
            try
            {
                CompanyViewModel companies = await this.BusinessLayer.Find(id);
                return companies;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
            }

        [HttpPost]
        public async Task<CompanyViewModel> Create(CompanyViewModel companyData)
        {
            try
            {

                return await this.BusinessLayer.Create(companyData);

            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<CompanyViewModel> Update(CompanyViewModel companyData)
        {
            try
            {
                return await this.BusinessLayer.Update(companyData);
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
