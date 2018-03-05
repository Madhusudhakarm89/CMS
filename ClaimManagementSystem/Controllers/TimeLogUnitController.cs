
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

    public class TimeLogUnitController : BaseApiController
    {
        private readonly ITimeLogUnitBusinessLayer BusinessLayer;

        public TimeLogUnitController()
        {
            this.BusinessLayer = new TimeLogUnitBusinessLayer();
        }

        [HttpGet]
        public async Task<IEnumerable<TimeLogUnitViewModel>> GetAllUnits()
        {
            try
            {
                IEnumerable<TimeLogUnitViewModel> timelogUnits = await this.BusinessLayer.GetAllTimeLogUnits();
                return timelogUnits;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}