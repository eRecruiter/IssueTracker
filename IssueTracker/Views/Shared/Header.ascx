<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% if (this.GetCurrentUser() != null) { %>
Welcome, 
<%= this.GetCurrentUser().Name %>
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