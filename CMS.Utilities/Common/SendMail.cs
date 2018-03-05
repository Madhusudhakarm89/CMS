namespace CMS.Utilities.Common
{
    using System.Net.Mail;
    using System.Text;
    
   public static class EmailHelper
    {
       public static bool SendMail(MailMessage message)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Send(message);
                return true;
            }
            catch { return false; }
        }

    }
}
