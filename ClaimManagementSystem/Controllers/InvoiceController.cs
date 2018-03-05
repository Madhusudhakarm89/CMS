
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
    using System.Configuration;
    using System.Web;
    #endregion

    public class InvoiceController : BaseApiController
    {
        private readonly IInvoiceBusinessLayer _businessLayer;
        private readonly IInvoiceDocumnetMappingBusinessLayer _invoiceDocumnetMappingBusinessLayer;
        public InvoiceController()
        {
            this._businessLayer = new InvoiceBusinessLayer();
            this._invoiceDocumnetMappingBusinessLayer = new InvoiceDocumnetMappingBusinessLayer();
        }
        private IInvoiceBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }
        private IInvoiceDocumnetMappingBusinessLayer InvoiceDocumnetMappingBusinessLayer
        {
            get { return this._invoiceDocumnetMappingBusinessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoice()
        {
            try
            {
                IEnumerable<InvoiceViewModel> invoices = await this.BusinessLayer.GetAllInvoices(User.Identity.GetUserId());
                return invoices;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        //[HttpGet]
        //public JsonResult GetLabelsReport(int id)
        //{
        //    try
        //    {
        //        ILabelSurvey labelSurvey = new LabelSurvey();
        //        DataSet ReportData = labelSurvey.GetLabelsReportData(id);
        //        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["reportPath"].ToString();
        //        string FilePath = System.Configuration.ConfigurationManager.AppSettings["filePath"].ToString();
        //        string TemplatePath = System.Configuration.ConfigurationManager.AppSettings["templatePath"].ToString();
        //        var filePath = FilePath;
        //        var reportPath = System.Web.HttpContext.Current.Server.MapPath(ReportPath);
        //        var row = ReportData.Tables[0].Rows[0];
        //        var companyName = row["companyName"].ToString();
        //        companyName = Regex.Replace(companyName, "[^a-zA-Z0-9_.\\-,\\s]+", "_");
        //        var fileName = companyName + "-" + id + "_" + WebSecurity.CurrentUserId + ".docx";
        //        reportPath += fileName;
        //        var templatePath = System.Web.HttpContext.Current.Server.MapPath(TemplatePath);
        //        LabelReport.GenerateReport(ReportData, templatePath, reportPath, filePath);
        //        return Json(new { fileName = fileName }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = new Exception("LabelsController -> (Get)GetLabelsReport: \r\n\r\n", ex);
        //        LogManager.WriteErrorLog(ex);
        //    }

        //    return Json(new { fileName = "" }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoiceByClaim(int claimId)
        {
            try
            {
                IEnumerable<InvoiceViewModel> invoices = await this.BusinessLayer.GetAllInvoices(User.Identity.GetUserId(), claimId);
                return invoices;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<InvoiceViewModel> Find(int invoiceId)
        {
            try
            {
                InvoiceViewModel invoice = await this.BusinessLayer.Find(invoiceId);
                return invoice;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IEnumerable<InvoiceViewModel>> Find(IEnumerable<FilterParameterViewModel> viewModel)
        {
            try
            {
                IEnumerable<InvoiceViewModel> invoices = await this.BusinessLayer.Find(viewModel);
                return invoices;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<InvoiceViewModel> Save(InvoiceViewModel viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                InvoiceViewModel invoice = await this.BusinessLayer.Create(viewModel);
                return invoice;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<InvoiceViewModel> Update(InvoiceViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                InvoiceViewModel invoice = await this.BusinessLayer.Update(viewModel);
                return invoice;
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
        [HttpGet]
        public async Task<IEnumerable<InvoiceDocumentViewModel>> GetGeneratedInvoices(int id, string claimNo)
        {
            try
            {
                //var applicationUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + ConfigurationManager.AppSettings.Get("InvoiceImageLocation");
                var applicationUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + ConfigurationManager.AppSettings.Get("InvoicePdfLocation");
                IEnumerable<InvoiceDocumentViewModel> generatedInvoices = await this.InvoiceDocumnetMappingBusinessLayer.GetAllGeneratedInvoices(User.Identity.GetUserId(), id, applicationUrl, claimNo);
                return generatedInvoices;

            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}
