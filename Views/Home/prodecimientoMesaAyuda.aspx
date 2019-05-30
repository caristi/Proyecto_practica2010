<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.procedimiento>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	prodecimientoMesaAyuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
    <h2>Prodecimiento Mesa de Ayuda</h2>
    <br />
    <table>
    
    <tr>
        <th>Código</th>
        <td><%= Html.Encode(ViewData["codigoPro"])%></td>
    </tr>
    
    <tr>
        <th>Nombre</th>
        <td><%= Html.Encode(ViewData["nombre"])%></td>
    </tr>
    
    <tr>
        <th>Cantidad </th>
        <td><%= Html.Encode(ViewData["cantidad"])%></td>
    </tr>
    
    </table>
</center>

<%= Html.ActionLink("Atras","listarProcedimientos") %>
</asp:Content>
