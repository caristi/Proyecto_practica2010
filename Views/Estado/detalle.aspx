<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.estados>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Estado
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Estado</legend>
        
        <table>
            <tr>
                <th>Código</th>
                <th>Descripción</th>
            </tr>
            <tr>
                <td><%= Html.Encode(Model.est_id) %></td>
                <td><%= Html.Encode(Model.est_descripcion) %></td>
            </tr>
        </table>        
    </fieldset>
    <p>
        <br />
        <%= Html.ActionLink("Editar", "editarEstado", new { id=Model.est_id }) %> |
        <%= Html.ActionLink("Atras", "inicioestado") %>
    </p>

</asp:Content>

