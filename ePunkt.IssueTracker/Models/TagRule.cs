using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePunkt.IssueTracker.Models
{
    public class TagRule
    {
        public int Id { get; set; }
        [DisplayName("Group")]
        [Required]
        public string Group { get; set; }
        [DisplayName("Tag")]
        [Required]
        public string Tag { get; set; }
        [DisplayName("RegEx (Text)")]
        public string TextRegex { get; set; }
        [DisplayName("RegEx (Creator)")]
        public string CreatorRegex { get; set; }
        [DisplayName("RegEx (Server Variables)")]
        public string ServerVariablesRegex { get; set; }
        [DisplayName("RegEx (Stack Trace)")]
        public string StackTraceRegex { get; set; }
    }
}