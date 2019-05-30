<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Helpers.PaginatedList<Sigacun.Models.actividades_hd>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Mesa de Ayuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <center><h2>Actividades Mesa de Ayuda</h2>
    <br />
    <table>
        <tr>
            <th></th>

            <th>N°</th><th>Bloque</th><th>Piso</th><th>Ubicacion</th><th>Fecha peticion</th>
                    <th>Estado</th><th>Prioridad</th><th>Persona asignado</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
            <a href="<%= Url.Action("editaracthdsuper",new { id=item.ahd_id })  %>"><img src="/Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
            </td>
            <td>
                <%= Html.Encode(item.ahd_hd_numsolicitud) %>
            </td>
            <td>
                <%= Html.Encode(item.ahd_bloque) %>
            </td>
            <td>
                <%= Html.Encode(item.ahd_piso) %>
            </td>
            <td>
                <%= Html.Encode(item.ahd_ubicacion) %>
            </td>
            <td>
                <%= Html.Encode(item.ahd_fhpeticion.ToString("f"))%>
            </td>
            <td>
                <%if(item.est_id == 3){ %>
                <%= Html.Encode(item.estados.est_descripcion) %>
                 <img src="../../Content/iconos/exclamation.png" alt="Editar" border="0" title="Editar">
                <%}
                  else{
                 %>
                 
                 <%= Html.Encode(item.estados.est_descripcion) %>
                 <%} %> 
            </td>
            <td>
                <%= Html.Encode(item.prioridad.pri_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.usuarios.usu_nombres) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <% if (Model.HasPreviousPage) { %>
            <%= Html.RouteLink("<<Anterior Página -",
                "achs",
            new { page=(Model.PageIndex-1) }) %>
        <% } %>
        <% if (Model.HasNextPage) { %>
        <%= Html.RouteLink("Siguiente Página>>",
            "achs",
        new { page = (Model.PageIndex + 1) })%>
    <% } %> 
</asp:Content>

