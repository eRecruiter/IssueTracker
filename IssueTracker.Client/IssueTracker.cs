using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;


namespace IssueTracker.Client {
    public class IssueTracker {

        public Configuration Configuration { get; private set; }


        #region Constructor
        public IssueTracker() : this(new Configuration()) {}


        public IssueTracker(Configuration configuration) {
            Configuration = configuration;
        }
        #endregion


        public void Post(Issue issue) {
            Post(null, issue);
        }


        public void Post(int? parentId, Issue issue) {
            var webClient = new WebClient();

            var values = new NameValueCollection();
            if (parentId.HasValue)
                values["parentId"] = parentId.Value.ToString(CultureInfo.InvariantCulture);
            values["text"] = issue.Text;
            values["source"] = issue.Source;
            values["stackTrace"] = issue.StackTrace;
            values["serverVariables"] = issue.ServerVariables;
            values["version"] = issue.Version;

            var response = webClient.UploadValues(Configuration.IssueTrackerUrl, values);
            issue.Id = int.Parse(Encoding.ASCII.GetString(response));
        }

    }
}
