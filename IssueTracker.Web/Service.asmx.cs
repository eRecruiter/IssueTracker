using System.Linq;
using System.Web.Services;
using IssueTracker.Web.Models;

namespace IssueTracker.Web {
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class IssueTrackerService : WebService {

        [WebMethod]
        public int CreateIssue(string creator, string text, string stackTrace, string serverVariables) {
            var issue = Issue.Create(creator, text, stackTrace, serverVariables);
            return issue == null ? 0 : issue.Id;
        }

        [WebMethod]
        public void AddComment(int issueId, string creator, string text) {
            using (var context = new Db()) {
                var issue = context.Issues.FirstOrDefault(x => x.Id == issueId);
                if (issue != null)
                    issue.AddComment(creator, text);
            }
        }

        [WebMethod]
        public void AddAttachment(int issueId, string creator, string attachmentFileName, string attachmentBase64) {
            using (var context = new Db()) {
                var issue = context.Issues.FirstOrDefault(x => x.Id == issueId);
                if (issue != null)
                    issue.AddAttachment(creator, attachmentFileName, attachmentBase64, Server);
            }
        }
    }
}
