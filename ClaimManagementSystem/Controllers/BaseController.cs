
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using CMS.BusinessLibrary.ViewModels;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    [Authorize]
    [HandleError(View = "Error")]
    public class BaseController : Controller
    {
        private readonly Logger _logger;
        private ApplicationUserManager _userManager;


        public BaseController()
        {
            this._logger = LogManager.GetCurrentClassLogger();
        }

        public BaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        protected Logger ExceptionLogger
        {
            get
            {
                return this._logger;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogAjaxException(string ajaxErrorMessage)
        {
            throw new HttpUnhandledException(ajaxErrorMessage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> SaveCompanyFiles(IEnumerable<HttpPostedFileBase> files, int companyId)
        {

            if (files != null)
            {
                CompanyDocumentViewModel obj = new CompanyDocumentViewModel();
                foreach (var file in files)
                {
                    var DocumentId = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Uploads/Companies"), DocumentId);
                    //file.SaveAs(physicalPath + "_" + companyId);
                    file.SaveAs(physicalPath);
                    obj.AccountId = companyId;
                    obj.DocumentId = DocumentId;
                    obj.FileName = fileName;
                }
                //string url = "http://localhost:23438/Api/AccountDocumentMapping/Create";
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(url);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage responseMessage = await client.PostAsJsonAsync<CompanyDocumentViewModel>(url, obj);
                var webApi = new AccountDocumentMappingController();
                webApi.Create(obj);
            }

            return Content("");
        }
        public ActionResult RemoveCompanyFile(string[] fileNames)
        {


            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Uploads/Companies"), fileName);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            return Content("");
        }

    }
  
}