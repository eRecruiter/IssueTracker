// ReSharper disable once CheckNamespace
namespace IssueTracker.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Password");
        }
    }
}
