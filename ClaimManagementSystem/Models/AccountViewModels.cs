
namespace ClaimManagementSystem.Models
{
    #region Namespace
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion
    
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter email id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{1,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?!.*<[^>]+>)" + @".*", ErrorMessage = "Html tags are not allowed")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public bool ShowCaptcha { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "Enter Registerd Email")]
        [Required(ErrorMessage = "Please enter a valid email id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{1,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "Enter New Password")]
        [Required(ErrorMessage = "Please provide your new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?!.*<[^>]+>).*^(?=.{8,})(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]*)(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password does not match password policy")]
        public string Password { get; set; }

        [Display(Name = "Re-enter New Password")]
        [Required(ErrorMessage = "Please re-enter your new password")]
        [Compare("Password", ErrorMessage = "The entered passwords do not match")]
        [RegularExpression(@"^(?!.*<[^>]+>)" + @".*", ErrorMessage = "No html tags allowed")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string UserId { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "Enter Current password")]
        [Required(ErrorMessage = "Please enter your current password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?!.*<[^>]+>)" + @".*", ErrorMessage = "Html tags are not allowed")]
        public string OldPassword { get; set; }

        [Display(Name = "Enter New password")]
        [Required(ErrorMessage = "Please enter your new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [RegularExpression(@"^(?!.*<[^>]+>).*^(?=.{8,})(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]*)(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password does not match password policy")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Re-enter New Password")]
        [Required(ErrorMessage = "Please re-enter your new password")]
        [Compare("NewPassword", ErrorMessage = "The entered passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class RegisterUserViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage="Required")]
        [StringLength(5, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 2)]
        public string Salutation { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "First name must be limited to 100 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "Last name must be limited to 100 characters")]
        public string LastName { get; set; }

        public string UserName
        {
            get
            {
                return string.Format("{0}{1}{2}", string.IsNullOrWhiteSpace(Salutation) ? string.Empty : String.Format("{0}{1}", Salutation, " ")
                                                    , string.IsNullOrWhiteSpace(FirstName) ? string.Empty : String.Format("{0}{1}", FirstName, " ")
                                                    , string.IsNullOrWhiteSpace(LastName) ? string.Empty : String.Format("{0}{1}", LastName, " "));
            }
        }

        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "First name must be limited to 100 characters")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{1,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?!.*<[^>]+>).*^(?=.{8,})(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]*)(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password does not match password policy")]
        public string Password { get; set; }
    }

    public class EmailViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CallbackUrl { get; set; }
    }
}