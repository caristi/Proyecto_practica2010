<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.procedimiento>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Procedimiento
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Procedimiento</legend>
 <center>
        <table width="80%">
         <tr>
            <th>Código UN</th>
            <th>Nombre</th>
            <th>Activo</th>
         </tr>
         <tr>
           <td>
           <%= Html.Encode(Model.pro_codigoun) %>
           </td>
           <td>
           <%= Html.Encode(Model.pro_nombre) %>
           </td> 
           <td>
            <% if(Model.pro_activo == 1){ %>
             <%= "Si" %>
             <% }
               else %>
               <%= "No"%>
           </td>  
         </tr>
        </table>
        <table width="80%">
            <tr>
             <th>Descripción</th>
             <th>Area</th>
            </tr>
            <tr>
             <td>
              <%= Html.Encode(Model.pro_describcion) %>
             </td>
             <td>
             <%= Html.Encode(Model.areas.are_nombre) %>
             </td>
            </tr>
        </table>
</center>               
    </fieldset>
    <p>
        <br />
        <%= Html.ActionLink("Editar", "editarpro", new { id=Model.pro_id }) %> |
        <%= Html.ActionLink("Atras", "iniciopro") %>
    </p>

</asp:Content>

