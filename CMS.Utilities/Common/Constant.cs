
namespace CMS.Utilities.Common
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    public static class Constant
    {
        public static class Message
        {
            public const string InvalidCredential = "The specified username and password are incorrect.";
            public const string InvalidCaptcha = "Captcha is not valid.";
            public const string InvalidUsername = "The specified username does not exists.";
            public const string InvalidEmailId = "The specified email id does not exists.";
            public const string NoUserFound = "User does not exists.";
            public const string ChangePasswordSuccess = "Your password has been changed successfully.";
            public const string ChangePasswordFailure = "The specified new password does not satisfy the password policy.";
            public const string AccountLocked = "Your account has been locked for {0} minutes.";
        }

        public static string EncryptionKey { get; set; }
    }
}
