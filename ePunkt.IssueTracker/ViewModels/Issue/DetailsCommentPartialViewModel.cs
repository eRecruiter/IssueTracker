using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using ePunkt.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.ViewModels.Issue {
    public class DetailsCommentPartialViewModel {

        public DetailsCommentPartialViewModel(Db db, Comment comment, ViewDataDictionary viewData) {
            var creatorUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.Creator));
            Creator = creatorUser == null ? comment.Creator : creatorUser.Name;
            DateOfCreation = comment.DateOfCreation.ToCentralEuropeanTime();
            Text = comment.Text;

            StatusText = "";
            if (comment.DuplicateIssueId.HasValue)
                StatusText += "Added a <a href=\"" + comment.DuplicateIssueId.Value + "\">duplicate</a>. ";

            if (comment.NewStatus.HasValue() && comment.OldStatus.HasValue())
                StatusText += "Changed status from <code>" + comment.OldStatus + "</code> to <code>" + comment.NewStatus + "</code>. ";
            else if (comment.NewStatus.HasValue())
                StatusText += "Changed status to <code>" + comment.NewStatus + "</code> .";
            else if (comment.OldStatus.HasValue())
                StatusText += "Removed status <code>" + comment.OldStatus + "</code>. ";

            var oldUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.OldAssignedTo));
            var newUser = Utils.GetAllUsers(db, viewData).FirstOrDefault(x => x.Username.Is(comment.NewAssignedTo));

            if (comment.NewAssignedTo.HasValue() && comment.OldAssignedTo.HasValue())
                StatusText += "Assigned from <code>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</code> to <code>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</code>. ";
            else if (comment.NewAssignedTo.HasValue())
                StatusText += "Assigned to <code>" + (newUser != null ? newUser.Name : comment.NewAssignedTo) + "</code>. ";
            else if (comment.OldAssignedTo.HasValue())
                StatusText += "Removed assignment from <code>" + (oldUser != null ? oldUser.Name : comment.OldAssignedTo) + "</code>. ";

            if (comment.Email.HasValue())
                StatusText += "Sent an e-mail to <code>" + comment.Email + "</code>. ";

            if (comment.AttachmentFileName.HasValue())
                StatusText += "Added the attachment <a href=\"../Attachment/" + comment.Id + "\">" + comment.AttachmentNiceName + "</a>. ";
        }


        public DateTime DateOfCreation { get; set; }
        public string Creator { get; set; }
        public string Text { get; set; }
        public string StatusText { get; set; }

    }
}