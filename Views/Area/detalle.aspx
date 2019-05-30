<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.areas>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Area
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Área</legend>
        <table>
            <tr>
                <th>Código Área</th>    
                <th>Nombre</th>
                <th>Supervisor</th>
            </tr>
            <tr>
                <td><%= Html.Encode(Model.are_id) %></td>
                <td><%= Html.Encode(Model.are_nombre) %></td>
                <td><%= Html.Encode(ViewData["Supervisor"])%></td>
            </tr>
        </table>        
    </fieldset>
    <p>
        <br />
        <%= Html.ActionLink("Editar", "editarArea", new { id=Model.are_id }) %> |
        <%= Html.ActionLink("Atras", "inicioArea") %>
    </p>

</asp:Content>

