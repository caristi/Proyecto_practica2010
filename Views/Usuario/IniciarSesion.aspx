<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Inicio.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Iniciar Sesion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
<br />
<br />
<br />
     <% using (Html.BeginForm()) { %>
     <div id="errorpanel">
    <%= Html.ValidationSummary() %>
</div>
     <table width="300px">
      <tr> <td><center><b> Iniciar sesión</b></center></td></tr>
     </table>
    <table width="300px">
        <tr>
            <td>
               <b> Nombre de Usuario:</b>
            </td>
            <td>
                <%= Html.TextBox("userName", null)%>
            </td>
        </tr>
        <tr>
            <td>
                <b>Contraseña:</b>
            </td>
            <td>
                <%= Html.Password("password")%>
            </td>
        </tr>
    </table>
    <table width="300px">
        <tr>
            <td>
              <center> <input type="submit" value= " Iniciar Sesión "  /></center> 
            </td>
        </tr>
    </table>
      <% } %>
</center>
<br />
<br />
<br />
</asp:Content>
