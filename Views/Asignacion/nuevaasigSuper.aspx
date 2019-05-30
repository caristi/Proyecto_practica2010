<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.asignaciones>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Asignación
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(function() {
    var dates = $('#rep_fechainicio, #asi_fechaterminar').datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 3,
            onSelect: function(selectedDate) {
            var option = this.id == "rep_fechainicio" ? "minDate" : "maxDate";
                var instance = $(this).data("datepicker");
                var date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    });
</script>

    <% using (Html.BeginForm("registrarasigSuper","Asignacion", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Asignación</legend>
 <center>
             <table>
             <tr>
               <td>
                <div>Nombre Actividad</div>
                <div>
                  <%= Html.DropDownList("act_id", (SelectList)ViewData["actividades"], "-Seleccionar uno-", new { style = "width:365px;" })%>
                  <%= Html.ValidationMessageFor(model => model.act_id) %>
                </div>             
               </td>
               <td>
                <div>Prioridad</div>
                <div>
                  <%= Html.DropDownList("pri_id", (SelectList)ViewData["prioridades"], new { style = "width:115px;" })%>
                  <%= Html.ValidationMessageFor(model => model.pri_id) %>
                </div>               
               </td> 
               <td>
                <div>Fecha para terminar</div>
                 <%= Html.TextBox("asi_fechaterminar","")%>
                 <img src="../../Content/jquery-ui/redmond/images/calendar.png" alt="Calendario" border="0">
                 <%= Html.ValidationMessageFor(model => model.asi_fechaterminar) %>
               </td>
             </tr>
            </table>
            <table>
               <tr>
                <td>
                 <div>Usuarios</div>
                 <div>
                  <%= Html.DropDownList("use_id", (SelectList)ViewData["usuarios"], new { size = 10,style = "width:180px;" })%>
                  <%= Html.ValidationMessageFor(model => model.use_id) %>
                 </div>                               
                </td>
                <td>
                 <div>Descripción</div>
                 <div>
                  <%= Html.TextAreaFor(model => model.asi_descripcion,8,58,null) %>
                  <%= Html.ValidationMessageFor(model => model.asi_descripcion) %>
                 </div>                
                </td>                 
               </tr> 
             </table>
            <table>
                <tr>
                  <td>
                     <div>Adjuntar Archivo - MAXIMO 4M</div>
                     <input type="file" id="ars_nombre" name="ars_nombre" value="Examinar" />          
                  </td>
                
                <%= Html.Hidden("ars_fecha", ViewData["hora"])%> 
                  <th width="110px" >REPETICION</th>   
                     
                  <td width="90px" ><b>Repetir cada </b><br /><%= Html.TextBox("rep_cantidad",null, new { style = "width:40px;" })+ " "%> <b>Días</b>
                  </td>
                  <td width="200px" >
                       <b>Fecha para Empezar</b><br />
                      <%= Html.TextBox("rep_fechainicio")%>
                      <img src="../../Content/jquery-ui/redmond/images/calendar.png" alt="Calendario" border="0">
                  </td>
                  </tr>
                  
             </table>
          
            <!-- Valor de la hora actual del servidor para fecha asignación !-->
                <%= Html.Hidden("asi_fhasignacion",ViewData["hora"]) %>
                <%= Html.Hidden("est_id",2) %>
            <!-- Coge el usuario logueado !-->
                <%= Html.Hidden("asi_solicitante", ViewData["usuario"])%>

            <p>
            <br />
                <input type="submit" value="Guardar" />
            </p>
</center>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioasigSuper") %>
    </div>

</asp:Content>

