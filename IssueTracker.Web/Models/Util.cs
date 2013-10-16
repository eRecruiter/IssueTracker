using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace IssueTracker.Web.Models
{
    public static class Util {

        public static string GetContentType(string fileName) {
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

        public static string MaxLength(string s, int maxLength) {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length > maxLength)
                return s.Substring(0, maxLength);
            return s;
        }

        public static string GetSetting(string key) {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
                throw new Exception("There is no application setting '" + key + "'.");
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetSetting(string key, string defaultValue) {
            var result = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public static int GetSetting(string key, int defaultValue) {
            var result = Convert.ToInt32(ConfigurationManager.AppSettings[key]);
            return result == 0 ? defaultValue : result;
        }

        public static bool GetSetting(string key, bool defaultValue) {
            bool result;
            if (bool.TryParse(ConfigurationManager.AppSettings[key], out result))
                return result;
            return defaultValue;
        }

        public static void SendMail(string fromName, string fromEmail, string toName, string toEmail, string subject, string body) {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(toEmail, toName));
            mail.From = new MailAddress(fromEmail, fromName);

            mail.Body = body;
            mail.Subject = subject;
            mail.IsBodyHtml = true;

            var server = new SmtpClient(GetSetting("SmtpHost", "localhost"), GetSetting("SmtpPort", 25));
            if (!string.IsNullOrEmpty(GetSetting("SmtpUserName", "")))
                server.Credentials = new NetworkCredential(GetSetting("SmtpUserName", ""), GetSetting("SmtpPassword", ""));
            server.EnableSsl = GetSetting("SmtpUseSsl", false);
            server.Send(mail);
        }
    }
}