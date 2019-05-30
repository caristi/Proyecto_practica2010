<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Sigacun.Models.evaluacion>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Evaluacion 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
    <h2>Evaluacion</h2>
    <table>
        <tr>
            <th>
                Pregunta
            </th>
            <th>
                Calificacion
            </th>
            <th>
                Observacion
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>

            <td>
                <%= Html.Encode(item.evapreguntas.epre_pregunta) %>
            </td>
            <td>
                <%= Html.Encode(item.eva_calificacion) %>
            </td>            
            <td>
                <%= Html.Encode(item.eva_observacion) %>
            </td>
        </tr>
    <% } %>

    </table>
    <div>
         <%= Html.ActionLink("Atras", "../ActividadHd/inicioMesaAyuda")%>
     </div>
   </center>
</asp:Content>

