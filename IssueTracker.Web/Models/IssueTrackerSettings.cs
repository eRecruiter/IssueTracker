
namespace ePunkt.IssueTracker.Web.Models
{
    public static class IssueTrackerSettings
    {

        public static string AttachmentsPath
        {
            get
            {
                return Utilities.Settings.Get("AttachmentsPath", "~/App_Data/Attachments");
            }
        }

        public static string StatusForNewIssues
        {
            get
            {
                return Utilities.Settings.Get("StatusForNewIssues", "New");
            }
        }

        public static int IssuesPerPage
        {
            get
            {
                return Utilities.Settings.Get("IssuesPerPage", 50);
            }
        }

    }
}