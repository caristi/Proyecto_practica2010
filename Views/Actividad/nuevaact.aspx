<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.actividades>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Actividad
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("registraract", "Actividad", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
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
                           <%= Html.DropDownList("pro_id", (SelectList)ViewData["procedimientos"], "-Seleccionar uno-", new { style = "width:180px;" })%>
                           <%= Html.ValidationMessageFor(model => model.pro_id) %>
                          </div>                 
                     </td> 
                     <td>
                        <div>Operacion I.G. </div>
                        <div>
                            Si <%= Html.RadioButton("act_operacion","1", false)%>

                            No <%= Html.RadioButton("act_operacion","0", true)%> 
                        </div>
                    </td>           
                </tr>
            </table>

            <table>
                   <tr>
                       <td>
                          <div>Descripción</div>
                          <div>
                             <%= Html.TextAreaFor(model => model.act_descripcion, new { style = "width:653px;" })%>
                             <%= Html.ValidationMessageFor(model => model.act_descripcion) %>
                          </div>                   
                       </td>
                    </tr>            
            </table> 
            <table>
                <tr>
                  <td>
                     <div>Adjuntar Archivo - MAXIMO 4M</div>
                     <input type="file" id="ara_nombre" name="ara_nombre" value="Examinar" />          
                  </td>
                </tr>
                <%= Html.Hidden("ara_fecha", ViewData["hora"])%> 
              </table> 

            <!-- Estado de actividad en 1 valor por default!-->
            <%= Html.Hidden("act_activo",1)%>
              
                          <br />              
            <p>
                <input type="submit" value="Guardar" />
            </p>

</center>                        
        </fieldset>
            <div>
  <%= Html.ActionLink("Atras", "inicioact") %>
    </div>

    <% } %>

</asp:Content>

