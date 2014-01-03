namespace ePunkt.IssueTracker.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class MoveUsersToWindowsAuth : DbMigration
    {
        public override void Up()
        {
            DropColumn("User", "Password");
        }
        
        public override void Down()
        {
            AddColumn("User", "Password", c => c.String());
        }
    }
}
