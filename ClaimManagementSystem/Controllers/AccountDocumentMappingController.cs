
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

    public class AccountDocumentMappingController : BaseApiController
    {
        private readonly IAccountDocumentMappingBusinessLayer _businessLayer;

        public AccountDocumentMappingController()
        {
            this._businessLayer = new AccountDocumnetManageBusinessLayer();
        }
        

        [HttpPost]
        public async Task<IHttpActionResult> Create(CompanyDocumentViewModel obj)
        {
            try
            {
               
                this._businessLayer.CompanyUpload(obj);
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IEnumerable<CompanyDocumentViewModel>> GetDocuments(int companyId)
        {
            try
            {
                IEnumerable<CompanyDocumentViewModel> companyDocuments = await this._businessLayer.GetAllCompanyDocuments(companyId);
                return companyDocuments;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

       [HttpGet]
        public HttpResponseMessage GetCompanyDocuments(string documentId,string fileName)
        {
            HttpResponseMessage result = null;
            var localFilePath = HttpContext.Current.Server.MapPath("~/Uploads/Companies/" + documentId);

            if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = fileName;
            }

            return result;
        }

       
    }
}
