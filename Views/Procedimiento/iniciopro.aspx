﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Helpers.PaginatedList<Sigacun.Models.procedimiento>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">                      
	  Inicio Procedimiento                                                                          
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        
        // Perform delete
        var action = "/Procedimiento/eliminar/" + recordId;
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
<center>
    <h2>Procedimientos</h2>

<br />
    <table width="95%">
        <tr>
            <th width="56px"></th>
            <th>Codigo UN</th>
            <th>Nombre</th>
            <th>Descripcion</th>
            <th>Activo</th>
            <th>Área</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="56px">
            <a href="<%= Url.Action("editarpro",new { id=item.pro_id })  %>"><img src="../../Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
            <a href="<%= Url.Action("detalle",new { id=item.pro_id })  %>"><img src="../../Content/iconos/detail.ico" alt="Detalle" border="0" title="Detalle"></a>
                <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.pro_id %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
            </td>
            <td>
                <%= Html.Encode(item.pro_codigoun) %>
            </td>
            <td>
                <%= Html.Encode(item.pro_nombre) %>
            </td>
            <td>
                <%= Html.Encode(item.pro_describcion) %>
            </td>
            <td>
                <% if (item.pro_activo == 1) {%>
                   <%= "Si"%>
                   <%}
                      else%>
                    <%= "No" %>
            </td>
            <td>    
                <%= Html.Encode(item.areas.are_nombre) %>
            </td>
        </tr>
    <% } %>
    </table>

    <% if (Model.HasPreviousPage) { %>
            <%= Html.RouteLink("<<Anterior Página -",
                "pro",
            new { page=(Model.PageIndex-1) }) %>
        <% } %>
        <% if (Model.HasNextPage) { %>
        <%= Html.RouteLink("Siguiente Página>>",
            "pro",
        new { page = (Model.PageIndex + 1) })%>
    <% } %>
 </center>   
    <p>
      <br />
        <%= Html.ActionLink("Registrar Procedimiento", "nuevopro") %>
    </p>

</asp:Content>

