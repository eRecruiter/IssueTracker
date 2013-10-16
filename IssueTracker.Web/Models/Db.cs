using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IssueTracker.Web.Models {
    public class Db : DbContext {

        public Db() : base("Db") { }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Status> Status { get; set; }
        public IDbSet<DiscardRule> DiscardRules { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
          
            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.Issue)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.IssueId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Issue>()
                .HasOptional(x => x.ParentIssue)
                .WithMany(x => x.ChildIssues)
                .HasForeignKey(x => x.ParentIssueId)
                .WillCascadeOnDelete(false);
        }
    }
}