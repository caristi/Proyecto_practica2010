<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.procedimiento>"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Procedimientos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">

    function deleteRecord(recordId) {
        // Perform delete
        var action = "/Procedimiento/eliminarArchivo/" + recordId;

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


    <% using (Html.BeginForm("editarpro", "Procedimiento", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Procedimiento</legend>    
<center>
         <table>
           <tr>
                <td>
                    <div>Código UN</div>
                    <div>
                    <%= Html.TextBox("pro_codigoun",null,new { style = "width:150px;" })%>
                    <%= Html.ValidationMessage("pro_codigoun") %>
                    </div>
                </td>
                <td>
                   <div>Nombre</div>
                   <div>
                    <%= Html.TextBox("pro_nombre",null,new { style = "width:380px;" })%>
                    <%= Html.ValidationMessage("pro_nombre") %>
                   </div> 
                </td>
                <td>
                    <div>Área</div>
                    <div>
                      <%= Html.DropDownList("are_id", (SelectList)ViewData["areas"], new { style = "width:250px;" })%>
                    </div>                
                </td>
           </tr>
           </table>
           <table>
           <tr>
                <td>
                   <div>Descripción</div>
                   <div>
                    <%= Html.TextArea("pro_describcion",new { style = "width:733px;" }) %>
                    <%= Html.ValidationMessage("pro_describcion") %>
                   </div>  
                </td>
                <td>
                   <div >Activo</div>
                   <div>

                            Si <%= Html.RadioButton("pro_activo", "1")%>

                            No <%= Html.RadioButton("pro_activo", "0")%>
                                        
                   </div>  
                </td>
           </tr> 
            <% if (ViewData["files"].ToString().CompareTo("") != 0)
               { %>          
            <tr>
            <td>
            
            <div>Archivo Adjunto</div>
                <%foreach (string file in (IEnumerable)ViewData["files"])
                  {%>
                 <%= Html.ActionLink(file, "Download", new { Action = "Download", fn = file })%> &nbsp;
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

                     <div>Adjuntar Archivo - MAXIMO 4M</div>
                     <input type="file" id="arp_nombre" name="arp_nombre" value="Examinar" />          
                  </td>
                </tr>
                            <%= Html.Hidden("arp_fechacrea", ViewData["hora"])%>
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
        <%= Html.ActionLink("Atras", "iniciopro") %>
    </div>

</asp:Content>

