<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.actividades>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Actividad
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Actividad/eliminarArchivo/" + recordId;

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
    <% using (Html.BeginForm("editaract", "Actividad", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Actividad</legend>
 <center>
            <table>
                <tr>
                    <td>
                         <div>Nombre</div>
                         <div>
                            <%= Html.TextBoxFor(model => model.act_nombre, new {style = "width:351px;" })%>
                            <%= Html.ValidationMessageFor(model => model.act_nombre) %>
                         </div>                        
                    </td>
                    <td>
                          <div>Procedimiento</div>
                          <div>
                           <%= Html.DropDownList("pro_id", (SelectList)ViewData["procedimientos"], "-Seleccionar uno-", new { style = "width:277px;" })%>
                           <%= Html.ValidationMessageFor(model => model.pro_id) %>
                          </div>                 
                     </td>                      
                     <td>
                        <div>Operacion I.G. </div>
                        <div>
                            Si <%= Html.RadioButton("act_operacion","1")%>

                            No <%= Html.RadioButton("act_operacion","0")%>
                        </div>
                    </td> 
          
                </tr>
            </table>

            <table>
                   <tr>
                       <td>
                          <div>Descripción</div>
                          <div>
                             <%= Html.TextAreaFor(model => model.act_descripcion, new { style = "width:670px;" })%>
                             <%= Html.ValidationMessageFor(model => model.act_descripcion) %>
                          </div>                   
                       </td>
                     <td>
                          <div><center> Activo</center></div>
                          <div>
                            Si <%= Html.RadioButton("act_activo", "1")%>

                            No <%= Html.RadioButton("act_activo", "0")%>
                          </div>                                          
                     </td>                       
                    </tr>            

                        <% if (ViewData["files"].ToString().CompareTo("") != 0)
               { %>          
            <tr>
            <td>
            
            <div>Archivo Adjunto - PROCEDIMIENTO</div>
                <%foreach (string file in (IEnumerable)ViewData["files"])
                  {%>
                 <%= Html.ActionLink(file, "Download", new { Action = "Download", fn = file })%> &nbsp;                  
                <%} %>
            </td>
            </tr>
            <%}%>
            </table>
            <table>
             <% if (ViewData["filesAct"].ToString().CompareTo("") != 0)
               { %>          
            <tr>
            <td>
            
            <div>Archivo Adjunto - Actividad</div>
                <%foreach (string file in (IEnumerable)ViewData["filesAct"])
                  {%>
                 <%= Html.ActionLink(file, "DownloadActividad", new { Action = "DownloadActividad", fn = file })%> &nbsp;
                   <%-- Ajax Eliminar --%>  
                <a onclick="deleteRecord(<%= ViewData["id_arc"] %>)" href="JavaScript:void(0)"><img src="../../Content/iconos/blue_delete.ico" alt="Eliminar" border="0" title="Eliminar"></a>
                <%} %>
            </td>
            </tr>
            <%}
               else
               {%>
               </table>
               <table>
                <tr>
                  <td>

                     <div>Adjuntar Archivo -Actividad- MAXIMO 4M</div>
                     <input type="file" id="arp_nombre" name="arp_nombre" value="Examinar" />          
                  </td>
                </tr>
                            <%= Html.Hidden("ara_fecha", ViewData["hora"])%>
               <%} %>  
              </table>
            
            <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
</center>                       
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioact") %>
    </div>

</asp:Content>

