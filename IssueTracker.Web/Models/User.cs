using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Web.Models {
    public class User {

        [Key]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}