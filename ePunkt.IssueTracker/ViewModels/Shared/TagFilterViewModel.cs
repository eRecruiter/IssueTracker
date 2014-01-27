using System;
using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using System.Collections.Generic;
using System.Linq;
using ePunkt.Utilities;

namespace ePunkt.IssueTracker.ViewModels.Shared
{
    public class TagFilterViewModel
    {
        public TagFilterViewModel(Db db, UserOptions userOptions)
        {
            var allTagRules = db.TagRules.OrderBy(x => x.Group).ThenBy(x => x.Tag).ToList();
            var issues = db.Issues.Filter(userOptions).Where(y => !y.ParentIssueId.HasValue);

            var issueTags = issues.SelectMany(y => y.Tags);
            var issuesCount = new List<Tuple<string, int>>();

            if (issueTags.Any())
                issuesCount = (from x in issueTags
                    group x by x.Tag
                    into g
                    select new {Tag = g.Key, Count = g.Count()}).ToList().Select(x => new Tuple<string, int>(x.Tag, x.Count)).ToList();

            var groups = new List<Group>();
            foreach (var groupName in allTagRules.Select(x => x.Group).Distinct())
            {
                var groupNameClosure = groupName;
                var group = new Group
                {
                    Name = groupName,
                    Tags = from x in allTagRules.Where(x => x.Group == groupNameClosure)
                        select new Tag
                        {
                            Name = x.Tag,
                            Count = issuesCount.Where(y => y.Item1 == x.Tag).Sum(y => y.Item2),
                            IsFiltered = userOptions.TagsFilter.Any() && !userOptions.TagsFilter.Any(y => y.Is(x.Tag))
                        }
                };
                groups.Add(group);
            }
            Groups = groups;

            CountUntagged = issues.Count(x => !x.Tags.Any());
            FilterUntagged = userOptions.TagsFilter.Any() && !userOptions.TagsFilter.Any(x => x.Is("untagged"));
        }

        public int CountUntagged { get; set; }
        public bool FilterUntagged { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public class Group
        {
            public string Name { get; set; }
            public IEnumerable<Tag> Tags { get; set; }
        }

        public class Tag
        {
            public string Name { get; set; }
            public int Count { get; set; }
            public bool IsFiltered { get; set; }
        }
    }
}