using System;
using System.Security.Principal;
using System.Web.Mvc;
using ePunkt.Utilities;
using IssueTracker.Web.Models;
using System.Linq;

namespace IssueTracker.Web.Code
{
    public class UpdateIssueService
    {
        private readonly Db _db;
        private readonly ViewDataDictionary _viewData;

        public UpdateIssueService(Db db, ViewDataDictionary viewData)
        {
            _viewData = viewData;
            _db = db;
        }

        public void Update(IPrincipal user, int issueId, string newUser, string newStatus, string text)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == issueId);
            if (issue != null)
            {
                var hasChanged = false;

                var comment = new Comment
                {
                    IssueId = issue.Id,
                    DateOfCreation = DateTime.Now,
                    Creator = Utils.GetCurrentUser(_db, _viewData, user).Username
                };

                if (newStatus.HasValue() && Utils.GetAllStati(_db, _viewData).Any(x => x.Name.Is(newStatus)))
                    if (!issue.Status.Is(newStatus))
                    {
                        comment.OldStatus = issue.Status;
                        comment.NewStatus = newStatus;
                        issue.Status = newStatus;
                        hasChanged = true;
                    }

                if (newUser.HasValue() && Utils.GetAllUsers(_db, _viewData).Any(x => x.Name.Is(newUser)))
                {
                    if (!issue.AssignedTo.Is(newUser))
                    {
                        comment.OldAssignedTo = issue.AssignedTo;
                        comment.NewAssignedTo = newUser;
                        issue.AssignedTo = newUser;
                        hasChanged = true;
                    }
                }
                else if (newUser.Is("-"))
                {
                    comment.OldAssignedTo = issue.AssignedTo;
                    comment.NewAssignedTo = "-";
                    issue.AssignedTo = null;
                    hasChanged = true;
                }

                if (text.HasValue())
                {
                    comment.Text = text;
                    hasChanged = true;
                }


                if (hasChanged)
                    _db.Comments.Add(comment);

                _db.SaveChanges();
            }
        }
    }
}