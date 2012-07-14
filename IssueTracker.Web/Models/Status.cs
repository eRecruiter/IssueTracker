using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueTracker.Models {
    public class Status {

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        public bool Reactivate { get; set; }
    }
}