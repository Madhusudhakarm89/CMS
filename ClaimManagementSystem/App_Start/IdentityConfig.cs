
namespace ClaimManagementSystem
{
    #region Namespace
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using System;
    using System.Configuration;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;

    using ClaimManagementSystem.Models;
    using CMS.Utilities.Common;
    #endregion

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            manager.UserLockoutEnabledByDefault = Convert.ToBoolean(ConfigurationManager.AppSettings["UserLockoutEnabledByDefault"].ToString());
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
            manager.MaxFailedAccessAttemptsBeforeLockout = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"].ToString());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure two factor email confirmation
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("CMS Identity Provider"));
            }
            return manager;
        }
    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage mail = new MailMessage
            {
                Subject = message.Subject,
                SubjectEncoding = Encoding.UTF8,
                Body = message.Body,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            mail.To.Add(message.Destination);
            bool isMailSent = EmailHelper.SendMail(mail);
            
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
