<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.usuarios>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
    <h2>Empleados</h2>
<br />
    <table>
        <tr>
            <th></th>
            <th> Nombre</th><th>Apellidos</th><th>Cargo</th><th>Area</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>               
                <%= Html.ActionLink("Procedimiento", "proPersonaSuper", new { id_user = item.usu_id })%>
            </td>
            <td>
                <%= Html.Encode(item.usu_nombres) %>
            </td>
            <td>
                <%= Html.Encode(item.usu_apellidos) %>
            </td>
            <td>
                <%= Html.Encode(item.cargos.car_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.areas.are_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>

<%= Html.ActionLink("Atras", "IndexSupervisor")%>

</asp:Content>

