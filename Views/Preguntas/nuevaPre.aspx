<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.evapreguntas>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Pregunta
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Registrar pregunta</legend>
            
            <div>Codigo pregunta mesa de ayuda</div>
            <div>
              <%= Html.DropDownList("epr_id")%>
            </div>

            <div>Pregunta</div>            
            <div>
                <%= Html.TextArea("epre_pregunta",null, new { style = "width:375px; height:33px;" })%>
                <%= Html.ValidationMessageFor(model => model.epre_pregunta) %>
            </div>
            <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioPreguntas") %>
    </div>

</asp:Content>

