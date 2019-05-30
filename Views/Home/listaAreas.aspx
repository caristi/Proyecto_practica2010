<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.areas>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporte de áreas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<center>
    <h2>Areas</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Nombre
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Mesa de Ayuda", "areaMesaAyuda", new { id=item.are_id }) %> 
            </td>
            <td>
                <%= Html.Encode(item.are_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    </center>
    <%= Html.ActionLink("Atras", "Index")%>
</asp:Content>

