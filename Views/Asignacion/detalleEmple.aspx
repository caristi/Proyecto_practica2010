<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.asignaciones>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle Asignación
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Asignación</legend>
        
        <table width="99%">
            <tr>
              <th> <div>Nombre Actividad</div></th>
              <th> <div>Fecha Asignación</div> </th>
              <th><div>Fecha Terminar</div></th>                              
            </tr>
            <tr>
             <td>
                <div><%= Html.Encode(Model.actividades.act_nombre) %></div>
             </td>
             <td>
                <%System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("Es-Es"); %>
                <%= Html.Encode(ci.DateTimeFormat.GetDayName(Model.asi_fhasignacion.DayOfWeek) + ", " + Model.asi_fhasignacion.Date.ToString("d MMMM 'del' yyyy"))%>
             </td>
             <td><div>
               <%= Html.Encode(ci.DateTimeFormat.GetDayName(Model.asi_fechaterminar.DayOfWeek) + ", " + Model.asi_fechaterminar.Date.ToString("d MMMM 'del' yyyy") )%>
              </div> </td>             
            </tr>
         </table>
         <table width="99%">
            <tr>
                <th><div>Usuario Asignado</div></th>
                <th><div>Solicitante</div></th>
                <th><div>Fecha Inicio</div></th>
                <th><div>Fecha Fin</div> </th>

            </tr>
            <tr> 
             <td>
                <div><%= Html.Encode(Model.usuarios.usu_nombres) %></div>                
             </td>
             <td>
                <div><%= Html.Encode(Model.asi_solicitante) %></div>             
             </td>
             <td><% 
                       if (Model.asi_fhinicio == null)
                       {
                   %>
                         <%= "No se ha empezado" %>
                         
                     <%}
                       else
                       {
                     %>                     
                   <%= Html.Encode(String.Format("{0:g}", Model.asi_fhinicio))%>            
                   <%} %>
              </td>
              <td>
                   <% 
                       if (Model.asi_fhinicio == null)
                       {
                   %>
                         <%= "No se ha Terminado" %>
                         
                     <%}
                       else
                       {
                     %> 
                        <%= Html.Encode(String.Format("{0:g}", Model.asi_fhfin)) %>   
                     <%} %>            
                    
              </td>
            </tr>
        </table>  
        <table width="99%">
            <tr>
                <th><div>Prioridad</div></th>
                <th><div>Descripcion</div></th>
            </tr>
            <tr>
                <td>
                   <div><%= Html.Encode(Model.prioridad.pri_nombre) %></div>                
                </td>
                <td>
                   <div><%= Html.Encode(Model.asi_descripcion) %></div>
                </td>
            
            </tr>         
        </table>
        <table width="99%">
          <tr>
            <th><div>Comentario</div></th>
          </tr>
          <tr>
           <td>
             <%if(Model.asi_comentario == null){%>
                <%="Sin Comentarios"%>
                
               <%}
               else %>
               
            <div><%= Html.Encode(Model.asi_comentario) %></div>
           </td>
          </tr>
        </table>        
    </fieldset>
    <p>

        <%= Html.ActionLink("Editar", "editarasigEmple", new { id=Model.asi_id }) %> |
        <%= Html.ActionLink("Atras", "inicioasigEmple") %>
    </p>

</asp:Content>