// <auto-generated />
namespace IssueTracker.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class RemovePublicFlag : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201207291845282_RemovePublicFlag"; }
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
