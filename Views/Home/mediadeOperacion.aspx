<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Media de Indicador de Operacion
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
         <% using (Html.BeginForm("mediadeOperacion", "Home"))
            {%>
                    <table>
         
         <%= Html.Hidden("act_id", ViewData["id_act"])%>
          <tr>
             <td>Desde
                <%= Html.TextBox("desde") %>
                Hasta
                <%= Html.TextBox("hasta") %>
                 
                 <input type="submit" value="Consultar" />
             
            </td>
         </tr>
        </table>
    
    <table>
    
    <tr><td>Nombre Actividad</td>
    <td>
      <%= Html.Encode(ViewData["actividad"])%>
    </td>
    </tr>
    <tr>
    <td>Media de la operacion</td>
    <td>
    <% if (ViewData["mediaOperacion"].ToString().CompareTo("-1") != 0)
       { %>
      <%= Html.Encode(ViewData["mediaOperacion"])%>
    <%}
       else
       { %>
       <%= Html.Encode("No hay resultados")%>
       
      <% }
    }%>
    </td>
    </tr>
    </table>
</center>
</asp:Content>
