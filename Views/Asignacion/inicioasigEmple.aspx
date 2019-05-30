<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Helpers.PaginatedList<Sigacun.Models.asignaciones>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Asignaciones.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <center> <h2>Asignaciones a personal</h2>
   <br />

    <table width="95%">
        <tr>
            <th width="36px"></th>
            <th>Nº</th>
            <th>
               Nombre Actividad
            </th>
            <th>
                Prioridad
            </th>
            <th>
                Fecha Terminar
            </th>
            <th>
               Estado
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="36px">
                <a href="<%= Url.Action("editarasigEmple",new {id=item.asi_id })  %>"><img src="/Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
                <a href="<%= Url.Action("detalleEmple",new { id=item.asi_id })  %>"><img src="/Content/iconos/detail.ico" alt="Detalle" border="0" title="Detalle"></a>
            </td>
            <td>
                <%= Html.Encode(item.asi_id) %>
            </td>
            <td>
                <%= Html.Encode(item.actividades.act_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.prioridad.pri_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.asi_fechaterminar.ToString("D") )%>
            </td>
            <td>
                <%= Html.Encode(item.estados.est_descripcion) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <% if (Model.HasPreviousPage) { %>
            <%= Html.RouteLink("<<Anterior Página -",
                "asige",
            new { page=(Model.PageIndex-1) }) %>
        <% } %>
        <% if (Model.HasNextPage) { %>
        <%= Html.RouteLink("Siguiente Página>>",
                "asige",
        new { page = (Model.PageIndex + 1) })%>
    <% } %>    
 </center>
    <p>
    <br />
        <%= Html.ActionLink("Registrar Asignacion", "nuevaasigEmple") %>
    </p>

</asp:Content>
