using System;

namespace IssueTracker.DemoClient {
    internal class Program {
        private static void Main(string[] args) {

            try {
                Console.WriteLine("Posting issue ...");
                var issueTracker = new IssueTracker.Client.IssueTracker();

                var issue = new IssueTracker.Client.Issue {
                    ServerVariables = "Server Variables" + Environment.NewLine + "Server Variables",
                    Source = "Source",
                    StackTrace = "Stack Trace" + Environment.NewLine + "Stack Trace",
                    Text = "Text" + Environment.NewLine + "Text",
                    Version = typeof (Program).Assembly.GetName().Version.ToString()
                };
                issueTracker.Post(issue);

                Console.WriteLine("Issue posted with ID " + issue.Id);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
