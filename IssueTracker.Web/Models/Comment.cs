using System;

namespace IssueTracker.Web.Models {
    public class Comment {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string Creator { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Text { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentNiceName { get; set; }
        public int? DuplicateIssueId { get; set; }
        public string Email { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string OldAssignedTo { get; set; }
        public string NewAssignedTo { get; set; }

        public Issue Issue { get; set; }
    }
}