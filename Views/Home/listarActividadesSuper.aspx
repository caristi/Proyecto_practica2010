<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.actividades>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Actividades con operaciones
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
    <h2>Listar Actividades con indicador de operacion</h2>
<br />
    <table>
        <tr>
            <th></th><th>Codigo actividad</th><th>Nombre actividad</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Indicador de operacion", "mediaOperacionSuper", new { id = item.act_id })%> |
            </td>
            <td>
                <%= Html.Encode(item.act_id) %>
            </td>
            <td>
                <%= Html.Encode(item.act_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>
</asp:Content>