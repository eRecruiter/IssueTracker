using IssueTracker.Web.Code;
using IssueTracker.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IssueTracker.Web.ViewModels.Issues
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
        public int Total { get; set; }
        public IEnumerable<IndexIssuePartialViewModel> Issues { get; set; }

        public IEnumerable<SelectListItem> AvailableStati { get; set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }
    }
}