using System;

namespace ePunkt.IssueTracker.Client {
    public class Issue {

        public Issue() {}

        public Issue(string source, Exception ex, string serverVariables, string version) {
            Source = source;

            if (ex != null) {
                Text = ex.Message;
                StackTrace = GetStackTrace(ex);
            }

            ServerVariables = serverVariables;
            Version = version;
        }


        public Issue(string source, string text, string stackTrace, string serverVariables, string version) {
            Source = source;
            Text = text;
            StackTrace = stackTrace;
            ServerVariables = serverVariables;
            Version = version;
        }


        private string GetStackTrace(Exception ex) {
            var stackTrace = ex.ToString();

            var innerException = ex.InnerException;
            while (innerException != null) {
                stackTrace += Environment.NewLine + "===== Inner Exception =====" + Environment.NewLine + innerException.StackTrace;
                innerException = innerException.InnerException;
            }

            return stackTrace;
        }


        public string Text { get; set; }
        public string StackTrace { get; set; }
        public string ServerVariables { get; set; }
        public string Source { get; set; }
        public string Version { get; set; }
        public int Id { get; internal set; }

    }
}
