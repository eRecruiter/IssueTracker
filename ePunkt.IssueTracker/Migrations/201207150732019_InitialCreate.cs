// ReSharper disable once CheckNamespace
namespace IssueTracker.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Email = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "Status",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Reactivate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "DiscardRule",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(),
                        Creator = c.String(),
                        ServerVariables = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        Creator = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        Text = c.String(),
                        AttachmentFileName = c.String(),
                        AttachmentNiceName = c.String(),
                        DuplicateIssueId = c.Int(),
                        Email = c.String(),
                        OldStatus = c.String(),
                        NewStatus = c.String(),
                        OldAssignedTo = c.String(),
                        NewAssignedTo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Issue", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
            CreateTable(
                "Issue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Creator = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        Status = c.String(),
                        Text = c.String(),
                        StackTrace = c.String(),
                        ServerVariables = c.String(),
                        ParentIssueId = c.Int(),
                        IsPublic = c.Boolean(nullable: false),
                        AssignedTo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Issue", t => t.ParentIssueId)
                .Index(t => t.ParentIssueId);
            
        }
        
        public override void Down()
        {
            DropIndex("Issue", new[] { "ParentIssueId" });
            DropIndex("Comment", new[] { "IssueId" });
            DropForeignKey("Issue", "ParentIssueId", "Issue");
            DropForeignKey("Comment", "IssueId", "Issue");
            DropTable("Issue");
            DropTable("Comment");
            DropTable("DiscardRule");
            DropTable("Status");
            DropTable("User");
        }
    }
}
