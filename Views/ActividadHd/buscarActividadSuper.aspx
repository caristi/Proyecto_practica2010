<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Buscar actividad de Mesa de Ayuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <center>
    <h2>Buscar actividad</h2>
    
     <% using (Html.BeginForm("obtenerActividadSuper","ActividadHd"))
        {%>
     <br />
        <table>
           <tr>
           <th>Número de solicitud</th>
           <td>
              <%= Html.TextBox("numero") %>
           </td>
           </tr>
        </table>
        <br />
        <input type="submit" value=" Buscar "  />
     
     <%} %>
    
    </center>

</asp:Content>
