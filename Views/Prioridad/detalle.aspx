<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.prioridad>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Prioridad
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Prioridad</legend>
        <table>
            <tr>
                <th>Código</th>
                <th>Nombre</th>
            </tr>
            <tr>
                <td><%= Html.Encode(Model.pri_id) %></td>
                <td><%= Html.Encode(Model.pri_nombre) %></td>
            </tr>
            
        </table>
    </fieldset>
    <p>
        <br>
        <%= Html.ActionLink("Editar", "editarpri", new { id=Model.pri_id }) %> |
        <%= Html.ActionLink("Atras", "Iniciopri") %>
    </p>

</asp:Content>

