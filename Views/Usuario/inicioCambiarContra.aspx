<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Cambiar contraseña
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>

<% using (Html.BeginForm("cambiarContrasena","Usuario"))
   { %>
    <h2>Cambiar Contraseña</h2>
     <br />
     <table>
     <tr>
       <th><%= Html.Encode("Nombre usuario") %></th>
       <td><%= Html.Encode(ViewData["usuario"])%></td>
     </tr>

     <tr>
       <th><%= Html.Encode("Nueva contraseña")%></th>
       <td><%= Html.TextBox("nuevaContr")%></td>
     </tr>
     </table>
    <%= Html.Hidden("id",ViewData["id"]) %> 
    <br />
     <input type="submit" value="Guardar" />
     <%} %>
</center>
</asp:Content>
