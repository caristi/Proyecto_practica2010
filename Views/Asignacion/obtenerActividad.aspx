<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.asignaciones>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Asignación
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Asignacion/eliminarArchivo/" + recordId;

        var request = new Sys.Net.WebRequest();
        request.set_httpVerb("DELETE");
        request.set_url(action);
        request.add_completed(deleteCompleted);
        request.invoke();
    }

    function deleteCompleted() {
        // Reload page  
        window.location.reload();
    }  
   
 </script>

    <% using (Html.BeginForm(Html.BeginForm("editarasig", "Asignacion", FormMethod.Post, new { enctype = "multipart/form-data" }))) {%>
        <%= Html.ValidationSummary(true) %>
        
        
        <%= Html.Hidden("id", ViewData["id"])%>
        <fieldset>
            <legend>Asignación</legend>
             <center>
             <table><tr><th>Número</th><td width="605px"><%= Html.TextBoxFor(model => model.asi_id, new { style = "width:92px;", disabled = true })%></td></tr></table>
             <table>
             <tr><th>Nombre Actividad</th><th>Prioridad</th><th>Fecha para terminar</th></tr>
             <tr>
               <td>             
                  <%= Html.DropDownList("act_id", (SelectList)ViewData["actividades"], "-Seleccionar uno-", new { style = "width:363px;" })%>
                  <%= Html.ValidationMessageFor(model => model.act_id) %>
               </td>
               <td>
                <div>
                  <%= Html.DropDownList("pri_id", (SelectList)ViewData["prioridades"], new { style = "width:113px;" })%>
                  <%= Html.ValidationMessageFor(model => model.pri_id) %>
                </div>               
               </td> 
               <td>
                 <%= Html.EditorFor(model => model.asi_fechaterminar)%>
                 <%= Html.ValidationMessageFor(model => model.asi_fechaterminar) %>
               </td>
             </tr>
             </table>
             
             <% if (ViewData["operacion"].ToString().CompareTo("1") == 0)
                { %>
                <table>
                  <tr>
                      <th>
                        OPERADOR
                      </th>
                      <td>
                        <%= Html.TextBoxFor(model=>model.asi_operador) %>
                      </td>
                  </tr>
             </table>
                
             <%} %>
             
             <table>
               <tr>
                 <th>Fecha asignación</th>
                 <td>
                  <%= Html.TextBoxFor(model => model.asi_fhasignacion, new { disabled = true })%>
                 </td>
                 <th>Solicitante</th>
                 <td>
                  <%= Html.TextBoxFor(model => model.asi_solicitante, new { style = "width:194px;" ,disabled = true})%>
                 </td>
                 <td>
                  <%= Html.DropDownList("est_id",(SelectList)ViewData["estado"], new { style = "width:97px;"}) %>
                 </td>
               </tr>
             </table>
             <table>
                <tr>
                 <th>Fecha Inicio</th>
                 <td> 
                    <%= Html.TextBoxFor(model=>model.asi_fhinicio, new{style = "width:247px;", disabled = true}) %>
                 </td>
                 <th>Fecha Fin</th>
                 <td>
                    <%= Html.TextBoxFor(model => model.asi_fhfin, new {style = "width:247px;", disabled = true })%>
                 </td>
                </tr>
             </table>
             
            <table>
               <tr>
                 <th>Usuarios</th><th>Descripción</th>
               </tr>
               <tr>
                <td>
                 <div>
                  <%= Html.DropDownList("use_id", (SelectList)ViewData["usuarios"], new { size = 10,style = "width:170px;" })%>
                  <%= Html.ValidationMessageFor(model => model.use_id) %>
                 </div>                               
                </td>
                <td>
                 <div>
                  <%= Html.TextAreaFor(model => model.asi_descripcion,8,58,null) %>
                  <%= Html.ValidationMessageFor(model => model.asi_descripcion) %>
                 </div>                
                </td>                 
               </tr> 
             </table>
             
             <table>
             <tr>
             <th>Fecha y Comentario empleado</th><th>Fecha y Comentario supervisor</th>
             </tr>
             <tr>
                <td>
                 <%= Html.TextBoxFor(model => model.asi_hfcomentario, new { disabled = true})%>
                </td>
                <td>
                 <%= Html.TextBoxFor(model => model.asi_hfcomesuper, new { disabled = true })%>
                </td>
             </tr>
             <tr>
                <td>
                  <%= Html.TextAreaFor(model=>model.asi_comentario,6,38,null) %>
                </td>
                <td>
                  <%= Html.TextAreaFor(model=>model.asi_comsuper,6,38,null) %>
                </td>
             </tr>
             </table>
             <table>
              <tr> 
                  <% if (ViewData["filesPro"].ToString().CompareTo("") != 0)
                  { %>
                 
                  <td> 
                     <div>Archivo Adjunto - PROCEDIMIENTO</div>
                      <%foreach (string file in (IEnumerable)ViewData["filesPro"])
                      {%>
                        <%= Html.ActionLink(file, "DownloadProcedimiento", new { Action = "DownloadProcedimiento", fn = file })%> &nbsp;                  
                       <%} %>
                  </td>
                  
                 <%}%>
                 <% if (ViewData["filesAct"].ToString().CompareTo("") != 0)
                   { %> 
                      <td>
                       <div>Archivo Adjunto - Actividad</div>
                        <%foreach (string file in (IEnumerable)ViewData["filesAct"])
                        {%>
                          <%= Html.ActionLink(file, "DownloadActividad", new { Action = "DownloadActividad", fn = file })%> &nbsp;                 
                       <%} %>
                        </td>
                    <%}%>           

                   <% if (ViewData["filesAsig"].ToString().CompareTo("") != 0)
                      { %> 
                      <td>
                       <div>Archivo Adjunto - Asignacion</div>
                        <%foreach (string file in (IEnumerable)ViewData["filesAsig"])
                          {%>
                          <%= Html.ActionLink(file, "DownloadAsignacion", new { Action = "DownloadAsignacion", fn = file })%> &nbsp;                 
                           <%-- Ajax Eliminar --%>  
                             <a onclick="deleteRecord(<%= ViewData["id_ars"] %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
                      </td>
                       <%} %>

                    <%}
                      else
                      {%> 
                         <td>
                           <div>Adjuntar Archivo -Asignacion- MAXIMO 4M</div>
                           <input type="file" id="ars_nombre" name="ars_nombre" value="Examinar" />  
                          </td>
                            <%= Html.Hidden("ars_fecha", ViewData["hora"])%>  
                        
                     <%} %>
                     </tr>        
               </table>
             
            <p>
            <br />
                <input type="submit" value="Guardar" />
            </p>
</center>  
        </fieldset>

    <div>
        <%= Html.ActionLink("Atras", "inicioasig") %>
    </div>
    <% } %>
</asp:Content>

