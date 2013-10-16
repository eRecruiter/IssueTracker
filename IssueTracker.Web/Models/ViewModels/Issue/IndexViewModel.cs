using ePunkt.Utilities;
using IssueTracker.Web.Code;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IssueTracker.Web.Models.ViewModels.Issue {
    public class IndexViewModel {

        public IndexViewModel(Db db, User currentUser, ViewDataDictionary viewData) {
            CurrentUser = currentUser;

            var usedStati = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "< all >",
                    Value = "",
                    Selected = StatusFilter.Is("")
                }
            };
            usedStati.AddRange(
                    from x in Utils.GetAllStati(db, viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = StatusFilter.Is(x.Name)
                    }
                );
            AvailableStati = usedStati;


            var usedUsers = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "< all >",
                    Value = "",
                    Selected = StatusFilter.Is("")
                },
                new SelectListItem
                {
                    Text = "< none >",
                    Value = "-",
                    Selected = StatusFilter.Is("")
                }
            };
            usedUsers.AddRange(
                    from x in Utils.GetAllUsers(db, viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = AssignedToFilter.Is(x.Name)
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

        public int? DuplicateId { get; set; }

        public string AssignedToFilter { get; set; }
        public string Order { get; set; }
        public string StatusFilter { get; set; }
        public int TimeFilter { get; set; }
        public string TextFilter { get; set; }

        public IEnumerable<SelectListItem> AvailableStati { get; set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }
        public User CurrentUser { get; set; }

        public IEnumerable<SelectListItem> AvailableOrders {
            get {
                var orders = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Comments",
                        Value = "comments",
                        Selected = Order == "comments"
                    },
                    new SelectListItem
                    {
                        Text = "Date",
                        Value = "date",
                        Selected = Order == "date"
                    },
                    new SelectListItem
                    {
                        Text = "Status",
                        Value = "status",
                        Selected = Order == "status"
                    }
                };
                return orders;
            }
        }


        public IEnumerable<SelectListItem> AvailableTimes {
            get {
                var times = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "< all >",
                        Value = "0",
                        Selected = TimeFilter == 0
                    },
                    new SelectListItem
                    {
                        Text = "last week",
                        Value = "7",
                        Selected = TimeFilter == 7
                    },
                    new SelectListItem
                    {
                        Text = "last month",
                        Value = "30",
                        Selected = TimeFilter == 30
                    },
                    new SelectListItem
                    {
                        Text = "last quarter",
                        Value = "90",
                        Selected = TimeFilter == 90
                    },
                    new SelectListItem
                    {
                        Text = "last year",
                        Value = "365",
                        Selected = TimeFilter == 365
                    }
                };

                return times;
            }
        }

    }
}