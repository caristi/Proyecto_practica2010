<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/InicioSuper.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reportes a los empleados
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
<h3>TIEMPO DE RESPUESTA DE SERVICIOS TODOS EMPLEADOS</h3>
    <% using (Html.BeginForm("ReporteGraficoTodasPersonasSuper", "Home")) 
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
       //Title t = new Title("CANTIDAD DE SERVICIOS TODOS EMPLEADOS", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
       //Chart2.Titles.Add(t);
       Chart2.ChartAreas.Add("Empleados");


       List<float> value = (List<float>)ViewData["Chart"];

       int i = 0;
       foreach (string usu in (List<string>)ViewData["usuario"])
       {
           Chart2.Series.Add(usu);

           if (value[i] < 0)
           {

               value[i] = -0.0000001F;
           }

           Chart2.Series[usu].Points.AddY(value[i]);
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
        <table><tr><th>EMPLEADO</th><th>Tiempo de respusta</th></tr>
        <% 
            List<float> tiempo = (List<float>)ViewData["Chart"];

            float valor = 0;
            int j = 0;
            foreach (string usu in (List<string>)ViewData["usuario"])
            {%>
                <tr>
                 <td>
                  <%= Html.Encode(usu) %>
                 </td>
                 <td>
                   <% valor = tiempo[j];
                      j++;

                      if (valor < 0) {

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
    <%= Html.ActionLink("Atras","IndexSupervisor") %>
</asp:Content>
