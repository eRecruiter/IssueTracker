<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IssueTracker.Models.User>" %>
<% if (Model != null) { %>
Welcome, 
<%= Model.Name %>
|
<%= Html.ActionLink("Log off", "LogOff", "Home") %>
|
<%= Html.ActionLink("Home", "Index", "Issue") %>
<% }
   else { %>
<%= Html.ActionLink("Log on", "LogOn", "Home") %>
|
<%= Html.ActionLink("Home", "Index", "Issue") %>
<% } %>