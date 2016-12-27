<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Robkeim.ViewModels.RegistrationViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    2011
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>2011 CoE</h2>

<p>
    Click <%: Html.ActionLink("here", "Create") %> to register
</p>

<h2>Participating</h2>

<% if (!ViewBag.HaveConfirms)
   { %>
<div>
    None
</div>
<% } else { %>
<table style="width:100%; vertical-align: top">
    <tr>
        <th style="width:25%">
            Person
        </th>
        <th style="width:25%">
            Address
        </th>
        <th style="width:25%">
            Registration date (EST)
        </th>
        <th style="width: 25%">
            Edit
        </th>
    </tr>

<% foreach (var item in Model)
   { %>
    <% if (item.Participating == "Yes")
       { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Person)%>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.AddressLine1)%>
            <br />
            <%: Html.DisplayFor(modelItem => item.AddressLine2) %>
        </td>
        <td>
            <%: String.Format("{0:t} on {0:M/d}", item.Date)%>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id = item.ID })%>
        </td>
    </tr>
    <% } %>
<% } %>

</table>
<% } %>
<h2>Not participating</h2>

<% if (!ViewBag.HaveDeclines)
   { %>
<div>
    None
</div>
<% }
   else
   { %>
<table style="width:100%">
    <tr>
        <th style="width:30%">
            Person
        </th>
        <th style="width:30%">
            Registration date (EST)
        </th>
        <th>
            Edit
        </th>
    </tr>

<% foreach (var item in Model)
   { %>
    <% if (item.Participating == "No")
       { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Person)%>
        </td>
        <td>
            <%: String.Format("{0:t} on {0:M/d}", item.Date)%>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id = item.ID })%>
        </td>
    </tr>
    <% } %>
<% } %>

</table>
<% } %>
</asp:Content>
