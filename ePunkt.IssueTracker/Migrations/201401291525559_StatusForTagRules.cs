namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class StatusForTagRules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagRule", "IssueStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TagRule", "IssueStatus");
        }
    }
}
