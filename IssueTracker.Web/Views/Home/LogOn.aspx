<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IssueTracker.ViewModels.Home.LogOnViewModel>" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <% if (!string.IsNullOrEmpty(Model.Message)) { %>
    <div class="message">
        <%= Model.Message %>
    </div>
    <% } %>
    <% using (Html.BeginForm()) { %>
    <fieldset>
        <legend>User credentials</legend>
        <div>
            Your username:
            <%= Html.TextBox("username")%></div>
        <div>
            Your password:
            <%= Html.Password("password")%></div>
    </fieldset>
    <button>
        Log in</button>
    <% } %>
</asp:Content>
