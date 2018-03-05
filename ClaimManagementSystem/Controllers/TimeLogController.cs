
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

    public class TimeLogController : BaseApiController
    {
        private readonly ITimeLogBusinessLayer _businessLayer;

        public TimeLogController()
        {
            this._businessLayer = new TimeLogBusinessLayer();
        }
        private ITimeLogBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<TimeLogViewModel>> GetTimeLogs(int claimId)
        {
            try
            {
                IEnumerable<TimeLogViewModel> timeLogs = await this.BusinessLayer.GetAllTimeLogs(User.Identity.GetUserId(), claimId);
                return timeLogs;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<TimeLogViewModel> Find(int timeLogId)
        {
            try
            {
                TimeLogViewModel timeLog = await this.BusinessLayer.Find(timeLogId);
                return timeLog;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        //[HttpPost]
        //public async Task<IEnumerable<TimeLogViewModel>> Find(IEnumerable<FilterParameterViewModel> viewModel)
        //{
        //    try
        //    {
        //        IEnumerable<TimeLogViewModel> timeLogs = await this.BusinessLayer.Find(viewModel);
        //        return timeLogs;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ExceptionLogger.Log(LogLevel.Error, ex);
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public async Task<TimeLogViewModel> Save(TimeLogViewModel viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                TimeLogViewModel timeLog = await this.BusinessLayer.Create(viewModel);
                return timeLog;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<TimeLogViewModel> Update(TimeLogViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                TimeLogViewModel timeLog = await this.BusinessLayer.Update(viewModel);
                return timeLog;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }


        [HttpPost]
        public async Task<TimeLogViewModel> IsBilledUpdate(TimeLogViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsBilled = true;


                TimeLogViewModel timeLog = await this.BusinessLayer.IsBilledUpdate(viewModel);
                return timeLog;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }


        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            try
            {
                bool isDeleted = await this.BusinessLayer.Delete(id);
                return isDeleted;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

    }
}
