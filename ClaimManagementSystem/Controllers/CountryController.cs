
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using NLog;
    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    #endregion

    public class CountryController : BaseApiController
    {
        private readonly ICountryBusinessLayer _businessLayer;

        public CountryController()
        {
            this._businessLayer = new CountryBusinessLayer();
        }
        private ICountryBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<CountryViewModel>> GetAllCountries()
        {
            try
            {
                IEnumerable<CountryViewModel> countries = await this.BusinessLayer.GetAllCountries();
                return countries;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<CountryViewModel>> Find(int id)
        {
            try
            {
                IEnumerable<CountryViewModel> countries = await this.BusinessLayer.GetAllCountries();
                return countries;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task Create(CountryViewModel ABC)
        {
            try
            {

                this.BusinessLayer.Create(ABC);

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
