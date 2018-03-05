
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Utilities.Enum;
    using ClaimManagementSystem.Models;

    using Microsoft.AspNet.Identity;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    #endregion

    public partial class ClaimsController : BaseApiController
    {
        private readonly IClaimBusinessLayer _businessLayer;
        private readonly IClaimDocumentMappingBusinessLayer _claimDocumentBusinessLayer;

        public ClaimsController()
        {
            this._businessLayer = new ClaimBusinessLayer();
            this._claimDocumentBusinessLayer = new ClaimDocumnetMappingBusinessLayer();
        }

        private IClaimBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        private IClaimDocumentMappingBusinessLayer ClaimDocumentBusinessLayer
        {
            get { return this._claimDocumentBusinessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimViewModel>> GetClaims()
        {
            try
            {
                IEnumerable<ClaimViewModel> claims = await this.BusinessLayer.GetAllClaims(User.Identity.GetUserId());
                return claims;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IEnumerable<ClaimDocumentViewModel>> GetClaimDocuments(int claimId, UploadFileType fileType)
        {
            try
            {
                IEnumerable<ClaimDocumentViewModel> claimDocuments = await this.ClaimDocumentBusinessLayer.GetAllClaimDocuments(User.Identity.GetUserId(), claimId, fileType);
                return claimDocuments;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ClaimViewModel> Find(int id)
        {
            try
            {
                ClaimViewModel claims = await this.BusinessLayer.Find(id);
                return claims;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ClaimViewModel> Save(ClaimViewModel viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                ClaimViewModel claim = await this.BusinessLayer.Create(viewModel);
                return claim;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ClaimViewModel> Update(ClaimViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                ClaimViewModel claim = await this.BusinessLayer.Update(viewModel);
                return claim;
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

        [HttpPost]
        public async Task<bool> DeleteClaimDocument(int id)
        {
            try
            {
                bool isDeleted = await this.ClaimDocumentBusinessLayer.Delete(id);
                return isDeleted;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimViewModel>> GetAllClaimsDueToday()
        {
            try
            {
                IEnumerable<ClaimViewModel> claimsDueToday = await this.BusinessLayer.GetAllClaimsDueDays(User.Identity.GetUserId(), true);
                return claimsDueToday;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimViewModel>> GetAllClaimsDueIn7Days()
        {
            try
            {
                IEnumerable<ClaimViewModel> claimsDueIn7Days = await this.BusinessLayer.GetAllClaimsDueDays(User.Identity.GetUserId(), false);
                return claimsDueIn7Days;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<AverageDaysOpen>> GetAverageDaysOpen()
        {
            try
            {
                IEnumerable<ClaimViewModel> averageDaysOpenTasks = await this.BusinessLayer.GetAverageDaysOpenTasks();
                List<string> contactList = new List<string>();
                List<double> countList = new List<double>();
                if (averageDaysOpenTasks != null)
                {
                    var averageDaysGroup = averageDaysOpenTasks.GroupBy(p => p.AdjusterName);
                    foreach (var averageDaysItem in averageDaysGroup)
                    {
                        contactList.Add(averageDaysItem.Key);
                        double daysCount = 0;
                        foreach (var item in averageDaysItem)
                        {
                            daysCount += item.Days;
                        }
                        countList.Add(daysCount / averageDaysItem.Count());

                    }
                }
                var averageDaysOpenList = new List<AverageDaysOpen>();// Average date required closed date. as of now expirattion date has considered closed date. In future it will change
                var averageDaysOpen = new AverageDaysOpen();
                averageDaysOpen.Count = countList;
                averageDaysOpen.ContactNames = contactList;
                averageDaysOpenList.Add(averageDaysOpen);
                IEnumerable<AverageDaysOpen> averageDaysOpenEnumerbleList = averageDaysOpenList;
                return averageDaysOpenEnumerbleList;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<OpenClaims>> GetOpenClaims()
        {
            try
            {
                IEnumerable<ClaimViewModel> openClaimsList = await this.BusinessLayer.GetOpenClaims();
                var openClaimsesList = new List<OpenClaims>();
                OpenClaims openClaims = new OpenClaims();
                List<string> colorsList = new List<string>();
                List<Claims> claims = new List<Claims>();
                if (openClaimsList != null)
                {
                    var openClaimsGroup = openClaimsList.GroupBy(p => p.AdjusterName);
                    foreach (var openclaim in openClaimsGroup)
                    {
                        claims.Add(new Claims { ContactName = openclaim.Key, Count = openclaim.Count() });
                        colorsList.Add(GetColor());
                        Thread.Sleep(50);
                    }
                }
                openClaims.Claimses = claims;
                openClaims.Color = colorsList;
                openClaimsesList.Add(openClaims);
                IEnumerable<OpenClaims> openClaimsEnumerableList = openClaimsesList;
                return openClaimsEnumerableList;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        private string GetColor()
        {
            var random = new Random();
            return String.Format("#{0:X6}", random.Next(0x1000000));
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimViewModel>> GetOverdueClaims()
        {
            try
            {
                IEnumerable<ClaimViewModel> overdueTasks = await this.BusinessLayer.GetOverdueClaims(User.Identity.GetUserId());
                return overdueTasks;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
    }
}
