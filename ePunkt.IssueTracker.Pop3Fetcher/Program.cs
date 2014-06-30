using ePunkt.IssueTracker.Client;
using ePunkt.Utilities;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ePunkt.IssueTracker.Pop3Fetcher
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Starting up ...");
            while (true)
            {
                Console.WriteLine("Beginning a new round...");
                DownloadMailsAndPushToIssueTracker();

                Console.WriteLine("Sleeping ...");
                Thread.Sleep(60000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void DownloadMailsAndPushToIssueTracker()
        {
            var host = Settings.Get("Pop3Host", "");
            var port = Settings.Get("Pop3Port", 110);
            var useSsl = Settings.Get("Pop3UseSsl", false);
            var username = Settings.Get("Pop3Username", "");
            var password = Settings.Get("Pop3Password", "");

            if (host.IsNoE())
            {
                Console.WriteLine("\tNo Pop3Host specified.");
                LogOwnError(new ApplicationException("No Pop3Host specified."));
                return;
            }

            try
            {
                Console.WriteLine("\tConnecting to POP3 server {0}:{1} ({4}) using {2} / {3} ...", host, port, username, password, useSsl ? "SSL" : "no SSL");

                using (var pop3Client = new Pop3Client())
                {
                    pop3Client.Connect(host, port, useSsl);
                    if (username.HasValue())
                        pop3Client.Authenticate(username, password);

                    Console.WriteLine("\tFetching message count ...");
                    var messageCount = pop3Client.GetMessageCount();

                    for (var i = 1; i <= messageCount; i++)
                    {
                        try
                        {
                            Console.WriteLine("\tFetching message {0} / {1} ...", i, messageCount);
                            var message = pop3Client.GetMessage(i);

                            if (PushToIssueTracker(message))
                                pop3Client.DeleteMessage(i);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\tUnable to fetch or push message: " + ex);
                            LogOwnError(new ApplicationException("Unable to fetch or push message: " + ex.Message, ex));
                        }
                    }

                    pop3Client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tUnable to download mails: " + ex);
                LogOwnError(new ApplicationException("Unable to download mails: " + ex.Message, ex));
            }
        }

        private static bool PushToIssueTracker(Message message)
        {
            Console.WriteLine("\t\tPushing message '{0}' to IssueTracker ...", message.Headers.Subject);
            try
            {
                var messageBody = message.FindFirstHtmlVersion().GetBodyAsText();

                var creator = GetMatch(messageBody, "Creator: (.*?)<br");
                var text = GetMatch(messageBody, "Text:<pre>(.*?)</pre>");
                var stackTrace = GetMatch(messageBody, "Stack Trace:<pre>(.*?)</pre>");
                var serverVariables = GetMatch(messageBody, "Server Variables:<pre>(.*?)</pre>");

                if (text.StartsWith("IssueTracker unreachable - "))
                    text = text.Substring("IssueTracker unreachable - ".Length);

                creator = HttpUtility.HtmlDecode(creator);
                text = HttpUtility.HtmlDecode(text);
                stackTrace = HttpUtility.HtmlDecode(stackTrace);
                serverVariables = HttpUtility.HtmlDecode(serverVariables);

                if (creator.IsNoE() && text.IsNoE() && stackTrace.IsNoE() && serverVariables.IsNoE())
                    return false;

                if (message.Headers.Sender != null && message.Headers.Sender.Address.HasValue())
                    creator += " (" + message.Headers.Sender.Address + ")";
                else if (message.Headers.ReplyTo != null && message.Headers.ReplyTo.Address.HasValue())
                    creator += " (" + message.Headers.ReplyTo.Address + ")";

                try
                {
                    var issueTracker = new Client.IssueTracker();
                    issueTracker.Post(new Issue
                    {
                        Text = text,
                        ServerVariables = serverVariables,
                        Source = creator,
                        StackTrace = stackTrace,
                        Version = ""
                    });
                    Console.WriteLine("\t\tMessage pushed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\t\tUnable to push message: " + ex);
                    LogOwnError(new ApplicationException("Unable to push message: " + ex.Message, ex));
                }
                return true;
            }
            catch
            {
                Console.WriteLine("\t\tMessage seems not to be an issue report.");
                return false;
            }
        }

        private static string GetMatch(string messageBody, string pattern)
        {
            var match = Regex.Match(messageBody, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Groups.Count >= 1)
                return match.Groups[1].Value;
            return "";
        }

        private static void LogOwnError(Exception ex)
        {
            try
            {
                var issueTracker = new Client.IssueTracker();
                issueTracker.Post(new Issue
                {
                    Text = ex.Message,
                    ServerVariables = null,
                    Source = "IssueTracker.Pop3Fetcher",
                    StackTrace = ex.ToString(),
                    Version = typeof(Program).Assembly.GetName().Version.ToString()
                });
            }
            catch (Exception innerException)
            {
                Console.WriteLine("Unable to log own error: " + innerException);
            }
        }
    }
}
