<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Helpers.PaginatedList<Sigacun.Models.asignaciones>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Asignaciones.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Asignacion/eliminar/" + recordId;

        var request = new Sys.Net.WebRequest();
        request.set_httpVerb("DELETE");
        request.set_url(action);
        request.add_completed(deleteCompleted);
        request.invoke();
    }

    function deleteCompleted() {
        // Reload page  
        window.location.reload();
    }  
   
 </script>
   <center> <h2>Asignaciones a personal</h2>
   <br />

    <table width="95%">
        <tr>
            <th width="56px"></th>
            <th>Nº</th>
            <th>
               Nombre Actividad
            </th>
            <th>
                Resposable
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
            <td width="56px">
                <a href="<%= Url.Action("editarasig",new {id=item.asi_id })  %>"><img src="/Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
                <a href="<%= Url.Action("detalle",new { id=item.asi_id })  %>"><img src="/Content/iconos/detail.ico" alt="Detalle" border="0" title="Detalle"></a>
                
             <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.asi_id %>)" href="JavaScript:void(0)"><img src="/Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a
            </td>
            <td>
                <%= Html.Encode(item.asi_id) %>
            </td>
            <td>
                <%= Html.Encode(item.actividades.act_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.usuarios.usu_nombres) %>
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
                "asig",
            new { page=(Model.PageIndex-1) }) %>
        <% } %>
        <% if (Model.HasNextPage) { %>
        <%= Html.RouteLink("Siguiente Página>>",
                "asig",
        new { page = (Model.PageIndex + 1) })%>
    <% } %>    
 </center>
    <p>
    <br />
        <%= Html.ActionLink("Registrar Asignacion", "nuevaasig") %>
    </p>

</asp:Content>

