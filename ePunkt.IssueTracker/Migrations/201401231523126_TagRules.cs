namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TagRules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueTag",
                c => new
                    {
                        Id = c.Int(false, true),
                        IssueId = c.Int(false),
                        Tag = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issue", t => t.IssueId, true)
                .Index(t => t.IssueId);

            CreateTable(
                "dbo.TagRule",
                c => new
                    {
                        Id = c.Int(false, true),
                        Tag = c.String(),
                        TextRegex = c.String(),
                        CreatorRegex = c.String(),
                        ServerVariablesRegex = c.String(),
                        StackTraceRegex = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            DropTable("dbo.DiscardRule");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.DiscardRule",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(),
                        Creator = c.String(),
                        ServerVariables = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            DropForeignKey("dbo.IssueTag", "IssueId", "dbo.Issue");
            DropIndex("dbo.IssueTag", new[] { "IssueId" });
            DropTable("dbo.TagRule");
            DropTable("dbo.IssueTag");
        }
    }
}
