using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ePunkt.IssueTracker.Models
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

        public static void SendMail(string fromName, string fromEmail, string toName, string toEmail, string subject, string body)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(toEmail, toName));
            mail.From = new MailAddress(fromEmail, fromName);

            mail.Body = body;
            mail.Subject = subject;
            mail.IsBodyHtml = true;

            var server = new SmtpClient(Utilities.Settings.Get("SmtpHost", "localhost"), Utilities.Settings.Get("SmtpPort", 25));
            if (!string.IsNullOrEmpty(Utilities.Settings.Get("SmtpUserName", "")))
                server.Credentials = new NetworkCredential(Utilities.Settings.Get("SmtpUserName", ""), Utilities.Settings.Get("SmtpPassword", ""));
            server.EnableSsl = Utilities.Settings.Get("SmtpUseSsl", false);
            server.Send(mail);
        }
    }
}