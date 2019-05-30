<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.fechasfestivo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Festivo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(function() {
    var dates = $('#fec_dia, #fechaFinal').datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 3,
            onSelect: function(selectedDate) {
            var option = this.id == "fec_dia" ? "minDate" : "maxDate";
                var instance = $(this).data("datepicker");
                var date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    });
</script>

    <% using (Html.BeginForm("crearFestivo", "Festivos")) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Nuevo dia no laboral</legend>

  <center> 
  <table>
  <tr>
    <td> <b>Fecha</b> </td>
    <td>
     <%= Html.TextBoxFor(model => model.fec_dia)%>
                 <img src="../../Content/jquery-ui/redmond/images/calendar.png" alt="Calendario" border="0">
                <%= Html.ValidationMessageFor(model => model.fec_dia) %>
    </td>
  </tr>
  
  </table>
  <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>
</center>
    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "InicioFestivos") %>
    </div>

</asp:Content>

