
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

    public class UserProfileController : BaseApiController
    {
        private readonly IUserProfileBusinessLayer _businessLayer;

        public UserProfileController()
        {
            this._businessLayer = new UserProfileBusinessLayer();
        }
        private IUserProfileBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<UserProfileViewModel>> GetAllProfiles()
        {
            try
            {
                IEnumerable<UserProfileViewModel> userProfiles = await this.BusinessLayer.GetAllProfiles();
                return userProfiles;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<UserProfileViewModel> Find(string id)
        {
            try
            {
                UserProfileViewModel userDetails = await this.BusinessLayer.Find(id);
                return userDetails;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}
