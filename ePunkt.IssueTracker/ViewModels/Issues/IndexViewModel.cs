using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.ViewModels.Issues
{
    public class IndexViewModel
    {
        public IndexViewModel(Db db, ViewDataDictionary viewData)
        {
            var usedStati = new List<SelectListItem>();
            usedStati.AddRange(
                    from x in Utils.GetAllStati(db, viewData)
                    select new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name,
                    }
                );
            AvailableStati = usedStati;


            var usedUsers = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "< none >",
                    Value = "-",
                }
            };
            usedUsers.AddRange(
                    from x in Utils.GetAllUsers(db, viewData)
                    select new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name,
                    }
                );
            AvailableUsers = usedUsers;
        }

        public int Page { get; set; }
        public int MaxPage { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public IEnumerable<IndexIssuePartialViewModel> Issues { get; set; }

        public IEnumerable<SelectListItem> AvailableStati { get; set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }
    }
}