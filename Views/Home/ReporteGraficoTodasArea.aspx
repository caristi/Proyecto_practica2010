<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reporte Grafico de todas las area
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
<h3>TIEMPO DE RESPUESTA DE SERVICIOS TODAS LAS AREAS</h3>
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
     <%   
       System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();

       Chart2.Width = 750;
       Chart2.Height = 296;
       Chart2.RenderType = RenderType.ImageTag;

       Chart2.Palette = ChartColorPalette.BrightPastel;
       Chart2.ChartAreas.Add("Empleados");


       List<float> value = (List<float>)ViewData["Chart"];

       int i = 0;
       foreach (string area in (List<string>)ViewData["area"])
       {
           Chart2.Series.Add(area);

           if (value[i] <= 0)
           {

               value[i] = -0.2F;
           }

           Chart2.Series[area].Points.AddY(value[i]);
           i++;
       }


       Chart2.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
       Chart2.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
       Chart2.BorderlineDashStyle = ChartDashStyle.Solid;
       Chart2.BorderWidth = 2;

       Chart2.Legends.Add("Legend1");

       // Render chart control
       Chart2.Page = this;
       HtmlTextWriter writer = new HtmlTextWriter(Page.Response.Output);
       Chart2.RenderControl(writer);

   } %>
        <div>
        <table><tr><th>Area</th><th>Tiempo de respusta</th></tr>
        <% 
            List<float> tiempo = (List<float>)ViewData["Chart"];

            float valor = 0;
            int j = 0;
            foreach (string area in (List<string>)ViewData["area"])
            {%>
                <tr>
                 <td>
                  <%= Html.Encode(area)%>
                 </td>
                 <td>
                   <% valor = tiempo[j];
                      j++;

                      if (valor <= 0) {

                          valor = 0;
                      }%>
                   <%= Html.Encode(valor)%>
                 </td>
                </tr>
                
            <%}
            %>
       </table>
        
        
        </div>
</center>   
    <%= Html.ActionLink("Atras","Index") %>

</asp:Content>
