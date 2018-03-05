
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

   

    public class GlobalSearchController : BaseApiController
    {
        private readonly IGlobalSearchBusinessLayer _businessLayer;

        public GlobalSearchController()
        {
            this._businessLayer = new GlobalSearchBusinessLayer();
        }
        private IGlobalSearchBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<GlobalSearchViewModel> GetGlobalSearchResult(string Id)
        {
            try
            {
                GlobalSearchViewModel searchResults = await this.BusinessLayer.GlobalSearch(Id);
                return searchResults;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

       
    }
}
