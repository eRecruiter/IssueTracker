using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePunkt.IssueTracker.Models {
    public class Status {

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        public bool Reactivate { get; set; }
    }
}