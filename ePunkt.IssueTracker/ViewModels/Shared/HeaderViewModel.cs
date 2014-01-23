using System.Globalization;
using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.ViewModels.Shared
{
    public class HeaderViewModel
    {
        private readonly Db _db;
        private readonly ViewDataDictionary _viewData;

        public HeaderViewModel(Db db, ViewDataDictionary viewData)
        {
            _viewData = viewData;
            _db = db;
        }

        public HeaderViewModel Fill(IPrincipal user, UserOptions userOptions)
        {
            CurrentUser = Utils.GetCurrentUser(_db, _viewData, user);

            var usedStati = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "-",
                    Text = "< All >"
                }
            };
            usedStati.AddRange(
                    from x in Utils.GetAllStati(_db, _viewData)
                    select new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name
                    }
                );
            AvailableStati = usedStati;

            var usedUsers = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "-",
                    Text = "< All >"
                },
                new SelectListItem
                {
                    Value = "--",
                    Text = "< None >"
                }
            };
            usedUsers.AddRange(
                    from x in Utils.GetAllUsers(_db, _viewData)
                    select new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name
                    }
                );
            AvailableUsers = usedUsers;


            CurrentSorting = userOptions.Sorting ?? "";
            CurrentDateFilter = userOptions.DateFilter.HasValue ? userOptions.DateFilter.Value.ToString(CultureInfo.InvariantCulture) : "";
            CurrentUserFilter = userOptions.UserFilter ?? "";
            CurrentStatusFilter = userOptions.StatusFilter ?? "";

            return this;
        }

        public IEnumerable<SelectListItem> AvailableStati { get; set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }
        public User CurrentUser { get; set; }

        public string CurrentSorting { get; set; }
        public string CurrentStatusFilter { get; set; }
        public string CurrentUserFilter { get; set; }
        public string CurrentDateFilter { get; set; }
    }
}