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
         <% using (Html.BeginForm("mostrarProcedmientoEmple", "Home")) 
             {%>	
        <table>
         
         <%= Html.Hidden("id", ViewData["id"])%>
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
             <th width="210px">Procedimiento</th>
             <td><%= Html.Encode(ViewData["procedimiento"])%></td>
           </tr>
        </table>        
        
        </center>     
    </fieldset>
    <%= Html.ActionLink("Atras","IndexEmple") %>

</asp:Content>
