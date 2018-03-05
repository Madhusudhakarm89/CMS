
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Utilities.Enum;
    using ClaimManagementSystem.Models;

    using Microsoft.AspNet.Identity;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Linq.Expressions;
    #endregion

    public class ClaimStatusController : BaseApiController
    {
        private readonly IClaimStatusBusinessLayer _businessLayer;

        public ClaimStatusController()
        {
            this._businessLayer = new ClaimStatusBusinessLayer();
        }

        private IClaimStatusBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimStatusViewModel>> GetAllClaimStatus(string type="")
        {
            try
            {
                IEnumerable<ClaimStatusViewModel> claimStatus = await this.BusinessLayer.GetAllClaimStatus(type);
                return claimStatus;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

    }
}
