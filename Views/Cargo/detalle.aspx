<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.cargos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Cargo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <legend>Cargo</legend>
     <table>
        <tr>
            <th>Código</th>
            <th>Nombre</th>
        </tr>
        <tr>
           <td><%= Html.Encode(Model.car_id) %></td>
           <td><%= Html.Encode(Model.car_nombre) %></td>
        </tr>
     </table>
    </fieldset>
    <p>
        <br />
        <%= Html.ActionLink("Editar", "editarcar", new { id=Model.car_id }) %> |
        <%= Html.ActionLink("Atras", "iniciocargo") %>
    </p>

</asp:Content>

