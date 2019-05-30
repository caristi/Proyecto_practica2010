<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporte de area
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
    <h2>Reporte por area mesa de ayuda</h2>
<br />
     <% using (Html.BeginForm("areaInfo", "Home")) 
             {%>	
        <table>
         
         <%= Html.Hidden("id", ViewData["id_area"])%>
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
    <table width="450px">
        <tr>
           <th width="200px">Area</th>
           <td><%= Html.Encode(ViewData["area"])%></td>
        </tr>
    </table> 
  
    
    </center>
    
    <%= Html.ActionLink("Atras","listaAreas") %>
</asp:Content>
