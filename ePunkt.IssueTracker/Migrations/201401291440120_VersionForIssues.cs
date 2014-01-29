namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class VersionForIssues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "Version", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issue", "Version");
        }
    }
}
