
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
using ClaimManagementSystem.Models;
    using Microsoft.Owin.Security;
    using CMS.Utilities.Enum;
    using System.Web;
    using System.Configuration;
    #endregion

    public class UserController : BaseApiController
    {
        private readonly IUserBusinessLayer _businessLayer;
        private readonly IAspNetUsersDocumnetMappingBusinessLayer _aspNetUsersDocumentBusinessLayer;
        public UserController()
        {
            this._businessLayer = new UserBusinessLayer();
            this._aspNetUsersDocumentBusinessLayer = new AspNetUsersDocumnetMappingBusinessLayer();
        }
        private IUserBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }

        private IAspNetUsersDocumnetMappingBusinessLayer AspNetUsersDocumentBusinessLayer
        {
            get { return this._aspNetUsersDocumentBusinessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            try
            {
                IEnumerable<UserViewModel> users = await this.BusinessLayer.GetAllUsers();
                
                return users;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<UserViewModel> Find(string id)
        {
            try
            {
                UserViewModel userDetails = await this.BusinessLayer.Find(id);
                return userDetails;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<UserViewModel> Find()
        {
            try
            {
                UserViewModel userDetails = await this.BusinessLayer.Find(User.Identity.GetUserId());
                return userDetails;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ApplicationUser> Create(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDetails = new ApplicationUser
                    {
                        UserName = model.Email,
                        Salutation = "mr",
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IsReceiveAlerts = model.ReceiveAlerts,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        IsApproved = true,
                        IsLockedOut = false,
                        IsActive = true,
                        EmailConfirmed=true,
                        CreateDate = DateTime.Now,
                        LastModifiedDate=DateTime.Now,
                        Street=model.Street,
                        City=model.City,
                        ProvinceId=model.ProvinceId,
                        CountryId=model.CountryId,
                        PostalCode=model.PostalCode,
                        Department=model.Department,
                        //UserType = Convert.ToString(model.UserTypeId),
                        //UserProfile = Convert.ToString(model.ProfileTypeId),
                        UserType=model.UserTypeName,
                        UserProfile=model.ProfileType,
                        CompanyName=model.CompanyName,
                        Status = Convert.ToString(model.StatusId),
                        PhoneNumber=model.Phone,
                        CellNumber=model.Cell
                        
                    };
                   

                    IdentityResult result = await UserManager.CreateAsync(userDetails, "CMS@1234");
                    if (result.Succeeded)
                    {
                       await SignInAsync(userDetails, isPersistent: false);
                    }
                    else
                    {
                        userDetails.Errors = new List<string>();
                        foreach (var error in result.Errors)
                        {
                            userDetails.Errors.Add(error.ToString());
                        }
                    }
                    return userDetails;
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
        public async Task<UserViewModel> Update(UserViewModel updateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await this._businessLayer.Update(updateModel);
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
            return null;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        public async Task<bool> Delete(string id)
        {
            try
            {
                return await this._businessLayer.Delete(id);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IEnumerable<AspNetUsersDocumentViewModel>> GetAspNetUserDocuments()
        {
            try
            {
                UploadFileType filetype = new UploadFileType();

                IEnumerable<AspNetUsersDocumentViewModel> aspNetUserDocuments = await this.AspNetUsersDocumentBusinessLayer.GetAllAspNetUsersDocuments(User.Identity.GetUserId(), UploadFileType.Image);
                return aspNetUserDocuments;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<bool> DeleteAspNetUserDocument(int id)
        {
            try
            {
                bool isDeleted = await this.AspNetUsersDocumentBusinessLayer.Delete(id);
                return isDeleted;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<AspNetUsersDocumentViewModel> GetAspNetUserImage()
        {
            try
            {
                UploadFileType filetype = new UploadFileType();

                AspNetUsersDocumentViewModel aspNetUserImage = await this.AspNetUsersDocumentBusinessLayer.GetAllAspNetUsersImage(User.Identity.GetUserId(), UploadFileType.Image);
                aspNetUserImage.FileLocation= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + ConfigurationManager.AppSettings.Get("ProfileImagesGetLocation") + aspNetUserImage.UserId;
                return aspNetUserImage;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }
       
    }
}
