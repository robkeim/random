<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Robkeim.ViewModels.TransactionViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style title="currentStyle" type="text/css">
	    @import "/css/demo_page.css";
	    @import "/css/demo_table.css";
    </style>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function ()
        {
            $('#table1').dataTable({
                "sScrollY": "400px",
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": true,
                "bInfo": false,
                "bAutoWidth": false
            });
        });
    </script>
    <h2>History</h2>
    <p>
    Here is all of the information that we have collected from previous exchanges, so you can see who you've given to/received from in the past.
    </p>
    <table id="table1">
        <thead>
            <tr>
                <th>
                    Giver
                </th>
                <th>
                    Receiver
                </th>
                <th>
                    Year
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%: Html.DisplayFor(modelItem => item.Giver) %>
                </td>
                <td>
                    <%: Html.DisplayFor(modelItem => item.Receiver) %>
                </td>
                <td>
                    <%: Html.DisplayFor(modelItem => item.Year.Year) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</asp:Content>
