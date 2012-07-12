
public static class Settings {

    public static string AttachmentsPath {
        get {
            return Util.GetSetting("AttachmentsPath");
        }
    }

    public static string StatusForNewIssues {
        get {
            return Util.GetSetting("StatusForNewIssues");
        }
    }

    public static int IssuesPerPage {
        get {
            return Util.GetSetting("IssuesPerPage", 50);
        }
    }

}