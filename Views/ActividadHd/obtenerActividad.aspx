<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.actividades_hd>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Actividad mesa de ayuda
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("editaracthd","ActividadHd"))
       {%>
        <%= Html.ValidationSummary(true) %>
      
        <fieldset>
            <legend>Actividad Mesa de Ayuda</legend>
            <center>
              <table>
                <b>INFORMACION SOLICITUD</b>  
                <%= Html.Hidden("id",ViewData["id"]) %>
              <tr>
              <th>Numero</th>
                 <td>
                   <%= Html.TextBoxFor(model => model.ahd_hd_numsolicitud, new { style = "width:50px;", disabled = true })%>
                 </td>
               <th>Fecha Solicitud</th>  
                 <td>
                   <%= Html.TextBoxFor(model => model.ahd_fhpeticion, new {style = "width:160px;", disabled=true })%>
                  </td>
                <th>Area</th>
                  <td>
                    <%= Html.TextBoxFor(model => model.usuarios.areas.are_nombre,new { style = "width:250px;", disabled = true }) %>
                  </td> 
              </tr>
              </table>
              <table>
               <tr><th>Solicitante</th>
                   <td>
                        <%= Html.TextAreaFor(model => model.ahd_solicitante, new { style = "width:175px; height:33px;", disabled = true })%>
                   </td>
                   <th>Estado</th>
                   <td>
                       <%= Html.TextBoxFor(model => model.estados.est_descripcion, new { style = "width:120px;", disabled = true })%>
                   </td>
                   <th>Asignado</th>
                   <td>
                   <%= Html.TextAreaFor(model => model.usuarios.usu_nombres, new { style = "width:175px; height:33px;", disabled = true})%>
                   </td>
               </tr>
              </table>
              <table>
                <tr>
                   <th>Bloque</th>
                   <td>
                      <%= Html.TextBoxFor(model => model.ahd_bloque, new { style = "width:35px;", disabled = true })%>
                   </td>
                   <th>Piso</th>
                     <td>
                        <%= Html.TextBoxFor(model => model.ahd_piso, new { style = "width:47px;", disabled = true })%>
                
                     </td>
                     <th>Ubicacion</th>                   
                     <td>
                        <%= Html.TextBoxFor(model => model.ahd_ubicacion, new { style = "width:260px;",  disabled = true })%>
                
                     </td>  
                     <th>Prioridad</th> 
                     <td>
                        <%= Html.TextBoxFor(model => model.prioridad.pri_nombre,new { style = "width:80px;", disabled = true }) %>
                     </td>                 
                
                </tr>
                </table>
                <table>
                  <tr><th>Descripción</th></tr>
                  <tr>
                    <td>
                        <%= Html.TextAreaFor(model => model.ahd_descripcion, new { style = "width:695px; height:90px;", disabled = true })%>
                    </td>
                   </tr>        
                </table>            
            <br />
            <table>
            <B>INFORMACIÓN TIEMPOS DE RESPUESTAS</B>
              <tr><th>Fecha Asignación</th>
                    <td>
                         <%= Html.TextBoxFor(model => model.ahd_fhasignacion, new { style = "width:240px;", disabled = true })%>
                    </td>             
                   <th>Fecha Fin</th>
                    <td>
                         <%= Html.TextBoxFor(model => model.ahd_fhfin, new { style = "width:240px;", disabled = true })%>
                    </td>
                 </tr>
             </table>
             <table>
             
                 <tr><th>Tiempo Duracion </th>
                                     <td>
                        <%= Html.TextBoxFor(model => model.ahd_duracion, new { style = "width:69px;", disabled = true })%>
                   
                    </td>
                 
                 <th>Tiempo respuesta</th>
                                     <td>
                        <%= Html.TextBoxFor(model => model.ahd_duratotal, new { style = "width:69px;", disabled = true })%>
                    </td>
                 
                 <th>Procedimiento</th>
                    <td>
                     <%= Html.DropDownList("pro_id", (SelectList)ViewData["procedimientos"], new { style = "width:180px;" })%>
                    </td>
                 </tr>
              </table>
              <br />

                <table>
                    <tr><th>Fecha y comentario Empleado</th><th>Fecha y comentario supervisor</th></tr>
                    <tr>
                        <td>
                          <%= Html.TextBoxFor(model => model.ahd_fhcomentario, new { disabled = true })%>
                        </td>
                        <td>
                          <%= Html.TextBoxFor(model => model.ahd_fhcomsuper, new { disabled = true })%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                          <%= Html.TextAreaFor(model => model.ahd_comentario, new { style = "width:335px; height: 90px;"})%>
                          <%= Html.ValidationMessageFor(model => model.ahd_comentario) %>
                        </td>
                        <td>
                          <%= Html.TextAreaFor(model => model.ahd_comsuper, new { style = "width:335px; height: 90px;"})%>
                          <%= Html.ValidationMessageFor(model => model.ahd_comsuper) %>
                        </td>
                    </tr>
                    <tr>
                    <% 
                        
                   if (ViewData["fecha"] != null)
                        { %>
                    <th>Evaluacion</th>
                    <td><a href="<%= Url.Action("../Evaluacion/detalle", new { id = ViewData["id"]})  %>"><img src="../../Content/iconos/37.ico" alt="Evaluacion" border="0" title="Evaluacion"> Evaluacion</a></td>
                    </tr>
                    
                    <%} %>
                </table>
                 <table>
                 
                 </table>
                <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
            </center>
            
        </fieldset>

    <% } %>

</asp:Content>

