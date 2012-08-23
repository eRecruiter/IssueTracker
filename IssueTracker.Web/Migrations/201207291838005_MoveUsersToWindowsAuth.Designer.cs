// <auto-generated />
namespace IssueTracker.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class MoveUsersToWindowsAuth : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201207291838005_MoveUsersToWindowsAuth"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return "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"; }
        }
    }
}