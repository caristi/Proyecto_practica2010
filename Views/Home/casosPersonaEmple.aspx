<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.actividades_hd>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Actividades Mesa de Ayuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%= Html.ActionLink("Atras", "InformeMesaAyudaEmple", new { id = ViewData["persona"], desde = ViewData["finicio"], hasta = ViewData["ffin"] })%>
    </p>
    <center>
    <h2>Actividades Mesa de Ayuda</h2>
    <br />
    
    <table>
        <tr>
            <th></th>

            <th>N°</th><th>Bloque</th><th>Piso</th><th>Ubicacion</th><th>Fecha peticion</th><th>Prioridad</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td width="36px">
            <a href="<%= Url.Action("../ActividadHd/editaracthdEmple",new { id=item.ahd_id})  %>"><img src="../../../Content/iconos/edit.ico" alt="Editarr" border="0" title="Editar"></a>
            
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
                <%= Html.Encode(item.prioridad.pri_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table> 
</center>
    <p>
        <%= Html.ActionLink("Atras", "InformeMesaAyudaEmple", new { id = ViewData["persona"], desde = ViewData["finicio"], hasta = ViewData["ffin"] })%>
    </p>

</asp:Content>