
using System.ComponentModel.DataAnnotations;

namespace ePunkt.IssueTracker.Models
{
    public class IssueTag
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        [Required]
        public string Tag { get; set; }

        public virtual Issue Issue { get; set; }
    }
}