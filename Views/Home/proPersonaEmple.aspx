<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.actividades>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Procedimiento por persona
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>

<h2>Procedimientos</h2>
<br />

    <table>
        <tr>
            <th>Codigo</th>
            <th>Nombre</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink(item.procedimiento.pro_codigoun, "actividadesProcedimientoEmple", new { id_user = ViewData["id_user"], id_pro = item.procedimiento.pro_id })%>
            </td>
            <td>
                <%= Html.Encode(item.procedimiento.pro_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>
<%= Html.ActionLink("Atras", "IndexEmple")%>
</asp:Content>
