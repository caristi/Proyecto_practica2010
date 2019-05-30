<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reportes todas las areas
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
<center>
<h3>TIEMPO DE RESPUESTA DE SERVICIOS TODOS AREAS</h3>
    <% using (Html.BeginForm("ReporteGraficoTodasArea", "Home")) 
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
    <%} %>
</center>

</asp:Content>
