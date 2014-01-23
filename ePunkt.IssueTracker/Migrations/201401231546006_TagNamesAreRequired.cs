namespace ePunkt.IssueTracker.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TagNamesAreRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IssueTag", "Tag", c => c.String(nullable: false));
            AlterColumn("dbo.TagRule", "Tag", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TagRule", "Tag", c => c.String());
            AlterColumn("dbo.IssueTag", "Tag", c => c.String());
        }
    }
}
