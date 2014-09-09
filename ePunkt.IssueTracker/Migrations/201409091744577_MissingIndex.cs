namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MissingIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Comment", "IssueId");
            CreateIndex("Issue", "ParentIssueId");
            CreateIndex("IssueTag", "IssueId");
        }

        public override void Down()
        {
            DropIndex("Comment", "IssueId");
            DropIndex("Issue", "ParentIssueId");
            DropIndex("IssueTag", "IssueId");
        }
    }
}
