﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.estados>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Estados
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Estado/eliminar/" + recordId;

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
    <h2>Estados</h2>
<br />
    <table width="50%">
        <tr>
            <th width="56px"></th>
            <th>
                Código
            </th>
            <th>
                Nombre
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="56px">
             <a href="<%= Url.Action("editarEstado",new { id=item.est_id })  %>"><img src="../../Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
             <a href="<%= Url.Action("detalle",new { id=item.est_id})  %>"><img src="../../Content/iconos/detail.ico" alt="Detalle" border="0" title="Detalle"></a>
                <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.est_id %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
            </td>
            <td>
                <%= Html.Encode(item.est_id) %>
            </td>
            <td>
                <%= Html.Encode(item.est_descripcion) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>
    <p>
    <br />
        <%= Html.ActionLink("Registrar estado", "nuevoestado")%>
    </p>

</asp:Content>

