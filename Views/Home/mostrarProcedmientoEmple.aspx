<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Actividades Procedimiento
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <script type="text/javascript">
    $(function() {
        var dates = $('#desde, #hasta').datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 3,
            onSelect: function(selectedDate) {
                var option = this.id == "desde" ? "minDate" : "maxDate";
                var instance = $(this).data("datepicker");
                var date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    });
</script>
    <fieldset>
        <legend>Reporte de empleado</legend>
        <center> 
         <% using (Html.BeginForm("mostrarProcedmiento", "Home")) 
             {%>	
        <table>
        
          <tr>
             <td>Desde
                <%= Html.TextBox("desde") %>
                Hasta
                <%= Html.TextBox("hasta") %>
                 
                 <input type="submit" value="Consultar" />
             
            </td>
         </tr>
        </table>
                  
            <%= Html.Hidden("id_user",ViewData["id_user"])%> 
            <%= Html.Hidden("id_pro", ViewData["id_pro"])%> 
               <%} %>
 
         <table width="450px">  
           <tr>
             <th width="210px">Empleado: </th>
             <td><%= Html.Encode(ViewData["usuario"])%></td>
           </tr>
           <tr>
             <th width="210px">Procedimiento:</th>
             <td><%= Html.Encode(ViewData["procedimiento"])%></td>
            </tr>
        </table>        
        
        <table width="450px">
             <tr>
             <th width="210px">Actividades Total:</th>
             <td><%= Html.Encode(ViewData["cantidadTotal"])%></td>
             </tr>
             
             <tr>
             <th width="210px">Actividades hechas a tiempo:</th>
             <td><%= Html.Encode(ViewData["cantidadPositivas"]) %></td>
             </tr>
             
             <tr>
             <th width="210px">Activdades No hechas a tiempo</th>
             <td><%= Html.Encode(ViewData["cantidadNegitivas"])%></td>
             </tr>
        
        </table>
        
        </center>     
    </fieldset>
    <%= Html.ActionLink("Atras","IndexEmple") %>

</asp:Content>

