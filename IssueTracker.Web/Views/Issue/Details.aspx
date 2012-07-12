<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IssueTracker.ViewModels.Issue.DetailsIssuePartialViewModel>" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <div style="text-align: right; margin: 3px 3px 0 0;">
        <% if (this.GetCurrentUser() != null) { %>
        <input type="button" value="Add a comment" onclick="location='<%= Url.Action("AddComment", "Issue", new { id = Model.Id }) %>';" />
        <input type="button" value="Delete" onclick="location='<%= Url.Action("Delete", "Issue", new { id = Model.Id }) %>';" />
        <div style="margin-top: 3px;">
            <% using (Html.BeginForm("Update", "Issue", new { id = Model.Id })) { %>
            <%= Html.DropDownList("status", Model.AvailableStati) %>
            <%= Html.DropDownList("assignedTo", Model.AvailableUsers) %>
            <% } %></div>
        <% } %>
        <script>
            $("select").change(function () {
                $("form").submit();
            });
        </script>
    </div>
    <div style="float: right; width: 30%; margin-left: 5px;">
        <fieldset>
            <legend>Details</legend>
            <p>
                Issue created on <i>
                    <%= Model.DateOfCreation.ToString() %></i> by <i>
                        <%= Model.Creator %></i></p>
            <% if (Model.IsPublic) { %>
            <p>
                This issue is public.</p>
            <% } %>
            <% if (Model.ParentIssueId.HasValue) { %>
            <p>
                This issue has a
                <%= Html.ActionLink("parent", "Details", new { id = Model.ParentIssueId.Value })%>.
            </p>
            <% } %>
        </fieldset>
        <fieldset>
            <legend>Comments</legend>
            <% var count = 0;
               foreach (var comment in Model.Comments) { %>
            <div class="comment<%= count ++ % 2 == 0 ? "" : " alternating" %>">
                <% Html.RenderPartial("Comment", comment); %>
            </div>
            <% } %>
        </fieldset>
    </div>
    <fieldset>
        <legend>Text</legend>
        <pre>
            <%: Model.Text %>
            </pre>
    </fieldset>
    <fieldset>
        <legend>Stack trace</legend>
        <pre>
                <%: Model.StackTrace %>
            </pre>
    </fieldset>
    <fieldset>
        <legend>Server variables</legend>
        <pre>
                <%: Model.ServerVariables %>
            </pre>
    </fieldset>
</asp:Content>
