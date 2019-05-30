<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.usuarios>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Usuario
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Usuario/eliminar/" + recordId;

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
    <h2>Usuarios</h2>
<br />
    <table>
        <tr>
            <th></th>
            <th>Codigo</th><th>Nombre Usuario</th><th>Nombre persona</th><th>Apellido persona</th><th>Tipo de Usuario</th><th>Activo</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="56px">
            <a href="<%= Url.Action("editarUsuario",new { id=item.usu_id })  %>"><img src="../../Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
            <a href="<%= Url.Action("inicioCambiarContra",new { id=item.usu_id })  %>"><img src="../../Content/iconos/camcont.ico" alt="Cambiar contraseña" border="0" title="Cambiar contraseña"></a>
                <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.usu_id %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
            </td>
            <td>
                <%= Html.Encode(item.usu_id) %>
            </td>
            <td>
                <%= Html.Encode(item.usu_username) %>
            </td>
            <td>
                <%= Html.Encode(item.usu_nombres) %>
            </td>
            <td>
                <%= Html.Encode(item.usu_apellidos) %>
            </td>
            <td>
                <%= Html.Encode(item.roles.rol_descripcion) %>
            </td>
            <td>
             <% if (item.usu_activo == 1)
                {%>
                    Si
               <% }
                else {%>
                    No                  
                  <%}   %>
            </td>
            
        </tr>
    
    <% } %>

    </table>
</center>
    <p>
        <%= Html.ActionLink("Registrar Usuario", "RegistrarUsuario") %>
    </p>

</asp:Content>

