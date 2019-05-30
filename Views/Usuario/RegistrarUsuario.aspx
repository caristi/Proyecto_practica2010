<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.usuarios>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Usuario
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Registar Usuario</legend>     
            
<center>
            <table>
            <tr>
             <td>Usuario Mesa de Ayuda</td><td>Tipo de usuario</td>
            </tr>     
            <tr>
              <td>
                <%= Html.DropDownList("usu_id",null, new { style = "width:230px;" })%>
              </td>  
              <td>
                <%= Html.DropDownList("rol_id", (SelectList)ViewData["roles"], "-Seleccionar uno-", new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.rol_id) %>

              </td>  
            </tr>
             </table> 
            <table>
            <tr><td>Nombre de usuario</td><td>Contraseña</td></tr>
            <tr> 
             <td>
                <%= Html.TextBoxFor(model => model.usu_username, new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.usu_username) %>              
              </td>  
              <td>
                <%= Html.TextBoxFor(model => model.usu_contrasena,new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.usu_contrasena) %>
              </td>  
            </tr>
            </table>
            <table>
            <tr><td>Nombres</td><td>Apellidos</td></tr>
            <tr> 
             <td>
                <%= Html.TextBoxFor(model => model.usu_nombres, new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.usu_nombres) %>
              
              </td>  
              <td>
                <%= Html.TextBoxFor(model => model.usu_apellidos, new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.usu_apellidos) %>
              </td>  
            </tr>
            </table>
            
            <table>
            <tr><td>Cargo</td><td>Area</td></tr>
            <tr> 
             <td>
               <%= Html.DropDownList("car_id", (SelectList)ViewData["cargo"], "-Seleccionar uno-", new { style = "width:230px;" })%>
               <br />
               <%= Html.ValidationMessageFor(model => model.car_id) %>
              
              </td>  
              <td>
                <%= Html.DropDownList("are_id", (SelectList)ViewData["area"], "-Seleccionar uno-", new { style = "width:230px;" })%>
                <br />
                <%= Html.ValidationMessageFor(model => model.are_id) %>
              </td>  
            </tr>
            </table>

         
                <!-- ESTADO DE USUARIO SE REGISTRA ACTIVO!-->
                <%= Html.Hidden("usu_activo",1) %>
                <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
</center>            
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioUsuario") %>
    </div>

</asp:Content>

