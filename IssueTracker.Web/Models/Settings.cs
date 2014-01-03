
namespace ePunkt.IssueTracker.Web.Models
{
    public static class Settings
    {

        public static string AttachmentsPath
        {
            get
            {
                return ePunkt.Utilities.Settings.Get("AttachmentsPath", "~/App_Data/Attachments");
            }
        }

        public static string StatusForNewIssues
        {
            get
            {
                return ePunkt.Utilities.Settings.Get("StatusForNewIssues", "New");
            }
        }

        public static int IssuesPerPage
        {
            get
            {
                return ePunkt.Utilities.Settings.Get("IssuesPerPage", 50);
            }
        }

    }
}