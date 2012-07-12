<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IssueTracker.ViewModels.Issue.IndexIssuePartialViewModel>" %>
<td>
    <%= Model.Time.Replace(" ", "&nbsp;") %>
</td>
<td>
    <div class="comments<%= Model.IsPublic ? " public" : "" %>">
        <%: Model.Comments.ToString("N0") %></div>
</td>
<td>
    <%: Model.Status %>
</td>
<td>
    <div class="assignedTo<%= this.GetCurrentUser() != null && (Model.AssignedTo.Is(this.GetCurrentUser().Name) || Model.AssignedTo.Is(this.GetCurrentUser().Username)) ? " own" : "" %>">
        <%: Model.AssignedTo %></div>
</td>
<td>
    <%: Model.Creator ?? "" %>
</td>
<td>
    <pre onclick="window.location = '<%= Url.Action("Details", "Issue", new { id = Model.Id }) %>';"><%: (string.IsNullOrWhiteSpace(Model.Text) ? "< no text >" : Model.Text)%></pre>
</td>
<% if (this.GetCurrentUser() != null) { %>
<td style="text-align:right;">
    <%= Html.CheckBox("issue" + Model.Id) %>
</td>
<% } %>