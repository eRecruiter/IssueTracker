using System;
using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.ViewModels.Issue {
    public class DetailsCommentPartialViewModel {

        public DetailsCommentPartialViewModel(Db db, Comment comment, ViewDataDictionary viewData) {
            var creatorUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.Creator));
            Creator = creatorUser == null ? comment.Creator : creatorUser.Name;
            DateOfCreation = comment.DateOfCreation;
            Text = comment.Text;

            StatusText = "";
            if (comment.DuplicateIssueId.HasValue)
                StatusText += "<div>Added a <a href=\"" + comment.DuplicateIssueId.Value + "\">duplicate</a>.</div>";

            if (comment.NewStatus.HasValue() && comment.OldStatus.HasValue())
                StatusText += "<div>Changed status from <i>" + comment.OldStatus + "</i> to <i>" + comment.NewStatus + "</i>.</div>";
            else if (comment.NewStatus.HasValue())
                StatusText += "<div>Changed status to <i>" + comment.NewStatus + "</i>.</div>";
            else if (comment.OldStatus.HasValue())
                StatusText += "<div>Removed status <i>" + comment.OldStatus + "</i>.</div>";

            var oldUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.OldAssignedTo));
            var newUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.NewAssignedTo));

            if (comment.NewAssignedTo.HasValue() && comment.OldAssignedTo.HasValue())
                StatusText += "<div>Assigned from <i>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</i> to <i>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</i>.</div>";
            else if (comment.NewAssignedTo.HasValue())
                StatusText += "<div>Assigned to <i>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</i>.</div>";
            else if (comment.OldAssignedTo.HasValue())
                StatusText += "<div>Removed assignment from <i>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</i>.</div>";

            if (comment.Email.HasValue())
                StatusText += "<div>Send an e-mail to <i>" + comment.Email + "</i>.</div>";

            if (comment.AttachmentFileName.HasValue())
                StatusText += "<div>Added the attachment <a href=\"../Attachment/" + comment.Id + "\">" + comment.AttachmentNiceName + "</a>.</div>";
        }


        public DateTime DateOfCreation { get; set; }
        public string Creator { get; set; }
        public string Text { get; set; }
        public string StatusText { get; set; }

    }
}