
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
    using CMS.Utilities.Enum;
    #endregion

    public class ClaimDocumentMappingController : BaseApiController
    {
        private readonly IClaimDocumentMappingBusinessLayer BusinessLayer;

        public ClaimDocumentMappingController()
        {
            this.BusinessLayer = new ClaimDocumnetMappingBusinessLayer();
        }


        [HttpGet]
        public async Task<IEnumerable<ClaimDocumentViewModel>> GetAllClaimDocuments(int ClaimId)
        {
            try
            {
                IEnumerable<ClaimDocumentViewModel> claimDocuments = await this.BusinessLayer.GetAllClaimDocuments(User.Identity.GetUserId(), ClaimId, UploadFileType.Image);
                return claimDocuments;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ClaimDocumentViewModel> Find(int id)
        {
            try
            {
                ClaimDocumentViewModel claimDocuments = await this.BusinessLayer.Find(id);
                return claimDocuments;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ClaimDocumentViewModel> Create(ClaimDocumentViewModel claimDocumentData)
        {
            try
            {

                return await this.BusinessLayer.Create(claimDocumentData);

            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ClaimDocumentViewModel> Update(ClaimDocumentViewModel claimDocumentData)
        {
            try
            {
                return await this.BusinessLayer.Update(claimDocumentData);
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
