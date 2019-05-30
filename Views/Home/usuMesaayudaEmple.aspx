<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteEmpleado.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporte Usuario
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
        <legend>Datos de Mesa de Ayuda</legend>
        <center> 
         <% using (Html.BeginForm("InformeMesaAyudaEmple", "Home")) 
             {%>
             
             PROMEDIOS TIEMPO DE RESPUESTA	
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
               <%} %>
        </center>     
    </fieldset>
    <%= Html.ActionLink("Atras", "IndexEmple")%>
</asp:Content>