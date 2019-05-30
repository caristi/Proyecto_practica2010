<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inicio
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
      <p>
        <b>REPORTES</b>
      </p>
     <br />
      <table>
      
        <tr>
            <td><center><b> PERSONA </b></center></td>
            <td> <center><b> AREA </b></center></td>
            <td><center><b>PROCEDIMIENTO</b></center></td>
        </tr> 
        <tr>
            <td><center><a href="<%= Url.Action("Usuarios")  %>"><img src="/Content/iconos/usuario.ico" alt="Persona" border="0" title="Persona"></a></center></td>
            <td><center><a href="<%= Url.Action("listaAreas")  %>"><img src="/Content/iconos/area.ico" alt="Area" border="0" title="Area"></a></center></td>
            <td><center><a href="<%= Url.Action("listarProcedimientos")  %>"><img src="/Content/iconos/card.ico" alt="Procedimiento" border="0" title="Procedimiento"></a></center></td>
        </tr>

         <tr>
            <td><center><b> ACTIVIDAD </b></center></td>
            <td width="130"> <center><b> TIEMPO DE TODAS LAS ÁREAS TODAS LAS AREA </b></center></td>
            <td width="130"><center><b>TIEMPO DE TODOS EMPLEADO MESA DE AYUDA</b></center></td>
        </tr> 
        <tr>
            <td><center><a href="<%= Url.Action("listarActividades")  %>"><img src="/Content/iconos/actividad.ico" alt="Listar actividades" border="0" title="Actividad"></a></center></td>
            <td><center><a href="<%= Url.Action("reportesTodasArea")  %>"><img src="/Content/iconos/diagram.ico" alt="Area" border="0" title="TODAS LAS AREA"></a></center></td>
            <td><center><a href="<%= Url.Action("repostesTodasPersona")  %>"><img src="/Content/iconos/mesadeayuda2.ico" alt="Mesa de Ayuda" border="0" title="TODOS EMPLEADO MESA DE AYUDA"></a></center></td>
        </tr>        
      </table>
      
    </center>
</asp:Content>
