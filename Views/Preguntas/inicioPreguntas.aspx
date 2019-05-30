<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.evapreguntas>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Preguntas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Preguntas/eliminar/" + recordId;

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
    <h2>Inicio preguntas</h2>
<br />
    <table>
        <tr>
            <th></th>
            <th>
                Codigo
            </th>
            <th>
                Pregunta
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                 <a href="<%= Url.Action("editarPre",new { id=item.epr_id })  %>"><img src="../../Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>                 
                   <%-- Ajax Eliminar --%>  
                 <a onclick="deleteRecord(<%= item.epr_id %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
            </td>
            <td>
                <%= Html.Encode(item.epr_id) %>
            </td>
            <td>
                <%= Html.Encode(item.epre_pregunta) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>
    <p>
        <%= Html.ActionLink("Registrar Pregunta", "nuevaPre") %>
    </p>

</asp:Content>

