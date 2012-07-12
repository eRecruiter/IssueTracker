<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IssueTracker.ViewModels.Issue.DetailsCommentPartialViewModel>" %>
<div style="float: right;">
    <%: Model.Creator %>
    |
    <%= Model.DateOfCreation.ToString() %></div>
<div>
    <%= Model.StatusText %>
</div>
<div>
    <%: Model.Text %>
</div>
