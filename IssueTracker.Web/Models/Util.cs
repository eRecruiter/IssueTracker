using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace IssueTracker.Web.Models
{
    public static class Util
    {

        public static string GetContentType(string fileName)
        {
            if (fileName == null)
                throw new ArgumentException("Parameter 'extension' must not be null");
            var extension = Path.GetFileName(fileName).ToLower().Replace(".", "");

            if (extension == "jpg" || extension == "jpeg")
                return "image/jpeg";
            if (extension == "png")
                return "image/png";
            if (extension == "gif")
                return "image/gif";
            if (extension == "bmp")
                return "image/bmp";
            if (extension == "doc" || extension == "docx")
                return "application/msword";
            if (extension == "xls" || extension == "xlsx")
                return "applicant/excel";

            return "application/octet-stream";

        }

        public static string MaxLength(string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length > maxLength)
                return s.Substring(0, maxLength);
            return s;
        }

        public static void SendMail(string fromName, string fromEmail, string toName, string toEmail, string subject, string body)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(toEmail, toName));
            mail.From = new MailAddress(fromEmail, fromName);

            mail.Body = body;
            mail.Subject = subject;
            mail.IsBodyHtml = true;

            var server = new SmtpClient(ePunkt.Utilities.Settings.Get("SmtpHost", "localhost"), ePunkt.Utilities.Settings.Get("SmtpPort", 25));
            if (!string.IsNullOrEmpty(ePunkt.Utilities.Settings.Get("SmtpUserName", "")))
                server.Credentials = new NetworkCredential(ePunkt.Utilities.Settings.Get("SmtpUserName", ""), ePunkt.Utilities.Settings.Get("SmtpPassword", ""));
            server.EnableSsl = ePunkt.Utilities.Settings.Get("SmtpUseSsl", false);
            server.Send(mail);
        }
    }
}