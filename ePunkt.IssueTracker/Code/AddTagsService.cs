using ePunkt.IssueTracker.Models;
using ePunkt.Utilities;
using System.Linq;
using System.Text.RegularExpressions;

namespace ePunkt.IssueTracker.Code
{
    public class AddTagsService
    {
        private readonly Db _db;

        public AddTagsService(Db db)
        {
            _db = db;
        }

        public void AddTagsForAllIssues()
        {
            var issues = _db.Issues.Where(x => !x.ParentIssueId.HasValue).ToList();
            foreach (var issue in issues)
                AddTags(issue);
        }

        public void AddTags(Issue issue)
        {
            var allTagsRules = _db.TagRules.ToList();

            foreach (var tagRule in allTagsRules)
                if (IsMatch(tagRule.CreatorRegex, issue.Creator)
                    || IsMatch(tagRule.TextRegex, issue.Text)
                    || IsMatch(tagRule.ServerVariablesRegex, issue.ServerVariables)
                    || IsMatch(tagRule.StackTraceRegex, issue.StackTrace))
                    AddTag(issue, tagRule);
        }

        private bool IsMatch(string pattern, string text)
        {
            if (pattern.IsNoE())
                return false;

            foreach (var patternLine in pattern.Split('\n').Select(x => x.Trim()).Where(x => x.HasValue()))
            {
                var isMatch = Regex.IsMatch(text, patternLine, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (isMatch)
                    return true;
            }
            return false;
        }

        private void AddTag(Issue issue, TagRule tagRule)
        {
            _db.IssueTags.Add(new IssueTag
            {
                IssueId = issue.Id,
                Tag = tagRule.Tag
            });

            if (tagRule.IssueStatus.HasValue() && _db.Status.Any(x => x.Name == tagRule.IssueStatus))
                issue.Status = tagRule.IssueStatus;

            _db.SaveChanges();
        }
    }
}