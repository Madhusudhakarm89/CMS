
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    #endregion

    [Authorize]
    public class BaseApiController : ApiController
    {
        private readonly ILogger _logger;
        private ApplicationUserManager _userManager;
        public BaseApiController()
        {
            this._logger = LogManager.GetCurrentClassLogger();
        }
        public BaseApiController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        protected ILogger ExceptionLogger
        {
            get { return this._logger; }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //return this._userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
