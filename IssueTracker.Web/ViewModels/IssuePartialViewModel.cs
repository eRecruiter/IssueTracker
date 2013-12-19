
namespace IssueTracker.Web.ViewModels {
    public abstract class IssuePartialViewModel {

        protected IssuePartialViewModel(Models.Issue issue) {
            Id = issue.Id;
            Creator = issue.Creator;

            Status = issue.Status;
            AssignedTo = issue.AssignedTo;
            Text = issue.Text;
        }

        public int Id { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string Text { get; set; }

    }
}