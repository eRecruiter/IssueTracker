<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IssueTracker.ViewModels.Issue.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <div class="filter">
        <% using (Html.BeginForm(null, null))
           { %>
        <% if (!Model.DuplicateId.HasValue)
           { %>
        Sorting:
        <%= Html.DropDownList("order", Model.AvailableOrders) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        Status:
        <%= Html.DropDownList("statusFilter", Model.AvailableStati) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        Assigned to:
        <%= Html.DropDownList("assignedToFilter", Model.AvailableUsers) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        Date:
        <%= Html.DropDownList("timeFilter", Model.AvailableTimes) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <% } %>
        Text:
        <%= Html.TextBox("search", Model.TextFilter) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button>
            Apply</button>
        <% } %></div>
    <% if (Model.Total > 0)
       { %>
    <div class="counter">
        <%= Model.Start.ToString("N0") + " - " + Model.End.ToString("N0") + " of " + Model.Total.ToString("N0") %>
    </div>
    <% using (Html.BeginForm("Update", "Issue"))
       { %>
    <% if (Model.CurrentUser != null)
       { %>
    <div style="text-align: right; margin: 3px 3px 3px 0;">
        <%= Html.DropDownList("status", Model.AvailableStati) %>
        <%= Html.DropDownList("assignedTo", Model.AvailableUsers) %>
        <input type="submit" name="Save" value="Save" />
        <input type="submit" name="Delete" value="Delete" />
        <input type="checkbox" id="checkAll" />
    </div>
    <% } %>
    <table style="width: 100%;">
        <%
           foreach (var issue in Model.Issues)
           { %>
        <tr id="issue<%= issue.Id %>" class="issue">
            <% Html.RenderPartial("Issue", issue); %>
        </tr>
        <% } %>
    </table>
    <% } %>
    <div class="pager">
        <% for (var i = 1; i < Model.MaxPage; i++)
           { %>
        <%= "&nbsp;&nbsp;&nbsp;" + (i == Model.Page ? i.ToString() : Html.ActionLink(i.ToString(), null, null, new { page = i }, null).ToString()) %>
        <% } %></div>
    <% }
       else
       { %>
    <div class="counter">
        No matching issues found.</div>
    <% } %>
    <script>
      
        $(document).ready(function() {
            <% foreach (var issue in Model.Issues) { %>
            $("#issue" + <%= issue.Id %>).hover(function() {
                $("#issue<%= issue.Id %>").addClass("hover");
            }, function() {
                $("#issue<%= issue.Id %>").removeClass("hover");
            });
            <% } %>

            setTimeout("location = location + '';", 600000);

            $("#checkAll").click(function () {
               
                $(":checkbox").attr("checked", $(this).attr("checked"));
            });
        });    

    </script>
</asp:Content>
