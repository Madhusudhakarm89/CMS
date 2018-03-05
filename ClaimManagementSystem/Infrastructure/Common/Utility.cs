using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PI.Web.Infrastructure.Common
{
    public static class Utility
    {
        public static string GetValuefromWebConfigKey(string KeyName)
        {
            return WebConfigurationManager.AppSettings[KeyName];
        }

        public static string RemoveLineBreaksFromStartAndEnd(string textToCheck)
        {
            if (!String.IsNullOrWhiteSpace(textToCheck) && textToCheck.Contains("<br />"))
            {
                if (textToCheck.Trim().Equals("<br />"))
                {
                    return string.Empty;
                }

                if (textToCheck.Trim().IndexOf("<br />") == 0)
                {
                    textToCheck = RemoveLineBreaksFromStartAndEnd(textToCheck.Substring(6));
                }
                else if (textToCheck.Trim().IndexOf("<br />") == textToCheck.Length - 6)
                {
                    textToCheck = RemoveLineBreaksFromStartAndEnd(textToCheck.Substring(0, textToCheck.Length - 6));
                }
            }
            return textToCheck;
        }
    }
}