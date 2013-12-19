using IssueTracker.Web.Models;
using System.Linq;

namespace IssueTracker.Web.Code
{
    public class DeleteIssueService
    {
        private readonly Db _db;

        public DeleteIssueService(Db db)
        {
            _db = db;
        }

        public void Delete(int issueId)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == issueId);

            if (issue != null)
            {
                foreach (var childIssueId in _db.Issues.Where(x => x.ParentIssueId == issue.Id).Select(x => x.Id).ToList())
                    Delete(childIssueId);

                _db.Issues.Remove(issue);
                _db.SaveChanges();
            }
        }
    }
}