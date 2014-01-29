namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoteHostForIssues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "RemoteHost", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issue", "RemoteHost");
        }
    }
}
