<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.prioridad>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar Prioridad
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Prioridad</legend>
            
            <div>Codigo prioridad</div>
            <div>
                <%= Html.DropDownList("pri_id") %>
            </div>
            
            <div>Nombre</div>
            <div>
                <%= Html.TextBoxFor(model => model.pri_nombre) %>
                <%= Html.ValidationMessageFor(model => model.pri_nombre) %>
            </div>
            
            <p>
            <br />
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "iniciopri") %>
    </div>

</asp:Content>

