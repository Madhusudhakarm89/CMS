
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
    #endregion

    public class ContactController : BaseApiController
    {
        private readonly IContactBusinessLayer _businessLayer;
        private readonly IUserBusinessLayer _userBusinessLayer;

        public ContactController()
        {
            this._businessLayer = new ContactBusinessLayer();
            this._userBusinessLayer = new UserBusinessLayer();
            
        }
        private IContactBusinessLayer BusinessLayer
        {
            get { return this._businessLayer; }
        }
        private IUserBusinessLayer UserBusinessLayer
        {
            get { return this._userBusinessLayer; }
        }

        [HttpGet]
        public async Task<IEnumerable<ContactViewModel>> GetContacts()
        {
            try
            {
                IEnumerable<ContactViewModel> contacts = await this.BusinessLayer.GetAllContacts(User.Identity.GetUserId());
                return contacts;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ContactViewModel> Find(int id)
        {
            try
            {
                ContactViewModel contact = await this.BusinessLayer.Find(id);
                return contact;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IEnumerable<ContactViewModel>> Find(IEnumerable<ContactViewModel> viewModel)
        {
            try
            {
                IEnumerable<ContactViewModel> contact = await this.BusinessLayer.Find(viewModel);
                return contact;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ContactViewModel> Save(ContactViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.CreatedBy = User.Identity.GetUserId();
                    viewModel.CreatedOn = DateTime.Now;
                    viewModel.LastModifiedBy = User.Identity.GetUserId();
                    viewModel.LastModifiedOn = DateTime.Now;
                    viewModel.IsActive = true;

                    ContactViewModel contact = await this.BusinessLayer.Create(viewModel);

                    if (contact.ContactId > 0)
                    {
                        var user = await UserManager.FindByEmailAsync(viewModel.Email);
                        if (user == null)
                        {
                            user = new ApplicationUser
                            {
                                UserName = viewModel.Email,
                                Salutation = "mr",
                                FirstName = viewModel.FirstName,
                                LastName = viewModel.LastName,
                                Email = viewModel.Email,
                                IsReceiveAlerts = false,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnabled = true,
                                AccessFailedCount = 0,
                                IsApproved = true,
                                IsLockedOut = false,
                                IsActive = true,
                                CreateDate = DateTime.Now,
                                LastModifiedDate = DateTime.Now,
                                Street = viewModel.Street,
                                City = viewModel.City,
                                ProvinceId = viewModel.StateId,
                                CountryId = viewModel.CountryId,
                                PostalCode = viewModel.PostalCode,
                                //UserType = viewModel.UserTypeName,
                                //UserProfile = viewModel.ProfileType,
                                CompanyName = viewModel.CompanyName,
                                PhoneNumber = viewModel.Phone,
                                CellNumber = viewModel.Cell

                            };


                            IdentityResult result = await UserManager.CreateAsync(user, "CMS@1234");
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("InvalidUser", "Unable to create user account for this contact.");
                            }
                        }
                    }

                    return contact;
                }
                else
                {
                    ModelState.AddModelError("InvalidModel", "Please provide valid details for the contact.");
                    return viewModel;
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ContactViewModel> Update(ContactViewModel viewModel)
        {
            try
            {
                viewModel.LastModifiedBy = User.Identity.GetUserId();
                viewModel.LastModifiedOn = DateTime.Now;
                viewModel.IsActive = true;

                ContactViewModel contact = await this.BusinessLayer.Update(viewModel);
                return contact;
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
