<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage" %>

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
            <td><center><b> TIEMPOS MESA DE AYUDA</b></center></td>
            <td><center><b>ACTIVIDADES HECHAS O NO</b></center></td>
            <td><center><b>INDICARDOR DE OPERACIÓN</b></center></td>
        </tr> 
        <tr>
            <td><center><a href="<%= Url.Action("../Home/usuMesaayudaEmple")  %>"><img src="/Content/iconos/usuario.ico" alt="Tiempos mesa de ayuda" border="0" title="Tiempos mesa de ayuda"></a></center></td>           
            <td><center><a href="<%= Url.Action("../Home/proPersonaEmple")  %>"><img src="/Content/iconos/checked3.png" alt="Procedimiento" border="0" title="Procedimiento"></a></center></td>
            <td><center><a href="<%= Url.Action("../Home/listarActividadesEmple")  %>"><img src="/Content/iconos/indicadores.png" alt="Indicadores de operación" border="0" title="Indicadores de operación"></a></center></td>
        </tr>
        
        
     
      </table>
      
    </center>
</asp:Content>
