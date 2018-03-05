
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
  
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Description;
    using Microsoft.AspNet.Identity;
    using NLog;

    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    using System.Net.Http;
    using System.Web;
    using System.IO;
    #endregion

    public class FileNamingController : BaseApiController
    {
        private readonly IFileNamingBusinessLayer _businessLayer;

        public FileNamingController()
        {
            this._businessLayer = new FileNamingBusinessLayer();
        }
        private IFileNamingBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<FileNameViewModel>> GetAllFileNamingCode()
        {
            try
            {
                IEnumerable<FileNameViewModel> fileNameCodes = await this.BusinessLayer.GetAllFileNamingCode();
                return fileNameCodes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<FileNameViewModel> Find(int id)
        {
            try
            {
                FileNameViewModel fileNameCode = await this.BusinessLayer.Find(id);
                return fileNameCode;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        
        [HttpPost]
        public async Task<FileNameViewModel> Save(FileNameViewModel viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                FileNameViewModel fileNameCode = await this.BusinessLayer.Create(viewModel);
                return fileNameCode;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<FileNameViewModel> Update(FileNameViewModel fileNamingModel)
        {
            try
            {
                fileNamingModel.LastModifiedBy = User.Identity.GetUserId();
                fileNamingModel.LastModifiedOn = DateTime.Now;
                fileNamingModel.IsActive = true;

                FileNameViewModel fileNameCode = await this.BusinessLayer.Update(fileNamingModel);
                return fileNameCode;
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
