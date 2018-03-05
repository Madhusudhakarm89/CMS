
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

    public class ContactTypeController : BaseApiController
    {
        private readonly IContactTypeBusinessLayer _businessLayer;

        public ContactTypeController()
        {
            this._businessLayer = new ContactTypeBusinessLayer();
        }
        private IContactTypeBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ContactTypeViewModel>> GetAllContactTypes()
        {
            try
            {
                IEnumerable<ContactTypeViewModel> contactTypes = await this.BusinessLayer.GetAllContactTypes();
                return contactTypes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

    }
}
