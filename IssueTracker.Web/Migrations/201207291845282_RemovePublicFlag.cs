// ReSharper disable once CheckNamespace
namespace IssueTracker.Web.Migrations {
    using System.Data.Entity.Migrations;

    public partial class RemovePublicFlag : DbMigration {
        public override void Up() {
            DropColumn("Issue", "IsPublic");
        }

        public override void Down() {
            AddColumn("Issue", "IsPublic", c => c.Boolean(nullable: false));
        }
    }
}
