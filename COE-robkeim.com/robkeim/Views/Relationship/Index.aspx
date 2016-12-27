<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Robkeim.Models.Relationship>>" %>
<%@ Import Namespace="Robkeim.BusinessLogic" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Relationships
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Relationships</h2>

<table>
    <tr>
        <th>Person 1</th>
        <th>Person 2</th>
        <th>Relationship</th>
    </tr>

<% foreach (var item in Model.OrderBy(m => m.Person1)) { %>
    <tr>
        <td><%: Helpers.GetNameFromEnumValue(item.Person1) %></td>
        <td><%: Helpers.GetNameFromEnumValue(item.Person2) %></td>
        <td><%: ((Robkeim.Models.Relation)item.Relation).ToString()%></td>
    </tr>
<% } %>

</table>

</asp:Content>
