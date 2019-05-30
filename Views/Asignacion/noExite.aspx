<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Buscar actividad de Mesa de Ayuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <center>
    <h2>Buscar actividad</h2>
    
     <% using (Html.BeginForm("obtenerActividad","Asignacion"))
        {%>
     <br />
        <table>
           <tr>
           <th>Número de solicitud</th>
           <td>
              <%= Html.TextBox("id") %>
           </td>
           </tr>
        </table>
        <br />
        <input type="submit" value=" Buscar "  />
     
     <%} %>
     <br />
    NO EXISTE NÚMERO DE SOLICITUD 
    </center>

</asp:Content>