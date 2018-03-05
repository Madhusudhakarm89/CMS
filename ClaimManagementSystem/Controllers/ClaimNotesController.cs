
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

    public class ClaimNotesController : BaseApiController
    {
        private readonly IClaimNotesBusinessLayer _businessLayer;

        public ClaimNotesController()
        {
            this._businessLayer = new ClaimNotesBusinessLayer();
        }

        private IClaimNotesBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotes(int claimId)
        {
            try
            {
                IEnumerable<ClaimNotesViewModel> claimNotes = await this.BusinessLayer.GetAllClaimNotes(User.Identity.GetUserId(), claimId);
                return claimNotes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ClaimNotesViewModel> Find(int id)
        {
            try
            {
                ClaimNotesViewModel claimNote = await this.BusinessLayer.Find(id);
                return claimNote;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        //[HttpPost]
        //public async Task<IEnumerable<ClaimNotesViewModel>> Find(IEnumerable<FilterParameterViewModel> viewModel)
        //{
        //    try
        //    {
        //        IEnumerable<ClaimNotesViewModel> claimNotes = await this.BusinessLayer.Find(viewModel);
        //        return claimNotes;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ExceptionLogger.Log(LogLevel.Error, ex);
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public async Task<ClaimNotesViewModel> Save(ClaimNotesViewModel viewModel)
        {
            try
            {
                if (viewModel.AssignedToUser == null)
                {
                    viewModel.AssignedToUser = new UserViewModel
                    {
                        UserId = User.Identity.GetUserId()
                    };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Type) && !viewModel.Type.Equals("Note", StringComparison.InvariantCultureIgnoreCase))
                        viewModel.AssignedToUser.UserId = User.Identity.GetUserId();
                }

                viewModel.CreatedBy = User.Identity.GetUserId();
                viewModel.CreatedOn = DateTime.Now;
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                ClaimNotesViewModel claimNote = await this.BusinessLayer.Create(viewModel);
                return claimNote;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ClaimNotesViewModel> Update(ClaimNotesViewModel viewModel)
        {
            try
            {
                if (viewModel.AssignedToUser == null)
                {
                    viewModel.AssignedToUser = new UserViewModel
                    {
                        UserId = User.Identity.GetUserId()
                    };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Type) && viewModel.Type.Equals("Note", StringComparison.InvariantCultureIgnoreCase))
                        viewModel.AssignedToUser.UserId = User.Identity.GetUserId();
                }

                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                ClaimNotesViewModel claimNote = await this.BusinessLayer.Update(viewModel);
                return claimNote;
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
        public async Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssignedToMe()
        {
            try
            {
                IEnumerable<ClaimNotesViewModel> claimNotes = await this.BusinessLayer.GetClaimNotesAssigneToMe(User.Identity.GetUserId());
                return claimNotes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IEnumerable<ClaimNotesViewModel>> GetClaimNotesAssignedByMe()
        {
            try
            {
                IEnumerable<ClaimNotesViewModel> claimNotes = await this.BusinessLayer.GetClaimNotesAssignedByMe(User.Identity.GetUserId());
                return claimNotes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ClaimNotesViewModel>> GetOverdueTasks()
        {
            try
            {
                var claimNotes = await this.BusinessLayer.GetOverdueTasks(User.Identity.GetUserId());
                if (claimNotes != null)
                {
                    IEnumerable<ClaimNotesViewModel> claimNotesList = claimNotes.GroupBy(e => e.ClaimId).Select(e => e.First());
                    return claimNotesList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        
    }
}
