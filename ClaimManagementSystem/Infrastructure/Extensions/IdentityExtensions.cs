using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ClaimManagementSystem.Infrastructure.Extensions
{
    public static class IdentityExtensions 
    {
        public static int UserRoleId(this IIdentity identity)
        {
            return !string.IsNullOrWhiteSpace(((ClaimsIdentity)identity).FindFirst("UserProfile").Value) && ((ClaimsIdentity)identity).FindFirst("UserProfile").Value == "Adjuster System Administrator"
                ? 2 : !string.IsNullOrWhiteSpace(((ClaimsIdentity)identity).FindFirst("UserProfile").Value) && ((ClaimsIdentity)identity).FindFirst("UserProfile").Value == "Adjuster Standard User" ? 1 : 3;
        }
        public static string FullName(this IIdentity identity)
        {
            var salutation = ((ClaimsIdentity)identity).FindFirst("Salutation").Value ?? string.Empty;
            var firstName = ((ClaimsIdentity)identity).FindFirst("FirstName").Value ?? string.Empty;
            var lastName = ((ClaimsIdentity)identity).FindFirst("LastName").Value ?? string.Empty;

            return String.Format("{0}{1}"//, !string.IsNullOrWhiteSpace(salutation) ? salutation.Trim() + " " : string.Empty
                                            , !string.IsNullOrWhiteSpace(firstName) ? firstName.Trim() + " " : string.Empty
                                            , !string.IsNullOrWhiteSpace(lastName) ? lastName.Trim() : string.Empty);
            
        }
    }
}