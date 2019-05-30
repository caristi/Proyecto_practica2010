<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage" %>

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
            <td><center><b>PERSONA </b></center></td>
            <td><center><b>PROCEDIMIENTO</b></center></td>
            <td><center><b>ACTIVIDAD </b></center></td>
        </tr> 
        <tr>
            <td><center><a href="<%= Url.Action("../Home/repostesTodasPersonaSuper")  %>"><img src="../../Content/iconos/usuario.ico" alt="Persona" border="0" title="Persona"></a></center></td>
            <td><center><a href="<%= Url.Action("../Home/UsuariosSuper")  %>"><img src="../../Content/iconos/checked3.png" alt="Procedimiento" border="0" title="Procedimiento"></a></center></td>
            <td><center><a href="<%= Url.Action("../Home/listarActividadesSuper")  %>"><img src="../../Content/iconos/actividad.ico" alt="Actividad" border="0" title="Actividad"></a></center></td>
        </tr>
              
      </table>
      
    </center>

</asp:Content>
