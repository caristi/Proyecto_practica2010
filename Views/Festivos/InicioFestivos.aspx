<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.fechasfestivo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inicio Festivos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Festivos/eliminar/" + recordId;

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

    <h2>Inicio Festivos</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Fecha
            </th>
            <th>
                Tipo
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
              <% if (item.fec_tipo == 3)
               {%>
               <a href="<%= Url.Action("editarFes",new { id=item.fec_id })  %>"><img src="../../Content/iconos/edit.ico" alt="Editar" border="0" title="Editar"></a>
                <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= item.fec_id %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
              <%}%>
              
              
            </td>
            <td>
            <% if (item.fec_tipo == 1 || item.fec_tipo == 2)
               {%>
               <%= Html.Encode(item.fec_dia.ToString("dd MMMM"))%>
            <%}%>
            
            <% if (item.fec_tipo == 3)
               {%>
               <%= Html.Encode(item.fec_dia.ToString("D"))%>
            <%}%>
               
            </td>
            <td>
            <% if (item.fec_tipo == 1) {%>
               <%= "Fecha fija" %>
            <%}%>
            <% if(item.fec_tipo == 2) {%>
               <%= "Siguiente Lunes" %>
            <%}%>
     
            <% if(item.fec_tipo == 3){ %>
               <%= "Fecha para el año" %>
            <%}%>
                
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Registrar festivo", "registrarFestivo") %>
    </p>

</asp:Content>

