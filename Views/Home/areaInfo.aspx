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
  
      <table width="450px">
        <tr>
            <th width="200px">Cantidad de casos</th>
            <td>
               <%= Html.Encode(ViewData["cantidad"]) %> 
            </td>
        
        </tr>
        <tr>
            <th width="200px">Tiempo promedio</th>
            <td>
            
            <% if (ViewData["promedioTiempo"].ToString().CompareTo("-1") == 0)
               { %>
            
                 No hay Tiempos
                 <%}
               else
               { %>
                  <%= Html.Encode(ViewData["promedioTiempo"])%></td>
                  <%} %>
        </tr>
        <tr>
            <th width="200px">Calificaciones</th>
            <td>
            
            <% if (ViewData["Calificacion"].ToString().CompareTo("-1") == 0)
               { %>
            
                 No hay Calificaciones
                 <%}
               else
               { %>
                  <%= Html.Encode(ViewData["Calificacion"])%></td>
                  <%} %>
        </tr>
        
    </table>
    
    
    </center>
    
    <%= Html.ActionLink("Atras","listaAreas") %>

    
    
    

</asp:Content>
