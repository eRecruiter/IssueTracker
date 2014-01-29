using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;
using System.Web.Mvc;
using ePunkt.IssueTracker.Models;

namespace ePunkt.IssueTracker.Code
{
    public static class Utils
    {
        public static User GetCurrentUser(Db db, ViewDataDictionary viewData, IPrincipal user)
        {
            if (viewData["user"] == null && user.Identity.IsAuthenticated)
            {
                var userinDatabase = db.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                viewData["user"] = userinDatabase;
            }

            return viewData["user"] as User;
        }

        public static IEnumerable<User> GetAllUsers(Db db, ViewDataDictionary viewData)
        {
            if (viewData["users"] == null)
                viewData["users"] = db.Users.OrderBy(x => x.Name).ToList();
            return viewData["users"] as IEnumerable<User>;
        }

        public static IEnumerable<Status> GetAllStati(Db db, ViewDataDictionary viewData)
        {
            if (viewData["stati"] == null)
                viewData["stati"] = db.Status.OrderBy(x => x.Name).ToList();
            return viewData["stati"] as IEnumerable<Status>;
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