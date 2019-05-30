<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Helpers.PaginatedList<Sigacun.Models.actividades>>" %>
                                                                                      
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Actividades
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Actividad/eliminar/" + recordId;

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
   <center> <h2>Actividades</h2>
<br />
    <table width="95%">
        <tr>
            <th width="56px"></th>
            <th>
                Código
            </th>
            <th>
                Nombre
            </th>
            <th>
                Descripcion
            </th>
            <th>
                Activo
            </th>
            <th>
                Operacion
            </th>
            <th>
                Procedimiento
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="56px">
            <a href="<%= Url.Action("editaractSuper",new { id=item.act_id })  %>"><img src="/Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
            <a href="<%= Url.Action("detalleSuper",new { id=item.act_id })  %>"><img src="/Content/iconos/detail.ico" alt="Detalle" border="0" title="Detalle"></a>
                <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.act_id %>)" href="JavaScript:void(0)"><img src="/Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
            </td>
            <td>
                <%= Html.Encode(item.act_id) %>
            </td>
            <td>
                <%= Html.Encode(item.act_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.act_descripcion) %>
            </td>
            <td>
              <% if (item.act_activo == 1)
                 {%>
               <%= "Si"%>
               <%}
                 else %>
                <%= "No"%>
            </td>
            <td>
               <% if (item.act_operacion == 1)
                  { %>
               <%= "Si"%>
               <%}else %>
                <%= "No" %>
            </td>
            <td>
                <%= Html.Encode(item.procedimiento.pro_codigoun) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <% if (Model.HasPreviousPage) { %>
            <%= Html.RouteLink("<<Anterior Página -",
                "acts",
            new { page =(Model.PageIndex-1) }) %>
        <% } %>
        <% if (Model.HasNextPage) { %>
        <%= Html.RouteLink("Siguiente Página>>",
            "acts",
        new { page = (Model.PageIndex + 1) })%>
    <% } %>

</center>   
    <p> 
    <br />
        <%= Html.ActionLink("Registrar actividad", "nuevaactSuper") %>
    </p>

</asp:Content>
