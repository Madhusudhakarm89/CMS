
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using CaptchaMvc.HtmlHelpers;
    using NLog;
    using CMS.Utilities.Common;
    using ClaimManagementSystem.Infrastructure.Extensions;
    using ClaimManagementSystem.Models;
    using System.IO;
    using RazorEngine;
    using RazorEngine.Templating;
    using System.Collections.Generic;
    #endregion

    public class AccountController : BaseController
    {
        // The Authorize Action is the end point which gets called when you access any
        // protected Web API. If the user is not logged in then they will be redirected to 
        // the Login page. After a successful login you can call a Web API.
        [HttpGet]
        public ActionResult Authorize()
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                LoginViewModel model = new LoginViewModel
                {
                    Email = string.Empty,
                    Password = string.Empty,
                    RememberMe = false,
                    ReturnUrl = returnUrl,
                    ShowCaptcha = false
                };

                return View(model);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result;

                    if ((ViewData["ShowCaptcha"] != null && Convert.ToBoolean(ViewData["ShowCaptcha"])) && !this.IsCaptchaValid("Captcha is not valid"))
                    {
                        ModelState.AddModelError("InvalidLogin", Constant.Message.InvalidCaptcha);
                    }
                    else
                    {
                        var user = await UserManager.FindAsync(model.Email, model.Password);
                        if (user != null)
                        {
                            await this.SignInAsync(user, model.RememberMe);

                            // When token is verified correctly, clear the access failed count used for lockout
                            result = await UserManager.ResetAccessFailedCountAsync(user.Id);

                            if (!result.Succeeded)
                            {
                                this.AddModelErrors("InvalidLogin", result);
                                this.ExceptionLogger.Log(LogLevel.Info, "Info", result.Errors);
                            }

                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                            {
                                return RedirectToLocal(model.ReturnUrl);
                            }
                            else
                            {
                                string externalUser = ConfigurationManager.AppSettings.Get("ExternalUser");
                                if (externalUser != user.UserProfile)
                                {
                                    return RedirectToAction("Index", "Home", new { Area = string.Empty });
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Cap", new { Area = string.Empty });
                                }
                            }
                        }
                        else
                        {
                            model.Password = string.Empty;
                            user = await UserManager.FindByEmailAsync(model.Email);

                            if (user != null)
                            {
                                if (await UserManager.IsLockedOutAsync(user.Id))
                                {
                                    // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                                    // the user can not login until the lockout duration has passed
                                    ModelState.AddModelError("InvalidLogin", String.Format(Constant.Message.AccountLocked, ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
                                }
                                else if (await UserManager.GetLockoutEnabledAsync(user.Id))
                                {
                                    // if user is subject to lockouts and the credentials are invalid, record the failure and check if user is lockedout and display message, otherwise,
                                    // display the number of attempts remaining before lockout

                                    result = await UserManager.AccessFailedAsync(user.Id);

                                    if (result.Succeeded)
                                    {
                                        if (await UserManager.IsLockedOutAsync(user.Id))
                                        {
                                            ModelState.AddModelError("InvalidLogin", String.Format(Constant.Message.AccountLocked, ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
                                        }
                                        else
                                        {
                                            int accessFailedCount = await UserManager.GetAccessFailedCountAsync(user.Id);

                                            if (accessFailedCount >= 3)
                                            {
                                                ViewData["ShowCaptcha"] = true;

                                                int attemptsLeft = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"]) - accessFailedCount;
                                                ModelState.AddModelError("InvalidLogin", String.Format("{0}\n{1}", Constant.Message.InvalidCredential, String.Format("You have {0} more attempt(s) before your account gets locked out.", attemptsLeft)));
                                            }
                                            else
                                            {
                                                ModelState.AddModelError("InvalidLogin", Constant.Message.InvalidCredential);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.AddModelErrors("InvalidLogin", result);
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("InvalidLogin", Constant.Message.InvalidCredential);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            try
            {
                var registerUser = new RegisterUserViewModel
                {
                    Email = string.Empty,
                    Salutation = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Password = string.Empty
                };

                return View(registerUser);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDetails = new ApplicationUser
                    {
                        UserName = model.Email,
                        Salutation = model.Salutation,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        IsApproved = true,
                        IsLockedOut = false,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        LastModifiedDate=DateTime.Now
                    };

                    IdentityResult result = await UserManager.CreateAsync(userDetails, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInAsync(userDetails, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        AddModelErrors("RegistrationFailed", result);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddModelErrors("EmailConfirm", result);
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            try
            {
                return PartialView(new ForgotPasswordViewModel { Email = string.Empty });
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userData = await UserManager.FindByEmailAsync(model.Email);

                    if (userData == null || (userData != null && !userData.EmailConfirmed))
                    {
                        ModelState.AddModelError("InvalidUser", Constant.Message.NoUserFound);
                    }
                    else
                    {
                        // Send an email with this link
                        string passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(userData.Id);

                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = userData.Id, code = passwordResetToken }, protocol: Request.Url.Scheme);
                        string emailTemlatePath = Server.MapPath("~/Templates/Email/ForgotPassword.cshtml");

                        var emailDataModel = new EmailViewModel
                        {
                            UserName = userData.UserName,
                            Email = userData.Email,
                            Salutation = userData.Salutation,
                            FirstName = userData.FirstName,
                            LastName = userData.LastName,
                            CallbackUrl = callbackUrl
                        };

                        IdentityMessage message = new IdentityMessage
                        {
                            Destination = userData.Email,
                            Subject = "Reset Password",
                            Body = this.RenderEmailTemplateUsingRazor(emailTemlatePath, emailDataModel)
                        };

                        await UserManager.EmailService.SendAsync(message);

                        TempData["ForgotPasswordViewModel"] = model;
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return PartialView("ForgotPassword", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            try
            {
                ForgotPasswordViewModel model = TempData["ForgotPasswordViewModel"] as ForgotPasswordViewModel;
                return PartialView("ForgotPasswordConfirmation", model);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                {
                    return View("Error");
                }
                else
                {
                    ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel()
                    {
                        Code = code,
                        UserId = userId,
                        ConfirmPassword = String.Empty,
                        Password = String.Empty
                    };

                    return View(resetPasswordViewModel);
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(model.UserId);

                    if (user == null)
                    {
                        ModelState.AddModelError("Unauthorised", Constant.Message.NoUserFound);
                    }
                    IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ResetPasswordConfirmation", "Account", new { Area = string.Empty });
                    }
                    else
                    {
                        ModelState.AddModelError("Unauthorised", result.Errors.FirstOrDefault() ?? "Your request cannot be authorised.");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            try
            {
                return PartialView("ResetPasswordConfirmation");
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            try
            {
                var model = new ChangePasswordViewModel
                {
                    OldPassword = string.Empty,
                    NewPassword = string.Empty,
                    ConfirmPassword = string.Empty
                };

                if (TempData["ChangePasswordViewModel"] != null)
                {
                    model.Success = ((ChangePasswordViewModel)TempData["ChangePasswordViewModel"]).Success;
                    model.Message = ((ChangePasswordViewModel)TempData["ChangePasswordViewModel"]).Message;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.HasPassword())
                    {
                        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            model.Success = true;
                            model.Message = Constant.Message.ChangePasswordSuccess;
                            TempData["ChangePasswordViewModel"] = model;


                            return RedirectToAction("ChangePassword", "Account", new { Area = string.Empty });
                        }
                        else
                        {
                            model.Success = false;
                            model.Message = result.Errors.FirstOrDefault() ?? Constant.Message.ChangePasswordFailure;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

        }

      

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddModelErrors(string exceptionKey, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(exceptionKey, error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771

        }

        [HttpPost]
        public ActionResult SendEmail(string id)
        {
            var result = SendEmailById(id);
            return Content(result.Result.ToString());
        }

       
        public async Task<bool> SendEmailById(string id)
        {
            bool IsSentEmail = false;

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var userData = UserManager.FindById(id);

                    if (userData == null || (userData != null && !userData.EmailConfirmed))
                    {
                        IsSentEmail= false;
                    }
                    else
                    {
                        // Send an email with this link
                        string passwordResetToken = UserManager.GeneratePasswordResetToken(userData.Id);

                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = userData.Id, code = passwordResetToken }, protocol: Request.Url.Scheme);
                        string emailTemlatePath = Server.MapPath("~/Templates/Email/AccountRegistration.cshtml");

                        var emailDataModel = new EmailViewModel
                        {
                            UserName = userData.UserName,
                            Email = userData.Email,
                            Salutation = userData.Salutation,
                            FirstName = userData.FirstName,
                            LastName = userData.LastName,
                            CallbackUrl = callbackUrl
                        };

                        IdentityMessage message = new IdentityMessage
                        {
                            Destination = userData.Email,
                            Subject = "Account Created",
                            Body = this.RenderEmailTemplateUsingRazor(emailTemlatePath, emailDataModel)
                        };

                        await UserManager.EmailService.SendAsync(message);

                     IsSentEmail=true;
                    }
                }
                else
                {
                    IsSentEmail = false;
                }
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }

            return IsSentEmail;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Load and Render the specified email template into string by feeding data from the supplied model
        /// </summary>
        /// <param name="emailTemplate">Path of template file to render</param>
        /// <param name="model">Dailies email model to feed data to the template</param>
        /// <returns>String: Rendered HTML as string</returns>
        private string RenderEmailTemplateUsingRazor(string emailTemplatePath, EmailViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(emailTemplatePath))
                {
                    if (System.IO.File.Exists(emailTemplatePath))
                    {
                        //return this.RenderViewToString(emailTemplatePath, model);
                        string emailTemplate = System.IO.File.ReadAllText(emailTemplatePath);
                        if (!string.IsNullOrWhiteSpace(emailTemplate))
                        {
                            return RazorEngine.Razor.Parse(emailTemplate, model);
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

        #endregion
    }
}