using CMS.BusinessLibrary;
using CMS.BusinessLibrary.ViewModels;
using CMS.Utilities.Enum;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using ClaimManagementSystem.Models;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.html;

namespace ClaimManagementSystem.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IClaimDocumentMappingBusinessLayer _claimDocumentBusinessLayer;
        private readonly IAspNetUsersDocumnetMappingBusinessLayer _aspNetUsersDocumentBusinessLayer;
        private readonly IInvoiceBusinessLayer _invoiceBusinessLayer;
        private readonly ITimeLogBusinessLayer _timeLogBusinessLayer;
        private readonly IInvoiceDocumnetMappingBusinessLayer _invoiceDocumnetMappingBusinessLayer;
        public static int counter = 1;
        public static string fileLocation = "";

        public HomeController()
        {
            this._claimDocumentBusinessLayer = new ClaimDocumnetMappingBusinessLayer();
            this._aspNetUsersDocumentBusinessLayer = new AspNetUsersDocumnetMappingBusinessLayer();
            this._invoiceBusinessLayer = new InvoiceBusinessLayer();
            this._timeLogBusinessLayer = new TimeLogBusinessLayer();
            this._invoiceDocumnetMappingBusinessLayer = new InvoiceDocumnetMappingBusinessLayer();
        }

        private IClaimDocumentMappingBusinessLayer ClaimDocumentBusinessLayer
        {
            get { return this._claimDocumentBusinessLayer; }
        }

        private IAspNetUsersDocumnetMappingBusinessLayer AspNetUsersDocumentBusinessLayer
        {
            get { return this._aspNetUsersDocumentBusinessLayer; }
        }
        private IInvoiceBusinessLayer InvoiceBusinessLayer
        {
            get { return this._invoiceBusinessLayer; }
        }
        private ITimeLogBusinessLayer TimeLogBusinessLayer
        {
            get { return this._timeLogBusinessLayer; }
        }
        private IInvoiceDocumnetMappingBusinessLayer InvoiceDocumnetMappingBusinessLayer
        {
            get { return this._invoiceDocumnetMappingBusinessLayer; }
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadClaimFiles(int claimId, UploadFileType fileType, IEnumerable<HttpPostedFileBase> claimFiles)
        {
            try
            {
                if (claimFiles != null && claimFiles.Any())
                {
                    string[] allowedFileExtensions = ConfigurationManager.AppSettings.Get("AllowedFileExtension").Split(',');
                    string fileUploadLocation = ConfigurationManager.AppSettings.Get("ClaimFilesUploadLocation");

                    if (!string.IsNullOrWhiteSpace(fileUploadLocation))
                    {
                        fileUploadLocation = Server.MapPath(Path.Combine(fileUploadLocation, claimId.ToString()));

                        if (!Directory.Exists(fileUploadLocation))
                        {
                            Directory.CreateDirectory(fileUploadLocation);
                        }

                        foreach (HttpPostedFileBase postedFile in claimFiles)
                        {
                            if (postedFile.ContentLength > 0 && allowedFileExtensions.Contains(Path.GetExtension(postedFile.FileName).ToLower()))
                            {
                                string uploadFileName = String.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(postedFile.FileName));
                                string filePath = Path.Combine(fileUploadLocation, uploadFileName);

                                switch (fileType)
                                {
                                    case UploadFileType.Document:
                                    case UploadFileType.Image:
                                        ClaimDocumentViewModel documentViewModel = new ClaimDocumentViewModel
                                        {
                                            ClaimId = claimId,
                                            FileType = fileType.ToString(),
                                            FileName = uploadFileName,
                                            FileLocation = fileUploadLocation,
                                            FileDisplayName = postedFile.FileName,
                                            CreatedBy = User.Identity.GetUserId(),
                                            CreatedOn = DateTime.Now,
                                            LastModifiedBy = User.Identity.GetUserId(),
                                            LastModifiedOn = DateTime.Now,
                                            IsActive = true
                                        };
                                        documentViewModel = await ClaimDocumentBusinessLayer.Create(documentViewModel);

                                        if (documentViewModel.DocumentId > 0)
                                        {
                                            postedFile.SaveAs(filePath);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    return Content("");
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return null;
        }

        [HttpPost]
        public async Task<ActionResult> RemoveClaimFiles(IEnumerable<HttpPostedFileBase> claimFiles)
        {
            try
            {
                //ClaimViewModel claim = await this.BusinessLayer.Create(viewModel);
                return null;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadProfileImages(string userId, UploadFileType fileType, IEnumerable<HttpPostedFileBase> profileImages)
        {
            try
            {
                if (profileImages != null && profileImages.Any())
                {
                    string[] allowedFileExtensions = ConfigurationManager.AppSettings.Get("AllowedImageExtension").Split(',');
                    string fileUploadLocation = ConfigurationManager.AppSettings.Get("ProfileImagesUploadLocation");

                    if (!string.IsNullOrWhiteSpace(fileUploadLocation))
                    {
                        fileUploadLocation = Server.MapPath(Path.Combine(fileUploadLocation, userId.ToString()));

                        if (!Directory.Exists(fileUploadLocation))
                        {
                            Directory.CreateDirectory(fileUploadLocation);
                        }

                        foreach (HttpPostedFileBase postedFile in profileImages)
                        {
                            if (postedFile.ContentLength > 0 && allowedFileExtensions.Contains(Path.GetExtension(postedFile.FileName).ToLower()))
                            {
                                // string uploadFileName = String.Format("{0}{1}", "photo", Path.GetExtension(postedFile.FileName));
                                string uploadFileName = String.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(postedFile.FileName));
                                // string uploadFileName = "photo";
                                string filePath = Path.Combine(fileUploadLocation, uploadFileName);

                                switch (fileType)
                                {
                                    case UploadFileType.Image:
                                        AspNetUsersDocumentViewModel documentViewModel = new AspNetUsersDocumentViewModel
                                        {
                                            UserId = userId,
                                            FileType = fileType.ToString(),
                                            FileName = uploadFileName,
                                            FileLocation = fileUploadLocation,
                                            FileDisplayName = postedFile.FileName,
                                            CreatedBy = User.Identity.GetUserId(),
                                            CreatedOn = DateTime.Now,
                                            LastModifiedBy = User.Identity.GetUserId(),
                                            LastModifiedOn = DateTime.Now,
                                            IsActive = true
                                        };
                                        documentViewModel = await AspNetUsersDocumentBusinessLayer.Create(documentViewModel);

                                        if (documentViewModel.DocumentId > 0)
                                        {
                                            postedFile.SaveAs(filePath);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    return Content("");
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return null;
        }
        [HttpPost]
        public async Task<ActionResult> RemoveProfileImages(IEnumerable<HttpPostedFileBase> profileImages)
        {
            try
            {
                //ClaimViewModel claim = await this.BusinessLayer.Create(viewModel);
                return null;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult GenerateIncoicePdf(int ClaimId, int InvoiceId,int TimelogId)
        {
            try
            {
                StringBuilder html = new StringBuilder();

                List<InvoiceViewModel> invoices = this.InvoiceBusinessLayer.GetAllInvoicesforPdf(User.Identity.GetUserId(), ClaimId,InvoiceId).ToList();
                IEnumerable<TimeLogViewModel> timeLogs = this.TimeLogBusinessLayer.GetAllTimeLogsforPdf(User.Identity.GetUserId(), ClaimId, TimelogId).Where(x => x.IsBilled == false);
                var SeriveBillingItems = new List<PdfBillingItems>();
                decimal sTotal = 0;
                decimal tax =0;
                decimal fTotal = 0;
                string claimNumber = string.Empty;
                string invoiceNumber = string.Empty;
                PdfViewModel pdfViewModel = null;
                if (invoices != null && timeLogs != null)
                {
                    foreach (var invoice in invoices)
                    {
                        foreach (var timeLog in invoice.TimeLogs)
                        {
                            var serviceItem = new PdfBillingItems();
                            serviceItem.Item = timeLog.ServiceItemName;
                            serviceItem.Description = timeLog.Comment;
                            serviceItem.Quantity = timeLog.Quantity;
                            //serviceItem.Rate = timeLog.ServiceRate;
                            serviceItem.Price = timeLog.TotalAmount;
                            serviceItem.Rate = decimal.Round(timeLog.ServiceRate, 2, MidpointRounding.AwayFromZero);
                            sTotal += timeLog.TotalAmount;
                            SeriveBillingItems.Add(serviceItem);
                        }
                        claimNumber = invoice.ClaimNumber;
                        invoiceNumber = invoice.InvoiceNumber;
                    }
                    foreach (var timeLog in timeLogs)
                    {
                        var serviceItem = new PdfBillingItems();
                        serviceItem.Item = timeLog.ServiceItemName;
                        serviceItem.Description = timeLog.Comment;
                        serviceItem.Quantity = timeLog.Quantity;
                        serviceItem.Rate = decimal.Round(0, 2, MidpointRounding.AwayFromZero);
                        serviceItem.Price = decimal.Round(0, 2, MidpointRounding.AwayFromZero);
                        SeriveBillingItems.Add(serviceItem);
                    }
                    fTotal = tax + sTotal;
                    pdfViewModel = new PdfViewModel
                    {
                        ImagePath = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + ConfigurationManager.AppSettings.Get("InvoiceImageLocation") + "InvoiceImage.png",
                        BgImagePath = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + ConfigurationManager.AppSettings.Get("InvoiceImageLocation") + "InvoiceBackground.png",
                        InvoiceNumber = invoices[0].InvoiceNumber,
                        InvoiceDate = invoices[0].InvoiceDate,
                        InvoiceDueDate = invoices[0].DueDate,
                        Insured = invoices[0].ClaimantName,
                        InsuredPolicy = invoices[0].PolicyNumber,
                        FileNumber = invoices[0].FileNumber,
                        Adjuster = invoices[0].AdjusterName,
                        LostUnit = invoices[0].LossType,
                        LostDate = invoices[0].LostDate,
                        CompanyName = invoices[0].CompanyName,
                        Street=invoices[0].Street,
                        City=invoices[0].City,
                        ProvinceName=invoices[0].ProvinceName,
                        CountryName=invoices[0].CountryName,
                        PostalCode=invoices[0].PostalCode,
                        ContactName=invoices[0].ContactName,
                        BillingItems = SeriveBillingItems,
                        SubTotal = sTotal,
                        Tax = tax,
                        Total = fTotal
                    };
                }
                string invoiceTemlatePath = Server.MapPath("~/Templates/Invoice/InvoicePdf.cshtml");
                //var emailDataModel = new EmailViewModel
                //{
                //    UserName ="TEst1",
                //    CallbackUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + ConfigurationManager.AppSettings.Get("InvoiceImageLocation")
                //};
                html.Append(RenderEmailTemplateUsingRazor(invoiceTemlatePath, pdfViewModel).ToString());

                string dirPath = GetDataDir_Data(User.Identity.GetUserId(), claimNumber, invoiceNumber) + ".pdf";
                FileStream fs = new FileStream(dirPath, FileMode.Create, FileAccess.Write, FileShare.None);
                StreamReader sr = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(html.ToString())));
                Document doc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
                HTMLWorker htmlParser = new HTMLWorker(doc);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                htmlParser.Parse(sr);
                //var htmlContext = new HtmlPipelineContext(null);
                //htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                //ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                //cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/Site/StyleSheets/Bootstrap/bootstrap.css"), true);
                //cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/Site/StyleSheets/Site.css"), true);
                //cssResolver.AddCss(".shadow {background-color: #ebdddd; }", true);
                //IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(doc, writer)));

                //var worker = new XMLWorker(pipeline, true);
                //var xmlParse = new XMLParser(true, worker);

                //xmlParse.Parse(sr);
                //xmlParse.Flush();
                doc.Close();
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename="+dirPath);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Write(doc);
                //Response.End();

                InvoiceDocumentViewModel documentViewModel = new InvoiceDocumentViewModel
                {
                    UserId = User.Identity.GetUserId(),
                    FileType = "Pdf",
                    FileName = dirPath,
                    FileLocation = fileLocation,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
                    LastModifiedBy = User.Identity.GetUserId(),
                    LastModifiedOn = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
                    IsActive = true,
                    ClaimId = ClaimId
                };
                documentViewModel = InvoiceDocumnetMappingBusinessLayer.Create(documentViewModel);

                if (documentViewModel.DocumentId > 0)
                {
                    return Content("True");
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
            return Content("False");
        }

        public string GetDataDir_Data(string userId, string claimNumber, string invoiceId)
        {
            try
            {
                string fileUploadLocation = ConfigurationManager.AppSettings.Get("InvoicesUploadLocation");

                if (!string.IsNullOrWhiteSpace(fileUploadLocation))
                {
                    fileUploadLocation = Server.MapPath(Path.Combine(fileUploadLocation, userId.ToString()+"_"+claimNumber));

                    if (!Directory.Exists(fileUploadLocation))
                    {
                        Directory.CreateDirectory(fileUploadLocation);
                        counter = 1;
                    }
                    else
                    {
                        var di = new DirectoryInfo(fileUploadLocation);
                        var newest = GetNewestFile(di);
                        counter = Convert.ToInt32(newest.ToString().Substring(newest.ToString().LastIndexOf('_') + 1, (newest.ToString().Length - newest.ToString().LastIndexOf('_') - 5)))+1;
                    }
                    string uploadFileName = invoiceId + "_" + claimNumber + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + counter;
                    string filePath = Path.Combine(fileUploadLocation, uploadFileName);
                    //counter += 1;
                    fileLocation = fileUploadLocation;
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return "";
        }
        public static FileInfo GetNewestFile(DirectoryInfo directory)
        {
            return directory.GetFiles()
              .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
              .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
              .FirstOrDefault();
        }
        private string RenderEmailTemplateUsingRazor(string pdfTemplatePath, PdfViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pdfTemplatePath))
                {
                    if (System.IO.File.Exists(pdfTemplatePath))
                    {
                        //return this.RenderViewToString(emailTemplatePath, model);
                        string pdfTemplate = System.IO.File.ReadAllText(pdfTemplatePath);
                        if (!string.IsNullOrWhiteSpace(pdfTemplate))
                        {
                            return RazorEngine.Razor.Parse(pdfTemplate, model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return string.Empty;
        }

    }
}
