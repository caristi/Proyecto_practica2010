<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.actividades>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Actividad
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Actividad</legend>
<center>
         <table width="80%">
            <tr>
                <th>Nombre</th>
                <th>Procedimiento</th>
                <th>Activo</th>
                <th>Operación</th>
            </tr>
            <tr>
             <td>
                <%= Html.Encode(Model.act_nombre) %>
             </td>
             <td>
                <%= Html.Encode(Model.procedimiento.pro_nombre) %>
             </td>
             <td>
                <% if (Model.act_activo == 1)
                   { %>
                 <%= "Si"%>
                 <%} else%>
                 <%= "No" %>
             </td>
             <td>
             <%if (Model.act_operacion == 1)
               { %>
               <%= "Si"%>
               <%} else %>
               <%= "No" %>
             </td>
            </tr>
         </table>
         <table width="80%">
            <tr>
                <th>Descripción</th>
            </tr>
            <tr>
                <td>
                 <%= Html.Encode(Model.act_descripcion) %>
                </td>
            </tr>
         </table>
</center>                 
    </fieldset>
    <p>

        <%= Html.ActionLink("Editar", "editaractSuper", new { id=Model.act_id }) %> |
        <%= Html.ActionLink("Atras", "inicioactSuper") %>
    </p>

</asp:Content>

