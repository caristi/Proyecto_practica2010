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

       <table width="450px">           
        
        <tr >
        
        <th width="200px">Area:</th>
        
        <td><%= Html.Encode(ViewData["area"])%></td>
        
        </tr>
        
        <tr>
        <th width="200px">Cantidad de Casos resueltos:</th>
        
        <td>
            <% if (ViewData["cantidadCasos"].ToString().CompareTo("0") == 0)
               { %>
                0
            <%}
               else
               {%>
                <%= Html.ActionLink(ViewData["cantidadCasos"].ToString(), "casosPersonaEmple", new { id = ViewData["id_user"], finicio = ViewData["finicio"], ffin = ViewData["ffin"] })%>
            <%} %>
        </td>            
        </tr>
        
        <tr>
        
        <th width="200px">Promedio Tiempo de respuesta:</th>
        
        <% if (ViewData["promedio"].ToString().CompareTo("-1") == 0)
           { %>
             
                <td> No hay tiempos</td>        
           <%}
           else
           {%>
                <td> <%= Html.Encode(ViewData["promedio"])%></td>
                <%} %>
        </tr>         
        </table>
        
        </center>
        
            </fieldset>
    <%= Html.ActionLink("Atras", "IndexEmple")%>
</asp:Content>