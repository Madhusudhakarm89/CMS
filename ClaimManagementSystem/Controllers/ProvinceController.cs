
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

    public class ProvinceController : BaseApiController
    {
        private readonly IProvinceBusinessLayer _businessLayer;

        public ProvinceController()
        {
            this._businessLayer = new ProvinceBusinessLayer();
        }
        private IProvinceBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ProvinceViewModel>> GetAllProvinces()
        {
            try
            {
                IEnumerable<ProvinceViewModel> provinces = await this.BusinessLayer.GetAllProvinces();
                return provinces;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ProvinceViewModel>> Find(int id)
        {
            try
            {
                IEnumerable<ProvinceViewModel> provinces = await this.BusinessLayer.GetAllProvinces();
                var provinces1 = await this.BusinessLayer.Find(id);
                return provinces;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
                       

        [HttpPost]
        public async Task Create(ProvinceViewModel ABC)
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
