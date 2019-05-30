<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.procedimiento>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listar Procedimientos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
    <h2>Lista Procedimientos</h2>
<br />
    <table>
        <tr>
            <th></th>

            <th>
                Codigoun
            </th>
            <th>
                Nombre
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Mesa de Ayuda", "prodecimientoMesaAyuda", new { id = item.pro_id })%>
            </td>
            <td>
                <%= Html.Encode(item.pro_codigoun) %>
            </td>
            <td>
                <%= Html.Encode(item.pro_nombre) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</center>
</asp:Content>

