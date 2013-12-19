using ePunkt.Utilities;
using IssueTracker.Web.Code;
using System;
using System.Linq;
using System.Web.Mvc;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.ViewModels.Issue {
    public class DetailsCommentPartialViewModel {

        public DetailsCommentPartialViewModel(Db db, Comment comment, ViewDataDictionary viewData) {
            var creatorUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.Creator));
            Creator = creatorUser == null ? comment.Creator : creatorUser.Name;
            DateOfCreation = comment.DateOfCreation;
            Text = comment.Text;

            StatusText = "";
            if (comment.DuplicateIssueId.HasValue)
                StatusText += "Added a <a href=\"" + comment.DuplicateIssueId.Value + "\">duplicate</a>. ";

            if (comment.NewStatus.HasValue() && comment.OldStatus.HasValue())
                StatusText += "Changed status from <i>" + comment.OldStatus + "</i> to <i>" + comment.NewStatus + "</i>. ";
            else if (comment.NewStatus.HasValue())
                StatusText += "Changed status to <i>" + comment.NewStatus + "</i> .";
            else if (comment.OldStatus.HasValue())
                StatusText += "Removed status <i>" + comment.OldStatus + "</i>. ";

            var oldUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.OldAssignedTo));
            var newUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.NewAssignedTo));

            if (comment.NewAssignedTo.HasValue() && comment.OldAssignedTo.HasValue())
                StatusText += "Assigned from <i>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</i> to <i>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</i>. ";
            else if (comment.NewAssignedTo.HasValue())
                StatusText += "Assigned to <i>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</i>. ";
            else if (comment.OldAssignedTo.HasValue())
                StatusText += "Removed assignment from <i>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</i>. ";

            if (comment.Email.HasValue())
                StatusText += "Sent an e-mail to <i>" + comment.Email + "</i>. ";

            if (comment.AttachmentFileName.HasValue())
                StatusText += "Added the attachment <a href=\"../Attachment/" + comment.Id + "\">" + comment.AttachmentNiceName + "</a>. ";
        }


        public DateTime DateOfCreation { get; set; }
        public string Creator { get; set; }
        public string Text { get; set; }
        public string StatusText { get; set; }

    }
}