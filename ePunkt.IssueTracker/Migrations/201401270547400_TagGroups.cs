namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TagGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagRule", "Group", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TagRule", "Group");
        }
    }
}
