<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<script src="<%: Url.Content("~/Scripts/calculations.js") %>" type="text/javascript"></script>

<table>
    <tr>
        <th>People</th>
        <% foreach (var person in ViewBag.People)
           { %>
                <th class="verticalName">
                    <%: person %>
                </th>
        <% } %>
    </tr>

    <% for (int i = 0; i < ViewBag.Length; i++)
       { %>
            <tr>
            <td>
                <%: ViewBag.People[i] %>
            </td>
            <% for (int j = 0; j < ViewBag.Length; j++)
               { %>
               <td>
                    <% if (i != j)
                       { %>
                    <%: Model[i, j]%>
                    <% }
                       else
                       { %>
                        --
                    <% } %>
               </td>
               <%} %>
            </tr>
    <% } %>
</table>

</asp:Content>
