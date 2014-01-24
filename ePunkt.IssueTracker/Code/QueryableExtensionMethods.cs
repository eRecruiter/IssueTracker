using System;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using ePunkt.IssueTracker.Models;
using ePunkt.Utilities;
using System.Linq;

namespace ePunkt.IssueTracker.Code
{
    public static class QueryableExtensionMethods
    {

        public static IQueryable<Issue> Sort(this IQueryable<Issue> issues, UserOptions userOptions)
        {
            var sortedIssues = issues.OrderByDescending(x => x.Comments.Count <= 0 ? x.DateOfCreation : x.Comments.Max(y => y.DateOfCreation));
            if (userOptions.Sorting.Is("status"))
                sortedIssues = issues.OrderBy(x => x.Status);
            else if (userOptions.Sorting.Is("comments"))
                sortedIssues = issues.OrderByDescending(x => x.Comments.Count);

            return sortedIssues.AsQueryable();
        }

        public static IQueryable<Issue> Filter(this IQueryable<Issue> issues, UserOptions userOptions)
        {
            if (userOptions.StatusFilter.HasValue() && !userOptions.StatusFilter.Is("-"))
                issues = issues.Where(x => x.Status.ToLower() == userOptions.StatusFilter.ToLower());

            if (userOptions.TextFilter.HasValue() && !userOptions.TextFilter.Is("-"))
                issues = issues.Where(x => SqlMethods.Like(x.Text, "%" + userOptions.TextFilter + "%") || SqlMethods.Like(x.StackTrace, "%" + userOptions.TextFilter + "%") || SqlMethods.Like(x.ServerVariables, "%" + userOptions.TextFilter + "%"));

            if (userOptions.UserFilter.HasValue() && userOptions.UserFilter.Is("--"))
                issues = issues.Where(x => x.AssignedTo == null);
            else if (userOptions.UserFilter.HasValue() && !userOptions.UserFilter.Is("-"))
                issues = issues.Where(x => x.AssignedTo.ToLower() == userOptions.UserFilter.ToLower());

            if (userOptions.DateFilter.HasValue)
                issues = issues.Where(x => DbFunctions.DiffDays(x.DateOfCreation, DateTime.Now) <= userOptions.DateFilter || x.Comments.Any(y => DbFunctions.DiffDays(y.DateOfCreation, DateTime.Now) <= userOptions.DateFilter));

            if (userOptions.TagsFilter.Any())
                issues = issues.Where(x => x.Tags.Any(y => userOptions.TagsFilter.Any(z=>y.Tag.EndsWith(z))));

            return issues;
        }
    }
}