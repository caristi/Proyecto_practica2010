<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.cargos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Cargo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Editar Cargo</legend>
            
            <div>Nombre</div>
            <div>
                <%= Html.TextBoxFor(model => model.car_nombre, new { style = "width:380px;" })%>
                <%= Html.ValidationMessageFor(model => model.car_nombre) %>
            </div>
            <br />
            
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "iniciocargo") %>
    </div>

</asp:Content>

