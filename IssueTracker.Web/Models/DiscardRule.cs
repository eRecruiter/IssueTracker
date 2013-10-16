using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Web.Models {
    public class DiscardRule {

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Creator { get; set; }
        public string ServerVariables { get; set; }
        public string StackTrace { get; set; }
    }
}