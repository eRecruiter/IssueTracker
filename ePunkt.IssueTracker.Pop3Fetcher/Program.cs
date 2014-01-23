using ePunkt.IssueTracker.Client;
using ePunkt.Utilities;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Text.RegularExpressions;
using System.Threading;

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
                return;
            }

            try
            {
                Console.WriteLine("\tConnecting to POP3 server ...");

                using (var pop3Client = new Pop3Client())
                {
                    pop3Client.Connect(host, port, useSsl);
                    if (username.HasValue())
                        pop3Client.Authenticate(username, password);

                    Console.WriteLine("\tFetching message count ...");
                    var messageCount = pop3Client.GetMessageCount();

                    for (var i = 1; i <= messageCount; i++)
                    {
                        Console.WriteLine("\tFetching message #{0} ...", i);
                        var message = pop3Client.GetMessage(i);

                        if (PushToIssueTracker(message))
                            pop3Client.DeleteMessage(i);
                    }

                    pop3Client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to download mails: " + ex);
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
                text = text.Replace("&gt;", ">");
                text = text.Replace("&lt;", "<");
                text = text.Replace("&quot;", "\"");

                if (creator.IsNoE() && text.IsNoE() && stackTrace.IsNoE() && serverVariables.IsNoE())
                    return false;

                if (message.Headers.Sender != null && message.Headers.Sender.Address.HasValue())
                    creator += " (" + message.Headers.Sender.Address + ")";
                else if (message.Headers.ReplyTo != null && message.Headers.ReplyTo.Address.HasValue())
                    creator += " (" + message.Headers.ReplyTo.Address + ")";

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
    }
}
